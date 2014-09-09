Imports ModelBase

Public Class EnergyBalanceModel
    Inherits ModelBase.ModelBase

#Region "Const"

    Public Const INPUT_XML_ELEMENT As String = "Input"

#End Region

    Public Vp As New Value("Vp", "Vp", Value.ValueType.Output)
    Public m As New Value("m", "m", Value.ValueType.Output)
    Public Sm As New Value("Sm", "Sm", Value.ValueType.Output)
    Public CD As New Value("CD", "CD", Value.ValueType.Output)
    Public CT As New Value("CT", "CT", Value.ValueType.Output)
    Public Vc As New Value("Vc", "Vc", Value.ValueType.Output)
    Public W As New Value("W", "W", Value.ValueType.Output)
    Public fh As New Value("fh", "fh", Value.ValueType.Output)
    Public T As New Value("T", "T", Value.ValueType.Output)
    Public Nb As New Value("Nb", "Nb", Value.ValueType.Output)
    Public Nbcc As New Value("Nbcc", "Nbcc", Value.ValueType.Output)
    Public NbM As New Value("NbM", "NbM", Value.ValueType.Output)
    Public I As New Value("I", "I", Value.ValueType.Output)
    Public QM As New Value("QM", "QM", Value.ValueType.Output)
    Public L1 As New Value("L1", "L1", Value.ValueType.Output)
    Public PA As New Value("PA", "PA", Value.ValueType.Output)
    Public O As New Value("O", "O", Value.ValueType.Output)
    Public Msm As New Value("Msm", "Msm", Value.ValueType.Output)
    Public r As New Value("r", "r", Value.ValueType.Output)
    Public Omax As New Value("Qmax", "Qmax", Value.ValueType.Output)
    Public Msmmax As New Value("Msmmax", "Msmmax", Value.ValueType.Output)
    Public A As New Value("A", "A", Value.ValueType.Output)

    Public alf1 As New ModelBase.Parameter("alf1", "alf1")
    Public a1 As New ModelBase.Parameter("a1", "a1")
    Public Tau As New ModelBase.Parameter("Tau", "Tau")
    Public alf2 As New ModelBase.Parameter("alf2", "alf2")
    Public Teta As New ModelBase.Parameter("Teta", "Teta")
    Public Bet1 As New ModelBase.Parameter("Bet1", "Bet1")
    Public Bet2 As New ModelBase.Parameter("Bet2", "Bet2")
    Public Gam1 As New ModelBase.Parameter("Gam1", "Gam1")
    Public Gam2 As New ModelBase.Parameter("Gam2", "Gam2")
    Public K1 As New ModelBase.Parameter("k1", "k1")
    Public K2 As New ModelBase.Parameter("k2", "k2")
    Public K3 As New ModelBase.Parameter("k3", "k3")
    Public T0 As New ModelBase.Parameter("T0", "T0")
    Public n01 As New ModelBase.Parameter("n01", "n01")
    Public n02 As New ModelBase.Parameter("n02", "n02")
    Public ksi As New ModelBase.Parameter("ksi", "ksi")
    Public d As New ModelBase.Parameter("d", "d")
    Public s As New ModelBase.Parameter("s", "s")
    Public L2 As New ModelBase.Parameter("L2", "L2")

    Private _InputFunctionControl As InputFunction.InputFunctionControl
    Public ReadOnly Property InputFunctionControl() As InputFunction.InputFunctionControl
        Get
            If _InputFunctionControl Is Nothing Then
                Dim InputValues As New List(Of Value)
                InputValues.Add(fh)
                InputValues.Add(T)
                InputValues.Add(I)
                InputValues.Add(QM)
                InputValues.Add(PA)
                InputValues.Add(O)
                InputValues.Add(Msm)
                InputValues.Add(r)
                InputValues.Add(Omax)
                InputValues.Add(Msmmax)
                _InputFunctionControl = New InputFunction.InputFunctionControl(InputValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

    Public Sub New()
        MyBase.New()

        Me.Name = "EnergyBalanceModel"
        Me.DisplayName = "EnergyBalanceModel"
        Me.Description = "EnergyBalanceModel - adapted version"

        Me.Step = 0.2

        Me.ShowControlAlways = True
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        CT.MinValue = 0
        CD.MinValue = 0
        A.MinValue = 0

    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        'input values
        fh.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(fh, CurrentTime)
        T.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(T, CurrentTime)
        I.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(I, CurrentTime)
        QM.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(QM, CurrentTime)
        PA.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(PA, CurrentTime)
        O.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(O, CurrentTime)
        Msm.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Msm, CurrentTime)
        r.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(r, CurrentTime)
        Omax.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Omax, CurrentTime)
        Msmmax.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Msmmax, CurrentTime)

        'other values
        Vp.Value(CurrentStep) = (m.Value(CurrentStep - 1) * Sm.Value(CurrentStep - 1) * alf1.Value * CD.Value(CurrentStep - 1)) / _
                                (a1.Value + CT.Value(CurrentStep - 1))

        If CurrentTime > Tau.Value Then
            m.Value(CurrentStep) = 1
        Else
            m.Value(CurrentStep) = 1 - Math.Exp(alf2.Value * (CurrentTime - Tau.Value))
        End If

        Vc.Value(CurrentStep) = Teta.Value * W.Value(CurrentStep - 1)

        CT.Value(CurrentStep) = Bet1.Value * Vp.Value(CurrentStep - 1) - Bet2.Value * Vc.Value(CurrentStep - 1)
        CT.Value(CurrentStep) = CT.FixValue(CT.Value(CurrentStep))

        CD.Value(CurrentStep) = Gam1.Value * Vc.Value(CurrentStep - 1) - Gam2.Value * Vp.Value(CurrentStep - 1)
        CD.Value(CurrentStep) = CD.FixValue(CD.Value(CurrentStep))

        W.Value(CurrentStep) = K1.Value * fh.Value(CurrentStep - 1) + K2.Value * (T.Value(CurrentStep - 1) - T0.Value) + _
                               K3.Value * Nb.Value(CurrentStep)

        Nb.Value(CurrentStep) = Nbcc.Value(CurrentStep - 1) * NbM.Value(CurrentStep - 1)

        Nbcc.Value(CurrentStep) = n01.Value * I.Value(CurrentStep - 1)

        NbM.Value(CurrentStep) = n02.Value * QM.Value(CurrentStep - 1)

        L1.Value(CurrentStep) = (ksi.Value) * PA.Value(CurrentStep - 1) * O.Value(CurrentStep - 1) * Msm.Value(CurrentStep - 1) / _
                                (r.Value(CurrentStep - 1) * Omax.Value(CurrentStep - 1) * Msmmax.Value(CurrentStep - 1))

        Dim dA As Double
        If CT.Value(CurrentStep - 1) > 0 Then
            dA = -d.Value
        Else
            dA = s.Value
        End If
        A.Value(CurrentStep) = A.Value(CurrentStep - 1) + [Step] * dA
        A.Value(CurrentStep) = A.FixValue(A.Value(CurrentStep))

        Dim dSm As Double
        If A.Value(CurrentStep - 1) > 0 Then
            dSm = L1.Value(CurrentStep - 1) * A.Value(CurrentStep - 1) - L2.Value
        Else
            dSm = -L2.Value
        End If
        Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1) + [Step] * dSm

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
