Imports System.Drawing
Imports System.Windows.Forms

Public Class ExceptionMessage
    Private Exception As System.Exception

#Region "Constructors"

    Public Sub New(ByVal niceDescription As String, ByVal fullExceptionText As String)
        Me.New(niceDescription, fullExceptionText, Nothing)
    End Sub

    Public Sub New(ByVal niceDescription As String, ByVal fullExceptionText As String, ByVal image As System.Drawing.Bitmap)
        Me.New()

        NiceMessageLabel.Text = niceDescription
        TextBox1.Text = fullExceptionText

        If Not image Is Nothing Then
            ErrorImagePictureBox.Image = image
        End If
    End Sub

    Public Sub New(ByVal niceDescription As String, ByVal exception As Exception)
        Me.New()

        NiceMessageLabel.Text = niceDescription
        TextBox1.Text = niceDescription & vbCrLf & vbCrLf & exception.ToString()

    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        OkButton.Text = "Ok"
        AdvancedButton.Text = "Details <<"
        Me.Text = "An error occured in Modelling tool"
        GroupBox1.Text = "Advanced error message"
        VersionInfoGroupBox.Text = "Version"

        HideAdvanced()

        SetInfoOSVersionAndApplicationVersion()
    End Sub

    Public Sub New(ByVal exception As System.Exception)
        Me.New()

        NiceMessageLabel.Text = "An error has occured in Modelling tool" & vbCrLf & vbCrLf & exception.Message()
        TextBox1.Text = exception.ToString()
    End Sub


#End Region

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Me.Owner Is Nothing Then
            Me.StartPosition = Windows.Forms.FormStartPosition.CenterScreen
            Me.TopMost = True
        End If

        MyBase.OnLoad(e)
    End Sub

    Private Sub AdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedButton.Click
        If GroupBox1.Visible Then
            HideAdvanced()
        Else
            ShowAdvanced()
        End If
    End Sub

    Private Sub ShowAdvanced()
        AdvancedButton.Text = AdvancedButton.Text.Replace(">>", "<<")
        GroupBox1.Visible = True
        Me.Height = Me.Height + GroupBox1.Height
    End Sub

    Private Sub HideAdvanced()
        AdvancedButton.Text = AdvancedButton.Text.Replace("<<", ">>")
        GroupBox1.Visible = False
        Me.Height = Me.Height - GroupBox1.Height
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        Me.Close()
    End Sub

    Private Sub SetInfoOSVersionAndApplicationVersion()
        Dim PlatformLabel As String
        Dim OSVersionLabel As String
        Dim AppVersion As String

        PlatformLabel = "Platform" & ":"
        OSVersionLabel = "Version operating system" & ":"
        AppVersion = "Version" & ":"


        Dim Labels() As String = {PlatformLabel, OSVersionLabel, AppVersion}

        Dim MaxWidthOfLabel As Integer = 0
        Dim TempLabel As New System.Windows.Forms.Label
        TempLabel.AutoSize = True
        For Each Str As String In Labels
            Dim tempSize As Rectangle = Functions.Text.GetSizeOfText(Str, TempLabel)
            MaxWidthOfLabel = Math.Max(MaxWidthOfLabel, tempSize.Width)
        Next

        VersionInfoLabel.Width = MaxWidthOfLabel
        VersionInfoValue.Location = New Point(VersionInfoLabel.Left + VersionInfoLabel.Width + 2 _
            , VersionInfoValue.Location.Y)
        VersionInfoLabel.Text = PlatformLabel & vbCrLf & OSVersionLabel & vbCrLf & AppVersion

        Dim FileVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
        VersionInfoValue.Text = Environment.OSVersion.Platform.ToString() & vbCrLf & Functions.OSInfo.GetOSVersion & vbCrLf & _
            FileVersion.FileVersion

    End Sub

End Class
