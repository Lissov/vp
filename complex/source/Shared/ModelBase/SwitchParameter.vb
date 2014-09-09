Public Class SwitchParameter
    Inherits ObjectBase

#Region "Events"

    Public Event ValueChanged(ByVal switchParameter As SwitchParameter)

#End Region

#Region "Properties"

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _DisplayName As String
    Public Property DisplayName() As String
        Get
            Return _DisplayName
        End Get
        Set(ByVal value As String)
            _DisplayName = value
        End Set
    End Property

    Private _Value As Boolean
    Public Overridable Property Value() As Boolean
        Get
            Return _Value
        End Get
        Set(ByVal value As Boolean)
            If _Value <> value Then
                _Value = value

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
            Return "SwitchParameter"
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

#End Region

#Region "Xml methods"

    Public Overloads Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
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
        If currentElement.Attributes("Value") IsNot Nothing Then
            Value = GetDouble(currentElement, "Value")
        End If

        Return Me
    End Function

#End Region

End Class
