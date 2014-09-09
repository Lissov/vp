Imports System.Text
Imports System.Security.Principal

Public Class Folder

    Public Shared Function FolderExists(ByVal folderName As String) As Boolean
        Try
            Dim FolderInfo As New System.IO.DirectoryInfo(folderName)
            If FolderInfo.Exists Then
                Return True
            Else
                Return False
            End If
        Catch e As Exception
            Return False
        End Try

    End Function

    Public Shared Function CheckExistsOrCreateFolder(ByVal folderName As String) As Boolean
        If FolderExists(folderName) Then Return True
        Try
            System.IO.Directory.CreateDirectory(folderName)
            Return True
        Catch e As Exception
            Return False
        End Try
        Return False
    End Function

    Public Shared Sub CleanFolder(ByVal folderName As String)
        If Not FolderExists(folderName) Then Return

        Try
            Dim FolderInfo As New System.IO.DirectoryInfo(folderName)
            Dim Files As System.IO.FileInfo() = FolderInfo.GetFiles

            If (Files IsNot Nothing AndAlso Files.Length > 0) Then
                For Each File As System.IO.FileInfo In Files
                    Functions.File.DeleteFile(File.FullName, "")
                Next
            End If
        Catch e As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns true is directory with given full name does not contain any file or folder
    ''' </summary>
    ''' <param name="folderName"></param>
    ''' <returns></returns>
    ''' <remarks>Check if directory exists is not performed here</remarks>
    Public Shared Function FolderIsEmpty(ByVal folderName As String) As Boolean
        Try
            Dim FolderInfo As New System.IO.DirectoryInfo(folderName)
            Dim Files As System.IO.FileInfo() = FolderInfo.GetFiles
            Dim Folders As System.IO.DirectoryInfo() = FolderInfo.GetDirectories
            If (Files Is Nothing OrElse Files.Length = 0) AndAlso _
               (Folders Is Nothing OrElse Folders.Length = 0) _
               Then
                Return True
            Else
                Return False
            End If
        Catch e As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Used to delete all empty subfolders in given folder and this folder if after all it became empty
    ''' </summary>
    ''' <param name="folderName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteEmptyFolders(ByVal folderName As String) As Boolean
        Dim Result As Boolean = False

        Try
            Dim FolderInfo As New System.IO.DirectoryInfo(folderName)
            Dim Folders As System.IO.DirectoryInfo() = FolderInfo.GetDirectories
            If Folders IsNot Nothing AndAlso Folders.Length > 0 Then
                For Each Folder As System.IO.DirectoryInfo In Folders
                    DeleteEmptyFolders(Folder.FullName)
                Next
            End If

            If FolderIsEmpty(folderName) Then
                System.IO.Directory.Delete(folderName)
                Result = True
            End If

        Catch e As Exception
            Result = False
        End Try

        Return Result
    End Function

    Public Shared Function GetLinkedFiles(ByVal folderName As String, ByVal fileName As String) As System.Collections.Generic.List(Of String)
        Dim LinkedFiles As New System.Collections.Generic.List(Of String)

        If FolderExists(folderName) AndAlso Functions.File.FileExists(fileName) Then
            Try
                Dim FolderInfo As New System.IO.DirectoryInfo(folderName)
                Dim Files As System.IO.FileInfo() = FolderInfo.GetFiles

                If (Files IsNot Nothing AndAlso Files.Length > 0) Then
                    For Each File As System.IO.FileInfo In Files
                        If File.FullName <> fileName Then
                            If IO.Path.GetFileNameWithoutExtension(File.FullName) = IO.Path.GetFileNameWithoutExtension(fileName) Then
                                LinkedFiles.Add(File.FullName)
                            End If
                        End If
                    Next
                End If
            Catch e As Exception
            End Try
        End If

        Return LinkedFiles
    End Function

End Class
