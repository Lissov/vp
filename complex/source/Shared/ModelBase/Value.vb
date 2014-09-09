Public Class Value
    Inherits ObjectBase

#Region "Const"

    Public Const RESULT_XML_ELEMENT As String = "Result"

#End Region

#Region "Enums"

    Public Enum ValueType As Integer
        Input = 0
        Internal = 1
        Output = 2
    End Enum

#End Region

#Region "Events"

    Public Event InitValueChanged(ByVal value As Value)

#End Region

#Region "Properties"

    Private _Name As String
    Public Property Name() As String
        Get
            If _Name Is Nothing Then _Name = String.Empty
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _DisplayName As String
    Public Property DisplayName() As String
        Get
            If _DisplayName Is Nothing Then _DisplayName = String.Empty
            Return _DisplayName
        End Get
        Set(ByVal value As String)
            _DisplayName = value
        End Set
    End Property

    Private _Measure As String
    Public Property Measure() As String
        Get
            If _Measure Is Nothing Then _Measure = String.Empty
            Return _Measure
        End Get
        Set(ByVal value As String)
            _Measure = value
        End Set
    End Property

    Private _GroupName As String = String.Empty
    ''' <summary>
    ''' Name of the group to which this value belongs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroupName() As String
        Get
            If _GroupName Is Nothing Then _GroupName = String.Empty
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
        End Set
    End Property

    Private _MinValue As Double = Double.MinValue
    Public Property MinValue() As Double
        Get
            Return _MinValue
        End Get
        Set(ByVal value As Double)
            _MinValue = value
        End Set
    End Property

    Private _MaxValue As Double = Double.MaxValue
    Public Property MaxValue() As Double
        Get
            Return _MaxValue
        End Get
        Set(ByVal value As Double)
            _MaxValue = value
        End Set
    End Property

    Private _InitValue As Double = 0
    Public Property InitValue() As Double
        Get
            Return _InitValue
        End Get
        Set(ByVal value As Double)
            If value <> _InitValue Then
                _InitValue = value

                FixInitValue()
                RaiseEvent InitValueChanged(Me)
            End If
        End Set
    End Property

    Private _Value As Double()
    Public Overridable Property Value() As Double()
        Get
            Return _Value
        End Get
        Set(ByVal value As Double())
            _Value = value
        End Set
    End Property

    Private _Type As ValueType = ValueType.Internal
    Public Property Type() As ValueType
        Get
            Return _Type
        End Get
        Set(ByVal value As ValueType)
            _Type = value
        End Set
    End Property

    Private _Visible As Boolean = False
    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
        End Set
    End Property

    Private _InitValueVisible As Boolean = True
    ''' <summary>
    ''' If true InitValue will be shown in the grid on main form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitValueVisible() As Boolean
        Get
            Return _InitValueVisible
        End Get
        Set(ByVal value As Boolean)
            _InitValueVisible = value
        End Set
    End Property

    Private _LinkModelName As String
    Public Property LinkModelName() As String
        Get
            Return _LinkModelName
        End Get
        Set(ByVal value As String)
            _LinkModelName = value
        End Set
    End Property

    Private _LinkValueName As String
    Public Property LinkValueName() As String
        Get
            Return _LinkValueName
        End Get
        Set(ByVal value As String)
            _LinkValueName = value
        End Set
    End Property

    Private _LinkConst As Double?
    Public Property LinkConst() As Double?
        Get
            Return _LinkConst
        End Get
        Set(ByVal value As Double?)
            _LinkConst = value
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
            Return "Value"
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType)
        Me.New()

        Me.Name = name
        Me.DisplayName = displayName
        Me.Type = type
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal Measure As String)
        Me.New(name, displayName, type)

        Me.Measure = Measure
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal minValue As Double, ByVal maxValue As Double)
        Me.New(name, displayName, type)

        Me.MinValue = minValue
        Me.MaxValue = maxValue
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal minValue As Double, ByVal maxValue As Double, ByVal groupName As String, ByVal initValueVisible As Boolean)
        Me.New(name, displayName, type, minValue, maxValue)

        Me.GroupName = groupName
        Me.InitValueVisible = initValueVisible
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal groupName As String, ByVal initValueVisible As Boolean)
        Me.New(name, displayName, type)

        Me.GroupName = groupName
        Me.InitValueVisible = initValueVisible
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal groupName As String, ByVal initValueVisible As Boolean, ByVal Measure As String)
        Me.New(name, displayName, type, groupName, initValueVisible)

        Me.Measure = Measure
    End Sub

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
        SetAttribute(CurrentElement, "Measure", Measure)
        SetAttribute(CurrentElement, "GroupName", GroupName)
        SetAttribute(CurrentElement, "MinValue", MinValue)
        SetAttribute(CurrentElement, "MaxValue", MaxValue)
        SetAttribute(CurrentElement, "InitValue", InitValue)
        SetAttribute(CurrentElement, "Type", Type)
        SetAttribute(CurrentElement, "Visible", Visible)
        SetAttribute(CurrentElement, "InitValueVisible", InitValueVisible)
        SetAttribute(CurrentElement, "LinkModelName", LinkModelName)
        SetAttribute(CurrentElement, "LinkValueName", LinkValueName)
        If LinkConst IsNot Nothing Then
            SetAttribute(CurrentElement, "LinkConst", LinkConst.Value)
        End If

        Return CurrentElement
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        If currentElement.Attributes("Name") IsNot Nothing Then
            Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("DisplayName") IsNot Nothing Then
            DisplayName = GetString(currentElement, "DisplayName")
        End If
        If currentElement.Attributes("Measure") IsNot Nothing Then
            Measure = GetString(currentElement, "Measure")
        End If
        If currentElement.Attributes("GroupName") IsNot Nothing Then
            GroupName = GetString(currentElement, "GroupName")
        End If
        If currentElement.Attributes("MinValue") IsNot Nothing Then
            MinValue = GetDouble(currentElement, "MinValue")
        End If
        If currentElement.Attributes("MaxValue") IsNot Nothing Then
            MaxValue = GetDouble(currentElement, "MaxValue")
        End If
        If currentElement.Attributes("InitValue") IsNot Nothing Then
            InitValue = GetDouble(currentElement, "InitValue")
        End If
        'If currentElement.Attributes("Type") IsNot Nothing Then
        '    Type = GetInteger(currentElement, "Type")
        'End If
        If currentElement.Attributes("Visible") IsNot Nothing Then
            Visible = GetBoolean(currentElement, "Visible")
        End If
        If currentElement.Attributes("InitValueVisible") IsNot Nothing Then
            InitValueVisible = GetBoolean(currentElement, "InitValueVisible")
        End If
        If currentElement.Attributes("LinkModelName") IsNot Nothing Then
            LinkModelName = GetString(currentElement, "LinkModelName")
        End If
        If currentElement.Attributes("LinkValueName") IsNot Nothing Then
            LinkValueName = GetString(currentElement, "LinkValueName")
        End If
        If currentElement.Attributes("LinkConst") IsNot Nothing Then
            LinkConst = GetDouble(currentElement, "LinkConst")
        Else
            LinkConst = Nothing
        End If

        Return Me
    End Function

    Public Sub LoadTypeFromXml(ByVal currentElement As System.Xml.XmlElement)
        If currentElement.Attributes("Type") IsNot Nothing Then
            Type = GetInteger(currentElement, "Type")
        End If
    End Sub


