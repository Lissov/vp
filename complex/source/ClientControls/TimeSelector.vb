Imports DevExpress.XtraEditors

Public Class TimeSelector
    Inherits DevExpress.XtraEditors.XtraForm

#Region "Properties"

    ''' <summary>
    ''' Ruterns selected time
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedTime() As Decimal
        Get
            Dim NewTime As Double
            Decimal.TryParse(TimeTextEdit.Text, NewTime)
            Return NewTime
        End Get
    End Property


#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region

#Region "Private helping methods"

    Private Sub CheckButtons()
        ButtonOk.Enabled = IsTimeCorrect()
    End Sub

    Private Function IsTimeCorrect() As Boolean
        Dim NewTime As Double

        Return Decimal.TryParse(TimeTextEdit.Text, NewTime)
    End Function

#End Region


#Region "Events"


    Private Sub TimeTextEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeTextEdit.EditValueChanged
        CheckButtons()
    End Sub

    Private Sub TimeTextEdit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TimeTextEdit.KeyDown
        If e.KeyCode = Keys.Enter AndAlso ButtonOk.Enabled Then
            ButtonOk_Click(ButtonOk, Nothing)
        End If
    End Sub

    Private Sub TimeSelector_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        TimeTextEdit.Focus()
    End Sub

#End Region



#Region "Exit"

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region


End Class

