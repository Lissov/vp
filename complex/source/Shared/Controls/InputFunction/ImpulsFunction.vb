Imports ModelBase

Public Class ImpulsFunction
    Inherits FunctionBase

#Region "Declarations"

    Public T1 As New ModelBase.Parameter("t1", "t1")
    Public K1 As New ModelBase.Parameter("k1", "k1")
    Public T2 As New ModelBase.Parameter("t2", "t2")
    Public K2 As New ModelBase.Parameter("k2", "k2")
    Public T3 As New ModelBase.Parameter("t3", "t3")
    Public K3 As New ModelBase.Parameter("k3", "k3")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Impuls
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.Impuls
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Impuls"
        Me.DisplayName = "Impuls"
    End Sub

#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        Dim Result As Double

        If time < T1.Value Then
            Result = K1.Value
        ElseIf time < T2.Value Then
            Result = K2.Value
        ElseIf time < T3.Value Then
            Result = K3.Value
        Else
            Result = 1
        End If

        Return Result
    End Function

End Class

