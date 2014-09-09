Public Class ConfigEditForm
    Implements IObjectWithMenu

#Region "Declarations"

    Private MenuLoader As ModelOutputsMenuLoader
    Private DescriptionEditor As New RtfEditor

#End Region

#Region "Properties"

    Private _Models As List(Of ModelBase.IModel)
    ''' <summary>
    ''' All existed models
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Models() As List(Of ModelBase.IModel)
        Get
            If _Models Is Nothing Then
                _Models = New List(Of ModelBase.IModel)
            End If
            Return _Models
        End Get
        Set(ByVal value As List(Of ModelBase.IModel))
            _Models = value
        End Set
    End Property

    Private _CurrentConfig As ModelBase.Configuration
    Public Property CurrentConfig() As ModelBase.Configuration
        Get
            If _CurrentConfig Is Nothing Then
                _CurrentConfig = New ModelBase.Configuration(String.Empty, Nothing)
            End If
            Return _CurrentConfig
        End Get
        Set(ByVal value As ModelBase.Configuration)
            _CurrentConfig = value
            If _CurrentConfig IsNot Nothing Then
                DescriptionEditor.Rtf = _CurrentConfig.Description
                DescriptionTextEdit.Text = Functions.Text.TrimText(DescriptionEditor.SimpleText, DescriptionTextEdit)
            End If
        End Set
    End Property

    Private _NeedRebuildMenu As Boolean = True
    ''' <summary>
    ''' If true context menu for outputs will be rebuld before show
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property NeedRebuildMenu() As Boolean
        Get
            Return _NeedRebuildMenu
        End Get
        Set(ByVal value As Boolean)
            _NeedRebuildMenu = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DescriptionEditor.Text = "Edit description"

    End Sub

    Public Sub New(ByVal models As List(Of ModelBase.IModel))
        Me.New(Nothing, models)

        Me.Text = "Create configuration"
    End Sub

    Public Sub New(ByVal config As ModelBase.Configuration, ByVal models As List(Of ModelBase.IModel))
        Me.New()

        CurrentConfig = config
        NameTextEdit.Text = CurrentConfig.Name
        Me.Models = models
        Me.Text = String.Format("Edit configuration {0}", CurrentConfig.Name)

        MenuLoader = New ModelOutputsMenuLoader(Me)

        RefreshModelsGrid()
        RefreshLinksGrid()

        CheckButtons()
    End Sub

#End Region

