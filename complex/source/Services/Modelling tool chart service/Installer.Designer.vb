<CLSCompliant(False)> <System.ComponentModel.RunInstaller(True)> Partial Class Installer
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ServiceInstaller1 = New System.ServiceProcess.ServiceInstaller
        Me.ServiceProcessInstaller1 = New System.ServiceProcess.ServiceProcessInstaller
        '
        'ServiceInstaller1
        '
        Me.ServiceInstaller1.Description = "This service optimizes TeeChart"
        Me.ServiceInstaller1.DisplayName = "Modelling tool chart service"
        Me.ServiceInstaller1.ServiceName = "ModellingToolChartService"
        Me.ServiceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        Me.ServiceInstaller1.Installers.Clear()
        '
        'ServiceProcessInstaller1
        '
        Me.ServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.ServiceProcessInstaller1.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceInstaller1})
        Me.ServiceProcessInstaller1.Password = Nothing
        Me.ServiceProcessInstaller1.Username = Nothing

      
        '
        'Installer
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.ServiceProcessInstaller1})

    End Sub

    Friend WithEvents ServiceInstaller1 As System.ServiceProcess.ServiceInstaller
    Friend WithEvents ServiceProcessInstaller1 As System.ServiceProcess.ServiceProcessInstaller

End Class
