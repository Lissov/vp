Imports ModelBase
Imports Functions.MathFunctions

Public Class Model_InsulineGlucose
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
                'InputValues.Add(I)
                'InputValues.Add(Vgc)
                'InputValues.Add(Vg_out)
                InputValues.Add(Vgif)
                InputValues.Add(A)
                _InputFunctionControl = New InputFunction.InputFunctionControl(InputValues)
            End If
            Return _InputFunctionControl
        End Get
    End Property

#End Region

#Region "inputs"



#End Region

#Region "outputs"

    Public G As New ModelBase.Value("G", "Model_InsulineGlucose: G", Value.ValueType.Output, "ללמכü/כ")
    Public DeltaG As New ModelBase.Value("DeltaG", "Model_InsulineGlucose: DeltaG", Value.ValueType.Output)
    Public Gl As New ModelBase.Value("Gl", "Model_InsulineGlucose: Gl", Value.ValueType.Output, "ללמכü/כ")
    Public I As New ModelBase.Value("I", "Model_InsulineGlucose: I", Value.ValueType.Output)
    Public X As New ModelBase.Value("X", "Model_InsulineGlucose: X", Value.ValueType.Output)

    ' Public Vgi As New ModelBase.Value("Vgi", "Model_InsulineGlucose: Vgi", Value.ValueType.Output)
    ' Public Vgc As New ModelBase.Value("Vgc", "Model_InsulineGlucose: Vgc", Value.ValueType.Output)
    Public Vg_out As New ModelBase.Value("Vg_out", "Model_InsulineGlucose: Vg_out", Value.ValueType.Output)

    Public Vig_e As New ModelBase.Value("Vig_e", "Model_InsulineGlucose: Vig_e", Value.ValueType.Output)

    Public Vgif As New ModelBase.Value("Vgif", "Model_InsulineGlucose: Vgif", Value.ValueType.Output)
    Public Vgl_g As New ModelBase.Value("Vgl_g", "Model_InsulineGlucose: Vgl_g", Value.ValueType.Output)
    Public Vg_gl As New ModelBase.Value("Vg_gl", "Model_InsulineGlucose: Vg_gl", Value.ValueType.Output)
    Public Vg_gl_2 As New ModelBase.Value("Vg_gl_2", "Model_InsulineGlucose: Vg_gl_2", Value.ValueType.Output)
    Public A As New ModelBase.Value("A", "Model_InsulineGlucose: A", Value.ValueType.Output)

#End Region

