Imports ModelBase

Public Class ConstantFunction
    Inherits FunctionBase

#Region "Declarations"

    Public K As New ModelBase.Parameter("k", "k")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Constant
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.k
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Constant"
        Me.DisplayName = "Constant"
    End Sub


#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        Return K.Value
    End Function

End Class
