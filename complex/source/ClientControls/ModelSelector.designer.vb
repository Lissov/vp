Imports Microsoft.VisualBasic
Imports System

Partial Public Class ModelSelector

#Region "Designer generated code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModelSelector))
        Me.ButtonOk = New DevExpress.XtraEditors.SimpleButton
        Me.ButtonCancel = New DevExpress.XtraEditors.SimpleButton
        Me.ModelsTreeView = New ClientControls.ModelsTree
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(155, 404)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOk.TabIndex = 1
        Me.ButtonOk.Text = "OK"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(246, 404)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        '
        'ModelsTreeView
        '
        Me.ModelsTreeView.ContextMenuAllowed = False
        Me.ModelsTreeView.ImageIndex = 0
        Me.ModelsTreeView.IsSelectMode = True
        Me.ModelsTreeView.Location = New System.Drawing.Point(13, 13)
        Me.ModelsTreeView.Name = "ModelsTreeView"
        Me.ModelsTreeView.NameEditingAllowed = False
        Me.ModelsTreeView.SelectedImageIndex = 0
        Me.ModelsTreeView.ShowInputValues = True
        Me.ModelsTreeView.Size = New System.Drawing.Size(306, 376)
        Me.ModelsTreeView.TabIndex = 3
        '
        'ModelSelector
        '
        Me.AcceptButton = Me.ButtonOk
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(331, 439)
        Me.Controls.Add(Me.ModelsTreeView)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ModelSelector"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select model"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private components As System.ComponentModel.IContainer
    Friend WithEvents ButtonOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ButtonCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ModelsTreeView As ModelsTree
End Class

