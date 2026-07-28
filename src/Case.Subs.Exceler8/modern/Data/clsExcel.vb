Option Infer On
Imports System.IO
Imports System.Reflection
Imports System.Data
Imports System.Linq
Imports Microsoft.Office.Interop

Namespace Data

  Public Class clsExcel

    Private _eMode As EnumExcelSrartupMode
    Private _fullFilepath As String
    Private _s As clsSettings
    Private _excelApp As Excel.Application
    Private _excelWorkbook As Excel.Workbook
    Private _excelAppExisting As Boolean
    Private _headerRowNumber As Integer = -1
    Private _dt As Array

#Region "Public Properties"

    ''' <summary>
    ''' Header Columns as they pass requirements from UI sync selections
    ''' Column Number is Key
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedSyncHeaderColumns As SortedDictionary(Of Integer, clsExcelHeader)

    ''' <summary>
    ''' Key Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KeyName As String
      Get
        Return _fullFilepath
      End Get
    End Property

    ''' <summary>
    ''' Cannot Continue on Failure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFailed As Boolean

    ''' <summary>
    ''' List of Worksheets in Workbook
    '''  - Column Number as KEY
    '''  - - Header (Parameter Name and Kind) as Value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WorkSheetColumns As Dictionary(Of String, SortedDictionary(Of Integer, clsExcelHeader))

    ''' <summary>
    ''' Records the Keys and What Row they are on
    '''  - Worksheet Name
    '''  - - Key String ; Row Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WorkSheetRows As Dictionary(Of String, Dictionary(Of String, Integer))

    ''' <summary>
    ''' List of Worksheets in Workbook
    '''  - Rows of Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WorkSheetData As Dictionary(Of String, Array)

    ''' <summary>
    ''' Header Row
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HeaderRowNumber As Integer
      Get
        Return _headerRowNumber
      End Get
    End Property

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
                   excelMode As EnumExcelSrartupMode)

      ' Widen Scope
      _fullFilepath = filePath
      _s = s
      _eMode = excelMode

      ' Only non schedule exports
      Setup()

    End Sub

#Region "Private Members - Setup"

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      Try

        ' Fresh List and Settings
        IsFailed = True
        WorkSheetColumns = New Dictionary(Of String, SortedDictionary(Of Integer, clsExcelHeader))
        Dim m_excelPathChecker As String = ""

        ' Excel Mode
        Select Case _eMode

          Case EnumExcelSrartupMode.IsExisting

            ' File Exists?
            If Not File.Exists(_fullFilepath) Then

              ' Failure
              MsgBox("Could not Find File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Import")

            Else

              ' Start Excel
              If ExcelStart() = True Then

                ' Get the Worksheet Names
                _GetWorksheetNames()

                ' Success
                IsFailed = False

              End If

            End If

          Case EnumExcelSrartupMode.IsNewFile

            ' Copy the Template File
            m_excelPathChecker = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) &
                                 "\Case.Subs.Exceler8.xlsx"

            If Not File.Exists(m_excelPathChecker) Then

              ' Failure
              MsgBox("Missing Excel Template File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Export")

            Else

              ' Copy and Open Workbook as New
              File.Copy(m_excelPathChecker, _fullFilepath, True)

              ' Start Excel
              If ExcelStart() = True Then

                ' Get the Worksheet Names
                _GetWorksheetNames()

                ' Success
                IsFailed = False

              End If

            End If

          Case EnumExcelSrartupMode.IsSmartSync

            ' File Exists?
            If Not File.Exists(_fullFilepath) Then

              ' Failure
              MsgBox("Could not Find File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Import")

            Else

              ' Start Excel
              If ExcelStart() = True Then

                ' Get the Worksheet Names
                _GetWorksheetNames()

                ' Success
                IsFailed = False

              End If

            End If

          Case EnumExcelSrartupMode.IsSchedule

            ' Copy the Template File
            m_excelPathChecker = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) &
                                 "\Case.Subs.Exceler8_Schedule.xlsx"

            If Not File.Exists(m_excelPathChecker) Then

              ' Failure
              MsgBox("Missing Excel Template File!" & vbCr & m_excelPathChecker,
                     MsgBoxStyle.Critical,
                     "Cannot Continue Export")

            Else

              ' Copy and Open Workbook as New
              File.Copy(m_excelPathChecker, _fullFilepath, True)

              ' Start Excel
              If ExcelStart() = True Then

                ' Get the Worksheet Names
                _GetWorksheetNames()

                ' Success
                IsFailed = False

              End If

            End If

        End Select

      Catch
      End Try

    End Sub

