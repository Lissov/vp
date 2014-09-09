<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomizeForm
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("tset1")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("tset2")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("tset1")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("tset2")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomizeForm))
        Me.CustomizePanelsTabPage = New DevExpress.XtraTab.XtraTabPage
        Me.CustomizePanelsListView = New System.Windows.Forms.ListView
        Me.TabControl = New DevExpress.XtraTab.XtraTabControl
        Me.CustomizeButtonsTabPage = New DevExpress.XtraTab.XtraTabPage
        Me.CustomizeButtonsListView = New System.Windows.Forms.ListView
        Me.ViewTabPage = New DevExpress.XtraTab.XtraTabPage
        Me.HintGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.HintColorEdit = New DevExpress.XtraEditors.ColorEdit
        Me.HintBevelCheckEdit = New DevExpress.XtraEditors.CheckEdit
        Me.HintBevelLabel = New System.Windows.Forms.Label
        Me.HintColorLabel = New System.Windows.Forms.Label
        Me.FontGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.FontSizeComboBox = New DevExpress.XtraEditors.ComboBoxEdit
        Me.FontNameComboBox = New DevExpress.XtraEditors.FontEdit
        Me.FontSizeLabel = New System.Windows.Forms.Label
        Me.FontNameLabel = New System.Windows.Forms.Label
        Me.LookGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.StyleLabel = New System.Windows.Forms.Label
        Me.StyleComboBox = New DevExpress.XtraEditors.ImageComboBoxEdit
        Me.StyleImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SystemTabPage = New DevExpress.XtraTab.XtraTabPage
        Me.ChartGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.MaxPointsPerSecTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.MaxPointsPerSecLabel = New System.Windows.Forms.Label
        Me.SaveTabPage = New DevExpress.XtraTab.XtraTabPage
        Me.SaveReportGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.Report_SaveOnlyVisiblePoints_CheckEdit = New DevExpress.XtraEditors.CheckEdit
        Me.SaveImageGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.SaveImageTypeLabel = New System.Windows.Forms.Label
        Me.SaveImageTypeComboBox = New DevExpress.XtraEditors.ImageComboBoxEdit
        Me.SaveResultGroupControl = New DevExpress.XtraEditors.GroupControl
        Me.Result_SaveOnlyVisiblePoints_CheckEdit = New DevExpress.XtraEditors.CheckEdit
        Me.MaxMarksCountTextEdit = New DevExpress.XtraEditors.TextEdit
        Me.MaxMarksCountLabel = New System.Windows.Forms.Label
        Me.CustomizePanelsTabPage.SuspendLayout()
        CType(Me.TabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.CustomizeButtonsTabPage.SuspendLayout()
        Me.ViewTabPage.SuspendLayout()
        CType(Me.HintGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HintGroupControl.SuspendLayout()
        CType(Me.HintColorEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HintBevelCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FontGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FontGroupControl.SuspendLayout()
        CType(Me.FontSizeComboBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FontNameComboBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LookGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LookGroupControl.SuspendLayout()
        CType(Me.StyleComboBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SystemTabPage.SuspendLayout()
        CType(Me.ChartGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ChartGroupControl.SuspendLayout()
        CType(Me.MaxPointsPerSecTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SaveTabPage.SuspendLayout()
        CType(Me.SaveReportGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SaveReportGroupControl.SuspendLayout()
        CType(Me.Report_SaveOnlyVisiblePoints_CheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SaveImageGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SaveImageGroupControl.SuspendLayout()
        CType(Me.SaveImageTypeComboBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SaveResultGroupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SaveResultGroupControl.SuspendLayout()
        CType(Me.Result_SaveOnlyVisiblePoints_CheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxMarksCountTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CustomizePanelsTabPage
        '
        Me.CustomizePanelsTabPage.Controls.Add(Me.CustomizePanelsListView)
        Me.CustomizePanelsTabPage.Name = "CustomizePanelsTabPage"
        Me.CustomizePanelsTabPage.Padding = New System.Windows.Forms.Padding(10)
        Me.CustomizePanelsTabPage.Size = New System.Drawing.Size(458, 463)
        Me.CustomizePanelsTabPage.Text = "Customize panels"
        '
        'CustomizePanelsListView
        '
        Me.CustomizePanelsListView.CheckBoxes = True
        Me.CustomizePanelsListView.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.StateImageIndex = 0
        Me.CustomizePanelsListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2})
        Me.CustomizePanelsListView.Location = New System.Drawing.Point(10, 10)
        Me.CustomizePanelsListView.Name = "CustomizePanelsListView"
        Me.CustomizePanelsListView.Size = New System.Drawing.Size(438, 443)
        Me.CustomizePanelsListView.TabIndex = 1
        Me.CustomizePanelsListView.UseCompatibleStateImageBehavior = False
        Me.CustomizePanelsListView.View = System.Windows.Forms.View.List
        '
        'TabControl
        '
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 0)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedTabPage = Me.CustomizePanelsTabPage
        Me.TabControl.Size = New System.Drawing.Size(467, 494)
        Me.TabControl.TabIndex = 1
        Me.TabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.CustomizePanelsTabPage, Me.CustomizeButtonsTabPage, Me.ViewTabPage, Me.SystemTabPage, Me.SaveTabPage})
        '
        'CustomizeButtonsTabPage
        '
        Me.CustomizeButtonsTabPage.Controls.Add(Me.CustomizeButtonsListView)
        Me.CustomizeButtonsTabPage.Name = "CustomizeButtonsTabPage"
        Me.CustomizeButtonsTabPage.Padding = New System.Windows.Forms.Padding(10)
        Me.CustomizeButtonsTabPage.Size = New System.Drawing.Size(458, 463)
        Me.CustomizeButtonsTabPage.Text = "Customize buttons"
        '
        'CustomizeButtonsListView
        '
        Me.CustomizeButtonsListView.CheckBoxes = True
        Me.CustomizeButtonsListView.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.StateImageIndex = 0
        Me.CustomizeButtonsListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem3, ListViewItem4})
        Me.CustomizeButtonsListView.Location = New System.Drawing.Point(10, 10)
        Me.CustomizeButtonsListView.Name = "CustomizeButtonsListView"
        Me.CustomizeButtonsListView.Size = New System.Drawing.Size(438, 443)
        Me.CustomizeButtonsListView.TabIndex = 2
        Me.CustomizeButtonsListView.UseCompatibleStateImageBehavior = False
        Me.CustomizeButtonsListView.View = System.Windows.Forms.View.List
        '
        'ViewTabPage
        '
        Me.ViewTabPage.Controls.Add(Me.HintGroupControl)
        Me.ViewTabPage.Controls.Add(Me.FontGroupControl)
        Me.ViewTabPage.Controls.Add(Me.LookGroupControl)
        Me.ViewTabPage.Name = "ViewTabPage"
        Me.ViewTabPage.Size = New System.Drawing.Size(458, 463)
        Me.ViewTabPage.Text = "View"
        '
        'HintGroupControl
        '
        Me.HintGroupControl.Controls.Add(Me.HintColorEdit)
        Me.HintGroupControl.Controls.Add(Me.HintBevelCheckEdit)
        Me.HintGroupControl.Controls.Add(Me.HintBevelLabel)
        Me.HintGroupControl.Controls.Add(Me.HintColorLabel)
        Me.HintGroupControl.Location = New System.Drawing.Point(9, 211)
        Me.HintGroupControl.Name = "HintGroupControl"
        Me.HintGroupControl.Size = New System.Drawing.Size(440, 99)
        Me.HintGroupControl.TabIndex = 22
        Me.HintGroupControl.Text = "Hint"
        '
        'HintColorEdit
        '
        Me.HintColorEdit.EditValue = System.Drawing.Color.Empty
        Me.HintColorEdit.Location = New System.Drawing.Point(73, 30)
        Me.HintColorEdit.Name = "HintColorEdit"
        Me.HintColorEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.HintColorEdit.ShowToolTips = False
        Me.HintColorEdit.Size = New System.Drawing.Size(178, 20)
        Me.HintColorEdit.TabIndex = 4
        '
        'HintBevelCheckEdit
        '
        Me.HintBevelCheckEdit.Location = New System.Drawing.Point(71, 64)
        Me.HintBevelCheckEdit.Name = "HintBevelCheckEdit"
        Me.HintBevelCheckEdit.Properties.Caption = ""
        Me.HintBevelCheckEdit.ShowToolTips = False
        Me.HintBevelCheckEdit.Size = New System.Drawing.Size(180, 19)
        Me.HintBevelCheckEdit.TabIndex = 3
        '
        'HintBevelLabel
        '
        Me.HintBevelLabel.AutoSize = True
        Me.HintBevelLabel.Location = New System.Drawing.Point(5, 66)
        Me.HintBevelLabel.Name = "HintBevelLabel"
        Me.HintBevelLabel.Size = New System.Drawing.Size(66, 13)
        Me.HintBevelLabel.TabIndex = 2
        Me.HintBevelLabel.Text = "Show bevel:"
        '
        'HintColorLabel
        '
        Me.HintColorLabel.AutoSize = True
        Me.HintColorLabel.Location = New System.Drawing.Point(5, 33)
        Me.HintColorLabel.Name = "HintColorLabel"
        Me.HintColorLabel.Size = New System.Drawing.Size(36, 13)
        Me.HintColorLabel.TabIndex = 1
        Me.HintColorLabel.Text = "Color:"
        '
        'FontGroupControl
        '
        Me.FontGroupControl.Controls.Add(Me.FontSizeComboBox)
        Me.FontGroupControl.Controls.Add(Me.FontNameComboBox)
        Me.FontGroupControl.Controls.Add(Me.FontSizeLabel)
        Me.FontGroupControl.Controls.Add(Me.FontNameLabel)
        Me.FontGroupControl.Location = New System.Drawing.Point(9, 94)
        Me.FontGroupControl.Name = "FontGroupControl"
        Me.FontGroupControl.Size = New System.Drawing.Size(440, 99)
        Me.FontGroupControl.TabIndex = 21
        Me.FontGroupControl.Text = "Font"
        '
        'FontSizeComboBox
        '
        Me.FontSizeComboBox.Location = New System.Drawing.Point(73, 63)
        Me.FontSizeComboBox.Name = "FontSizeComboBox"
        Me.FontSizeComboBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FontSizeComboBox.Properties.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.FontSizeComboBox.Size = New System.Drawing.Size(178, 20)
        Me.FontSizeComboBox.TabIndex = 4
        '
        'FontNameComboBox
        '
        Me.FontNameComboBox.Location = New System.Drawing.Point(73, 30)
        Me.FontNameComboBox.Name = "FontNameComboBox"
        Me.FontNameComboBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FontNameComboBox.Size = New System.Drawing.Size(178, 20)
        Me.FontNameComboBox.TabIndex = 3
        '
        'FontSizeLabel
        '
        Me.FontSizeLabel.AutoSize = True
        Me.FontSizeLabel.Location = New System.Drawing.Point(5, 66)
        Me.FontSizeLabel.Name = "FontSizeLabel"
        Me.FontSizeLabel.Size = New System.Drawing.Size(30, 13)
        Me.FontSizeLabel.TabIndex = 2
        Me.FontSizeLabel.Text = "Size:"
        '
        'FontNameLabel
        '
        Me.FontNameLabel.AutoSize = True
        Me.FontNameLabel.Location = New System.Drawing.Point(5, 33)
        Me.FontNameLabel.Name = "FontNameLabel"
        Me.FontNameLabel.Size = New System.Drawing.Size(62, 13)
        Me.FontNameLabel.TabIndex = 1
        Me.FontNameLabel.Text = "Font name:"
        '
        'LookGroupControl
        '
        Me.LookGroupControl.Controls.Add(Me.StyleLabel)
        Me.LookGroupControl.Controls.Add(Me.StyleComboBox)
        Me.LookGroupControl.Location = New System.Drawing.Point(9, 14)
        Me.LookGroupControl.Name = "LookGroupControl"
        Me.LookGroupControl.Size = New System.Drawing.Size(440, 63)
        Me.LookGroupControl.TabIndex = 20
        Me.LookGroupControl.Text = "Look"
        '
        'StyleLabel
        '
        Me.StyleLabel.AutoSize = True
        Me.StyleLabel.Location = New System.Drawing.Point(5, 34)
        Me.StyleLabel.Name = "StyleLabel"
        Me.StyleLabel.Size = New System.Drawing.Size(35, 13)
        Me.StyleLabel.TabIndex = 0
        Me.StyleLabel.Text = "Style:"
        '
        'StyleComboBox
        '
        Me.StyleComboBox.Location = New System.Drawing.Point(73, 31)
        Me.StyleComboBox.Name = "StyleComboBox"
        Me.StyleComboBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.StyleComboBox.Properties.LargeImages = Me.StyleImageList
        Me.StyleComboBox.Size = New System.Drawing.Size(178, 20)
        Me.StyleComboBox.TabIndex = 1
        '
        'StyleImageList
        '
        Me.StyleImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.StyleImageList.ImageSize = New System.Drawing.Size(16, 16)
        Me.StyleImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'SystemTabPage
        '
        Me.SystemTabPage.Controls.Add(Me.ChartGroupControl)
        Me.SystemTabPage.Name = "SystemTabPage"
        Me.SystemTabPage.Size = New System.Drawing.Size(458, 463)
        Me.SystemTabPage.Text = "System"
        '
        'ChartGroupControl
        '
        Me.ChartGroupControl.Controls.Add(Me.MaxMarksCountTextEdit)
        Me.ChartGroupControl.Controls.Add(Me.MaxMarksCountLabel)
        Me.ChartGroupControl.Controls.Add(Me.MaxPointsPerSecTextEdit)
        Me.ChartGroupControl.Controls.Add(Me.MaxPointsPerSecLabel)
        Me.ChartGroupControl.Location = New System.Drawing.Point(9, 14)
        Me.ChartGroupControl.Name = "ChartGroupControl"
        Me.ChartGroupControl.Size = New System.Drawing.Size(440, 97)
        Me.ChartGroupControl.TabIndex = 21
        Me.ChartGroupControl.Text = "Result chart"
        '
        'MaxPointsPerSecTextEdit
        '
        Me.MaxPointsPerSecTextEdit.Location = New System.Drawing.Point(121, 31)
        Me.MaxPointsPerSecTextEdit.Name = "MaxPointsPerSecTextEdit"
        Me.MaxPointsPerSecTextEdit.Properties.Mask.EditMask = "n0"
        Me.MaxPointsPerSecTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.MaxPointsPerSecTextEdit.Size = New System.Drawing.Size(129, 20)
        Me.MaxPointsPerSecTextEdit.TabIndex = 1
        '
        'MaxPointsPerSecLabel
        '
        Me.MaxPointsPerSecLabel.AutoSize = True
        Me.MaxPointsPerSecLabel.Location = New System.Drawing.Point(5, 34)
        Me.MaxPointsPerSecLabel.Name = "MaxPointsPerSecLabel"
        Me.MaxPointsPerSecLabel.Size = New System.Drawing.Size(110, 13)
        Me.MaxPointsPerSecLabel.TabIndex = 0
        Me.MaxPointsPerSecLabel.Text = "Max points per 1 sec:"
        '
        'SaveTabPage
        '
        Me.SaveTabPage.Controls.Add(Me.SaveReportGroupControl)
        Me.SaveTabPage.Controls.Add(Me.SaveImageGroupControl)
        Me.SaveTabPage.Controls.Add(Me.SaveResultGroupControl)
        Me.SaveTabPage.Name = "SaveTabPage"
        Me.SaveTabPage.Size = New System.Drawing.Size(458, 463)
        Me.SaveTabPage.Text = "Save"
        '
        'SaveReportGroupControl
        '
        Me.SaveReportGroupControl.Controls.Add(Me.Report_SaveOnlyVisiblePoints_CheckEdit)
        Me.SaveReportGroupControl.Location = New System.Drawing.Point(9, 174)
        Me.SaveReportGroupControl.Name = "SaveReportGroupControl"
        Me.SaveReportGroupControl.Size = New System.Drawing.Size(440, 63)
        Me.SaveReportGroupControl.TabIndex = 23
        Me.SaveReportGroupControl.Text = "Report"
        '
        'Report_SaveOnlyVisiblePoints_CheckEdit
        '
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.Location = New System.Drawing.Point(6, 30)
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.Name = "Report_SaveOnlyVisiblePoints_CheckEdit"
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.Properties.Caption = "Save only visible points"
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.ShowToolTips = False
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.Size = New System.Drawing.Size(180, 19)
        Me.Report_SaveOnlyVisiblePoints_CheckEdit.TabIndex = 5
        '
        'SaveImageGroupControl
        '
        Me.SaveImageGroupControl.Controls.Add(Me.SaveImageTypeLabel)
        Me.SaveImageGroupControl.Controls.Add(Me.SaveImageTypeComboBox)
        Me.SaveImageGroupControl.Location = New System.Drawing.Point(9, 92)
        Me.SaveImageGroupControl.Name = "SaveImageGroupControl"
        Me.SaveImageGroupControl.Size = New System.Drawing.Size(440, 63)
        Me.SaveImageGroupControl.TabIndex = 22
        Me.SaveImageGroupControl.Text = "Save image"
        '
        'SaveImageTypeLabel
        '
        Me.SaveImageTypeLabel.AutoSize = True
        Me.SaveImageTypeLabel.Location = New System.Drawing.Point(5, 34)
        Me.SaveImageTypeLabel.Name = "SaveImageTypeLabel"
        Me.SaveImageTypeLabel.Size = New System.Drawing.Size(66, 13)
        Me.SaveImageTypeLabel.TabIndex = 0
        Me.SaveImageTypeLabel.Text = "Image type:"
        '
        'SaveImageTypeComboBox
        '
        Me.SaveImageTypeComboBox.EditValue = 0
        Me.SaveImageTypeComboBox.Location = New System.Drawing.Point(73, 31)
        Me.SaveImageTypeComboBox.Name = "SaveImageTypeComboBox"
        Me.SaveImageTypeComboBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SaveImageTypeComboBox.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Coloured", 0, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Black and white", 1, -1)})
        Me.SaveImageTypeComboBox.Properties.LargeImages = Me.StyleImageList
        Me.SaveImageTypeComboBox.Size = New System.Drawing.Size(178, 20)
        Me.SaveImageTypeComboBox.TabIndex = 1
        '
        'SaveResultGroupControl
        '
        Me.SaveResultGroupControl.Controls.Add(Me.Result_SaveOnlyVisiblePoints_CheckEdit)
        Me.SaveResultGroupControl.Location = New System.Drawing.Point(9, 14)
        Me.SaveResultGroupControl.Name = "SaveResultGroupControl"
        Me.SaveResultGroupControl.Size = New System.Drawing.Size(440, 63)
        Me.SaveResultGroupControl.TabIndex = 21
        Me.SaveResultGroupControl.Text = "Save result"
        '
        'Result_SaveOnlyVisiblePoints_CheckEdit
        '
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.Location = New System.Drawing.Point(6, 32)
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.Name = "Result_SaveOnlyVisiblePoints_CheckEdit"
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.Properties.Caption = "Save only visible points"
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.ShowToolTips = False
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.Size = New System.Drawing.Size(180, 19)
        Me.Result_SaveOnlyVisiblePoints_CheckEdit.TabIndex = 4
        '
        'MaxMarksCountTextEdit
        '
        Me.MaxMarksCountTextEdit.Location = New System.Drawing.Point(121, 63)
        Me.MaxMarksCountTextEdit.Name = "MaxMarksCountTextEdit"
        Me.MaxMarksCountTextEdit.Properties.Mask.EditMask = "n0"
        Me.MaxMarksCountTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.MaxMarksCountTextEdit.Size = New System.Drawing.Size(129, 20)
        Me.MaxMarksCountTextEdit.TabIndex = 3
        '
        'MaxMarksCountLabel
        '
        Me.MaxMarksCountLabel.AutoSize = True
        Me.MaxMarksCountLabel.Location = New System.Drawing.Point(5, 66)
        Me.MaxMarksCountLabel.Name = "MaxMarksCountLabel"
        Me.MaxMarksCountLabel.Size = New System.Drawing.Size(92, 13)
        Me.MaxMarksCountLabel.TabIndex = 2
        Me.MaxMarksCountLabel.Text = "Max marks count:"
        '
        'CustomizeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 494)
        Me.Controls.Add(Me.TabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CustomizeForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customize"
        Me.CustomizePanelsTabPage.ResumeLayout(False)
        CType(Me.TabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.CustomizeButtonsTabPage.ResumeLayout(False)
        Me.ViewTabPage.ResumeLayout(False)
        CType(Me.HintGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HintGroupControl.ResumeLayout(False)
        Me.HintGroupControl.PerformLayout()
        CType(Me.HintColorEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HintBevelCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FontGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FontGroupControl.ResumeLayout(False)
        Me.FontGroupControl.PerformLayout()
        CType(Me.FontSizeComboBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FontNameComboBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LookGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LookGroupControl.ResumeLayout(False)
        Me.LookGroupControl.PerformLayout()
        CType(Me.StyleComboBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SystemTabPage.ResumeLayout(False)
        CType(Me.ChartGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ChartGroupControl.ResumeLayout(False)
        Me.ChartGroupControl.PerformLayout()
        CType(Me.MaxPointsPerSecTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SaveTabPage.ResumeLayout(False)
        CType(Me.SaveReportGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SaveReportGroupControl.ResumeLayout(False)
        CType(Me.Report_SaveOnlyVisiblePoints_CheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SaveImageGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SaveImageGroupControl.ResumeLayout(False)
        Me.SaveImageGroupControl.PerformLayout()
        CType(Me.SaveImageTypeComboBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SaveResultGroupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SaveResultGroupControl.ResumeLayout(False)
        CType(Me.Result_SaveOnlyVisiblePoints_CheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxMarksCountTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CustomizePanelsTabPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents CustomizePanelsListView As System.Windows.Forms.ListView
    Friend WithEvents TabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ViewTabPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents StyleComboBox As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents StyleLabel As System.Windows.Forms.Label
    Friend WithEvents StyleImageList As System.Windows.Forms.ImageList
    Friend WithEvents FontGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LookGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FontSizeLabel As System.Windows.Forms.Label
    Friend WithEvents FontNameLabel As System.Windows.Forms.Label
    Friend WithEvents FontNameComboBox As DevExpress.XtraEditors.FontEdit
    Friend WithEvents FontSizeComboBox As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents HintGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents HintBevelLabel As System.Windows.Forms.Label
    Friend WithEvents HintColorLabel As System.Windows.Forms.Label
    Friend WithEvents HintColorEdit As DevExpress.XtraEditors.ColorEdit
    Friend WithEvents HintBevelCheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SystemTabPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ChartGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents MaxPointsPerSecTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents MaxPointsPerSecLabel As System.Windows.Forms.Label
    Friend WithEvents CustomizeButtonsTabPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents CustomizeButtonsListView As System.Windows.Forms.ListView
    Friend WithEvents SaveTabPage As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SaveReportGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SaveImageGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SaveImageTypeLabel As System.Windows.Forms.Label
    Friend WithEvents SaveImageTypeComboBox As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents SaveResultGroupControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Result_SaveOnlyVisiblePoints_CheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Report_SaveOnlyVisiblePoints_CheckEdit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents MaxMarksCountTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents MaxMarksCountLabel As System.Windows.Forms.Label
End Class
