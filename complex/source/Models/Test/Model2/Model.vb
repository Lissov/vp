Imports ModelBase
Imports BetaSetup

Public Class Model
    Inherits ModelBase.ModelBase

    Public I1 As New Value("I1", "I1", Value.ValueType.Input)
    Public I2 As New Value("I2", "I2", Value.ValueType.Input)

    Public X1 As New Value("X1", "X1", Value.ValueType.Output)
    Public X2 As New Value("X2", "X2", Value.ValueType.Output)

    Public K1 As New Parameter("k1", "k1")

    Public Alpha As New Parameter("Alpha", "Alpha", 0, 1)
    Public Beta As New Parameter("Beta", "Beta", 0, 100)
    Public Th As New Parameter("Th", "Th", 0, 200)

    Public UseFirstEquation As New SwitchParameter("UseFirstEquation", "Use first equation")

    Private _BetaSetupControl As BetaSetupControl
    Public ReadOnly Property BetaSetupControl() As BetaSetupControl
        Get
            If _BetaSetupControl Is Nothing Then
                _BetaSetupControl = New BetaSetupControl(Alpha, Beta, Th, 0, 200)
            End If
            Return _BetaSetupControl
        End Get
    End Property

    Private _BetaSetupForm As BetaSetupForm
    Public ReadOnly Property BetaSetupForm() As BetaSetupForm
        Get
            If _BetaSetupForm Is Nothing Then
                _BetaSetupForm = New BetaSetupForm(Alpha, Beta, Th, 0, 200)
            End If
            Return _BetaSetupForm
        End Get
    End Property

    Public Sub New()
        MyBase.New()

        Me.Name = "Model2"
        Me.DisplayName = "Model 2"
        Me.Description = "Test model where X1=Input*Paremeter and X2=Sin(x1) or X2=Cos(x1)"

        Alpha.Value = 0.07
        Beta.Value = 40
        Th.Value = 70

        UseFirstEquation.Value = True
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        X1.Value(CurrentStep) = I1.Value(CurrentStep - 1) * K1.Value

        If UseFirstEquation.Value Then
            X2.Value(CurrentStep) = Math.Sin(X1.Value(CurrentStep - 1))
        Else
            X2.Value(CurrentStep) = Math.Cos(X1.Value(CurrentStep - 1))
        End If

    End Sub

#End Region

#Region "UI Methods"

    Public Overrides Function GetMenuItems() As List(Of ModelBase.MenuItem)
        Dim MenuItems As New List(Of ModelBase.MenuItem)

        Dim App As New ModelBase.MenuItem("Application", "Config", My.Resources.applications)
        AddHandler App.ItemClicked, AddressOf AppItemClicked
        MenuItems.Add(App)

        Return MenuItems
    End Function


    Private Sub AppItemClicked(ByVal sender As ModelBase.MenuItem)
        BetaSetupForm.ShowDialog()
    End Sub

#End Region

End Class