#End Region

#Region "Private Members"

    ''' <summary>
    ''' Return an Empty Array with Bounds set to maximum row and column counts
    ''' </summary>
    ''' <param name="ws">Worksheet to Check</param>
    ''' <returns>Range for all Data - no header</returns>
    ''' <remarks></remarks>
    Private Function _GetFullDataRange(ws As Excel.Worksheet) As Excel.Range

      Try

        ' Count Columns
        Dim m_iCol As Integer = 1
        Dim m_noData As Boolean = False
        Do While m_noData = False

          Try

            ' Test for Value - Make sure we are testing the header row
            If String.IsNullOrEmpty(ws.Cells(8, m_iCol).Value.ToString) Then
              Exit Do
            Else
              m_iCol += 1
            End If

          Catch
            m_noData = True
          End Try

        Loop

        ' Count Rows
        Dim m_iRow As Integer = 1
        m_noData = False
        Do While m_noData = False

          Try

            ' Test for Value
            If String.IsNullOrEmpty(ws.Cells(m_iRow + 8, 1).Value.ToString) Then
              Exit Do
            Else
              m_iRow += 1
            End If

          Catch
            m_noData = True
          End Try

        Loop

        ' Return Range
        Return ws.Range(ws.Cells(6, 1), ws.Cells(7 + m_iRow, m_iCol - 1))

      Catch
      End Try

      ' Failure
      Return Nothing

    End Function

    ''' <summary>
    ''' Get the Header Columns that were selected for data transfer.
    ''' Gets column names from IniFile Settings
    ''' </summary>
    ''' <param name="wsName">Worksheet Name to Target</param>
    ''' <remarks>Stores results in SelectedSyncHeaderColumns</remarks>
    Private Sub _GetSelectedSyncHeaderColumns(wsName As String)

      ' Fresh Start
      SelectedSyncHeaderColumns = New SortedDictionary(Of Integer, clsExcelHeader)

      Try

        ' INI Data
        Dim m_ini = _s.IniFile.FileSettings(_fullFilepath.ToLower & "[" & wsName.ToLower & "]")
        If m_ini Is Nothing Then Exit Sub

        ' Process Headers
        For Each x In WorkSheetColumns(wsName)

          Select Case x.Value.Kind

            Case EnumExcelHeaderKind.IsE
              x.Value.Direction = EnumSyncDir.ToExcel
              SelectedSyncHeaderColumns.Add(x.Key + 1, x.Value)

            Case EnumExcelHeaderKind.IsI
              For Each d In m_ini("inst")
                If d.NameAndGroup.ToLower = x.Value.Name.ToLower & "|" & x.Value.GroupName.ToLower Then
                  If Not d.Direction = EnumSyncDir.IsIgnore Then

                    ' Add this one
                    x.Value.Direction = d.Direction
                    SelectedSyncHeaderColumns.Add(x.Key + 1, x.Value)
                    Exit For

                  End If
                End If
              Next

            Case EnumExcelHeaderKind.IsT
              For Each d In m_ini("type")
                If d.NameAndGroup.ToLower = x.Value.Name.ToLower & "|" & x.Value.GroupName.ToLower Then
                  If Not d.Direction = EnumSyncDir.IsIgnore Then

                    ' Add this one
                    x.Value.Direction = d.Direction
                    SelectedSyncHeaderColumns.Add(x.Key + 1, x.Value)
                    Exit For

                  End If
                End If
              Next

          End Select

        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the List of Worksheet Names
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _GetWorksheetNames()
      Try
        WorkSheetColumns = New Dictionary(Of String, SortedDictionary(Of Integer, clsExcelHeader))
        WorkSheetRows = New Dictionary(Of String, Dictionary(Of String, Integer))
        WorkSheetData = New Dictionary(Of String, Array)
        For Each ws As Excel.Worksheet In _excelWorkbook.Worksheets
          Try
            WorkSheetColumns.Add(ws.Name, New SortedDictionary(Of Integer, clsExcelHeader))
            WorkSheetRows.Add(ws.Name, New Dictionary(Of String, Integer))
          Catch
          End Try
        Next
      Catch
      End Try
    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Update With New Rows
    ''' </summary>
    ''' <param name="f">Progress Form</param>
    ''' <param name="n">Name of Excel Worksheet</param>
    ''' <remarks></remarks>
    Public Sub UpdateAfterSyncAddNewElements(f As form_Progress, n As String)

      Try

        ' Worksheet
        Dim m_ws As Excel.Worksheet = DirectCast(_excelWorkbook.Sheets(n), Excel.Worksheet)

        ' Process New Rows
        For Each x In WorkSheetData(n)

          Try
            ' Update Progress
            f.StepProgress("Adding New Excel Values...")
          Catch
          End Try

        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Excel Data
    ''' </summary>
    ''' <param name="wsName">Worksheet Name</param>
    ''' <remarks></remarks>
    Public Sub GetValues(wsName As String)

      Try

        ' Worksheet
        Dim m_ws As Microsoft.Office.Interop.Excel.Worksheet = _excelWorkbook.Sheets(wsName)

        ' Data Bounds
        Dim m_rangeAll As Microsoft.Office.Interop.Excel.Range = _GetFullDataRange(m_ws)

        ' Header?
        _GetSelectedSyncHeaderColumns(wsName)

        ' To Array Object
        Dim m_rangeArray(,) As Object = m_rangeAll.Value

        ' Save Data
        WorkSheetData(wsName) = m_rangeArray

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Read the data in each worksheet and retrieve valid column names as parameter names
    ''' </summary>
    ''' <param name="keyName">Mandatory 'Key' Column (guid, ElementID, etc) that must be the first column with data</param>
    ''' <param name="useGroupNames">Match Group Names?</param>
    ''' <remarks></remarks>
    Public Sub GetHeaderData(keyName As String, useGroupNames As Boolean)

      ' Module Variables
      Dim m_ws As Excel.Worksheet
      Dim m_range As Excel.Range
      Dim m_rangeGroupName As Excel.Range
      Dim m_rangeRecord As Excel.Range
      Dim m_groupName As String = ""

      ' Read each Worksheet
      For Each x In WorkSheetColumns
        x.Value.Clear() ' = New SortedDictionary(Of Integer, clsExcelHeader)

        Try

          ' Get the Worksheet
          m_ws = _excelWorkbook.Sheets(x.Key.ToString)

          ' Find the "Key" Cell
          For i = 1 To 20 Step 1
            For ii = 1 To 20 Step 1

              Try

                ' Range to Find Key
                m_range = m_ws.Cells(ii, i)

                ' Is it the One?
                If m_range Is Nothing Then Continue For
                If m_range.Value.ToString.ToLower = keyName.ToLower Then

                  ' Header Row
                  _headerRowNumber = ii

                  ' Read header Row
                  Dim m_iEmpty As Integer = 0
                  Dim m_iColCnt As Integer = i

                  ' Get Header Data
                  Do Until m_iEmpty = 3

                    Try

                      ' Group Name
                      If useGroupNames = True Then
                        m_rangeGroupName = m_ws.Cells(ii - 1, m_iColCnt)
                        If Not m_rangeGroupName Is Nothing Then
                          m_groupName = m_rangeGroupName.Value.ToString
                        End If
                      End If

                    Catch
                    End Try

                    Try

                      ' Get the Range Item
                      m_range = m_ws.Cells(ii, m_iColCnt)
                      m_rangeRecord = m_ws.Cells(ii + 1, m_iColCnt)

                      If m_range Is Nothing Then

                        m_iEmpty += 1

                      Else

                        ' value
                        Dim m_name As String = m_range.Value.ToString

                        ' DataType
                        Dim m_dt As EnumCellDataType = EnumCellDataType.IsNormal
                        Try
                          Dim m_dtStyle As String = m_rangeRecord.Style.Name.ToLower
                          If m_dtStyle = "complex" Then m_dt = EnumCellDataType.IsComplex
                          If m_dtStyle = "read only" Then m_dt = EnumCellDataType.IsReadOnly
                        Catch
                        End Try

                        ' Get the Style
                        ' - Add Value under correct place in listing...
                        Select Case m_range.Style.Name.ToLower
                          Case "elements"
                            If m_name.ToLower = "key" Then m_name = "0key"
                            x.Value.Add(m_iColCnt - 1, New clsExcelHeader(m_name, EnumExcelHeaderKind.IsE, m_groupName, m_dt))
                          Case "type parameter"
                            x.Value.Add(m_iColCnt - 1, New clsExcelHeader(m_name, EnumExcelHeaderKind.IsT, m_groupName, m_dt))
                          Case "instance parameter"
                            x.Value.Add(m_iColCnt - 1, New clsExcelHeader(m_name, EnumExcelHeaderKind.IsI, m_groupName, m_dt))
                          Case Else
                            m_iEmpty += 1
                        End Select

                      End If

                    Catch
                      m_iEmpty += 1
                    End Try

                    ' Step the ii
                    m_iColCnt += 1

                  Loop

                  ' All Done
                  Exit Sub

                End If

              Catch
              End Try

            Next
          Next

        Catch
        End Try

      Next

    End Sub

    ''' <summary>
    ''' Copy a Worksheet Name as New Tab
    ''' </summary>
    ''' <param name="names">List of Tab Names to Create. Will copy from first tab</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddWorksheets(names As List(Of String)) As Boolean

      ' Default
      Dim m_ret As Boolean = False
      Dim m_worksheet As Excel.Worksheet

      Try

        ' Add the Tabs
        For i = 1 To names.Count - 1

          _excelWorkbook.Worksheets(1).Copy(Before:=_excelWorkbook.Worksheets(1))

        Next

        ' Name the Tabs
        For i = 1 To names.Count

          m_worksheet = _excelWorkbook.Worksheets(i)
          m_worksheet.Name = names(i - 1)

        Next

        ' Save
        _excelWorkbook.Save()

        ' Success
        m_ret = True

      Catch
      End Try

      ' Update Worksheet Names
      _GetWorksheetNames()

      ' Return
      Return m_ret

    End Function

    ''' <summary>
    ''' Write DataTable to Worksheet
    ''' </summary>
    ''' <param name="worksheetName"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>    
    Public Sub WriteWorksheetFromListOfString(worksheetName As String, dt As List(Of List(Of String)))

      Dim m_range As Excel.Range
      Dim m_rangeStart As Excel.Range
      Dim m_rangeEnd As Excel.Range
      Dim m_ws As Excel.Worksheet

      Try

        ' Get the Worksheet
        m_ws = _excelWorkbook.Sheets(worksheetName)

        ' Boundary of Data
        Dim m_colList As List(Of String) = dt.Last()
        Dim m_array(dt.Count - 1, m_colList.Count - 1)

        ' Set Range
        m_rangeStart = m_ws.Cells(1, 1)
        m_rangeEnd = m_ws.Cells(dt.Count, m_colList.Count)
        m_range = m_ws.Range(m_rangeStart, m_rangeEnd)

        ' Row Count
        Dim m_rowId As Integer = 0
        Dim m_colId As Integer = 0

        ' Get the Data into an Array
        For Each m_row In dt
          For Each cellText In m_row
            Dim dVal As Double = -66.666
            Dim dBool As Boolean = Double.TryParse(cellText, dVal)
            If dBool = False Then
              m_array(m_rowId, m_colId) = cellText
            Else
              m_array(m_rowId, m_colId) = dVal
            End If
            m_colId += 1

          Next

          m_rowId += 1
          m_colId = 0

        Next

        ' Values
        m_range.Value2 = m_array


      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Write Data to Excel
    ''' </summary>
    ''' <param name="worksheetName">Worksheet to write data</param>
    ''' <param name="headerDict">Header Object Scopes for Styling</param>
    ''' <param name="dt">Data as Array of String</param>
    ''' <param name="writeHeaders">Write Header data?</param>
    ''' <param name="startingRow">Row to start range writing</param>
    ''' <remarks></remarks>
    Public Sub UpdateWorksheetFromArray(worksheetName As String,
                                        headerDict As Dictionary(Of String, SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader))),
                                        dt As Array,
                                        writeHeaders As Boolean,
                                        Optional startingRow As Integer = -1)

      ' Widen Scope
      _dt = dt

      ' Fresh Column Listing
      WorkSheetColumns(worksheetName) = New SortedDictionary(Of Integer, clsExcelHeader)
      Dim m_iWsCol As Integer = 1
      For Each grp In headerDict("e").Values
        For Each param In grp.Values
          WorkSheetColumns(worksheetName).Add(m_iWsCol, param)
          m_iWsCol += 1
        Next
      Next
      For Each grp In headerDict("t").Values
        For Each param In grp.Values
          WorkSheetColumns(worksheetName).Add(m_iWsCol, param)
          m_iWsCol += 1
        Next
      Next
      For Each grp In headerDict("i").Values
        For Each param In grp.Values
          WorkSheetColumns(worksheetName).Add(m_iWsCol, param)
          m_iWsCol += 1
        Next
      Next

      ' Method Variables
      Dim m_iMasterColCnt As Integer = 1
      Dim m_range As Excel.Range
      Dim m_ws As Excel.Worksheet = Nothing

      Try

        ' Get the Worksheet
        m_ws = _excelWorkbook.Sheets(worksheetName)

        ' Header Data?
        If writeHeaders = True Then

          ' Counting, etc.
          Dim m_iInnerColCnt As Integer = 0
          Dim m_startCell As Excel.Range = Nothing

          ' Element Data
          m_range = m_ws.Cells(6, m_iMasterColCnt)
          m_range.Value2 = "Element Data"
          For Each grp In headerDict("e")

            ' Group Name
            m_range = m_ws.Cells(7, m_iMasterColCnt)
            If m_iInnerColCnt = 0 Then m_startCell = m_range
            m_range.Value2 = grp.Key.ToString

            ' Element Group Parameters
            For Each prm In grp.Value
              m_range = m_ws.Cells(8, m_iInnerColCnt + m_iMasterColCnt)
              m_range.Value2 = prm.Value.Name
              m_range.Style = "Elements"

              ' Step the Column
              m_iInnerColCnt += 1

            Next

            ' Reset and Adjust Column Values
            m_iMasterColCnt += m_iInnerColCnt
            m_iInnerColCnt = 0
            m_range = m_ws.Range(m_startCell, m_range)
            m_range.Style = "Elements"
            m_range.Font.Bold = True

          Next

          Try

            ' Type Parameters
            m_range = m_ws.Cells(6, m_iMasterColCnt)
            m_range.Value2 = "Type Parameters"
            For Each grp In headerDict("t")

              ' Group Name
              m_range = m_ws.Cells(7, m_iMasterColCnt)
              If m_iInnerColCnt = 0 Then m_startCell = m_range
              m_range.Value2 = grp.Key.ToString

              ' Type Group Parameters
              For Each prm In grp.Value
                m_range = m_ws.Cells(8, m_iInnerColCnt + m_iMasterColCnt)
                m_range.Value2 = prm.Value.Name
                m_range.Style = "Type Parameter"

                ' Step the Column
                m_iInnerColCnt += 1

              Next

              ' Reset and Adjust Column Values
              m_iMasterColCnt += m_iInnerColCnt
              m_iInnerColCnt = 0
              m_range = m_ws.Range(m_startCell, m_range)
              m_range.Style = "Type Parameter"
              m_range.Font.Bold = True

            Next

          Catch
          End Try

          Try

            ' Instance Parameters
            m_range = m_ws.Cells(6, m_iMasterColCnt)
            If headerDict("i").Count > 0 Then
              m_range.Value2 = "Instance Parameters"
            End If
            For Each grp In headerDict("i")

              ' Group Name
              m_range = m_ws.Cells(7, m_iMasterColCnt)
              If m_iInnerColCnt = 0 Then m_startCell = m_range
              m_range.Value2 = grp.Key.ToString

              ' Instance Group Parameters
              For Each prm In grp.Value
                m_range = m_ws.Cells(8, m_iInnerColCnt + m_iMasterColCnt)
                m_range.Value2 = prm.Value.Name
                m_range.Style = "Instance Parameter"

                ' Step the Column
                m_iInnerColCnt += 1

              Next

              ' Reset and Adjust Column Values
              m_iMasterColCnt += m_iInnerColCnt
              m_iInnerColCnt = 0
              m_range = m_ws.Range(m_startCell, m_range)
              m_range.Style = "Instance Parameter"
              m_range.Font.Bold = True

            Next

          Catch
          End Try

        End If

        ' Write Data
        If writeHeaders = True Then
          m_range = m_ws.Range(m_ws.Cells(9, 1), m_ws.Cells(_dt.GetUpperBound(0) + 9, _dt.GetUpperBound(1) + 1))
        Else
          If startingRow > -1 Then
            m_range = m_ws.Range(m_ws.Cells(startingRow, 1), m_ws.Cells(_dt.GetUpperBound(0) + startingRow, _dt.GetUpperBound(1)))
          Else
            m_range = m_ws.Range(m_ws.Cells(6, 1), m_ws.Cells(_dt.GetUpperBound(0) + 5, _dt.GetUpperBound(1)))
          End If
        End If
        'assign values to range
        m_range.Value = _dt
        'Dim newDT(0, _dt.GetUpperBound(1))

        'For i As Integer = _dt.GetLowerBound(0) To _dt.GetUpperBound(0)
        '  For z As Integer = _dt.GetLowerBound(1) To _dt.GetUpperBound(1)
        '    Dim o As String = _dt.GetValue(i, z)
        '    Dim dVal As Double = -66.666
        '    Dim dBool As Boolean = Double.TryParse(o, dVal)
        '    If dBool = False Then
        '      newDT(i, z) = o
        '    Else
        '      newDT(i, z) = dVal
        '    End If
        '  Next
        'Next
        'Finally add new array to range
        'm_range.Value = newDT

      Catch ex As Exception
        Dim s As String = ex.Message

      End Try

      Try

        ' Style Cells
        For Each x In WorkSheetColumns(worksheetName)

          Select Case x.Value.DataType

            Case EnumCellDataType.IsReadOnly

              ' Range
              If writeHeaders = True Then
                m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 9, x.Key))
              Else
                If startingRow > -1 Then
                  m_range = m_ws.Range(m_ws.Cells(startingRow, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + startingRow, x.Key))
                Else
                  m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 5, x.Key))
                End If
              End If

              ' Style
              m_range.Style = "Read Only"

            Case EnumCellDataType.IsComplex

              ' Range
              If writeHeaders = True Then
                m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 9, x.Key))
              Else
                If startingRow > -1 Then
                  m_range = m_ws.Range(m_ws.Cells(startingRow, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + startingRow, x.Key))
                Else
                  m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 5, x.Key))
                End If
              End If

              ' Style
              m_range.Style = "Complex"

            Case Else

              ' Range
              If writeHeaders = True Then
                m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 9, x.Key))
              Else
                If startingRow > -1 Then
                  m_range = m_ws.Range(m_ws.Cells(startingRow, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + startingRow, x.Key))
                Else
                  m_range = m_ws.Range(m_ws.Cells(9, x.Key), m_ws.Cells(_dt.GetUpperBound(0) + 5, x.Key))
                End If
              End If

              ' Style
              m_range.Style = "Normal"

          End Select

        Next

      Catch ex As Exception
        dim s as String = ex.Message
      End Try

    End Sub

