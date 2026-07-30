Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Reflection
Imports System.Windows.Forms

Public Class clsExcel

  Private _fileName As String
  Private _s As clsSettings
  Private _excelApp As Excel.Application
  Private _excelWorkbook As Excel.Workbook
  Private _excelOpenPrevious As Boolean
  Private _dt As DataTable

#Region "Public Properties"

  ''' <summary>
  ''' Worksheets
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property WorkSheetNames As List(Of String)

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="fPath"></param>
  ''' <param name="s"></param>
  ''' <param name="wMode"></param>
  ''' <remarks></remarks>
  Public Sub New(fPath As String,
                 s As clsSettings,
                 Optional wMode As Boolean = True)

    ' Widen Scope
    _fileName = fPath
    _s = s

    ' Setup
    doSetup(wMode)

  End Sub

  ''' <summary>
  ''' Setup
  ''' </summary>
  ''' <param name="p_writMode"></param>
  ''' <remarks></remarks>
  Private Sub doSetup(p_writMode As Boolean)

    ' Fresh List
    WorkSheetNames = New List(Of String)

    Try

      If p_writMode = True Then

        ' Copy the Template File
        Dim m_ret As String = CopyTemplateAsNew()
        If Not String.IsNullOrEmpty(m_ret) Then
          MsgBox(m_ret, MsgBoxStyle.Critical, "Cannot Continue Export")
          Exit Sub
        End If

        ' Datatable
        _dt = _s.PointsCollectionDatatable

      End If

      ' Start Excel
      If StartExcel() = False Then
        MsgBox("Failed to Start Excel", MsgBoxStyle.Critical, "Cannot Continue")
      End If

      ' Get the Worksheet Names
      GetWorksheetNames()

    Catch
    End Try

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Get the List of Worksheet Names
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetWorksheetNames()
    Try
      For Each ws As Excel.Worksheet In _excelWorkbook.Worksheets
        Try
          WorkSheetNames.Add(ws.Name)
        Catch
        End Try
      Next
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Copy the Template as New
  ''' </summary>
  ''' <remarks></remarks>
  Private Function CopyTemplateAsNew() As String

    ' Current Path
    Dim m_p As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\CASE_PointsManager_Template.xlsx"

    ' Does the Template Exist?
    If Not File.Exists(m_p) Then
      Return "Template File Not Found!" & vbCr & m_p
    Else
      Try
        ' Copy as New
        File.Copy(m_p, _fileName, True)

      Catch

        Return "Failed to Create New Export File"

      End Try

    End If

    Return ""

  End Function

#End Region

