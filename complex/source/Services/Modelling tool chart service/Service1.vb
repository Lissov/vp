Public Class Service1

    Protected ReadOnly Property ServiceDisplayName() As String
        Get
            Return "Modelling tool chart service"
        End Get
    End Property

    Private _TaskPerformer As TaskPerformer
    Protected ReadOnly Property TaskPerformer() As TaskPerformer
        Get
            If _TaskPerformer Is Nothing Then
                _TaskPerformer = New TaskPerformer
            End If
            Return _TaskPerformer
        End Get
    End Property

    Protected Overrides Sub OnStart(ByVal args() As String)
        Threading.Thread.Sleep(15000)

        TaskPerformer.ServiceDisplayName = ServiceDisplayName

        TaskPerformer.OperationLog.WriteLog(vbCrLf, "")
        TaskPerformer.OperationLog.WriteLog("-----------------------------------------------------------------------------------------------", "")
        Dim LogText As String
        LogText = ServiceDisplayName & " started."
        TaskPerformer.OperationLog.WriteLog(LogText, "")

        TaskPerformer.MyThread = New Threading.Thread(AddressOf TaskPerformer.Run)
        TaskPerformer.MyThread.Start()
    End Sub

    Protected Overrides Sub OnStop()

        Dim LogText As String
        LogText = ServiceDisplayName & " stopped."
        TaskPerformer.OperationLog.WriteLog(LogText, "")

        TaskPerformer.Dispose()
    End Sub

End Class
