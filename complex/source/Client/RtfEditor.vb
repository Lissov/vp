Imports DevExpress.XtraEditors

Public Class RtfEditor
    Inherits DevExpress.XtraEditors.XtraForm

#Region "Properties"

    Private _DocumentChanged As Boolean = False
    ''' <summary>
    ''' If true document was edited by this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentChanged() As Boolean
        Get
            Return _DocumentChanged
        End Get
        Set(ByVal value As Boolean)
            _DocumentChanged = value
        End Set
    End Property

    Private _EnableEvents As Boolean = True
    Public Property EnableEvents() As Boolean
        Get
            Return _EnableEvents
        End Get
        Set(ByVal value As Boolean)
            _EnableEvents = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets font size used by default
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefaultFontSize() As Single
        Get
            Return rtfText.Font.Size
        End Get
        Set(ByVal value As Single)
            Dim NewFont As Font
            NewFont = New Font(rtfText.Font.Name, value)
            rtfText.Font = NewFont
        End Set
    End Property

    ''' <summary>
    ''' If true (as default) status panel (bottom panel) will be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowStatusPanel() As Boolean
        Get
            Return barStatus.Visible
        End Get
        Set(ByVal value As Boolean)
            barStatus.Visible = value
        End Set
    End Property

    Private _ShowEditors As Boolean = True
    ''' <summary>
    ''' If true (as default) bars with editors (top panel) will be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowEditors() As Boolean
        Get
            Return _ShowEditors
        End Get
        Set(ByVal value As Boolean)
            _ShowEditors = value
            barFont.Visible = value
            barStandard.Visible = value
            barFormat.Visible = value
        End Set
    End Property

    Private _ReadOnlyState As Boolean = False
    ''' <summary>
    ''' If true all edit actions will be forbidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReadOnlyState() As Boolean
        Get
            Return _ReadOnlyState
        End Get
        Set(ByVal value As Boolean)
            _ReadOnlyState = value
            ShowEditors = Not _ReadOnlyState
            rtfText.ReadOnly = True
            rtfText.BackColor = Color.White

            If _ReadOnlyState Then
                bCut.Visible = False
                bPaste.Visible = False
            End If

        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the current text in the rich text box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SimpleText() As String
        Get
            Return rtfText.Text
        End Get
        Set(ByVal value As String)
            rtfText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the text of the control, including all rich text format (RTF) codes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rtf() As String
        Get
            Return rtfText.Rtf
        End Get
        Set(ByVal value As String)
            rtfText.Rtf = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets context menu for RichTextBox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TextBoxContextMenu() As ContextMenuStrip
        Get
            Return rtfText.ContextMenuStrip
        End Get
        Set(ByVal value As ContextMenuStrip)
            rtfText.ContextMenuStrip = value
            UseOwnContextMenu = False
        End Set
    End Property

    Private _UseOwnContextMenu As Boolean = True
    ''' <summary>
    ''' If true predefined in this control context menu will be shown 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseOwnContextMenu() As Boolean
        Get
            Return _UseOwnContextMenu
        End Get
        Set(ByVal value As Boolean)
            _UseOwnContextMenu = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region

#Region "Control events"

    Private Sub Control_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        barFormat.Offset = barStandard.Offset + barStandard.FloatSize.Width
        barFont.Offset = barFormat.Offset + barFormat.FloatSize.Width

        EnableDocMenuItems()
        UpdateStatusBar()

        comboFont.EditValue = rtfText.Font.Name
        comboFontSize.EditValue = CStr(rtfText.Font.Size)

    End Sub

#End Region

#Region "Private helping methods"


    ''' <summary>
    ''' Gets font according to checked buttons
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNewFont() As System.Drawing.Font
        Dim FontStyle As System.Drawing.FontStyle
        If buttonFontBold.Down Then
            FontStyle = FontStyle Or System.Drawing.FontStyle.Bold
        End If
        If buttonFontItalic.Down Then
            FontStyle = FontStyle Or System.Drawing.FontStyle.Italic
        End If
        If buttonFontUnderline.Down Then
            FontStyle = FontStyle Or System.Drawing.FontStyle.Underline
        End If

        Dim FontName As String
        If Not comboFont.EditValue Is Nothing Then
            FontName = comboFont.EditValue.ToString()
        Else
            FontName = rtfText.Font.Name
        End If
        Dim FontSize As Int32
        If Not comboFontSize.EditValue Is Nothing Then
            FontSize = Int32.Parse(comboFontSize.EditValue.ToString())
        Else
            FontSize = CInt(rtfText.Font.Size)
        End If

        Dim NewFont As New System.Drawing.Font(FontName, FontSize, FontStyle)

        Return NewFont
    End Function

#End Region

