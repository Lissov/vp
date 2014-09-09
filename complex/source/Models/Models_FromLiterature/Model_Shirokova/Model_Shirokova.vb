Imports ModelBase
Imports Functions.MathFunctions


Public Class Model_Shirokova
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
                InputValues.Add(S)
                _InputFunctionControl = New InputFunction.InputFunctionControl(InputValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

#End Region

#Region "outputs"

    Public G As New ModelBase.Value("G", "Model_Shirokova: G", Value.ValueType.Output, "ммоль/л")
    Public I As New ModelBase.Value("I", "Model_Shirokova: I", Value.ValueType.Output, "mU/ml")
    Public S As New ModelBase.Value("S", "Model_Shirokova: S", Value.ValueType.Output, "ммоль/л")

#End Region

#Region "parameters"

    Public G0 As New ModelBase.Parameter("G0", "G0")
    Public Gcr As New ModelBase.Parameter("Gcr", "Gcr")
    Public Alpha As New ModelBase.Parameter("Alpha", "Alpha")
    Public Beta As New ModelBase.Parameter("Beta", "Beta")
    Public Gamma As New ModelBase.Parameter("Gamma", "Gamma")
    Public Sigma As New ModelBase.Parameter("Sigma", "Sigma")
    Public Mu As New ModelBase.Parameter("Mu", "Mu")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_Shirokova"
        Me.DisplayName = "Model of Shirokova (Insuline - Glucose)"
        Me.Description = "Model of Shirokova (Insuline - Glucose)"

        G.InitValue = 7
        I.InitValue = 0


        G0.Value = 5.5
        Gcr.Value = 10
        Alpha.Value = 0.2
        Beta.Value = 2
        Gamma.Value = 6
        Sigma.Value = 44
        Mu.Value = 5

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
        Dim dGcr As Double = 0
        If G.Value(CurrentStep - 1) > Gcr.Value Then
            dGcr = G.Value(CurrentStep - 1) - Gcr.Value
        End If
        If G.Value(CurrentStep - 1) > G0.Value Then
            dG = Gamma.Value * (G.Value(CurrentStep - 1) - G0.Value) - _
                 Sigma.Value * G.Value(CurrentStep - 1) * I.Value(CurrentStep - 1) - _
                 Mu.Value * dGcr + _
                 S.Value(CurrentStep - 1)
        Else
            dG = -Sigma.Value * G.Value(CurrentStep - 1) * I.Value(CurrentStep - 1) - _
                 Mu.Value * dGcr + _
                 S.Value(CurrentStep - 1)
        End If
        G.Value(CurrentStep) = G.Value(CurrentStep - 1) + [Step] * dG

        'calculating I
        Dim dI As Double
        If G.Value(CurrentStep - 1) > G0.Value Then
            dG = Alpha.Value * (G.Value(CurrentStep - 1) - G0.Value) - _
                 Beta.Value * G.Value(CurrentStep - 1) * I.Value(CurrentStep - 1)
        Else
            dG = - Beta.Value * G.Value(CurrentStep - 1) * I.Value(CurrentStep - 1)
        End If
        I.Value(CurrentStep) = I.Value(CurrentStep - 1) + [Step] * dI

      
    End Sub

    Private Sub SetInputValues()
        S.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(S, CurrentTime)
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
