Namespace Data
  Module UnitConversion

    Public lstFootUnits As String() = New String() {"foots", "foot", "feets", "feet", "ft", "f", "'"}
    Public sFootUnit As String = "'"
    Public lstInchUnits As String() = New String() {"inches", "inchs", "inch", "in", "i", """"}
    Public sInchUnit As String = """"

    ''' <summary>
    ''' Calcualte Feet
    ''' </summary>
    ''' <param name="dFt"></param>
    ''' <param name="dInch"></param>
    ''' <returns></returns>
    Public Function CalculateFt(dFt As Double, dInch As Double) As Double
      Dim dFeet As Double = 0.0
      If dFt >= 0 AndAlso dInch >= 0 AndAlso dInch <= 12 Then
        dFeet = dFt + (dInch / 12)
      End If
      Return dFeet
    End Function

    ''' <summary>
    ''' Calculate Double Length from string representation of a length
    ''' </summary>
    ''' <param name="length"></param>
    ''' <returns></returns>
    Public Function CalculateDoubleLength(length As String) As Double
      Dim isNegative As Boolean = False

      If length.Trim().StartsWith("-") Then
        isNegative = True
      End If

      Dim cleanLength As String = CleanHeight(length)

      cleanLength = length.Replace("-", "")

      cleanLength = cleanLength.Replace(sFootUnit, "|")
      cleanLength = cleanLength.Replace(sInchUnit, "|")

      Dim sParts As String() = cleanLength.Split("|"c)

      Dim dFeet As Double = 0.0
      Dim dInches As Double = 0.0
      Dim dFeetParsed As Double
      Dim dInchesParsed As Double

      If sParts.Length >= 2 AndAlso Double.TryParse(sParts(0).Trim(), dFeetParsed) Then
        dFeet = dFeetParsed
      End If

      If sParts.Length >= 3 AndAlso sParts(1).Trim().Contains("/") Then
        Dim iParts As String() = sParts(1).Trim().Split(" "c)

        dInches = 0.0
        Dim dInch1 As Double, dInch2 As Double, dInch3 As Double

        If UBound(iParts) > 3 AndAlso Double.TryParse(iParts(0).Trim(), dInch1) AndAlso Double.TryParse(iParts(1).Trim(), dInch2) AndAlso Double.TryParse(iParts(3).Trim(), dInch3) Then
          dInches = dInch1 + (dInch2 / dInch3)
        ElseIf UBound(iParts) = 3 AndAlso Double.TryParse(iParts(0).Trim(), dInch1) AndAlso Double.TryParse(iParts(2).Trim(), dInch2) Then
          dInches = dInch1 / dInch2
        End If
      ElseIf sParts.Length >= 3 AndAlso Double.TryParse(sParts(1).Trim(), dInchesParsed) Then
        dInches = dInchesParsed
      End If

      If dFeet >= 0 AndAlso dInches >= 0 AndAlso dInches <= 12 Then
        dFeet = dFeet + (dInches / 12)
      ElseIf dFeet >= 0 AndAlso dInches > 12 Then
        dFeet = dFeet + (dInches / 12)
      End If

      ' Return as negative?
      If isNegative Then dFeet *= -1

      Return dFeet

    End Function

    ''' <summary>
    ''' Clean Unit
    ''' </summary>
    ''' <param name="sOriginal"></param>
    ''' <param name="lstReplaceUnits"></param>
    ''' <param name="sReplaceWithUnit"></param>
    ''' <returns></returns>
    Private Function CleanUnit(sOriginal As String, lstReplaceUnits As String(), sReplaceWithUnit As String) As String
      Dim m_isNegative As Boolean = False

      If sOriginal.StartsWith("-", StringComparison.CurrentCulture) Then
        m_isNegative = True
        sOriginal = sOriginal.Remove(0, 1)
      End If

      Dim m_sbPattern As New System.Text.StringBuilder()

      For Each x As String In lstReplaceUnits
        If m_sbPattern.Length > 0 Then
          m_sbPattern.Append("|")
        End If
        m_sbPattern.Append(x)
      Next

      m_sbPattern.Insert(0, "(^|\s)(")
      m_sbPattern.Append(")(\s|$)")

      Dim rReplace As New System.Text.RegularExpressions.Regex(m_sbPattern.ToString(), System.Text.RegularExpressions.RegexOptions.IgnoreCase)

      sOriginal = rReplace.Replace(sOriginal, sReplaceWithUnit)

      If Not sOriginal.Contains("'") AndAlso Not sOriginal.Contains("""") Then
        sOriginal = sOriginal & " """
      End If

      If Not sOriginal.Contains("'") AndAlso sReplaceWithUnit = sFootUnit Then
        sOriginal = "0' " & sOriginal
      End If

      If Not sOriginal.Contains("""") AndAlso sReplaceWithUnit = sInchUnit Then
        sOriginal = sOriginal & " 0"""
      End If

      If sOriginal.Contains("'") AndAlso Not sOriginal.Contains("-") Then
        sOriginal = sOriginal.Replace("'", "'-")
      End If

      If m_isNegative AndAlso Not sOriginal.StartsWith("-", StringComparison.CurrentCulture) Then
        sOriginal = "- " & sOriginal
      End If


      Return sOriginal
    End Function

    ''' <summary>
    ''' Does a string have numbers?
    ''' </summary>
    ''' <param name="sText">String to test</param>
    ''' <returns>True if numbers found</returns>
    Private Function StringHasNumbers(sText As String) As Boolean
      Dim rxNumbers As New System.Text.RegularExpressions.Regex("[0-9]+")
      Return rxNumbers.IsMatch(sText)
    End Function

    ''' <summary>
    ''' Reduce Spaces
    ''' </summary>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    Private Function ReduceSpaces(txt As String) As String
      While txt.Contains("  ")
        txt = txt.Replace("  ", " ")
      End While
      Return txt
    End Function

    ''' <summary>
    ''' Reduce More Spaces
    ''' </summary>
    ''' <param name="sText"></param>
    ''' <returns></returns>
    Private Function ReduceMoreSpaces(sText As String) As String
      If sText.Contains(" '") Then
        sText = sText.Replace(" '", "'")
      End If

      If sText.Contains(" """) Then
        sText = sText.Replace(" """, """")
      End If

      Return sText
    End Function

    ''' <summary>
    ''' Separate Numbers
    ''' </summary>
    ''' <param name="sText"></param>
    ''' <returns></returns>
    Private Function SeparateNumbers(sText As String) As String
      Dim m_isNumber As Boolean = False
      If Not [String].IsNullOrEmpty(sText) Then
        For iChar As Integer = 0 To sText.Length - 1
          Dim bIsNumber As Boolean = (sText(iChar) >= "0"c _
                                      AndAlso sText(iChar) <= "9"c) OrElse (sText(iChar) = "."c _
                                                                            AndAlso iChar < sText.Length - 1 _
                                                                            AndAlso sText(iChar + 1) >= "0"c _
                                                                            AndAlso sText(iChar + 1) <= "9"c)

          If iChar > 0 AndAlso bIsNumber <> m_isNumber Then
            sText = sText.Insert(iChar, " ")
            iChar += 1
          End If

          m_isNumber = bIsNumber
        Next
      End If

      Return sText
    End Function

    ''' <summary>
    ''' Clean Height
    ''' </summary>
    ''' <param name="sHeight"></param>
    ''' <returns></returns>
    Public Function CleanHeight(sHeight As String) As String

      ' Height Fix
      If StringHasNumbers(sHeight) Then
        sHeight = SeparateNumbers(sHeight)
        sHeight = CleanUnit(sHeight, lstFootUnits, sFootUnit)
        sHeight = CleanUnit(sHeight, lstInchUnits, sInchUnit)
        sHeight = SeparateNumbers(sHeight)
        sHeight = ReduceSpaces(sHeight)
      Else
        sHeight = ""
      End If

      Return sHeight
    End Function

    ''' <summary>
    ''' String Representation of a Length Expression
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function DisplayFormat(value As String) As String

      ' Fix String into proper Length representation
      If StringHasNumbers(value) Then
        value = SeparateNumbers(value)
        value = CleanUnit(value, lstFootUnits, sFootUnit)
        value = CleanUnit(value, lstInchUnits, sInchUnit)
        value = SeparateNumbers(value)
        value = ReduceSpaces(value)
        value = ReduceMoreSpaces(value)
      Else
        value = ""
      End If

      Return value
    End Function

  End Module
End Namespace