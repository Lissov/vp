Imports ModelBase
Imports Functions.MathFunctions

Public Class Model_Topp
    Inherits ModelBase.ModelBase

#Region "Const"

    Public Const INPUT_XML_ELEMENT As String = "Input"

#End Region

#Region "Properties"

    Private _InputFunctionControl As InputFunction.InputFunctionControl
    Public ReadOnly Property InputFunctionControl() As InputFunction.InputFunctionControl
        Get
            If _InputFunctionControl Is Nothing Then
                Dim InputValues As New List(Of Value)
                InputValues.Add(G_Additional)
                _InputFunctionControl = New InputFunction.InputFunctionControl(InputValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

#End Region

#Region "outputs"

    Public G As New ModelBase.Value("G", "Model_Topp: G", Value.ValueType.Output, "mg/dl")
    Public G_Additional As New ModelBase.Value("G_Additional", "Model_Topp: G_Additional", Value.ValueType.Output, "mg/dl")

    Public I As New ModelBase.Value("I", "Model_Topp: I", Value.ValueType.Output, "mU/ml")
    Public B_cell As New ModelBase.Value("B_cell", "Model_Topp: B_cell", Value.ValueType.Output, "mg")

#End Region

#Region "parameters"

    Public R0 As New ModelBase.Parameter("R0", "R0")
    Public Eg0 As New ModelBase.Parameter("Eg0", "Eg0")
    Public Si As New ModelBase.Parameter("Si", "Si")
    Public Sigma As New ModelBase.Parameter("Sigma", "Sigma")
    Public Alpha As New ModelBase.Parameter("Alpha", "Alpha")
    Public k As New ModelBase.Parameter("k", "k")
    Public D0 As New ModelBase.Parameter("D0", "D0")
    Public R1 As New ModelBase.Parameter("R1", "R1")
    Public R2 As New ModelBase.Parameter("R2", "R2")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_Topp"
        Me.DisplayName = "Model of Topp (Insuline - Glucose & beta-cell mass)"
        Me.Description = "Model of Topp (Insuline - Glucose & beta-cell mass)"

        G.InitValue = 100
        I.InitValue = 0
        B_cell.InitValue = 300

        Si.Value = 0.72
        Eg0.Value = 1.44
        R0.Value = 864
        Sigma.Value = 43.2
        Alpha.Value = 20000
        k.Value = 432
        D0.Value = 0.06
        R1.Value = 0.00084
        R2.Value = 0.0000024

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
        SetInputValues()

        'calculating G
        Dim dG As Double
        dG = R0.Value - (Eg0.Value + Si.Value * I.Value(CurrentStep - 1)) * G.Value(CurrentStep - 1) + _
                         G_Additional.Value(CurrentStep - 1)
        G.Value(CurrentStep) = G.Value(CurrentStep - 1) + [Step] * dG

        'calculating I
        Dim dI As Double
        dI = B_cell.Value(CurrentStep - 1) * Sigma.Value * G.Value(CurrentStep - 1) * G.Value(CurrentStep - 1) / _
            (Alpha.Value + G.Value(CurrentStep - 1) * G.Value(CurrentStep - 1)) _
            - k.Value * I.Value(CurrentStep - 1)
        I.Value(CurrentStep) = I.Value(CurrentStep - 1) + [Step] * dI

        'calculating B_cell
        Dim dB_cell As Double
        dB_cell = (-D0.Value + R1.Value * G.Value(CurrentStep - 1) - R2.Value * G.Value(CurrentStep - 1) * G.Value(CurrentStep - 1)) * B_cell.Value(CurrentStep - 1)
        B_cell.Value(CurrentStep) = B_cell.Value(CurrentStep - 1) + [Step] * dB_cell

    End Sub

    Private Sub SetInputValues()
        G_Additional.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(G_Additional, CurrentTime)
    End Sub

#End Region

#Region "UI Methods"

    Public Overrides Function GetControl() As System.Windows.Forms.UserControl
        Return InputFunctionControl
    End Function

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save input
        InputFunctionControl.InputFunctionsList.ToXml(CurrentElement, INPUT_XML_ELEMENT)

        Return CurrentElement
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        MyBase.FromXml(currentElement)

        'load input
        Dim InputElement As System.Xml.XmlElement
        InputElement = currentElement.Item(INPUT_XML_ELEMENT)
        InputFunctionControl.InputFunctionsList.FromXml(InputElement, INPUT_XML_ELEMENT)

        Return Me
    End Function

#End Region

End Class
