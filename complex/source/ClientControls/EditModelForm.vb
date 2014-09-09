Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System.CodeDom.Compiler
Imports System.Text

Public Class EditModelForm

#Region "Declarations"

    Friend RunningProcess As New SharedControls.App.RunningProcess

#End Region

#Region "Properties"

    Private _FolderPath As String
    Private Property FolderPath() As String
        Get
            Return _FolderPath
        End Get
        Set(ByVal value As String)
            _FolderPath = value
        End Set
    End Property

    Private _EditableModel As ModelBase.EditableModel
    Private Property EditableModel() As ModelBase.EditableModel
        Get
            If _EditableModel Is Nothing Then
                _EditableModel = New ModelBase.EditableModel
            End If
            Return _EditableModel
        End Get
        Set(ByVal value As ModelBase.EditableModel)
            _EditableModel = value

            If value Is Nothing Then
                Me.Text = "Create new model"
            Else
                Me.Text = String.Format("Edit model '{0}'", value.DisplayName)
            End If

            FillControls()
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal folderPath As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = "Create new model"

        Me.FolderPath = folderPath
        Me.EditableModel = New ModelBase.EditableModel

        InfoMemo.Text = "Add equations in the next style:" & vbCrLf & _
                        "X1.Value(CurrentStep) = X1.Value(CurrentStep - 1) + [Step] * K1.Value" & vbCrLf & _
                        "where" & vbCrLf & _
                        "X1 - variable" & vbCrLf & _
                        "X1.Value(CurrentStep) - its value on the step which is being calculated" & vbCrLf & _
                        "X1.Value(CurrentStep - 1)  - its value on the previous step" & vbCrLf & _
                        "K1 - parameter" & vbCrLf & _
                        "K1.Value - its value" & vbCrLf & _
                        "[Step] - calculation step"
        InfoMemo.DeselectAll()

        EquationsMemoEdit.Focus()
    End Sub

    Public Sub New(ByVal folderPath As String, ByVal editableModel As ModelBase.EditableModel)
        Me.New(folderPath)

        Me.EditableModel = editableModel
    End Sub

#End Region

#Region "Fill controls"

    Private Sub FillControls()
        NameTextEdit.Text = EditableModel.Name
        DisplayNameTextEdit.Text = EditableModel.DisplayName
        DescriptionMemoEdit.Text = EditableModel.Description

        EquationsMemoEdit.Text = EditableModel.Equations

        FileNameTextEdit.Text = EditableModel.FileName

        FillValuesGrid()
        FillParametersGrid()

        CheckButtons()
    End Sub

    Private Sub FillValuesGrid()
        ValuesGrid.Rows.Clear()

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Value As ModelBase.Value In EditableModel.Values
            RowIndex = ValuesGrid.Rows.Add()
            Row = ValuesGrid.Rows(RowIndex)
            Row.Cells(ValueNameColumn.Index).Value = Value.DisplayName
            Row.Cells(ValueTypeColumn.Index).Value = Value.Type.ToString
            Row.Tag = Value
        Next
    End Sub

    Private Sub FillParametersGrid()
        ParameterGrid.Rows.Clear()

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Parameter As ModelBase.Parameter In EditableModel.Parameters
            RowIndex = ParameterGrid.Rows.Add()
            Row = ParameterGrid.Rows(RowIndex)
            Row.Cells(ParameterNameColumn.Index).Value = Parameter.DisplayName
            Row.Tag = Parameter
        Next

    End Sub

#End Region

#Region "Methods for grids"

    Private Function GetSelectedParameter() As ModelBase.Parameter
        If Not (ParameterGrid.SelectedRows.Count = 1) Then Return Nothing
        If ParameterGrid.SelectedRows(0).Tag Is Nothing Then Return Nothing

        Return TryCast(ParameterGrid.SelectedRows(0).Tag, ModelBase.Parameter)
    End Function

    Private Function GetSelectedValue() As ModelBase.Value
        If Not (ValuesGrid.SelectedRows.Count = 1) Then Return Nothing
        If ValuesGrid.SelectedRows(0).Tag Is Nothing Then Return Nothing

        Return TryCast(ValuesGrid.SelectedRows(0).Tag, ModelBase.Value)
    End Function

#End Region

    Private Sub CheckButtons()
        ButtonDeleteParameter.Enabled = ParameterGrid.SelectedRows.Count = 1
        ButtonEditParameter.Enabled = ParameterGrid.SelectedRows.Count = 1
        ButtonDeleteValue.Enabled = ValuesGrid.SelectedRows.Count = 1
        ButtonEditValue.Enabled = ValuesGrid.SelectedRows.Count = 1
    End Sub

#Region "Controls events"

    Private Sub Grid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValuesGrid.SelectionChanged, ParameterGrid.SelectionChanged
        CheckButtons()
    End Sub

