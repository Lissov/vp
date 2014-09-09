Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils.Drawing

Public Class CustomizeForm

#Region "Properties"

    Private _EventsAllowed As Boolean = False
    Public Property EventsAllowed() As Boolean
        Get
            Return _EventsAllowed
        End Get
        Set(ByVal value As Boolean)
            _EventsAllowed = value
        End Set
    End Property

    Private _SkinName As String
    Public Property SkinName() As String
        Get
            Return _SkinName
        End Get
        Set(ByVal value As String)
            _SkinName = value
        End Set
    End Property

    Private Property ClientFont() As Font
        Get
            Return AppStaticClass.MainForm.UserPreference.Font
        End Get
        Set(ByVal value As Font)
            AppStaticClass.MainForm.ApplyNewFont(value)
        End Set
    End Property

    Private Property ClientHintColor() As Color
        Get
            Return AppStaticClass.MainForm.UserPreference.HintColor
        End Get
        Set(ByVal value As Color)
            AppStaticClass.MainForm.ApplyNewHintColor(value)
        End Set
    End Property

    Private Property ClientHintShowBevel() As Boolean
        Get
            Return AppStaticClass.MainForm.UserPreference.ShowHintBevel
        End Get
        Set(ByVal value As Boolean)
            AppStaticClass.MainForm.ApplyNewHintBevelStyle(value)
        End Set
    End Property

    ''' <summary>
    ''' Maximum amount of shown points per 1 sec
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Client_MaxPointsPerSecond() As Integer
        Get
            Return AppStaticClass.MainForm.UserPreference.MaxPointsPerSecond
        End Get
        Set(ByVal value As Integer)
            AppStaticClass.MainForm.UserPreference.MaxPointsPerSecond = value
        End Set
    End Property

    ''' <summary>
    ''' Maximum amount of shown points per 1 sec
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Client_MaxMarksCount() As Integer
        Get
            Return AppStaticClass.MainForm.UserPreference.MaxMarksCount
        End Get
        Set(ByVal value As Integer)
            AppStaticClass.MainForm.UserPreference.MaxMarksCount = value
        End Set
    End Property

#Region "save result parameters"

    Public Property Result_SaveOnlyVisiblePoints() As Boolean
        Get
            Return AppStaticClass.MainForm.UserPreference.Result_SaveOnlyVisiblePoints
        End Get
        Set(ByVal value As Boolean)
            AppStaticClass.MainForm.UserPreference.Result_SaveOnlyVisiblePoints = value
        End Set
    End Property

    Public Property Report_SaveOnlyVisiblePoints() As Boolean
        Get
            Return AppStaticClass.MainForm.UserPreference.Report_SaveOnlyVisiblePoints
        End Get
        Set(ByVal value As Boolean)
            AppStaticClass.MainForm.UserPreference.Report_SaveOnlyVisiblePoints = value
        End Set
    End Property

    Public Property ResultImage_SaveColoured() As Boolean
        Get
            Return AppStaticClass.MainForm.UserPreference.ResultImage_SaveColoured
        End Get
        Set(ByVal value As Boolean)
            AppStaticClass.MainForm.UserPreference.ResultImage_SaveColoured = value
        End Set
    End Property

#End Region

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
        FillSkins()
    End Sub

    Public Sub New(ByVal panels As List(Of ClientPanel), _
                   ByVal buttons As List(Of DevExpress.XtraBars.BarButtonItem))
        Me.New()

        FillCustomizePanelsListView(panels)
        FillCustomizeButtonsListView(buttons)
    End Sub


#End Region

