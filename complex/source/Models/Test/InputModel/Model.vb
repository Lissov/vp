Imports ModelBase

Public Class InputModel
    Inherits ModelBase.ModelBase

#Region "Const"

    Public Const INPUT_XML_ELEMENT As String = "Input"

#End Region

    Public X1 As New Value("X1", "X1", Value.ValueType.Output, "kg")
    Public X2 As New Value("X2", "X2", Value.ValueType.Output, "m")
    Public X3 As New Value("X3", "X3", Value.ValueType.Output)
    Public X4 As New Value("X4", "X4", Value.ValueType.Output)
    Public aaaaaa As New Value("aaaaa", "aaaaa", Value.ValueType.Output)

    Public K1 As New ModelBase.Parameter("k1", "k1")
    Public K2 As New ModelBase.Parameter("k2", "k2")
    Public K3 As New ModelBase.Parameter("k3", "k3")
    Public K4 As New ModelBase.Parameter("k4", "k4")

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

        Me.Name = "TestInputModel"
        Me.DisplayName = "Input Model - test"
        Me.Description = "Model to test input functions"

        Me.Step = 0.2

        Me.ShowControlAlways = True

        X1.InitValueVisible = False
        X2.GroupName = "bugaga"
        X3.GroupName = "bugaga"
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        X1.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(X1, CurrentTime)
        X2.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(X2, CurrentTime)
        X3.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(X3, CurrentTime)
        X4.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(X4, CurrentTime)
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
