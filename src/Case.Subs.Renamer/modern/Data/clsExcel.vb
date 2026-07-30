Imports System.Data
Imports System.IO
Imports System.Reflection
Imports Microsoft.Office.Interop

Namespace Data

  Public Enum EnumExcelWriteMode
    IsReadOnly
    IsWrite
  End Enum

  Public Class clsExcel

    Private _eMode As EnumExcelWriteMode
    Private _fileName As String
    Private _s As clsSettings
    Private _dt As DataTable
    Private _excelApp As Excel.Application
    Private _excelWorkbook As Excel.Workbook
    Private _excelAppExisting As Boolean

#Region "Public Properties"

    ''' <summary>
    ''' Cannot Continue on Failure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFailed As Boolean

    ''' <summary>
    ''' List of Worksheets in Workbook
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WorkSheetNames As List(Of String)

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="filePath">Excel Workbook File Path</param>
    ''' <param name="s">Settings Object</param>
    ''' <param name="excelMode">Document Mode</param>
    ''' <remarks></remarks>
    Public Sub New(filePath As String,
                   s As clsSettings,
                   excelMode As EnumExcelWriteMode)

      ' Widen Scope
      _fileName = filePath
      _s = s
      _eMode = ExcelMode

      ' Setup
      Setup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      Try

        ' Fresh List and Settings
        isFailed = True
        WorkSheetNames = New List(Of String)
        Dim m_excelPathChecker As String = ""

        ' Excel Mode
        Select Case _eMode

          Case EnumExcelWriteMode.IsReadOnly

            ' File Exists?
            If Not File.Exists(_fileName) Then

              ' Failure
              MsgBox("Could not Find File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Import")

            Else

              ' Start Excel
              If StartExcel() = True Then

                ' Get the Worksheet Names
                GetWorksheetNames()

                ' Success
                isFailed = False

              End If

            End If


          Case EnumExcelWriteMode.IsWrite

            ' Copy the Template File
            m_excelPathChecker = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) &
                                 "\Case.Subs.Renamer.xlsx"

            If Not File.Exists(m_excelPathChecker) Then

              ' Failure
              MsgBox("Missing Excel Template File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Export")

            Else

              ' Copy and Open Workbook as New
              File.Copy(m_excelPathChecker, _fileName, True)

              ' Start Excel
              If StartExcel() = True Then

                ' Get the Worksheet Names
                GetWorksheetNames()

                ' Success
                isFailed = False

              End If

            End If

        End Select

      Catch
      End Try

    End Sub

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

#End Region

#Region "Public Members - Excel Functions"

    ''' <summary>
    ''' Fill from Excel column
    ''' </summary>
    ''' <param name="nameColumn">Name of the column</param>
    ''' <returns>True on success</returns>
    ''' <remarks></remarks>
    Public Function FillDataTableBySheetName(ByVal nameColumn As String) As Boolean
      Dim worksheet As Excel.Worksheet
      Dim row As DataRow
      Try
        _dt = New DataTable()
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
    ''' <param name="wsName">Worksheet name</param>
    ''' <returns>Datatable of resulting data</returns>
    ''' <remarks></remarks>
    Public Function FillDataTableFromExcelWorksheet(ByVal wsName As String) As DataTable

      Dim m_iCol As Integer
      Dim m_iRow As Integer
      Dim m_iCntColumns As Integer = 0

      ' Fresh table
      _dt = New DataTable

      Try

        ' Get the column headings from the first row of the spreadsheet and create the columns
        Dim m_ws As Excel.Worksheet
        m_ws = DirectCast(_excelWorkbook.Sheets(wsName), Excel.Worksheet)
        _dt = New DataTable
        m_iCol = 1
        Dim m_range As Excel.Range

        m_range = DirectCast(m_ws.Cells(1, 1), Excel.Range)
        While m_range.Value2 IsNot Nothing
          _dt.Columns.Add(m_range.Value2.ToString, GetType(String))
          m_iCntColumns = m_iCol
          m_iCol += 1
          m_range = DirectCast(m_ws.Cells(1, m_iCol), Excel.Range)
        End While

        ' Continue with remaining rows and add the data to the table
        m_iRow = 2

        ' First Column Required
        m_range = DirectCast(m_ws.Cells(m_iRow, 1), Excel.Range)

        ' Check Each Row
        While m_range.Value2 IsNot Nothing
          Dim m_row As DataRow
          m_row = _dt.NewRow()
          m_row(0) = m_range.Value2.ToString
          For i As Integer = 1 To m_iCntColumns - 1
            m_range = DirectCast(m_ws.Cells(m_iRow, i + 1), Excel.Range)
            If m_range.Value2 IsNot Nothing Then
              m_row(i) = m_range.Value2.ToString
            End If
          Next

          ' Add the Row
          _dt.Rows.Add(m_row)

          ' Next Excel Row
          m_iRow += 1
          m_range = DirectCast(m_ws.Cells(m_iRow, 1), Excel.Range)

        End While

        Try

          ' Close All
          _excelWorkbook.Close(False)
          ShutDownExcel()

        Catch
        End Try

        ' Success
        Return _dt

      Catch ex As Exception

        ' Return Nothing along with an Error Message
        MsgBox("Error Reading Excel Worksheet." & vbCr &
               "System message:" & vbCr &
               ex.Message,
               MsgBoxStyle.Critical,
               "Error")

      End Try

      ' Failure
      Return Nothing

    End Function

    ''' <summary>
    ''' Fill Excel Worksheet from DataTable
    ''' </summary>
    ''' <param name="dt">Datatable to Write</param>
    ''' <returns>True on success</returns>
    ''' <remarks>Header is assumed to be first row, data starts on row 2</remarks>
    Public Function FillExcelWorksheetFromDataTable(dt As DataTable) As Boolean

      ' Method Variables
      Dim m_range As Excel.Range
      Dim m_ws As Excel.Worksheet

      ' Widen Scope
      _dt = dt

      Try

        ' Get the Worksheet
        m_ws = DirectCast(_excelWorkbook.Sheets("Family and Types"), Excel.Worksheet)

        Dim m_iR As Integer = 1
        For Each x As DataRow In _dt.Rows
          For m_iC As Integer = 0 To _dt.Columns.Count - 1
            m_range = DirectCast(m_ws.Cells(m_iR + 1, m_iC + 1), Excel.Range)
            If x(m_iC).ToString IsNot Nothing Then
              m_range.Value2 = x(m_iC).ToString
            End If
          Next
          m_iR += 1
        Next

        ' Save Changes
        _excelWorkbook.Save()

        ' Close Excel?
        _excelWorkbook.Close()
        ShutDownExcel()

        ' Success
        Return True

      Catch
      End Try

      ' Failure
      Return False

    End Function

