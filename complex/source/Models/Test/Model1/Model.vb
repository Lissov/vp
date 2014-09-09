Imports ModelBase

Public Class Model
    Inherits ModelBase.ModelBase

    Public External_1 As New ModelBase.Value("External_1", "External_1", ModelBase.Value.ValueType.Input)

    Public X1 As New ModelBase.Value("X1", "X1", ModelBase.Value.ValueType.Output)

    Public K1 As New ModelBase.Parameter("k1", "k1")

    Public Sub New()
        MyBase.New()

        Me.Name = "Model1"
        Me.DisplayName = "Model 1"
        Me.Description = "Test model just to calculate dX1/dt=k1"
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        X1.Value(CurrentStep) = X1.Value(CurrentStep - 1) + [Step] * K1.Value

    End Sub

#End Region

End Class
