Imports System.Collections.Generic

Public Class MathExpression

#Region "Const"

    Public Const TIME As String = "time"

#End Region

#Region "Testing Brackets"

    ''' <summary>
    ''' Used to test if brackets in given expressin are correct
    ''' </summary>
    ''' <param name="expression">string with expression to be tested</param>
    ''' <returns>true if correct, false if expression is wrong</returns>
    Public Shared Function TestBrackets(ByVal expression As String) As Boolean
        Dim Brackets As String
        Brackets = String.Empty

        For i As Integer = 0 To expression.Length - 1
            If expression(i) = "(" Or expression(i) = ")" Then
                Brackets += expression(i)
            End If
        Next
        While Brackets.Contains("()")
            Brackets = Brackets.Remove(Brackets.IndexOf("()"), 2)
        End While

        Return Brackets.Length = 0

    End Function

#End Region

#Region "Testing expression"

    ''' <summary>
    ''' Used to test if given expression is correct
    ''' </summary>
    ''' <param name="expression">string with expression to be tested</param>
    ''' <returns>true if correct, false if expression is wrong</returns>
    ''' <remarks></remarks>
    Public Shared Function TestExpression(ByVal expression As String, _
                                          ByVal allNumericFields As List(Of String)) _
                                            As Boolean

        If String.IsNullOrEmpty(expression) Then Return False

        expression = FixDouble(expression)

        Dim Result As Boolean = True

        Dim EvalExpression As New MathExpression()
        EvalExpression.m_Primatives = New Hashtable
        '1 - replace all numeric fields with "0"
        If allNumericFields IsNot Nothing Then
            For Each FieldTag As String In allNumericFields
                If Not EvalExpression.m_Primatives.ContainsKey(FieldTag) Then
                    EvalExpression.m_Primatives.Add(FieldTag, "0")
                End If
            Next
        End If
        '2 - evaluate
        Try
            EvalExpression.EvaluateExpression(expression)
        Catch
            Result = False
        End Try

        Return Result
    End Function

    ''' <summary>
    ''' Used to test if given expression is correct
    ''' </summary>
    ''' <param name="expression">string with expression to be tested</param>
    ''' <returns>true if correct, false if expression is wrong</returns>
    ''' <remarks></remarks>
    Public Shared Function TestTimeExpression(ByVal expression As String) _
                                                As Boolean

        If String.IsNullOrEmpty(expression) Then Return False

        expression = FixDouble(expression)

        Dim Result As Boolean = True

        Dim EvalExpression As New MathExpression()
        EvalExpression.m_Primatives = New Hashtable

        '1 - replace time field with "0"
        If Not EvalExpression.m_Primatives.ContainsKey(TIME) Then
            EvalExpression.m_Primatives.Add(TIME, "0")
        End If
        '2 - evaluate
        Try
            EvalExpression.EvaluateExpression(expression)
        Catch
            Result = False
        End Try

        Return Result
    End Function

#End Region

    ''' <summary>
    ''' Evaluates given math expression. 
    ''' Supported functions: sin, cos, tan, sqrt, abs, exp, log, factorial, zeroabs, zeroone.
    ''' Supported operations: +, -, /, \, %, *, (, ).
    ''' Other supported primitives: time vaiable (see const region how to write it in expression)
    ''' </summary>
    ''' <param name="expression">Math expression to be evaluated</param>
    ''' <param name="timeValue">Time to be put into expression</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EvaluateTimeExpression(ByVal expression As String, _
                                                  ByVal timeValue As Double) _
                                                    As Double

        Dim EvalExpression As New MathExpression()
        EvalExpression.m_Primatives = New Hashtable

        '1 - replace time field with its value
        If Not EvalExpression.m_Primatives.ContainsKey(TIME) Then
            EvalExpression.m_Primatives.Add(TIME, timeValue)
        End If

        '2 - evaluate
        Dim Result As Double
        Try
            Result = EvalExpression.EvaluateExpression(expression)
        Catch
            Result = 0
        End Try

        Return Result
    End Function

    ''' <summary>
    ''' Used to fix string representation of double value with current decimal separator
    ''' </summary>
    ''' <param name="doubleValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FixDouble(ByVal doubleValue As String) As String
        If String.IsNullOrEmpty(doubleValue) Then Return String.Empty

        Dim DecimalSeparator As String
        DecimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        If DecimalSeparator <> "." Then
            While doubleValue.Contains(".")
                doubleValue = doubleValue.Replace(".", DecimalSeparator)
            End While
        End If

        If DecimalSeparator <> "," Then
            While doubleValue.Contains(",")
                doubleValue = doubleValue.Replace(",", DecimalSeparator)
            End While
        End If

        Return doubleValue
    End Function

#Region "Constructors"

    Public Sub New()
        m_Primatives = New Hashtable
    End Sub

    Public Sub New(ByVal primitives As Dictionary(Of String, String))
        Me.New()

        If primitives IsNot Nothing AndAlso primitives.Count > 0 Then
            For Each key As String In primitives.Keys
                Dim Value As String = primitives.Item(key).ToString
                Value = FixDouble(Value)
                m_Primatives.Add(key, Value)
            Next
        End If
    End Sub

#End Region

#Region "Evaluate expression"

#Region "about"

    ''' Find the higher precedence operator in the expression. 
    ''' Break the expression into the operator's operands 
    ''' (for example, in 1 + 3 the operator is + and the operands are 1 and 3). 
    ''' Recursively call the EvaluateExpression function to evaluate the operands 
    ''' and combine the results using the correct Visual Basic operator. 
    ''' 
    '''A couple details require some thought. 
    '''First, keep track of the number of open parentheses. 
    '''If an operator is inside open parentheses, it does not have the highest precedence. 
    '''
    '''Second, the code needs to watch for unary operators as in +12 and 13 + -6. 
    '''Where the function encounters a + or - determines whether it is unary. 
    '''For example, in 13 + -6 the - is unary but the + is not. 
    '''
    ''' NOTE! All double vues MUST be in 1.1 format (1,1 will not be parsed)
