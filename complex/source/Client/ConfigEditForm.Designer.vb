<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigEditForm
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigEditForm))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.StatusBar = New DevExpress.XtraBars.Bar
        Me.ActionBar = New DevExpress.XtraBars.Bar
        Me.CloseButton = New DevExpress.XtraBars.BarButtonItem
        Me.SaveButton = New DevExpress.XtraBars.BarButtonItem
        Me.SaveASButton = New DevExpress.XtraBars.BarButtonItem
        Me.EditBar = New DevExpress.XtraBars.Bar
        Me.AddModelButton = New DevExpress.XtraBars.BarButtonItem
        Me.RemoveModelButton = New DevExpress.XtraBars.BarButtonItem
        Me.ViewGraphButton = New DevExpress.XtraBars.BarButtonItem
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.TopPanel = New DevExpress.XtraEditors.PanelControl
        Me.DescriptionEditButton = New DevExpress.XtraEditors.SimpleButton
        Me.DescriptionTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.DescriptionLabel = New DevExpress.XtraEditors.LabelControl
        Me.NameTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.NameLabel = New DevExpress.XtraEditors.LabelControl
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MainLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.LinksGrid = New System.Windows.Forms.DataGridView
        Me.ModelNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.InputValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinkColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ModelsGrid = New System.Windows.Forms.DataGridView
        Me.ModelColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinksLabel = New DevExpress.XtraEditors.LabelControl
        Me.ModelsLabel = New DevExpress.XtraEditors.LabelControl
        Me.LinksMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TopPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TopPanel.SuspendLayout()
        CType(Me.DescriptionTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainLayoutPanel.SuspendLayout()
        CType(Me.LinksGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModelsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.StatusBar, Me.ActionBar, Me.EditBar})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.AddModelButton, Me.RemoveModelButton, Me.SaveButton, Me.SaveASButton, Me.CloseButton, Me.ViewGraphButton})
        Me.BarManager1.MaxItemId = 7
        Me.BarManager1.StatusBar = Me.StatusBar
        '
        'StatusBar
        '
        Me.StatusBar.BarName = "Custom 3"
        Me.StatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.StatusBar.DockCol = 0
        Me.StatusBar.DockRow = 0
        Me.StatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.StatusBar.OptionsBar.AllowQuickCustomization = False
        Me.StatusBar.OptionsBar.DrawDragBorder = False
        Me.StatusBar.OptionsBar.UseWholeRow = True
        Me.StatusBar.Text = "Custom 3"
        '
        'ActionBar
        '
        Me.ActionBar.BarName = "ActionBar"
        Me.ActionBar.DockCol = 0
        Me.ActionBar.DockRow = 0
        Me.ActionBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.ActionBar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CloseButton), New DevExpress.XtraBars.LinkPersistInfo(Me.SaveButton), New DevExpress.XtraBars.LinkPersistInfo(Me.SaveASButton)})
        Me.ActionBar.Text = "Custom 4"
        '
        'CloseButton
        '
        Me.CloseButton.Caption = "Cancel"
        Me.CloseButton.Glyph = Global.Client.My.Resources.Resources.cancel_24x24
        Me.CloseButton.Hint = "Cancel"
        Me.CloseButton.Id = 5
        Me.CloseButton.Name = "CloseButton"
        '
        'SaveButton
        '
        Me.SaveButton.Caption = "Save"
        Me.SaveButton.Glyph = Global.Client.My.Resources.Resources.save_blue_24x24
        Me.SaveButton.Hint = "Save"
        Me.SaveButton.Id = 2
        Me.SaveButton.Name = "SaveButton"
        '
        'SaveASButton
        '
        Me.SaveASButton.Caption = "Save as"
        Me.SaveASButton.Glyph = Global.Client.My.Resources.Resources.save_as2_blue_24x24
        Me.SaveASButton.Hint = "Save as"
        Me.SaveASButton.Id = 3
        Me.SaveASButton.Name = "SaveASButton"
        '
        'EditBar
        '
        Me.EditBar.BarName = "EditBar"
        Me.EditBar.DockCol = 1
        Me.EditBar.DockRow = 0
        Me.EditBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.EditBar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.AddModelButton), New DevExpress.XtraBars.LinkPersistInfo(Me.RemoveModelButton), New DevExpress.XtraBars.LinkPersistInfo(Me.ViewGraphButton)})
        Me.EditBar.Text = "Custom 5"
        '
        'AddModelButton
        '
        Me.AddModelButton.Caption = "Add model "
        Me.AddModelButton.Glyph = Global.Client.My.Resources.Resources.add_24x24
        Me.AddModelButton.Hint = "Add model "
        Me.AddModelButton.Id = 0
        Me.AddModelButton.Name = "AddModelButton"
        '
        'RemoveModelButton
        '
        Me.RemoveModelButton.Caption = "Remove model"
        Me.RemoveModelButton.Glyph = Global.Client.My.Resources.Resources.delete_24X24
        Me.RemoveModelButton.Hint = "Remove model"
        Me.RemoveModelButton.Id = 1
        Me.RemoveModelButton.Name = "RemoveModelButton"
        '
        'ViewGraphButton
        '
        Me.ViewGraphButton.Glyph = Global.Client.My.Resources.Resources.view_24x24
        Me.ViewGraphButton.Hint = "View graph"
        Me.ViewGraphButton.Id = 6
        Me.ViewGraphButton.Name = "ViewGraphButton"
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.DescriptionEditButton)
        Me.TopPanel.Controls.Add(Me.DescriptionTextEdit)
        Me.TopPanel.Controls.Add(Me.DescriptionLabel)
        Me.TopPanel.Controls.Add(Me.NameTextEdit)
        Me.TopPanel.Controls.Add(Me.NameLabel)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 34)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(781, 90)
        Me.TopPanel.TabIndex = 4
        '
        'DescriptionEditButton
        '
        Me.DescriptionEditButton.Location = New System.Drawing.Point(713, 54)
        Me.DescriptionEditButton.Name = "DescriptionEditButton"
        Me.DescriptionEditButton.Size = New System.Drawing.Size(40, 20)
        Me.DescriptionEditButton.TabIndex = 4
        Me.DescriptionEditButton.Text = "..."
        '
        'DescriptionTextEdit
        '
        Me.DescriptionTextEdit.Location = New System.Drawing.Point(128, 54)
        Me.DescriptionTextEdit.Name = "DescriptionTextEdit"
        Me.DescriptionTextEdit.Properties.ReadOnly = True
        Me.DescriptionTextEdit.Size = New System.Drawing.Size(578, 20)
        Me.DescriptionTextEdit.TabIndex = 3
        '
        'DescriptionLabel
        '
        Me.DescriptionLabel.Location = New System.Drawing.Point(12, 57)
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.DescriptionLabel.Size = New System.Drawing.Size(57, 13)
        Me.DescriptionLabel.TabIndex = 2
        Me.DescriptionLabel.Text = "Description:"
        '
        'NameTextEdit
        '
        Me.NameTextEdit.Location = New System.Drawing.Point(128, 20)
        Me.NameTextEdit.Name = "NameTextEdit"
        Me.NameTextEdit.Size = New System.Drawing.Size(625, 20)
        Me.NameTextEdit.TabIndex = 1
        '
        'NameLabel
        '
        Me.NameLabel.Location = New System.Drawing.Point(12, 23)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(98, 13)
        Me.NameLabel.TabIndex = 0
        Me.NameLabel.Text = "Configuration name:"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'MainLayoutPanel
        '
        Me.MainLayoutPanel.ColumnCount = 2
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.MainLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.MainLayoutPanel.Controls.Add(Me.LinksGrid, 0, 3)
        Me.MainLayoutPanel.Controls.Add(Me.ModelsGrid, 0, 1)
        Me.MainLayoutPanel.Controls.Add(Me.LinksLabel, 0, 2)
        Me.MainLayoutPanel.Controls.Add(Me.ModelsLabel, 0, 0)
        Me.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLayoutPanel.Location = New System.Drawing.Point(0, 124)
        Me.MainLayoutPanel.Name = "MainLayoutPanel"
        Me.MainLayoutPanel.RowCount = 4
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.MainLayoutPanel.Size = New System.Drawing.Size(781, 370)
        Me.MainLayoutPanel.TabIndex = 5
        '
        'LinksGrid
        '
        Me.LinksGrid.AllowUserToAddRows = False
        Me.LinksGrid.AllowUserToDeleteRows = False
        Me.LinksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.LinksGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ModelNameColumn, Me.InputValueColumn, Me.LinkColumn})
        Me.LinksGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LinksGrid.Location = New System.Drawing.Point(3, 208)
        Me.LinksGrid.MinimumSize = New System.Drawing.Size(750, 162)
        Me.LinksGrid.Name = "LinksGrid"
        Me.LinksGrid.RowHeadersVisible = False
        Me.LinksGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.LinksGrid.Size = New System.Drawing.Size(750, 162)
        Me.LinksGrid.TabIndex = 8
        '
        'ModelNameColumn
        '
        Me.ModelNameColumn.HeaderText = "Model"
        Me.ModelNameColumn.Name = "ModelNameColumn"
        Me.ModelNameColumn.ReadOnly = True
        Me.ModelNameColumn.Width = 150
        '
        'InputValueColumn
        '
        Me.InputValueColumn.HeaderText = "Input value"
        Me.InputValueColumn.Name = "InputValueColumn"
        Me.InputValueColumn.ReadOnly = True
        Me.InputValueColumn.Width = 150
        '
        'LinkColumn
        '
        Me.LinkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LinkColumn.HeaderText = "Link"
        Me.LinkColumn.Name = "LinkColumn"
        '
        'ModelsGrid
        '
        Me.ModelsGrid.AllowUserToAddRows = False
        Me.ModelsGrid.AllowUserToDeleteRows = False
        Me.ModelsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ModelsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ModelColumn})
        Me.ModelsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModelsGrid.Location = New System.Drawing.Point(3, 23)
        Me.ModelsGrid.MinimumSize = New System.Drawing.Size(750, 162)
        Me.ModelsGrid.Name = "ModelsGrid"
        Me.ModelsGrid.RowHeadersVisible = False
        Me.ModelsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ModelsGrid.Size = New System.Drawing.Size(750, 162)
        Me.ModelsGrid.TabIndex = 6
        '
        'ModelColumn
        '
        Me.ModelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ModelColumn.HeaderText = "Model"
        Me.ModelColumn.Name = "ModelColumn"
        Me.ModelColumn.ReadOnly = True
        '
        'LinksLabel
        '
        Me.LinksLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LinksLabel.Location = New System.Drawing.Point(3, 188)
        Me.LinksLabel.Name = "LinksLabel"
        Me.LinksLabel.Size = New System.Drawing.Size(27, 13)
        Me.LinksLabel.TabIndex = 7
        Me.LinksLabel.Text = "Links:"
        '
        'ModelsLabel
        '
        Me.ModelsLabel.Location = New System.Drawing.Point(3, 3)
        Me.ModelsLabel.Name = "ModelsLabel"
        Me.ModelsLabel.Size = New System.Drawing.Size(37, 13)
        Me.ModelsLabel.TabIndex = 0
        Me.ModelsLabel.Text = "Models:"
        '
        'LinksMenu
        '
        Me.LinksMenu.Name = "LinksMenu"
        Me.LinksMenu.Size = New System.Drawing.Size(61, 4)
        '
        'ConfigEditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 516)
        Me.Controls.Add(Me.MainLayoutPanel)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(789, 550)
        Me.Name = "ConfigEditForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit configuration"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TopPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        CType(Me.DescriptionTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainLayoutPanel.ResumeLayout(False)
        Me.MainLayoutPanel.PerformLayout()
        CType(Me.LinksGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModelsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents AddModelButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RemoveModelButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SaveButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SaveASButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TopPanel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents NameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents NameLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents StatusBar As DevExpress.XtraBars.Bar
    Friend WithEvents ActionBar As DevExpress.XtraBars.Bar
    Friend WithEvents EditBar As DevExpress.XtraBars.Bar
    Friend WithEvents CloseButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents MainLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ModelsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ModelsLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LinksLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LinksGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ModelColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ModelNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InputValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinksMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DescriptionEditButton As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DescriptionTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DescriptionLabel As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ViewGraphButton As DevExpress.XtraBars.BarButtonItem
End Class
