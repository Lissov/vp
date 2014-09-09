Imports ModelBase

Public Class EnergyModel_New
    Inherits ModelBase.ModelBase

#Region "Const"

    Public Const INPUT_XML_ELEMENT As String = "Input"

#End Region

#Region "Input function"

    Public Overrides Function GetInputValues() As List(Of Value)
        Dim InputValues As New List(Of Value)

        InputValues.Add(Ne)
        InputValues.Add(Su)

        Return InputValues
    End Function

    Private _InputFunctionControl As InputFunction.InputFunctionControl
    Public ReadOnly Property InputFunctionControl() As InputFunction.InputFunctionControl
        Get
            If _InputFunctionControl Is Nothing Then
                _InputFunctionControl = New InputFunction.InputFunctionControl(GetInputValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

    Public Overrides Function GetControl() As System.Windows.Forms.UserControl
        Return InputFunctionControl
    End Function

#End Region

#Region "Variables"

    Public Ne As New Value("Ne", "Ne", Value.ValueType.Output) 'real input
    Public Su As New Value("Su", "Su", Value.ValueType.Output) 'real input

    Public C2 As New Value("C2", "C2", Value.ValueType.Output)
    Public C3 As New Value("C3", "C3", Value.ValueType.Output)
    Public C23 As New Value("C23", "C23", Value.ValueType.Output)

    Public Vs As New Value("Vs", "Vs", Value.ValueType.Output)
    Public Vs1 As New Value("Vs1", "Vs1", Value.ValueType.Output)
    Public Vs2 As New Value("Vs2", "Vs2", Value.ValueType.Output)
    Public Vs3 As New Value("Vs3", "Vs3", Value.ValueType.Output)
    Public Vc As New Value("Vc", "Vc", Value.ValueType.Output)

    Public Psi As New Value("Psi", "Psi", Value.ValueType.Output)
    Public Sm As New Value("Sm", "Sm", Value.ValueType.Output)
    Public B As New Value("B", "B", Value.ValueType.Output)
    Public O2 As New Value("O2", "O2", Value.ValueType.Output)
    Public Vcm As New Value("Vcm", "Vcm", Value.ValueType.Output)

#End Region

#Region "Parameters"

    Public C2min As New ModelBase.Parameter("C2min", "C2min")
    Public C2max As New ModelBase.Parameter("C2max", "C2max")
    Public C3min As New ModelBase.Parameter("C3min", "C3min")
    Public C3max As New ModelBase.Parameter("C3max", "C3max")
    Public C23min As New ModelBase.Parameter("C23min", "C23min")
    Public C23max As New ModelBase.Parameter("C23max", "C23max")

    Public A As New ModelBase.Parameter("A", "A")
    Public a1 As New ModelBase.Parameter("a1", "a1")
    Public a2 As New ModelBase.Parameter("a2", "a2")
    Public a3 As New ModelBase.Parameter("a3", "a3")
    Public a4 As New ModelBase.Parameter("a4", "a4")
    Public a5 As New ModelBase.Parameter("a5", "a5")

    Public Psi0 As New ModelBase.Parameter("Psi0", "Psi0")

    Public b0 As New ModelBase.Parameter("b0", "b0")
    Public b1 As New ModelBase.Parameter("b1", "b1")
    Public b2 As New ModelBase.Parameter("b2", "b2")

    Public k As New ModelBase.Parameter("k", "k")

    Public Hi1 As New ModelBase.Parameter("Hi1", "Hi1")
    Public Hi2 As New ModelBase.Parameter("Hi2", "Hi2")

    Public gamma As New ModelBase.Parameter("gamma", "gamma")

    Public Sm_max As New ModelBase.Parameter("Sm_max", "Sm_max")
    Public Sm_min As New ModelBase.Parameter("Sm_min", "Sm_min")
    Public kSm1 As New ModelBase.Parameter("kSm1", "kSm1")
    Public kSm2 As New ModelBase.Parameter("kSm2", "kSm2")

    Public sigma As New ModelBase.Parameter("sigma", "sigma")

    Public Pmo2 As New ModelBase.Parameter("Pmo2", "Pmo2")
    Public Rmp As New ModelBase.Parameter("Rmp", "Rmp")
    Public Pcap As New ModelBase.Parameter("Pcap", "Pcap")
    Public Rcc As New ModelBase.Parameter("Rcc", "Rcc")

    Public Pco2 As New ModelBase.Parameter("Pco2", "Pco2")

    Public Psi_0 As New ModelBase.Parameter("Psi_0", "Psi_0")
    Public Psi_th As New ModelBase.Parameter("Psi_th", "Psi_th")
    Public Sm_th As New ModelBase.Parameter("Sm_th", "Sm_th")
    Public Ne_th As New ModelBase.Parameter("Ne_th", "Ne_th")
    Public k0 As New ModelBase.Parameter("k0", "k0")
    Public k1 As New ModelBase.Parameter("k1", "k1")
    Public k2 As New ModelBase.Parameter("k2", "k2")
    Public k3 As New ModelBase.Parameter("k3", "k3")

    Public d0 As New ModelBase.Parameter("d0", "d0")
    Public d1 As New ModelBase.Parameter("d1", "d1")
    Public d2 As New ModelBase.Parameter("d2", "d2")

    Public e1 As New ModelBase.Parameter("e1", "e1")
    Public e2 As New ModelBase.Parameter("e2", "e2")
    Public e3 As New ModelBase.Parameter("e3", "e3")


#End Region

    Public Sub New()
        MyBase.New()

        Me.Name = "EnergyModel_New"
        Me.DisplayName = "EnergyModel_New"
        Me.Description = "EnergyModel_New"

        Me.ShowControlAlways = True

        Psi.MinValue = 0
        B.MinValue = 0
        O2.MinValue = 0
    End Sub


#Region "Calculate"

    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        C2.MinValue = C2min.Value
        C2.MaxValue = C2max.Value

        C3.MinValue = C3min.Value
        C3.MaxValue = C3max.Value

        Sm.MinValue = Sm_min.Value
        Sm.MaxValue = Sm_max.Value

        B.MinValue = 0
        O2.MinValue = 0

        Psi.MinValue = 0
    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        Ne.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Ne, CurrentTime)
        Su.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Su, CurrentTime)

        Vc.Value(CurrentStep) = a1.Value * Ne.Value(CurrentStep - 1)

        If C3min.Value <= C3.Value(CurrentStep - 1) AndAlso C3.Value(CurrentStep - 1) <= C3max.Value Then
            C3.Value(CurrentStep) = C3.Value(CurrentStep - 1) + [Step] * (a2.Value * (Vs.Value(CurrentStep - 1) - Vc.Value(CurrentStep - 1)))
        Else
            C3.Value(CurrentStep) = C3.Value(CurrentStep - 1)
        End If
        C3.Value(CurrentStep) = C3.FixValue(C3.Value(CurrentStep))

        If C2min.Value <= C2.Value(CurrentStep - 1) AndAlso C2.Value(CurrentStep - 1) <= C2max.Value Then
            C2.Value(CurrentStep) = C2.Value(CurrentStep - 1) + [Step] * (a3.Value * (Vc.Value(CurrentStep - 1)))
            'C2.Value(CurrentStep) = C2.Value(CurrentStep - 1) + [Step] * (a3.Value * (Vc.Value(CurrentStep - 1) - Vs.Value(CurrentStep - 1)))
        Else
            C2.Value(CurrentStep) = C2.Value(CurrentStep - 1)
        End If
        C2.Value(CurrentStep) = C2.FixValue(C2.Value(CurrentStep))

        C23.Value(CurrentStep) = C2.Value(CurrentStep - 1) / (A.Value + C3.Value(CurrentStep - 1))
        If A.Value + C3max.Value <> 0 Then
            C23min.Value = C2min.Value / (A.Value + C3max.Value)
        Else
            C23min.Value = 0
        End If
        If A.Value + C3min.Value <> 0 Then
            C23max.Value = C2max.Value / (A.Value + C3min.Value)
        Else
            C23max.Value = 0
        End If

        If C23min.Value <= C23.Value(CurrentStep - 1) AndAlso C23.Value(CurrentStep - 1) <= C23max.Value Then
            Vs1.Value(CurrentStep) = (a4.Value * C23.Value(CurrentStep - 1))
        Else
            Vs1.Value(CurrentStep) = Vs1.Value(CurrentStep - 1)
        End If

        If True Then
            Vs2.Value(CurrentStep) = a5.Value * Sm.Value(CurrentStep - 1)
        Else
            Vs2.Value(CurrentStep) = Vs2.Value(CurrentStep - 1)
        End If

        If False Then
            Vs3.Value(CurrentStep) = Vs3.Value(CurrentStep - 1) + [Step] * (-k.Value * (B.Value(CurrentStep - 1) - b0.Value))
        Else
            Vs3.Value(CurrentStep) = Vs3.Value(CurrentStep - 1)
        End If

        Vs.Value(CurrentStep) = Vs1.Value(CurrentStep) + Vs2.Value(CurrentStep) + Vs3.Value(CurrentStep)

        'If C23.Value(CurrentStep - 1) > Hi1.Value Then
        '    Psi.Value(CurrentStep) = Psi.Value(CurrentStep - 1) + [Step] * (b1.Value - gamma.Value * Psi.Value(CurrentStep - 1))
        'Else
        '    Psi.Value(CurrentStep) = Psi.Value(CurrentStep - 1) + [Step] * (-gamma.Value * Psi.Value(CurrentStep - 1))
        'End If
        'dSM = k1.Value * (Sm_th.Value - Sm.Value(CurrentStep - 1)) + _
        '      k2.Value * (Psi.Value(CurrentStep - 1) - Psi_th.Value) - _
        '      k3.Value * (Ne.Value(CurrentStep - 1) - Ne_th.Value)
        Dim dPsi As Double
        If Su.Value(CurrentStep - 1) = 0 Then
            dPsi = 0
        Else
            dPsi = e1.Value * (Ne.Value(CurrentStep - 1) / Su.Value(CurrentStep - 1)) - _
               e2.Value * C3.Value(CurrentStep - 1) - _
               e3.Value * Sm.Value(CurrentStep - 1)
        End If
        Psi.Value(CurrentStep) = Psi.Value(CurrentStep - 1) + [Step] * dPsi
        Psi.Value(CurrentStep) = Psi.FixValue(Psi.Value(CurrentStep))


        If O2.Value(CurrentStep - 1) > Hi2.Value Then
            B.Value(CurrentStep) = B.Value(CurrentStep - 1) + [Step] * (b2.Value * C23.Value(CurrentStep - 1) - B.Value(CurrentStep - 1))
        Else
            B.Value(CurrentStep) = B.Value(CurrentStep - 1)
        End If
        B.Value(CurrentStep) = B.FixValue(B.Value(CurrentStep))

        'If (Sm_max.Value > Sm.Value(CurrentStep - 1)) AndAlso (Psi.Value(CurrentStep - 1) > 0) Then
        '    Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1) + [Step] * (kSm1.Value * (Sm.Value(CurrentStep - 1) - Sm_min.Value) * Psi.Value(CurrentStep - 1))
        'ElseIf (Sm_min.Value < Sm.Value(CurrentStep - 1)) AndAlso (Psi.Value(CurrentStep - 1) = 0) Then
        '    Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1) + [Step] * (kSm2.Value * (Sm_max.Value - Sm.Value(CurrentStep - 1)))
        'Else
        '    Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1)
        'End If
        Dim dSM As Double
        'dSM = k1.Value * (Sm_th.Value - Sm.Value(CurrentStep - 1)) + _
        '      k2.Value * (Psi.Value(CurrentStep - 1) - Psi_th.Value) - _
        '      k3.Value * (Ne.Value(CurrentStep - 1) - Ne_th.Value)
        dSM = d1.Value * ((Psi.Value(CurrentStep - 1) - Psi_0.Value)) - _
              d2.Value * Sm.Value(CurrentStep - 1)
        dSM = d0.Value * dSM
        'Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1) + [Step] * dSM * k0.Value
        Sm.Value(CurrentStep) = Sm.Value(CurrentStep - 1) + [Step] * dSM
        Sm.Value(CurrentStep) = Sm.FixValue(Sm.Value(CurrentStep))

        O2.Value(CurrentStep) = O2.Value(CurrentStep - 1) + [Step] * (sigma.Value * (Vcm.Value(CurrentStep - 1) - Vs.Value(CurrentStep - 1)))
        O2.Value(CurrentStep) = O2.FixValue(O2.Value(CurrentStep))

        If Rmp.Value <> 0 Then
            Vcm.Value(CurrentStep) = (Pco2.Value - Pmo2.Value) / Rmp.Value
        Else
            Vcm.Value(CurrentStep) = 0
        End If

        'If Rcc.Value <> 0 Then
        '    Pco2.Value(CurrentStep) = (Pcap.Value - Pco2.Value(CurrentStep - 1)) / Rcc.Value
        'Else
        '    Pco2.Value(CurrentStep) = 0
        'End If
    End Sub

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
