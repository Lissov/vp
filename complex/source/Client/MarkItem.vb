Public Class MarkItem
    Implements ICloneable

#Region "Constructors"

    Public Sub New(ByVal configuration As Object)
        Me.Configuration = configuration
    End Sub

    Public Sub New(ByVal configuration As Object, ByVal modelName As String, ByVal valueName As String)
        Me.New(configuration)

        Me.ModelName = modelName
        Me.ValueName = valueName
    End Sub

#End Region

    Public Configuration As Object  'can be ModelBase.Configuration or ModelBase.SavedConfiguration

    Public ModelName As String = String.Empty
    Public ValueName As String = String.Empty

    Public ReadOnly Property DisplayName() As String
        Get
            Return ModelName & "\" & ValueName
        End Get
    End Property

    Public MarkStyle As Steema.TeeChart.Styles.PointerStyles = Steema.TeeChart.Styles.PointerStyles.Nothing

    Public Property MarkStyleDisplayName() As String
        Get
            Return MarkStyle.ToString
        End Get
        Set(ByVal value As String)
            Dim IsFound As Boolean = False

            Dim Style As Steema.TeeChart.Styles.PointerStyles
            For Style = Steema.TeeChart.Styles.PointerStyles.Rectangle To Steema.TeeChart.Styles.PointerStyles.PolishedSphere Step 1
                If Style.ToString = value Then
                    MarkStyle = Style
                    IsFound = True
                    Exit For
                End If
            Next

            If Not IsFound Then MarkStyle = Steema.TeeChart.Styles.PointerStyles.Nothing
        End Set
    End Property


    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return Me.MemberwiseClone
    End Function

End Class