#End Region

#Region "Public Members - Excel Startup and Shutdown"

    ''' <summary>
    ''' Start the Excel Application
    ''' </summary>
    ''' <returns>True on success</returns>
    ''' <remarks></remarks>
    Public Function StartExcel() As Boolean

      ' Does the File Exist?
      If Not File.Exists(_fileName) Then

        ' Error Message
        MsgBox("Unable to locate file: " & _fileName,
               MsgBoxStyle.Exclamation,
               "Error")

      Else

        Try

          ' Use Exisitng Session or Start One
          _excelWorkbook = DirectCast(Runtime.InteropServices.Marshal.BindToMoniker(_fileName), Excel.Workbook)
          _excelApp = _excelWorkbook.Application

          ' Is Excel running, by verifying a workbook
          If _excelWorkbook.Application.ActiveWorkbook Is Nothing Then
            _excelAppExisting = False
          Else
            _excelAppExisting = True
          End If

          ' Path Fix
          _fileName = _fileName.Substring(_fileName.LastIndexOf("\") + 1)
          _excelWorkbook.Application.Windows(_fileName).Visible = True

          ' Success
          Return True

        Catch ex As Exception

          ' Failure Message
          MsgBox("Unable to find or start Excel Interop session with file: " &
                 Convert.ToString(_fileName) & ". " & vbLf & vbLf &
                 "System Error Message: " & vbLf & ex.Message,
                 MsgBoxStyle.Exclamation,
                 "Error")
        End Try

      End If

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Terminate the Excel Application
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShutDownExcel()

      Try

        ' Was Excel Already open?
        If Not _excelAppExisting Then
          If _excelApp IsNot Nothing Then
            If _excelWorkbook IsNot Nothing Then

              ' Save the Workbook
              _excelWorkbook.Save()

            End If

            ' Quit if we have an Application Reference
            If _excelApp IsNot Nothing Then
              _excelApp.Quit()
            End If

          End If
        End If

        ' Set References to Nothing
        _excelWorkbook = Nothing
        _excelApp = Nothing
        GC.Collect()

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace