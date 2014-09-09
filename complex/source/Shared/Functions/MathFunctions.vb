
''' <summary>
''' All integrals here are calculated using Simpson's formula
'''    /
'''   |           h
'''   | f(x)dx =  - (f   + 4 f   + 2 f + 4 f  + ... + 2 f   + 4 f    + f )
'''   |           3   0       1       2     3            N-2     N-1    N
'''  /
''' </summary>
''' <remarks></remarks>
Public Class MathFunctions

    ''' <summary>
    ''' Calculates integral using Simpson formula
    '''    /
    '''   |           h
    '''   | f(x)dx =  - (f   + 4 f   + 2 f + 4 f  + ... + 2 f   + 4 f    + f )
    '''   |           3   0       1       2     3            N-2     N-1    N
    '''  /
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Integral(ByVal [step] As Double, _
                                    ByVal stepsCount As Double, _
                                    ByVal [function] As Double()) _
                                        As Double

        Dim Result As Double = [function](0)

        Dim k As Integer

        For i As Integer = 1 To stepsCount
            If i = stepsCount Then
                k = 1
            ElseIf i Mod 2 = 0 Then
                k = 4
            Else
                k = 2
            End If

            Result += k * [function](i)
        Next

        Result *= [step] / 3

        Return Result
    End Function

    ''' <summary>
    ''' Calculates beta function 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Beta(ByVal a As Double, _
                                ByVal b As Double) _
                                    As Double
        Dim Result As Double = 0

        Dim StepsCount As Integer = 10
        Dim [Step] As Double = 0.1
        Dim X As Double
        Dim k As Integer

        For i As Integer = 1 To StepsCount
            If i = StepsCount Then
                k = 1
            ElseIf i Mod 2 = 0 Then
                k = 4
            Else
                k = 2
            End If

            X = [Step] * i
            If Math.Abs(X) < 0.01 Then X = 0

            Result += k * Math.Pow(X, a - 1) * Math.Pow(1 - X, b - 1)
        Next

        Result *= [Step] / 3

        Return Result
    End Function

    ''' <summary>
    ''' Calculates beta function integral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IntBeta(ByVal ind As Integer, _
                                   ByVal n As Integer, _
                                   ByVal a As Double, _
                                   ByVal b As Double) _
                                        As Double
        Dim Result As Double = 0

        Dim StepsCount As Integer = 10
        Dim [Step] As Double = 1 / n / StepsCount
        Dim X As Double
        Dim k As Integer

        For i As Integer = 1 To StepsCount
            If i = StepsCount Then
                k = 1
            ElseIf i Mod 2 = 0 Then
                k = 4
            Else
                k = 2
            End If

            X = ind / n + [Step] * i
            If Math.Abs(X) < 0.01 Then X = 0

            Result += k * 1 / Beta(a, b) * Math.Pow(X, a - 1) * Math.Pow(1 - X, b - 1)
        Next

        Result *= [Step] / 3

        Return Result
    End Function

    Public Shared Function Sqr(ByVal x As Double) As Double
        Return x * x
    End Function

    ''' <summary>
    ''' Calculates Gauss function where
    ''' a=Mx, b - sigma
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Gauss(ByVal x As Double, _
                                 ByVal a As Double, _
                                 ByVal b As Double) _
                                    As Double
        Dim Result As Double

        Result = (Math.Exp(-Sqr(x - a) / (2 * Sqr(b)))) / (b * Math.Sqrt(2 * Math.PI))

        Return Result
    End Function

    ''' <summary>
    ''' Calculates gauss function integral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IntGauss(ByVal ind As Integer, _
                                    ByVal n As Integer, _
                                    ByVal a As Double, _
                                    ByVal b As Double) _
                                        As Double
        Dim Result As Double = 0

        Dim StepsCount As Integer = 10
        Dim [Step] As Double = 1 / n / StepsCount
        Dim X As Double
        Dim k As Integer

        For i As Integer = 1 To StepsCount
            If i = StepsCount Then
                k = 1
            ElseIf i Mod 2 = 0 Then
                k = 4
            Else
                k = 2
            End If

            X = ind / n + [Step] * i
            If Math.Abs(X) < 0.01 Then X = 0

            Result += k * Gauss(X, a, b)
        Next

        Result *= [Step] / 3

        Return Result
    End Function

    ''' <summary>
    ''' Calculates power function integral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IntPower(ByVal ind As Integer, _
                                    ByVal n As Integer, _
                                    ByVal a As Double) _
                                        As Double
        Dim Result As Double = 0

        Dim StepsCount As Integer = 10
        Dim [Step] As Double = 1 / n / StepsCount
        Dim X As Double
        Dim k As Integer

        For i As Integer = 1 To StepsCount
            If i = StepsCount Then
                k = 1
            ElseIf i Mod 2 = 0 Then
                k = 4
            Else
                k = 2
            End If

            X = ind / n + [Step] * i
            If Math.Abs(X) < 0.01 Then X = 0

            Result += k * Math.Pow(X, -a) * Math.Pow(0.01, a)
        Next

        Result *= [Step] / 3

        Return Result
    End Function

    ''' <summary>
    ''' If x>0 returns x otherwise returns zero
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ZeroAbs(ByVal x As Double) As Double
        Dim Result As Double

        If x < 0 Then
            Result = 0
        Else
            Result = x
        End If

        Return Result
    End Function

    ''' <summary>
    ''' If x>0 returns 1 otherwise returns zero
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ZeroOne(ByVal x As Double) As Double
        Dim Result As Double

        If x < 0 Then
            Result = 0
        Else
            Result = 1
        End If

        Return Result
    End Function

    Public Shared Function BF(ByVal time As Double, ByVal alpha As Double, ByVal beta As Double, ByVal th As Double) As Double
        Return (1 - Math.Exp(-alpha * (time - th))) / (1 + beta * Math.Exp(-alpha * (time - th)))
    End Function

End Class
