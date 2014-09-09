Public Class Log

#Region " Properties"


    ''' <summary>
    ''' Returns max size of file log.txt in Mb
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaxLogFileSize() As Integer
        Get
            Return My.Settings.ClearLogAtSizeMb
        End Get
    End Property

#End Region

#Region "Operation logging"

    Private Const OPERATION_LOG As String = "Modelling tool chart service log.txt"

    Private Shared _OperationLogPath As String
    ''' <summary>
    ''' Path to log file with all operations
    ''' </summary>
    Public Shared ReadOnly Property OperationLogPath() As String
        Get
            If String.IsNullOrEmpty(_OperationLogPath) Then
                _OperationLogPath = IO.Path.Combine(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), OPERATION_LOG)
            End If
            Return _OperationLogPath
        End Get
    End Property

    Public Sub InitializeOperationLog()
        Try
            If Not Functions.File.FileExists(OperationLogPath) Then
                IO.File.AppendAllText(OperationLogPath, "Date" & vbTab & vbTab & "Time" & vbTab & vbTab & "Operation" & vbTab & vbTab & "Result" & vbTab & vbTab & vbCrLf)
                IO.File.AppendAllText(OperationLogPath, "---------------------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
            End If
        Catch ex As Exception
            'empty try...catch to be sure that service will not crash if sth is wrong with log
        End Try
    End Sub

    Public Sub WriteLog(ByVal operation As String, ByVal result As String)
        Try
            CheckOperationLogFile()

            Dim LogEntry As String = String.Empty
            LogEntry &= Date.Today.ToShortDateString & vbTab
            LogEntry &= DateTime.Now.ToLongTimeString & vbTab
            LogEntry &= operation & vbTab
            LogEntry &= result & vbCrLf
            IO.File.AppendAllText(OperationLogPath, LogEntry)

        Catch ex As Exception
            'empty try...catch to be sure that service will not crash if sth is wrong with log
        End Try
    End Sub

    Public Sub WriteLog(ByVal operation As String)
        WriteLog(operation, "")
    End Sub

    ''' <summary>
    ''' Prepares log.txt file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckOperationLogFile()
        If Not Functions.File.FileExists(OperationLogPath) Then
            InitializeOperationLog()
        End If

        Try
            Dim LogInfo As New IO.FileInfo(OperationLogPath)
            If LogInfo IsNot Nothing Then
                If ((LogInfo.Length) / 1024) / 1024 > MaxLogFileSize Then
                    Try
                        LogInfo.Delete()
                        InitializeOperationLog()
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            'empty try...catch to be sure that service will not crash if sth is wrong with log
        End Try

    End Sub

    ''' <summary>
    ''' Clears log file
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearLog()
        Functions.File.DeleteFile(OperationLogPath, "")
    End Sub

    ''' <summary>
    ''' Gets all text from log.txt
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllLogRecords() As String
        Try
            Return IO.File.ReadAllText(OperationLogPath)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

#End Region

    Public Sub WriteErrorLog(ByVal operation As String, ByVal errorMessage As String)
        WriteLog(operation, "Failed: " & errorMessage)
    End Sub

    Public Sub WriteErrorLog(ByVal errorMessage As String)
        WriteErrorLog("", errorMessage)
    End Sub

End Class
