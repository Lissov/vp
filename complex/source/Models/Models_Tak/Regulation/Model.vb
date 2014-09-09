Imports ModelBase
Imports Functions
Imports Functions.MathFunctions
Imports System.Collections.Generic
Imports System.Reflection
Imports BetaSetup

Public Class Model
    Inherits ModelBase.ModelBase

#Region "inputs"

    Public Sa As New ModelBase.Value("Sa", "Regulation: Sa", Value.ValueType.Input)

    'qm=Q[8]=Qah
    Public qm As New ModelBase.Value("qm", "Regulation: qm", Value.ValueType.Input)

    Public CO2 As New ModelBase.Value("CO2", "Regulation: CO2", Value.ValueType.Input)
    Public Horm As New ModelBase.Value("Horm", "Regulation: Horm", Value.ValueType.Input)
    Public Pa As New ModelBase.Value("Pa", "Regulation: Pa", Value.ValueType.Input)
    Public Dx As New ModelBase.Value("Dx", "Regulation: Dx", Value.ValueType.Input)
    Public CurrentT As New ModelBase.Value("CurrentT", "Regulation: CurrentT", Value.ValueType.Input)

#End Region

#Region "outputs"

    Public df As New ModelBase.Value("df", "Regulation: df", Value.ValueType.Output)
    Public dU As New ModelBase.Value("dU", "Regulation: dU", Value.ValueType.Output)
    Public dTs As New ModelBase.Value("dTs", "Regulation: dTs", Value.ValueType.Output)
    Public dKr As New ModelBase.Value("dKr", "Regulation: dKr", Value.ValueType.Output)

    Public DeltaDx As New ModelBase.Value("DeltaDx", "Regulation: DeltaDx", Value.ValueType.Output)
    Public DeltaDb As New ModelBase.Value("DeltaDb", "Regulation: DeltaDb", Value.ValueType.Output)
    Public DeltaAh As New ModelBase.Value("DeltaAh", "Regulation: DeltaAh", Value.ValueType.Output)
    Public DeltaAx As New ModelBase.Value("DeltaAx", "Regulation: DeltaAx", Value.ValueType.Output)
    Public DeltaAib As New ModelBase.Value("DeltaAib", "Regulation: DeltaAib", Value.ValueType.Output)
    Public DeltaS As New ModelBase.Value("DeltaS", "Regulation: DeltaS", Value.ValueType.Output)
    Public DeltaFt As New ModelBase.Value("DeltaFt", "Regulation: DeltaFt", Value.ValueType.Output)
    Public DeltaD As New ModelBase.Value("DeltaD", "Regulation: DeltaD", Value.ValueType.Output, 0, 60)
    Public DeltaA As New ModelBase.Value("DeltaA", "Regulation: DeltaA", Value.ValueType.Output, 0, 250)

#End Region