#End Region

#Region "Public Members - Excel Startup and Shutdown"

    ''' <summary>
    ''' Start the Excel Application
    ''' </summary>
    ''' <returns>True on success</returns>
    ''' <remarks></remarks>
    Public Function ExcelStart() As Boolean

      ' Does the File Exist?
      If Not File.Exists(_fullFilepath) Then

        ' Error Message
        MsgBox("Unable to locate file: " & _fullFilepath,
               MsgBoxStyle.Exclamation,
               "Error")

      Else

        Try

          ' Use Exisitng Session or Start One
          _excelWorkbook = DirectCast(Runtime.InteropServices.Marshal.BindToMoniker(_fullFilepath), Excel.Workbook)
          _excelApp = _excelWorkbook.Application

          ' Is Excel running, by verifying a workbook
          If _excelWorkbook.Application.ActiveWorkbook Is Nothing Then
            _excelAppExisting = False
          Else
            _excelAppExisting = True
          End If

          ' Path Fix
          Dim m_fileName As String = _fullFilepath.Substring(_fullFilepath.LastIndexOf("\") + 1)
          _excelWorkbook.Application.Windows(m_fileName).Visible = True

          ' Success
          Return True

        Catch ex As Exception

          ' Failure Message
          MsgBox("Unable to find or start Excel Interop session with file: " & vbCr &
                 Convert.ToString(_fullFilepath),
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
    Public Sub ExcelShutDown()

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