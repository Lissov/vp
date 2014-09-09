Imports Microsoft.VisualBasic
Imports System

Partial Public Class ValuePropertyEditor

#Region "Designer generated code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ValuePropertyEditor))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar
        Me.CloseButtonItem = New DevExpress.XtraBars.BarButtonItem
        Me.SaveButtonItem = New DevExpress.XtraBars.BarButtonItem
        Me.Bar3 = New DevExpress.XtraBars.Bar
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
        Me.NameLabel = New System.Windows.Forms.Label
        Me.NameTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.GroupLabel = New System.Windows.Forms.Label
        Me.MeasureLabel = New System.Windows.Forms.Label
        Me.GroupNameTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.MeasureTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.ErrorProvider = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.TypeLabel = New System.Windows.Forms.Label
        Me.TypeComboBox = New DevExpress.XtraEditors.ComboBoxEdit
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MeasureTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TypeComboBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.SaveButtonItem, Me.CloseButtonItem})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 2
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CloseButtonItem), New DevExpress.XtraBars.LinkPersistInfo(Me.SaveButtonItem)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'CloseButtonItem
        '
        Me.CloseButtonItem.Caption = "Cancel"
        Me.CloseButtonItem.Glyph = Global.ClientControls.My.Resources.Resources.cancel_16x16
        Me.CloseButtonItem.Hint = "Cancel"
        Me.CloseButtonItem.Id = 1
        Me.CloseButtonItem.Name = "CloseButtonItem"
        '
        'SaveButtonItem
        '
        Me.SaveButtonItem.Caption = "Save"
        Me.SaveButtonItem.Glyph = Global.ClientControls.My.Resources.Resources.save_blue_16x16
        Me.SaveButtonItem.Hint = "Save"
        Me.SaveButtonItem.Id = 0
        Me.SaveButtonItem.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.SaveButtonItem.Name = "SaveButtonItem"
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.BackColor = System.Drawing.Color.Transparent
        Me.NameLabel.Location = New System.Drawing.Point(12, 49)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(38, 13)
        Me.NameLabel.TabIndex = 4
        Me.NameLabel.Text = "Name:"
        '
        'NameTextEdit
        '
        Me.NameTextEdit.Location = New System.Drawing.Point(87, 46)
        Me.NameTextEdit.Name = "NameTextEdit"
        Me.NameTextEdit.Size = New System.Drawing.Size(232, 20)
        Me.NameTextEdit.TabIndex = 5
        '
        'GroupLabel
        '
        Me.GroupLabel.AutoSize = True
        Me.GroupLabel.BackColor = System.Drawing.Color.Transparent
        Me.GroupLabel.Location = New System.Drawing.Point(12, 85)
        Me.GroupLabel.Name = "GroupLabel"
        Me.GroupLabel.Size = New System.Drawing.Size(69, 13)
        Me.GroupLabel.TabIndex = 6
        Me.GroupLabel.Text = "Group name:"
        '
        'MeasureLabel
        '
        Me.MeasureLabel.AutoSize = True
        Me.MeasureLabel.Location = New System.Drawing.Point(12, 122)
        Me.MeasureLabel.Name = "MeasureLabel"
        Me.MeasureLabel.Size = New System.Drawing.Size(52, 13)
        Me.MeasureLabel.TabIndex = 7
        Me.MeasureLabel.Text = "Measure:"
        '
        'GroupNameTextEdit
        '
        Me.GroupNameTextEdit.Location = New System.Drawing.Point(87, 82)
        Me.GroupNameTextEdit.Name = "GroupNameTextEdit"
        Me.GroupNameTextEdit.Size = New System.Drawing.Size(232, 20)
        Me.GroupNameTextEdit.TabIndex = 8
        '
        'MeasureTextEdit
        '
        Me.MeasureTextEdit.Location = New System.Drawing.Point(87, 119)
        Me.MeasureTextEdit.Name = "MeasureTextEdit"
        Me.MeasureTextEdit.Size = New System.Drawing.Size(232, 20)
        Me.MeasureTextEdit.TabIndex = 9
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'TypeLabel
        '
        Me.TypeLabel.AutoSize = True
        Me.TypeLabel.Location = New System.Drawing.Point(12, 161)
        Me.TypeLabel.Name = "TypeLabel"
        Me.TypeLabel.Size = New System.Drawing.Size(35, 13)
        Me.TypeLabel.TabIndex = 10
        Me.TypeLabel.Text = "Type:"
        Me.TypeLabel.Visible = False
        '
        'TypeComboBox
        '
        Me.TypeComboBox.Location = New System.Drawing.Point(87, 158)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.TypeComboBox.Size = New System.Drawing.Size(232, 20)
        Me.TypeComboBox.TabIndex = 11
        Me.TypeComboBox.Visible = False
        '
        'ValuePropertyEditor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(331, 230)
        Me.Controls.Add(Me.TypeComboBox)
        Me.Controls.Add(Me.TypeLabel)
        Me.Controls.Add(Me.MeasureTextEdit)
        Me.Controls.Add(Me.GroupNameTextEdit)
        Me.Controls.Add(Me.MeasureLabel)
        Me.Controls.Add(Me.GroupLabel)
        Me.Controls.Add(Me.NameTextEdit)
        Me.Controls.Add(Me.NameLabel)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ValuePropertyEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit properties of value"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupNameTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MeasureTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TypeComboBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private components As System.ComponentModel.IContainer
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents SaveButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CloseButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents NameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupLabel As System.Windows.Forms.Label
    Friend WithEvents MeasureLabel As System.Windows.Forms.Label
    Friend WithEvents MeasureTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupNameTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ErrorProvider As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents TypeLabel As System.Windows.Forms.Label
    Friend WithEvents TypeComboBox As DevExpress.XtraEditors.ComboBoxEdit
End Class