#Region "Parameters"

    Public Sa_Th As New ModelBase.Parameter("Sa_Th", "Sa_Th")
    Public Sa_k1 As New ModelBase.Parameter("Sa_k1", "Sa_k1")
    Public qm_Th As New ModelBase.Parameter("qm_Th", "qm_Th")
    Public Aib_k1 As New ModelBase.Parameter("Aib_k1", "Aib_k1")
    Public Ax_k1 As New ModelBase.Parameter("Ax_k1", "Ax_k1")
    Public Ah_k1 As New ModelBase.Parameter("Ah_k1", "Ah_k1")
    Public Db_k1 As New ModelBase.Parameter("Db_k1", "Db_k1")
    Public Dx_k1 As New ModelBase.Parameter("Dx_k1", "Dx_k1")
    Public CurrentT_Th As New ModelBase.Parameter("CurrentT_Th", "CurrentT_Th")
    Public CurrentT_k1 As New ModelBase.Parameter("CurrentT_k1", "CurrentT_k1")
    Public dU_k1 As New ModelBase.Parameter("dU_k1", "dU_k1")
    Public dU_k2 As New ModelBase.Parameter("dU_k2", "dU_k2")
    Public dTs_k1 As New ModelBase.Parameter("dTs_k1", "dTs_k1")
    Public dTs_k2 As New ModelBase.Parameter("dTs_k2", "dTs_k2")
    Public dKr_k1 As New ModelBase.Parameter("dKr_k1", "dKr_k1")
    Public dKr_k2 As New ModelBase.Parameter("dKr_k2", "dKr_k2")


    Public AlphaH As New Parameter("AlphaH", "AlphaH", 0, 100)
    Public BetaH As New Parameter("BetaH", "BetaH", 0, 100)
    Public ThH As New Parameter("ThH", "ThH", 0, 1)

    Public AlphaCO2 As New Parameter("AlphaCO2", "AlphaCO2", 0, 1)
    Public BetaCO2 As New Parameter("BetaCO2", "BetaCO2", 0, 100)
    Public ThCO2 As New Parameter("ThCO2", "ThCO2", 0, 1)

    Public AlphaPa As New Parameter("AlphaPa", "AlphaPa", 0, 100)
    Public BetaPa As New Parameter("BetaPa", "BetaPa", 0, 100)
    Public ThPa As New Parameter("ThPa", "ThPa", 0, 100)

    Public AlphaDx As New Parameter("AlphaDx", "AlphaDx", 0, 100)
    Public BetaDx As New Parameter("BetaDx", "BetaDx", 0, 100)
    Public ThDx As New Parameter("ThDx", "ThDx", 0, 1)

#End Region

#Region "regulators"

    Public EnableAib As New Regulator("EnableAib", "Enable Aib", True)
    Public EnableAx As New Regulator("EnableAx", "Enable Ax", True)
    Public EnableAh As New Regulator("EnableAh", "Enable Ah", False)
    Public EnableDb As New Regulator("EnableDb", "Enable Db", True)
    Public EnableDx As New Regulator("EnableDx", "Enable Dx", False)
    Public EnableSa As New Regulator("EnableSa", "Enable Sa", False)
    Public EnableFt As New Regulator("EnableFt", "Enable Ft", False)

    Public Enable_dU As New Regulator("Enable_dU", "Enable dU", True)
    Public Enable_dTs As New Regulator("Enable_dTs", "Enable dTs", True)
    Public Enable_dKr As New Regulator("Enable_dKr", "Enable dKr", True)

#End Region

#Region "Properties"

    Private _Regulators As List(Of Regulator) = Nothing
    Public ReadOnly Property Regulators() As List(Of Regulator)
        Get
            If _Regulators Is Nothing Then
                _Regulators = New List(Of Regulator)
                CollectRegulators()
            End If
            Return _Regulators
        End Get
    End Property

    Private _HormSetupControl As BetaSetupControl
    Public ReadOnly Property HormSetupControl() As BetaSetupControl
        Get
            If _HormSetupControl Is Nothing Then
                _HormSetupControl = New BetaSetupControl(AlphaH, BetaH, ThH, 0, 200)
            End If
            Return _HormSetupControl
        End Get
    End Property

    Private _HifSetupControl As BetaSetupControl
    Public ReadOnly Property HifSetupControl() As BetaSetupControl
        Get
            If _HifSetupControl Is Nothing Then
                _HifSetupControl = New BetaSetupControl(AlphaCO2, BetaCO2, ThCO2, 0, 200)
            End If
            Return _HifSetupControl
        End Get
    End Property

    Private _BSetupControl As BetaSetupControl
    Public ReadOnly Property BSetupControl() As BetaSetupControl
        Get
            If _BSetupControl Is Nothing Then
                _BSetupControl = New BetaSetupControl(AlphaPa, BetaPa, ThPa, 0, 200)
            End If
            Return _BSetupControl
        End Get
    End Property

    Private _DxSetupControl As BetaSetupControl
    Public ReadOnly Property DxSetupControl() As BetaSetupControl
        Get
            If _DxSetupControl Is Nothing Then
                _DxSetupControl = New BetaSetupControl(AlphaDx, BetaDx, ThDx, 0, 200)
            End If
            Return _DxSetupControl
        End Get
    End Property

    Private _ModelControl As ModelControl
    Public ReadOnly Property ModelControl() As ModelControl
        Get
            If _ModelControl Is Nothing Then
                _ModelControl = New ModelControl(Regulators, _
                                                 HormSetupControl, _
                                                 HifSetupControl, _
                                                 BSetupControl, _
                                                 DxSetupControl)
            End If
            Return _ModelControl
        End Get
    End Property

