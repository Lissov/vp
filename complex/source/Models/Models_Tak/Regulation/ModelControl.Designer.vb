<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModelControl
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
        Me.components = New System.ComponentModel.Container
        Me.MainTabControl = New System.Windows.Forms.TabControl
        Me.TabPageRegulators = New System.Windows.Forms.TabPage
        Me.RegulatorsGrid = New System.Windows.Forms.DataGridView
        Me.NameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ValueColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.RegulatorsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HormSetupPage = New System.Windows.Forms.TabPage
        Me.HifSetupPage = New System.Windows.Forms.TabPage
        Me.BSetupPage = New System.Windows.Forms.TabPage
        Me.DxSetupPage = New System.Windows.Forms.TabPage
        Me.MainTabControl.SuspendLayout()
        Me.TabPageRegulators.SuspendLayout()
        CType(Me.RegulatorsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RegulatorsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.TabPageRegulators)
        Me.MainTabControl.Controls.Add(Me.HormSetupPage)
        Me.MainTabControl.Controls.Add(Me.HifSetupPage)
        Me.MainTabControl.Controls.Add(Me.BSetupPage)
        Me.MainTabControl.Controls.Add(Me.DxSetupPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(500, 410)
        Me.MainTabControl.TabIndex = 0
        '
        'TabPageRegulators
        '
        Me.TabPageRegulators.Controls.Add(Me.RegulatorsGrid)
        Me.TabPageRegulators.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRegulators.Name = "TabPageRegulators"
        Me.TabPageRegulators.Padding = New System.Windows.Forms.Padding(10)
        Me.TabPageRegulators.Size = New System.Drawing.Size(492, 384)
        Me.TabPageRegulators.TabIndex = 0
        Me.TabPageRegulators.Text = "Regulators"
        Me.TabPageRegulators.UseVisualStyleBackColor = True
        '
        'RegulatorsGrid
        '
        Me.RegulatorsGrid.AutoGenerateColumns = False
        Me.RegulatorsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RegulatorsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameColumn, Me.ValueColumn})
        Me.RegulatorsGrid.DataSource = Me.RegulatorsBindingSource
        Me.RegulatorsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RegulatorsGrid.Location = New System.Drawing.Point(10, 10)
        Me.RegulatorsGrid.Name = "RegulatorsGrid"
        Me.RegulatorsGrid.RowHeadersVisible = False
        Me.RegulatorsGrid.Size = New System.Drawing.Size(472, 364)
        Me.RegulatorsGrid.TabIndex = 0
        '
        'NameColumn
        '
        Me.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NameColumn.DataPropertyName = "DisplayName"
        Me.NameColumn.HeaderText = "Name"
        Me.NameColumn.Name = "NameColumn"
        Me.NameColumn.ReadOnly = True
        '
        'ValueColumn
        '
        Me.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ValueColumn.DataPropertyName = "Value"
        Me.ValueColumn.HeaderText = "Value"
        Me.ValueColumn.Name = "ValueColumn"
        Me.ValueColumn.Width = 50
        '
        'HormSetupPage
        '
        Me.HormSetupPage.Location = New System.Drawing.Point(4, 22)
        Me.HormSetupPage.Name = "HormSetupPage"
        Me.HormSetupPage.Padding = New System.Windows.Forms.Padding(10)
        Me.HormSetupPage.Size = New System.Drawing.Size(492, 384)
        Me.HormSetupPage.TabIndex = 1
        Me.HormSetupPage.Text = "Setup Horm"
        Me.HormSetupPage.UseVisualStyleBackColor = True
        '
        'HifSetupPage
        '
        Me.HifSetupPage.Location = New System.Drawing.Point(4, 22)
        Me.HifSetupPage.Name = "HifSetupPage"
        Me.HifSetupPage.Padding = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.HifSetupPage.Size = New System.Drawing.Size(492, 384)
        Me.HifSetupPage.TabIndex = 2
        Me.HifSetupPage.Text = "Setup hif"
        Me.HifSetupPage.UseVisualStyleBackColor = True
        '
        'BSetupPage
        '
        Me.BSetupPage.Location = New System.Drawing.Point(4, 22)
        Me.BSetupPage.Name = "BSetupPage"
        Me.BSetupPage.Padding = New System.Windows.Forms.Padding(10)
        Me.BSetupPage.Size = New System.Drawing.Size(492, 384)
        Me.BSetupPage.TabIndex = 3
        Me.BSetupPage.Text = "Setup B function"
        Me.BSetupPage.UseVisualStyleBackColor = True
        '
        'DxSetupPage
        '
        Me.DxSetupPage.Location = New System.Drawing.Point(4, 22)
        Me.DxSetupPage.Name = "DxSetupPage"
        Me.DxSetupPage.Padding = New System.Windows.Forms.Padding(10)
        Me.DxSetupPage.Size = New System.Drawing.Size(492, 384)
        Me.DxSetupPage.TabIndex = 4
        Me.DxSetupPage.Text = "Setup Dx"
        Me.DxSetupPage.UseVisualStyleBackColor = True
        '
        'ModelControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainTabControl)
        Me.Name = "ModelControl"
        Me.Size = New System.Drawing.Size(500, 410)
        Me.MainTabControl.ResumeLayout(False)
        Me.TabPageRegulators.ResumeLayout(False)
        CType(Me.RegulatorsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RegulatorsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPageRegulators As System.Windows.Forms.TabPage
    Friend WithEvents HormSetupPage As System.Windows.Forms.TabPage
    Friend WithEvents RegulatorsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents NameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValueColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RegulatorsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents HifSetupPage As System.Windows.Forms.TabPage
    Friend WithEvents BSetupPage As System.Windows.Forms.TabPage
    Friend WithEvents DxSetupPage As System.Windows.Forms.TabPage

End Class
