Imports System.Collections.Generic
Imports System.ComponentModel

Public Class ResultGridView

#Region "Const"

    Public Const MSG_BOX_TITLE As String = "Modelling tool"

#End Region

#Region "Declarations"

    Friend RunningProcess As New SharedControls.App.RunningProcess

#End Region

#Region "Properties"

    Private _CurrentConfig As ModelBase.Configuration
    Private Property CurrentConfig() As ModelBase.Configuration
        Get
            Return _CurrentConfig
        End Get
        Set(ByVal value As ModelBase.Configuration)
            _CurrentConfig = value
            If _CurrentConfig IsNot Nothing Then
                FromXmlString(_CurrentConfig.ResultGridViewData)
            End If
        End Set
    End Property

    Private _Times As ModelBase.ObjectList(Of Decimal)
    ''' <summary>
    ''' All added times
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Times() As ModelBase.ObjectList(Of Decimal)
        Get
            If _Times Is Nothing Then
                _Times = New ModelBase.ObjectList(Of Decimal)
            End If
            _Times.Sort(ListSortDirection.Ascending)
            Return _Times
        End Get
        Set(ByVal value As ModelBase.ObjectList(Of Decimal))
            _Times = value
        End Set
    End Property

    Private _Values As ModelBase.ObjectList(Of ValueItem)
    ''' <summary>
    ''' All added values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Values() As ModelBase.ObjectList(Of ValueItem)
        Get
            If _Values Is Nothing Then
                _Values = New ModelBase.ObjectList(Of ValueItem)
            End If
            Return _Values
        End Get
        Set(ByVal value As ModelBase.ObjectList(Of ValueItem))
            _Values = value
        End Set
    End Property

    Private _LastShownTime As Decimal = 0
    Private Property LastShownTime() As Decimal
        Get
            Return _LastShownTime
        End Get
        Set(ByVal value As Decimal)
            _LastShownTime = value
        End Set
    End Property


#End Region

#Region "Public methods"

    Public Sub Initialize(ByVal configuration As ModelBase.Configuration)
        CurrentConfig = configuration

        LastShownTime = 0
        ClearTable()
        RebuildTable()
    End Sub

    Public Sub PrepareGrid()
        LastShownTime = 0
        ClearTable()
    End Sub

    Public Sub UpdateGridWithCalculatedData()
        If CurrentConfig Is Nothing Then Return

        Dim CurrentTime As Decimal = CurrentConfig.GetCurrentTime

        If LastShownTime > CurrentTime Then LastShownTime = 0

        For Each Time As Decimal In Times
            If Time >= LastShownTime AndAlso Time <= CurrentTime Then
                FillRowWithCalculatedData(Time)
            End If
        Next

        LastShownTime = CurrentTime
    End Sub

#End Region

