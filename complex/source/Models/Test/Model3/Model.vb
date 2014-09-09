Public Class Model
    Inherits ModelBase.ModelBase

    Public X1 As New ModelBase.Value("X1", "X1", ModelBase.Value.ValueType.Input)
    Public X2 As New ModelBase.Value("X2", "X2", ModelBase.Value.ValueType.Internal)

    Public K1 As New ModelBase.Parameter("k1", "k1")

    Public Sub New()
        MyBase.New()

        Me.Name = "Model3"
        Me.DisplayName = "Model 3"
        Me.Description = "Test model where X1=Input+Paremeter and X2=Sin(x1)"

        Me.Step = 0.2
    End Sub

#Region "Calculate"

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Cycle()
        X1.Value(CurrentStep) = X1.Value(CurrentStep) - K1.Value

        If X1.Value(CurrentStep) <> 0 Then
            X2.Value(CurrentStep) = 1 / X1.Value(CurrentStep)
        Else
            Throw New Exception("X1.Value(CurrentStep) = 0")
        End If
    End Sub

#End Region

#Region "UI Methods"

    Public Overrides Function GetControl() As System.Windows.Forms.UserControl
        Dim TestControl As New TestControl
        Return TestControl
    End Function

    Public Overrides Function GetMenuItems() As List(Of ModelBase.MenuItem)
        Dim MenuItems As New List(Of ModelBase.MenuItem)

        Dim Ball1 As New ModelBase.MenuItem("Balls", "Green", My.Resources.ball_green)
        AddHandler Ball1.ItemClicked, AddressOf BallItemClicked
        MenuItems.Add(Ball1)
        Dim Ball2 As New ModelBase.MenuItem("Balls", "Red", My.Resources.ball_red)
        AddHandler Ball2.ItemClicked, AddressOf BallItemClicked
        MenuItems.Add(Ball2)
        Dim Ball3 As New ModelBase.MenuItem("Balls", "Yellow", My.Resources.ball_yellow)
        AddHandler Ball3.ItemClicked, AddressOf BallItemClicked
        MenuItems.Add(Ball3)

        Dim App As New ModelBase.MenuItem("Application", "Config", My.Resources.applications)
        AddHandler App.ItemClicked, AddressOf AppItemClicked
        MenuItems.Add(App)

        Return MenuItems
    End Function

    Private Sub BallItemClicked(ByVal sender As ModelBase.MenuItem)
        MsgBox(String.Format("Menu item {0} clicked", sender.DisplayName))
    End Sub

    Private Sub AppItemClicked(ByVal sender As ModelBase.MenuItem)
        MsgBox(String.Format("My name is {0}", Me.DisplayName))
    End Sub

#End Region

End Class