#Region "parameters"

    Public Kg As New ModelBase.Parameter("Kg", "Kg")
    Public Kgl As New ModelBase.Parameter("Kgl", "Kgl")
    Public KVgi As New ModelBase.Parameter("KVgi", "KVgi")
    Public KVgig As New ModelBase.Parameter("KVgig", "KVgig")
    Public KVg_gl As New ModelBase.Parameter("KVg_gl", "KVg_gl")
    Public KVg_gl_1 As New ModelBase.Parameter("KVg_gl_1", "KVg_gl_1")
    Public KVg_gl_2 As New ModelBase.Parameter("KVg_gl_2", "KVg_gl_2")
    Public KVgl_g As New ModelBase.Parameter("KVgl_g", "KVgl_g")
    Public KVgl_g_1 As New ModelBase.Parameter("KVgl_g_1", "KVgl_g_1")
    Public Kig As New ModelBase.Parameter("Kig", "Kig")
    Public Ki As New ModelBase.Parameter("Ki", "Ki")
    Public Ki1 As New ModelBase.Parameter("Ki_1", "Ki_1")
    Public Ki2 As New ModelBase.Parameter("Ki_2", "Ki_2")
    Public Ki3 As New ModelBase.Parameter("Ki_3", "Ki_3")

    Public G_Cr As New ModelBase.Parameter("G_Cr", "G_Cr (for glycogen)")
    Public G_Cr_Out As New ModelBase.Parameter("G_Cr_Out", "G_Cr_Out")
    Public Gl_Norm As New ModelBase.Parameter("Gl_Norm", "Gl_Norm")

    Public KDeltaG As New ModelBase.Parameter("KDeltaG", "KDeltaG")
    Public KVg_out As New ModelBase.Parameter("KVg_out", "KVg_out")

    Public KVig_e As New ModelBase.Parameter("KVig_e", "KVig_e")
    Public KVig_e_part1 As New ModelBase.Parameter("KVig_e_part1", "KVig_e_part1")
    Public KVig_e_part2 As New ModelBase.Parameter("KVig_e_part2", "KVig_e_part2")
    Public KVig_e_1 As New ModelBase.Parameter("KVig_e_1", "KVig_e_1")
    Public KVig_e_2 As New ModelBase.Parameter("KVig_e_2", "KVig_e_2")
    Public KVig_e_3 As New ModelBase.Parameter("KVig_e_3", "KVig_e_3")

    Public Kx1 As New ModelBase.Parameter("Kx_1", "Kx_1")
    Public Kx2 As New ModelBase.Parameter("Kx_2", "Kx_2")

    Public KVg_gl2_1 As New ModelBase.Parameter("KVg_gl2_1", "KVg_gl2_1")

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_InsulineGlucose"
        Me.DisplayName = "Insuline - Glucose"
        Me.Description = "Insuline - Glucose part of CVS"

        G.InitValue = 100

        Gl.MinValue = 170
        Gl.MaxValue = 350
        Gl.InitValue = 230

        X.MinValue = 0
    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        G.MinValue = 3
        G.MaxValue = 300
        I.MinValue = 0.005
        I.MaxValue = 120
        Gl.MinValue = 170
        Gl.MaxValue = 350

        Vg_out.MinValue = 0
        Vgif.MinValue = 0
        Vgl_g.MinValue = 0
        Vg_gl.MinValue = 0
    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        SetInputValues()

        'calculating G
        Dim dG As Double
        dG = Vgif.Value(CurrentStep - 1) + _
             Vgl_g.Value(CurrentStep - 1) - _
             Vg_gl.Value(CurrentStep - 1) - _
             Vg_out.Value(CurrentStep - 1) - _
             Vig_e.Value(CurrentStep - 1)
        G.Value(CurrentStep) = G.Value(CurrentStep - 1) + [Step] * dG * Kg.Value
        G.Value(CurrentStep) = G.FixValue(G.Value(CurrentStep))

        'calculating Vig_e (v to produce energy)
        Dim MMG As Double = G.Value(CurrentStep - 1) / (KVig_e_1.Value * G.Value(CurrentStep - 1) + 1)
        Dim MMI As Double = I.Value(CurrentStep - 1) / (KVig_e_2.Value * I.Value(CurrentStep - 1) + 1)
        Dim MMA As Double = A.Value(CurrentStep - 1) '/ (KVig_e_3.Value * A.Value(CurrentStep - 1) + 1)
        Vig_e.Value(CurrentStep) = KVig_e.Value * (KVig_e_part1.Value * MMG * MMI * MMA + KVig_e_part2.Value * MMG * MMA)

        'calculate DeltaG
        'DeltaG.Value(CurrentStep) = G.Value(CurrentStep - 1) - A.Value(CurrentStep - 1) * KDeltaG.Value
        DeltaG.Value(CurrentStep) = 0

        'calculate Vg_out
        If G.Value(CurrentStep) > G_Cr_Out.Value Then
            Vg_out.Value(CurrentStep) = KVg_out.Value '* DeltaG.Value(CurrentStep)
            Vg_out.Value(CurrentStep) = Vg_out.FixValue(Vg_out.Value(CurrentStep))

        Else
            Vg_out.Value(CurrentStep) = 0
        End If

        'calculating X (buffer for conversion from G to glikogen)
        Dim dX As Double
        If G.Value(CurrentStep) > G_Cr.Value Then
            dX = kx1.Value  * (G.Value(CurrentStep) - G_Cr.Value)
        Else
            dX = Kx2.Value * (G.Value(CurrentStep) - G_Cr.Value)
        End If
        X.Value(CurrentStep) = X.Value(CurrentStep - 1) + [Step] * dX
        X.Value(CurrentStep) = X.FixValue(X.Value(CurrentStep))

        'calculating Vg_gl (v from G to glikogen)
        If G.Value(CurrentStep) > G_Cr.Value AndAlso Gl.Value(CurrentStep - 1) < Gl.MaxValue Then
            Vg_gl.Value(CurrentStep) = (I.Value(CurrentStep - 1) * G.Value(CurrentStep - 1) * Kig.Value - _
                                      Gl.Value(CurrentStep - 1) * KVg_gl_1.Value) * KVg_gl_2.Value
            Vg_gl.Value(CurrentStep) = Vg_gl.FixValue(Vg_gl.Value(CurrentStep))

        Else
            Vg_gl.Value(CurrentStep) = 0
        End If

        'calculating Vg_gl (v from X to glikogen)
        Vg_gl_2.Value(CurrentStep) = KVg_gl2_1.Value * X.Value(CurrentStep)

        'calculating Vgl_g (v from glikogen to G)
        If G.Value(CurrentStep) < G_Cr.Value AndAlso Gl.Value(CurrentStep - 1) > Gl.MinValue Then
            Vgl_g.Value(CurrentStep) = -(G.Value(CurrentStep) - G_Cr.Value) * Gl.Value(CurrentStep - 1) * KVgl_g_1.Value
            Vgl_g.Value(CurrentStep) = Vgl_g.FixValue(Vgl_g.Value(CurrentStep))
        Else
            Vgl_g.Value(CurrentStep) = 0
        End If

        'calculating Gl
        Dim dGl As Double
        dGl = Vg_gl.Value(CurrentStep - 1) + Vg_gl_2.Value(CurrentStep) - Vgl_g.Value(CurrentStep - 1)
        Gl.Value(CurrentStep) = Gl.Value(CurrentStep - 1) + [Step] * dGl * Kgl.Value
        Gl.Value(CurrentStep) = Gl.FixValue(Gl.Value(CurrentStep))


        'calculating I
        Dim dI As Double
        'dI = G.Value(CurrentStep - 1) * Ki1.Value - I.Value(CurrentStep - 1) * Ki2.Value
        'ki1 =0,1 ki2=0,2
        'dI = G.Value(CurrentStep - 1) * Ki1.Value - Vig_e.Value(CurrentStep - 1) * Ki2.Value
        Dim G2 As Double = G.Value(CurrentStep - 1) * G.Value(CurrentStep - 1)
        dI = Ki3.Value * (G2 / (G2 + Ki1.Value)) - I.Value(CurrentStep - 1) * Ki2.Value

        I.Value(CurrentStep) = I.Value(CurrentStep - 1) + [Step] * dI * Ki.Value
        I.Value(CurrentStep) = I.FixValue(I.Value(CurrentStep))
        If I.Value(CurrentStep) < 2 Then
            I.Value(CurrentStep) = I.Value(CurrentStep)
        End If

    End Sub

    Private Sub SetInputValues()
        'I.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(I, CurrentTime)
        'Vgc.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vgc, CurrentTime)
        'Vg_out.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vg_out, CurrentTime)
        Vgif.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(Vgif, CurrentTime)
        A.Value(CurrentStep) = InputFunctionControl.GetCalculatedValue(A, CurrentTime)
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