#Region "Private helping methods"

    Private Sub ClearTable()
        Dim TimeColumnIndex As Integer = GetTimeColumnIndex()

        For i As Integer = 0 To ResultsGrid.RowCount - 1
            For j As Integer = 0 To ResultsGrid.ColumnCount - 1
                If j <> TimeColumnIndex Then
                    ResultsGrid.Rows(i).Cells(j).Value = ""
                End If
            Next
        Next

    End Sub

    ''' <summary>
    ''' Removes all rows and columns from table
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveAllFromTable()
        ResultsGrid.Rows.Clear()
        ResultsGrid.Columns.Clear()
    End Sub

    ''' <summary>
    ''' Adds proper rows and columns according to current Times and Values
    ''' Rows = Times
    ''' Coumns = values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RebuildTable()
        RemoveAllFromTable()

        'first column - fot time, then add column for each valueitem
        Dim TimeColumn As New System.Windows.Forms.DataGridViewTextBoxColumn
        TimeColumn.HeaderText = "Time"
        TimeColumn.Tag = "Time"
        ResultsGrid.Columns.Add(TimeColumn)
        For Each ValueItem As ValueItem In Me.Values
            Dim Column As New System.Windows.Forms.DataGridViewTextBoxColumn
            Column.HeaderText = ValueItem.DisplayName
            Column.Tag = ValueItem
            ResultsGrid.Columns.Add(Column)
        Next

        'add row for each time
        For Each Time As Decimal In Me.Times
            Dim Row As New DataGridViewRow
            Row.Tag = Time
            ResultsGrid.Rows.Add(Row)
        Next

        'fill rows
        For Each Time As Decimal In Me.Times
            Dim Row As DataGridViewRow = GetRowByTime(Time)
            If Row IsNot Nothing Then
                Row.Cells(TimeColumn.Index).ValueType = GetType(String)
                Row.Cells(TimeColumn.Index).Value = Time.ToString
            End If
        Next

    End Sub

    Private Sub FillRowWithCalculatedData(ByVal time As Decimal)
        Dim Row As DataGridViewRow = GetRowByTime(time)
        If Row IsNot Nothing Then
            For Each ValueItem As ValueItem In Me.Values
                Dim ColumnIndex As Integer = GetValueColumnIndex(ValueItem)
                If ColumnIndex = -1 Then Continue For

                Row.Cells(ColumnIndex).ValueType = GetType(String)
                Row.Cells(ColumnIndex).Value = ValueItem.CalculatedValue(time)
            Next
        End If
    End Sub


    ''' <summary>
    ''' Returns index of column "Time" or -1 of column not found
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTimeColumnIndex() As Integer
        Dim Result As Integer = -1

        For Each Column As DataGridViewColumn In ResultsGrid.Columns
            If Column.Tag IsNot Nothing AndAlso TypeOf Column.Tag Is String AndAlso CType(Column.Tag, String) = "Time" Then
                Result = Column.Index
                Exit For
            End If
        Next

        Return Result
    End Function

    ''' <summary>
    ''' Returns index of column "Time" or -1 of column not found
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetValueColumnIndex(ByVal value As ValueItem) As Integer
        Dim Result As Integer = -1

        For Each Column As DataGridViewColumn In ResultsGrid.Columns
            If Column.Tag IsNot Nothing AndAlso TypeOf Column.Tag Is ValueItem AndAlso CType(Column.Tag, ValueItem).Equals(value) Then
                Result = Column.Index
                Exit For
            End If
        Next

        Return Result
    End Function


    Private Function GetRowByTime(ByVal time As Decimal) As DataGridViewRow
        For Each Row As DataGridViewRow In ResultsGrid.Rows
            If Row.Tag IsNot Nothing AndAlso TypeOf Row.Tag Is Decimal AndAlso CType(Row.Tag, Decimal) = time Then
                Return Row
            End If
        Next

        Return Nothing
    End Function

    Private Function GetGridResult() As String(,)
        'Dim Result(ResultsGrid.RowCount + 1, ResultsGrid.ColumnCount) As String
        Dim Result(ResultsGrid.RowCount, ResultsGrid.ColumnCount - 1) As String

        'add column headers
        For j As Integer = 0 To ResultsGrid.ColumnCount - 1
            Result(0, j) = ResultsGrid.Columns(j).HeaderText
        Next

        'add values
        For i As Integer = 0 To ResultsGrid.RowCount - 1
            For j As Integer = 0 To ResultsGrid.ColumnCount - 1
                Result(i + 1, j) = ResultsGrid.Rows(i).Cells(j).Value
            Next
        Next

        Return Result
    End Function

    ''' <summary>
    ''' Copies all grid + column headers to the windows' clopboard in Excel format
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyAllToClipboard()
        Dim Text As String = String.Empty

        'add column headers
        For j As Integer = 0 To ResultsGrid.ColumnCount - 1
            If Not String.IsNullOrEmpty(Text) Then
                Text = Text & vbTab & ResultsGrid.Columns(j).HeaderText
            Else
                Text = ResultsGrid.Columns(j).HeaderText
            End If
        Next

        'add values
        Dim IsFirstRowAdded As Boolean
        For i As Integer = 0 To ResultsGrid.RowCount - 1
            Text &= vbCrLf
            IsFirstRowAdded = False
            For j As Integer = 0 To ResultsGrid.ColumnCount - 1
                If IsFirstRowAdded Then
                    Text &= vbTab & ResultsGrid.Rows(i).Cells(j).Value
                Else
                    IsFirstRowAdded = True
                    Text &= ResultsGrid.Rows(i).Cells(j).Value
                End If
            Next
        Next

        Clipboard.SetText(Text)

        MsgBox("All values are copied to the clipboard", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)

    End Sub

    ''' <summary>
    ''' Copies celected cells to the windows' clopboard in Excel format
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopySelectedCellsToClipboard()
        If ResultsGrid.SelectedCells Is Nothing OrElse ResultsGrid.SelectedCells.Count = 0 Then Return

        'get all possible indexes of all selected cells
        Dim RowIndexes As New List(Of Integer)
        For Each Cell As DataGridViewCell In ResultsGrid.SelectedCells
            If Not RowIndexes.Contains(Cell.RowIndex) Then RowIndexes.Add(Cell.RowIndex)
        Next
        RowIndexes.Sort()

        'get all possible indexes of all selected columns
        Dim ColumnIndexes As New List(Of Integer)
        For Each Cell As DataGridViewCell In ResultsGrid.SelectedCells
            If Not ColumnIndexes.Contains(Cell.ColumnIndex) Then ColumnIndexes.Add(Cell.ColumnIndex)
        Next
        ColumnIndexes.Sort()


        Dim Text As String = String.Empty

        Dim IsFirstRowAdded As Boolean
        For Each RowIndex As Integer In RowIndexes
            IsFirstRowAdded = False
            For Each ColumnIndex As Integer In ColumnIndexes
                Dim Value As String = String.Empty
                For Each Cell As DataGridViewCell In ResultsGrid.SelectedCells
                    If Cell.RowIndex = RowIndex AndAlso Cell.ColumnIndex = ColumnIndex Then
                        If Cell.Value IsNot Nothing Then Value = Cell.Value
                        Exit For
                    End If
                Next
                If IsFirstRowAdded Then
                    Text &= vbTab & Value
                Else
                    IsFirstRowAdded = True
                    Text &= Value
                End If
            Next
            Text &= vbCrLf
        Next

        Clipboard.SetText(Text)

        MsgBox("Selected values are copied to the clipboard", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)

    End Sub