#Region "Private methods"

    Private Sub FillCustomizePanelsListView(ByVal panels As List(Of ClientPanel))
        CustomizePanelsListView.Items.Clear()

        If panels Is Nothing OrElse panels.Count = 0 Then Return

        Try
            For Each Panel As ClientPanel In panels
                Dim PanelItem As New ListViewItem(Panel.Text)
                PanelItem.Checked = Panel.Visible
                PanelItem.Tag = Panel
                CustomizePanelsListView.Items.Add(PanelItem)
            Next
        Catch
        End Try

    End Sub

    Private Sub FillCustomizeButtonsListView(ByVal buttons As List(Of DevExpress.XtraBars.BarButtonItem))
        CustomizeButtonsListView.Items.Clear()

        If buttons Is Nothing OrElse buttons.Count = 0 Then Return

        Try
            For Each Button As DevExpress.XtraBars.BarButtonItem In buttons
                Dim ButtonItem As New ListViewItem(Button.Caption & " (" & Button.Description & ")")
                ButtonItem.Checked = (Button.Visibility = DevExpress.XtraBars.BarItemVisibility.Always)
                ButtonItem.Tag = Button
                CustomizeButtonsListView.Items.Add(ButtonItem)
            Next
        Catch
        End Try

    End Sub

    Private Sub FillSkins()
        StyleComboBox.Properties.Items.Clear()
        StyleImageList.Images.Clear()

        For Index As Integer = 0 To SkinManager.Default.Skins.Count - 1
            Dim Skin As SkinContainer = SkinManager.Default.Skins(Index)
            Dim ImageComboBoxItem As New ImageComboBoxItem()
            ImageComboBoxItem.Description = Skin.SkinName
            ImageComboBoxItem.Value = Skin.SkinName

            StyleImageList.Images.Add(Index.ToString, GetSkinImage(Skin, 16, 16))
            ImageComboBoxItem.ImageIndex = StyleImageList.Images.IndexOfKey(Index.ToString)

            StyleComboBox.Properties.Items.Add(ImageComboBoxItem)
        Next

        For Each Item As ImageComboBoxItem In StyleComboBox.Properties.Items
            If Item.Description = SkinName Then
                StyleComboBox.SelectedIndex = StyleComboBox.Properties.Items.IndexOf(Item)
            End If
        Next

    End Sub

    Private Function GetSkinImage(ByVal skin As SkinContainer, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim Button As New SimpleButton
        Button.LookAndFeel.SetSkinStyle(skin.SkinName)

        Dim image As Bitmap = New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(image)
            Dim info As StyleObjectInfoArgs = New StyleObjectInfoArgs(New GraphicsCache(g))
            info.Bounds = New Rectangle(0, 0, width, height)
            Button.LookAndFeel.Painter.GroupPanel.DrawObject(info)
            Button.LookAndFeel.Painter.Border.DrawObject(info)
            info.Bounds = New Rectangle(0, 0, width, height)
            Button.LookAndFeel.Painter.Button.DrawObject(info)
        End Using

        Return image
    End Function

#End Region

#Region "Events handlers"

    Private Sub CustomizeForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FontNameComboBox.EditValue = ClientFont.Name
        FontSizeComboBox.EditValue = CStr(ClientFont.Size)

        HintBevelCheckEdit.Checked = ClientHintShowBevel
        HintColorEdit.Color = ClientHintColor
        MaxPointsPerSecTextEdit.Text = Client_MaxPointsPerSecond
        MaxMarksCountTextEdit.Text = Client_MaxMarksCount

        'save tab
        Result_SaveOnlyVisiblePoints_CheckEdit.Checked = Result_SaveOnlyVisiblePoints
        If ResultImage_SaveColoured Then
            SaveImageTypeComboBox.SelectedIndex = 0
        Else
            SaveImageTypeComboBox.SelectedIndex = 1
        End If
        Report_SaveOnlyVisiblePoints_CheckEdit.Checked = Report_SaveOnlyVisiblePoints
    End Sub

    Private Sub CustomizeForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        EventsAllowed = True
    End Sub

    Private Sub CustomizePanelsListView_ItemChecked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles CustomizePanelsListView.ItemChecked
        If Not EventsAllowed Then Return

        If e.Item Is Nothing OrElse e.Item.Tag Is Nothing OrElse Not TypeOf e.Item.Tag Is ClientPanel Then Return

        Try
            CType(e.Item.Tag, ClientPanel).ChangeVisibility(e.Item.Checked)
        Catch
        End Try

    End Sub

    Private Sub CustomizeButtonsListView_ItemChecked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles CustomizeButtonsListView.ItemChecked
        If Not EventsAllowed Then Return

        If e.Item Is Nothing OrElse e.Item.Tag Is Nothing OrElse Not TypeOf e.Item.Tag Is DevExpress.XtraBars.BarButtonItem Then Return

        Try
            If e.Item.Checked Then
                CType(e.Item.Tag, DevExpress.XtraBars.BarButtonItem).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                CType(e.Item.Tag, DevExpress.XtraBars.BarButtonItem).Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If
        Catch
        End Try

        If AppStaticClass.MainForm IsNot Nothing Then
            AppStaticClass.MainForm.UpdateRibbonPanels()
        End If
    End Sub

    Private Sub StyleComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StyleComboBox.SelectedIndexChanged
        Dim Item As ImageComboBoxItem
        Item = StyleComboBox.Properties.Items(StyleComboBox.SelectedIndex)
        Dim NewSkinName As String = Item.Description
        'apply new skin
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(NewSkinName)
    End Sub

    Private Sub FontNameComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontNameComboBox.SelectedIndexChanged
        If Not EventsAllowed Then Return

        'apply new font
        If Not FontNameComboBox.EditValue Is Nothing Then
            'some fonts could be not supported
            Try
                Dim FamilyName As String = FontNameComboBox.EditValue.ToString
                Dim EmSize As Single = ClientFont.SizeInPoints
                Dim Style As System.Drawing.FontStyle = Font.Style

                ClientFont = New Font(FamilyName, EmSize, Style)
            Catch
            End Try
        End If
    End Sub

    Private Sub FontSizeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontSizeComboBox.SelectedIndexChanged
        If Not EventsAllowed Then Return

        'apply new font
        If Not FontSizeComboBox.EditValue Is Nothing Then
            'some fonts could be not supported
            Try
                Dim FamilyName As String = ClientFont.Name
                Dim EmSize As Single = CType(FontSizeComboBox.EditValue.ToString, Single)
                Dim Style As System.Drawing.FontStyle = Font.Style

                ClientFont = New Font(FamilyName, EmSize, Style)
            Catch
            End Try
        End If
    End Sub

    Private Sub HintColorEdit_ColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HintColorEdit.ColorChanged
        ClientHintColor = HintColorEdit.Color
    End Sub

    Private Sub HintBevelCheckEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HintBevelCheckEdit.CheckedChanged
        ClientHintShowBevel = HintBevelCheckEdit.Checked
    End Sub

    Private Sub MaxPointsPerSecTextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaxPointsPerSecTextEdit.TextChanged
        Client_MaxPointsPerSecond = MaxPointsPerSecTextEdit.Text
    End Sub

    Private Sub MaxMarksCountTextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaxMarksCountTextEdit.TextChanged
        Client_MaxMarksCount = MaxMarksCountTextEdit.Text
    End Sub

#Region "save tab"

    Private Sub Result_SaveOnlyVisiblePoints_CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Result_SaveOnlyVisiblePoints_CheckEdit.CheckedChanged
        Result_SaveOnlyVisiblePoints = Result_SaveOnlyVisiblePoints_CheckEdit.Checked
    End Sub

    Private Sub Report_SaveOnlyVisiblePoints_CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Report_SaveOnlyVisiblePoints_CheckEdit.CheckedChanged
        Report_SaveOnlyVisiblePoints = Report_SaveOnlyVisiblePoints_CheckEdit.Checked
    End Sub

    Private Sub SaveImageTypeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveImageTypeComboBox.SelectedIndexChanged
        ResultImage_SaveColoured = (SaveImageTypeComboBox.SelectedIndex = 0)
    End Sub

#End Region

#End Region


End Class