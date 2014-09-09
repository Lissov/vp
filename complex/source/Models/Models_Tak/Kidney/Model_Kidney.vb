Imports ModelBase
Imports Functions.MathFunctions

Public Class Model_Kidney
    Inherits ModelBase.ModelBase

#Region "inputs"

    'p arterial
    Public Pa As New ModelBase.Value("Pa", "Model_Kidney: Pa", Value.ValueType.Input)

    'regulation
    Public dKr As New ModelBase.Value("dKr", "Model_Kidney: dKr", Value.ValueType.Input)

#End Region

#Region "outputs"

    Public Qf As New ModelBase.Value("Qf", "Model_Kidney: Qf", Value.ValueType.Output)
    Public Qr As New ModelBase.Value("Qr", "Model_Kidney: Qr", Value.ValueType.Output)
    Public Kr As New ModelBase.Value("Kr", "Model_Kidney: Kr", Value.ValueType.Output)

#End Region

#Region "parameters"

    Public Kr_0 As New ModelBase.Parameter("Kr_0", "Kr_0")
    Public Kf As New ModelBase.Parameter("Kf", "Kf")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_Kidneys"
        Me.DisplayName = "Kidney"
        Me.Description = "Kidney part of CVS"

    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        Qf.Value(CurrentStep) = Kf.Value * Pa.Value(CurrentStep - 1)

        'Kr.Value(CurrentStep) = Kr_0.Value

        Qr.Value(CurrentStep) = Kr.Value(CurrentStep - 1) * Qf.Value(CurrentStep - 1)

        'calculating Kr
        Kr.Value(CurrentStep) = Kr.Value(CurrentStep - 1) + _
                               [Step] * dKr.Value(CurrentStep - 1) * Kr_0.Value


    End Sub

#End Region

End Class
