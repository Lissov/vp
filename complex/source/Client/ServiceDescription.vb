Imports System.ServiceProcess

Public Class ServiceDescription

#Region "Properties"

    Private _ServiceName As String
    ''' <summary>
    ''' Name of the service
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServiceName() As String
        Get
            Return _ServiceName
        End Get
        Set(ByVal value As String)
            _ServiceName = value
        End Set
    End Property

    Private _ServiceDisplayName As String
    ''' <summary>
    ''' Display name of the service
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServiceDisplayName() As String
        Get
            Return _ServiceDisplayName
        End Get
        Set(ByVal value As String)
            _ServiceDisplayName = value
        End Set
    End Property


    Private _LibraryFileName As String
    ''' <summary>
    ''' Full path to the service's executable file in install directory 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LibraryFileName() As String
        Get
            Return _LibraryFileName
        End Get
        Set(ByVal value As String)
            _LibraryFileName = value
        End Set
    End Property

    Private _WindowsService As System.ServiceProcess.ServiceController
    Public Property WindowsService() As System.ServiceProcess.ServiceController
        Get
            If _WindowsService Is Nothing Then
                Dim services() As System.ServiceProcess.ServiceController
                ' get list of services
                services = System.ServiceProcess.ServiceController.GetServices()

                Dim i As Integer
                ' find needed service
                For i = 0 To services.GetLength(0) - 1
                    If services(i).ServiceName = _ServiceName Then
                        _WindowsService = services(i)
                        Exit For
                    End If
                Next
            End If
            Return _WindowsService
        End Get
        Set(ByVal value As System.ServiceProcess.ServiceController)
            _WindowsService = value
        End Set
    End Property

    ''' <summary>
    ''' FullPath to the executable file of the service
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property PathToService() As String
        Get
            Return System.IO.Path.Combine(Application.ExecutablePath, _LibraryFileName)
        End Get
    End Property

    ''' <summary>
    ''' Name of the dicrectory with the executable file of the service
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property ServiceDirectory() As String
        Get
            Return System.IO.Path.GetDirectoryName(PathToService)
        End Get
    End Property

    ''' <summary>
    ''' Name of the executable file of the service
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property ServiceFileName() As String
        Get
            Return System.IO.Path.GetFileName(PathToService)
        End Get
    End Property

    Public ReadOnly Property ServiceIsInstalled() As Boolean
        Get
            Return Not WindowsService Is Nothing
        End Get
    End Property

    Private _LogFilePath As String = String.Empty
    Public Property LogFilePath() As String
        Get
            Return _LogFilePath
        End Get
        Set(ByVal value As String)
            _LogFilePath = value
        End Set
    End Property

    Private _InstallUtilPath As String
    ''' <summary>
    ''' FullPath to the InstallUtil.exe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property InstallUtilPath() As String
        Get
            If String.IsNullOrEmpty(_InstallUtilPath) Then
                Dim FrameWorkPath As String = IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "Microsoft.NET\Framework\v2.0.50727")
                _InstallUtilPath = System.IO.Path.Combine(FrameWorkPath, "InstallUtil.exe")
            End If
            Return _InstallUtilPath
        End Get
    End Property

#End Region

    Public Sub SetServiceManual()
        If Not WindowsService Is Nothing Then
            Dim path As String = "Win32_Service.Name='" & ServiceName & "'"
            Dim ManagementPath As New System.Management.ManagementPath(path)
            Dim ManagementObject As New System.Management.ManagementObject(ManagementPath)
            Dim Parameters(1) As Object
            Parameters(0) = "Manual"
            ManagementObject.InvokeMethod("ChangeStartMode", Parameters)
        End If
    End Sub

    Public Sub SetServiceAutomatic()
        If Not WindowsService Is Nothing Then
            Dim path As String = "Win32_Service.Name='" & ServiceName & "'"
            Dim ManagementPath As New System.Management.ManagementPath(path)
            Dim ManagementObject As New System.Management.ManagementObject(ManagementPath)
            Dim Parameters(1) As Object
            Parameters(0) = "Automatic"
            ManagementObject.InvokeMethod("ChangeStartMode", Parameters)
        End If
    End Sub

#Region "Start-stop service"

    Public Function StartService(Optional ByVal progress As SharedControls.App.RunningProcess = Nothing) As Boolean
        Try
            If Not WindowsService Is Nothing Then
                WindowsService.Start()
            End If
        Catch ex As Exception
            Dim Prompt As String
            Prompt = "Exception was thrown during starting of the service " & ServiceDisplayName & "."
            Prompt &= vbCrLf & vbCrLf
            Prompt &= ex.ToString
            ShowErrorMessage(Prompt, progress)
            Return False
        End Try

        Return True
    End Function

    Public Function StopService(Optional ByVal progress As SharedControls.App.RunningProcess = Nothing) As Boolean
        If Not WindowsService Is Nothing AndAlso WindowsService.Status <> ServiceProcess.ServiceControllerStatus.Stopped Then
            Try
                WindowsService.Stop()
            Catch ex As Exception
                Dim Prompt As String
                Prompt = "Exception was thrown during stopping of the service " & ServiceDisplayName & "."
                Prompt &= vbCrLf & vbCrLf
                Prompt &= ex.ToString
                ShowErrorMessage(Prompt, progress)
                Return False
            End Try
        End If

        Return True
    End Function

