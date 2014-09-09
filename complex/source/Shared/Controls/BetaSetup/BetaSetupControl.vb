Imports ModelBase
Imports Functions
Imports Steema.TeeChart
Imports Steema.TeeChart.Styles

Public Class BetaSetupControl

#Region "Properties"

    Private _Alpha As Parameter
    Public Property Alpha() As Parameter
        Get
            Return _Alpha
        End Get
        Set(ByVal value As Parameter)
            _Alpha = value
        End Set
    End Property

    Private _Beta As Parameter
    Public Property Beta() As Parameter
        Get
            Return _Beta
        End Get
        Set(ByVal value As Parameter)
            _Beta = value
        End Set
    End Property

    Private _Th As Parameter
    Public Property Th() As Parameter
        Get
            Return _Th
        End Get
        Set(ByVal value As Parameter)
            _Th = value
        End Set
    End Property

#Region "Preview"

    Private _StartTime As Double = 0
    ''' <summary>
    ''' Start time for preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StartTime() As Double
        Get
            Return _StartTime
        End Get
        Set(ByVal value As Double)
            _StartTime = value
        End Set
    End Property

    Private _EndTime As Double = 100
    ''' <summary>
    ''' End time for preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndTime() As Double
        Get
            Return _EndTime
        End Get
        Set(ByVal value As Double)
            _EndTime = value
        End Set
    End Property

    Private _StepsCount As Integer = 100
    ''' <summary>
    ''' Defines how many points will be shown on preview
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StepsCount() As Integer
        Get
            Return _StepsCount
        End Get
        Set(ByVal value As Integer)
            _StepsCount = value
        End Set
    End Property

    Private _EventsAllowed As Boolean = False
    Public Property EventsAllowed() As Boolean
        Get
            Return _EventsAllowed
        End Get
        Set(ByVal value As Boolean)
            _EventsAllowed = value
        End Set
    End Property

#End Region

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _EventsAllowed = True
    End Sub

    Public Sub New(ByVal alpha As Parameter, _
                   ByVal beta As Parameter, _
                   ByVal th As Parameter)
        Me.New()

        Me.Alpha = alpha
        Me.Beta = beta
        Me.Th = th

        UpdateUI()
    End Sub

    Public Sub New(ByVal alpha As Parameter, _
                   ByVal beta As Parameter, _
                   ByVal th As Parameter, _
                   ByVal startTime As Double, _
                   ByVal endTime As Double)
        Me.New()

        Me.Alpha = alpha
        Me.Beta = beta
        Me.Th = th

        Me.StartTime = startTime
        Me.EndTime = endTime

        UpdateUI()
    End Sub

#End Region

#Region "Event handlers"

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox_Alpha.KeyPress
        If Not (e.KeyChar >= "0"c AndAlso e.KeyChar <= "9"c) AndAlso Not e.KeyChar = vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub TrackBar_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_Alpha.ValueChanged, TrackBar_Th.ValueChanged, TrackBar_Beta.ValueChanged
        If Not EventsAllowed Then Return

        UpdateValues()
        UpdateUI()
    End Sub

    Private Sub BetaSetupControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateColors()
    End Sub

#End Region

