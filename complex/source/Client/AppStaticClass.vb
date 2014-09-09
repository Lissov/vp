Public Class AppStaticClass

    Private Shared _MainForm As MainForm
    Public Shared Property MainForm() As MainForm
        Get
            Return _MainForm
        End Get
        Set(ByVal value As MainForm)
            _MainForm = value
        End Set
    End Property

End Class
