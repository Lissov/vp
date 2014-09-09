Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class Text

    Public Shared Function GetSizeOfText(ByVal text As String, ByVal control As Windows.Forms.Control) As Rectangle
        Dim fnt As Font = control.Font
        Return GetSizeOfText(text, control, fnt)
    End Function

    Public Shared Function GetSizeOfText(ByVal text As String, ByVal control As Windows.Forms.Control, ByVal fnt As Font) As Rectangle
        Dim Rctngl As New Rectangle
        Try
            Dim Wdth As Integer
            Dim Hght As Integer
            Using tempGraphics As Graphics = control.CreateGraphics()
                Wdth = CInt(tempGraphics.MeasureString(text, fnt).Width)
                Hght = CInt(tempGraphics.MeasureString(text, fnt).Height)
            End Using
            Rctngl.Width = Wdth
            Rctngl.Height = Hght
            Return Rctngl
        Catch ex As Exception
            Return Rctngl
        End Try
    End Function

    ''' <summary>
    ''' Trims the end of the given text if it is longer then control and then add ...
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="control"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TrimText(ByVal text As String, ByVal control As Control) As String
        If String.IsNullOrEmpty(text) Then Return text

        If text.Contains(vbCrLf) Then
            Dim vbCrLfIndex As Integer = text.IndexOf(vbCrLf)
            text = text.Remove(vbCrLfIndex)
            text &= "..."
        End If
        If text.Contains(vbLf) Then
            Dim vbLfIndex As Integer = text.IndexOf(vbLf)
            text = text.Remove(vbLfIndex)
            text &= "..."
        End If

        Dim NeedAddText As Boolean = False
        Dim g As Graphics = control.CreateGraphics()

        While Not String.IsNullOrEmpty(text) AndAlso Convert.ToInt32(g.MeasureString(text, control.Font, control.Width).Height) > control.Height
            NeedAddText = True
            text = text.Remove(text.Length - 1, 1)
        End While

        If NeedAddText AndAlso text.Length > 3 Then
            text = text.Remove(text.Length - 3, 3)
            text &= "..."
        End If

        Return text
    End Function

    Private Shared _IsBigFontUsed As Boolean? = Nothing
    ''' <summary>
    ''' If true - user choose large font in windows settings
    ''' </summary>
    ''' <remarks>Used to fix corrupted views</remarks>
    Public Shared Property IsBigFontUsed() As Boolean
        Get
            If _IsBigFontUsed Is Nothing Then
                Dim Panel As New System.Windows.Forms.Panel
                Dim FontSize As Size = TextRenderer.MeasureText(Panel.CreateGraphics, "a", Panel.Font)
                _IsBigFontUsed = (FontSize.Width >= 15)
            End If
            Return CBool(_IsBigFontUsed)
        End Get
        Set(ByVal value As Boolean)
            _IsBigFontUsed = value
        End Set
    End Property

    Public Shared Function IsValidVariableName(ByVal name As String) As Boolean
        Dim CodeProvider As New VBCodeProvider
        Return CodeProvider.IsValidIdentifier(name)
    End Function

End Class
