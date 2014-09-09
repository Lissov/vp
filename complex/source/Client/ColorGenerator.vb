Imports System.Collections.Generic

''' <summary>
''' Use this class to generate contrast colors.
''' Do not forget to call Reset before generating.
''' </summary>
''' <remarks></remarks>
Public Class ColorGenerator

#Region "Properties"

    Private Shared _Instance As ColorGenerator
    Public Shared ReadOnly Property Instance() As ColorGenerator
        Get
            If _Instance Is Nothing Then
                _Instance = New ColorGenerator
            End If
            Return _Instance
        End Get
    End Property

    Private _Colors As List(Of Color)
    ''' <summary>
    ''' List with conrast colors
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property Colors() As List(Of Color)
        Get
            If _Colors Is Nothing Then
                _Colors = New List(Of Color)
                _Colors.Add(Color.Blue)
                _Colors.Add(Color.Red)
                _Colors.Add(Color.Green)
                _Colors.Add(Color.Lime)
                _Colors.Add(Color.Orange)
                _Colors.Add(Color.Cyan)
                _Colors.Add(Color.Fuchsia)
                _Colors.Add(Color.Black)
                _Colors.Add(Color.SeaGreen)
                _Colors.Add(Color.Pink)
                _Colors.Add(Color.Navy)
                _Colors.Add(Color.Olive)
                _Colors.Add(Color.Chocolate)
                _Colors.Add(Color.Salmon)
                _Colors.Add(Color.Yellow)
                _Colors.Add(Color.Indigo)
                _Colors.Add(Color.Maroon)
            End If
            Return _Colors
        End Get
    End Property

    Private _ColorIndex As Integer = -1
    Private Property ColorIndex() As Integer
        Get
            Return _ColorIndex
        End Get
        Set(ByVal value As Integer)
            _ColorIndex = value
        End Set
    End Property


#End Region

#Region "Constructors"

    Private Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Function GenerateColor() As Color
        ColorIndex += 1
        If ColorIndex >= Colors.Count Then ColorIndex = 0
        Return Colors(ColorIndex)
    End Function

    Public Sub Reset()
        ColorIndex = -1
    End Sub

#End Region


End Class
