Imports ModelBase
Imports System.Collections.Generic
Imports System.Reflection

Public MustInherit Class FunctionBase
    Inherits ModelBase.ObjectBase

#Region "Const"

    Public Const PARAMETERS_XML_ELEMENT As String = "Parameters"

#End Region

#Region "Properties"

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
            If String.IsNullOrEmpty(_Name) Then
                _Name = Me.GetType.FullName
            End If
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Type() As InputFunction.FunctionTypes

    Public Overridable ReadOnly Property Image() As Image
        Get
            Return Nothing
        End Get
    End Property

    Private _Parameters As List(Of Parameter)
    Private Property Parameters() As List(Of Parameter)
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

    Private _IsLoaded As Boolean = False
    Public Property IsLoaded() As Boolean
        Get
            Return _IsLoaded
        End Get
        Set(ByVal value As Boolean)
            _IsLoaded = value
        End Set
    End Property

    ''' <summary>
    ''' If is not empty - this text will be shown in the text box 'formula' instead of showing the image
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property DisplayFormulaText() As String
        Get
            Return String.Empty
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

    End Sub

#End Region

#Region "Private methods"

    Public Sub CollectLists()

        Dim ParameterType As System.Type = GetType(Parameter)
        Dim Parameter As Parameter

        For Each ModelField As FieldInfo In Me.GetType.GetFields
            If ModelField.FieldType.FullName = ParameterType.FullName Then
                Parameter = GetParameter(ModelField.Name)
                If Parameter IsNot Nothing Then
                    Parameters.Add(Parameter)
                End If
            End If
        Next

        IsLoaded = True
    End Sub

    Public Function GetParameter(ByVal parameterName As String) As Parameter
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

#End Region

#Region "Public methods"

    Function GetParameters() As List(Of Parameter)
        If Not IsLoaded Then CollectLists()

        Return Parameters
    End Function

    Public Function GetParameterByName(ByVal parameterName As String) As Parameter
        Dim Result As Parameter = Nothing

        For Each Parameter As Parameter In Me.Parameters
            If Parameter.Name = parameterName Then
                Result = Parameter
                Exit For
            End If
        Next

        Return Result
    End Function

    Public MustOverride Function GetCalculatedValue(ByVal time As Double) As Double

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)


        'save parameters
        Dim ParametersElement As System.Xml.XmlElement
        ParametersElement = CurrentElement.OwnerDocument.CreateElement(PARAMETERS_XML_ELEMENT)
        CurrentElement.AppendChild(ParametersElement)
        For Each Parameter As Parameter In Parameters
            Parameter.ToXml(ParametersElement, Parameter.Name)
        Next

        Return CurrentElement
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        Dim FunctionBase As FunctionBase = Me

        If currentElement.Attributes("Name") IsNot Nothing Then
            FunctionBase.Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("DisplayName") IsNot Nothing Then
            FunctionBase.DisplayName = GetString(currentElement, "DisplayName")
        End If


        'load parameters
        If Me.GetParameters IsNot Nothing Then
            Dim ParametersElement As System.Xml.XmlElement
            ParametersElement = currentElement.Item(PARAMETERS_XML_ELEMENT)
            If ParametersElement IsNot Nothing Then
                For Each childElement As System.Xml.XmlElement In ParametersElement.ChildNodes
                    Dim Parameter As Parameter = GetParameterByName(childElement.Name)
                    If Parameter IsNot Nothing Then
                        Parameter = Parameter.FromXml(childElement, Parameter.Name)
                    End If
                Next
            End If
        End If

        Return FunctionBase
    End Function

#End Region

End Class
