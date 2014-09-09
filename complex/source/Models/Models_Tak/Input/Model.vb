Imports ModelBase

Public Class Model
    Inherits ModelBase.ModelBase

#Region "Const"

    Public Const INPUT_XML_ELEMENT As String = "Input"

#End Region

    Public Dx As New ModelBase.Value("Dx", "Dx", Value.ValueType.Output)
    Public qm As New ModelBase.Value("qm", "qm", Value.ValueType.Output)
    Public Sa As New ModelBase.Value("Sa", "Sa", Value.ValueType.Output)
    Public CurrentT As New ModelBase.Value("CurrentT", "CurrentT", Value.ValueType.Output)
    Public Horm As New ModelBase.Value("Horm", "Horm", Value.ValueType.Output)
    Public Ed As New ModelBase.Value("Ed", "Ed", Value.ValueType.Output)
    Public Vh As New ModelBase.Value("Vh", "Vh", Value.ValueType.Output)
    Public Vm As New ModelBase.Value("Vm", "Vm", Value.ValueType.Output)
    Public Vt As New ModelBase.Value("Vt", "Vt", Value.ValueType.Output)
    Public Air_O2 As New ModelBase.Value("Air_O2", "Air_O2", Value.ValueType.Output)
    Public Air_CO2 As New ModelBase.Value("Air_CO2", "Air_CO2", Value.ValueType.Output)
    Public Q_Limf As New ModelBase.Value("Q_Limf", "Q_Limf", Value.ValueType.Output)


    Private _InputFunctionControl As InputFunction.InputFunctionControl
    Public ReadOnly Property InputFunctionControl() As InputFunction.InputFunctionControl
        Get
            If _InputFunctionControl Is Nothing Then
                _InputFunctionControl = New InputFunction.InputFunctionControl(GetValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

    Public Sub New()
        MyBase.New()

        Me.Name = "InputModel"
        Me.DisplayName = "Input"
        Me.Description = "Model for input functions"

        Me.Step = 0.01

        Me.ShowControlAlways = True
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        Dx.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Dx, CurrentTime)
        qm.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(qm, CurrentTime)
        Sa.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Sa, CurrentTime)
        CurrentT.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(CurrentT, CurrentTime)
        Horm.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Horm, CurrentTime)
        Ed.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Ed, CurrentTime)
        Vh.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vh, CurrentTime)
        Vm.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vm, CurrentTime)
        Vt.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vt, CurrentTime)
        Air_O2.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Air_O2, CurrentTime)
        Air_CO2.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Air_CO2, CurrentTime)
        Q_Limf.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Q_Limf, CurrentTime)
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
