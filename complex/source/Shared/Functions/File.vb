Imports System.Text
Imports System.Security.Principal

Public Class File

    Public Shared Function FileExists(ByVal fileName As String) As Boolean
        If String.IsNullOrEmpty(fileName) Then
            Return False
        End If
        Try
            Dim FileInfo As New System.IO.FileInfo(fileName)
            If FileInfo.Exists Then
                Return True
            Else
                Return False
            End If
        Catch e As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Exeption-safe moves a specified file to a new location
    ''' </summary>
    ''' <param name="sourceFileName"></param>
    ''' <param name="destFileName"></param>
    ''' <remarks></remarks>
    Public Shared Function MoveFile(ByVal sourceFileName As String, ByVal destFileName As String, ByRef errorString As String) As Boolean
        errorString = String.Empty

        If Not FileExists(sourceFileName) Then
            errorString = String.Format("Unable to move file '{0}' - it does not exist", sourceFileName)
            Return False
        End If

        Try
            DeleteFile(destFileName, errorString)
            System.IO.File.Move(sourceFileName, destFileName)
        Catch ex As Exception
            errorString = ex.Message
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Exeption-safe copies a specified file to a new location
    ''' </summary>
    ''' <param name="sourceFileName"></param>
    ''' <param name="destFileName"></param>
    ''' <remarks></remarks>
    Public Shared Function CopyFile(ByVal sourceFileName As String, ByVal destFileName As String, ByRef errorString As String) As Boolean
        errorString = String.Empty

        If Not FileExists(sourceFileName) Then
            errorString = String.Format("Unable to copy file '{0}' - it does not exist", sourceFileName)
            Return False
        End If

        Try
            DeleteFile(destFileName, errorString)
            System.IO.File.Copy(sourceFileName, destFileName)
        Catch ex As Exception
            errorString = ex.Message
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Exeption-safe deletes a specified file 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Shared Function DeleteFile(ByVal fileName As String, ByRef errorString As String) As Boolean
        errorString = String.Empty

        If Not FileExists(fileName) Then Return False

        Try
            System.IO.File.SetAttributes(fileName, IO.FileAttributes.Normal)
            System.IO.File.Delete(fileName)
        Catch ex As Exception
            errorString = ex.Message
            Return False
        End Try

        Return True

    End Function

    Public Shared Function CompareFiles(ByVal file1 As String, ByVal file2 As String) As Boolean
        If file1 <> file2 Then
            If FileExists(file1) AndAlso FileExists(file2) Then
                Using Stream1 As New System.IO.FileStream(file1, System.IO.FileMode.Open), _
                      Stream2 As New System.IO.FileStream(file2, System.IO.FileMode.Open)
                    If Stream1.Length <> Stream2.Length Then
                        Return False
                    Else
                        Dim File1Byte As Integer
                        Dim File2Byte As Integer
                        Do
                            File1Byte = Stream1.ReadByte()
                            File2Byte = Stream2.ReadByte()
                            If File1Byte <> File2Byte Then
                                Return False
                            End If
                        Loop While (File1Byte <> -1)
                    End If
                End Using
            Else
                Return False ' one of files does not exist
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' This function used to check if we can access file with given name
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks>Should be used before checkin</remarks>
    Public Shared Function FileIsAccessible(ByVal fileName As String) As Boolean
        If String.IsNullOrEmpty(fileName) Or Not FileExists(fileName) Then
            Return False
        End If
        Try
            Dim sr As System.IO.FileStream = System.IO.File.OpenWrite(fileName)
            sr.Close()
            Return True
        Catch e As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Check, does file exists, and it's length less that maxSize
    ''' If length more, that maxSize - delete it
    ''' Use this function for write info to log files
    ''' </summary>
    ''' <param name="fileName">path to file</param>
    ''' <param name="maxSize">max size that alloved for this size (bytes)</param>
    ''' <remarks></remarks>
    Public Shared Sub CheckSizeAndDeleteIfTooBig(ByVal fileName As String, ByVal maxSize As Long)
        Dim fl As New System.IO.FileInfo(fileName)
        If fl.Exists Then
            If fl.Length > maxSize AndAlso FileIsAccessible(fileName) Then
                Try
                    System.IO.File.Delete(fileName)
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub


    ''' <summary>
    ''' The method add text info to the specified file
    ''' if file doesn't exist it should be created
    ''' </summary>
    ''' <param name="filePath">path to file</param>
    ''' <param name="info">info to be logged</param>
    ''' <remarks></remarks>
    Public Shared Sub LogInfo(ByVal filePath As String, ByVal info As String)
        My.Computer.FileSystem.WriteAllText(filePath, info, True)
    End Sub

End Class