#End Region

#Region "Hot keys handlers"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Dim e As New KeyEventArgs(keyData)

        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopySelectedCellsToClipboard()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

#Region "Button's events"

    Private Sub AddModelToResultGridButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddModelValueToResultGridButton.Click
        Dim ModelSelectForm As New ClientControls.ModelSelector(CurrentConfig)
        If ModelSelectForm.ShowDialog() = DialogResult.OK Then
            If Values.Where(Function(v) v.Value.Equals(ModelSelectForm.SelectedValue)).Count = 0 Then
                Dim ValueItem As New ValueItem
                ValueItem.Value = ModelSelectForm.SelectedValue
                ValueItem.Model = CurrentConfig.GetModelByValue(ValueItem.Value)
                Values.Add(ValueItem)
                RebuildTable()
            Else
                MsgBox("This value was already added to the result grid", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
            End If
        End If
    End Sub

    Private Sub AddTimeToResultGridButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTimeToResultGridButton.Click
        Dim TimeSelectForm As New ClientControls.TimeSelector
        If TimeSelectForm.ShowDialog() = DialogResult.OK Then
            If TimeSelectForm.SelectedTime < 0 Then
                MsgBox("Time must be greater then 0", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
            Else
                If Not Times.Contains(TimeSelectForm.SelectedTime) Then
                    Times.Add(TimeSelectForm.SelectedTime)
                    RebuildTable()
                Else
                    MsgBox("This time was already added to the result grid", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
                End If
            End If
        End If
    End Sub

    Private Sub RemoveFromGridResultButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromGridResultButton.Click
        Times = Nothing
        Values = Nothing
        RebuildTable()
    End Sub

    Private Sub CopyGridResultButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyGridResultButton.Click
        CopyAllToClipboard()
    End Sub

    Private Sub SaveGridResultButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveGridResultButton.Click
        If CurrentConfig Is Nothing Then Return

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.RESULT_EXTENSION
        SaveFileDialog.InitialDirectory = MyApplication.DataFolder
        SaveFileDialog.Filter = String.Format("grid results (*{0})|*{0}", ModelBase.Configuration.REPORT_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileDialog.FileName

            SaveFileDialog.Dispose()
            GC.Collect()
            Me.Refresh()

            ShowProgress("Generating file...")
            Try
                OfficeIntegration.Report.SaveGridResult(FileName, CurrentConfig, GetGridResult())
                ShowInformationMessage("File was successfully saved")
            Catch ex As Exception
                ShowExeptionMessage(ex, "Generating file failed")
            Finally
                HideProgress()
            End Try

        End If
    End Sub

#End Region

#Region "Grid events"

    Private Sub ResultsGrid_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ResultsGrid.ColumnHeaderMouseClick
        If e.Button <> Forms.MouseButtons.Right Then Return

        If e.ColumnIndex = GetTimeColumnIndex() Then Return

        Dim ContextMenu As New ContextMenuStrip
        Dim MenuItem As New ToolStripMenuItem("Delete value", My.Resources.delete_16x16)
        MenuItem.Tag = e.ColumnIndex
        AddHandler MenuItem.Click, AddressOf DeleteValueMenuItemClicked
        ContextMenu.Items.Add(MenuItem)

        ContextMenu.Show(ResultsGrid, e.X + ResultsGrid.GetColumnDisplayRectangle(e.ColumnIndex, True).Left, e.Y)
    End Sub

    Private Sub DeleteValueMenuItemClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender Is Nothing OrElse Not TypeOf sender Is ToolStripMenuItem OrElse CType(sender, ToolStripMenuItem).Tag Is Nothing OrElse Not TypeOf CType(sender, ToolStripMenuItem).Tag Is Integer Then Return

        Dim ColumnIndex As Integer = CInt(CType(sender, ToolStripMenuItem).Tag)
        Dim Column As DataGridViewColumn = ResultsGrid.Columns(ColumnIndex)
        If Column Is Nothing OrElse Column.Tag Is Nothing Then Return
        Dim ValueItem As ValueItem = TryCast(Column.Tag, ValueItem)
        If ValueItem Is Nothing Then Return

        If MsgBox("Are you sure you want to delete value '" & ValueItem.DisplayName & "'?", MsgBoxStyle.Question Or MsgBoxStyle.OkCancel, MSG_BOX_TITLE) = MsgBoxResult.Ok Then
            If Values.Contains(ValueItem) Then Values.Remove(ValueItem)
            RebuildTable()
        End If

    End Sub

    Private Sub ResultsGrid_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ResultsGrid.RowHeaderMouseClick
        If e.Button <> Forms.MouseButtons.Right Then Return

        If Not e.RowIndex >= 0 OrElse Not e.ColumnIndex = -1 Then Return

        Dim ContextMenu As New ContextMenuStrip
        Dim MenuItem As New ToolStripMenuItem("Delete time", My.Resources.delete_16x16)
        MenuItem.Tag = e.RowIndex
        AddHandler MenuItem.Click, AddressOf DeleteTimeMenuItemClicked
        ContextMenu.Items.Add(MenuItem)

        ContextMenu.Show(ResultsGrid, e.X, ResultsGrid.GetCellDisplayRectangle(0, e.RowIndex, True).Y + e.Y)

    End Sub

    Private Sub DeleteTimeMenuItemClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender Is Nothing OrElse Not TypeOf sender Is ToolStripMenuItem OrElse CType(sender, ToolStripMenuItem).Tag Is Nothing OrElse Not TypeOf CType(sender, ToolStripMenuItem).Tag Is Integer Then Return

        Dim RowIndex As Integer = CInt(CType(sender, ToolStripMenuItem).Tag)
        Dim Row As DataGridViewRow = ResultsGrid.Rows(RowIndex)
        If Row Is Nothing OrElse Row.Tag Is Nothing OrElse Not TypeOf Row.Tag Is Decimal Then Return
        Dim Time As Decimal = CType(Row.Tag, Decimal)

        If MsgBox("Are you sure you want to delete time '" & Time.ToString & "'?", MsgBoxStyle.Question Or MsgBoxStyle.OkCancel, MSG_BOX_TITLE) = MsgBoxResult.Ok Then
            If Times.Contains(Time) Then Times.Remove(Time)
            RebuildTable()
        End If

    End Sub

    Private Sub ResultsGrid_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ResultsGrid.CellMouseClick
        If e.Button <> Forms.MouseButtons.Right Then Return

        If Not ResultsGrid.SelectedCells.Count > 1 Then Return

        Dim ContextMenu As New ContextMenuStrip
        Dim MenuItem As New ToolStripMenuItem("Copy selected values", My.Resources.copy_16x16)
        AddHandler MenuItem.Click, AddressOf CopySelectedValuesMenuItemClicked
        ContextMenu.Items.Add(MenuItem)

        ContextMenu.Show(ResultsGrid, ResultsGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).X + e.X, ResultsGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True).Y + e.Y)

    End Sub

    Private Sub CopySelectedValuesMenuItemClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender Is Nothing OrElse Not TypeOf sender Is ToolStripMenuItem Then Return

        CopySelectedCellsToClipboard()
    End Sub

#End Region

#Region "Private classes"

    Private Class ValueItem
        Inherits ModelBase.ObjectBase

        Public Model As ModelBase.IModel
        Public Value As ModelBase.Value

        Public ReadOnly Property DisplayName() As String
            Get
                Return Model.DisplayName & "\" & Value.DisplayName
            End Get
        End Property

        Public ReadOnly Property CalculatedValue(ByVal time As Decimal) As String
            Get
                Dim Result As String = String.Empty

                If Model IsNot Nothing Then
                    Result = Model.GetValueByTime(Value, time).ToString
                End If

                Return Result
            End Get
        End Property

        'properties to load from xml
        Private _ModelName As String
        Public Property ModelName() As String
            Get
                Return _ModelName
            End Get
            Set(ByVal value As String)
                _ModelName = value
            End Set
        End Property

        Private _ValueName As String
        Public Property ValueName() As String
            Get
                Return _ValueName
            End Get
            Set(ByVal value As String)
                _ValueName = value
            End Set
        End Property

#Region "Xml methods"

        Public Overloads Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
            Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

            SetAttribute(CurrentElement, "ModelName", Model.GetName)
            SetAttribute(CurrentElement, "ValueName", Value.Name)

            Return CurrentElement
        End Function

        Public Overloads Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
            If currentElement.Attributes("ModelName") IsNot Nothing Then
                ModelName = GetString(currentElement, "ModelName")
            End If
            If currentElement.Attributes("ValueName") IsNot Nothing Then
                ValueName = GetString(currentElement, "ValueName")
            End If

            Return Me
        End Function

#End Region


    End Class

#End Region

#Region "Progress-messages"

    Private Sub ShowProgress(ByVal message As String)
        If Not RunningProcess.IsProgressRunning Then
            RunningProcess = New SharedControls.App.RunningProcess(message)
            RunningProcess.StartProgress()
        Else
            RunningProcess.UpdateProgress(message)
        End If
    End Sub

    Private Sub HideProgress()
        If RunningProcess.IsProgressRunning Then
            RunningProcess.EndProgress()
        End If
    End Sub

    Private Sub ShowInformationMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
    End Sub

    Private Sub ShowErrorMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
    End Sub

    Private Sub ShowExeptionMessage(ByVal ex As Exception, ByVal description As String)
        HideProgress()

        Dim ExceptionMessage As New SharedControls.ExceptionMessage(description, ex)
        ExceptionMessage.ShowDialog()

    End Sub

#End Region

#Region "Xml methods"

    Public Function ToXmlString() As String
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement("ResultGridView")
        XmlDocument.AppendChild(XmlElement)
        Times.ToXmlElement(XmlElement, "Times")
        Values.ToXmlElement(XmlElement, "Values")
        Return XmlDocument.OuterXml
    End Function

    Public Sub FromXmlString(ByVal xmlString As String)
        If String.IsNullOrEmpty(xmlString) Then
            Times = Nothing
            Values = Nothing
        Else
            Try
                Dim XmlDocument As New Xml.XmlDocument
                XmlDocument.LoadXml(xmlString)
                Dim CurrentElement As Xml.XmlElement = XmlDocument.DocumentElement
                Times = ModelBase.ObjectList(Of Decimal).FromXml(CurrentElement, "Times")
                Values = ModelBase.ObjectList(Of ValueItem).FromXml(CurrentElement, "Values")
                'update values with real model+value from configuration
                For i As Integer = Values.Count - 1 To 0 Step -1
                    Values(i).Model = CurrentConfig.GetModelByName(Values(i).ModelName)
                    If Values(i).Model Is Nothing Then
                        Values.RemoveAt(i)
                    Else
                        Values(i).Value = Values(i).Model.GetValue(Values(i).ValueName)
                        If Values(i).Value Is Nothing Then Values.RemoveAt(i)
                    End If
                Next
            Catch ex As Exception
                ShowExeptionMessage(ex, "Unable to open saved data")
                Times = Nothing
                Values = Nothing
            End Try
        End If

    End Sub

#End Region


End Class
