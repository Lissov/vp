Imports ModelBase
Imports Functions.MathFunctions

Public Class Model
    Inherits ModelBase.ModelBase

#Region "inputs"

    Public Br As New ModelBase.Value("Br", "System: Br", Value.ValueType.Input)
    Public Q_LS As New ModelBase.Value("Q_LS", "System: Q_LS", Value.ValueType.Input)
    Public Eh As New ModelBase.Value("Eh", "System: Eh", Value.ValueType.Input)
    Public Em As New ModelBase.Value("Em", "System: Em", Value.ValueType.Input)
    Public Et As New ModelBase.Value("Et", "System: Et", Value.ValueType.Input)
    Public Ed As New ModelBase.Value("Ed", "System: Ed", Value.ValueType.Input)


#End Region

#Region "outputs"

    Public InputO2 As New ModelBase.Value("InputO2", "System: InputO2", Value.ValueType.Output)
    Public Pm As New ModelBase.Value("Pm", "System: Pm", Value.ValueType.Output, 0.2, 1)
    Public VsO2 As New ModelBase.Value("VsO2", "System: VsO2", Value.ValueType.Output)
    Public Vs As New ModelBase.Value("Vs", "System: Vs", Value.ValueType.Output, 0, 1)
    Public Vu As New ModelBase.Value("Vu", "System: Vu", Value.ValueType.Output)
    Public ql As New ModelBase.Value("ql", "System: ql", Value.ValueType.Output)
    Public qf As New ModelBase.Value("qf", "System: qf", Value.ValueType.Output)
    Public qp As New ModelBase.Value("qp", "System: qp", Value.ValueType.Output)
    Public hif As New ModelBase.Value("hif", "System: hif", Value.ValueType.Output, 0.01, 1)
    Public CO2 As New ModelBase.Value("CO2", "System: CO2", Value.ValueType.Output)
    Public rn As New ModelBase.Value("rn", "System: rn", Value.ValueType.Output)
    Public deltaE As New ModelBase.Value("deltaE", "System: deltaE", Value.ValueType.Output)
    Public Eu As New ModelBase.Value("Eu", "System: Eu", Value.ValueType.Output)
    Public E As New ModelBase.Value("E", "System: E", Value.ValueType.Output)
    Public O2 As New ModelBase.Value("O2", "System: O2", Value.ValueType.Output)
    Public Fl As New ModelBase.Value("Fl", "System: Fl", Value.ValueType.Output, 15, 30)
    Public Hb As New ModelBase.Value("Hb", "System: Hb", Value.ValueType.Output)
    Public Gl As New ModelBase.Value("Gl", "System: Gl", Value.ValueType.Output)
    Public BloodStore As New ModelBase.Value("BloodStore", "System: BloodStore", Value.ValueType.Output, 0, 1)

#End Region

