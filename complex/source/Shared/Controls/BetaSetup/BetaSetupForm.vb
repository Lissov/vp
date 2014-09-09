Imports ModelBase

Public Class BetaSetupForm

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal alpha As Parameter, _
                   ByVal beta As Parameter, _
                   ByVal th As Parameter)
        Me.New()

        SetupControl.Alpha = alpha
        SetupControl.Beta = beta
        SetupControl.Th = th

        SetupControl.UpdateUI()
    End Sub

    Public Sub New(ByVal alpha As Parameter, _
                   ByVal beta As Parameter, _
                   ByVal th As Parameter, _
                   ByVal startTime As Double, _
                   ByVal endTime As Double)
        Me.New()

        SetupControl.Alpha = alpha
        SetupControl.Beta = beta
        SetupControl.Th = th

        SetupControl.StartTime = startTime
        SetupControl.EndTime = endTime

        SetupControl.UpdateUI()
    End Sub

#End Region




End Class