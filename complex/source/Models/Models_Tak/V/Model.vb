Imports ModelBase
Imports Functions.MathFunctions

Public Class Model
    Inherits ModelBase.ModelBase

#Region "inputs"

    'p from i+1 part
    Public P_Output As New ModelBase.Value("P_Output", "Model_V: P_Output", Value.ValueType.Input)

    'q from i-1 part
    Public Q_Input As New ModelBase.Value("Q_Input", "Model_V: Q_Input", Value.ValueType.Input)

    'regulation
    Public dTs_Input As New ModelBase.Value("dTs_Input", "Model_V: dTs_Input", Value.ValueType.Input)
    Public dU_Input As New ModelBase.Value("dU_Input", "Model_V: dU_Input", Value.ValueType.Input)

    'kidney
    Public Qr As New ModelBase.Value("Qr", "Model_Kidney: Qr", Value.ValueType.Input)

#End Region

#Region "outputs"

    Public dQ As New ModelBase.Value("dQ", "Model_V: dQ", Value.ValueType.Output)
    Public dTs As New ModelBase.Value("dTs", "Model_V: dTs", Value.ValueType.Output)
    Public dU As New ModelBase.Value("dU", "Model_V: dU", Value.ValueType.Output)
    Public R As New ModelBase.Value("R", "Model_V: R", Value.ValueType.Output)
    Public Ts As New ModelBase.Value("Ts", "Model_V: Ts", Value.ValueType.Output)
    Public U As New ModelBase.Value("U", "Model_V: U", Value.ValueType.Output)
    Public V As New ModelBase.Value("V", "Model_V: V", Value.ValueType.Output)
    Public Q As New ModelBase.Value("Q", "Model_V: Q", Value.ValueType.Output)
    Public P As New ModelBase.Value("P", "Model_V: P", Value.ValueType.Output)

    '  Br - Vbase - виртуальный поток из самой большой вены (для забора крови)
    Public V_Transfusion As New ModelBase.Value("V_Transfusion", "Model_V: V_Transfusion", Value.ValueType.Output)

#End Region

#Region "parameters"

    Public k_P As New ModelBase.Parameter("k_P", "k_P")
    Public k_Q As New ModelBase.Parameter("k_Q", "k_Q")
    Public k_U As New ModelBase.Parameter("k_U", "k_U")
    Public k_Ts As New ModelBase.Parameter("k_Ts", "k_Ts")
    Public k_R As New ModelBase.Parameter("k_R", "k_R")
    Public k_dTs As New ModelBase.Parameter("k_dTs", "k_dTs")
    Public k_dU As New ModelBase.Parameter("k_R", "k_dU")

    Public Transfusion_Start As New ModelBase.Parameter("Transfusion_Start", "Transfusion_Start")
    Public Transfusion_End As New ModelBase.Parameter("Transfusion_End", "Transfusion_End")
    Public Transfusion_Speed As New ModelBase.Parameter("Transfusion_Speed", "Transfusion_Speed")
    Public Transfusion_Direction As New ModelBase.Parameter("Transfusion_Direction", "Transfusion_Direction")

    Public Time_Establishment As New ModelBase.Parameter("Time_Establishment", "Time_Establishment")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_V"
        Me.DisplayName = "V"
        Me.Description = "V part of CVS"

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

        'X1.Value(CurrentStep) = X1.Value(CurrentStep - 1) + [Step] * (I1.Value(CurrentStep - 1))

        'set values
        V.Value(CurrentStep) = V.Value(CurrentStep - 1)
        U.Value(CurrentStep) = U.Value(CurrentStep - 1)
        Ts.Value(CurrentStep) = Ts.Value(CurrentStep - 1)
        R.Value(CurrentStep) = R.Value(CurrentStep - 1)

        'calculating P
        P.Value(CurrentStep) = (V.Value(CurrentStep) - U.Value(CurrentStep)) * Ts.Value(CurrentStep) * k_P.Value

        'calculating Q
        Q.Value(CurrentStep) = ((P.Value(CurrentStep - 1) - P_Output.Value(CurrentStep - 1)) / R.Value(CurrentStep - 1)) * k_Q.Value

        'calculating dQ 
        dQ.Value(CurrentStep) = Q_Input.Value(CurrentStep - 1) - Q.Value(CurrentStep - 1) + Qr.Value(CurrentStep - 1)

        'calculating V --------------------------------------------------------
        V.Value(CurrentStep) = V.Value(CurrentStep - 1) + [Step] * (dQ.Value(CurrentStep - 1) + dQ.Value(CurrentStep)) / 2
        'breakpoint for debug
        If CurrentStep >= Math.Round(160 / [Step]) Then
            V.Value(CurrentStep) = V.Value(CurrentStep)
        End If
        '  Br - Vbase - виртуальный поток из самой большой вены (для забора крови)
        If (CurrentTime >= Transfusion_Start.Value) AndAlso (CurrentTime <= Transfusion_End.Value) Then
            V_Transfusion.Value(CurrentStep) = Transfusion_Speed.Value / (Transfusion_End.Value - Transfusion_Start.Value)
        End If
        If Not Transfusion_Direction.Value > 0 Then
            V_Transfusion.Value(CurrentStep) = -V_Transfusion.Value(CurrentStep)
        End If
        V.Value(CurrentStep) = V.Value(CurrentStep) - V_Transfusion.Value(CurrentStep)
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

    End Sub

#End Region

End Class