#Region "parameters"

    Public hif_Th As New ModelBase.Parameter("hif_Th", "hif_Th")
    Public Pm_k1 As New ModelBase.Parameter("Pm_k1", "Pm_k1")
    Public Pm_k2 As New ModelBase.Parameter("Pm_k2", "Pm_k2")
    Public Pm_k3 As New ModelBase.Parameter("Pm_k3", "Pm_k3")
    Public k_Pm As New ModelBase.Parameter("k_Pm", "k_Pm")
    Public k_InputO2 As New ModelBase.Parameter("k_InputO2", "k_InputO2")
    Public VsO2_k1 As New ModelBase.Parameter("VsO2_k1", "VsO2_k1")
    Public VsO2_k2 As New ModelBase.Parameter("VsO2_k2", "VsO2_k2")
    Public VsO2_k3 As New ModelBase.Parameter("VsO2_k3", "VsO2_k3")
    Public k_Vs As New ModelBase.Parameter("k_Vs", "k_Vs")
    Public Eu_k1 As New ModelBase.Parameter("Eu_k1", "Eu_k1")
    Public Eu_k2 As New ModelBase.Parameter("Eu_k2", "Eu_k2")
    Public Eu_k3 As New ModelBase.Parameter("Eu_k3", "Eu_k3")
    Public Eu_k4 As New ModelBase.Parameter("Eu_k4", "Eu_k4")
    Public E_k1 As New ModelBase.Parameter("E_k1", "E_k1")
    Public E_k2 As New ModelBase.Parameter("E_k2", "E_k2")
    Public E_th As New ModelBase.Parameter("E_th", "E_th")
    Public hif_k1 As New ModelBase.Parameter("hif_k1", "hif_k1")
    Public hif_k2 As New ModelBase.Parameter("hif_k2", "hif_k2")
    Public hif_k3 As New ModelBase.Parameter("hif_k3", "hif_k3")
    Public hif_k4 As New ModelBase.Parameter("hif_k4", "hif_k4")
    Public hif_k5 As New ModelBase.Parameter("hif_k5", "hif_k5")
    Public Fl_k1 As New ModelBase.Parameter("Fl_k1", "Fl_k1")
    Public Fl_k2 As New ModelBase.Parameter("Fl_k2", "Fl_k2")
    Public Fl_k3 As New ModelBase.Parameter("Fl_k3", "Fl_k3")
    Public Hb_k1 As New ModelBase.Parameter("Hb_k1", "Hb_k1")
    Public Hb_k2 As New ModelBase.Parameter("Hb_k2", "Hb_k2")
    Public Hb_k3 As New ModelBase.Parameter("Hb_k3", "Hb_k3")
    Public Hb_k4 As New ModelBase.Parameter("Hb_k4", "Hb_k4")
    Public Hb_k5 As New ModelBase.Parameter("Hb_k5", "Hb_k5")
    Public Hb_k6 As New ModelBase.Parameter("Hb_k6", "Hb_k6")
    Public BloodStore_k1 As New ModelBase.Parameter("BloodStore_k1", "BloodStore_k1")
    Public BloodStore_k2 As New ModelBase.Parameter("BloodStore_k2", "BloodStore_k2")
    Public BloodStore_k3 As New ModelBase.Parameter("BloodStore_k3", "BloodStore_k3")
    Public Gl_k1 As New ModelBase.Parameter("Gl_k1", "Gl_k1")
    Public Gl_k2 As New ModelBase.Parameter("Gl_k2", "Gl_k2")
    Public Gl_k3 As New ModelBase.Parameter("Gl_k3", "Gl_k3")
    Public rn_k1 As New ModelBase.Parameter("rn_k1", "rn_k1")
    Public rn_k2 As New ModelBase.Parameter("rn_k2", "rn_k2")
    Public rn_k3 As New ModelBase.Parameter("rn_k3", "rn_k3")
    Public O2_k1 As New ModelBase.Parameter("O2_k1", "O2_k1")
    Public O2_k2 As New ModelBase.Parameter("O2_k2", "O2_k2")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_System"
        Me.DisplayName = "System"
        Me.Description = "System model (energy)"

        Pm.InitValue = 0.6
        E.InitValue = 0.3
        Eu.InitValue = 0.5
        InputO2.InitValue = 0.5
        Fl.InitValue = 20
        Hb.InitValue = 0.2
        Gl.InitValue = 0.5
        rn.InitValue = 0.5
        O2.InitValue = 0.5

        hif_Th.Value = 0.05
        Pm_k1.Value = 2
        Pm_k2.Value = 0.1
        Pm_k3.Value = 0.21
        k_Pm.Value = 0.005
        k_InputO2.Value = 0.006
        VsO2_k1.Value = 1
        VsO2_k2.Value = 1
        VsO2_k3.Value = 0.15
        k_Vs.Value = 1
        Eu_k1.Value = 0.464
        Eu_k2.Value = 1
        Eu_k3.Value = 1
        Eu_k4.Value = 1
        E_k1.Value = 0.007
        E_k2.Value = 1000
        E_th.Value = 0.2
        hif_k1.Value = 40000
        hif_k2.Value = 0.2
        hif_k3.Value = 0.00001
        hif_k4.Value = 100
        hif_k5.Value = 0.00001
        Fl_k1.Value = 14.285714286
        Fl_k2.Value = 0.09
        Fl_k3.Value = 1
        Hb_k1.Value = 7.4074074074
        Hb_k2.Value = 0.074
        Hb_k3.Value = 0.001
        Hb_k4.Value = 7.4074074074
        Hb_k5.Value = 0.074
        Hb_k6.Value = 0.001
        BloodStore_k1.Value = 15
        BloodStore_k2.Value = 0.1
        BloodStore_k3.Value = 1
        Gl_k1.Value = 1
        Gl_k2.Value = 1
        Gl_k3.Value = 1
        rn_k1.Value = 2.4630541872
        rn_k2.Value = 0.485
        rn_k3.Value = 1
        O2_k1.Value = 1
        O2_k2.Value = 1

    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        'E.MaxValue = 2 * E.InitValue
        'E.MinValue = 0.5 * E.InitValue

        Hb.MaxValue = 2 * Hb.InitValue
        Hb.MinValue = 0.5 * Hb.InitValue

        Gl.MaxValue = 2 * Gl.InitValue
        Gl.MinValue = 0.5 * Gl.InitValue

        rn.MaxValue = 2 * rn.InitValue
        rn.MinValue = 0.5 * rn.InitValue

    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        'calculate Pm ------------------------------------------
        Dim Temp As Double
        If hif.Value(CurrentStep - 1) >= hif_Th.Value Then
            Temp = Pm_k1.Value * (hif.Value(CurrentStep - 1) - hif_Th.Value) - _
                   Pm_k2.Value * Pm.Value(CurrentStep - 1) - _
                   Pm_k3.Value * Br.Value(CurrentStep - 1)
        Else
            Temp = -Pm_k3.Value * Br.Value(CurrentStep - 1)
        End If
        Pm.Value(CurrentStep) = Pm.Value(CurrentStep - 1) + [Step] * Temp * k_Pm.Value
        Pm.Value(CurrentStep) = Pm.FixValue(Pm.Value(CurrentStep))
        'end of calculate Pm ------------------------------------------

        'calculate InputO2 ------------------------------------------
        'fix Qls to 0.006
        'InputO2.Value(CurrentStep) = Q_LS.Value(CurrentStep - 1) * k_InputO2.Value

        InputO2.Value(CurrentStep) = Hb.Value(CurrentStep - 1) * Fl.Value(CurrentStep - 1) * Q_LS.Value(CurrentStep - 1) * k_InputO2.Value

        'calculate VsO2 ------------------------------------------
        VsO2.Value(CurrentStep) = (1 + VsO2_k1.Value * Pm.Value(CurrentStep)) / _
                                  (VsO2_k2.Value + VsO2_k2.Value / InputO2.Value(CurrentStep))

        'calculate Vs ------------------------------------------
        Vs.Value(CurrentStep) = k_Vs.Value * VsO2.Value(CurrentStep)
        Vs.Value(CurrentStep) = Vs.FixValue(Vs.Value(CurrentStep))

        'calculate Eu ------------------------------------------
        '// old - not in use as enrgy, now is used as a part of Vu
        Eu.Value(CurrentStep) = Eu_k1.Value * (Eu_k2.Value * Et.Value(CurrentStep - 1) + _
                                               Eu_k3.Value * Em.Value(CurrentStep - 1) + _
                                               Eu_k4.Value * Eh.Value(CurrentStep - 1))

        Vu.Value(CurrentStep) = Eu_k2.Value * Et.Value(CurrentStep - 1) + _
                                Eu_k3.Value * Em.Value(CurrentStep - 1) + _
                                Eu_k4.Value * Eh.Value(CurrentStep - 1)

        'calculate E --------------------------------------------
        E.Value(CurrentStep) = E.Value(CurrentStep - 1) + _
                            [Step] * (E_k1.Value * (Vs.Value(CurrentStep) - Vu.Value(CurrentStep))) * E_k2.Value
        E.Value(CurrentStep) = E.FixValue(E.Value(CurrentStep))

        deltaE.Value(CurrentStep) = -Eu.Value(CurrentStep) + E.Value(CurrentStep)


        'calculate hif --------------------------------------------
        If E.Value(CurrentStep) <= E_th.Value Then
            hif.Value(CurrentStep) = hif.Value(CurrentStep - 1) + _
                                     [Step] * (hif_k1.Value * (E_th.Value - E.Value(CurrentStep)) - _
                                     hif_k2.Value * Ed.Value(CurrentStep - 1)) * hif_k3.Value
        Else
            hif.Value(CurrentStep) = hif.Value(CurrentStep - 1) + _
                                     [Step] * (-hif_k4.Value * Ed.Value(CurrentStep - 1)) * hif_k5.Value
            If hif.Value(CurrentStep) > hif.Value(CurrentStep - 1) Then
                hif.Value(CurrentStep) = hif.Value(CurrentStep - 1)
            End If
        End If
        hif.Value(CurrentStep) = hif.FixValue(hif.Value(CurrentStep))
        'end of calculate hif --------------------------------------------

        Dim hifTmp As Double = hif.Value(CurrentStep) - 0.05

        'calculate fl --------------------------------------------
        Fl.Value(CurrentStep) = Fl.Value(CurrentStep - 1) + _
                                [Step] * (Fl_k1.Value * hifTmp - _
                                Fl_k2.Value * Fl.Value(CurrentStep - 1)) * Fl_k3.Value
        Fl.Value(CurrentStep) = Fl.FixValue(Fl.Value(CurrentStep))

        'calculate Hb --------------------------------------------
        'delta for Hb consists of 2 parts:
        ' 1 - from blood store (small, very fast)
        ' 2 - by regulation
        If hifTmp > 0 Then
            BloodStore.Value(CurrentStep) = BloodStore.Value(CurrentStep - 1) - _
                                    [Step] * (BloodStore_k1.Value * hifTmp - BloodStore_k2.Value) * BloodStore_k3.Value
        Else
            BloodStore.Value(CurrentStep) = BloodStore.Value(CurrentStep - 1)
        End If
        BloodStore.Value(CurrentStep) = BloodStore.FixValue(BloodStore.Value(CurrentStep))

        Dim DeltaBs As Double = BloodStore.Value(CurrentStep) - BloodStore.Value(CurrentStep - 1)
        If DeltaBs < 0 Then
            DeltaBs = -DeltaBs
        Else
            DeltaBs = 0
        End If
        Hb.Value(CurrentStep) = Hb.Value(CurrentStep - 1) + DeltaBs + _
                                [Step] * (Hb_k1.Value * hifTmp - Hb_k2.Value) * Hb_k3.Value
        'Hb.Value(CurrentStep) = Hb.Value(CurrentStep - 1) + _
        '                        [Step] * (Hb_k1.Value * hifTmp - Hb_k2.Value) * Hb_k3.Value + _
        '                        [Step] * (Hb_k4.Value * hifTmp - Hb_k5.Value) * Hb_k6.Value
        Hb.Value(CurrentStep) = Hb.FixValue(Hb.Value(CurrentStep))

        'calculate Gl --------------------------------------------
        Gl.Value(CurrentStep) = Gl.Value(CurrentStep - 1) + _
                                [Step] * (Gl_k1.Value * hifTmp - Gl_k2.Value) * Gl_k3.Value
        Gl.Value(CurrentStep) = Gl.FixValue(Gl.Value(CurrentStep))

        'calculate rn --------------------------------------------
        rn.Value(CurrentStep) = rn.Value(CurrentStep - 1) + _
                                [Step] * (rn_k1.Value * hifTmp - rn_k2.Value * rn.Value(CurrentStep - 1)) * rn_k3.Value
        rn.Value(CurrentStep) = rn.FixValue(rn.Value(CurrentStep))

        'calculate O2 --------------------------------------------
        O2.Value(CurrentStep) = O2_k1.Value * Fl.Value(CurrentStep) + _
                                O2_k2.Value * Hb.Value(CurrentStep)


    End Sub

#End Region

End Class