#Region "Parameters grid menu events"

    Private Sub ButtonAddParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddParameter.Click
        Dim Parameter As New ModelBase.Parameter()
        Dim ParameterPropertyEditor As New ParameterPropertyEditor(Parameter)
        If ParameterPropertyEditor.ShowDialog(Me) = Forms.DialogResult.OK Then
            Parameter.Name = Parameter.DisplayName

            For Each ModelParameter As ModelBase.Parameter In EditableModel.Parameters
                If Parameter.Name = ModelParameter.Name Then
                    ShowErrorMessage(String.Format("Parameter with name {0} already exists!", Parameter.Name))
                    Return
                ElseIf Parameter.DisplayName = ModelParameter.DisplayName Then
                    ShowErrorMessage(String.Format("Parameter with display name {0} already exists!", Parameter.DisplayName))
                    Return
                End If
            Next

            EditableModel.Parameters.Add(Parameter)

            FillParametersGrid()
        End If

    End Sub

    Private Sub CheckButtonEditParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEditParameter.Click
        Try
            Dim Parameter As ModelBase.Parameter = GetSelectedParameter()
            If Parameter IsNot Nothing Then
                Dim ParameterPropertyEditor As New ParameterPropertyEditor(Parameter)
                If ParameterPropertyEditor.ShowDialog(Me) = Forms.DialogResult.OK Then
                    FillParametersGrid()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub ButtonDeleteParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDeleteParameter.Click
        Try
            Dim Parameter As ModelBase.Parameter = GetSelectedParameter()
            If Parameter IsNot Nothing Then
                If MsgBox(String.Format("Are you sure you want to delete parameter '{0}'?", Parameter.DisplayName), MsgBoxStyle.OkCancel Or MsgBoxStyle.Question, "Modelling tool") <> DialogResult.OK Then Return
                EditableModel.Parameters.Remove(Parameter)

                FillParametersGrid()
            End If
        Catch
        End Try
    End Sub

#End Region

#Region "Values grid menu events"

    Private Sub ButtonAddValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddValue.Click
        Dim Value As New ModelBase.Value()
        Value.Type = ModelBase.Value.ValueType.Output
        Dim ValuePropertyEditor As New ValuePropertyEditor(Value)
        ValuePropertyEditor.FullEditState = True
        If ValuePropertyEditor.ShowDialog(Me) = Forms.DialogResult.OK Then
            Value.Name = Value.DisplayName

            For Each ModelValue As ModelBase.Value In EditableModel.Values
                If Value.Name = ModelValue.Name Then
                    ShowErrorMessage(String.Format("Value with name {0} already exists!", Value.Name))
                    Return
                ElseIf Value.DisplayName = ModelValue.DisplayName Then
                    ShowErrorMessage(String.Format("Value with display name {0} already exists!", Value.DisplayName))
                    Return
                End If
            Next

            EditableModel.Values.Add(Value)

            FillValuesGrid()
        End If
    End Sub

    Private Sub ButtonEditValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEditValue.Click
        Try
            Dim Value As ModelBase.Value = GetSelectedValue()
            If Value IsNot Nothing Then
                Dim ValuePropertyEditor As New ValuePropertyEditor(Value)
                ValuePropertyEditor.FullEditState = True
                If ValuePropertyEditor.ShowDialog(Me) = Forms.DialogResult.OK Then
                    FillValuesGrid()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub ButtonDeleteValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDeleteValue.Click
        Try
            Dim Value As ModelBase.Value = GetSelectedValue()
            If Value IsNot Nothing Then
                If MsgBox(String.Format("Are you sure you want to delete value '{0}'?", Value.DisplayName), MsgBoxStyle.OkCancel Or MsgBoxStyle.Question, "Modelling tool") <> DialogResult.OK Then Return

                EditableModel.Values.Remove(Value)

                FillValuesGrid()
            End If
        Catch
        End Try
    End Sub

#End Region

#End Region

