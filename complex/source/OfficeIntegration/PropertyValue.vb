Public Class PropertyValue

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

    Private _Value As Object
    Public Property Value() As Object
        Get
            Return _Value
        End Get
        Set(ByVal value As Object)
            _Value = value
        End Set
    End Property

    Private _ValueType As OfficeDocument.ValueType
    Public Property ValueType() As OfficeDocument.ValueType
        Get
            Return _ValueType
        End Get
        Set(ByVal value As OfficeDocument.ValueType)
            _ValueType = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

    End Sub

    Public Sub New(ByVal name As String, _
                   ByVal value As Object, _
                   ByVal valueType As OfficeDocument.ValueType)
        Me.New()

        Me.Name = name
        Me.Value = value
        Me.ValueType = valueType
    End Sub

#End Region

End Class
