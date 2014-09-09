Public Class MenuItem

#Region "Declarations"

    Public Event ItemClicked(ByVal sender As MenuItem)

#End Region

#Region "Properties"

    Private _Category As String
    Public Property Category() As String
        Get
            Return _Category
        End Get
        Set(ByVal value As String)
            _Category = value
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

    Private _Image As System.Drawing.Image
    Public Property Image() As System.Drawing.Image
        Get
            Return _Image
        End Get
        Set(ByVal value As System.Drawing.Image)
            _Image = value
        End Set
    End Property

    Private _Enabled As Boolean = True
    Public Property Enabled() As Boolean
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal category As String, _
                   ByVal displayName As String, _
                   ByVal image As System.Drawing.Image)

        MyBase.New()

        Me.Category = category
        Me.DisplayName = displayName
        Me.Image = image
    End Sub

#End Region

    Public Sub Click()
        RaiseEvent ItemClicked(Me)
    End Sub

End Class