#Region "Result"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parentElement"></param>
    ''' <param name="name"></param>
    ''' <param name="saveStepNumber">Each saveStepNumber value should be saved</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResultToXml(ByVal parentElement As System.Xml.XmlElement, _
                                ByVal name As String, _
                                ByVal saveStepNumber As Integer) _
                                    As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        Me.ResultToXml(CurrentElement, saveStepNumber)
        Return CurrentElement
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parentElement"></param>
    ''' <param name="saveStepNumber">Each saveStepNumber value should be saved</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function ResultToXml(ByVal parentElement As System.Xml.XmlElement, _
                                            ByVal saveStepNumber As Integer) _
                                                    As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = Me.ToXml(parentElement)

        'save result
        If Value IsNot Nothing Then
            Dim ValuesElement As System.Xml.XmlElement
            ValuesElement = CurrentElement.OwnerDocument.CreateElement(RESULT_XML_ELEMENT)
            CurrentElement.AppendChild(ValuesElement)
            For Index As Integer = 0 To Value.Length - 1
                If Index Mod saveStepNumber = 0 Then
                    SetAttribute(ValuesElement, "ResultValue_" & Index.ToString, Value(Index))
                End If
            Next
        End If

        Return CurrentElement
    End Function

    Public Sub ResultFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String)
        If parentElement Is Nothing OrElse parentElement.Name <> name Then Return

        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = parentElement.Item(XmlName)

        Me.FromXml(CurrentElement)

        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.Item(RESULT_XML_ELEMENT)
        If ValuesElement IsNot Nothing Then
            Dim ValueLength As Integer = ValuesElement.Attributes.Count
            ReDim Value(ValueLength)
            For i As Integer = 0 To ValueLength - 1
                Value(i) = GetDouble(ValuesElement, ValuesElement.Attributes(i).Name)
            Next
        End If

    End Sub

#End Region

#Region "Selection"

    Public Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        Me.SelectionToXml(CurrentElement)
        Return CurrentElement
    End Function

    Public Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Visible", Visible)

        Return CurrentElement
    End Function


    Public Sub SelectionFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String)
        If parentElement Is Nothing OrElse parentElement.Name <> name Then Return

        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = parentElement.Item(XmlName)

        If CurrentElement.Attributes("Visible") IsNot Nothing Then
            Visible = GetBoolean(CurrentElement, "Visible")
        End If
    End Sub

#End Region

    Public Function GetXmlName() As String
        Return XmlName
    End Function

#End Region


#Region "Private methods"

    Private Sub FixInitValue()
        If InitValue > MaxValue Then
            InitValue = MaxValue
        ElseIf InitValue < MinValue Then
            InitValue = MinValue
        End If
    End Sub

#End Region


#Region "Public methods"

    Public Function FixValue(ByVal value As Double) As Double
        If value > MaxValue Then
            value = MaxValue
        ElseIf value < MinValue Then
            value = MinValue
        End If

        Return value
    End Function

#End Region

End Class