#Region "Update menu & status"

    Private Sub UpdateStatusBar()
        Dim Line As String = "Line" + " "
        Dim Symbol As String = "Ch" + " "
        Dim CurrentLine As Integer
        CurrentLine = rtfText.GetLineFromCharIndex(rtfText.SelectionStart) + 1
        Dim CurrentSymbol As Integer
        CurrentSymbol = rtfText.SelectionStart - rtfText.GetFirstCharIndexOfCurrentLine
        labelStatus.Caption = Line + CurrentLine.ToString() + Chr(9) + "   " + Symbol + CurrentSymbol.ToString()
    End Sub

    Private Sub UpdateSelectionButtonState()
        EnableEvents = False

        UpdateStatusBar()

        If rtfText.SelectionLength = 0 Then
            buttonCut.Enabled = False
            buttonCopy.Enabled = False
            bCut.Enabled = False
            bCopy.Enabled = False
        Else
            buttonCut.Enabled = True
            buttonCopy.Enabled = True
            bCut.Enabled = True
            bCopy.Enabled = True
        End If

        buttonTextColor.EditValue = rtfText.SelectionColor

        buttonAlignLeft.Down = (rtfText.SelectionAlignment = HorizontalAlignment.Left)
        buttonAlignRight.Down = (rtfText.SelectionAlignment = HorizontalAlignment.Right)
        buttonAlignCenter.Down = (rtfText.SelectionAlignment = HorizontalAlignment.Center)

        If Not rtfText.SelectionFont Is Nothing Then
            buttonFontBold.Down = rtfText.SelectionFont.Bold
            buttonFontItalic.Down = rtfText.SelectionFont.Italic
            buttonFontUnderline.Down = rtfText.SelectionFont.Underline

            comboFont.EditValue = rtfText.SelectionFont.Name
            comboFontSize.EditValue = CStr(rtfText.SelectionFont.Size)
        End If

        buttonUndo.Enabled = rtfText.CanUndo

        EnableEvents = True
    End Sub

    Private Sub EnableDocMenuItems()
        If Not Me.Visible Then Exit Sub

        ' Disabled Edit items
        buttonPaste.Enabled = True
        bPaste.Enabled = True
        bSelectAll.Enabled = True

        ' Disable Format items
        buttonAlignLeft.Enabled = True
        buttonAlignRight.Enabled = True
        buttonAlignCenter.Enabled = True
        buttonFontBold.Enabled = True
        buttonFontItalic.Enabled = True
        buttonFontUnderline.Enabled = True
        buttonTextColor.Enabled = True
        comboFont.Enabled = True
        comboFontSize.Enabled = True

        UpdateSelectionButtonState()
    End Sub

    Private Sub DisableDocMenuItems()
        ' Disabled Edit items
        buttonCut.Enabled = False
        buttonCopy.Enabled = False
        buttonPaste.Enabled = False
        bCut.Enabled = False
        bCopy.Enabled = False
        bPaste.Enabled = False
        bSelectAll.Enabled = False

        ' Disable Format items
        buttonAlignLeft.Enabled = False
        buttonAlignRight.Enabled = False
        buttonAlignCenter.Enabled = False
        buttonFontBold.Enabled = False
        buttonFontItalic.Enabled = False
        buttonFontUnderline.Enabled = False
        buttonTextColor.Enabled = False
        comboFont.Enabled = False
        comboFontSize.Enabled = False
    End Sub

#End Region

#Region "Text box events"

    Private Sub rtfText_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtfText.SelectionChanged
        UpdateSelectionButtonState()
    End Sub

    Private Sub rtfText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtfText.TextChanged
        DocumentChanged = True
    End Sub

    Private Sub rtfText_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtfText.MouseDown
        If e.Button <> MouseButtons.Right Then Exit Sub
        If ReadOnlyState Then Exit Sub

        If UseOwnContextMenu Then
            'Show context menu
            RtfContextMenu.Show(Control.MousePosition)
        ElseIf TextBoxContextMenu IsNot Nothing Then
            TextBoxContextMenu.Show(rtfText, New System.Drawing.Point(e.X, e.Y))
        End If

    End Sub

#End Region

