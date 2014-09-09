Imports System.Threading

Namespace App

    Public Class RunningProcess

#Region "Properties"

        Private _ProgressForm As ProgressForm
        Public Property ProgressForm() As ProgressForm
            Get
                Return _ProgressForm
            End Get
            Set(ByVal value As ProgressForm)
                _ProgressForm = value
            End Set
        End Property

        Private _IsProgressRunning As Boolean
        Public Property IsProgressRunning() As Boolean
            Get
                Return _IsProgressRunning
            End Get
            Set(ByVal value As Boolean)
                _IsProgressRunning = value
            End Set
        End Property

        Private _ActiveForm As Form
        Public Property ActiveForm() As Form
            Get
                Return _ActiveForm
            End Get
            Set(ByVal value As Form)
                _ActiveForm = value
            End Set
        End Property

#End Region '"Properties"

#Region "Constructors"

        Public Sub New()
            _ProgressForm = New ProgressForm()
            _IsProgressRunning = False
        End Sub

        Public Sub New(ByVal text As String)
            Me.New()

            _ProgressForm.ProgressLabel.Text = text
        End Sub

#End Region

        Public Sub StartProgress()
            If Not TypeOf Form.ActiveForm Is ProgressForm Then
                Me.ActiveForm = Form.ActiveForm
            End If

            Dim t As Thread = New Thread(New ThreadStart(AddressOf Execute))
            t.IsBackground = True
            t.Priority = ThreadPriority.AboveNormal
            t.Start()
            _IsProgressRunning = True
        End Sub

        Public Sub EndProgress()
            'firstly activate current form...
            If Not Me.ActiveForm Is Nothing AndAlso Me.ActiveForm.Visible Then
                Try
                    Me.ActiveForm.Activate()
                Catch
                End Try
            End If

            'Thread.Sleep(3000)

            '... secondary close progressform
            Try
                Dim CloseForm As New ProgressForm.CloseForm(AddressOf _ProgressForm.EndProgress)
                ProgressForm.Invoke(CloseForm)
            Catch ex As Exception
                Thread.Sleep(1000)
                Try
                    Dim CloseForm As New ProgressForm.CloseForm(AddressOf _ProgressForm.EndProgress)
                    ProgressForm.Invoke(CloseForm)
                Catch ex1 As Exception
                    ' do nothing
                Finally
                End Try
            End Try

            _IsProgressRunning = False
        End Sub

        Public Sub UpdateProgress(ByVal newProgressText As String)
            'firstly activate current form...
            If Not Me.ActiveForm Is Nothing AndAlso Me.ActiveForm.Visible Then
                Try
                    Me.ActiveForm.Activate()
                Catch
                End Try
            End If

            'Thread.Sleep(3000)

            '... secondary close progressform
            Try
                Dim UpdateForm As New ProgressForm.UpdateForm(AddressOf _ProgressForm.UpdateProgress)
                ProgressForm.Invoke(UpdateForm, newProgressText)
            Catch ex As Exception
                Thread.Sleep(1000)
                Try
                    Dim UpdateForm As New ProgressForm.UpdateForm(AddressOf _ProgressForm.UpdateProgress)
                    ProgressForm.Invoke(UpdateForm, newProgressText)
                Catch ex1 As Exception
                    ' do nothing
                Finally
                End Try
            End Try

        End Sub

        Private Sub Execute()
            _ProgressForm.ShowDialog()
        End Sub

    End Class 'RunningProcess

    Public Interface IModalShow
        Property IgnoreModalFormWithStartUpNextInstance() As Boolean
    End Interface

End Namespace