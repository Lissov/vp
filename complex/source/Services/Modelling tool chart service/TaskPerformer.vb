Imports Microsoft.Win32

''' <summary>
''' Base class where services's work is beeng performed.
''' Service MUST have settings ConnectionString and Sleep.
''' The next properties MUST be overrided: MySettings, IntancePropertyName.
''' The next sub MUST be overrided: DoWork.
''' </summary>
''' <remarks></remarks>
Public Class TaskPerformer
    Implements IDisposable

    Friend Shared MyThread As System.Threading.Thread

#Region "Const"

    Protected Const APPLICATION_NAME As String = "Modelling tool chart service"

#End Region

#Region " Properties"

    Protected ReadOnly Property SleepTimeSec() As Integer
        Get
            Dim Result As Integer = 0

            'Find in settings
            Dim Obj As Object
            Try
                Obj = My.Settings.Item("Sleep")
            Catch
                Obj = Nothing
            End Try
            If Obj IsNot Nothing AndAlso TypeOf Obj Is Integer Then
                Result = CInt(Obj)
            End If

            Return Result
        End Get
    End Property

    Protected ReadOnly Property Identity() As System.Security.Principal.IIdentity
        Get
            'do not use My.User.CurrentPrincipal.Identity as it is not filled for service
            Return System.Security.Principal.WindowsIdentity.GetCurrent
        End Get
    End Property

    Private _ServiceDisplayName As String = APPLICATION_NAME
    ''' <summary>
    ''' Name of the service. Used in logging.
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

    Private _OperationLog As Log
    Public ReadOnly Property OperationLog() As Log
        Get
            If _OperationLog Is Nothing Then
                _OperationLog = New Log
            End If
            Return _OperationLog
        End Get
    End Property

    Private ReadOnly Property Priority() As ProcessPriorityClass
        Get
            If Not String.IsNullOrEmpty(My.Settings.ProcessPriorityClass) Then
                Dim EnumType As System.Type = GetType(ProcessPriorityClass)
                Select Case My.Settings.ProcessPriorityClass.ToLower
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.Idle).ToLower
                        Return ProcessPriorityClass.Idle
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.BelowNormal).ToLower
                        Return ProcessPriorityClass.BelowNormal
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.Normal).ToLower
                        Return ProcessPriorityClass.Normal
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.AboveNormal).ToLower
                        Return ProcessPriorityClass.AboveNormal
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.High).ToLower
                        Return ProcessPriorityClass.High
                    Case System.Enum.GetName(EnumType, ProcessPriorityClass.RealTime).ToLower
                        Return ProcessPriorityClass.RealTime
                End Select
            End If
            Return ProcessPriorityClass.Normal
        End Get
    End Property

#End Region

    Protected Sub SetPriority()
        Try
            Process.GetCurrentProcess().PriorityClass = Priority
        Catch ex As Exception
            OperationLog.WriteErrorLog("Failed to set the priority to " & System.Enum.GetName(GetType(ProcessPriorityClass), Priority), ex.ToString)
        End Try
    End Sub

    Private Sub CrackTeeChart()
        'value of key HKEY_CLASSES_ROOT\CLSID\{ED6227D7-889F-483E-AEF4-C090D24BFEE1}\TypeLib 
        'must be deleted
        Const SubKeyName As String = "CLSID\{ED6227D7-889F-483E-AEF4-C090D24BFEE1}\TypeLib"
        Try
            Registry.ClassesRoot.DeleteSubKey(SubKeyName, False)
        Catch
        End Try
    End Sub

    Public Sub Run()
        Try

            While MyThread IsNot Nothing AndAlso MyThread.ThreadState <> Threading.ThreadState.Stopped

                If MyThread IsNot Nothing AndAlso MyThread.ThreadState <> Threading.ThreadState.Stopped Then
                    Try

                        SetPriority()

                        DoWork()

                    Catch ex As Exception
                    End Try
                End If

                If MyThread IsNot Nothing AndAlso MyThread.ThreadState <> Threading.ThreadState.Stopped Then
                    Threading.Thread.Sleep(SleepTimeSec * 1000)
                End If

            End While


        Catch ex As Exception
            OperationLog.WriteErrorLog("Exception occurred: ", ex.ToString)
        End Try

    End Sub

    Protected Sub DoWork()
        CrackTeeChart()
    End Sub

#Region " IDisposable Support "

    ''' <summary>
    ''' To detect redundant calls
    ''' </summary>
    ''' <remarks></remarks>
    Private disposedValue As Boolean = False

    ''' <summary>
    ''' IDisposable
    ''' </summary>
    ''' <param name="disposing"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                MyThread = Nothing
            End If

        End If
        Me.disposedValue = True
    End Sub

    ''' <summary>
    ''' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region


End Class

