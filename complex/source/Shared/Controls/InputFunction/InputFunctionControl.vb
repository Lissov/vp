Imports ModelBase
Imports Functions
Imports System.Collections.Generic
Imports Steema.TeeChart
Imports Steema.TeeChart.Styles

Public Class InputFunctionControl

#Region "Const"

    Private Const MAX_CHART_VALUE As Double = 100000

#End Region

#Region "Properties"

    Private _InputFunctionsList As InputFunctionsList
    Public Property InputFunctionsList() As InputFunctionsList
        Get
            Return _InputFunctionsList
        End Get
        Set(ByVal value As InputFunctionsList)
            _InputFunctionsList = value
        End Set
    End Property

    Private ReadOnly Property SelectedValue() As Value
        Get
            Return AllValuesComboBox.SelectedItem
        End Get
    End Property

    Private ReadOnly Property SelectedInputFunction() As InputFunction
        Get
            Return InputFunctionsList.ValuesDict(SelectedValue)
        End Get
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ClearChart()

        FillFunctionTypeComboBox()
    End Sub

    Public Sub New(ByVal Values As List(Of Value))

        Me.New()

        InputFunctionsList = New InputFunctionsList(Values)

        UpdateUI()

    End Sub

#End Region

#Region "Private methods"

    Private Sub FillAllValuesComboBox()

        AllValuesComboBox.Items.Clear()

        For Each Value As Value In InputFunctionsList.ValuesDict.Keys
            AllValuesComboBox.Items.Add(Value)
        Next

        AllValuesComboBox.SelectedIndex = 0

    End Sub

    Private Sub FillFunctionTypeComboBox()
        FunctionTypeComboBox.Items.Clear()

        Dim Type As InputFunction.FunctionTypes

        For Type = InputFunction.FunctionTypes.First To InputFunction.FunctionTypes.Last Step 1
            If Type = InputFunction.FunctionTypes.First OrElse Type = InputFunction.FunctionTypes.Last Then Continue For

            Dim StringValue As String
            StringValue = InputFunction.EnumValueToString(Type)
            If Not String.IsNullOrEmpty(StringValue) Then FunctionTypeComboBox.Items.Add(StringValue)
        Next

    End Sub

    Private Sub FunctionTypeComboBox_SelectType(ByVal type As InputFunction.FunctionTypes)
        For Each Item As String In FunctionTypeComboBox.Items
            If Item = type.ToString Then
                FunctionTypeComboBox.SelectedItem = Item
                Exit For
            End If
        Next
    End Sub

    Private Function GetTypeByName(ByVal functionTypeName As String) As InputFunction.FunctionTypes
        Dim Result As InputFunction.FunctionTypes

        For Type = InputFunction.FunctionTypes.First To InputFunction.FunctionTypes.Last Step 1
            If InputFunction.EnumValueToString(Type) = functionTypeName Then
                Result = Type
                Exit For
            End If
        Next

        Return Result
    End Function

    Private Sub FillProperties(ByVal inputFunction As InputFunction)
        Dim CurrentFunction As FunctionBase
        CurrentFunction = inputFunction.GetFunctionByType(inputFunction.Type)

        FunctionPicture.Image = CurrentFunction.Image
        FormulaTextBox.Text = CurrentFunction.DisplayFormulaText

        ParameterGrid.Rows.Clear()

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Parameter As ModelBase.Parameter In CurrentFunction.GetParameters
            RowIndex = ParameterGrid.Rows.Add()
            Row = ParameterGrid.Rows(RowIndex)
            Row.Cells(ParameterNameColumn.Index).Value = Parameter.DisplayName
            Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
            Row.Tag = Parameter
        Next

    End Sub

    Private Sub UpdateUI()
        StartTimeTextBox.Text = InputFunctionsList.StartTime.ToString
        EndTimeTextBox.Text = InputFunctionsList.EndTime.ToString

        FillAllValuesComboBox()
    End Sub

    Private Sub SetupFormulaControls(ByVal isFormulaVisible As Boolean, ByVal isFormulaEnabled As Boolean, ByVal isGridVisible As Boolean)
        'hide\show formula
        FormulaLabel.Visible = isFormulaVisible
        FormulaTextBox.Visible = isFormulaVisible
        FormulaTextBox.ReadOnly = Not isFormulaEnabled
        HelpButton.Visible = isFormulaEnabled
        If HelpButton.Visible Then
            If FormulaTextBox.Width + FormulaTextBox.Left > HelpButton.Left Then
                FormulaTextBox.Width -= HelpButton.Width + 5
            End If
        Else
            If FormulaTextBox.Width + FormulaTextBox.Left < HelpButton.Left Then
                FormulaTextBox.Width += HelpButton.Width + 5
            End If
        End If

        'hide\show properties
        FunctionPicture.Visible = Not isFormulaVisible
        ParametersLabel.Visible = isGridVisible
        ParameterGrid.Visible = isGridVisible
    End Sub

    Private Sub UpdateCurrentFunction()
        Dim Type As InputFunction.FunctionTypes
        Type = GetTypeByName(FunctionTypeComboBox.SelectedItem)

        SelectedInputFunction.Type = Type

        If SelectedInputFunction.Type = InputFunction.FunctionTypes.Formula Then
            SelectedInputFunction.Formula = FormulaTextBox.Text
        Else
            For Each Row As DataGridViewRow In ParameterGrid.Rows
                Dim Parameter As ModelBase.Parameter
                Parameter = TryCast(Row.Tag, ModelBase.Parameter)
                If Parameter Is Nothing Then Continue For

                Dim NewValue As String
                NewValue = Row.Cells(ParameterValueColumn.Index).Value
                Dim NewInitValue As Double
                If Not Double.TryParse(NewValue, NewInitValue) Then
                    Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
                    Continue For
                End If

                If NewInitValue < Parameter.MinValue Then
                    Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
                ElseIf NewInitValue > Parameter.MaxValue Then
                    Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
                Else
                    Parameter.Value = NewValue
                End If
            Next
        End If

    End Sub

    Private Function CreateMinMaxPointsStyle() As Steema.TeeChart.Styles.Points
        Dim MinMaxPoints As New Steema.TeeChart.Styles.Points

        MinMaxPoints.LinePen.Color = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        MinMaxPoints.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None
        MinMaxPoints.Marks.Callout.ArrowHeadSize = 8
        MinMaxPoints.Marks.Callout.Brush.Color = System.Drawing.Color.Black
        MinMaxPoints.Marks.Callout.Distance = 0
        MinMaxPoints.Marks.Callout.Draw3D = False
        MinMaxPoints.Marks.Callout.Length = 0
        MinMaxPoints.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle
        MinMaxPoints.Marks.Font.Shadow.Visible = False
        MinMaxPoints.Marks.Font.Unit = System.Drawing.GraphicsUnit.World
        MinMaxPoints.Pointer.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        MinMaxPoints.Pointer.Dark3D = False
        MinMaxPoints.Pointer.Draw3D = False
        MinMaxPoints.Pointer.HorizSize = 1
        MinMaxPoints.Pointer.InflateMargins = False
        MinMaxPoints.Pointer.Pen.Color = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        MinMaxPoints.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.SmallDot
        MinMaxPoints.Pointer.VertSize = 1
        MinMaxPoints.Pointer.Visible = True
        MinMaxPoints.Title = "@MinMax"
        MinMaxPoints.XValues.DataMember = "X"
        MinMaxPoints.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending
        MinMaxPoints.YValues.DataMember = "Y"
        MinMaxPoints.Clear()

        Return MinMaxPoints
    End Function