#End Region

    ''' <summary>
    ''' Used to install service to installDirectory.
    ''' Returns true if service was installed.
    ''' </summary>
    ''' <param name="installDirectory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InstallService(ByVal progress As SharedControls.App.RunningProcess) As Boolean
        If Not Functions.File.FileExists(InstallUtilPath) OrElse ServiceIsInstalled Then Return False

        'install service from install directory
        Dim PathToService As String
        PathToService = System.IO.Path.Combine(My.Application.Info.DirectoryPath, LibraryFileName)

        If Functions.File.FileExists(PathToService) Then
            Dim startInfo As New System.Diagnostics.ProcessStartInfo
            startInfo.FileName = InstallUtilPath
            startInfo.Arguments = GenerateInstallutilInstallArgs(PathToService)
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.RedirectStandardOutput = True
            startInfo.UseShellExecute = False

            Try
                Dim Process As System.Diagnostics.Process
                Process = System.Diagnostics.Process.Start(startInfo)

                Dim OutputResult As String = Process.StandardOutput.ReadToEnd()

                Process.WaitForExit()
                If Process.ExitCode <> 0 Then
                    Dim Prompt As String
                    Prompt = "The service " & ServiceDisplayName & " was not installed."
                    Prompt &= vbCrLf & vbCrLf
                    Prompt &= "The next message was received during the installation of the service:" & vbCrLf
                    Prompt &= OutputResult

                    ShowErrorMessage(Prompt, progress)
                    Return False
                End If

                'flag that install completed 
                WindowsService = Nothing
                Return True
            Catch ex As Exception
                Dim Prompt As String
                Prompt = "Exception was thrown during the installation of the service " & ServiceDisplayName & "."
                Prompt &= vbCrLf & vbCrLf
                Prompt &= ex.ToString

                ShowErrorMessage(Prompt, progress)
                Return False
            End Try
        End If

        Return False
    End Function

    Private Function GenerateInstallutilInstallArgs(ByVal pathToService As String) As String
        Dim InstallUtilArguments As String = String.Empty

        InstallUtilArguments &= " """ + pathToService + """"

        'add log if exists
        If Not String.IsNullOrEmpty(LogFilePath) Then
            InstallUtilArguments &= " /LogFile='" + LogFilePath + "'"
        End If

        Return InstallUtilArguments
    End Function

    ''' <summary>
    ''' Used to show message when error occured
    ''' </summary>
    ''' <param name="prompt">Text to be shown</param>
    ''' <param name="progress">Progress to be hidden\shown</param>
    ''' <remarks></remarks>
    Private Sub ShowErrorMessage(ByVal prompt As String, ByVal progress As SharedControls.App.RunningProcess)
        If progress IsNot Nothing AndAlso progress.ProgressForm IsNot Nothing Then
            progress.ProgressForm.Hide()
        End If

        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)

        If progress IsNot Nothing AndAlso progress.ProgressForm IsNot Nothing Then
            progress.ProgressForm.Show()
        End If
    End Sub

    ''' <summary>
    ''' Used to uninstall windows service 
    ''' </summary>
    ''' <param name="servicePath"></param>
    ''' <remarks></remarks>
    Public Function RemoveService(Optional ByVal progress As SharedControls.App.RunningProcess = Nothing) _
                                        As Boolean

        If WindowsService Is Nothing OrElse Not Functions.File.FileExists(InstallUtilPath) Then Return False

        Dim PathToService As String
        PathToService = System.IO.Path.Combine(My.Application.Info.DirectoryPath, LibraryFileName)

        ' service exists and already stopped - unindstall it
        Dim startInfo As New System.Diagnostics.ProcessStartInfo
        startInfo.FileName = InstallUtilPath
        startInfo.Arguments = "/u """ & PathToService & """"
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.RedirectStandardOutput = True
        startInfo.UseShellExecute = False

        Try
            Dim Process As System.Diagnostics.Process
            Process = System.Diagnostics.Process.Start(startInfo)

            Dim OutputResult As String = Process.StandardOutput.ReadToEnd()

            Process.WaitForExit()
            If Process.ExitCode <> 0 Then
                Dim Prompt As String
                Prompt = "The service " & ServiceDisplayName & " was not removed."
                Prompt &= vbCrLf & vbCrLf
                Prompt &= "The next message was received during the removing of the service:" & vbCrLf
                Prompt &= OutputResult
                ShowErrorMessage(Prompt, progress)
                Return False
            End If

            'flag that uninstall completed (checked before install)
            WindowsService = Nothing
            Return True
        Catch ex As Exception
            Dim Prompt As String
            Prompt = "Exception was thrown during the removing of the service " & ServiceDisplayName & "."
            Prompt &= vbCrLf & vbCrLf
            Prompt &= ex.ToString
            ShowErrorMessage(Prompt, progress)
            Return False
        End Try

        Return False
    End Function

End Class
