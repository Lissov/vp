﻿Imports ModelBase

Public Class Model
    Inherits ModelBase.ModelBase

    'Public V As New ModelBase.Value("V", "V", ModelBase.Value.ValueType.Input)

    Public Pa As New ModelBase.Value("Pa", "Pa", ModelBase.Value.ValueType.Output)
    Public Pv As New ModelBase.Value("Pv", "Pv", ModelBase.Value.ValueType.Output)
    Public Q As New ModelBase.Value("Q", "Q", ModelBase.Value.ValueType.Output)

    Public K1 As New ModelBase.Parameter("k1", "k1")

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_NeuprSSS"
        Me.DisplayName = "Неуправляемая ССС"
        Me.Description = "Модель неуправляемой ССС"
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        Pa.Value(CurrentStep) = Pa.Value(CurrentStep - 1) + [Step] * 0

    End Sub

#End Region

#Region "UI Methods"

    Public Overrides Function GetControl() As System.Windows.Forms.UserControl
        Dim TestControl As New TestControl
        Return TestControl
    End Function

#End Region

End Class