#Region "Public Members - Excel Functions"

  ''' <summary>
  ''' Fill from Excel column
  ''' </summary>
  ''' <param name="nameColumn">Name of the column</param>
  ''' <returns>True on success</returns>
  ''' <remarks></remarks>
  Public Function FillDataTableFromExcelSheetNames(ByVal nameColumn As String) As Boolean
    Dim worksheet As Excel.Worksheet
    Dim row As DataRow = Nothing
    Try
      _dt = New System.Data.DataTable()
      _dt.Columns.Add(nameColumn, GetType(String))
      For i As Integer = 1 To _excelWorkbook.Sheets.Count
        'note index starts at 1
        worksheet = DirectCast(_excelWorkbook.Sheets(i), Excel.Worksheet)
        row = _dt.NewRow()
        row(0) = worksheet.Name
        _dt.Rows.Add(row)
      Next
      Return True
    Catch
      Return False
    End Try
  End Function

  ''' <summary>
  ''' Fill from Excel Worksheet
  ''' </summary>
  ''' <param name="nameWorksheet">Worksheet name</param>
  ''' <param name="isPlacement">True requires values for C, D, E only</param>
  ''' <returns>Datatable of resulting data</returns>
  ''' <remarks></remarks>
  Public Function FillDataTableFromExcelWorksheet(ByVal nameWorksheet As String,
                                                  Optional isPlacement As Boolean = False) As DataTable

    Dim iCol As Integer
    Dim iRow As Integer
    Dim countColumns As Integer = 0

    ' Fresh table
    _dt = New DataTable

    Try

      ' Get the column headings from the first row of the spreadsheet and create the columns
      Dim m_ws As Excel.Worksheet
      m_ws = DirectCast(_excelWorkbook.Sheets(nameWorksheet), Excel.Worksheet)
      _dt = New System.Data.DataTable
      iCol = 1
      Dim m_range As Excel.Range

      m_range = DirectCast(m_ws.Cells(1, 1), Excel.Range)
      While m_range.Value2 IsNot Nothing
        _dt.Columns.Add(m_range.Value2.ToString, GetType(String))
        countColumns = iCol
        iCol += 1
        m_range = DirectCast(m_ws.Cells(1, iCol), Excel.Range)
      End While

      ' Continue with remaining rows and add the data to the table
      iRow = 2
      ' Is this for placement?
      If isPlacement = True Then

        ' Ok Item
        Dim itemOK As Boolean = True

        ' Columns 3, 4, and 5 cannot be blank
        If DirectCast(m_ws.Cells(iRow, 3), Excel.Range).Value2 Is Nothing Then itemOK = False
        If DirectCast(m_ws.Cells(iRow, 4), Excel.Range).Value2 Is Nothing Then itemOK = False
        If DirectCast(m_ws.Cells(iRow, 5), Excel.Range).Value2 Is Nothing Then itemOK = False

        ' Add if Item Ok
        While itemOK = True

          ' New Row
          Dim m_row As System.Data.DataRow
          m_row = _dt.NewRow()
          For i = 1 To countColumns - 1
            Try
              m_row(i) = DirectCast(m_ws.Cells(iRow, i), Excel.Range).Value2.ToString
            Catch ex As Exception

            End Try
          Next

          ' Add the Row
          _dt.Rows.Add(m_row)

          ' Next Excel Row
          iRow += 1
          If DirectCast(m_ws.Cells(iRow, 3), Excel.Range).Value2 Is Nothing Then itemOK = False
          If DirectCast(m_ws.Cells(iRow, 4), Excel.Range).Value2 Is Nothing Then itemOK = False
          If DirectCast(m_ws.Cells(iRow, 5), Excel.Range).Value2 Is Nothing Then itemOK = False

        End While

      Else

        ' First Column Required
        m_range = DirectCast(m_ws.Cells(iRow, 1), Excel.Range)

        ' Check Each Row
        While m_range.Value2 IsNot Nothing
          Dim m_row As System.Data.DataRow
          m_row = _dt.NewRow()
          m_row(0) = m_range.Value2.ToString
          For i As Integer = 1 To countColumns - 1
            m_range = DirectCast(m_ws.Cells(iRow, i + 1), Excel.Range)
            If m_range.Value2 IsNot Nothing Then
              m_row(i) = m_range.Value2.ToString
            End If
          Next

          ' Add the Row
          _dt.Rows.Add(m_row)

          ' Next Excel Row
          iRow += 1
          m_range = DirectCast(m_ws.Cells(iRow, 1), Excel.Range)

        End While

      End If

      Return _dt

    Catch ex As Exception

      ' Return Nothing along with an Error Message
      MsgBox("Error Reading Excel Worksheet." & vbCr &
             "System message:" & vbCr &
             ex.Message,
             MsgBoxStyle.Critical,
             "Error")
      Return Nothing

    End Try
  End Function

  ''' <summary>
  ''' Fill Excel Worksheet from Table
  ''' </summary>
  ''' <param name="nameWorksheet">Worksheet name</param>
  ''' <returns>True on success</returns>
  ''' <remarks></remarks>
  Public Function FillExcelWorksheetFromDataTable(ByVal nameWorksheet As String, Optional isNewExport As Boolean = False) As Boolean

    ' Localized Variables
    Dim range As Excel.Range
    Dim worksheet As Excel.Worksheet

    Try
      ' What is the Worksheet Name
      If isNewExport = True Then
        worksheet = DirectCast(_excelWorkbook.Sheets("TEMPLATE"), Excel.Worksheet)
      Else
        worksheet = DirectCast(_excelWorkbook.Sheets(nameWorksheet), Excel.Worksheet)
      End If

      ' Write Headers
      For indexColumn As Integer = 0 To _dt.Columns.Count - 1
        range = DirectCast(worksheet.Cells(1, indexColumn + 1), Excel.Range)

        ' Get Map from Category Name - nameWorksheet
        Dim m_map As clsParamMap = Nothing

        If _s.ConfigFile.DictParamMaps.TryGetValue(nameWorksheet, m_map) Then

          ' P
          Try
            If indexColumn = 1 Then
              range.Value2 = "P[" & m_map.Param_P & "]"
            End If
          Catch ex As Exception
          End Try
          ' N
          Try
            If indexColumn = 2 Then
              range.Value2 = "N[" & m_map.Param_N & "]"
            End If
          Catch ex As Exception
          End Try
          ' E
          Try
            If indexColumn = 3 Then
              range.Value2 = "E[" & m_map.Param_E & "]"
            End If
          Catch ex As Exception
          End Try
          ' Z
          Try
            If indexColumn = 4 Then
              range.Value2 = "Z[" & m_map.Param_Z & "]"
            End If
          Catch ex As Exception
          End Try
          ' D
          Try
            If indexColumn = 5 Then
              range.Value2 = "D[" & m_map.Param_D & "]"
            End If
          Catch ex As Exception
          End Try
          ' 1
          Try
            If indexColumn = 12 Then
              range.Value2 = "1[" & m_map.Param_1 & "]"
            End If
          Catch ex As Exception
          End Try
          ' 2
          Try
            If indexColumn = 13 Then
              range.Value2 = "2[" & m_map.Param_2 & "]"
            End If
          Catch ex As Exception
          End Try
          ' 3
          Try
            If indexColumn = 14 Then
              range.Value2 = "3[" & m_map.Param_3 & "]"
            End If
          Catch ex As Exception
          End Try
          ' 4
          Try
            If indexColumn = 15 Then
              range.Value2 = "4[" & m_map.Param_4 & "]"
            End If
          Catch ex As Exception
          End Try
          ' 5
          Try
            If indexColumn = 16 Then
              range.Value2 = "5[" & m_map.Param_5 & "]"
            End If
          Catch ex As Exception
          End Try

        End If

      Next

      Dim indexRow As Integer = 1
      For Each dataRow As System.Data.DataRow In _dt.Rows
        For indexColumn As Integer = 0 To _dt.Columns.Count - 1
          range = DirectCast(worksheet.Cells(indexRow + 1, indexColumn + 1), Excel.Range)
          If dataRow(indexColumn).ToString IsNot Nothing Then
            range.Value2 = dataRow(indexColumn).ToString
          End If
        Next
        indexRow += 1
      Next

      ' No Need to Rename if an Update
      If isNewExport = False Then Return True

      Try
        ' Rename the Worksheet
        worksheet.Name = nameWorksheet
        _excelWorkbook.Save()

      Catch

      End Try

      Return True
    Catch
      Return False
    End Try
  End Function

#End Region

#Region "Public Members - Excel Startup and Shutdown"

  ''' <summary>
  ''' Start the Excel Application
  ''' </summary>
  ''' <returns>True on success</returns>
  ''' <remarks></remarks>
  Public Function StartExcel() As Boolean

    If Not File.Exists(_fileName) Then
      MessageBox.Show("Unable to locate file: " & Convert.ToString(_fileName), "Error")
      Return False
    End If

    Try
      'Either finds running instance or starts a new one; failure to find file causes exception
      _excelWorkbook = DirectCast(System.Runtime.InteropServices.Marshal.BindToMoniker(_fileName), Excel.Workbook)
      _excelApp = _excelWorkbook.Application

      'Test below not very logical but, so far, the only way to determine if Excel was running
      If _excelWorkbook.Application.ActiveWorkbook Is Nothing Then
        _excelOpenPrevious = False
      Else
        _excelOpenPrevious = True
      End If
      _fileName = _fileName.Substring(_fileName.LastIndexOf("\") + 1)
      _excelWorkbook.Application.Windows(_fileName).Visible = True
      'm_ExcelWorkbook.Application.Visible = true;
      Return True
    Catch ex As Exception
      MessageBox.Show("Unable to find or start Excel Interop session with file: " & _
                      Convert.ToString(_fileName) & ". " & vbLf & vbLf & _
                      "System Error Message: " & vbLf & Convert.ToString(ex), "Error")
      Return False
    End Try
  End Function

  ''' <summary>
  ''' Terminate the Excel Application
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub ShutDownExcel()
    Try
      If Not _excelOpenPrevious Then
        If _excelApp IsNot Nothing Then
          If _excelWorkbook IsNot Nothing Then
            _excelWorkbook.Save()
          End If
          If _excelApp IsNot Nothing Then
            _excelApp.Quit()
          End If
        End If
      End If
      _excelWorkbook = Nothing
      _excelApp = Nothing
      GC.Collect()
      'Don't do anything; we're just trying to clean up
    Catch
    End Try
  End Sub

#End Region

End Class