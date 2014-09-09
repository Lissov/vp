Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading
Imports ModelBase.Enums
Imports System.Windows.Forms

Public Class EditableModel
    Inherits ObjectBase

#Region "Const"

    Public Const VALUES_XML_ELEMENT As String = "Values"
    Public Const PARAMETERS_XML_ELEMENT As String = "Parameters"
    Public Const SWITCH_PARAMETERS_XML_ELEMENT As String = "SwitchParameters"

    Public Const FILE_EXTENSION As String = ".mdl"

#End Region

#Region "Properties"

    Private _Values As List(Of Value)
    Public Property Values() As List(Of Value)
        Get
            If _Values Is Nothing Then
                _Values = New List(Of Value)
            End If
            Return _Values
        End Get
        Set(ByVal value As List(Of Value))
            _Values = value
        End Set
    End Property

    Private _Parameters As List(Of Parameter)
    Public Property Parameters() As List(Of Parameter)
        Get
            If _Parameters Is Nothing Then
                _Parameters = New List(Of Parameter)
            End If
            Return _Parameters
        End Get
        Set(ByVal value As List(Of Parameter))
            _Parameters = value
        End Set
    End Property

    Private _SwitchParameters As List(Of SwitchParameter)
    Public Property SwitchParameters() As List(Of SwitchParameter)
        Get
            If _SwitchParameters Is Nothing Then
                _SwitchParameters = New List(Of SwitchParameter)
            End If
            Return _SwitchParameters
        End Get
        Set(ByVal value As List(Of SwitchParameter))
            _SwitchParameters = value
        End Set
    End Property

    Private _DisplayName As String = String.Empty
    Public Property DisplayName() As String
        Get
            Return _DisplayName
        End Get
        Set(ByVal value As String)
            _DisplayName = value
        End Set
    End Property

    Private _Name As String = String.Empty
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Description As String = String.Empty
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _Equations As String = String.Empty
    Public Property Equations() As String
        Get
            If String.IsNullOrEmpty(_Equations) Then
                _Equations = String.Empty
            End If
            Return _Equations
        End Get
        Set(ByVal value As String)
            _Equations = value
        End Set
    End Property

    Private _FileName As String = String.Empty
    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns name of the xml root for the class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property XmlName() As String
        Get
            Return GetType(EditableModel).Name().Replace("`", "")
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        'CollectLists()
    End Sub

#End Region

