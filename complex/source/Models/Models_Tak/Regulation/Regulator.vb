Public Class Regulator

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
    Public Property Value() As Boolean
        Get
            Return _Value
        End Get
        Set(ByVal value As Boolean)
            _Value = value
        End Set
    End Property

#End Region


#Region "Constructors"

    Private Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String)
        Me.New()

        Me.Name = name
        Me.DisplayName = displayName
    End Sub

    Public Sub New(ByVal name As String, ByVal displayName As String, ByVal value As Boolean)
        Me.New(name, displayName)

        Me.Value = value
    End Sub

#End Region


End Class