#End Region

    ''' <summary>
    ''' Contians pairs textName-value (string) which would be parsed while evaluating expression
    ''' </summary>
    Private m_Primatives As Hashtable

    Private Enum Precedence
        None = 11
        Unary = 10      ' Not actually used.
        Power = 9
        Times = 8
        Div = 7
        IntDiv = 6
        Modulus = 5
        Plus = 4
    End Enum

    ''' <summary>
    ''' Evaluates the expression.
    ''' Supported functions: sin, cos, tan, sqrt, abs, exp, log, factorial.
    ''' Supported operations: +, -, /, \, %, *, (, ).
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EvaluateExpression(ByVal expression As String) As Double
        Dim expr As String
        Dim is_unary As Boolean
        Dim next_unary As Boolean
        Dim parens As Integer
        Dim expr_len As Integer
        Dim ch As String
        Dim lexpr As String
        Dim rexpr As String
        Dim status As Long
        Dim best_pos As Integer
        Dim best_prec As Precedence

        ' Remove all spaces.
        expr = expression.Replace(" ", "")
        expr = FixDouble(expr)
        expr_len = Len(expr)
        If expr_len = 0 Then Return 0

        ' If we find + or - now, it is a unary operator.
        is_unary = True

        ' So far we have nothing.
        best_prec = Precedence.None

        ' Find the operator with the lowest precedence.
        ' Look for places where there are no open
        ' parentheses.
        For pos As Integer = 0 To expr_len - 1
            ' Examine the next character.
            ch = expr.Substring(pos, 1)

            ' Assume we will not find an operator. In
            ' that case, the next operator will not
            ' be unary.
            next_unary = False

            If ch = " " Then
                ' Just skip spaces. We keep them here
                ' to make the error messages easier to
            ElseIf ch = "(" Then
                ' Increase the open parentheses count.
                parens += 1

                ' A + or - after "(" is unary.
                next_unary = True
            ElseIf ch = ")" Then
                ' Decrease the open parentheses count.
                parens -= 1

                ' An operator after ")" is not unary.
                next_unary = False

                ' If parens < 0, too many ')'s.
                If parens < 0 Then
                    Dim ErrorText As String
                    ErrorText = "Expression has too many close parentheses"
                    Throw New FormatException(ErrorText)
                End If
            ElseIf parens = 0 Then
                ' See if this is an operator.
                If ch = "^" Or ch = "*" Or _
                   ch = "/" Or ch = "\" Or _
                   ch = "%" Or ch = "+" Or _
                   ch = "-" _
                Then
                    ' An operator after an operator
                    ' is unary.
                    next_unary = True

                    ' See if this operator has higher
                    ' precedence than the current one.
                    Select Case ch
                        Case "^"
                            If best_prec >= Precedence.Power Then
                                best_prec = Precedence.Power
                                best_pos = pos
                            End If

                        Case "*", "/"
                            If best_prec >= Precedence.Times Then
                                best_prec = Precedence.Times
                                best_pos = pos
                            End If

                        Case "\"
                            If best_prec >= Precedence.IntDiv Then
                                best_prec = Precedence.IntDiv
                                best_pos = pos
                            End If

                        Case "%"
                            If best_prec >= Precedence.Modulus Then
                                best_prec = Precedence.Modulus
                                best_pos = pos
                            End If

                        Case "+", "-"
                            ' Ignore unary operators
                            ' for now.
                            If (Not is_unary) And _
                                best_prec >= Precedence.Plus _
                            Then
                                best_prec = Precedence.Plus
                                best_pos = pos
                            End If
                    End Select
                End If
            End If
            is_unary = next_unary
        Next pos

        ' If the parentheses count is not zero,
        ' there's a ')' missing.
        If parens <> 0 Then
            Dim ErrorText As String
            ErrorText = String.Format("Missing close parenthesis on expression {0}", expression)
            Throw New FormatException(ErrorText)
        End If

        ' Hopefully we have the operator.
        If best_prec < Precedence.None Then
            lexpr = expr.Substring(0, best_pos)
            rexpr = expr.Substring(best_pos + 1)
            Select Case expr.Substring(best_pos, 1)
                Case "^"
                    Return _
                        EvaluateExpression(lexpr) ^ _
                        EvaluateExpression(rexpr)
                Case "*"
                    Return _
                        EvaluateExpression(lexpr) * _
                        EvaluateExpression(rexpr)
                Case "/"
                    Return _
                        EvaluateExpression(lexpr) / _
                        EvaluateExpression(rexpr)
                Case "\"
                    Return _
                        CLng(EvaluateExpression(lexpr)) \ _
                        CLng(EvaluateExpression(rexpr))
                Case "%"
                    Return _
                        EvaluateExpression(lexpr) Mod _
                        EvaluateExpression(rexpr)
                Case "+"
                    Return _
                        EvaluateExpression(lexpr) + _
                        EvaluateExpression(rexpr)
                Case "-"
                    Return _
                        EvaluateExpression(lexpr) - _
                        EvaluateExpression(rexpr)
            End Select
        End If

        ' If we do not yet have an operator, there
        ' are several possibilities:
        '
        ' 1. expr is (expr2) for some expr2.
        ' 2. expr is -expr2 or +expr2 for some expr2.
        ' 3. expr is Fun(expr2) for a function Fun.
        ' 4. expr is a primitive.
        ' 5. It's a literal like "3.14159".

        ' Look for (expr2).
        If expr.StartsWith("(") And expr.EndsWith(")") Then
            ' Remove the parentheses.
            Return EvaluateExpression(expr.Substring(1, expr_len - 2))
            Exit Function
        End If

        ' Look for -expr2.
        If expr.StartsWith("-") Then
            Return -EvaluateExpression(expr.Substring(1))
        End If

        ' Look for +expr2.
        If expr.StartsWith("+") Then
            Return EvaluateExpression(expr.Substring(2))
        End If

        ' Look for Fun(expr2).
        If expr_len > 5 And expr.EndsWith(")") Then
            ' Find the first (.
            Dim paren_pos As Integer = expr.IndexOf("(")
            If paren_pos > 0 Then
                ' See what the function is.
                lexpr = expr.Substring(0, paren_pos)
                rexpr = expr.Substring(paren_pos + 1, expr_len - paren_pos - 2)
                Select Case lexpr.ToLower
                    Case "sin"
                        Return Math.Sin(EvaluateExpression(rexpr))
                    Case "cos"
                        Return Math.Cos(EvaluateExpression(rexpr))
                    Case "tan"
                        Return Math.Tan(EvaluateExpression(rexpr))
                    Case "sqrt"
                        Return Math.Sqrt(EvaluateExpression(rexpr))
                    Case "abs"
                        Return Math.Abs(EvaluateExpression(rexpr))
                    Case "exp"
                        Return Math.Exp(EvaluateExpression(rexpr))
                    Case "log"
                        Return Math.Log(EvaluateExpression(rexpr))
                    Case "factorial"
                        Return Factorial(EvaluateExpression(rexpr))
                    Case "zeroabs"
                        Return MathFunctions.ZeroAbs(EvaluateExpression(rexpr))
                    Case "zeroone", "o"
                        Return MathFunctions.ZeroOne(EvaluateExpression(rexpr))
                        ' Add other functions (including
                        ' program-defined functions) here.
                End Select
            End If
        End If

        ' See if it's a primitive.
        If m_Primatives.Contains(expr) Then
            ' Return the corresponding value,
            ' converted into a Double.
            Try
                ' Try to convert the expression into a value.
                Dim value As Double = _
                    Double.Parse(m_Primatives.Item(expr).ToString)
                Return value
            Catch ex As Exception
                Dim ErrorText As String
                Dim Args As String() = {expr, m_Primatives.Item(expr).ToString}
                ErrorText = String.Format("Expression has wrong value {0}", Args)
                Throw New FormatException(ErrorText)
            End Try
        End If

        ' It must be a literal like "2.71828".
        Try
            ' Try to convert the expression into a Double.
            Dim value As Double = Double.Parse(expr)
            Return value
        Catch ex As Exception
            Dim ErrorText As String
            ErrorText = String.Format("Error evaluating constant {0}", expression)
            Throw New FormatException(ErrorText)
        End Try
    End Function

    ''' <summary>
    ''' Returns the factorial of the expression.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Factorial(ByVal value As Double) As Double
        Dim result As Double

        ' Make sure the value is an integer.
        If CLng(value) <> value Then
            Throw New ArgumentException( _
                "Parameter to Factorial function must be an integer in Factorial(" & _
                Microsoft.VisualBasic.Strings.Format$(value) & ")")
        End If

        result = 1
        Do While value > 1
            result = result * value
            value = value - 1
        Loop
        Return result
    End Function

#End Region


End Class
