Public Class Parameter
    Inherits ObjectBase

#Region "Events"

    Public Event ValueChanged(ByVal parameter As Parameter)

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

    Private _Value As Double = 0
    Public Overridable Property Value() As Double
        Get
            Return _Value
        End Get
        Set(ByVal value As Double)
            If _Value <> value Then
                _Value = value

                FixValue()
                RaiseEvent ValueChanged(Me)
            End If
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
            Return "Parameter"
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String)
        Me.New()

        Me.Name = name
        Me.DisplayName = displayName
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal minValue As Double, ByVal maxValue As Double)
        Me.New(name, displayName)

        Me.MinValue = minValue
        Me.MaxValue = maxValue
    End Sub

#End Region

#Region "Xml methods"

    Public Overloads Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
        SetAttribute(CurrentElement, "MinValue", MinValue)
        SetAttribute(CurrentElement, "MaxValue", MaxValue)
        SetAttribute(CurrentElement, "Value", Value)

        Return CurrentElement
    End Function

    Public Overloads Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        If currentElement.Attributes("Name") IsNot Nothing Then
            Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("DisplayName") IsNot Nothing Then
            DisplayName = GetString(currentElement, "DisplayName")
        End If
        If currentElement.Attributes("MinValue") IsNot Nothing Then
            MinValue = GetDouble(currentElement, "MinValue")
        End If
        If currentElement.Attributes("MaxValue") IsNot Nothing Then
            MaxValue = GetDouble(currentElement, "MaxValue")
        End If
        If currentElement.Attributes("Value") IsNot Nothing Then
            Value = GetDouble(currentElement, "Value")
        End If

        Return Me
    End Function

#End Region


#Region "Private methods"

    Private Sub FixValue()
        If Value > MaxValue Then
            Value = MaxValue
        ElseIf Value < MinValue Then
            Value = MinValue
        End If
    End Sub

#End Region

End Class
