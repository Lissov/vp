Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading
Imports System
Imports ModelBase.Enums
Imports Microsoft.Win32
Imports Steema.TeeChart
Imports Steema.TeeChart.Styles
Imports VisualControls.Helpers

Public Class MainForm

#Region "Declarations"

    Friend RealTimeTimer As New SharedControls.Timer
    Friend WithEvents CalculatingTimer As New System.Windows.Forms.Timer

    Friend RunningProcess As New SharedControls.App.RunningProcess

#End Region

#Region "Properties"

    Private _AllModels As List(Of ModelBase.IModel)
    ''' <summary>
    ''' All existed models
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllModels() As List(Of ModelBase.IModel)
        Get
            If _AllModels Is Nothing Then
                _AllModels = New List(Of ModelBase.IModel)
            End If
            Return _AllModels
        End Get
        Set(ByVal value As List(Of ModelBase.IModel))
            _AllModels = value
        End Set
    End Property

    Private _CurrentConfig As ModelBase.Configuration
    Public Property CurrentConfig() As ModelBase.Configuration
        Get
            Return _CurrentConfig
        End Get
        Set(ByVal value As ModelBase.Configuration)
            _CurrentConfig = value

            'clear calculation state
            CalculatingState = CalculatingStates.NotStarted

            'update UI
            ClearUI()

            ResultGridView1.Initialize(_CurrentConfig)

            Dim FileVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)

            If _CurrentConfig IsNot Nothing Then
                Me.Text = String.Format("Modelling tool {0} - ", FileVersion.FileVersion) & _CurrentConfig.Name & " (" & IO.Path.GetFileName(_CurrentConfig.FileName) & ")"
                ModelsTree.SetConfiguration(_CurrentConfig)

                If _CurrentConfig.ExperimentTime > 0 Then
                    ExperimentTimeInSec = _CurrentConfig.ExperimentTime
                    FixExperimentTimeMeasure()
                End If
            Else
                Me.Text = String.Format("Modelling tool {0}", FileVersion.FileVersion)
            End If

            UpdateAlwaysShownControls()

            CheckRibbon()
        End Set
    End Property

    Private _HistoryConfigurations As List(Of ModelBase.SavedConfiguration)
    ''' <summary>
    ''' Currently opened saved configurations (to be shown in tree as history)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property HistoryConfigurations() As List(Of ModelBase.SavedConfiguration)
        Get
            If _HistoryConfigurations Is Nothing Then
                _HistoryConfigurations = New List(Of ModelBase.SavedConfiguration)
            End If
            Return _HistoryConfigurations
        End Get
        Set(ByVal value As List(Of ModelBase.SavedConfiguration))
            _HistoryConfigurations = value
        End Set
    End Property

    Private _ExperimentTimeInSec As Decimal = 10
    ''' <summary>
    ''' Experimant time in seconds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExperimentTimeInSec() As Decimal
        Get
            Return _ExperimentTimeInSec
        End Get
        Set(ByVal value As Decimal)
            _ExperimentTimeInSec = value
            UpdateExperimentTime()
        End Set
    End Property

    Private _ExperimentTime As Decimal = 10
    ''' <summary>
    ''' Experiment time in current measure unit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExperimentTime() As Decimal
        Get
            Return _ExperimentTime
        End Get
        Set(ByVal value As Decimal)
            _ExperimentTime = value
        End Set
    End Property

    Private _ExperimentTimeMeasure As TimeHelper.MeasureUnit = TimeHelper.MeasureUnit.Second
    Public Property ExperimentTimeMeasure() As TimeHelper.MeasureUnit
        Get
            Return _ExperimentTimeMeasure
        End Get
        Set(ByVal value As TimeHelper.MeasureUnit)
            _ExperimentTimeMeasure = value
        End Set
    End Property

    Private _HiddenTime As Double = 0
    ''' <summary>
    ''' Time till which results should not be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HiddenTime() As Double
        Get
            If _HiddenTime > 0 Then
                Return _HiddenTime
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            _HiddenTime = value
        End Set
    End Property

    Private _ShownPoints As Double = 100
    ''' <summary>
    ''' Percentage of points which should be shown in chart
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShownPoints() As Double
        Get
            Return _ShownPoints
        End Get
        Set(ByVal value As Double)
            _ShownPoints = value
        End Set
    End Property

    Private _CurrentTime As Decimal = 0
    Public Property CurrentTime() As Decimal
        Get
            Return _CurrentTime
        End Get
        Set(ByVal value As Decimal)
            _CurrentTime = value
        End Set
    End Property

    Private _LastPaintedTime As Decimal = 0
    Public Property LastPaintedTime() As Decimal
        Get
            Return _LastPaintedTime
        End Get
        Set(ByVal value As Decimal)
            _LastPaintedTime = value
        End Set
    End Property

    Private Property ShowResultInProgess() As Boolean
        Get
            Return ShowResultInProgessCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowResultInProgessCheckBox.Checked = value
        End Set
    End Property

    Private Property ShowResultAfterCalculating() As Boolean
        Get
            Return ShowResultAfterCalculatingCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowResultAfterCalculatingCheckBox.Checked = value
        End Set
    End Property

    Private Property ShowInputValues() As Boolean
        Get
            Return ShowInputValuesCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowInputValuesCheckBox.Checked = value
        End Set
    End Property

    Private Property ShowInternalValues() As Boolean
        Get
            Return ShowInternalValuesCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowInputValuesCheckBox.Checked = value
        End Set
    End Property


    Private Property ShowHintOnSerieClick() As Boolean
        Get
            Return ShowHintOnSerieClickCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowHintOnSerieClickCheckBox.Checked = value
        End Set
    End Property

    Private Property ShowHintPercents() As Boolean
        Get
            Return ShowHintPercentsCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            ShowHintPercentsCheckBox.Checked = value
        End Set
    End Property

    Private _CalculatingState As CalculatingStates = CalculatingStates.NotStarted
    Public Property CalculatingState() As CalculatingStates
        Get
            Return _CalculatingState
        End Get
        Set(ByVal value As CalculatingStates)
            If _CalculatingState = value Then Return

            _CalculatingState = value
            Select Case _CalculatingState
                Case CalculatingStates.NotStarted
                    CalculatingStateText.Caption = "Calculating state: not started"
                Case CalculatingStates.InProcess
                    CalculatingStateText.Caption = "Calculating state: in process"
                Case CalculatingStates.Paused
                    CalculatingStateText.Caption = "Calculating state: paused"
                Case CalculatingStates.Finished
                    CalculatingStateText.Caption = "Calculating state: finished"
            End Select

            If _CalculatingState <> CalculatingStates.NotStarted AndAlso CalculatingState <> CalculatingStates.InProcess Then
                If ShowResultAfterCalculating Then
                    RefreshChart()
                End If
            End If
        End Set
    End Property

    Private _UserPreference As UserPreference
    Public Property UserPreference() As UserPreference
        Get
            If _UserPreference Is Nothing Then
                _UserPreference = New UserPreference(Me.ClientPanels, Me.ClientButtons)
            End If
            Return _UserPreference
        End Get
        Set(ByVal value As UserPreference)
            _UserPreference = value
        End Set
    End Property

    Private _ClientPanels As List(Of Client.ClientPanel) = Nothing
    Public ReadOnly Property ClientPanels() As List(Of Client.ClientPanel)
        Get
            If _ClientPanels Is Nothing Then
                _ClientPanels = New List(Of Client.ClientPanel)
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(ModelsPanel))
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(GeneralPanel))
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(PropertiesPanel))
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(InitValuesPanel))
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(SettingsPanel))
                _ClientPanels.Add(Client.ClientPanel.GetClientPanel(GridResultsTabPage))
            End If
            Return _ClientPanels
        End Get
    End Property

    Private _ClientButtons As List(Of DevExpress.XtraBars.BarButtonItem) = Nothing
    Public ReadOnly Property ClientButtons() As List(Of DevExpress.XtraBars.BarButtonItem)
        Get
            If _ClientButtons Is Nothing Then
                _ClientButtons = New List(Of DevExpress.XtraBars.BarButtonItem)
                '_ClientButtons.Add(RunButtonItem)
                '_ClientButtons.Add(PauseButtonItem)
                '_ClientButtons.Add(StopButtonItem)
                _ClientButtons.Add(RefreshResultButtonItem)
                _ClientButtons.Add(SaveResultButtonItem)
                _ClientButtons.Add(OpenResultButtonItem)
                _ClientButtons.Add(SavePictureResultButtonItem)
                _ClientButtons.Add(ReportButtonItem)
                _ClientButtons.Add(ConfigCreateButtonItem)
                _ClientButtons.Add(ConfigEditButtonItem)
                _ClientButtons.Add(ConfigOpenButtonItem)
                _ClientButtons.Add(ConfigSaveButtonItem)
                '_ClientButtons.Add(CustomizeButtonItem)
                _ClientButtons.Add(ConfigurationAboutButtonItem)
                _ClientButtons.Add(HelpButtonItem)
                _ClientButtons.Add(AboutButtonItem)
            End If
            Return _ClientButtons
        End Get
    End Property


    Private _Hints As List(Of Label) = Nothing
    Private ReadOnly Property Hints() As List(Of Label)
        Get
            If _Hints Is Nothing Then
                _Hints = New List(Of Label)
            End If
            Return _Hints
        End Get
    End Property

    Private _FormLoaded As Boolean = False
    ''' <summary>
    ''' If true form was already loaded
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FormLoaded() As Boolean
        Get
            Return _FormLoaded
        End Get
        Set(ByVal value As Boolean)
            _FormLoaded = value
        End Set
    End Property

    ''' <summary>
    ''' Increment of the left chart scale
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LeftScaleIncrement(Optional ByVal showMessage As Boolean = True) As Double
        Get
            Dim value As Double
            If Double.TryParse(LeftScaleTextEdit.Text, value) Then
                If value < 0 Then
                    If showMessage Then
                        MsgBox("Scale increment must be a positive number", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
                    End If
                    LeftScaleTextEdit.Text = "0"
                    Return 0
                Else
                    Return value
                End If
            Else
                If showMessage Then
                    MsgBox("Scale increment must be a positive number", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
                End If
                LeftScaleTextEdit.Text = "0"
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            LeftScaleTextEdit.Text = value.ToString
        End Set
    End Property

    ''' <summary>
    ''' Increment of the bottom chart scale
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BottomScaleIncrement(Optional ByVal showMessage As Boolean = True) As Double
        Get
            Dim value As Double
            If Double.TryParse(BottomScaleTextEdit.Text, value) Then
                If value < 0 Then
                    If showMessage Then
                        MsgBox("Scale increment must be a positive number", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
                    End If
                    BottomScaleTextEdit.Text = "0"
                    Return 0
                Else
                    Return value
                End If
            Else
                If showMessage Then
                    MsgBox("Scale increment must be a positive number", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
                End If
                BottomScaleTextEdit.Text = "0"
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            BottomScaleTextEdit.Text = value.ToString
        End Set
    End Property

    Private _MarkItems As List(Of MarkItem)
    ''' <summary>
    ''' List of marks for grid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MarkItems() As List(Of MarkItem)
        Get
            If _MarkItems Is Nothing Then
                _MarkItems = New List(Of MarkItem)
            End If
            Return _MarkItems
        End Get
        Set(ByVal value As List(Of MarkItem))
            _MarkItems = value
        End Set
    End Property

    Public ReadOnly Property TemporaryFolderForModels() As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, "Modelling tool tempopary data")
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ModelManagementTabPage.PageVisible = False

        AppStaticClass.MainForm = Me

        FillExperimentTimeMeasureComboBox()

        CheckInstallChartService()
        CrackTeeChart()
        ClearChart()

        CurrentConfig = Nothing

        ClientControls.MyApplication.DataFolder = My.Settings.DataFolder
    End Sub

#End Region

#Region "Private helping methods for TeeChart handline"

    Const CHART_SERVICE_NAME As String = "ModellingToolChartService"
    Const CHART_SERVICE_DISPLAY_NAME As String = "Modelling tool chart service"

    Private Sub CrackTeeChart()
        'value of key HKEY_CLASSES_ROOT\CLSID\{ED6227D7-889F-483E-AEF4-C090D24BFEE1}\TypeLib 
        'must be deleted
        Const SubKeyName As String = "CLSID\{ED6227D7-889F-483E-AEF4-C090D24BFEE1}\TypeLib"
        Try
            Registry.ClassesRoot.DeleteSubKey(SubKeyName, False)
        Catch
        End Try
    End Sub

    Private Sub CheckInstallChartService()
        Try
            Dim ServiceDescription As New ServiceDescription
            ServiceDescription.ServiceName = CHART_SERVICE_NAME
            ServiceDescription.ServiceDisplayName = CHART_SERVICE_DISPLAY_NAME
            ServiceDescription.LibraryFileName = "Modelling tool chart service.exe"

            If ServiceDescription.InstallService(RunningProcess) Then
                ServiceDescription.StartService(RunningProcess)
            End If
        Catch ex As Exception
            ShowExeptionMessage(ex, "Error occured while installin Modelling tool chart service")
        End Try
    End Sub

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
        MsgBox(prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
    End Sub

    Private Sub ShowErrorMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
    End Sub

    Private Sub ShowExeptionMessage(ByVal ex As Exception, ByVal description As String)
        HideProgress()

        Dim ExceptionMessage As New SharedControls.ExceptionMessage(description, ex)
        ExceptionMessage.ShowDialog()

    End Sub

#End Region

#Region "Form events"

    Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'save user preference on exit
        If CurrentConfig IsNot Nothing Then
            UserPreference.ConfigurationFileName = CurrentConfig.FileName
        End If
        UserPreference.ExperimentTime = ExperimentTimeInSec
        UserPreference.HiddenTime = HiddenTime
        UserPreference.ShownPoints = ShownPoints
        UserPreference.ShowResultInProgess = ShowResultInProgess
        UserPreference.ShowResultAfterCalculating = ShowResultAfterCalculating
        UserPreference.ShowHintOnSerieClick = ShowHintOnSerieClick
        UserPreference.ShowHintPercents = ShowHintPercents
        UserPreference.Font = New Font(ResultChart.Legend.Font.Name, ResultChart.Legend.Font.Size, Me.Font.Style)
        UserPreference.LeftScaleIncrement = LeftScaleIncrement(False)
        UserPreference.BottomScaleIncrement = BottomScaleIncrement(False)


        UserPreference.WindowState = Me.WindowState

        UserPreference.Save()
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadMainForm()
        Catch ex As Exception
            ShowExeptionMessage(ex, "Error occured while form loading")
        End Try

        FormLoaded = True
    End Sub

    Public Sub LoadMainForm()
        ShowProgress("Loading models...")
        LoadModels()

        'load user preference
        ShowProgress("Loading user preference...")
        UserPreference = UserPreference.Load

        ShowProgress("Updating user interface...")

        Me.WindowState = UserPreference.WindowState
        ShowInputValues = UserPreference.ShowInputValues
        ShowInternalValues = UserPreference.ShowInternalValues

        If UserPreference.Font IsNot Nothing Then
            ApplyNewFont(UserPreference.Font)
        Else
            'font in user preference must be set before user can do something with min form !
            UserPreference.Font = New Font(ResultChart.Legend.Font.Name, ResultChart.Legend.Font.Size, Me.Font.Style)
        End If

        ExperimentTimeInSec = UserPreference.ExperimentTime
        FixExperimentTimeMeasure()

        HiddenTime = UserPreference.HiddenTime
        HiddenTimeTextEdit.Text = HiddenTime
        ShownPoints = UserPreference.ShownPoints
        ShownPointsTextEdit.Text = ShownPoints
        ShowResultInProgess = UserPreference.ShowResultInProgess
        ShowResultAfterCalculating = UserPreference.ShowResultAfterCalculating
        ShowHintOnSerieClick = UserPreference.ShowHintOnSerieClick
        ShowHintPercents = UserPreference.ShowHintPercents
        BottomScaleIncrement = UserPreference.BottomScaleIncrement
        LeftScaleIncrement = UserPreference.LeftScaleIncrement


        If Not String.IsNullOrEmpty(UserPreference.ConfigurationFileName) Then
            ShowProgress("Loading configuration...")
            LoadConfuguration(UserPreference.ConfigurationFileName)
        End If

        UpdateRibbonPanels()

        CheckRibbon()

        HideProgress()
    End Sub

#End Region

#Region "Load dlls"

    Dim modelFolders() As String = {"\", "\Archive"}

    ''' <summary>
    ''' Load all models from dlls in ModelsFolder
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadModels()
        Dim FolderPath As String = My.Settings.ModelsFolder

        If String.IsNullOrEmpty(FolderPath) Then
            FolderPath = My.Application.Info.DirectoryPath
        End If
        If Not Functions.Folder.FolderExists(FolderPath) Then
            ShowErrorMessage("Unable to load models - folder does not exist")
            Return
        End If

        ' Update list of model folders with current application path
        For i As Integer = 0 To modelFolders.Length - 1
            modelFolders(i) = FolderPath + modelFolders(i)
        Next i

        If Not Functions.Folder.CheckExistsOrCreateFolder(TemporaryFolderForModels) Then
            ShowErrorMessage("Unable to load models - tempopary folder does not exist")
            Return
        End If
        Functions.Folder.CleanFolder(TemporaryFolderForModels)

        ' add handler to resolve assemblies in Archive if some model references them
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ARHandler

        For Each folder As String In modelFolders
            CopyModelsToTempFolder(folder)
        Next
    End Sub

    Function ARHandler(ByVal sender As Object, ByVal args As ResolveEventArgs) As [Assembly]
        Dim assemblyName As String = args.Name.Split(",")(0) + ".dll"
        Dim assemblyPath As String = TemporaryFolderForModels + "\" + assemblyName
        For Each folder As String In modelFolders
            If (Functions.File.FileExists(folder + "\" + assemblyName)) Then
                If Not Functions.File.CopyFile(folder + "\" + assemblyName, assemblyPath, "") Then Return Nothing

                Dim refAssembly As Assembly = Assembly.LoadFile(assemblyPath)
                Return refAssembly
            End If
        Next

        Return Nothing
    End Function

    Public Function CopyModelsToTempFolder(ByVal folder As String)

        Dim FolderInfo As New System.IO.DirectoryInfo(folder)
        Dim Files As System.IO.FileInfo() = FolderInfo.GetFiles
        If Files IsNot Nothing AndAlso Files.Length > 0 Then
            For Each File As System.IO.FileInfo In Files
                If Not File.Extension = ".dll" Then Continue For
                If File.Name = "TeeChart.dll" Then Continue For
                If File.Name.Contains("DevExpress") Then Continue For
                If File.Name = "ModelBase.dll" OrElse File.Name = "LissovBase.dll" Then Continue For

                'load assembly with IModel
                Dim Model As ModelBase.IModel = GetModel(File.FullName)
                If Model IsNot Nothing Then AllModels.Add(Model)
            Next
        End If
    End Function

    ''' <summary>
    ''' Tries to load assembly with IModel from given file
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks>Returns Nothing if load fails</remarks>
    Public Function GetModel(ByVal filePath As String) As ModelBase.IModel
        Dim Model As ModelBase.IModel = Nothing
        Dim ModelObject As Object = Nothing

        Dim TempFileName As String = IO.Path.Combine(TemporaryFolderForModels, IO.Path.GetFileName(filePath))
        If Not Functions.File.FileExists(TempFileName) Then
            If Not Functions.File.CopyFile(filePath, TempFileName, "") Then Return Nothing
        End If

        Try
            Dim ModelAssembly As Assembly = Assembly.LoadFile(TempFileName)
            For Each AssemblyType As Type In ModelAssembly.GetTypes
                Try
                    If AssemblyType.GetInterface("IModel") Is Nothing Then Continue For
                    ModelObject = ModelAssembly.CreateInstance(AssemblyType.FullName, True)
                Catch ex As Exception
                    ShowExeptionMessage(ex, "Unable to create model " & AssemblyType.FullName & " from assembly " & filePath)
                End Try
                Model = TryCast(ModelObject, ModelBase.IModel)
                If Model IsNot Nothing Then
                    'copy related files
                    Dim LinkedFiles As List(Of String)
                    LinkedFiles = Functions.Folder.GetLinkedFiles(IO.Path.GetDirectoryName(filePath), filePath)
                    If LinkedFiles IsNot Nothing AndAlso LinkedFiles.Count > 0 Then
                        For Each LinkedFile As String In LinkedFiles
                            If IO.Path.GetExtension(LinkedFile) = ".mdl" Then Continue For
                            Functions.File.CopyFile(LinkedFile, IO.Path.Combine(TemporaryFolderForModels, IO.Path.GetFileName(LinkedFile)), "")
                        Next
                    End If
                    Exit For
                End If
            Next
        Catch
        Finally
            ' Btw this normally returns False, as model is loaded and file cannot be deleted :)
            Functions.File.DeleteFile(TempFileName, "")
        End Try

        Return Model
    End Function


#End Region

#Region "Experiment"

    Private Sub FillExperimentTimeMeasureComboBox()
        ExperimentTimeMeasureComboBox.Properties.Items.Clear()

        Dim Measure As TimeHelper.MeasureUnit

        For Measure = TimeHelper.MeasureUnit.Second To TimeHelper.MeasureUnit.Day Step 1
            Dim StringValue As String
            Select Case Measure
                Case TimeHelper.MeasureUnit.Second
                    StringValue = "seconds"
                Case TimeHelper.MeasureUnit.Minute
                    StringValue = "minutes"
                Case TimeHelper.MeasureUnit.Hour
                    StringValue = "hours"
                Case TimeHelper.MeasureUnit.Day
                    StringValue = "days"
            End Select
            If Not String.IsNullOrEmpty(StringValue) Then
                ExperimentTimeMeasureComboBox.Properties.Items.Add(StringValue)
            End If
        Next

        ExperimentTimeMeasureComboBox.SelectedIndex = 0
    End Sub

    Private Sub RunButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles RunButtonItem.ItemClick
        RunExperiment()
    End Sub

    Public Sub RunExperiment()
        If CurrentConfig Is Nothing OrElse CurrentConfig.Models Is Nothing OrElse CurrentConfig.Models.Count = 0 Then
            MsgBox("Unable to calculate - no models exists", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return
        End If

        If Not ExperimentTimeInSec > 0 Then
            MsgBox("Unable to calculate - experiment time should be greater then 0", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return
        End If

        If Not ShownPoints > 0 OrElse Not ShownPoints <= 100 Then
            MsgBox("Unable to show result - shown points value should be greater then 0 and less then 100", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return
        End If

        StartCalculating()
    End Sub

    Private Sub PauseButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles PauseButtonItem.ItemClick
        PauseCalculating()
    End Sub

    Private Sub StopButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles StopButtonItem.ItemClick
        StopCalculating()
    End Sub

    Private Sub ExperimentTimeTextEdit_Properties_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExperimentTimeTextEdit.Properties.EditValueChanged
        Dim NewExperimentTime As Double
        If Not Double.TryParse(ExperimentTimeTextEdit.Text, NewExperimentTime) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            ExperimentTimeTextEdit.Text = ExperimentTimeInSec
            Return
        End If

        ExperimentTime = NewExperimentTime

        ExperimentTimeInSec = ExperimentTime * TimeHelper.GetDivider(ExperimentTimeMeasure)

        If FormLoaded AndAlso CurrentConfig IsNot Nothing Then
            CurrentConfig.ExperimentTime = ExperimentTimeInSec
        End If
    End Sub

    Private Sub HiddenTimeTextEdit_Properties_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HiddenTimeTextEdit.Properties.EditValueChanged, ShownPointsTextEdit.Properties.EditValueChanged
        If Not FormLoaded Then Return

        Dim NewHiddenTime As Double
        If Not Double.TryParse(HiddenTimeTextEdit.Text, NewHiddenTime) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            HiddenTimeTextEdit.Text = HiddenTime
            Return
        End If

        HiddenTime = NewHiddenTime
    End Sub

    Private Sub ShownPointsTextEdit_Properties_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShownPointsTextEdit.Properties.EditValueChanged, ShownPointsTextEdit.Properties.EditValueChanged
        If Not FormLoaded Then Return

        Dim NewShownPoints As Double
        If Not Double.TryParse(ShownPointsTextEdit.Text, NewShownPoints) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            ShownPointsTextEdit.Text = HiddenTime
            Return
        End If

        ShownPoints = NewShownPoints
    End Sub

    Private Sub ExperimentTimeMeasureComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExperimentTimeMeasureComboBox.SelectedIndexChanged
        ExperimentTimeMeasure = CType(ExperimentTimeMeasureComboBox.SelectedIndex, TimeHelper.MeasureUnit)

        UpdateExperimentTime()
    End Sub

    Private Sub UpdateExperimentTime()
        ExperimentTime = ExperimentTimeInSec / TimeHelper.GetDivider(ExperimentTimeMeasure)
        ExperimentTimeTextEdit.Text = ExperimentTime
    End Sub

    Private Sub FixExperimentTimeMeasure()
        Dim TimeDivider As TimeHelper.TimeDivider
        TimeDivider = TimeHelper.GetOptimalUnit(ExperimentTimeInSec)

        ExperimentTimeMeasureComboBox.SelectedIndex = TimeDivider.Measure
    End Sub


#End Region

#Region "Calculate"

    Private Sub StartCalculating()
        'clear ui
        If CalculatingState <> CalculatingStates.Paused Then
            ClearChart()
            ProgressItem.EditValue = 0
            RealTimeTime.Caption = "0 sec"
            CurrentTime = 0
            LastPaintedTime = 0
            GC.Collect()
        End If

        'start timers
        RealTimeTimer.Start()
        CalculatingTimer.Start()

        AddHandler CurrentConfig.CalculationStopped, AddressOf StopCalculating

        'clear results in datagrid
        ResultGridView1.PrepareGrid()

        'update ui
        CalculatingState = CalculatingStates.InProcess
        CheckRibbon()

        'start calculating
        CurrentConfig.StartCalculating(ExperimentTimeInSec)
    End Sub

    Private Sub StopCalculating()
        'stop calculatuing
        CurrentConfig.StopCalculating()

        'stop timers
        RealTimeTimer.Stop(True)
        CalculatingTimer.Stop()

        'upade results in datagrid
        ResultGridView1.UpdateGridWithCalculatedData()

        'update ui
        CalculatingState = CalculatingStates.Finished
        CheckRibbon()
    End Sub

    Private Sub PauseCalculating()
        'pause calculatuing
        CurrentConfig.PauseCalculating()

        'stop timers
        RealTimeTimer.Pause()
        CalculatingTimer.Stop()

        'upade results in datagrid
        ResultGridView1.UpdateGridWithCalculatedData()

        'update ui
        CalculatingState = CalculatingStates.Paused
        CheckRibbon()
    End Sub

    Private Sub CalculatingTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalculatingTimer.Tick
        CurrentTime = CurrentConfig.GetCurrentTime
        If CurrentTime > ExperimentTimeInSec Then CurrentTime = ExperimentTimeInSec

        Dim ProgressPercent As Integer
        ProgressPercent = Math.Ceiling((CurrentTime / ExperimentTimeInSec) * 100)
        If ProgressPercent > 100 Then ProgressPercent = 100
        ProgressItem.EditValue = ProgressPercent
        ProgressPercentText.Caption = ProgressPercent.ToString & "%"

        RealTimeTimer.Stop()
        RealTimeTime.Caption = Str(RealTimeTimer.Seconds) & " sec"
        If ShowResultInProgess Then
            ShowResult()
            LastPaintedTime = CurrentTime
        End If

        'upade results in datagrid
        ResultGridView1.UpdateGridWithCalculatedData()

        'check whether calculating is finished
        If CurrentTime >= ExperimentTimeInSec OrElse _
           CurrentConfig.GetCalculatingState = CalculatingStates.Finished _
           Then
            StopCalculating()
        End If

    End Sub

#End Region

#Region "Results"

#Region "Save results"

    Private Sub SavePictureResultButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SavePictureResultButtonItem.ItemClick
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ".jpg"
        SaveFileDialog.InitialDirectory = My.Settings.DataFolder
        SaveFileDialog.Filter = "images (*.jpg)|*.jpg"
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            ShowProgress("Saving picture...")
            Try
                Dim Color As Color = ResultChart.BackColor
                ResultChart.BackColor = Color.White
                Dim GrayScale As Boolean = ResultChart.Export.Image.JPEG.GrayScale
                If UserPreference.ResultImage_SaveColoured Then
                    ResultChart.Export.Image.JPEG.GrayScale = False
                    ResultChart.Export.Image.JPEG.Save(SaveFileDialog.FileName)
                Else
                    ResultChart.Export.Image.JPEG.GrayScale = True
                    ResultChart.Export.Image.JPEG.Save(SaveFileDialog.FileName)
                End If
                ResultChart.Export.Image.JPEG.GrayScale = GrayScale
                ResultChart.BackColor = Color
                ShowInformationMessage("Done")
            Catch ex As Exception
                ShowExeptionMessage(ex, "Saving picture failed")
            Finally
                HideProgress()
            End Try
        End If
    End Sub

    Private Sub SaveResultButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveResultButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.RESULT_EXTENSION
        SaveFileDialog.InitialDirectory = My.Settings.DataFolder
        SaveFileDialog.Filter = String.Format("results (*{0})|*{0}", ModelBase.Configuration.RESULT_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileDialog.FileName

            SaveFileDialog.Dispose()
            GC.Collect()
            Me.Refresh()

            ShowProgress("Saving result...")
            Try
                FixValuesBeforeSave(UserPreference.Result_SaveOnlyVisiblePoints)
                CurrentConfig.SaveResult(FileName)
                ShowInformationMessage("Result was successfully saved")
            Catch ex As Exception
                ShowExeptionMessage(ex, "Saving result failed")
            Finally
                HideProgress()
            End Try

        End If

    End Sub

    Private Sub ReportButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ReportButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.RESULT_EXTENSION
        SaveFileDialog.InitialDirectory = My.Settings.DataFolder
        SaveFileDialog.Filter = String.Format("reports (*{0})|*{0}", ModelBase.Configuration.REPORT_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileDialog.FileName

            SaveFileDialog.Dispose()
            GC.Collect()
            Me.Refresh()

            ShowProgress("Generating report...")
            Try
                FixValuesBeforeSave(UserPreference.Report_SaveOnlyVisiblePoints)
                OfficeIntegration.Report.GenerateReport(FileName, CurrentConfig)
                ShowInformationMessage("Report was successfully saved")
            Catch ex As Exception
                ShowExeptionMessage(ex, "Generating report failed")
            Finally
                HideProgress()
            End Try

        End If

    End Sub

    Private Sub FixValuesBeforeSave(ByVal calculateShownStep As Boolean)
        If CurrentConfig IsNot Nothing Then
            For Each Model As ModelBase.IModel In CurrentConfig.Models
                If calculateShownStep Then
                    Model.ShownStep = GetGeneralStep(Model)
                Else
                    Model.ShownStep = Model.Step
                End If
            Next
        End If
    End Sub

#End Region

    Private Sub OpenResultButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles OpenResultButtonItem.ItemClick
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.InitialDirectory = My.Settings.DataFolder
        OpenFileDialog.Filter = String.Format("results (*{0})|*{0}", ModelBase.Configuration.RESULT_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            LoadSavedConfuguration(OpenFileDialog.FileName)
        End If
    End Sub

    Public Sub LoadSavedConfuguration(ByVal fileName As String)
        If IsSavedConfigurationAlreadyOpened(fileName) Then
            ShowErrorMessage(String.Format("Results from file '{0}' are already loaded", fileName))
            Return
        End If

        Dim SavedConfuguration As ModelBase.SavedConfiguration = Nothing
        ShowProgress(String.Format("Opening results from file '{0}'...", fileName))
        Try
            Dim XmlDocument As New System.Xml.XmlDocument
            XmlDocument.Load(fileName)

            SavedConfuguration = ModelBase.SavedConfiguration.FromXml(XmlDocument, fileName)

        Catch ex As Exception
            ShowExeptionMessage(ex, String.Format("Unable to load results from file '{0}'", fileName))
            Return
        Finally
            HideProgress()
        End Try

        If SavedConfuguration IsNot Nothing Then
            HistoryConfigurations.Add(SavedConfuguration)
            ModelsTree.AddHistoryConfiguration(SavedConfuguration)
        End If
    End Sub

    ''' <summary>
    ''' Returns true if this file is already loaded
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSavedConfigurationAlreadyOpened(ByVal fileName As String) As Boolean
        Return HistoryConfigurations.Where(Function(c) c.FileName = fileName).Count > 0
    End Function

#End Region

#Region "Models tree"

    Private Sub ModelsTree_ChartRefreshNeeded() Handles ModelsTree.ChartRefreshNeeded
        'refresh chart 
        If RefreshResultButtonItem.Enabled AndAlso ShownPoints > 0 AndAlso ShownPoints <= 100 Then
            RefreshChart()
        End If
    End Sub

    Private Sub ModelsTree_ModelSelected(ByVal model As ModelBase.IModel) Handles ModelsTree.ModelSelected
        FillPropertiesGrid(model)
        FillInitValuesGrid(model)
        UpdateControls(model)
        FillModelManagementGrid(model)
        UpdateRibbon(model)
        UpdateSettingsPanel(model)
    End Sub

    Private Sub ModelsTree_SavedConfigurationRemoved(ByVal savedConfiguration As ModelBase.SavedConfiguration) Handles ModelsTree.SavedConfigurationRemoved
        If savedConfiguration Is Nothing Then Return

        If HistoryConfigurations.Contains(savedConfiguration) Then
            HistoryConfigurations.Remove(savedConfiguration)
        End If
    End Sub

    Private Sub ModelsTree_SelectedNodeChanged() Handles ModelsTree.SelectedNodeChanged
        If ModelsTree.SelectedNode Is Nothing OrElse ModelsTree.SelectedNode.Tag Is Nothing OrElse Not TypeOf ModelsTree.SelectedNode.Tag Is ModelBase.IModel Then
            ModelManagementTabPage.PageVisible = False
        End If
    End Sub

#End Region

#Region "Update UI"

    Public Sub ClearUI()
        ClearChart()
        ModelsTree.SetConfiguration(CurrentConfig)
        ParameterGrid.Rows.Clear()
        InitValuesGrid.Rows.Clear()
        ClearNotSystemTabs(True)
        ClearNotSystemRibbonPages()
        UpdateSettingsPanel(Nothing)
    End Sub

    Public Sub CheckRibbon()

        If CurrentConfig IsNot Nothing Then
            Select Case CalculatingState
                Case CalculatingStates.NotStarted, CalculatingStates.Finished
                    RunButtonItem.Enabled = True
                    StopButtonItem.Enabled = False
                    PauseButtonItem.Enabled = False
                Case CalculatingStates.Paused
                    RunButtonItem.Enabled = True
                    StopButtonItem.Enabled = True
                    PauseButtonItem.Enabled = False
                Case CalculatingStates.InProcess
                    RunButtonItem.Enabled = False
                    StopButtonItem.Enabled = True
                    PauseButtonItem.Enabled = True
            End Select
        Else
            RunButtonItem.Enabled = False
            StopButtonItem.Enabled = False
            PauseButtonItem.Enabled = False
        End If

        RefreshResultButtonItem.Enabled = CalculatingState <> CalculatingStates.NotStarted
        SaveResultButtonItem.Enabled = CalculatingState <> CalculatingStates.NotStarted
        SavePictureResultButtonItem.Enabled = CalculatingState <> CalculatingStates.NotStarted
        ReportButtonItem.Enabled = CalculatingState <> CalculatingStates.NotStarted

        ConfigEditButtonItem.Enabled = CurrentConfig IsNot Nothing
        ConfigSaveButtonItem.Enabled = CurrentConfig IsNot Nothing AndAlso Not String.IsNullOrEmpty(CurrentConfig.FileName)
        ConfigurationAboutButtonItem.Enabled = CurrentConfig IsNot Nothing AndAlso Not String.IsNullOrEmpty(CurrentConfig.Description)
    End Sub

#End Region

#Region "Central tabs"

    Private Sub UpdateAlwaysShownControls()
        ClearNotSystemTabs(True)

        If CurrentConfig IsNot Nothing AndAlso CurrentConfig.Models IsNot Nothing Then
            For Each Model As ModelBase.IModel In CurrentConfig.Models
                If Model.IsControlAlwaysShown Then
                    UpdateControls(Model)
                End If
            Next
        End If

        'select first not system control
        For i As Integer = 0 To CentralTabControl.TabPages.Count - 1
            If CentralTabControl.TabPages(i).Tag IsNot Nothing Then
                CentralTabControl.SelectedTabPage = CentralTabControl.TabPages(i)
                Return
            End If
        Next

    End Sub

    Private Sub UpdateControls(ByVal model As ModelBase.IModel)
        ClearNotSystemTabs(False)

        'check whether Model management tab should be shown
        Dim SwitchParameters As List(Of ModelBase.SwitchParameter) = model.GetSwitchParameters()
        If SwitchParameters IsNot Nothing AndAlso SwitchParameters.Count > 0 Then
            ModelManagementTabPage.PageVisible = True
        Else
            ModelManagementTabPage.PageVisible = False
        End If

        Dim ModelControl As System.Windows.Forms.UserControl
        ModelControl = model.GetControl
        If ModelControl Is Nothing Then Return

        'check whether control was already added
        For Each Page As DevExpress.XtraTab.XtraTabPage In CentralTabControl.TabPages
            If Page.Tag IsNot Nothing AndAlso CStr(Page.Tag) = model.GetName Then
                Return
            End If
        Next

        'control was not added - add it
        Dim ModelTabPage As DevExpress.XtraTab.XtraTabPage
        ModelTabPage = CentralTabControl.TabPages.Add(model.DisplayName)
        ModelTabPage.Controls.Add(ModelControl)
        ModelControl.Dock = DockStyle.Fill
        ModelControl.Visible = True

        If model.IsControlAlwaysShown Then
            ModelTabPage.Tag = model.GetName
        End If

    End Sub

    Private Sub ClearNotSystemTabs(ByVal removeAlwaysShownControls As Boolean)
        For i As Integer = CentralTabControl.TabPages.Count - 1 To 0 Step -1
            If Not IsSystemTabPage(CentralTabControl.TabPages(i)) Then
                If removeAlwaysShownControls OrElse CentralTabControl.TabPages(i).Tag Is Nothing Then
                    CentralTabControl.TabPages.RemoveAt(i)
                End If
            End If
        Next
    End Sub

    Private Function IsSystemTabPage(ByVal tabPage As DevExpress.XtraTab.XtraTabPage) As Boolean
        Dim Result As Boolean = False

        If tabPage.Equals(MainTabPage) OrElse tabPage.Equals(GridResultsTabPage) OrElse tabPage.Equals(ModelManagementTabPage) Then
            Result = True
        End If

        Return Result
    End Function

#End Region

#Region "User's ribbon"

    Private Sub UpdateRibbon(ByVal model As ModelBase.IModel)
        ClearNotSystemRibbonPages()

        Dim ModelMenuItems As List(Of ModelBase.MenuItem)
        ModelMenuItems = model.GetMenuItems
        If ModelMenuItems Is Nothing OrElse ModelMenuItems.Count = 0 Then Return

        Dim ModelTabPage As New DevExpress.XtraBars.Ribbon.RibbonPage(model.DisplayName)
        RibbonControl.Pages.Add(ModelTabPage)

        For Each MenuItem As ModelBase.MenuItem In ModelMenuItems
            Dim PageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
            PageGroup = GetRibbonPageGroupByText(ModelTabPage, MenuItem.Category)

            Dim ButtonItem As New DevExpress.XtraBars.BarButtonItem
            ButtonItem.Caption = MenuItem.DisplayName
            ButtonItem.Glyph = MenuItem.Image
            ButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
            ButtonItem.Enabled = MenuItem.Enabled
            ButtonItem.Tag = MenuItem
            AddHandler ButtonItem.ItemClick, AddressOf ModelButtonItem_Click

            PageGroup.ItemLinks.Add(ButtonItem)
        Next

    End Sub

    Private Sub ClearNotSystemRibbonPages()
        For i As Integer = RibbonControl.Pages.Count - 1 To 0 Step -1
            If Not IsSystemRibbonPage(RibbonControl.Pages(i)) Then
                RibbonControl.Pages.RemoveAt(i)
            End If
        Next
    End Sub

    Private Function IsSystemRibbonPage(ByVal ribbonPage As DevExpress.XtraBars.Ribbon.RibbonPage) As Boolean
        Dim Result As Boolean = False

        If ribbonPage.Equals(MainRibbonPage) Then
            Result = True
        End If

        Return Result
    End Function

    ''' <summary>
    ''' Ges group by its text; if not exist - creates
    ''' </summary>
    ''' <param name="ribbonPage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRibbonPageGroupByText(ByVal ribbonPage As DevExpress.XtraBars.Ribbon.RibbonPage, ByVal text As String) As DevExpress.XtraBars.Ribbon.RibbonPageGroup
        Dim PageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup = Nothing

        PageGroup = ribbonPage.Groups.GetGroupByText(text)

        If PageGroup Is Nothing Then
            PageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup(text)
            ribbonPage.Groups.Add(PageGroup)
        End If

        Return PageGroup
    End Function

    Private Sub ModelButtonItem_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim MenuItem As ModelBase.MenuItem
        MenuItem = TryCast(e.Item.Tag, ModelBase.MenuItem)

        If MenuItem IsNot Nothing Then MenuItem.Click()
    End Sub

#End Region

#Region "Settings panel"

    Private Sub UpdateSettingsPanel(ByVal model As ModelBase.IModel)
        StepTextEdit.Tag = Nothing

        If model IsNot Nothing Then
            StepTextEdit.Text = model.Step
            StepTextEdit.Tag = model

            'lock control for opened saved result
            StepTextEdit.Properties.ReadOnly = model.IsOpenedFromResultFile
        Else
            StepTextEdit.Text = ""
        End If

    End Sub

    Private Sub StepTextEdit_Properties_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StepTextEdit.Properties.EditValueChanged
        If TypeOf StepTextEdit.Tag Is ModelBase.IModel Then
            Dim Model As ModelBase.IModel
            Model = CType(StepTextEdit.Tag, ModelBase.IModel)

            Dim NewStep As Double
            If Not Double.TryParse(StepTextEdit.Text, NewStep) Then
                MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
                StepTextEdit.Text = Model.Step
                Return
            End If

            Model.Step = NewStep
        End If
    End Sub

#End Region

#Region "Properties grid"

    Private Sub FillPropertiesGrid(ByVal model As ModelBase.IModel)
        ParameterGrid.Rows.Clear()

        If model IsNot Nothing Then
            Dim Row As DataGridViewRow
            Dim RowIndex As Integer
            For Each Parameter As ModelBase.Parameter In model.GetParameters
                RowIndex = ParameterGrid.Rows.Add()
                Row = ParameterGrid.Rows(RowIndex)
                Row.Cells(ParameterNameColumn.Index).Value = Parameter.DisplayName
                Row.Cells(ParameterValueColumn.Index).Value = Parameter.Value
                Row.Tag = Parameter

                AddHandler Parameter.ValueChanged, AddressOf ParameterValueChanged
            Next

            'lock grid for opened saved result
            ParameterGrid.ReadOnly = model.IsOpenedFromResultFile
        End If

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
        NewValue = Row.Cells(InitValueColumn.Index).Value
        Dim NewInitValue As Double
        If Not Double.TryParse(NewValue, NewInitValue) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Parameter.Value
            Return
        End If

        If NewInitValue < Parameter.MinValue Then
            MsgBox("Input value was less then min value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Parameter.Value
        ElseIf NewInitValue > Parameter.MaxValue Then
            MsgBox("Input value was bigger then max value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Parameter.Value
        Else
            Parameter.Value = NewValue
        End If
    End Sub

    Private Sub ParameterValueChanged(ByVal parameter As ModelBase.Parameter)
        Dim Row As DataGridViewRow = GetParameterRow(parameter)
        If Row Is Nothing Then Return

        Row.Cells(ParameterValueColumn.Index).Value = parameter.Value
    End Sub

    Private Function GetParameterRow(ByVal parameter As ModelBase.Parameter) As DataGridViewRow
        Dim Result As DataGridViewRow = Nothing

        If ParameterGrid.Rows IsNot Nothing AndAlso ParameterGrid.Rows.Count > 0 Then
            For Each Row As DataGridViewRow In ParameterGrid.Rows
                If Row.Tag.Equals(parameter) Then
                    Result = Row
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

#End Region

#Region "Model management grid"

    Private Sub FillModelManagementGrid(ByVal model As ModelBase.IModel)
        ModelManagementGrid.Rows.Clear()

        If model IsNot Nothing Then
            Dim Row As DataGridViewRow
            Dim RowIndex As Integer

            Dim SwitchParameters As List(Of ModelBase.SwitchParameter) = model.GetSwitchParameters()
            If SwitchParameters IsNot Nothing AndAlso SwitchParameters.Count > 0 Then
                For Each SwitchParameter As ModelBase.SwitchParameter In SwitchParameters
                    RowIndex = ModelManagementGrid.Rows.Add()
                    Row = ModelManagementGrid.Rows(RowIndex)
                    Row.Cells(ModelManagementNameColumn.Index).Value = SwitchParameter.DisplayName
                    Row.Cells(ModelManagementValueColumn.Index).Value = SwitchParameter.Value
                    Row.Tag = SwitchParameter

                    AddHandler SwitchParameter.ValueChanged, AddressOf ModelManagementValueChanged
                Next
            End If

            'lock grid for opened saved result
            ModelManagementGrid.ReadOnly = model.IsOpenedFromResultFile
        End If

    End Sub

    Private Sub ModelManagement_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ModelManagementGrid.CellValueChanged
        Dim RowIndex As Integer = e.RowIndex
        If RowIndex < 0 OrElse RowIndex >= ModelManagementGrid.Rows.Count Then
            Return
        End If

        Dim Row As DataGridViewRow = ModelManagementGrid.Rows(RowIndex)

        Dim SwitchParameter As ModelBase.SwitchParameter
        SwitchParameter = TryCast(Row.Tag, ModelBase.SwitchParameter)
        If SwitchParameter Is Nothing Then Return

        Dim NewValue As Object = Row.Cells(InitValueColumn.Index).Value
        If TypeOf NewValue Is Boolean Then
            SwitchParameter.Value = NewValue
        End If

    End Sub

    Private Sub ModelManagementValueChanged(ByVal switchParameter As ModelBase.SwitchParameter)
        Dim Row As DataGridViewRow = GetSwitchParameterRow(switchParameter)
        If Row Is Nothing Then Return

        Row.Cells(ParameterValueColumn.Index).Value = switchParameter.Value
    End Sub

    Private Function GetSwitchParameterRow(ByVal switchParameter As ModelBase.SwitchParameter) As DataGridViewRow
        Dim Result As DataGridViewRow = Nothing

        If ParameterGrid.Rows IsNot Nothing AndAlso ModelManagementGrid.Rows.Count > 0 Then
            For Each Row As DataGridViewRow In ModelManagementGrid.Rows
                If Row.Tag.Equals(switchParameter) Then
                    Result = Row
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

#End Region

#Region "Init values grid"

    Private Sub FillInitValuesGrid(ByVal model As ModelBase.IModel)
        InitValuesGrid.Rows.Clear()

        If model IsNot Nothing Then
            Dim Row As DataGridViewRow
            Dim RowIndex As Integer
            For Each Value As ModelBase.Value In model.GetValues
                If Not ShowInputValues AndAlso Value.Type = ModelBase.Value.ValueType.Input Then Continue For
                If Not ShowInternalValues AndAlso Value.Type = ModelBase.Value.ValueType.Internal Then Continue For
                If Not Value.InitValueVisible Then Continue For

                RowIndex = InitValuesGrid.Rows.Add()
                Row = InitValuesGrid.Rows(RowIndex)
                Row.Cells(ValueNameColumn.Index).Value = Value.DisplayName
                Row.Cells(InitValueColumn.Index).Value = Value.InitValue
                Row.Tag = Value

                AddHandler Value.InitValueChanged, AddressOf InitValueChanged
            Next

            'lock grid for opened saved result
            InitValuesGrid.ReadOnly = model.IsOpenedFromResultFile
        End If

    End Sub

    Private Sub InitValuesGrid_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles InitValuesGrid.CellValueChanged
        Dim RowIndex As Integer = e.RowIndex
        If RowIndex < 0 OrElse RowIndex >= InitValuesGrid.Rows.Count Then
            Return
        End If

        Dim Row As DataGridViewRow
        Row = InitValuesGrid.Rows(RowIndex)

        Dim Value As ModelBase.Value
        Value = TryCast(Row.Tag, ModelBase.Value)
        If Value Is Nothing Then Return

        Dim NewValue As String
        NewValue = Row.Cells(InitValueColumn.Index).Value
        Dim NewInitValue As Double
        If Not Double.TryParse(NewValue, NewInitValue) Then
            MsgBox("Input value was not in correct format", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Value.InitValue
            Return
        End If

        If NewInitValue < Value.MinValue Then
            MsgBox("Input value was less then min value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Value.InitValue
        ElseIf NewInitValue > Value.MaxValue Then
            MsgBox("Input value was bigger then max value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Row.Cells(InitValueColumn.Index).Value = Value.InitValue
        Else
            Value.InitValue = NewValue
        End If

    End Sub

    Private Sub InitValueChanged(ByVal value As ModelBase.Value)
        Dim Row As DataGridViewRow = GetInitValueRow(value)
        If Row Is Nothing Then Return

        Row.Cells(InitValueColumn.Index).Value = value.InitValue
    End Sub

    Private Function GetInitValueRow(ByVal value As ModelBase.Value) As DataGridViewRow
        Dim Result As DataGridViewRow = Nothing

        If InitValuesGrid.Rows IsNot Nothing AndAlso InitValuesGrid.Rows.Count > 0 Then
            For Each Row As DataGridViewRow In InitValuesGrid.Rows
                If Row.Tag.Equals(value) Then
                    Result = Row
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

#End Region

#Region "Configuration"

    Private Sub ConfigCreateButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigCreateButtonItem.ItemClick
        Dim ConfigEditForm As New ConfigEditForm(AllModels)
        ConfigEditForm.ShowDialog()
    End Sub

    Private Sub ConfigEditButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigEditButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        'clone configuration before editing to avoid id being corrupted
        Dim ConfigToEdit As Object = TryCast(CurrentConfig.Clone, ModelBase.Configuration)
        If ConfigToEdit Is Nothing Then Return

        Dim ConfigEditForm As New ConfigEditForm(ConfigToEdit, AllModels)
        ConfigEditForm.ShowDialog()
    End Sub

    Private Sub ConfigOpenButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigOpenButtonItem.ItemClick
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.InitialDirectory = My.Settings.DataFolder
        OpenFileDialog.Filter = String.Format("configurations (*{0})|*{0}", ModelBase.Configuration.CONFIG_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            LoadConfuguration(OpenFileDialog.FileName)
        End If

    End Sub

    Public Sub LoadConfuguration(ByVal fileName As String)
        Dim Config As ModelBase.Configuration = Nothing
        Try
            Dim XmlDocument As New System.Xml.XmlDocument
            XmlDocument.Load(fileName)

            Config = ModelBase.Configuration.FromXml(XmlDocument, AllModels, fileName)

        Catch ex As Exception
            ShowExeptionMessage(ex, String.Format("Unable to load configuration from file '{0}'", fileName))
            Return
        End Try

        If Config IsNot Nothing Then
            CurrentConfig = Config
        End If
    End Sub

    Private Sub ConfigSaveButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigSaveButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        'update properties before saving configuration
        CurrentConfig.ResultGridViewData = ResultGridView1.ToXmlString

        If CurrentConfig.Save Then
            MsgBox("Configuration was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
        End If
    End Sub


    Private Sub ConfigSaveAsButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigSaveAsButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        'update properties before saving configuration
        CurrentConfig.ResultGridViewData = ResultGridView1.ToXmlString

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        SaveFileDialog.InitialDirectory = My.Settings.DataFolder
        SaveFileDialog.Filter = String.Format("configurations (*{0})|*{0}", ModelBase.Configuration.CONFIG_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            CurrentConfig.FileName = SaveFileDialog.FileName

            If CurrentConfig.Save Then
                CurrentConfig = CurrentConfig 'looks strange but must be present to update UI
                MsgBox("Configuration was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If
        End If

    End Sub

#End Region

#Region "Show results"

    Private Sub RefreshResultButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles RefreshResultButtonItem.ItemClick
        If Not ShownPoints > 0 OrElse Not ShownPoints <= 100 Then
            MsgBox("Unable to show result - shown points value should be greater then 0 and less then 100", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return
        End If

        RefreshChart()
    End Sub

    Private Sub RefreshChart()
        MarkItems = Nothing

        ClearChart()

        LastPaintedTime = 0

        ShowResult()

    End Sub

    Public Sub ClearChart()
        ClearHints()

        For i As Integer = ResultChart.Series.Count - 1 To 0 Step -1
            ResultChart.Series(i).Dispose()
        Next

        ResultChart.Series.Clear()
        ResultChart.Header.Visible = False
        ResultChart.Footer.Visible = False
        ResultChart.Panel.Shadow.Visible = False
        ResultChart.Aspect.View3D = False
        ResultChart.Axes.Bottom.Title.Text = ""
    End Sub

    ''' <summary>
    ''' Returns step whith which given model's result should be shown
    ''' </summary>
    Private Function GetGeneralStep(ByVal model As ModelBase.IModel) As Decimal
        Dim GeneralStep As Decimal = model.Step

        'check max shown points in %
        Dim MaxShownPoints As Double = ShownPoints
        If UserPreference.MaxPointsPerSecond > 0 Then
            Dim ModelPointsPerSec As Double = 1 / model.Step
            If ModelPointsPerSec > UserPreference.MaxPointsPerSecond Then
                Dim ShownPoints As Double = (UserPreference.MaxPointsPerSecond / ModelPointsPerSec) * 100
                If ShownPoints < MaxShownPoints Then
                    MaxShownPoints = ShownPoints
                End If
            End If
        End If

        If MaxShownPoints < 100 Then
            GeneralStep = (GeneralStep * 100) / MaxShownPoints
        End If

        Return GeneralStep
    End Function

    ''' <summary>
    ''' Returns total amount of steps to be shown
    ''' </summary>
    ''' <param name="model"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetShownSteps(ByVal model As ModelBase.IModel) As Integer
        'total amount of calculated steps
        Dim MaxStep As Integer
        If CurrentTime < model.GetLastCalculatedTime Then
            MaxStep = Math.Floor((CurrentTime - HiddenTime - LastPaintedTime) / model.Step)
        Else
            MaxStep = Math.Floor((model.GetLastCalculatedTime - HiddenTime - LastPaintedTime) / model.Step)
        End If

        'check max shown points in %
        Dim MaxShownPoints As Double = ShownPoints
        If UserPreference.MaxPointsPerSecond > 0 Then
            Dim ModelPointsPerSec As Double = 1 / model.Step
            If ModelPointsPerSec > UserPreference.MaxPointsPerSecond Then
                Dim ShownPoints As Double = (UserPreference.MaxPointsPerSecond / ModelPointsPerSec) * 100
                If ShownPoints < MaxShownPoints Then
                    MaxShownPoints = ShownPoints
                End If
            End If
        End If

        'each ShownNumber point should be displayed
        Dim ShownNumber As Integer = Math.Floor(100 / MaxShownPoints)
        If Not ShownNumber > 0 Then Return 0
        'total amount of steps to be shown
        Dim ShownSteps As Integer = Math.Floor(MaxStep / ShownNumber)

        Return ShownSteps
    End Function


    Public Sub ShowResult()
        ColorGenerator.Instance.Reset()

        Dim TimeDivider As TimeHelper.TimeDivider
        TimeDivider = TimeHelper.GetOptimalUnit(CurrentTime)
        Select Case TimeDivider.Measure
            Case TimeHelper.MeasureUnit.Second
                ResultChart.Axes.Bottom.Title.Text = "Time, sec"
            Case TimeHelper.MeasureUnit.Minute
                ResultChart.Axes.Bottom.Title.Text = "Time, min"
            Case TimeHelper.MeasureUnit.Hour
                ResultChart.Axes.Bottom.Title.Text = "Time, hr"
            Case TimeHelper.MeasureUnit.Day
                ResultChart.Axes.Bottom.Title.Text = "Time, days"
        End Select

        If CurrentConfig IsNot Nothing Then ShowResult(CurrentConfig.Models, TimeDivider, False)

        If CalculatingState <> CalculatingStates.InProcess AndAlso HistoryConfigurations.Count > 0 Then
            For Each SavedConfiguration As ModelBase.SavedConfiguration In HistoryConfigurations
                ShowResult(SavedConfiguration.Models, TimeDivider, True)
            Next
        End If

    End Sub

    'Looks more logical but Serie.Add(Times, Values)
    'removes all previous values
    Public Sub ShowResult(ByVal models As List(Of ModelBase.IModel), _
                          ByVal timeDivider As TimeHelper.TimeDivider, _
                          ByVal isSavedResult As Boolean)

        If models Is Nothing OrElse models.Count = 0 Then Return

        'check first shown time
        If GetFirstTimeToShow() > CurrentTime Then Return

        Dim MaxYValue As Double = Double.MinValue
        Dim MinYValue As Double = Double.MaxValue

        'last added step
        Dim LastStep As Integer = 0
        'total amount of added to the chart series
        Dim ShownSeries As Integer = 0

        For Each Model As ModelBase.IModel In models
            If Not Model.HasVisibleValues Then Continue For

            Dim Times As Double() = GetTimes(Model, isSavedResult)
            If Times Is Nothing Then Continue For

            'fix times to current measure unit
            Dim FixedTimes As Double() = FixTimes(Times, timeDivider)

            For Each Value As ModelBase.Value In Model.GetValues
                'check whether this value should be shown
                If Not Value.Visible Then Continue For

                'get/create serie
                Dim SerieName As String
                If Not String.IsNullOrEmpty(Value.Measure) Then
                    SerieName = Value.DisplayName & ", " & Value.Measure
                Else
                    SerieName = Value.DisplayName
                End If
                Dim Serie As Series
                If CalculatingState = CalculatingStates.InProcess Then
                    Serie = GetSerieByName(SerieName, True)
                Else
                    Serie = CreateSerie(SerieName)
                End If

                ShownSeries += 1

                Dim Values As Double()
                ReDim Values(Times.Length - 1)
                For i As Integer = 0 To Times.Length - 1
                    If i = Times.Length - 1 Then
                        i = Times.Length - 1
                    End If
                    Values(i) = FixSerieValue(Model.GetValueByTime(Value, Times(i)))
                Next

                Serie.Visible = True
                Serie.Add(FixedTimes, Values)

                If MaxYValue < Serie.MaxYValue Then MaxYValue = Serie.MaxYValue
                If MinYValue > Serie.MinYValue Then MinYValue = Serie.MinYValue

            Next

        Next

        If ShownSeries > 0 Then
            'add serie with min and max points
            If timeDivider.Divider > 0 Then
                AddMinMaxPoint(MaxYValue, MinYValue, GetFirstTimeToShow() / timeDivider.Divider)
            Else
                AddMinMaxPoint(MaxYValue, MinYValue, GetFirstTimeToShow)
            End If
        End If

    End Sub

    Private Sub ShowMarks()
        'remove all old series with marks
        If ResultChart.Series IsNot Nothing AndAlso ResultChart.Series.Count > 0 Then
            For i As Integer = ResultChart.Series.Count - 1 To 0 Step -1
                If TypeOf ResultChart.Series(i) Is Steema.TeeChart.Styles.Points AndAlso ResultChart.Series(i).Title <> "@MinMax" Then
                    ResultChart.Series.Remove(ResultChart.Series(i))
                End If
            Next
        End If

        'get current time divider
        Dim TimeDivider As TimeHelper.TimeDivider = TimeHelper.GetOptimalUnit(CurrentTime)

        'show series with marks as black point style series
        For Each MarkItem As MarkItem In MarkItems
            If MarkItem.MarkStyle = PointerStyles.Nothing Then Continue For

            If MarkItem.Configuration Is Nothing Then Continue For
            Dim IsSavedResult As Boolean = False

            Dim Model As ModelBase.IModel
            If TypeOf MarkItem.Configuration Is ModelBase.Configuration Then
                Model = CType(MarkItem.Configuration, ModelBase.Configuration).GetModelByName(MarkItem.ModelName)
            ElseIf TypeOf MarkItem.Configuration Is ModelBase.SavedConfiguration Then
                Model = CType(MarkItem.Configuration, ModelBase.SavedConfiguration).GetModelByName(MarkItem.ModelName)
                IsSavedResult = True
            End If
            If Model Is Nothing Then Continue For
            Dim Value As ModelBase.Value = Model.GetValue(MarkItem.ValueName)
            If Value Is Nothing Then Continue For

            ShowMarks(Model, Value, MarkItem.MarkStyle, TimeDivider, IsSavedResult)
        Next
    End Sub

    Private Sub ShowMarks(ByVal model As ModelBase.IModel, _
                          ByVal value As ModelBase.Value, _
                          ByVal markStyle As Steema.TeeChart.Styles.PointerStyles, _
                          ByVal timeDivider As TimeHelper.TimeDivider, _
                          ByVal isSavedResult As Boolean)

        'only MAX points should be shown!
        Dim Times As Double() = GetTimes(model, isSavedResult, UserPreference.MaxMarksCount)
        If Times Is Nothing Then Return

        'fix times to current measure unit
        Dim FixedTimes As Double() = FixTimes(Times, timeDivider)

        Dim Serie As Series = CreateMarkSerie(markStyle)
        Dim Values As Double()
        ReDim Values(Times.Length - 1)
        For i As Integer = 0 To Times.Length - 1
            If i = Times.Length - 1 Then
                i = Times.Length - 1
            End If
            Values(i) = FixSerieValue(model.GetValueByTime(value, Times(i)))
        Next

        Serie.Visible = True
        Serie.Add(FixedTimes, Values)
    End Sub

#Region "Helping methods for show result"

    ''' <summary>
    ''' Returns first shown time
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFirstTimeToShow() As Double
        Dim FirstTimeToShow As Double
        If LastPaintedTime > HiddenTime Then
            FirstTimeToShow = LastPaintedTime
        Else
            FirstTimeToShow = HiddenTime
        End If
        Return FirstTimeToShow
    End Function

    ''' <summary>
    ''' Gets array with all times (in sec!) for which values are calculated
    ''' </summary>
    ''' <param name="model"></param>
    ''' <param name="isSavedResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTimes(ByVal model As ModelBase.IModel, _
                              ByVal isSavedResult As Boolean, _
                              Optional ByVal maxShownSteps As Integer = -1) _
                                As Double()

        Dim Times As Double()

        'get first shown time
        Dim FirstTimeToShow As Double = GetFirstTimeToShow()
        'last added step
        Dim LastStep As Integer = 0


        'total amount of calculated steps
        Dim MaxStep As Integer
        If CurrentTime < model.GetLastCalculatedTime Then
            MaxStep = Math.Floor((CurrentTime - FirstTimeToShow) / model.Step)
        Else
            MaxStep = Math.Floor((model.GetLastCalculatedTime - FirstTimeToShow) / model.Step)
        End If

        'check max shown points in %
        Dim MaxShownPoints As Double
        If Not isSavedResult Then
            MaxShownPoints = ShownPoints
        Else
            MaxShownPoints = 100
        End If
        If UserPreference.MaxPointsPerSecond > 0 Then
            Dim ModelPointsPerSec As Double = 1 / model.Step
            If ModelPointsPerSec > UserPreference.MaxPointsPerSecond Then
                Dim ShownPoints As Double = (UserPreference.MaxPointsPerSecond / ModelPointsPerSec) * 100
                If ShownPoints < MaxShownPoints Then
                    MaxShownPoints = ShownPoints
                End If
            End If
        End If

        'in some cases fixed amount of points shold be shown - check it
        If maxShownSteps > 0 Then
            If MaxShownPoints > (maxShownSteps * 100) / MaxStep Then
                MaxShownPoints = (maxShownSteps * 100) / MaxStep
            End If
        End If

        'each ShownNumber point should be displayed
        Dim ShownNumber As Integer = Math.Floor(100 / MaxShownPoints)
        If Not ShownNumber > 0 Then Return Nothing
        'total amount of steps to be shown
        Dim ShownSteps As Integer = Math.Floor(MaxStep / ShownNumber)

        'add place for last point in case we show less then 100% of points
        If MaxShownPoints < 100 Then MaxShownPoints += 1
        'check whether there is something to show
        If Not ShownSteps > 0 Then Return Nothing
        ReDim Times(ShownSteps - 1)

        'get first shown time
        Dim Time As Double = FirstTimeToShow
        Dim CurrentStep As Integer = 0
        LastStep = 0
        While Time <= CurrentTime AndAlso CurrentStep < MaxStep AndAlso LastStep < ShownSteps
            If CurrentStep Mod ShownNumber = 0 Then
                Times(LastStep) = Time
                LastStep += 1
            End If
            'update values
            Time += model.Step
            CurrentStep += 1
        End While
        CurrentStep -= 1
        Time -= model.Step
        'add last point in case we show les then 100% of points
        If MaxShownPoints < 100 Then
            Times(ShownSteps - 1) = CurrentTime
            CurrentStep += 1
        End If

        Return Times
    End Function

    ''' <summary>
    ''' Fixes times to given measure unit
    ''' </summary>
    ''' <param name="times"></param>
    ''' <param name="timeDivider"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FixTimes(ByVal times As Double(), ByVal timeDivider As TimeHelper.TimeDivider) As Double()
        Dim FixedTimes As Double()

        ReDim FixedTimes(times.Length - 1)

        If timeDivider.Divider > 0 Then
            For i As Integer = 0 To times.Length - 1
                FixedTimes(i) = times(i) / timeDivider.Divider
            Next
        Else
            For i As Integer = 0 To times.Length - 1
                FixedTimes(i) = times(i)
            Next
        End If

        Return FixedTimes
    End Function

    Private Sub AddMinMaxPoint(ByVal maxYValue As Double, ByVal minYValue As Double, ByVal firstTimeToShow As Double)
        'add series with min and max points
        Dim MinMaxSerie As Series = GetSerieByName("@MinMax")
        If MinMaxSerie Is Nothing Then
            MinMaxSerie = ResultChart.Series.Add(CreateMinMaxPointsStyle)
            MinMaxSerie.Title = "@MinMax"
            MinMaxSerie.ShowInLegend = False
        End If
        Dim MinMaxTimes As Double() = New Double() {firstTimeToShow, firstTimeToShow}

        Dim ChartDiff As Double = Math.Max(Math.Abs(minYValue), Math.Abs(maxYValue)) * 0.03
        If ChartDiff < 1 Then ChartDiff = 1

        Dim MinMaxValues As Double() = New Double() {minYValue - ChartDiff, maxYValue + ChartDiff}
        MinMaxSerie.Visible = True
        MinMaxSerie.Clear()
        MinMaxSerie.Add(MinMaxTimes, MinMaxValues)
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

        'MinMaxPoints.Add(LastPaintedTime + HiddenTime, MinYValue - 1)
        'MinMaxPoints.Add(LastPaintedTime + HiddenTime, MaxYValue + 1)

        Return MinMaxPoints
    End Function

    Private Function CreateMarkSerie(ByVal markStyle As Steema.TeeChart.Styles.PointerStyles) As Series
        Dim MarkSerie As Series

        MarkSerie = ResultChart.Series.Add(CreateMarkPointsStyle(markStyle))
        MarkSerie.Title = Guid.NewGuid.ToString
        MarkSerie.ShowInLegend = False
        MarkSerie.Visible = True
        MarkSerie.Clear()

        Return MarkSerie
    End Function

    Private Function CreateMarkPointsStyle(ByVal markStyle As Steema.TeeChart.Styles.PointerStyles) As Steema.TeeChart.Styles.Points
        Dim MinMaxPoints As New Steema.TeeChart.Styles.Points

        MinMaxPoints.Color = Color.Black
        MinMaxPoints.LinePen.Color = System.Drawing.Color.Black
        MinMaxPoints.Marks.Callout.ArrowHead = Steema.TeeChart.Styles.ArrowHeadStyles.None
        MinMaxPoints.Marks.Callout.ArrowHeadSize = 8
        MinMaxPoints.Marks.Callout.Brush.Color = System.Drawing.Color.Black
        MinMaxPoints.Marks.Callout.Distance = 0
        MinMaxPoints.Marks.Callout.Draw3D = False
        MinMaxPoints.Marks.Callout.Length = 0
        MinMaxPoints.Marks.Callout.Style = Steema.TeeChart.Styles.PointerStyles.Rectangle
        MinMaxPoints.Marks.Font.Shadow.Visible = False
        MinMaxPoints.Marks.Font.Unit = System.Drawing.GraphicsUnit.World
        MinMaxPoints.Pointer.Brush.Color = System.Drawing.Color.Black
        MinMaxPoints.Pointer.Dark3D = False
        MinMaxPoints.Pointer.Draw3D = False
        MinMaxPoints.Pointer.HorizSize = 4
        MinMaxPoints.Pointer.InflateMargins = False
        MinMaxPoints.Pointer.Pen.Color = System.Drawing.Color.Black
        MinMaxPoints.Pointer.Style = markStyle
        MinMaxPoints.Pointer.VertSize = 4
        MinMaxPoints.Pointer.Visible = True
        MinMaxPoints.Title = Guid.NewGuid.ToString
        MinMaxPoints.XValues.DataMember = "X"
        MinMaxPoints.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending
        MinMaxPoints.YValues.DataMember = "Y"
        MinMaxPoints.Clear()

        Return MinMaxPoints
    End Function

    Private Function FixSerieValue(ByVal value As Double) As Double
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then
            value = My.Settings.MaxChartValue
        End If
        If value > My.Settings.MaxChartValue Then
            value = My.Settings.MaxChartValue
        End If
        If value < -My.Settings.MaxChartValue Then
            value = -My.Settings.MaxChartValue
        End If

        Return value
    End Function

    ''' <summary>
    ''' Gets serie by its title
    ''' </summary>
    ''' <param name="name">Title of the serie</param>
    ''' <param name="createIfNotExist">If true and series was not found it will be created</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSerieByName(ByVal name As String, _
                                   Optional ByVal createIfNotExist As Boolean = False) _
                                        As Series
        Dim Serie As Series = Nothing

        For Each ChartSerie As Series In ResultChart.Series
            If ChartSerie.Title = name Then
                Serie = ChartSerie
                Exit For
            End If
        Next

        'if series was not found and it should be created - create
        If Serie Is Nothing AndAlso createIfNotExist Then
            Serie = CreateSerie(name)
        End If

        Return Serie
    End Function

    Private Function CreateSerie(ByVal name As String) As Series
        Dim Serie As Series

        Dim LineStyle As New Steema.TeeChart.Styles.Line
        Serie = ResultChart.Series.Add(LineStyle)
        Serie.Color = ColorGenerator.Instance.GenerateColor
        Serie.Title = name
        Serie.ShowInLegend = True
        AddHandler Serie.Click, AddressOf Serie_Click
        ResultChart.Series.Add(Serie)

        Return Serie
    End Function

#End Region

#Region "Hints"

    Private Sub Serie_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not ShowHintOnSerieClick Then Return

        Dim Serie As Series = TryCast(sender, Series)
        If Serie Is Nothing Then Return

        Dim xValue As Double = Serie.XScreenToValue(e.X)

        Dim Value As Double = GetSerieYValue(Serie, xValue)
        'Value = Serie.YScreenToValue(e.Y)
        Dim ValueString As String = Math.Round(Value, 6, MidpointRounding.AwayFromZero).ToString
        If ValueString.Length > 4 AndAlso Value < 100 Then
            ValueString = ValueString.Substring(0, 4)
        End If
        Dim i As Integer 'variable just to test real type of the value
        While ValueString.EndsWith("0") AndAlso ValueString.Length > 1 AndAlso Not Integer.TryParse(ValueString, i)
            ValueString = ValueString.Remove(ValueString.Length - 1, 1)
        End While
        If ValueString.EndsWith(".") OrElse ValueString.EndsWith(",") Then ValueString = ValueString.Remove(ValueString.Length - 1, 1)
        If ValueString = "-0" Then ValueString = "0"

        Dim HintText As String
        HintText = Serie.Title & vbCrLf
        If ShowHintPercents Then
            Dim StartValue As Double = Serie.YValues(0) 'get first shown point as start value
            Dim Persent As Double
            Persent = (Value * 100) / StartValue - 100
            HintText &= Math.Round(Persent, 2, MidpointRounding.AwayFromZero).ToString & "%" & vbCrLf
        End If
        HintText &= ValueString

        Dim Hint As New Label
        If UserPreference.ShowHintBevel Then
            Hint.BorderStyle = BorderStyle.FixedSingle
        Else
            Hint.BorderStyle = BorderStyle.None
        End If
        Hint.BackColor = UserPreference.HintColor
        Hint.AutoSize = True
        ' Hint.BorderStyle = BorderStyle.FixedSingle
        Hint.Text = HintText
        Hint.Font = UserPreference.Font
        Hint.Parent = ResultChart
        Hint.Left = e.X
        Hint.Top = e.Y
        Hint.Visible = True
        'Hint.Show(HintText, ResultChart, e.X, e.Y)
        Hints.Add(Hint)


    End Sub

    Private Function GetSerieYValue(ByVal Serie As Series, ByVal xValue As Double) As Double
        If Serie.XValues Is Nothing OrElse Serie.XValues.Count = 0 Then Return 0

        Dim MinIndex As Integer = 0
        Dim MaxIndex As Integer = Serie.XValues.Count - 1

        For i As Integer = 0 To Serie.XValues.Count - 1
            If Serie.XValues(i) = xValue Then
                'bungo - index is found - just return it
                Return Serie.YValues(i)
            ElseIf Serie.XValues(i) < xValue Then
                MinIndex = i
            Else
                MaxIndex = i
            End If
        Next

        Dim MinXValue As Double = Serie.XValues(MinIndex)
        Dim MaxXValue As Double = Serie.XValues(MaxIndex)
        Dim MinYValue As Double = Serie.YValues(MinIndex)
        Dim MaxYValue As Double = Serie.YValues(MaxIndex)

        If MinYValue = MaxYValue Then
            Return MinYValue
        End If

        Return ((xValue - MinXValue) * (MaxYValue - MinYValue)) / (MaxXValue - MinXValue) + MinYValue

    End Function

    Private Sub ClearHints()
        Try
            For i As Integer = Hints.Count - 1 To 0 Step -1
                'Hints(i).Hide(ResultChart)
                Hints(i).Visible = True
                Hints(i).Dispose()
            Next
        Catch
        End Try

        Hints.Clear()
    End Sub

#End Region

#Region "Chart events"

    Private Sub ResultChart_UndoneZoom(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResultChart.UndoneZoom
        ClearHints()
    End Sub

    Private Sub ResultChart_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResultChart.VisibleChanged
        ClearHints()
    End Sub

    Private Sub ResultChart_Zoomed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResultChart.Zoomed
        ClearHints()
    End Sub

#End Region

    Private Sub CentralTabControl_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles CentralTabControl.SelectedPageChanged
        ClearHints()
    End Sub

#Region "Chart context menu"

    Private Sub ShowChartEditorMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowChartEditorMenuItem.Click
        ChartEditor.ShowModal()
    End Sub

    Private Sub ClearHintsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearHintsMenuItem.Click
        ClearHints()
    End Sub

    Private Sub SetMarksMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetMarksMenuItem.Click
        RecreateMarkItemsList()
        If MarkItems.Count = 0 Then
            ShowInformationMessage("Unable to set marks - you have no shown values")
            Return
        End If

        Dim SetMarksDialog As New SetMarksDialog(MarkItems)
        If SetMarksDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            MarkItems = SetMarksDialog.MarkItems
            ShowMarks()
        End If
    End Sub

    Private Sub RecreateMarkItemsList()
        If MarkItems.Count = 0 Then

            If CurrentConfig IsNot Nothing AndAlso CurrentConfig.Models IsNot Nothing AndAlso CurrentConfig.Models.Count > 0 Then
                For Each Model As ModelBase.IModel In CurrentConfig.Models
                    If Not Model.HasVisibleValues Then Continue For
                    For Each Value As ModelBase.Value In Model.GetValues
                        'check whether this value should be shown
                        If Not Value.Visible Then Continue For
                        MarkItems.Add(New MarkItem(CurrentConfig, Model.GetName, Value.Name))
                    Next
                Next
            End If

            If CalculatingState <> CalculatingStates.InProcess AndAlso HistoryConfigurations.Count > 0 Then
                For Each SavedConfiguration As ModelBase.SavedConfiguration In HistoryConfigurations
                    If SavedConfiguration IsNot Nothing AndAlso SavedConfiguration.Models IsNot Nothing AndAlso SavedConfiguration.Models.Count > 0 Then
                        For Each Model As ModelBase.IModel In SavedConfiguration.Models
                            If Not Model.HasVisibleValues Then Continue For
                            For Each Value As ModelBase.Value In Model.GetValues
                                'check whether this value should be shown
                                If Not Value.Visible Then Continue For
                                MarkItems.Add(New MarkItem(SavedConfiguration, Model.GetName, Value.Name))
                            Next
                        Next
                    End If
                Next
            End If

        End If
    End Sub

    Private Sub CopyImageToClipboardMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImageToClipboardMenuItem.Click
        ShowProgress("Saving picture...")
        Try
            Dim Color As Color = ResultChart.BackColor
            ResultChart.BackColor = Color.White
            Dim GrayScale As Boolean = ResultChart.Export.Image.JPEG.GrayScale
            If UserPreference.ResultImage_SaveColoured Then
                ResultChart.Export.Image.JPEG.GrayScale = False
                ResultChart.Export.Image.JPEG.CopyToClipboard()
            Else
                ResultChart.Export.Image.JPEG.GrayScale = True
                ResultChart.Export.Image.JPEG.CopyToClipboard()
            End If
            ResultChart.Export.Image.JPEG.GrayScale = GrayScale
            ResultChart.BackColor = Color
            ShowInformationMessage("Done")
        Catch ex As Exception
            ShowExeptionMessage(ex, "Saving picture failed")
        Finally
            HideProgress()
        End Try
    End Sub

    Private Sub RefreshMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshMenuItem.Click
        RefreshChart()
    End Sub

    Private Sub ChartContextMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ChartContextMenu.Opening
        ClearHintsMenuItem.Visible = Hints.Count > 0
        RefreshMenuItem.Visible = RefreshResultButtonItem.Enabled
        SetMarksMenuItem.Visible = RefreshResultButtonItem.Enabled
    End Sub

#End Region

#End Region

#Region "Customize"

    Private Sub CustomizeButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles CustomizeButtonItem.ItemClick
        'reload client panels to ensure they will be correctly shown
        _ClientPanels = Nothing

        'show customize form
        Dim CustomizeForm As New CustomizeForm(ClientPanels, ClientButtons)
        CustomizeForm.ShowDialog()
    End Sub

    Public Function GetParentName(ByVal control As Control) As String
        Dim Result As String = String.Empty

        If Me.Controls.Contains(control) Then
            Result = Me.Name
        Else
            For Each ChildControl As Control In Me.Controls
                Result = GetParentName(control, ChildControl)
                If Not String.IsNullOrEmpty(Result) Then
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetParentName(ByVal control As Control, ByVal parentControl As Control) As String
        Dim Result As String = String.Empty

        If parentControl.Controls.Contains(control) Then
            Result = parentControl.Name
        Else
            For Each ChildControl As Control In parentControl.Controls
                Result = GetParentName(control, ChildControl)
                If Not String.IsNullOrEmpty(Result) Then
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetControlByName(ByVal name As String) As Control
        Dim Result As Control = Nothing

        For Each Control As Control In Me.Controls
            If Control.Name = name Then
                Result = Control
            ElseIf Control.Controls IsNot Nothing Then
                For Each ChildControl As Control In Control.Controls
                    Result = GetControlByName(ChildControl, name)
                    If Result IsNot Nothing Then
                        Exit For
                    End If
                Next
            End If
        Next

        Return Result
    End Function

    Private Function GetControlByName(ByVal parentControl As Control, ByVal name As String) As Control
        Dim Result As Control = Nothing

        For Each Control As Control In parentControl.Controls
            If Control.Name = name Then
                Result = Control
            ElseIf Control.Controls IsNot Nothing Then
                For Each ChildControl As Control In Control.Controls
                    Result = GetControlByName(ChildControl, name)
                    If Result IsNot Nothing Then
                        Exit For
                    End If
                Next
            End If
        Next

        Return Result
    End Function

    Public Sub ApplyNewFont(ByVal newFont As Font)
        UserPreference.Font = newFont

        'Me.Font = newFont

        'For Each Control As Control In Me.Controls
        '    ApplyNewFont(newFont, Control)
        'Next

        'update font for hints
        For i As Integer = Hints.Count - 1 To 0 Step -1
            Hints(i).Font = UserPreference.Font
        Next

        'update font for chart
        ResultChart.Legend.Font.Name = newFont.Name
        ResultChart.Legend.Font.Size = newFont.Size

        ResultChart.Axes.Bottom.Labels.Font.Name = newFont.Name
        ResultChart.Axes.Bottom.Labels.Font.Size = newFont.Size
        ResultChart.Axes.Bottom.Title.Font.Name = newFont.Name
        ResultChart.Axes.Bottom.Title.Font.Size = newFont.Size
        ResultChart.Axes.Left.Labels.Font.Name = newFont.Name
        ResultChart.Axes.Left.Labels.Font.Size = newFont.Size
        ResultChart.Axes.Left.Title.Font.Name = newFont.Name
        ResultChart.Axes.Left.Title.Font.Size = newFont.Size

    End Sub

    Private Sub ApplyNewFont(ByVal newFont As Font, ByVal control As Control)
        control.Font = newFont

        For Each ChildControl As Control In control.Controls
            ApplyNewFont(newFont, ChildControl)
        Next
    End Sub

    Public Sub ApplyNewHintBevelStyle(ByVal showBevel As Boolean)
        UserPreference.ShowHintBevel = showBevel

        For i As Integer = Hints.Count - 1 To 0 Step -1
            If UserPreference.ShowHintBevel Then
                Hints(i).BorderStyle = BorderStyle.FixedSingle
            Else
                Hints(i).BorderStyle = BorderStyle.None
            End If
        Next
    End Sub

    Public Sub ApplyNewHintColor(ByVal newColor As Color)
        UserPreference.HintColor = newColor

        For i As Integer = Hints.Count - 1 To 0 Step -1
            Hints(i).BackColor = UserPreference.HintColor
        Next
    End Sub

    Public Sub UpdateRibbonPanels()
        For Each RibbonGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup In MainRibbonPage.Groups
            Dim HasVisibleItems As Boolean = False
            For Each Item As DevExpress.XtraBars.BarItemLink In RibbonGroup.ItemLinks
                If Item.Item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    HasVisibleItems = True
                    Exit For
                End If
            Next
            RibbonGroup.Visible = HasVisibleItems
        Next
        RibbonControl.PerformLayout()
    End Sub

#End Region

#Region "Help-about"

    Private Sub AboutButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles AboutButtonItem.ItemClick
        Dim AboutBox As New AboutBox
        AboutBox.ForeColor = Me.ForeColor
        AboutBox.BackColor = Me.BackColor
        AboutBox.ShowDialog()
    End Sub

    Private Sub HelpButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles HelpButtonItem.ItemClick
        Dim Process As System.Diagnostics.Process
        Dim FileName As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "Help.mht")

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

    Private Sub ConfigurationAboutButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ConfigurationAboutButtonItem.ItemClick
        If CurrentConfig Is Nothing Then Return

        Dim DescriptionEditor As New RtfEditor
        DescriptionEditor.Rtf = CurrentConfig.Description
        DescriptionEditor.Text = "Configuration " & CurrentConfig.Name
        DescriptionEditor.ReadOnlyState = True
        DescriptionEditor.ShowDialog()
    End Sub

#End Region

#Region "General"

    Private Sub ShowInputValuesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowInputValuesCheckBox.CheckedChanged, ShowInternalValuesCheckBox.CheckedChanged
        'update UI
        ClearUI()
        ModelsTree.ShowInputValues = Me.ShowInputValues
        ModelsTree.ShowInternalValues = Me.ShowInternalValues
        CheckRibbon()
    End Sub

    Private Sub LeftScaleTextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftScaleTextEdit.TextChanged
        ResultChart.Axes.Left.Increment = LeftScaleIncrement
    End Sub

    Private Sub BottomScaleTextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BottomScaleTextEdit.TextChanged
        ResultChart.Axes.Bottom.Increment = BottomScaleIncrement
    End Sub

#End Region

#Region "Hot keys handlers"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Dim e As New KeyEventArgs(keyData)

        If e.Control AndAlso e.KeyCode = Keys.U Then
            CustomizeButtonItem_ItemClick(CustomizeButtonItem, Nothing)
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

#Region "Main application menu"

    Private Sub AddModelBarButtonItem_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles AddModelBarButtonItem.ItemClick
        Dim FolderPath As String = My.Settings.ModelsFolder
        If String.IsNullOrEmpty(FolderPath) OrElse Not Functions.Folder.FolderExists(FolderPath) Then
            FolderPath = My.Application.Info.DirectoryPath
        End If

        Dim EditModelForm As New ClientControls.EditModelForm(FolderPath)
        EditModelForm.ShowDialog()
    End Sub

    Private Sub EditModelBarButtonItem_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles EditModelBarButtonItem.ItemClick
        Dim FolderPath As String = My.Settings.ModelsFolder
        If String.IsNullOrEmpty(FolderPath) OrElse Not Functions.Folder.FolderExists(FolderPath) Then
            FolderPath = My.Application.Info.DirectoryPath
        End If

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.InitialDirectory = FolderPath
        OpenFileDialog.Filter = String.Format("editable models (*{0})|*{0}", ModelBase.EditableModel.FILE_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim LoadedModel As ModelBase.EditableModel = ClientControls.EditModelForm.LoadEditableModel(OpenFileDialog.FileName)
            If LoadedModel IsNot Nothing Then
                Dim EditModelForm As New ClientControls.EditModelForm(FolderPath, LoadedModel)
                EditModelForm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub ExitBarButtonItem_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ExitBarButtonItem.ItemClick
        Me.Close()
    End Sub

#End Region



End Class