#Region "Main menu"

    Private Sub SaveButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButtonItem.ItemClick
        If Not ValidateModel() Then Return

        FillModel()

        If EditableModel.Save(FolderPath) Then
            MsgBox("Model was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub OpenButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles OpenButtonItem.ItemClick
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.InitialDirectory = FolderPath
        OpenFileDialog.Filter = String.Format("editable models (*{0})|*{0}", ModelBase.EditableModel.FILE_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim LoadedModel As ModelBase.EditableModel = LoadEditableModel(OpenFileDialog.FileName)
            If LoadedModel IsNot Nothing Then
                EditableModel = LoadedModel
            End If
        End If
    End Sub

    Private Sub PreviewCodeButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles PreviewCodeButtonItem.ItemClick
        If Not ValidateModel() Then Return

        FillModel()

        Dim Code As String
        ShowProgress("Code generation...")
        Try
            Code = GetModelClassCode()
        Catch ex As Exception
            ShowExeptionMessage(ex, String.Format("Unable to generate code"))
        Finally
            HideProgress()
        End Try

        If Not String.IsNullOrEmpty(Code) Then
            Dim PreviewForm As New PreviewForm(Code)
            PreviewForm.ShowDialog()
        End If
    End Sub

    Private Sub CompileButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles CompileButtonItem.ItemClick
        If Not ValidateModel() Then Return

        FillModel()

        ShowProgress("Compilation...")
        Try
            If CompileModel() Then
                ShowInformationMessage("Model was successfully compiled. It can be used after application restart.")
            End If
        Catch ex As Exception
            ShowExeptionMessage(ex, String.Format("Unable to compile model"))
        Finally
            HideProgress()
        End Try
    End Sub

#End Region

#Region "Private helping methods"

    Private Function ValidateModel() As Boolean
        ErrorProvider.ClearErrors()

        If String.IsNullOrEmpty(NameTextEdit.Text) Then
            ErrorProvider.SetError(NameTextEdit, "Name must be filled!")
            Return False
        End If
        If Not Functions.Text.IsValidVariableName(NameTextEdit.Text) Then
            ErrorProvider.SetError(NameTextEdit, "Name is not valid! The first character must be a letter A through Z (uppercase or lowercase letters may be used). Succeeding characters can be letters, digits, or the underscore (_) character (no spaces or other characters allowed).")
            Return False
        End If
        If String.IsNullOrEmpty(DisplayNameTextEdit.Text) Then
            ErrorProvider.SetError(DisplayNameTextEdit, "Display name must be filled!")
            Return False
        End If
        If String.IsNullOrEmpty(DescriptionMemoEdit.Text) Then
            ErrorProvider.SetError(DescriptionMemoEdit, "Description must be filled!")
            Return False
        End If
        If String.IsNullOrEmpty(FileNameTextEdit.Text) Then
            ErrorProvider.SetError(FileNameTextEdit, "File name must be filled!")
            Return False
        End If
        If String.IsNullOrEmpty(EquationsMemoEdit.Text) Then
            ErrorProvider.SetError(EquationsMemoEdit, "Equations must be filled!")
            Return False
        End If

        'todo: model name is unique; perameter names are uniques and valid; values names are unique and valid

        Return True
    End Function

    Private Sub FillModel()
        EditableModel.Name = NameTextEdit.Text
        EditableModel.DisplayName = DisplayNameTextEdit.Text
        EditableModel.Description = DescriptionMemoEdit.Text

        EditableModel.Equations = EquationsMemoEdit.Text

        EditableModel.FileName = FileNameTextEdit.Text
    End Sub

#End Region

#Region "Public shared methods"

    Public Shared Function LoadEditableModel(ByVal fileName As String) As ModelBase.EditableModel
        Dim EditableModel As ModelBase.EditableModel = Nothing
        Try
            Dim XmlDocument As New System.Xml.XmlDocument
            XmlDocument.Load(fileName)

            EditableModel = ModelBase.EditableModel.FromXml(XmlDocument, fileName)

        Catch ex As Exception
            Dim ExceptionMessage As New SharedControls.ExceptionMessage(String.Format("Unable to load configuration from file '{0}'", fileName), ex)
            ExceptionMessage.ShowDialog()
            Return Nothing
        End Try

        Return EditableModel
    End Function

#End Region

#Region "Compile"

    Private Function CompileModel() As Boolean
        Dim CodeProvider As New VBCodeProvider
        Dim CodeCompiler As ICodeCompiler = CodeProvider.CreateCompiler()
        Dim CompilerParameters As CompilerParameters = New CompilerParameters()

        CompilerParameters.ReferencedAssemblies.Add("system.dll")
        CompilerParameters.ReferencedAssemblies.Add("ModelBase.dll")
        CompilerParameters.CompilerOptions = "/t:library"
        CompilerParameters.GenerateInMemory = False
        CompilerParameters.OutputAssembly = IO.Path.Combine(FolderPath, EditableModel.FileName & ".dll")

        Dim cr As CompilerResults = CodeCompiler.CompileAssemblyFromSource(CompilerParameters, GetModelClassCode)

        If (cr.Errors.Count > 0) Then
            'unable to compile code
            If Not String.IsNullOrEmpty(cr.Errors(0).ErrorText) AndAlso cr.Errors(0).ErrorText.Contains("Unable to write to output file") Then
                ShowErrorMessage("Unable to compile code:" & vbCrLf & cr.Errors(0).ErrorText & vbCrLf & vbCrLf & "You can change file name and try again.")
            ElseIf Not String.IsNullOrEmpty(cr.Errors(0).ErrorText) Then
                ShowErrorMessage("Unable to compile code:" & vbCrLf & cr.Errors(0).ErrorText)
            Else
                ShowErrorMessage("Unable to compile code")
            End If
            Return False
        End If

        Return True
    End Function

    Private Function GetTestModelClassCode() As String
        Return _
        "Imports ModelBase" & vbCrLf & _
"Public Class Model" & vbCrLf & _
"    Inherits ModelBase.ModelBase" & vbCrLf & _
"    Public External_1 As New ModelBase.Value(""External_1"", ""External_1"", ModelBase.Value.ValueType.Input)" & vbCrLf & _
"    Public X1 As New ModelBase.Value(""X1"", ""X1"", ModelBase.Value.ValueType.Output)" & vbCrLf & _
"    Public K1 As New ModelBase.Parameter(""k1"", ""k1"")" & vbCrLf & _
"    Public Sub New()" & vbCrLf & _
"        MyBase.New()" & vbCrLf & _
"        Me.Name = ""Model1""" & vbCrLf & _
"        Me.DisplayName = ""Model 1""" & vbCrLf & _
"        Me.Description = ""Test model just to calculate dX1/dt=k1""" & vbCrLf & _
"   End Sub" & vbCrLf & _
"    Public Overrides Sub Cycle()" & vbCrLf & _
"        X1.Value(CurrentStep) = X1.Value(CurrentStep - 1) + [Step] * K1.Value" & vbCrLf & _
"    End Sub" & vbCrLf & _
" End Class"
    End Function

    Private Function GetModelClassCode() As String
        Dim sb As New StringBuilder("")
        sb.Append("Imports ModelBase" & vbCrLf)
        sb.Append("Public Class Model" & vbCrLf)
        sb.Append("Inherits ModelBase.ModelBase" & vbCrLf)

        'add values
        If EditableModel.Values IsNot Nothing AndAlso EditableModel.Values.Count > 0 Then
            For Each Value As ModelBase.Value In EditableModel.Values
                Dim ValueType As String
                Select Case Value.Type
                    Case ModelBase.Value.ValueType.Input
                        ValueType = "ModelBase.Value.ValueType.Input"
                    Case ModelBase.Value.ValueType.Internal
                        ValueType = "ModelBase.Value.ValueType.Internal"
                    Case ModelBase.Value.ValueType.Output
                        ValueType = "ModelBase.Value.ValueType.Output"
                End Select
                'use Public Sub New(ByVal name As String, ByVal displayName As String, ByVal type As ValueType, ByVal groupName As String, ByVal initValueVisible As Boolean, ByVal Measure As String)
                Dim ValueString As String
                ValueString = String.Format("Public {0} As New ModelBase.Value(""{0}"", ""{1}"", {2}, ""{3}"", {4}, ""{5}"")", New Object() {Value.Name, Value.DisplayName, ValueType, Value.GroupName, CStr(Value.InitValueVisible), Value.Measure})
                sb.Append(ValueString & vbCrLf)
            Next
        End If

        'add parameters
        If EditableModel.Parameters IsNot Nothing AndAlso EditableModel.Parameters.Count > 0 Then
            For Each Parameter As ModelBase.Parameter In EditableModel.Parameters
                'use Public Sub New(ByVal name As String, ByVal displayName As String)
                Dim ParameterString As String
                ParameterString = String.Format("Public {0} As New ModelBase.Parameter(""{0}"", ""{1}"")", New Object() {Parameter.Name, Parameter.DisplayName})
                sb.Append(ParameterString & vbCrLf)
            Next
        End If

        'add constructor
        sb.Append("Public Sub New()" & vbCrLf)
        sb.Append("MyBase.New()" & vbCrLf)
        sb.Append(String.Format("Me.Name = ""{0}""", EditableModel.Name) & vbCrLf)
        sb.Append(String.Format("Me.DisplayName = ""{0}""", EditableModel.DisplayName) & vbCrLf)
        sb.Append(String.Format("Me.Description = ""{0}""", EditableModel.Description) & vbCrLf)
        sb.Append("End Sub" & vbCrLf)

        'add main method
        sb.Append("Public Overrides Sub Cycle()" & vbCrLf)
        sb.Append(EditableModel.Equations & vbCrLf)
        sb.Append("End Sub" & vbCrLf)

        'add end tag
        sb.Append("End Class" & vbCrLf)

        Return sb.ToString
    End Function

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
        MsgBox(prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Modelling tool")
    End Sub

    Private Sub ShowErrorMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Modelling tool")
    End Sub

    Private Sub ShowExeptionMessage(ByVal ex As Exception, ByVal description As String)
        HideProgress()

        Dim ExceptionMessage As New SharedControls.ExceptionMessage(description, ex)
        ExceptionMessage.ShowDialog()

    End Sub

#End Region



End Class