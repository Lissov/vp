Imports ModelBase

Public Class SinFunction
    Inherits FunctionBase

#Region "Declarations"

    Public A As New ModelBase.Parameter("A", "A")
    Public W As New ModelBase.Parameter("w", "w")
    Public v As New ModelBase.Parameter("v", "v")
    Public K As New ModelBase.Parameter("k", "k")
    Public B As New ModelBase.Parameter("b", "b")

#End Region

#Region "Properties"

    Public Overrides ReadOnly Property Type() As InputFunction.FunctionTypes
        Get
            Return InputFunction.FunctionTypes.Sin
        End Get
    End Property

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return My.Resources.sin
        End Get
    End Property

    ''' <summary>
    ''' If is not empty - this text will be shown in the text box 'formula' instead of showing the image
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property DisplayFormulaText() As String
        Get
            Return "K*(A* Sin(W * time + v) + B)"
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        Me.Name = "Sin"
        Me.DisplayName = "Sin"
    End Sub


#End Region

    Public Overrides Function GetCalculatedValue(ByVal time As Double) As Double
        Return K.Value * (A.Value * Math.Sin(W.Value * time + v.Value) + B.Value)
    End Function

End Class