#Region "Methods to work with collections"

    Private Function GetParameter(ByVal parameterName As String) As Parameter
        Dim Parameter As Parameter = Nothing

        Dim ParameterObject As Object = Nothing
        Try
            ParameterObject = Me.GetType.InvokeMember(parameterName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If ParameterObject IsNot Nothing AndAlso TypeOf ParameterObject Is Parameter Then
            Parameter = DirectCast(ParameterObject, Parameter)
        End If

        Return Parameter
    End Function

    Private Function GetSwitchParameter(ByVal switchParameterName As String) As SwitchParameter
        Dim SwitchParameter As SwitchParameter = Nothing

        Dim SwitchParameterObject As Object = Nothing
        Try
            SwitchParameterObject = Me.GetType.InvokeMember(switchParameterName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If SwitchParameterObject IsNot Nothing AndAlso TypeOf SwitchParameterObject Is SwitchParameter Then
            SwitchParameter = DirectCast(SwitchParameterObject, SwitchParameter)
        End If

        Return SwitchParameter
    End Function

    Public Function GetValue(ByVal valueName As String) As Value
        Dim Value As Value = Nothing

        Dim ValueObject As Object = Nothing
        Try
            ValueObject = Me.GetType.InvokeMember(valueName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If ValueObject IsNot Nothing AndAlso TypeOf ValueObject Is Value Then
            Value = DirectCast(ValueObject, Value)
        End If

        Return Value
    End Function

    Public Function GetValueByName(ByVal valueName As String) As Value
        Dim Result As Value = Nothing

        If _Values IsNot Nothing Then
            For Each Value As Value In Me.Values
                If Value.Name = valueName Then
                    Result = Value
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetParameterByName(ByVal parameterName As String) As Parameter
        Dim Result As Parameter = Nothing

        If Parameters IsNot Nothing AndAlso Parameters.Count > 0 Then
            For Each Parameter As Parameter In Parameters
                If Parameter.Name = parameterName Then
                    Result = Parameter
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetSwitchParameterByName(ByVal switchParameterName As String) As SwitchParameter
        Dim Result As SwitchParameter = Nothing

        If SwitchParameters IsNot Nothing AndAlso SwitchParameters.Count > 0 Then
            For Each SwitchParameter As SwitchParameter In SwitchParameters
                If SwitchParameter.Name = switchParameterName Then
                    Result = SwitchParameter
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

#End Region


#Region "Xml methods"

    ''' <summary>
    ''' Saves model to its file name
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Exeption-safe</remarks>
    Public Function Save(ByVal folderPath As String) As Boolean
        Dim Result As Boolean = True

        Try
            Dim XmlDocument As System.Xml.XmlDocument
            XmlDocument = Me.ToXmlDocument
            XmlDocument.Save(IO.Path.Combine(folderPath, Me.FileName & FILE_EXTENSION))
        Catch ex As Exception
            Result = False
        End Try

        Return Result
    End Function

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
        SetAttribute(CurrentElement, "Description", Description)
        SetAttribute(CurrentElement, "FileName", FileName)
        SetAttribute(CurrentElement, "Equations", Equations)

        'save values
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.OwnerDocument.CreateElement(VALUES_XML_ELEMENT)
        CurrentElement.AppendChild(ValuesElement)
        For Each Value As Value In Values
            Value.ToXml(ValuesElement, Value.Name)
        Next

        'save parameters
        SaveParametersToXml(CurrentElement)

        'save switch parameters
        SaveSwitchParametersToXml(CurrentElement)

        Return CurrentElement
    End Function

    Public Overloads Shared Function FromXml(ByVal xmlDocument As System.Xml.XmlDocument, ByVal fileName As String) As EditableModel
        If xmlDocument Is Nothing Then Return Nothing

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return Nothing

        Dim ElementName As String
        ElementName = GetType(EditableModel).Name().Replace("`", "")

        Return FromXmlElement(RootElement.Item(ElementName))
    End Function

    Public Shared Function FromXmlElement(ByVal currentElement As System.Xml.XmlElement) As EditableModel
        Dim EditableModel As New EditableModel
        Return EditableModel.FromXml(currentElement)
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        Dim EditableModel As EditableModel = Me

        If currentElement.Attributes("Name") IsNot Nothing Then
            EditableModel.Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("DisplayName") IsNot Nothing Then
            EditableModel.DisplayName = GetString(currentElement, "DisplayName")
        End If
        If currentElement.Attributes("Description") IsNot Nothing Then
            EditableModel.Description = GetString(currentElement, "Description")
        End If
        If currentElement.Attributes("FileName") IsNot Nothing Then
            EditableModel.FileName = GetString(currentElement, "FileName")
        End If
        If currentElement.Attributes("Equations") IsNot Nothing Then
            EditableModel.Equations = GetString(currentElement, "Equations")
        End If

        'load values
        Me.Values = New List(Of Value)
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = currentElement.Item(VALUES_XML_ELEMENT)
        If ValuesElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In ValuesElement.ChildNodes
                Dim Value As New Value()
                Value.FromXml(childElement, childElement.Name)
                Dim ValueElement As System.Xml.XmlElement = childElement.Item(Value.GetXmlName)
                Value.LoadTypeFromXml(ValueElement)
                Values.Add(Value)
            Next
        End If

        'load parameters
        LoadParametersFromXml(currentElement)

        'load switch parameters
        LoadSwitchParametersFromXml(currentElement)


        Return EditableModel
    End Function


#Region "Parameters"

    Private Sub SaveParametersToXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        'save parameters
        Dim ParametersToSave As List(Of Parameter) = Parameters()
        If ParametersToSave IsNot Nothing AndAlso ParametersToSave.Count > 0 Then
            Dim ParametersElement As System.Xml.XmlElement
            ParametersElement = currentElement.OwnerDocument.CreateElement(PARAMETERS_XML_ELEMENT)
            currentElement.AppendChild(ParametersElement)
            For Each Parameter As Parameter In ParametersToSave
                Parameter.ToXml(ParametersElement, Parameter.Name)
            Next
        End If
    End Sub

    Private Sub LoadParametersFromXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        Me.Parameters = New List(Of Parameter)
        Dim ParametersElement As System.Xml.XmlElement
        ParametersElement = currentElement.Item(PARAMETERS_XML_ELEMENT)
        If ParametersElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In ParametersElement.ChildNodes
                Dim Parameter As New Parameter
                Parameter = Parameter.FromXml(childElement, childElement.Name)
                Me.Parameters.Add(Parameter)
            Next
        End If

    End Sub

#End Region

#Region "Switch Parameters"

    Private Sub SaveSwitchParametersToXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        'save switch parameters
        Dim SwitchParametersToSave As List(Of SwitchParameter) = SwitchParameters()
        If SwitchParametersToSave IsNot Nothing AndAlso SwitchParametersToSave.Count > 0 Then
            Dim SwitchParametersElement As System.Xml.XmlElement
            SwitchParametersElement = currentElement.OwnerDocument.CreateElement(SWITCH_PARAMETERS_XML_ELEMENT)
            currentElement.AppendChild(SwitchParametersElement)
            For Each SwitchParameter As SwitchParameter In SwitchParametersToSave
                SwitchParameter.ToXml(SwitchParametersElement, SwitchParameter.Name)
            Next
        End If
    End Sub

    Private Sub LoadSwitchParametersFromXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        Me.SwitchParameters = New List(Of SwitchParameter)
        Dim SwitchParametersElement As System.Xml.XmlElement
        SwitchParametersElement = currentElement.Item(SWITCH_PARAMETERS_XML_ELEMENT)
        If SwitchParametersElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In SwitchParametersElement.ChildNodes
                Dim SwitchParameter As New SwitchParameter
                SwitchParameter = SwitchParameter.FromXml(childElement, childElement.Name)
                Me.SwitchParameters.Add(SwitchParameter)
            Next
        End If

    End Sub

#End Region

#End Region


End Class