#End Region

#Region "Private methods"

    Private Sub CollectRegulators()

        Dim RegulatorType As System.Type = GetType(Regulator)

        Dim Regulator As Regulator

        For Each ModelField As FieldInfo In Me.GetType.GetFields
            If ModelField.FieldType.Equals(RegulatorType) OrElse _
               ModelField.FieldType.IsSubclassOf(RegulatorType) _
               Then
                Regulator = GetRegulator(ModelField.Name)
                If Regulator IsNot Nothing Then
                    Regulators.Add(Regulator)
                End If
            End If
        Next

    End Sub

    Private Function GetRegulator(ByVal regulatorName As String) As Regulator
        Dim Regulator As Regulator = Nothing

        Dim RegulatorObject As Object = Nothing
        Try
            RegulatorObject = Me.GetType.InvokeMember(regulatorName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If RegulatorObject IsNot Nothing AndAlso TypeOf RegulatorObject Is Regulator Then
            Regulator = DirectCast(RegulatorObject, Regulator)
        End If

        Return Regulator
    End Function

#End Region

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        Me.Name = "Model_Regulation"
        Me.DisplayName = "Regulation"
        Me.Description = "Regulation for all parts of CVS"

        Sa_Th.Value = 0.5
        Sa_k1.Value = 40
        qm_Th.Value = 14.925373134
        Aib_k1.Value = 40
        Ax_k1.Value = 30.303030303
        Ah_k1.Value = 30.303030303
        Db_k1.Value = 58.823529412
        Dx_k1.Value = 17.060606061
        CurrentT_Th.Value = 37.037037037
        CurrentT_k1.Value = 10
        dU_k1.Value = 1
        dU_k2.Value = 0
        dTs_k1.Value = 1
        dTs_k2.Value = 0

        AlphaCO2.Value = 0.1
        BetaCO2.Value = 70
        ThCO2.Value = 0.1

        AlphaH.Value = 10.8
        BetaH.Value = 70
        ThH.Value = 0.1

        AlphaPa.Value = 0.07
        BetaPa.Value = 40
        ThPa.Value = 70

        AlphaDx.Value = 10.8
        BetaDx.Value = 70
        ThDx.Value = 0.1

    End Sub

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Setup min\max values for variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BeforeCalculate()
        MyBase.BeforeCalculate()

        ModelControl.UpdateValues()

    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()

        'calculate DeltaS ------------------------------------------
        If EnableSa.Value Then
            DeltaS.Value(CurrentStep) = Sa_k1.Value * (Sa.Value(CurrentStep - 1) - Sa_Th.Value)
        Else
            DeltaS.Value(CurrentStep) = 0
        End If

        'calculate DeltaAib ------------------------------------------
        If EnableAib.Value AndAlso qm_Th.Value <> 0 AndAlso qm.Value(CurrentStep - 1) < qm_Th.Value Then
            DeltaAib.Value(CurrentStep) = Aib_k1.Value * (1 - (qm.Value(CurrentStep - 1) / qm_Th.Value))
        Else
            DeltaAib.Value(CurrentStep) = 0
        End If

        'calculate DeltaAx ------------------------------------------
        If EnableAx.Value Then
            Dim BCO2 As Double = CalculateX(CO2.Value(CurrentStep - 1))
            DeltaAx.Value(CurrentStep) = Ax_k1.Value * BCO2
        Else
            DeltaAx.Value(CurrentStep) = 0
        End If

        'calculate DeltaAx ------------------------------------------
        If EnableAh.Value Then
            Dim CalculatedDx As Double = CalculateH(Horm.Value(CurrentStep - 1))
            DeltaAh.Value(CurrentStep) = Ah_k1.Value * CalculatedDx
        Else
            DeltaAh.Value(CurrentStep) = 0
        End If

        'calculate DeltaA ------------------------------------------
        DeltaA.Value(CurrentStep) = DeltaAib.Value(CurrentStep) + DeltaAx.Value(CurrentStep) + DeltaAh.Value(CurrentStep)
        DeltaA.Value(CurrentStep) = DeltaA.FixValue(DeltaA.Value(CurrentStep))

        'calculate DeltaDb ------------------------------------------
        If EnableDb.Value Then
            Dim CalculatedB As Double = CalculateB(Pa.Value(CurrentStep - 1))
            DeltaDb.Value(CurrentStep) = Db_k1.Value * CalculatedB
        Else
            DeltaDb.Value(CurrentStep) = 0
        End If

        'calculate DeltaDx ------------------------------------------
        If EnableDx.Value Then
            Dim CalculatedDx As Double = CalculateDX(Dx.Value(CurrentStep - 1))
            DeltaDx.Value(CurrentStep) = Dx_k1.Value * CalculatedDx
        Else
            DeltaDx.Value(CurrentStep) = 0
        End If

        'calculate DeltaD ------------------------------------------
        DeltaD.Value(CurrentStep) = DeltaDb.Value(CurrentStep) + DeltaDx.Value(CurrentStep)
        DeltaD.Value(CurrentStep) = DeltaD.FixValue(DeltaD.Value(CurrentStep))

        'calculate DeltaFt ------------------------------------------
        If EnableFt.Value Then
            'fix CurrentT to [35,41]
            Dim Temp As Double = 6 * CurrentT.Value(CurrentStep - 1) + 35
            DeltaFt.Value(CurrentStep) = CurrentT_k1.Value * (Temp - CurrentT_Th.Value)
        Else
            DeltaFt.Value(CurrentStep) = 0
        End If

        'calculate df ------------------------------------------
        'df.Value(CurrentStep) = 0
        df.Value(CurrentStep) = (DeltaA.Value(CurrentStep) - _
                                 DeltaD.Value(CurrentStep) + _
                                 DeltaFt.Value(CurrentStep) + _
                                 DeltaS.Value(CurrentStep)) _
                                 / 166.67

        'calculate du ------------------------------------------
        If Enable_dU.Value Then
            dU.Value(CurrentStep) = -dU_k1.Value * DeltaA.Value(CurrentStep) - _
                                     dU_k2.Value * DeltaS.Value(CurrentStep)
        Else
            dU.Value(CurrentStep) = 0
        End If

        'calculate dTs ------------------------------------------
        If Enable_dTs.Value Then
            dTs.Value(CurrentStep) = dTs_k1.Value * DeltaA.Value(CurrentStep) + _
                                     dTs_k2.Value * DeltaS.Value(CurrentStep)
        Else
            dTs.Value(CurrentStep) = 0
        End If

        'calculate dKr ------------------------------------------
        If Enable_dKr.Value Then
            dKr.Value(CurrentStep) = dKr_k1.Value * DeltaAx.Value(CurrentStep) + _
                                     dKr_k2.Value * DeltaDb.Value(CurrentStep)
        Else
            dKr.Value(CurrentStep) = 0
        End If

    End Sub

#Region "Helping methods"

    Private Function CalculateB(ByVal pa As Double) As Double
        Dim Result As Double

        Result = (1 - Math.Exp(-AlphaPa.Value * (pa - ThPa.Value))) / _
                 (1 + BetaPa.Value * Math.Exp(-AlphaPa.Value * (pa - ThPa.Value)))

        If Result < 0 Then Result = 0

        Return Result
    End Function

    Private Function CalculateH(ByVal horm As Double) As Double
        Dim Result As Double

        Result = (1 - Math.Exp(-AlphaH.Value * (horm - ThH.Value))) / _
                 (1 + BetaH.Value * Math.Exp(-AlphaH.Value * (horm - ThH.Value)))

        If Result < 0 Then Result = 0

        Return Result
    End Function

    Private Function CalculateX(ByVal hif As Double) As Double
        Dim Result As Double

        Result = (1 - Math.Exp(-AlphaCO2.Value * (hif - ThCO2.Value))) / _
                 (1 + BetaCO2.Value * Math.Exp(-AlphaCO2.Value * (hif - ThCO2.Value)))

        If Result < 0 Then Result = 0

        Return Result
    End Function

    Private Function CalculateDX(ByVal dx As Double) As Double
        Dim Result As Double

        Result = (1 - Math.Exp(-AlphaDx.Value * (dx - ThDx.Value))) / _
                 (1 + BetaDx.Value * Math.Exp(-AlphaDx.Value * (dx - ThCO2.Value)))

        If Result < 0 Then Result = 0

        Return Result
    End Function

#End Region

#End Region

#Region "UI Methods"

    Public Overrides Function GetControl() As System.Windows.Forms.UserControl
        Return ModelControl
    End Function

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save parameters
        ModelControl.UpdateValues()
        SetAttribute(CurrentElement, "EnableAib", EnableAib.Value)
        SetAttribute(CurrentElement, "EnableAx", EnableAx.Value)
        SetAttribute(CurrentElement, "EnableAh", EnableAh.Value)
        SetAttribute(CurrentElement, "EnableDb", EnableDb.Value)
        SetAttribute(CurrentElement, "EnableDx", EnableDx.Value)
        SetAttribute(CurrentElement, "EnableSa", EnableSa.Value)
        SetAttribute(CurrentElement, "EnableFt", EnableFt.Value)
        SetAttribute(CurrentElement, "Enable_dU", Enable_dU.Value)
        SetAttribute(CurrentElement, "Enable_dTs", Enable_dTs.Value)
        SetAttribute(CurrentElement, "Enable_dKr", Enable_dKr.Value)

        Return CurrentElement
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        MyBase.FromXml(currentElement)

        'load parameters
        If currentElement.Attributes("EnableAib") IsNot Nothing Then
            EnableAib.Value = GetBoolean(currentElement, "EnableAib")
        End If
        If currentElement.Attributes("EnableAx") IsNot Nothing Then
            EnableAx.Value = GetBoolean(currentElement, "EnableAx")
        End If
        If currentElement.Attributes("EnableAh") IsNot Nothing Then
            EnableAh.Value = GetBoolean(currentElement, "EnableAh")
        End If
        If currentElement.Attributes("EnableDb") IsNot Nothing Then
            EnableDb.Value = GetBoolean(currentElement, "EnableDb")
        End If
        If currentElement.Attributes("EnableDx") IsNot Nothing Then
            EnableDx.Value = GetBoolean(currentElement, "EnableDx")
        End If
        If currentElement.Attributes("EnableSa") IsNot Nothing Then
            EnableSa.Value = GetBoolean(currentElement, "EnableSa")
        End If
        If currentElement.Attributes("EnableFt") IsNot Nothing Then
            EnableFt.Value = GetBoolean(currentElement, "EnableFt")
        End If
        If currentElement.Attributes("Enable_dU") IsNot Nothing Then
            Enable_dU.Value = GetBoolean(currentElement, "Enable_dU")
        End If
        If currentElement.Attributes("Enable_dTs") IsNot Nothing Then
            Enable_dTs.Value = GetBoolean(currentElement, "Enable_dTs")
        End If
        If currentElement.Attributes("Enable_dKr") IsNot Nothing Then
            Enable_dKr.Value = GetBoolean(currentElement, "Enable_dKr")
        End If

        Return Me
    End Function

#End Region

End Class
