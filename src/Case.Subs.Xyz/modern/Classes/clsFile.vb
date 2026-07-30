Imports System.IO
Imports System.Data

Public Enum eFileType
  isCSV
  isTXT
End Enum

Public Class clsFile

  Private _fileName As String
  Private _s As clsSettings
  Private _sep As String
  Private _paramMap As clsParamMap = Nothing
  Private _catName As String = ""

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="fPath">Filename to Export to</param>
  ''' <param name="s">Settings Class</param>
  ''' <param name="fType">CSV or TXT</param>
  ''' <param name="isWriteMode">False to Read</param>
  ''' <remarks></remarks>
  Public Sub New(fPath As String,
                 s As clsSettings,
                 fType As eFileType,
                 isWriteMode As Boolean,
                 Optional catName As String = "")

    ' Widen Scope
    _fileName = fPath
    _s = s
    If Not String.IsNullOrEmpty(catName) Then _catName = catName

    ' Comma or Tab Delimited
    If fType = eFileType.isCSV Then
      _sep = ","
    Else
      _sep = vbTab
    End If

    If isWriteMode = True Then

      ' Write the Data
      WriteData()

    End If

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Write the Data to Disk
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub WriteData()

    ' Stream Writer
    Using sw As New StreamWriter(_fileName, False)

      ' Write Header
      If _s.ConfigFile.DictParamMaps.TryGetValue(_catName, _paramMap) Then

        ' With Headers
        sw.WriteLine("ElementID" & _sep &
                            "P[" & _paramMap.Param_P & "]" & _sep &
                            "N[" & _paramMap.Param_N & "]" & _sep &
                            "E[" & _paramMap.Param_E & "]" & _sep &
                            "Z[" & _paramMap.Param_Z & "]" & _sep &
                            "D[" & _paramMap.Param_D & "]" & _sep &
                            "Family Name" & _sep &
                            "Family Type Name" & _sep &
                            "Category" & _sep &
                            "Level" & _sep &
                            "Design Option Set" & _sep &
                            "Design Option" & _sep &
                            "1[" & _paramMap.Param_1 & "]" & _sep &
                            "2[" & _paramMap.Param_2 & "]" & _sep &
                            "3[" & _paramMap.Param_3 & "]" & _sep &
                            "4[" & _paramMap.Param_4 & "]" & _sep &
                            "5[" & _paramMap.Param_5 & "]")

      Else

        ' Missing Headers
        sw.WriteLine("ElementID" & _sep &
                            "P[]" & _sep &
                            "N[]" & _sep &
                            "E[]" & _sep &
                            "Z[]" & _sep &
                            "D[]" & _sep &
                            "Family Name" & _sep &
                            "Family Type Name" & _sep &
                            "Category" & _sep &
                            "Level" & _sep &
                            "Design Option Set" & _sep &
                            "Design Option" & _sep &
                            "1[]" & _sep &
                            "2[]" & _sep &
                            "3[]" & _sep &
                            "4[]" & _sep &
                            "5[]")

      End If

      ' Write Data, Tab Delimited
      For Each dr As DataRow In _s.PointsCollectionDatatable.Rows
        Dim m_lineitem As String = ""
        For i = 0 To dr.Table.Columns.Count - 1
          m_lineitem += dr(i) & _sep
        Next
        sw.WriteLine(m_lineitem)
      Next

    End Using

  End Sub

#End Region

#Region "Public Members"

  ''' <summary>
  ''' Read the Data into a DataTable
  ''' </summary>
  ''' <remarks></remarks>
  Public Function ReadData() As List(Of clsReportingData)

    ' Fresh Table
    Dim m_pts As New List(Of clsReportingData)

    ' Stream Writer
    Using sr As New StreamReader(_fileName)

      Try

        ' The Header is Always Atop
        Dim m_header As String = sr.ReadLine

        ' Read All Records
        Do While sr.Peek() >= 0

          ' Assemble the Item
          m_pts.Add(New clsReportingData(sr.ReadLine, m_header, _sep))

        Loop

      Catch ex As Exception

        MsgBox("Error reading File", MsgBoxStyle.Exclamation, "Error")

      End Try

    End Using

    ' Return the Value
    Return m_pts

  End Function

#End Region

End Class