<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddModelDialog
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddModelDialog))
        Me.CloseButton = New DevExpress.XtraEditors.SimpleButton
        Me.OkButton = New DevExpress.XtraEditors.SimpleButton
        Me.ModelsGrid = New System.Windows.Forms.DataGridView
        Me.NameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.ModelsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CloseButton.Location = New System.Drawing.Point(307, 443)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(75, 23)
        Me.CloseButton.TabIndex = 1
        Me.CloseButton.Text = "Cancel"
        '
        'OkButton
        '
        Me.OkButton.Location = New System.Drawing.Point(213, 443)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 0
        Me.OkButton.Text = "Ok"
        '
        'ModelsGrid
        '
        Me.ModelsGrid.AllowUserToAddRows = False
        Me.ModelsGrid.AllowUserToDeleteRows = False
        Me.ModelsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ModelsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameColumn})
        Me.ModelsGrid.Location = New System.Drawing.Point(12, 12)
        Me.ModelsGrid.Name = "ModelsGrid"
        Me.ModelsGrid.RowHeadersVisible = False
        Me.ModelsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ModelsGrid.Size = New System.Drawing.Size(370, 414)
        Me.ModelsGrid.TabIndex = 2
        '
        'NameColumn
        '
        Me.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NameColumn.HeaderText = "Name"
        Me.NameColumn.Name = "NameColumn"
        Me.NameColumn.ReadOnly = True
        '
        'AddModelDialog
        '
        Me.AcceptButton = Me.OkButton
        Me.CancelButton = Me.CloseButton
        Me.ClientSize = New System.Drawing.Size(398, 478)
        Me.Controls.Add(Me.ModelsGrid)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.CloseButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddModelDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add model"
        CType(Me.ModelsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LinkColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CloseButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OkButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ModelsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents NameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