#Region "Private methods"

    Private Sub UpdateValues()
        Alpha.Value = GetParameterValueFromTrackBar(Alpha, TrackBar_Alpha)
        Beta.Value = GetParameterValueFromTrackBar(Beta, TrackBar_Beta)
        Th.Value = GetParameterValueFromTrackBar(Th, TrackBar_Th)
    End Sub

    Private Function GetParameterValueFromTrackBar(ByVal parameter As Parameter, ByVal trackBar As TrackBar) As Double
        Dim MinValue As Double
        If parameter.MinValue > Double.MinValue Then
            MinValue = parameter.MinValue
        Else
            MinValue = 0
        End If

        Dim MaxValue As Double
        If parameter.MaxValue < Double.MaxValue Then
            MaxValue = parameter.MaxValue
        Else
            MaxValue = 100
        End If

        Return (MaxValue - MinValue) * trackBar.Value / trackBar.Maximum + MinValue

    End Function

    Private Sub UpdateTrackBarFromParameterValue(ByVal parameter As Parameter, ByVal trackBar As TrackBar)
        Dim MinValue As Double
        If parameter.MinValue > Double.MinValue Then
            MinValue = parameter.MinValue
        Else
            MinValue = 0
        End If

        Dim MaxValue As Double
        If parameter.MaxValue < Double.MaxValue Then
            MaxValue = parameter.MaxValue
        Else
            MaxValue = 100
        End If

        trackBar.Value = Math.Round((parameter.Value - MinValue) * trackBar.Maximum / (MaxValue - MinValue))
    End Sub

#End Region

#Region "Chart"

    Public Sub ClearChart()
        For i As Integer = PreviewChart.Series.Count - 1 To 0 Step -1
            PreviewChart.Series(i).Dispose()
        Next

        PreviewChart.Series.Clear()
        'PreviewChart.Header.Visible = False
        PreviewChart.Footer.Visible = False
        PreviewChart.Panel.Shadow.Visible = False
        PreviewChart.Aspect.View3D = False
    End Sub

    Private Sub ShowPreview()
        Try

            ClearChart()

            Dim Times As Double()
            Dim Values As Double()
            Dim [Step] As Double = (EndTime - StartTime) / StepsCount

            ReDim Times(StepsCount)
            ReDim Values(StepsCount)

            Dim Time As Double = StartTime
            Dim CurrentStep As Integer = 0
            While CurrentStep <= StepsCount
                Times(CurrentStep) = Time
                Values(CurrentStep) = GetCalculatedValue(Time)

                'update values
                Time += [Step]
                CurrentStep += 1
            End While

            'create serie
            Dim Serie As Series
            Dim LineStyle As New Steema.TeeChart.Styles.Line
            Serie = PreviewChart.Series.Add(LineStyle)
            Serie.ShowInLegend = False
            PreviewChart.Series.Add(Serie)

            Serie.Visible = True
            Serie.Add(Times, Values)
        Catch
        End Try
    End Sub

#End Region

#Region "Public methods"

    Public Function GetCalculatedValue(ByVal time As Double) As Double
        Dim Result As Double

        Result = MathFunctions.BF(time, Alpha.Value, Beta.Value, Th.Value)

        Return Result
    End Function


    Public Sub UpdateUI()
        EventsAllowed = False

        If Alpha IsNot Nothing Then
            TextBox_Alpha.Text = Alpha.Value.ToString
            UpdateTrackBarFromParameterValue(Alpha, TrackBar_Alpha)
        End If
        If Beta IsNot Nothing Then
            TextBox_Beta.Text = Beta.Value.ToString
            UpdateTrackBarFromParameterValue(Beta, TrackBar_Beta)
        End If
        If Th IsNot Nothing Then
            TextBox_Th.Text = Th.Value.ToString
            UpdateTrackBarFromParameterValue(Th, TrackBar_Th)
        End If

        EventsAllowed = True

        ShowPreview()
    End Sub

    Public Sub UpdateColors()
        Dim BackColor As Color = Me.BackColor
        Dim Parent As Control = Me
        While Parent IsNot Nothing
            If Parent.BackColor <> System.Drawing.Color.Transparent Then
                BackColor = Parent.BackColor
            End If
            Parent = Parent.Parent
        End While

        Try
            TrackBar_Alpha.BackColor = Me.BackColor
            TrackBar_Beta.BackColor = Me.BackColor
            TrackBar_Th.BackColor = Me.BackColor
            TextBox_Alpha.BackColor = Me.BackColor
            TextBox_Beta.BackColor = Me.BackColor
            TextBox_Th.BackColor = Me.BackColor
        Catch
        End Try
    End Sub

#End Region




End Class
