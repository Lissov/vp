Imports Microsoft.VisualBasic
Imports System

Partial Public Class TimeSelector

#Region "Designer generated code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimeSelector))
        Me.ButtonOk = New DevExpress.XtraEditors.SimpleButton
        Me.ButtonCancel = New DevExpress.XtraEditors.SimpleButton
        Me.TimeTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.TimeLabel = New DevExpress.XtraEditors.LabelControl
        CType(Me.TimeTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(121, 91)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOk.TabIndex = 1
        Me.ButtonOk.Text = "OK"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(212, 91)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        '
        'TimeTextEdit
        '
        Me.TimeTextEdit.Location = New System.Drawing.Point(67, 33)
        Me.TimeTextEdit.Name = "TimeTextEdit"
        Me.TimeTextEdit.Properties.Mask.EditMask = "f"
        Me.TimeTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.TimeTextEdit.Size = New System.Drawing.Size(220, 20)
        Me.TimeTextEdit.TabIndex = 3
        '
        'TimeLabel
        '
        Me.TimeLabel.Location = New System.Drawing.Point(12, 36)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(49, 13)
        Me.TimeLabel.TabIndex = 4
        Me.TimeLabel.Text = "Time, sec:"
        '
        'TimeSelector
        '
        Me.AcceptButton = Me.ButtonOk
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(297, 123)
        Me.Controls.Add(Me.TimeLabel)
        Me.Controls.Add(Me.TimeTextEdit)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TimeSelector"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select time"
        CType(Me.TimeTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ButtonOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ButtonCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TimeTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TimeLabel As DevExpress.XtraEditors.LabelControl
End Class

