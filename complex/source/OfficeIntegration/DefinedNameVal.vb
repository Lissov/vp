''' <summary>
''' This class used to store full path to the cell
''' </summary>
''' <remarks></remarks>
Public Class DefinedNameVal

#Region "Properties"

    Private _Key As String
    ''' <summary>
    ''' User defined name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal value As String)
            _Key = value
        End Set
    End Property

    Private _SheetName As String
    Public Property SheetName() As String
        Get
            Return _SheetName
        End Get
        Set(ByVal value As String)
            _SheetName = value
        End Set
    End Property

    Private _StartColumn As String
    Public Property StartColumn() As String
        Get
            Return _StartColumn
        End Get
        Set(ByVal value As String)
            _StartColumn = value
        End Set
    End Property

    Private _StartRow As String
    Public Property StartRow() As String
        Get
            Return _StartRow
        End Get
        Set(ByVal value As String)
            _StartRow = value
        End Set
    End Property

    Private _EndColumn As String
    Public Property EndColumn() As String
        Get
            Return _EndColumn
        End Get
        Set(ByVal value As String)
            _EndColumn = value
        End Set
    End Property

    Private _EndRow As String
    Public Property EndRow() As String
        Get
            Return _EndRow
        End Get
        Set(ByVal value As String)
            _EndRow = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

    End Sub

    Public Sub New(ByVal key As String, _
                   ByVal sheetName As String, _
                   ByVal startColumn As String, _
                   ByVal startRow As String, _
                   ByVal endColumn As String, _
                   ByVal endRow As String)
        Me.New()

        Me.Key = key
        Me.SheetName = sheetName
        Me.StartColumn = startColumn
        Me.StartRow = startRow
        Me.EndColumn = endColumn
        Me.EndRow = endRow
    End Sub

    Public Sub New(ByVal key As String, _
                   ByVal sheetName As String, _
                   ByVal startColumn As String, _
                   ByVal startRow As String)
        Me.New(key, sheetName, startColumn, startRow, startColumn, startRow)

    End Sub

#End Region

End Class
