Imports ModelBase
Imports Functions.MathFunctions

Public Class Model
    Inherits ModelBase.ModelBase

#Region "inputs"

    'p from i-1 part
    Public P_Input As New ModelBase.Value("P_Input", "Model_LS: P_Input", Value.ValueType.Input)

    'q from i-1 part
    Public Q_Input As New ModelBase.Value("Q_Input", "Model_LS: Q_Input", Value.ValueType.Input)

    'regulation
    Public dTs_Input As New ModelBase.Value("dTs_Input", "Model_V: dTs_Input", Value.ValueType.Input)
    Public dU_Input As New ModelBase.Value("dU_Input", "Model_V: dU_Input", Value.ValueType.Input)


    Public Kls As New ModelBase.Value("Kls", "Model_LS: Kls", Value.ValueType.Input)
    Public FullF As New ModelBase.Value("FullF", "Model_LS: FullF", Value.ValueType.Input)

#End Region

#Region "outputs"

    Public dQ As New ModelBase.Value("dQ", "Model_LS: dQ", Value.ValueType.Output)
    Public dTs As New ModelBase.Value("dTs", "Model_LS: dTs", Value.ValueType.Output)
    Public dU As New ModelBase.Value("dU", "Model_LS: dU", Value.ValueType.Output)
    Public R As New ModelBase.Value("R", "Model_LS: R", Value.ValueType.Output)
    Public Ts As New ModelBase.Value("Ts", "Model_LS: Ts", Value.ValueType.Output)
    Public U As New ModelBase.Value("U", "Model_LS: U", Value.ValueType.Output)
    Public V As New ModelBase.Value("V", "Model_LS: V", Value.ValueType.Output)
    Public Q As New ModelBase.Value("Q", "Model_LS: Q", Value.ValueType.Output)
    Public P As New ModelBase.Value("P", "Model_LS: P", Value.ValueType.Output)

    'virtual coronar part
    Public Rkor As New ModelBase.Value("Rkor", "Model_LS: Rkor", Value.ValueType.Output)
    Public Qkor As New ModelBase.Value("Qkor", "Model_LS: Qkor", Value.ValueType.Output)
    Public dK As New ModelBase.Value("dK", "Model_LS: dK", Value.ValueType.Output)

#End Region

#Region "parameters"

    Public k_P As New ModelBase.Parameter("k_P", "k_P")
    Public k_Q As New ModelBase.Parameter("k_Q", "k_Q")
    Public k_U As New ModelBase.Parameter("k_U", "k_U")
    Public k_Ts As New ModelBase.Parameter("k_Ts", "k_Ts")
    Public k_R As New ModelBase.Parameter("k_R", "k_R")
    Public k_dTs As New ModelBase.Parameter("k_dTs", "k_dTs")
    Public k_dU As New ModelBase.Parameter("k_R", "k_dU")

    Public k_Rkor1 As New ModelBase.Parameter("k_Rkor1", "k_Rkor1")
    Public k_Rkor2 As New ModelBase.Parameter("k_Rkor2", "k_Rkor2")
    Public dK_enabled As New ModelBase.Parameter("dK_enabled", "dK_enabled")
    Public k_dK As New ModelBase.Parameter("k_dK", "k_dK")

    Public Time_Establishment As New ModelBase.Parameter("Time_Establishment", "Time_Establishment")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_LS"
        Me.DisplayName = "LS"
        Me.Description = "LS part of CVS"

        Time_Establishment.Value = 15

        k_dTs.Value = 0.01
        k_dU.Value = 1

    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        U.MaxValue = 2 * U.InitValue
        U.MinValue = 0.5 * U.InitValue

        Ts.MaxValue = 2 * Ts.InitValue
        Ts.MinValue = 0.5 * Ts.InitValue

    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        'set values
        V.Value(CurrentStep) = V.Value(CurrentStep - 1)
        U.Value(CurrentStep) = U.Value(CurrentStep - 1)
        Ts.Value(CurrentStep) = Ts.Value(CurrentStep - 1)
        R.Value(CurrentStep) = R.Value(CurrentStep - 1)

        'calculating P
        P.Value(CurrentStep) = (V.Value(CurrentStep) - U.Value(CurrentStep)) * Ts.Value(CurrentStep) * k_P.Value

        'calculating Q
        Q.Value(CurrentStep) = (Kls.Value(CurrentStep - 1) * P_Input.Value(CurrentStep - 1) * FullF.Value(CurrentStep - 1)) * k_Q.Value


        'calculating dQ 
        dQ.Value(CurrentStep) = Q_Input.Value(CurrentStep - 1) - Q.Value(CurrentStep - 1)

        'calculating V --------------------------------------------------------
        V.Value(CurrentStep) = V.Value(CurrentStep - 1) + [Step] * (dQ.Value(CurrentStep - 1) + dQ.Value(CurrentStep)) / 2
        'end of calculating V --------------------------------------------------------

        'calculating dU
        dU.Value(CurrentStep) = dU.Value(CurrentStep - 1) + _
                               [Step] * (dU_Input.Value(CurrentStep - 1) - dU.Value(CurrentStep - 1)) * k_dU.Value
        'calculating dTs
        dTs.Value(CurrentStep) = dTs.Value(CurrentStep - 1) + _
                               [Step] * (dTs_Input.Value(CurrentStep - 1) - dTs.Value(CurrentStep - 1)) * k_dTs.Value

        'calculating U
        U.Value(CurrentStep) = U.InitValue + dU.Value(CurrentStep) * (U.InitValue / 100) * k_U.Value
        U.Value(CurrentStep) = U.FixValue(U.Value(CurrentStep))

        'calculating Ts
        Ts.Value(CurrentStep) = Ts.InitValue + dTs.Value(CurrentStep) * (Ts.InitValue / 100) * k_Ts.Value
        Ts.Value(CurrentStep) = Ts.FixValue(Ts.Value(CurrentStep))

        '---- calculating R ----------------------------------------------
        If CurrentTime >= Time_Establishment.Value Then
            Dim StepTh As Integer = Math.Floor(Time_Establishment.Value / [Step])
            R.Value(CurrentStep) = R.Value(StepTh) * k_R.Value * Sqr(V.Value(StepTh) / V.Value(CurrentStep))
        Else
            R.Value(CurrentStep) = R.Value(CurrentStep - 1)
        End If

        '---- calculating virtual coronar part------------------------------------------
        If CurrentTime >= Time_Establishment.Value Then
            Dim StepTh As Integer = Math.Floor(Time_Establishment.Value / [Step])

            Rkor.Value(CurrentStep) = k_Rkor1.Value * (1 + k_Rkor2.Value)
            Qkor.Value(CurrentStep) = Q.Value(CurrentStep) / Rkor.Value(CurrentStep)

            If dK_enabled.Value > 0 Then
                Dim tmp As Double = 0.16665954
                'Qkor<   ->   kps<
                dK.Value(CurrentStep) = dK.Value(CurrentStep - 1) + [Step] * (-Kls.Value(CurrentStep - 1) + tmp * Qkor.Value(CurrentStep)) * k_dK.Value
            Else
                dK.Value(CurrentStep) = 0
            End If

        Else
            Rkor.Value(CurrentStep) = 0
            Qkor.Value(CurrentStep) = 0
            dK.Value(CurrentStep) = 0
        End If
        '---- end of calculating virtual coronar part-----------------------------------

    End Sub

#End Region

End Class
