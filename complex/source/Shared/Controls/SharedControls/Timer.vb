
Public Class Timer

#Region "Properties"

    ''' <summary>
    ''' Used to return time betwen timer's start and end in milliseconds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MilliSeconds() As Integer
        Get
            Return CInt(Math.Round((_EndTimer - _StartTimer) / TimeSpan.TicksPerMillisecond, 0))
        End Get
    End Property

    ''' <summary>
    ''' Used to return time betwen timer's start and end in seconds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Seconds() As Double
        Get
            Return MilliSeconds / 1000
        End Get
    End Property

    Private _StartTimer As Long
    ''' <summary>
    ''' Time when timer was started
    ''' </summary>
    Private Property StartTimer() As Long
        Get
            Return _StartTimer
        End Get
        Set(ByVal value As Long)
            _StartTimer = value
        End Set
    End Property

    Private _EndTimer As Long
    ''' <summary>
    ''' Time when timer was stopped
    ''' </summary>
    Private Property EndTimer() As Long
        Get
            Return _EndTimer
        End Get
        Set(ByVal value As Long)
            _EndTimer = value
        End Set
    End Property

    Private _PauseTimer As Long = -1
    ''' <summary>
    ''' Time when timer was paused
    ''' </summary>
    Private Property PauseTimer() As Long
        Get
            Return _PauseTimer
        End Get
        Set(ByVal value As Long)
            _PauseTimer = value
        End Set
    End Property

#End Region

#Region "Public functions"

    Public Sub Start()
        If PauseTimer <> -1 Then
            StartTimer = Now.Ticks - (PauseTimer - StartTimer)
        Else
            StartTimer = Now.Ticks
        End If
        PauseTimer = -1
    End Sub

    Public Sub [Stop](Optional ByVal finalise As Boolean = False)
        EndTimer = Now.Ticks
        If finalise Then
            PauseTimer = -1
        End If
    End Sub

    Public Sub Pause()
        PauseTimer = Now.Ticks
    End Sub


#End Region

End Class

