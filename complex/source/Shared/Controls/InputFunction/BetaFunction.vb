Imports ModelBase

Public Class BetaFunction
    Inherits FunctionBase

#Region "Declarations"

    Public Time1 As New ModelBase.Parameter("Time1", "Time1")
    Public Time2 As New ModelBase.Parameter("Time2", "Time2")
    Public C As New ModelBase.Parameter("C", "C")
    Public Alpha As New ModelBase.Parameter("Alpha", "Alpha")
    Public Beta As New ModelBase.Parameter("Beta", "Beta")
    Public Th As New ModelBase.Parameter("Th", "Th")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Beta
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.Beta
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Beta"
        Me.DisplayName = "Beta"
    End Sub


#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        If Time1.Value = Time2.Value Then Return 0
        Dim t1 As Double
        Dim t2 As Double
        If Time1.Value < Time2.Value Then
            t1 = Time1.Value
            t2 = Time2.Value
        Else
            t1 = Time2.Value
            t2 = Time1.Value
        End If
        If time <= t1 OrElse time >= t2 Then Return 0

        Dim tMid As Double = t1 + (t2 - t1) / 2
        Dim BetaValue As Double = 0
        If time = tMid Then
            BetaValue = 1
        ElseIf time < tMid Then
            Dim tBeta As Double = (time - t1) / (tMid - t1)
            BetaValue = Functions.MathFunctions.BF(tBeta, Alpha.Value, Beta.Value, Th.Value)
        Else
            Dim tBeta As Double = (t2 - time) / (t2 - tMid)
            BetaValue = Functions.MathFunctions.BF(tBeta, Alpha.Value, Beta.Value, Th.Value)
        End If
        If BetaValue < 0 Then BetaValue = 0

        Return BetaValue * C.Value
    End Function

End Class