#Region "Main menu"

    Private Sub CloseButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles CloseButton.ItemClick
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SaveButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButton.ItemClick
        If Not ValidateConfiguration() Then Return

        If CurrentConfig.Save Then
            MsgBox("Configuration was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub SaveASButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveASButton.ItemClick
        If Not ValidateConfiguration() Then Return

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        SaveFileDialog.InitialDirectory = My.Settings.DataFolder
        SaveFileDialog.Filter = String.Format("configurations (*{0})|*{0}", ModelBase.Configuration.CONFIG_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            CurrentConfig.FileName = SaveFileDialog.FileName

            If CurrentConfig.Save Then
                MsgBox("Configuration was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If
        End If

        CheckButtons()
    End Sub

    Private Sub AddModelButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles AddModelButton.ItemClick
        Dim AddModelDialog As New AddModelDialog(Models)
        If AddModelDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            'add selected in dialog models
            For Each Model As ModelBase.IModel In AddModelDialog.SelectedModels
                If Not CurrentConfig.Models.Contains(Model) Then
                    CurrentConfig.Models.Add(Model)
                End If
            Next

            'update 
            NeedRebuildMenu = True

            RefreshModelsGrid()
            RefreshLinksGrid()

            CheckButtons()
        End If
    End Sub

    Private Sub RemoveModelButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles RemoveModelButton.ItemClick

        'remove selected in grid models
        For Each Row As DataGridViewRow In ModelsGrid.SelectedRows
            Dim Model As ModelBase.IModel
            Model = TryCast(Row.Tag, ModelBase.IModel)
            If Model IsNot Nothing Then
                If CurrentConfig.Models.Contains(Model) Then
                    CurrentConfig.Models.Remove(Model)
                End If
            End If
        Next

        'update 
        NeedRebuildMenu = True

        RefreshModelsGrid()
        RefreshLinksGrid()

        CheckButtons()
    End Sub

#End Region

#Region "Private methods"

    Private Sub CheckButtons()
        SaveButton.Enabled = Not String.IsNullOrEmpty(CurrentConfig.FileName)

        RemoveModelButton.Enabled = ModelsGrid.SelectedRows.Count > 0
    End Sub

    Private Function ValidateConfiguration() As Boolean
        Dim Validated As Boolean = True

        ErrorProvider1.Clear()

        If String.IsNullOrEmpty(NameTextEdit.Text) Then
            ErrorProvider1.SetError(NameTextEdit, "Name can not be empty")
            Validated = False
        End If

        If CurrentConfig.Models.Count = 0 Then
            ErrorProvider1.SetError(ModelsGrid, "Configuration should have at least one model")
            Validated = False
        End If

        For Each Model As ModelBase.IModel In CurrentConfig.Models
            If Model.GetValues Is Nothing Then Continue For

            For Each Value As ModelBase.Value In Model.GetValues
                If Value.Type = ModelBase.Value.ValueType.Input Then
                    If Value.LinkConst Is Nothing Then
                        If String.IsNullOrEmpty(Value.LinkModelName) OrElse String.IsNullOrEmpty(Value.LinkValueName) Then
                            ErrorProvider1.SetError(LinksGrid, "All links must be set")
                            Validated = False
                            Exit For
                        ElseIf Value.LinkModelName = Model.GetName Then
                            ErrorProvider1.SetError(LinksGrid, "Model can not have a link to herself")
                            Validated = False
                            Exit For
                        ElseIf CurrentConfig.GetModelByName(Value.LinkModelName) Is Nothing Then
                            ErrorProvider1.SetError(LinksGrid, "Model can not have a link to a model which does not exist in configuration")
                            Validated = False
                            Exit For
                        End If
                    End If
                End If
            Next
        Next

        Return Validated
    End Function

    Private Sub RefreshModelsGrid()
        ModelsGrid.Rows.Clear()
        If CurrentConfig Is Nothing OrElse CurrentConfig.Models Is Nothing OrElse CurrentConfig.Models.Count = 0 Then Return

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Model As ModelBase.IModel In CurrentConfig.Models
            RowIndex = ModelsGrid.Rows.Add()
            Row = ModelsGrid.Rows(RowIndex)
            Row.Cells(ModelColumn.Index).Value = Model.DisplayName
            Row.Tag = Model
        Next

    End Sub

    Private Sub RefreshLinksGrid()
        Dim FirstRowIndex = LinksGrid.FirstDisplayedScrollingRowIndex

        LinksGrid.Rows.Clear()
        If CurrentConfig Is Nothing OrElse CurrentConfig.Models Is Nothing OrElse CurrentConfig.Models.Count = 0 Then Return

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Model As ModelBase.IModel In CurrentConfig.Models
            If Model.GetValues Is Nothing Then Continue For

            For Each Value As ModelBase.Value In Model.GetValues
                If Value.Type = ModelBase.Value.ValueType.Input Then
                    RowIndex = LinksGrid.Rows.Add()
                    Row = LinksGrid.Rows(RowIndex)
                    Row.Cells(ModelNameColumn.Index).Value = Model.DisplayName
                    Row.Cells(InputValueColumn.Index).Value = Value.DisplayName
                    If Value.LinkConst IsNot Nothing Then
                        Row.Cells(LinkColumn.Index).Value = Value.LinkConst.Value
                    Else
                        Row.Cells(LinkColumn.Index).Value = Value.LinkModelName & " \ " & Value.LinkValueName
                    End If
                    Row.Tag = Value
                End If
            Next

        Next

        Try
            If FirstRowIndex >= 0 AndAlso FirstRowIndex < LinksGrid.Rows.Count Then
                LinksGrid.FirstDisplayedScrollingRowIndex = FirstRowIndex
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' This sub used to get context menu start point
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStartPoint(ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) As Point
        Dim StartPoint As Point = New Point(LinksGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).Location)
        StartPoint.Y += +LinksGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).Height
        StartPoint.X += 1
        Return StartPoint
    End Function

#End Region

#Region "Events handlers"

    Private Sub NameTextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameTextEdit.TextChanged
        CurrentConfig.Name = NameTextEdit.Text
    End Sub

    Private Sub ModelsGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelsGrid.SelectionChanged
        CheckButtons()
    End Sub

    Private Sub ModelsGrid_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ModelsGrid.KeyDown
        If e.KeyCode = Keys.Delete Then
            RemoveModelButton_ItemClick(RemoveModelButton, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' This sub handles clicks on grid cotext menu button
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Public Sub MenuItemSelected(ByVal item As MenuItem) Implements IObjectWithMenu.MenuItemSelected
        If item Is Nothing Then Return

        Dim Value As ModelBase.Value
        Value = TryCast(LinksGrid.CurrentRow.Tag, ModelBase.Value)
        If Value Is Nothing Then Return

        Value.LinkModelName = item.ModelName
        Value.LinkValueName = item.ValueName
        Value.LinkConst = item.LinkConst

        'refresh links grid
        RefreshLinksGrid()

    End Sub

    Private Sub LinksGrid_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles LinksGrid.CellBeginEdit
        If e.ColumnIndex <> LinkColumn.Index Then
            e.Cancel = True
            Return
        End If

        'build context menu
        If NeedRebuildMenu Then
            LinksMenu = MenuLoader.BuildMenu(CurrentConfig.Models)
            NeedRebuildMenu = False
        End If
        LinksMenu.Show(LinksGrid, GetStartPoint(e))
        e.Cancel = True
    End Sub

    Private Sub LinksGrid_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles LinksGrid.CellMouseDown
        'if click on current cell or cell does not exists - exit
        If Not LinksGrid.CurrentCell Is Nothing _
            AndAlso Not (e.ColumnIndex = LinksGrid.CurrentCell.ColumnIndex _
                AndAlso e.RowIndex = LinksGrid.CurrentCell.RowIndex) Then

            'begin edit new cell
            If e.Button = Windows.Forms.MouseButtons.Left _
               AndAlso e.RowIndex >= 0 _
               AndAlso e.ColumnIndex >= 0 _
               Then
                Try
                    'try to chenge edited cell
                    LinksGrid.CurrentCell = LinksGrid.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    LinksGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
                    LinksGrid.BeginEdit(False)
                Catch
                End Try
            End If
        End If
    End Sub

    Private Sub DescriptionEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescriptionEditButton.Click
        DescriptionEditor.Rtf = Me.CurrentConfig.Description
        If DescriptionEditor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.CurrentConfig.Description = DescriptionEditor.Rtf
            DescriptionTextEdit.Text = Functions.Text.TrimText(DescriptionEditor.SimpleText, DescriptionTextEdit)
        End If
    End Sub

    Private Sub ViewGraphButton_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ViewGraphButton.ItemClick
        Dim GraphForm As New Graph.ConfigurationGraphForm(CurrentConfig)
        GraphForm.ShowDialog()
    End Sub

#End Region




End Class