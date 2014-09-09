Public Class MyApplication

    Private Shared _DataFolder As String = String.Empty
    Public Shared Property DataFolder() As String
        Get
            Return _DataFolder
        End Get
        Set(ByVal value As String)
            _DataFolder = value
        End Set
    End Property

End Class
