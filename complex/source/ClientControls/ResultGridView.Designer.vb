<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResultGridView
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResultGridView))
        Me.ResultsGeridBottomPanel = New System.Windows.Forms.Panel
        Me.ResultsGrid = New System.Windows.Forms.DataGridView
        Me.AddModelValueToResultGridButton = New DevExpress.XtraEditors.SimpleButton
        Me.AddTimeToResultGridButton = New DevExpress.XtraEditors.SimpleButton
        Me.RemoveFromGridResultButton = New DevExpress.XtraEditors.SimpleButton
        Me.CopyGridResultButton = New DevExpress.XtraEditors.SimpleButton
        Me.SaveGridResultButton = New DevExpress.XtraEditors.SimpleButton
        Me.ResultsGeridBottomPanel.SuspendLayout()
        CType(Me.ResultsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ResultsGeridBottomPanel
        '
        Me.ResultsGeridBottomPanel.Controls.Add(Me.AddModelValueToResultGridButton)
        Me.ResultsGeridBottomPanel.Controls.Add(Me.AddTimeToResultGridButton)
        Me.ResultsGeridBottomPanel.Controls.Add(Me.RemoveFromGridResultButton)
        Me.ResultsGeridBottomPanel.Controls.Add(Me.CopyGridResultButton)
        Me.ResultsGeridBottomPanel.Controls.Add(Me.SaveGridResultButton)
        Me.ResultsGeridBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ResultsGeridBottomPanel.Location = New System.Drawing.Point(0, 430)
        Me.ResultsGeridBottomPanel.Name = "ResultsGeridBottomPanel"
        Me.ResultsGeridBottomPanel.Size = New System.Drawing.Size(521, 37)
        Me.ResultsGeridBottomPanel.TabIndex = 2
        '
        'ResultsGrid
        '
        Me.ResultsGrid.AllowUserToAddRows = False
        Me.ResultsGrid.AllowUserToDeleteRows = False
        Me.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ResultsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultsGrid.Location = New System.Drawing.Point(0, 0)
        Me.ResultsGrid.Name = "ResultsGrid"
        Me.ResultsGrid.Size = New System.Drawing.Size(521, 430)
        Me.ResultsGrid.TabIndex = 3
        '
        'AddModelValueToResultGridButton
        '
        Me.AddModelValueToResultGridButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddModelValueToResultGridButton.Image = Global.ClientControls.My.Resources.Resources.add_16x16
        Me.AddModelValueToResultGridButton.Location = New System.Drawing.Point(114, 10)
        Me.AddModelValueToResultGridButton.Name = "AddModelValueToResultGridButton"
        Me.AddModelValueToResultGridButton.Size = New System.Drawing.Size(79, 23)
        Me.AddModelValueToResultGridButton.TabIndex = 4
        Me.AddModelValueToResultGridButton.Text = "Add value"
        '
        'AddTimeToResultGridButton
        '
        Me.AddTimeToResultGridButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddTimeToResultGridButton.Image = Global.ClientControls.My.Resources.Resources.add_16x16
        Me.AddTimeToResultGridButton.Location = New System.Drawing.Point(199, 10)
        Me.AddTimeToResultGridButton.Name = "AddTimeToResultGridButton"
        Me.AddTimeToResultGridButton.Size = New System.Drawing.Size(75, 23)
        Me.AddTimeToResultGridButton.TabIndex = 3
        Me.AddTimeToResultGridButton.Text = "Add time"
        '
        'RemoveFromGridResultButton
        '
        Me.RemoveFromGridResultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveFromGridResultButton.Image = Global.ClientControls.My.Resources.Resources.delete_16x16
        Me.RemoveFromGridResultButton.Location = New System.Drawing.Point(280, 10)
        Me.RemoveFromGridResultButton.Name = "RemoveFromGridResultButton"
        Me.RemoveFromGridResultButton.Size = New System.Drawing.Size(80, 23)
        Me.RemoveFromGridResultButton.TabIndex = 2
        Me.RemoveFromGridResultButton.Text = "Remove all"
        '
        'CopyGridResultButton
        '
        Me.CopyGridResultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CopyGridResultButton.Image = CType(resources.GetObject("CopyGridResultButton.Image"), System.Drawing.Image)
        Me.CopyGridResultButton.Location = New System.Drawing.Point(365, 10)
        Me.CopyGridResultButton.Name = "CopyGridResultButton"
        Me.CopyGridResultButton.Size = New System.Drawing.Size(75, 23)
        Me.CopyGridResultButton.TabIndex = 1
        Me.CopyGridResultButton.Text = "Copy"
        '
        'SaveGridResultButton
        '
        Me.SaveGridResultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveGridResultButton.Image = Global.ClientControls.My.Resources.Resources.save_as2_blue_16x16
        Me.SaveGridResultButton.Location = New System.Drawing.Point(446, 10)
        Me.SaveGridResultButton.Name = "SaveGridResultButton"
        Me.SaveGridResultButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveGridResultButton.TabIndex = 0
        Me.SaveGridResultButton.Text = "Save"
        '
        'ResultGridView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.ResultsGrid)
        Me.Controls.Add(Me.ResultsGeridBottomPanel)
        Me.Name = "ResultGridView"
        Me.Size = New System.Drawing.Size(521, 467)
        Me.ResultsGeridBottomPanel.ResumeLayout(False)
        CType(Me.ResultsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ResultsGeridBottomPanel As System.Windows.Forms.Panel
    Friend WithEvents AddTimeToResultGridButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RemoveFromGridResultButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CopyGridResultButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SaveGridResultButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResultsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents AddModelValueToResultGridButton As DevExpress.XtraEditors.SimpleButton

End Class
