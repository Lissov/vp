
Public Class ProgressForm
    Implements App.IModalShow

#Region "Properties"

    Public Property ProgressText() As String
        Get
            Return ProgressLabel.Text
        End Get
        Set(ByVal value As String)
            ProgressLabel.Text = value
        End Set
    End Property

    Private _IgnoreModalFormWithStartUpNextInstance As Boolean
    Public Property IgnoreModalFormWithStartUpNextInstance() As Boolean Implements App.IModalShow.IgnoreModalFormWithStartUpNextInstance
        Get
            Return _IgnoreModalFormWithStartUpNextInstance
        End Get
        Set(ByVal value As Boolean)
            _IgnoreModalFormWithStartUpNextInstance = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = "Modelling tool is working..."
    End Sub

#End Region

    Public Sub EndProgress()

        Me.Close()

    End Sub

    Public Sub UpdateProgress(ByVal newProgressText As String)

        ProgressText = newProgressText

    End Sub

    Public Delegate Sub CloseForm()

    Public Delegate Sub UpdateForm(ByVal newProgressText As String)

End Class