#End Region

#Region "Chart"

    Public Sub ClearChart()
        For i As Integer = PreviewChart.Series.Count - 1 To 0 Step -1
            PreviewChart.Series(i).Dispose()
        Next

        PreviewChart.Series.Clear()
        'PreviewChart.Header.Visible = False
        PreviewChart.Footer.Visible = False
        PreviewChart.Panel.Shadow.Visible = False
        PreviewChart.Aspect.View3D = False
    End Sub

    Private Sub ShowPreview()
        Try
            UpdateCurrentFunction()

            ClearChart()

            Dim MinValue As Double = 0
            Dim MaxValue As Double = 0

            Dim Times As Double()
            Dim Values As Double()
            Dim [Step] As Double = (InputFunctionsList.EndTime - InputFunctionsList.StartTime) / InputFunctionsList.StepsCount

            ReDim Times(InputFunctionsList.StepsCount)
            ReDim Values(InputFunctionsList.StepsCount)

            Dim Time As Double = InputFunctionsList.StartTime
            Dim CurrentStep As Integer = 0
            While CurrentStep <= InputFunctionsList.StepsCount
                Times(CurrentStep) = Time
                Values(CurrentStep) = FixSerieValue(GetCalculatedValue(SelectedValue, Time))

                If Values(CurrentStep) < MinValue Then MinValue = Values(CurrentStep)
                If Values(CurrentStep) > MaxValue Then MaxValue = Values(CurrentStep)

                'update values
                Time += [Step]
                CurrentStep += 1
            End While

            'create serie
            Dim Serie As Series
            Dim LineStyle As New Steema.TeeChart.Styles.Line
            Serie = PreviewChart.Series.Add(LineStyle)
            Serie.ShowInLegend = False
            PreviewChart.Series.Add(Serie)

            Serie.Visible = True
            Serie.Add(Times, Values)

            'add min and max points
            Dim MinMaxSerie As Series = CreateMinMaxPointsStyle()
            MinMaxSerie.ShowInLegend = False
            PreviewChart.Series.Add(MinMaxSerie)
            MinMaxSerie.Visible = True
            MinMaxSerie.Add(0, MinValue - 0.5)
            MinMaxSerie.Add(0, MaxValue + 0.5)
        Catch
        End Try
    End Sub

    Private Function FixSerieValue(ByVal value As Double) As Double
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then
            value = MAX_CHART_VALUE
        End If
        If value > MAX_CHART_VALUE Then
            value = MAX_CHART_VALUE
        End If
        If value < -MAX_CHART_VALUE Then
            value = -MAX_CHART_VALUE
        End If

        Return value
    End Function

