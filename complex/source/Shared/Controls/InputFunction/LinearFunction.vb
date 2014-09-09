Imports ModelBase

Public Class LinearFunction
    Inherits FunctionBase

#Region "Declarations"

    Public K As New ModelBase.Parameter("k", "k")
    Public B As New ModelBase.Parameter("b", "b")
    Public Lim As New ModelBase.Parameter("lim", "lim")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Linear
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.kx_b
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Linear"
        Me.DisplayName = "Linear"
    End Sub

#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        Dim Result As Double

        Result = time * K.Value + B.Value

        If Result > Lim.Value Then
            Result = Lim.Value
        End If

        Return Result
    End Function

End Class

