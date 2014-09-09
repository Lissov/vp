Imports ModelBase

Public Class InputFunctionsList
    Inherits ModelBase.ObjectBase

#Region "Const"

    Public Const INPUT_EXTENSION As String = ".input"

    Public Const DICTIONARY_XML_ELEMENT As String = "ValuesDict"

#End Region

#Region "Properties"

    Private _ValuesDict As Dictionary(Of Value, InputFunction)
    Public Property ValuesDict() As Dictionary(Of Value, InputFunction)
        Get
            If _ValuesDict Is Nothing Then
                _ValuesDict = New Dictionary(Of Value, InputFunction)
            End If
            Return _ValuesDict
        End Get
        Set(ByVal value As Dictionary(Of Value, InputFunction))
            _ValuesDict = value
        End Set
    End Property

#Region "Preview"

    Private _StartTime As Double = 0
    ''' <summary>
    ''' Start time for preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartTime() As Double
        Get
            Return _StartTime
        End Get
        Set(ByVal value As Double)
            _StartTime = value
        End Set
    End Property

    Private _EndTime As Double = 100
    ''' <summary>
    ''' End time for preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndTime() As Double
        Get
            Return _EndTime
        End Get
        Set(ByVal value As Double)
            _EndTime = value
        End Set
    End Property

    Private _StepsCount As Integer = 1000
    ''' <summary>
    ''' Defines how many points will be shown on preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StepsCount() As Integer
        Get
            Return _StepsCount
        End Get
        Set(ByVal value As Integer)
            _StepsCount = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Returns name of the xml root for the class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property XmlName() As String
        Get
            Return "InputFunctionsList"
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal Values As List(Of Value))

        MyBase.New()

        If Values IsNot Nothing Then
            For Each Value As Value In Values
                ValuesDict.Add(Value, New InputFunction)
            Next

        End If

    End Sub

#End Region

#Region "Public methods"

    Public Function GetValueByName(ByVal valueName As String) As Value
        Dim Result As Value = Nothing

        For Each Value As Value In ValuesDict.Keys
            If Value.Name = valueName Then
                Result = Value
                Exit For
            End If
        Next

        Return Result
    End Function

    Public Function GetCalculatedValue(ByVal value As Value, ByVal time As Double)
        Dim InputFunction As InputFunction
        InputFunction = ValuesDict(value)

        Return InputFunction.GetCalculatedValue(time)
    End Function

#End Region

#Region "XML"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save properties
        SetAttribute(CurrentElement, "StartTime", StartTime)
        SetAttribute(CurrentElement, "EndTime", EndTime)
        SetAttribute(CurrentElement, "StepsCount", StepsCount)

        'save dictionary
        Dim DictElement As System.Xml.XmlElement
        DictElement = currentElement.OwnerDocument.CreateElement(DICTIONARY_XML_ELEMENT)
        currentElement.AppendChild(DictElement)
        For Each Value As Value In ValuesDict.Keys
            Dim InputFunction As InputFunction
            InputFunction = ValuesDict(Value)
            InputFunction.ToXml(DictElement, Value.Name)
        Next

        Return CurrentElement
    End Function

    Public Sub FromXmlDocument(ByVal xmlDocument As System.Xml.XmlDocument)
        If xmlDocument Is Nothing Then Return

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ModelBase.ObjectBase.ROOT_NAME Then Return

        FromXml(RootElement.Item(XmlName))
    End Sub

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        If currentElement Is Nothing OrElse _
           currentElement.Name <> XmlName _
           Then
            Return Nothing
        End If

        'load properties
        If currentElement.Attributes("StartTime") IsNot Nothing Then
            StartTime = GetDouble(currentElement, "StartTime")
        End If
        If currentElement.Attributes("EndTime") IsNot Nothing Then
            EndTime = GetDouble(currentElement, "EndTime")
        End If
        If currentElement.Attributes("StepsCount") IsNot Nothing Then
            StepsCount = GetInteger(currentElement, "StepsCount")
        End If

        'load dictionary
        Dim DictElement As System.Xml.XmlElement
        DictElement = currentElement.Item(DICTIONARY_XML_ELEMENT)
        If DictElement Is Nothing Then
            Throw New Exception("No items found")
        End If
        For Each childElement As System.Xml.XmlElement In DictElement.ChildNodes
            Dim ValueName As String = childElement.Name
            Dim Value As Value = GetValueByName(ValueName)
            If Value Is Nothing Then Continue For

            Dim InputFunction As InputFunction
            InputFunction = ValuesDict(Value)

            Try
                ValuesDict(Value) = InputFunction.FromXml(childElement.ChildNodes(0))
            Catch
            End Try
        Next

        Return Me
    End Function

#End Region

End Class
