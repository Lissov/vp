Public Class Edge

#Region "Const"

    Private Const KEY_LINK As String = "@@##@@" & Chr(10) & Chr(13)

#End Region

#Region "Properties"

    Private _Source As String = String.Empty
    Public Property Source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property

    Private _Target As String = String.Empty
    Public Property Target() As String
        Get
            Return _Target
        End Get
        Set(ByVal value As String)
            _Target = value
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

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(ByVal source As String, ByVal target As String)
        Me.New()

        Me.Source = source
        Me.Target = target
    End Sub

    Public Sub New(ByVal source As String, ByVal target As String, ByVal name As String)
        Me.New(source, target)

        Me.Name = name
    End Sub

#End Region

#Region "Public methods"

    Public Function GetKey() As String
        Return GetKey(Source, Target)
    End Function

#End Region

#Region "Public shared methods"

    Public Shared Function GetKey(ByVal source As String, ByVal target As String) As String
        Return source & KEY_LINK & target
    End Function

#End Region


End Class
