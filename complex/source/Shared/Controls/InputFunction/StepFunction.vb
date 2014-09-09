Imports ModelBase

Public Class StepFunction
    Inherits FunctionBase

#Region "Declarations"

    Public T1 As New ModelBase.Parameter("t1", "t1")
    Public K1 As New ModelBase.Parameter("k1", "k1")
    Public T2 As New ModelBase.Parameter("t2", "t2")
    Public K2 As New ModelBase.Parameter("k2", "k2")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Step
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.LinFunc
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Step"
        Me.DisplayName = "Step"
    End Sub

#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        Dim Result As Double

        If time <= T1.Value Then
            Result = K1.Value
        ElseIf time >= T2.Value Then
            Result = K2.Value
        Else
            Dim Temp As Double
            Temp = (K2.Value - K1.Value) / (T2.Value - T1.Value)
            Result = Temp * (time - T1.Value) + K1.Value
        End If

        Return Result
    End Function

End Class

