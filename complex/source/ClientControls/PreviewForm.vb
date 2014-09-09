Public Class PreviewForm

#Region "Properties"
    Private Property PreviewText() As String
        Get
            Return PreviewTextBox.Text
        End Get
        Set(ByVal value As String)
            PreviewTextBox.Text = value

            PreviewTextBox.DeselectAll()
        End Set
    End Property


#End Region

#Region "Constructors"

    Public Sub New(ByVal previewText As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.PreviewText = previewText
    End Sub

#End Region



#Region "Hot keys handlers"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Dim e As New KeyEventArgs(keyData)

        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class