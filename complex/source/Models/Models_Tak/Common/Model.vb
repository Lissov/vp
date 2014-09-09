Imports ModelBase
Imports Functions.MathFunctions

Public Class Model
    Inherits ModelBase.ModelBase

#Region "inputs"

    'V from all parts
    Public V_partV As New ModelBase.Value("V_partV", "Common: V_partV", Value.ValueType.Input)
    Public V_partCV As New ModelBase.Value("V_partCV", "Common: V_partCV", Value.ValueType.Input)
    Public V_partPS As New ModelBase.Value("V_partPS", "Common: V_partPS", Value.ValueType.Input)
    Public V_partLA As New ModelBase.Value("V_partLA", "Common: V_partLA", Value.ValueType.Input)
    Public V_partLV As New ModelBase.Value("V_partLV", "Common: V_partLV", Value.ValueType.Input)
    Public V_partLS As New ModelBase.Value("V_partLS", "Common: V_partLS", Value.ValueType.Input)
    Public V_partAA As New ModelBase.Value("V_partAA", "Common: V_partAA", Value.ValueType.Input)
    Public V_partA As New ModelBase.Value("V_partA", "Common: V_partA", Value.ValueType.Input)
    Public V_partAH As New ModelBase.Value("V_partAH", "Common: V_partAH", Value.ValueType.Input)
    Public V_partVH As New ModelBase.Value("V_partVH", "Common: V_partVH", Value.ValueType.Input)

    Public Rah As New ModelBase.Value("Rah", "Common: Rah", Value.ValueType.Input)
    Public Rh As New ModelBase.Value("Rh", "Common: Rh", Value.ValueType.Input)
    Public Rvh As New ModelBase.Value("Rvh", "Common: Rvh", Value.ValueType.Input)

    Public Ra As New ModelBase.Value("Ra", "Common: Ra", Value.ValueType.Input)
    Public Rav As New ModelBase.Value("Rav", "Common: Rav", Value.ValueType.Input)
    Public Rv As New ModelBase.Value("Rv", "Common: Rv", Value.ValueType.Input)


    Public dK As New ModelBase.Value("dK", "Common: dK", Value.ValueType.Input)
    Public dF As New ModelBase.Value("dF", "Common: dF", Value.ValueType.Input)

#End Region

#Region "outputs"

    Public K As New ModelBase.Value("K", "Common: K", Value.ValueType.Output, 0.15, 0.8)
    Public fullK As New ModelBase.Value("fullK", "Common: fullK", Value.ValueType.Output)
    Public f As New ModelBase.Value("f", "Common: f", Value.ValueType.Output, 0.27, 1.32)
    Public fullF As New ModelBase.Value("fullF", "Common: fullF", Value.ValueType.Output)
    Public Vsum As New ModelBase.Value("Vsum", "Common: Vsum", Value.ValueType.Output)

    Public Rtop As New ModelBase.Value("Rtop", "Common: Rtop", Value.ValueType.Output)
    Public Rbottom As New ModelBase.Value("Rbottom", "Common: Rbottom", Value.ValueType.Output)
    Public TPR As New ModelBase.Value("TPR", "Common: TPR", Value.ValueType.Output)

#End Region

#Region "parameters"

    Public k_Tc As New ModelBase.Parameter("k_Tc", "k_Tc")

    Private Tc As Double = 0

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_Common"
        Me.DisplayName = "Common"
        Me.Description = "Common for all parts of CVS"

    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        Vsum.Value(CurrentStep) = V_partV.Value(CurrentStep - 1) + _
                                  V_partCV.Value(CurrentStep - 1) + _
                                  V_partPS.Value(CurrentStep - 1) + _
                                  V_partLA.Value(CurrentStep - 1) + _
                                  V_partLV.Value(CurrentStep - 1) + _
                                  V_partLS.Value(CurrentStep - 1) + _
                                  V_partAA.Value(CurrentStep - 1) + _
                                  V_partA.Value(CurrentStep - 1) + _
                                  V_partAH.Value(CurrentStep - 1) + _
                                  V_partVH.Value(CurrentStep - 1)

        'calculate TPR
        Rtop.Value(CurrentStep) = Rah.Value(CurrentStep - 1) + Rh.Value(CurrentStep - 1) + Rvh.Value(CurrentStep - 1)
        Rbottom.Value(CurrentStep) = Ra.Value(CurrentStep - 1) + Rav.Value(CurrentStep - 1) + Rv.Value(CurrentStep - 1)
        TPR.Value(CurrentStep) = (Rtop.Value(CurrentStep) * Rbottom.Value(CurrentStep)) / (Rtop.Value(CurrentStep) + Rbottom.Value(CurrentStep))

        'calculate k ------------------------------------------
        K.Value(CurrentStep) = K.Value(0) + dK.Value(CurrentStep - 1)
        K.Value(CurrentStep) = K.FixValue(K.Value(CurrentStep))
        fullK.Value(CurrentStep) = K.Value(CurrentStep) * 100

        'calculate f ------------------------------------------
        f.Value(CurrentStep) = f.Value(CurrentStep - 1)
        Tc = (1 / (f.Value(0) * 166.67)) / [Step]  'in steps
        If CurrentStep >= Tc Then
            f.Value(CurrentStep) = f.Value(0) + dF.Value(CurrentStep - 1)
            f.Value(CurrentStep) = f.FixValue(f.Value(CurrentStep))
        End If
        fullF.Value(CurrentStep) = f.Value(CurrentStep) * 166.67
        'end of calculate f -----------------------------------

    End Sub

#End Region

End Class