#Region "Execute command"

    Private Sub buttonFontBold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonFontBold.DownChanged
        If Not EnableEvents Then Return

        If rtfText.SelectionFont Is Nothing Then
            rtfText.SelectionFont = GetNewFont()
        Else
            If rtfText.SelectionFont.Bold Then
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style And Not (rtfText.SelectionFont.Style And FontStyle.Bold)))
            Else
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style Or FontStyle.Bold))
            End If
        End If

        CommandExecuted()
    End Sub

    Private Sub buttonFontItalic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonFontItalic.DownChanged
        If Not EnableEvents Then Return

        If rtfText.SelectionFont Is Nothing Then
            rtfText.SelectionFont = GetNewFont()
        Else

            If rtfText.SelectionFont.Italic Then
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style And Not (rtfText.SelectionFont.Style And FontStyle.Italic)))
            Else
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style Or FontStyle.Italic))
            End If
        End If

        CommandExecuted()
    End Sub

    Private Sub buttonFontUnderline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonFontUnderline.DownChanged
        If Not EnableEvents Then Return

        If rtfText.SelectionFont Is Nothing Then
            rtfText.SelectionFont = GetNewFont()
        Else
            If rtfText.SelectionFont.Underline Then
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style And Not (rtfText.SelectionFont.Style And FontStyle.Underline)))
            Else
                rtfText.SelectionFont = New Font(rtfText.SelectionFont, (rtfText.SelectionFont.Style Or FontStyle.Underline))
            End If
        End If

        CommandExecuted()
    End Sub

    Private Sub buttonAlignLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonAlignLeft.DownChanged
        If Not EnableEvents Then Return

        rtfText.SelectionAlignment = HorizontalAlignment.Left

        CommandExecuted()
    End Sub

    Private Sub buttonAlignCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonAlignCenter.DownChanged
        If Not EnableEvents Then Return

        rtfText.SelectionAlignment = HorizontalAlignment.Center

        CommandExecuted()
    End Sub

    Private Sub buttonAlignRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonAlignRight.DownChanged
        If Not EnableEvents Then Return

        rtfText.SelectionAlignment = HorizontalAlignment.Right

        CommandExecuted()
    End Sub

    Private Sub buttonTextColor_SelectedColorChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonTextColor.EditValueChanged
        If Not EnableEvents Then Return

        If buttonTextColor.EditValue IsNot Nothing AndAlso TypeOf buttonTextColor.EditValue Is Color Then
            rtfText.SelectionColor = CType(buttonTextColor.EditValue, Color)
        End If

        CommandExecuted()
    End Sub


    Private Sub buttonUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonUndo.ItemClick
        If Not EnableEvents Then Return

        rtfText.Undo()

        CommandExecuted()
    End Sub

    Private Sub buttonCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCut.Click, buttonCut.ItemClick
        If Not EnableEvents OrElse ReadOnlyState Then Return

        rtfText.Cut()

        CommandExecuted()
    End Sub

    Private Sub buttonCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCopy.Click, buttonCopy.ItemClick
        If Not EnableEvents Then Return

        rtfText.Copy()

        CommandExecuted()
    End Sub

    Private Sub buttonPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bPaste.Click, buttonPaste.ItemClick
        If Not EnableEvents OrElse ReadOnlyState Then Return

        rtfText.Paste()

        CommandExecuted()
    End Sub

    Private Sub comboFont_ComboBoxTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboFont.EditValueChanged
        If Not EnableEvents Then Return

        If Not comboFont.EditValue Is Nothing Then
            'some fonts could be not supported
            Try
                If rtfText.SelectionFont Is Nothing Then
                    rtfText.SelectionFont = GetNewFont()
                Else
                    Dim NewFont As Font
                    NewFont = New Font(comboFont.EditValue.ToString(), rtfText.SelectionFont.Size, rtfText.SelectionFont.Style)
                    rtfText.SelectionFont = NewFont
                End If
            Catch
            End Try
        End If

        CommandExecuted()
    End Sub

    Private Sub comboFontSize_ComboBoxTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboFontSize.EditValueChanged
        If Not EnableEvents Then Return

        If Not comboFontSize.EditValue Is Nothing Then
            If rtfText.SelectionFont Is Nothing Then
                rtfText.SelectionFont = GetNewFont()
            Else
                Dim NewFont As Font
                NewFont = New Font(rtfText.SelectionFont.Name, Int32.Parse(comboFontSize.EditValue.ToString()), rtfText.SelectionFont.Style)
                rtfText.SelectionFont = NewFont
            End If
        End If

        CommandExecuted()
    End Sub

    Private Sub bSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSelectAll.Click
        If Not EnableEvents Then Return

        rtfText.SelectAll()

        CommandExecuted()
    End Sub

    Private Sub CommandExecuted()
        If Not EnableEvents Then Return

        DocumentChanged = True

        UpdateSelectionButtonState()
    End Sub

#End Region

#Region "Other events"

    Private Sub ZoomTrackBar_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomTrackBar.EditValueChanging
        Dim val As Integer = CType(e, Controls.ChangingEventArgs).NewValue
        If val < 100 Then

            val = CInt((val / 100 * 75 + 25))
        Else
            val = CInt(((val - 100) / 100.0F * 400 + 100))
        End If

        rtfText.ZoomFactor = val / 100.0F
    End Sub

#End Region

#Region "Public methods"

    Public Sub InsertText(ByVal textToInsert As String)
        Dim Index As Integer = rtfText.SelectionStart

        rtfText.SelectionLength = 1
        rtfText.SelectedText += textToInsert

        rtfText.SelectionStart = Index + textToInsert.Length + 1
    End Sub

#End Region


#Region "Exit"

    Private Sub buttonOk_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles buttonOk.ItemClick
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub buttonNo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles buttonNo.ItemClick
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region
    


End Class

