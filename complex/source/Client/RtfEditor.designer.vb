Imports Microsoft.VisualBasic
Imports System

Partial Public Class RtfEditor

#Region "Designer generated code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RtfEditor))
        Me.mHelp = New DevExpress.XtraBars.BarSubItem
        Me.iWeb = New DevExpress.XtraBars.BarButtonItem
        Me.iAbout = New DevExpress.XtraBars.BarButtonItem
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.buttonAlignCenter = New DevExpress.XtraBars.BarButtonItem
        Me.iSelectAll = New DevExpress.XtraBars.BarButtonItem
        Me.buttonCopy = New DevExpress.XtraBars.BarButtonItem
        Me.buttonCut = New DevExpress.XtraBars.BarButtonItem
        Me.buttonPaste = New DevExpress.XtraBars.BarButtonItem
        Me.iClear = New DevExpress.XtraBars.BarButtonItem
        Me.barManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barStandard = New DevExpress.XtraBars.Bar
        Me.buttonOk = New DevExpress.XtraBars.BarButtonItem
        Me.buttonNo = New DevExpress.XtraBars.BarButtonItem
        Me.buttonUndo = New DevExpress.XtraBars.BarButtonItem
        Me.barFormat = New DevExpress.XtraBars.Bar
        Me.buttonFontBold = New DevExpress.XtraBars.BarButtonItem
        Me.buttonFontItalic = New DevExpress.XtraBars.BarButtonItem
        Me.buttonFontUnderline = New DevExpress.XtraBars.BarButtonItem
        Me.buttonAlignLeft = New DevExpress.XtraBars.BarButtonItem
        Me.buttonAlignRight = New DevExpress.XtraBars.BarButtonItem
        Me.barFont = New DevExpress.XtraBars.Bar
        Me.comboFont = New DevExpress.XtraBars.BarEditItem
        Me.RepositoryItemFontEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemFontEdit
        Me.comboFontSize = New DevExpress.XtraBars.BarEditItem
        Me.RepositoryItemComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        Me.buttonTextColor = New DevExpress.XtraBars.BarEditItem
        Me.RepositoryItemColorEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemColorEdit
        Me.barStatus = New DevExpress.XtraBars.Bar
        Me.labelStatus = New DevExpress.XtraBars.BarStaticItem
        Me.ZoomSlider = New DevExpress.XtraBars.BarEditItem
        Me.ZoomTrackBar = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
        Me.barAndDockingController1 = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barDockControl1 = New DevExpress.XtraBars.BarDockControl
        Me.barDockControl2 = New DevExpress.XtraBars.BarDockControl
        Me.barDockControl3 = New DevExpress.XtraBars.BarDockControl
        Me.barDockControl4 = New DevExpress.XtraBars.BarDockControl
        Me.iClose = New DevExpress.XtraBars.BarButtonItem
        Me.iSaveAs = New DevExpress.XtraBars.BarButtonItem
        Me.iExit = New DevExpress.XtraBars.BarButtonItem
        Me.mFile = New DevExpress.XtraBars.BarSubItem
        Me.iFind = New DevExpress.XtraBars.BarButtonItem
        Me.iReplace = New DevExpress.XtraBars.BarButtonItem
        Me.mEdit = New DevExpress.XtraBars.BarSubItem
        Me.iProtected = New DevExpress.XtraBars.BarButtonItem
        Me.mFormat = New DevExpress.XtraBars.BarSubItem
        Me.iToolBars = New DevExpress.XtraBars.BarToolbarsListItem
        Me.iPaintStyle = New DevExpress.XtraBars.BarSubItem
        Me.ipsDefault = New DevExpress.XtraBars.BarButtonItem
        Me.ipsWXP = New DevExpress.XtraBars.BarButtonItem
        Me.ipsOXP = New DevExpress.XtraBars.BarButtonItem
        Me.ipsO2K = New DevExpress.XtraBars.BarButtonItem
        Me.ipsO3 = New DevExpress.XtraBars.BarButtonItem
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        Me.RepositoryItemZoomTrackBar2 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        Me.popupControlContainer1 = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.rtfText = New System.Windows.Forms.RichTextBox
        Me.RtfContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.bCut = New System.Windows.Forms.ToolStripMenuItem
        Me.bCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.bPaste = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.bSelectAll = New System.Windows.Forms.ToolStripMenuItem
        Me.imageList2 = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemColorEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ZoomTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupControlContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RtfContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'mHelp
        '
        Me.mHelp.Caption = "&Help"
        Me.mHelp.CategoryGuid = New System.Guid("02ed6c1c-df85-47f8-9572-b20022d647f9")
        Me.mHelp.Id = 23
        Me.mHelp.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iWeb), New DevExpress.XtraBars.LinkPersistInfo(Me.iAbout, True)})
        Me.mHelp.Name = "mHelp"
        '
        'iWeb
        '
        Me.iWeb.Caption = "&Developer Express on the Web"
        Me.iWeb.Description = "Opens the web page."
        Me.iWeb.Hint = "Developer Express on the Web"
        Me.iWeb.Id = 21
        Me.iWeb.ImageIndex = 21
        Me.iWeb.Name = "iWeb"
        '
        'iAbout
        '
        Me.iAbout.Caption = "&About"
        Me.iAbout.Description = "Displays the description of this program."
        Me.iAbout.Id = 22
        Me.iAbout.Name = "iAbout"
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList1.Images.SetKeyName(0, "")
        Me.imageList1.Images.SetKeyName(1, "")
        Me.imageList1.Images.SetKeyName(2, "")
        Me.imageList1.Images.SetKeyName(3, "")
        Me.imageList1.Images.SetKeyName(4, "")
        Me.imageList1.Images.SetKeyName(5, "")
        Me.imageList1.Images.SetKeyName(6, "")
        Me.imageList1.Images.SetKeyName(7, "")
        Me.imageList1.Images.SetKeyName(8, "")
        Me.imageList1.Images.SetKeyName(9, "")
        Me.imageList1.Images.SetKeyName(10, "")
        Me.imageList1.Images.SetKeyName(11, "")
        Me.imageList1.Images.SetKeyName(12, "")
        Me.imageList1.Images.SetKeyName(13, "")
        Me.imageList1.Images.SetKeyName(14, "")
        Me.imageList1.Images.SetKeyName(15, "")
        Me.imageList1.Images.SetKeyName(16, "")
        Me.imageList1.Images.SetKeyName(17, "")
        Me.imageList1.Images.SetKeyName(18, "")
        Me.imageList1.Images.SetKeyName(19, "")
        Me.imageList1.Images.SetKeyName(20, "")
        Me.imageList1.Images.SetKeyName(21, "")
        Me.imageList1.Images.SetKeyName(22, "")
        Me.imageList1.Images.SetKeyName(23, "")
        Me.imageList1.Images.SetKeyName(24, "")
        Me.imageList1.Images.SetKeyName(25, "")
        Me.imageList1.Images.SetKeyName(26, "")
        '
        'buttonAlignCenter
        '
        Me.buttonAlignCenter.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonAlignCenter.Caption = "Aligh &Center"
        Me.buttonAlignCenter.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonAlignCenter.Description = "Centers the selected text."
        Me.buttonAlignCenter.GroupIndex = 1
        Me.buttonAlignCenter.Hint = "Aligh Center"
        Me.buttonAlignCenter.Id = 28
        Me.buttonAlignCenter.ImageIndex = 19
        Me.buttonAlignCenter.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.buttonAlignCenter.Name = "buttonAlignCenter"
        '
        'iSelectAll
        '
        Me.iSelectAll.Caption = "Select A&ll"
        Me.iSelectAll.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.iSelectAll.Description = "Selects all text in the active document."
        Me.iSelectAll.Id = 13
        Me.iSelectAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.iSelectAll.Name = "iSelectAll"
        '
        'buttonCopy
        '
        Me.buttonCopy.Caption = "&Copy"
        Me.buttonCopy.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.buttonCopy.Description = "Copies the selection to the Clipboard."
        Me.buttonCopy.Hint = "Copy"
        Me.buttonCopy.Id = 10
        Me.buttonCopy.ImageIndex = 1
        Me.buttonCopy.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C))
        Me.buttonCopy.Name = "buttonCopy"
        '
        'buttonCut
        '
        Me.buttonCut.Caption = "Cu&t"
        Me.buttonCut.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.buttonCut.Description = "Removes the selection from the active document and places it on the Clipboard."
        Me.buttonCut.Hint = "Cut"
        Me.buttonCut.Id = 9
        Me.buttonCut.ImageIndex = 2
        Me.buttonCut.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X))
        Me.buttonCut.Name = "buttonCut"
        '
        'buttonPaste
        '
        Me.buttonPaste.Caption = "&Paste"
        Me.buttonPaste.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.buttonPaste.Description = "Inserts the contents of the Clipboard at the insertion point, and replaces any se" & _
            "lection. This command is available only if you have cut or copied a text."
        Me.buttonPaste.Hint = "Paste"
        Me.buttonPaste.Id = 11
        Me.buttonPaste.ImageIndex = 8
        Me.buttonPaste.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V))
        Me.buttonPaste.Name = "buttonPaste"
        '
        'iClear
        '
        Me.iClear.Caption = "Cle&ar"
        Me.iClear.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.iClear.Description = "Deletes the selected text without putting it on the Clipboard. This command is av" & _
            "ailable only if a text is selected. "
        Me.iClear.Hint = "Clear"
        Me.iClear.Id = 12
        Me.iClear.ImageIndex = 13
        Me.iClear.Name = "iClear"
        '
        'barManager1
        '
        Me.barManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barStandard, Me.barFormat, Me.barFont, Me.barStatus})
        Me.barManager1.Categories.AddRange(New DevExpress.XtraBars.BarManagerCategory() {New DevExpress.XtraBars.BarManagerCategory("Edit", New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")), New DevExpress.XtraBars.BarManagerCategory("Format", New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")), New DevExpress.XtraBars.BarManagerCategory("Popup", New System.Guid("78945463-36c5-4beb-a57f-d50b6a74e9b9")), New DevExpress.XtraBars.BarManagerCategory("Status", New System.Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d")), New DevExpress.XtraBars.BarManagerCategory("Built-in Menus", New System.Guid("02ed6c1c-df85-47f8-9572-b20022d647f9")), New DevExpress.XtraBars.BarManagerCategory("ToolBars", New System.Guid("3856091a-e0f6-4a7c-80e9-103dece20d5b"))})
        Me.barManager1.Controller = Me.barAndDockingController1
        Me.barManager1.DockControls.Add(Me.barDockControl1)
        Me.barManager1.DockControls.Add(Me.barDockControl2)
        Me.barManager1.DockControls.Add(Me.barDockControl3)
        Me.barManager1.DockControls.Add(Me.barDockControl4)
        Me.barManager1.Form = Me
        Me.barManager1.Images = Me.imageList1
        Me.barManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.iClose, Me.iSaveAs, Me.iExit, Me.mFile, Me.buttonUndo, Me.buttonCut, Me.buttonCopy, Me.buttonPaste, Me.iClear, Me.iSelectAll, Me.iFind, Me.iReplace, Me.mEdit, Me.iProtected, Me.mFormat, Me.iWeb, Me.iAbout, Me.mHelp, Me.buttonFontBold, Me.buttonFontItalic, Me.buttonFontUnderline, Me.buttonAlignLeft, Me.buttonAlignCenter, Me.buttonAlignRight, Me.labelStatus, Me.iToolBars, Me.iPaintStyle, Me.ipsWXP, Me.ipsOXP, Me.ipsO2K, Me.ipsO3, Me.ipsDefault, Me.ZoomSlider, Me.buttonTextColor, Me.comboFontSize, Me.buttonOk, Me.buttonNo, Me.comboFont})
        Me.barManager1.MaxItemId = 105
        Me.barManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemZoomTrackBar1, Me.RepositoryItemTextEdit1, Me.RepositoryItemZoomTrackBar2, Me.RepositoryItemTextEdit2, Me.ZoomTrackBar, Me.RepositoryItemColorEdit1, Me.RepositoryItemComboBox1, Me.RepositoryItemComboBox2, Me.RepositoryItemFontEdit1})
        Me.barManager1.StatusBar = Me.barStatus
        '
        'barStandard
        '
        Me.barStandard.BarName = "Standard"
        Me.barStandard.DockCol = 0
        Me.barStandard.DockRow = 0
        Me.barStandard.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barStandard.FloatSize = New System.Drawing.Size(48, 26)
        Me.barStandard.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.buttonOk, True), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonNo), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonCut, True), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonCopy), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonPaste), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonUndo, True)})
        Me.barStandard.Text = "Standard"
        '
        'buttonOk
        '
        Me.buttonOk.Caption = "Save"
        Me.buttonOk.Hint = "Save"
        Me.buttonOk.Id = 102
        Me.buttonOk.ImageIndex = 10
        Me.buttonOk.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.buttonOk.Name = "buttonOk"
        '
        'buttonNo
        '
        Me.buttonNo.Caption = "Cancel"
        Me.buttonNo.Glyph = Global.Client.My.Resources.Resources.cancel_16x16
        Me.buttonNo.Hint = "Cancel"
        Me.buttonNo.Id = 103
        Me.buttonNo.Name = "buttonNo"
        '
        'buttonUndo
        '
        Me.buttonUndo.Caption = "&Undo"
        Me.buttonUndo.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.buttonUndo.Description = "Reverses the last command or deletes the last entry you typed."
        Me.buttonUndo.Hint = "Undo"
        Me.buttonUndo.Id = 8
        Me.buttonUndo.ImageIndex = 11
        Me.buttonUndo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z))
        Me.buttonUndo.Name = "buttonUndo"
        '
        'barFormat
        '
        Me.barFormat.BarName = "Format"
        Me.barFormat.DockCol = 1
        Me.barFormat.DockRow = 0
        Me.barFormat.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barFormat.FloatLocation = New System.Drawing.Point(790, 160)
        Me.barFormat.FloatSize = New System.Drawing.Size(27, 168)
        Me.barFormat.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.buttonFontBold), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonFontItalic), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonFontUnderline), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonAlignLeft, True), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonAlignCenter), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonAlignRight)})
        Me.barFormat.Offset = 155
        Me.barFormat.Text = "Format"
        '
        'buttonFontBold
        '
        Me.buttonFontBold.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonFontBold.Caption = "&Bold"
        Me.buttonFontBold.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonFontBold.Description = "Makes selected text and numbers bold. If the selection is already bold, clicking " & _
            "button removes bold formatting."
        Me.buttonFontBold.Hint = "Bold"
        Me.buttonFontBold.Id = 24
        Me.buttonFontBold.ImageIndex = 15
        Me.buttonFontBold.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B))
        Me.buttonFontBold.Name = "buttonFontBold"
        '
        'buttonFontItalic
        '
        Me.buttonFontItalic.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonFontItalic.Caption = "&Italic"
        Me.buttonFontItalic.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonFontItalic.Description = "Makes selected text and numbers italic. If the selection is already italic, click" & _
            "ing button removes italic formatting."
        Me.buttonFontItalic.Hint = "Italic"
        Me.buttonFontItalic.Id = 25
        Me.buttonFontItalic.ImageIndex = 16
        Me.buttonFontItalic.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I))
        Me.buttonFontItalic.Name = "buttonFontItalic"
        '
        'buttonFontUnderline
        '
        Me.buttonFontUnderline.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonFontUnderline.Caption = "&Underline"
        Me.buttonFontUnderline.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonFontUnderline.Description = "Underlines selected text and numbers. If the selection is already underlined, cli" & _
            "cking button removes underlining."
        Me.buttonFontUnderline.Hint = "Underline"
        Me.buttonFontUnderline.Id = 26
        Me.buttonFontUnderline.ImageIndex = 17
        Me.buttonFontUnderline.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
        Me.buttonFontUnderline.Name = "buttonFontUnderline"
        '
        'buttonAlignLeft
        '
        Me.buttonAlignLeft.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonAlignLeft.Caption = "Align &Left"
        Me.buttonAlignLeft.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonAlignLeft.Description = "Aligns the selected text to the left."
        Me.buttonAlignLeft.GroupIndex = 1
        Me.buttonAlignLeft.Hint = "Align Left"
        Me.buttonAlignLeft.Id = 27
        Me.buttonAlignLeft.ImageIndex = 18
        Me.buttonAlignLeft.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
        Me.buttonAlignLeft.Name = "buttonAlignLeft"
        '
        'buttonAlignRight
        '
        Me.buttonAlignRight.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.buttonAlignRight.Caption = "Align &Right"
        Me.buttonAlignRight.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.buttonAlignRight.Description = "Aligns the selected text to the right."
        Me.buttonAlignRight.GroupIndex = 1
        Me.buttonAlignRight.Hint = "Align Right"
        Me.buttonAlignRight.Id = 29
        Me.buttonAlignRight.ImageIndex = 20
        Me.buttonAlignRight.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.buttonAlignRight.Name = "buttonAlignRight"
        '
        'barFont
        '
        Me.barFont.BarName = "Font"
        Me.barFont.DockCol = 2
        Me.barFont.DockRow = 0
        Me.barFont.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barFont.FloatLocation = New System.Drawing.Point(493, 252)
        Me.barFont.FloatSize = New System.Drawing.Size(48, 26)
        Me.barFont.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.comboFont), New DevExpress.XtraBars.LinkPersistInfo(Me.comboFontSize), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonTextColor)})
        Me.barFont.Offset = 360
        Me.barFont.Text = "Font"
        '
        'comboFont
        '
        Me.comboFont.Caption = "Font"
        Me.comboFont.Edit = Me.RepositoryItemFontEdit1
        Me.comboFont.Hint = "Font"
        Me.comboFont.Id = 104
        Me.comboFont.Name = "comboFont"
        Me.comboFont.Width = 150
        '
        'RepositoryItemFontEdit1
        '
        Me.RepositoryItemFontEdit1.AutoHeight = False
        Me.RepositoryItemFontEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemFontEdit1.Name = "RepositoryItemFontEdit1"
        '
        'comboFontSize
        '
        Me.comboFontSize.Caption = "Font size"
        Me.comboFontSize.Edit = Me.RepositoryItemComboBox2
        Me.comboFontSize.Hint = "Font size"
        Me.comboFontSize.Id = 101
        Me.comboFontSize.Name = "comboFontSize"
        Me.comboFontSize.Width = 40
        '
        'RepositoryItemComboBox2
        '
        Me.RepositoryItemComboBox2.AutoHeight = False
        Me.RepositoryItemComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox2.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.RepositoryItemComboBox2.Name = "RepositoryItemComboBox2"
        '
        'buttonTextColor
        '
        Me.buttonTextColor.Caption = "Font Color"
        Me.buttonTextColor.Edit = Me.RepositoryItemColorEdit1
        Me.buttonTextColor.Hint = "Font Color"
        Me.buttonTextColor.Id = 99
        Me.buttonTextColor.ImageIndex = 5
        Me.buttonTextColor.Name = "buttonTextColor"
        '
        'RepositoryItemColorEdit1
        '
        Me.RepositoryItemColorEdit1.AutoHeight = False
        Me.RepositoryItemColorEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemColorEdit1.Name = "RepositoryItemColorEdit1"
        '
        'barStatus
        '
        Me.barStatus.BarName = "StatusBar"
        Me.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.barStatus.DockCol = 0
        Me.barStatus.DockRow = 0
        Me.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barStatus.FloatLocation = New System.Drawing.Point(86, 499)
        Me.barStatus.FloatSize = New System.Drawing.Size(48, 26)
        Me.barStatus.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.labelStatus), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomSlider, True)})
        Me.barStatus.OptionsBar.AllowQuickCustomization = False
        Me.barStatus.OptionsBar.DrawDragBorder = False
        Me.barStatus.OptionsBar.DrawSizeGrip = True
        Me.barStatus.OptionsBar.UseWholeRow = True
        Me.barStatus.Text = "StatusBar"
        '
        'labelStatus
        '
        Me.labelStatus.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None
        Me.labelStatus.Caption = "Line: 0 Ch:0"
        Me.labelStatus.CategoryGuid = New System.Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d")
        Me.labelStatus.Id = 32
        Me.labelStatus.Name = "labelStatus"
        Me.labelStatus.RightIndent = 2
        Me.labelStatus.TextAlignment = System.Drawing.StringAlignment.Near
        Me.labelStatus.Width = 100
        '
        'ZoomSlider
        '
        Me.ZoomSlider.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.ZoomSlider.Edit = Me.ZoomTrackBar
        Me.ZoomSlider.EditValue = "100"
        Me.ZoomSlider.Id = 95
        Me.ZoomSlider.Name = "ZoomSlider"
        Me.ZoomSlider.Width = 150
        '
        'ZoomTrackBar
        '
        Me.ZoomTrackBar.Maximum = 200
        Me.ZoomTrackBar.Name = "ZoomTrackBar"
        Me.ZoomTrackBar.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight
        '
        'barAndDockingController1
        '
        Me.barAndDockingController1.PaintStyleName = "Skin"
        Me.barAndDockingController1.PropertiesBar.AllowLinkLighting = False
        '
        'iClose
        '
        Me.iClose.Caption = "&Close"
        Me.iClose.Description = "Closes the active document."
        Me.iClose.Hint = "Close Document"
        Me.iClose.Id = 2
        Me.iClose.ImageIndex = 12
        Me.iClose.Name = "iClose"
        '
        'iSaveAs
        '
        Me.iSaveAs.Caption = "Save &As..."
        Me.iSaveAs.Description = "Saves the active document with a different file name."
        Me.iSaveAs.Id = 4
        Me.iSaveAs.Name = "iSaveAs"
        '
        'iExit
        '
        Me.iExit.Caption = "E&xit"
        Me.iExit.Description = "Closes this program after prompting you to save unsaved document."
        Me.iExit.Id = 6
        Me.iExit.Name = "iExit"
        '
        'mFile
        '
        Me.mFile.Caption = "&File"
        Me.mFile.CategoryGuid = New System.Guid("02ed6c1c-df85-47f8-9572-b20022d647f9")
        Me.mFile.Id = 7
        Me.mFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.iClose), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, Me.iSaveAs, "", False, False, True, 0), New DevExpress.XtraBars.LinkPersistInfo(Me.iExit, True)})
        Me.mFile.MenuBarWidth = 20
        Me.mFile.Name = "mFile"
        '
        'iFind
        '
        Me.iFind.Caption = "&Find..."
        Me.iFind.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.iFind.Description = "Searches for the specified text."
        Me.iFind.Hint = "Find"
        Me.iFind.Id = 14
        Me.iFind.ImageIndex = 3
        Me.iFind.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
        Me.iFind.Name = "iFind"
        '
        'iReplace
        '
        Me.iReplace.Caption = "R&eplace..."
        Me.iReplace.CategoryGuid = New System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")
        Me.iReplace.Description = "Searches for and replaces the specified text."
        Me.iReplace.Hint = "Replace"
        Me.iReplace.Id = 15
        Me.iReplace.ImageIndex = 14
        Me.iReplace.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H))
        Me.iReplace.Name = "iReplace"
        '
        'mEdit
        '
        Me.mEdit.Caption = "&Edit"
        Me.mEdit.CategoryGuid = New System.Guid("02ed6c1c-df85-47f8-9572-b20022d647f9")
        Me.mEdit.Id = 16
        Me.mEdit.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.buttonUndo), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonCut, True), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonCopy), New DevExpress.XtraBars.LinkPersistInfo(Me.buttonPaste), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, Me.iClear, "", True, False, True, 0), New DevExpress.XtraBars.LinkPersistInfo(Me.iSelectAll), New DevExpress.XtraBars.LinkPersistInfo(Me.iFind, True), New DevExpress.XtraBars.LinkPersistInfo(Me.iReplace)})
        Me.mEdit.Name = "mEdit"
        '
        'iProtected
        '
        Me.iProtected.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.iProtected.Caption = "P&rotected"
        Me.iProtected.CategoryGuid = New System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")
        Me.iProtected.Description = "Protectes the selected text."
        Me.iProtected.Id = 19
        Me.iProtected.Name = "iProtected"
        '
        'mFormat
        '
        Me.mFormat.Caption = "F&ormat"
        Me.mFormat.CategoryGuid = New System.Guid("02ed6c1c-df85-47f8-9572-b20022d647f9")
        Me.mFormat.Id = 20
        Me.mFormat.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, Me.iProtected, "", False, False, True, 0)})
        Me.mFormat.Name = "mFormat"
        '
        'iToolBars
        '
        Me.iToolBars.Caption = "&ToolBars"
        Me.iToolBars.CategoryGuid = New System.Guid("3856091a-e0f6-4a7c-80e9-103dece20d5b")
        Me.iToolBars.Id = 38
        Me.iToolBars.Name = "iToolBars"
        '
        'iPaintStyle
        '
        Me.iPaintStyle.Caption = "Paint Style"
        Me.iPaintStyle.Id = 55
        Me.iPaintStyle.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ipsDefault), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsWXP), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsOXP), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsO2K), New DevExpress.XtraBars.LinkPersistInfo(Me.ipsO3)})
        Me.iPaintStyle.Name = "iPaintStyle"
        Me.iPaintStyle.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'ipsDefault
        '
        Me.ipsDefault.Caption = "Default"
        Me.ipsDefault.Description = "Default"
        Me.ipsDefault.Id = 74
        Me.ipsDefault.Name = "ipsDefault"
        '
        'ipsWXP
        '
        Me.ipsWXP.Caption = "Windows XP"
        Me.ipsWXP.Description = "WindowsXP"
        Me.ipsWXP.Id = 56
        Me.ipsWXP.ImageIndex = 25
        Me.ipsWXP.Name = "ipsWXP"
        '
        'ipsOXP
        '
        Me.ipsOXP.Caption = "Office XP"
        Me.ipsOXP.Description = "OfficeXP"
        Me.ipsOXP.Id = 57
        Me.ipsOXP.ImageIndex = 23
        Me.ipsOXP.Name = "ipsOXP"
        '
        'ipsO2K
        '
        Me.ipsO2K.Caption = "Office 2000"
        Me.ipsO2K.Description = "Office2000"
        Me.ipsO2K.Id = 58
        Me.ipsO2K.ImageIndex = 24
        Me.ipsO2K.Name = "ipsO2K"
        '
        'ipsO3
        '
        Me.ipsO3.Caption = "Office 2003"
        Me.ipsO3.Description = "Office2003"
        Me.ipsO3.Id = 69
        Me.ipsO3.ImageIndex = 26
        Me.ipsO3.Name = "ipsO3"
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        Me.RepositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemZoomTrackBar2
        '
        Me.RepositoryItemZoomTrackBar2.Name = "RepositoryItemZoomTrackBar2"
        Me.RepositoryItemZoomTrackBar2.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Allegro BT", "Arial", "Arial Black", "Arial Narrow", "AvantGarde Bk BT", "AWLUnicode", "BankGothic Md BT", "Benguiat Bk BT", "BernhardFashion BT", "BernhardMod BT", "Book Antiqua", "Bookman Old Style", "Bremen Bd BT", "Century Gothic", "Charlesworth", "Comic Sans MS", "CommonBullets", "CopprplGoth Bd BT", "Courier New", "Dauphin", "DiagramTTBlindAll", "DiagramTTBlindBlack", "DiagramTTBlindwhite", "DiagramTTCrystals", "DiagramTTFritz", "DiagramTTHabsburg", "DiagramTTOldstyle", "DiagramTTUSCF", "English111 Vivace BT", "Estrangelo Edessa", "Euclid", "Euclid Extra", "Euclid Fraktur", "Euclid Math One", "Euclid Math Two", "Euclid Symbol", "Fences", "FigurineCB AriesSP", "FigurineCB LetterSP", "FigurineCB TimeSP", "Franklin Gothic Medium", "Frutiger SAIN Bd v.1", "Frutiger SAIN It v.1", "Frutiger SAIN Rm v.1", "Frutiger SBIN Bd v.1", "Frutiger SBIN It v.1", "Frutiger SBIN Rm v.1", "Frutiger SCIN Bd v.1", "Frutiger SCIN It v.1", "Frutiger SCIN Rm v.1", "Futura Lt BT", "Futura Md BT", "Futura XBlk BT", "FuturaBlack BT", "Garamond", "Gautami", "Georgia", "GoudyHandtooled BT", "GoudyOlSt BT", "Haettenschweiler", "Humanst521 BT", "Impact", "Kabel Bk BT", "Kabel Ult BT", "Latha", "Lithograph", "LithographLight", "Lucida Console", "Lucida Sans Unicode", "Mangal", "Marlett", "Microsoft Sans Serif", "Monotype Corsiva", "MS Outlook", "MT Extra", "MV Boli", "Nina", "OCR-A BT", "OCR-B 10 BT", "OzHandicraft BT", "Palatino Linotype", "PosterBodoni BT", "Raavi", "Segoe Condensed", "Serifa BT", "Serifa Th BT", "Shruti", "Souvenir Lt BT", "Staccato222 BT", "StoneSerif SAIN SmBd v.1", "StoneSerif SBIN SmBd v.1", "StoneSerif SCIN SmBd v.1", "Swiss911 XCm BT", "Sylfaen", "Symbol", "Tahoma", "Times New Roman", "Trebuchet MS", "Tunga", "TypoUpright BT", "Verdana", "Webdings", "Wingdings", "Wingdings 2", "Wingdings 3", "WP Arabic Sihafa", "WP ArabicScript Sihafa", "WP BoxDrawing", "WP CyrillicA", "WP CyrillicB", "WP Greek Century", "WP Greek Courier", "WP Greek Helve", "WP Hebrew David", "WP IconicSymbolsA", "WP IconicSymbolsB", "WP Japanese", "WP MathA", "WP MathB", "WP MathExtendedA", "WP MathExtendedB", "WP MultinationalA Courier", "WP MultinationalA Helve", "WP MultinationalA Roman", "WP MultinationalB Courier", "WP MultinationalB Helve", "WP MultinationalB Roman", "WP Phonetic", "WP TypographicSymbols", "ZapfEllipt BT", "Zurich Ex BT"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'popupControlContainer1
        '
        Me.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.popupControlContainer1.Location = New System.Drawing.Point(396, 304)
        Me.popupControlContainer1.Manager = Me.barManager1
        Me.popupControlContainer1.Name = "popupControlContainer1"
        Me.popupControlContainer1.Size = New System.Drawing.Size(44, 40)
        Me.popupControlContainer1.TabIndex = 6
        Me.popupControlContainer1.Visible = False
        '
        'rtfText
        '
        Me.rtfText.ContextMenuStrip = Me.RtfContextMenu
        Me.rtfText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtfText.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.rtfText.Location = New System.Drawing.Point(0, 26)
        Me.rtfText.Name = "rtfText"
        Me.rtfText.Size = New System.Drawing.Size(762, 472)
        Me.rtfText.TabIndex = 4
        Me.rtfText.Text = ""
        '
        'RtfContextMenu
        '
        Me.RtfContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bCut, Me.bCopy, Me.bPaste, Me.ToolStripMenuItem1, Me.bSelectAll})
        Me.RtfContextMenu.Name = "RtfContextMenu"
        Me.RtfContextMenu.ShowItemToolTips = False
        Me.RtfContextMenu.Size = New System.Drawing.Size(167, 98)
        '
        'bCut
        '
        Me.bCut.Image = CType(resources.GetObject("bCut.Image"), System.Drawing.Image)
        Me.bCut.Name = "bCut"
        Me.bCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.bCut.Size = New System.Drawing.Size(166, 22)
        Me.bCut.Text = "Cut"
        '
        'bCopy
        '
        Me.bCopy.Image = CType(resources.GetObject("bCopy.Image"), System.Drawing.Image)
        Me.bCopy.Name = "bCopy"
        Me.bCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.bCopy.Size = New System.Drawing.Size(166, 22)
        Me.bCopy.Text = "Copy"
        '
        'bPaste
        '
        Me.bPaste.Image = CType(resources.GetObject("bPaste.Image"), System.Drawing.Image)
        Me.bPaste.Name = "bPaste"
        Me.bPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.bPaste.Size = New System.Drawing.Size(166, 22)
        Me.bPaste.Text = "Paste"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(163, 6)
        '
        'bSelectAll
        '
        Me.bSelectAll.Name = "bSelectAll"
        Me.bSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.bSelectAll.Size = New System.Drawing.Size(166, 22)
        Me.bSelectAll.Text = "Select all"
        '
        'imageList2
        '
        Me.imageList2.ImageStream = CType(resources.GetObject("imageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList2.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList2.Images.SetKeyName(0, "")
        Me.imageList2.Images.SetKeyName(1, "")
        Me.imageList2.Images.SetKeyName(2, "")
        Me.imageList2.Images.SetKeyName(3, "")
        Me.imageList2.Images.SetKeyName(4, "")
        Me.imageList2.Images.SetKeyName(5, "")
        Me.imageList2.Images.SetKeyName(6, "")
        Me.imageList2.Images.SetKeyName(7, "")
        Me.imageList2.Images.SetKeyName(8, "")
        Me.imageList2.Images.SetKeyName(9, "")
        Me.imageList2.Images.SetKeyName(10, "")
        Me.imageList2.Images.SetKeyName(11, "")
        Me.imageList2.Images.SetKeyName(12, "")
        Me.imageList2.Images.SetKeyName(13, "")
        Me.imageList2.Images.SetKeyName(14, "")
        Me.imageList2.Images.SetKeyName(15, "")
        Me.imageList2.Images.SetKeyName(16, "")
        Me.imageList2.Images.SetKeyName(17, "")
        Me.imageList2.Images.SetKeyName(18, "")
        Me.imageList2.Images.SetKeyName(19, "")
        Me.imageList2.Images.SetKeyName(20, "")
        Me.imageList2.Images.SetKeyName(21, "")
        Me.imageList2.Images.SetKeyName(22, "")
        Me.imageList2.Images.SetKeyName(23, "")
        Me.imageList2.Images.SetKeyName(24, "")
        Me.imageList2.Images.SetKeyName(25, "")
        Me.imageList2.Images.SetKeyName(26, "")
        '
        'RtfEditor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(762, 521)
        Me.Controls.Add(Me.rtfText)
        Me.Controls.Add(Me.popupControlContainer1)
        Me.Controls.Add(Me.barDockControl3)
        Me.Controls.Add(Me.barDockControl4)
        Me.Controls.Add(Me.barDockControl2)
        Me.Controls.Add(Me.barDockControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(770, 555)
        Me.Name = "RtfEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rtf Editor"
        CType(Me.barManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemColorEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ZoomTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupControlContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RtfContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private barDockControl1 As DevExpress.XtraBars.BarDockControl
    Private WithEvents barManager1 As DevExpress.XtraBars.BarManager
    Private WithEvents rtfText As System.Windows.Forms.RichTextBox
    Private barDockControl4 As DevExpress.XtraBars.BarDockControl
    Private barDockControl3 As DevExpress.XtraBars.BarDockControl
    Private barDockControl2 As DevExpress.XtraBars.BarDockControl
    Private imageList1 As System.Windows.Forms.ImageList
    Private WithEvents mFile As DevExpress.XtraBars.BarSubItem
    Private mFormat As DevExpress.XtraBars.BarSubItem
    Private mHelp As DevExpress.XtraBars.BarSubItem
    Private WithEvents iClose As DevExpress.XtraBars.BarButtonItem
    Private mEdit As DevExpress.XtraBars.BarSubItem
    Private WithEvents iSaveAs As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iExit As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iClear As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonPaste As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iFind As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonCut As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonCopy As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonUndo As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iReplace As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iSelectAll As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonFontBold As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonAlignRight As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonAlignCenter As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonFontUnderline As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonAlignLeft As DevExpress.XtraBars.BarButtonItem
    Private WithEvents buttonFontItalic As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iProtected As DevExpress.XtraBars.BarButtonItem
    Private WithEvents iWeb As DevExpress.XtraBars.BarButtonItem
    Private popupControlContainer1 As DevExpress.XtraBars.PopupControlContainer
    Private labelStatus As DevExpress.XtraBars.BarStaticItem
    Private components As System.ComponentModel.IContainer
    Private iToolBars As DevExpress.XtraBars.BarToolbarsListItem
    Private barStandard As DevExpress.XtraBars.Bar
    Private barFormat As DevExpress.XtraBars.Bar
    Private barFont As DevExpress.XtraBars.Bar
    Private barStatus As DevExpress.XtraBars.Bar
    Private iPaintStyle As DevExpress.XtraBars.BarSubItem
    Private WithEvents ipsWXP As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsOXP As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsO2K As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsO3 As DevExpress.XtraBars.BarButtonItem
    Private WithEvents ipsDefault As DevExpress.XtraBars.BarButtonItem
    Private imageList2 As System.Windows.Forms.ImageList
    Private barAndDockingController1 As DevExpress.XtraBars.BarAndDockingController
    Private WithEvents iAbout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemZoomTrackBar2 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ZoomSlider As DevExpress.XtraBars.BarEditItem
    Friend WithEvents ZoomTrackBar As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents buttonTextColor As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemColorEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemColorEdit
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents comboFontSize As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RtfContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents bCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents buttonOk As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents buttonNo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents comboFont As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemFontEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemFontEdit
End Class

