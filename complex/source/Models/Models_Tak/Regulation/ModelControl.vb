Imports System.Windows.Forms

Public Class ModelControl

#Region "Properties"

    Private _Regulators As List(Of Regulator)
    Public Property Regulators() As List(Of Regulator)
        Get
            Return _Regulators
        End Get
        Set(ByVal value As List(Of Regulator))
            _Regulators = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal regulators As List(Of Regulator), _
                   ByVal hormSetupControl As Control, _
                   ByVal hifSetupControl As Control, _
                   ByVal bSetupControl As Control, _
                   ByVal dxSetupControl As Control)
        Me.New()

        Me.Regulators = regulators
        FillRegulatorsGrid()

        If hormSetupControl IsNot Nothing Then
            hormSetupControl.Dock = DockStyle.Fill
            HormSetupPage.Controls.Add(hormSetupControl)
        Else
            HormSetupPage.Hide()
        End If

        If hifSetupControl IsNot Nothing Then
            hifSetupControl.Dock = DockStyle.Fill
            HifSetupPage.Controls.Add(hifSetupControl)
        Else
            HifSetupPage.Hide()
        End If

        If bSetupControl IsNot Nothing Then
            bSetupControl.Dock = DockStyle.Fill
            BSetupPage.Controls.Add(bSetupControl)
        Else
            BSetupPage.Hide()
        End If

        If dxSetupControl IsNot Nothing Then
            dxSetupControl.Dock = DockStyle.Fill
            DxSetupPage.Controls.Add(dxSetupControl)
        Else
            DxSetupPage.Hide()
        End If

    End Sub

#End Region

#Region "Public methods"

    Public Sub UpdateValues()
        RegulatorsBindingSource.EndEdit()
    End Sub

#End Region

#Region "Private methods"

    Private Sub FillRegulatorsGrid()
        RegulatorsGrid.DataBindings.DefaultDataSourceUpdateMode = Windows.Forms.DataSourceUpdateMode.OnPropertyChanged
        RegulatorsGrid.AutoGenerateColumns = False
        RegulatorsBindingSource.DataSource = Regulators
    End Sub


#End Region

#Region "Event handlers"

    Private Sub RegulatorsGrid_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles RegulatorsGrid.CellValueChanged
        RegulatorsBindingSource.EndEdit()
    End Sub

#End Region


End Class