#End Region


#Region "Public methods"

    Public Function GetCalculatedValue(ByVal value As Value, ByVal time As Double)
        Return InputFunctionsList.GetCalculatedValue(value, time)
    End Function

#End Region

#Region "Event handlers"

    Private Sub AllValuesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllValuesComboBox.SelectedIndexChanged
        FunctionTypeComboBox_SelectType(SelectedInputFunction.Type)

        FunctionTypeComboBox_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub FunctionTypeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FunctionTypeComboBox.SelectedIndexChanged
        Dim Type As InputFunction.FunctionTypes
        Type = GetTypeByName(FunctionTypeComboBox.SelectedItem)

        SelectedInputFunction.Type = Type

        Dim CurrentFunction As FunctionBase
        CurrentFunction = SelectedInputFunction.GetFunctionByType(SelectedInputFunction.Type)

        Dim IsFormulaVisible As Boolean = (Type = InputFunction.FunctionTypes.Formula) OrElse Not String.IsNullOrEmpty(CurrentFunction.DisplayFormulaText)
        Dim IsFormulaEnabled As Boolean = (Type = InputFunction.FunctionTypes.Formula)
        Dim IsGridVisible As Boolean = (Type <> InputFunction.FunctionTypes.Formula)
        SetupFormulaControls(IsFormulaVisible, IsFormulaEnabled, IsGridVisible)

        If Type = InputFunction.FunctionTypes.Formula Then
            FormulaTextBox.Text = SelectedInputFunction.Formula
        Else
            FillProperties(SelectedInputFunction)
        End If

        ShowPreview()
    End Sub

    Private Sub ParameterGrid_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ParameterGrid.CellValueChanged
        Dim RowIndex As Integer = e.RowIndex
        If RowIndex < 0 OrElse RowIndex >= ParameterGrid.Rows.Count Then
            Return
        End If

        Dim Row As DataGridViewRow
        Row = ParameterGrid.Rows(RowIndex)

        Dim Parameter As ModelBase.Parameter
        Parameter = TryCast(Row.Tag, ModelBase.Parameter)
        If Parameter Is Nothing Then Return

        Dim NewValue As String
        NewValue = Row.Cells(ParameterValueColumn.Index).Value
        Dim NewInitValue As Double
        If Not Double.TryParse(NewValue, NewInitValue) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
            Return
        End If

        If NewInitValue < Parameter.MinValue Then
            MsgBox("Input value was less then min value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
        ElseIf NewInitValue > Parameter.MaxValue Then
            MsgBox("Input value was bigger then max value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
        Else
            Parameter.Value = NewValue
        End If

        ShowPreview()
    End Sub

    Private Sub FormulaTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormulaTextBox.TextChanged
        Dim Type As InputFunction.FunctionTypes
        Type = GetTypeByName(FunctionTypeComboBox.SelectedItem)
        If Type <> InputFunction.FunctionTypes.Formula Then Return

        SelectedInputFunction.Formula = FormulaTextBox.Text

        If MathExpression.TestTimeExpression(FormulaTextBox.Text) Then
            ShowPreview()
        End If

    End Sub

    Private Sub TimeTextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles StartTimeTextBox.KeyPress, EndTimeTextBox.KeyPress
        If Not (e.KeyChar >= "0"c AndAlso e.KeyChar <= "9"c) AndAlso Not e.KeyChar = vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub StartTimeTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTimeTextBox.TextChanged
        If InputFunctionsList Is Nothing Then Return

        Dim NewValueString As String
        NewValueString = StartTimeTextBox.Text

        Dim NewValue As Double
        If Not Double.TryParse(NewValueString, NewValue) Then
            MsgBox("Start time was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            StartTimeTextBox.Text = InputFunctionsList.StartTime.ToString
            Return
        End If

        If NewValue < 0 Then
            MsgBox("Start time was less then 0", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            StartTimeTextBox.Text = InputFunctionsList.StartTime.ToString
        ElseIf NewValue > InputFunctionsList.EndTime Then
            MsgBox("Start time was bigger then end time", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            StartTimeTextBox.Text = InputFunctionsList.StartTime.ToString
        Else
            InputFunctionsList.StartTime = NewValue

            'refresh chart
            ShowPreview()
        End If
    End Sub

    Private Sub EndTimeTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndTimeTextBox.TextChanged
        If InputFunctionsList Is Nothing Then Return

        Dim NewValueString As String
        NewValueString = EndTimeTextBox.Text

        Dim NewValue As Double
        If Not Double.TryParse(NewValueString, NewValue) Then
            MsgBox("End time was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            EndTimeTextBox.Text = InputFunctionsList.EndTime.ToString
            Return
        End If

        If NewValue < 0 Then
            MsgBox("End time was less then 0", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            EndTimeTextBox.Text = InputFunctionsList.StartTime.ToString
        ElseIf NewValue < InputFunctionsList.StartTime Then
            MsgBox("End time was less then end time", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            EndTimeTextBox.Text = InputFunctionsList.EndTime.ToString
        Else
            InputFunctionsList.EndTime = NewValue

            'refresh chart
            ShowPreview()
        End If
    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        ShowPreview()
    End Sub

    Private Sub HelpButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpButton.Click
        Dim Process As System.Diagnostics.Process
        Dim FileName As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "InputControlHelp.mht")

        Try
            If Functions.File.FileExists(FileName) Then

                Dim FileInfo As New System.IO.FileInfo(FileName)
                If FileInfo.Exists Then
                    'use try-catch to avoid exeptions for some files
                    Try
                        FileInfo.Attributes = IO.FileAttributes.ReadOnly
                    Catch ex As Exception
                    End Try
                    FileInfo.GetAccessControl()
                End If

                Dim startInfo As New System.Diagnostics.ProcessStartInfo
                startInfo.FileName = FileName
                startInfo.Arguments = ""
                startInfo.UseShellExecute = True
                startInfo.WindowStyle = ProcessWindowStyle.Maximized

                System.Diagnostics.Process.Start(startInfo)
            Else
                ShowErrorMessage("Help file is missing")
            End If
        Catch ex As Exception
            ShowExeptionMessage(ex, String.Format("Unable to open file '{0}'", FileName))
            Return
        End Try
    End Sub

#End Region

#Region "Load-save"

    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.Filter = String.Format("input functions (*{0})|*{0}", InputFunctionsList.INPUT_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            LoadInput(OpenFileDialog.FileName)
        End If
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        SaveFileDialog.Filter = String.Format("input functions (*{0})|*{0}", InputFunctionsList.INPUT_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            If SaveInput(SaveFileDialog.FileName) Then
                MsgBox("Input functions were successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If
        End If

    End Sub

    Public Function SaveInput(ByVal fileName As String) As Boolean
        Dim Result As Boolean = True

        Try
            Dim XmlDocument As System.Xml.XmlDocument
            XmlDocument = InputFunctionsList.ToXmlDocument
            XmlDocument.Save(fileName)
        Catch ex As Exception
            Result = False
        End Try

        Return Result
    End Function

    Public Sub LoadInput(ByVal fileName As String)
        Try
            Dim XmlDocument As New System.Xml.XmlDocument
            XmlDocument.Load(fileName)
            InputFunctionsList.FromXmlDocument(XmlDocument)

            UpdateUI()

        Catch ex As Exception
            MsgBox(String.Format("Unable to load input functions from file '{0}'", fileName), MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return
        End Try
    End Sub

#End Region

#Region "Progress-messages"

    Private Sub ShowInformationMessage(ByVal prompt As String)
        MsgBox(prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
    End Sub

    Private Sub ShowErrorMessage(ByVal prompt As String)
        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
    End Sub

    Private Sub ShowExeptionMessage(ByVal ex As Exception, ByVal description As String)
        Dim ExceptionMessage As New SharedControls.ExceptionMessage(description, ex)
        ExceptionMessage.ShowDialog()
    End Sub

#End Region

End Class
