Imports System.Collections.Generic
Imports System.Web.UI
Imports System.IO
Imports Autodesk.Revit.DB
Imports System.Data

Namespace Data
  Public Class clsRvtScheduleData
    
    Private _writer As HtmlTextWriter
    Private _headerSection As TableSectionData
    Private _bodySection As TableSectionData
    Private _footerSection As TableSectionData
    Private _summarySection As TableSectionData
    
    Friend Schedule As ViewSchedule

    ''' <summary>
    ''' A collection of cells which have already been output.  This is needed to deal with
    ''' cell merging - each cell should be written only once even as all the cells are iterated in
    ''' order.
    ''' </summary>
    Private _cells As New List(Of Tuple(Of Integer, Integer))

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="vs">The schedule to be exported.</param>
    Public Sub New(vs As ViewSchedule)
      Schedule = vs
    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Exports the schedule to formatted HTML.
    ''' </summary>
    Public Sub ExportToHtml()

      ' Setup file location in temp directory
      Dim m_folder As String = Environment.GetEnvironmentVariable("TEMP")
      Dim m_htmlFile As String = Path.Combine(m_folder,
                                              ReplaceIllegalCharacters(Schedule.Name) & Convert.ToString(".html"))

      ' Initialize StringWriter instance.
      Using sw As New StreamWriter(m_htmlFile)
        Try
          ' Put HtmlTextWriter in using block because it needs to call Dispose.
          Using hw = New HtmlTextWriter(sw)
            hw.AddAttribute(HtmlTextWriterAttribute.Align, "center")
            hw.RenderBeginTag(HtmlTextWriterTag.Div)

            'Write schedule header
            WriteHeader()

            'Write schedule body
            WriteBody()

            hw.RenderEndTag()
          End Using
        Finally
          sw.Close()
        End Try
      End Using

      ' Show the created file
      Diagnostics.Process.Start(m_htmlFile)

    End Sub

    ''' <summary>
    ''' Writes the header section of the table to the HTML file.
    ''' </summary>
    Private Sub WriteHeader()

      ' Clear written cells
      _cells.Clear()

      ' Start table to represent the header
      _writer.AddAttribute(HtmlTextWriterAttribute.Border, "1")
      _writer.RenderBeginTag(HtmlTextWriterTag.Table)

      ' Get header section and write each cell
      _headerSection = Schedule.GetTableData().GetSectionData(SectionType.Header)
      Dim m_numRows As Integer = _headerSection.NumberOfRows
      Dim m_numCols As Integer = _headerSection.NumberOfColumns

      For i As Integer = _headerSection.FirstRowNumber To m_numRows - 1
        WriteHeaderSectionRowHtml(i, m_numCols)
      Next

      ' Close header table
      _writer.RenderEndTag()

    End Sub

    ''' <summary>
    ''' Writes the body section of the table to the HTML file.
    ''' </summary>
    Private Sub WriteBody()

      ' Clear written cells
      _cells.Clear()

      ' Write the start of the body table
      _writer.AddAttribute(HtmlTextWriterAttribute.Border, "1")
      _writer.RenderBeginTag(HtmlTextWriterTag.Table)

      ' Get body section and write contents
      _bodySection = Schedule.GetTableData().GetSectionData(SectionType.Body)
      Dim m_numRows As Integer = _bodySection.NumberOfRows
      Dim m_numCols As Integer = _bodySection.NumberOfColumns

      For i As Integer = _bodySection.FirstRowNumber To m_numRows - 1
        WriteBodySectionRowHtml(i, m_numCols)
      Next

      ' Close the table
      _writer.RenderEndTag()

    End Sub

    ''' <summary>
    ''' Gets the Color value formatted for HTML (#XXXXXX) output.
    ''' </summary>
    ''' <param name="color">he color.</param>
    ''' <returns>The color string.</returns>
    Private Shared Function GetColorHtmlString(color As Color) As String
      Return String.Format("#{0}{1}{2}",
                           color.Red.ToString("X"),
                           color.Green.ToString("X"),
                           color.Blue.ToString("X"))
    End Function

    ''' <summary>
    ''' A predefined color value used for comparison.
    ''' </summary>
    Private Shared ReadOnly Property Black() As Color
      Get
        Return New Color(0, 0, 0)
      End Get
    End Property

    ''' <summary>
    ''' A predefined color value used for comparison.
    ''' </summary>
    Private Shared ReadOnly Property White() As Color
      Get
        Return New Color(255, 255, 255)
      End Get
    End Property

    ''' <summary>
    ''' Compares two colors.
    ''' </summary>
    ''' <param name="color1">The first color.</param>
    ''' <param name="color2">The second color.</param>
    ''' <returns>True if the colors are equal, false otherwise.</returns>
    Private Function ColorsEqual(color1 As Color, color2 As Color) As Boolean
      Return color1.Red = color2.Red AndAlso color1.Green = color2.Green AndAlso color1.Blue = color2.Blue
    End Function

    ''' <summary>
    ''' Gets the HTML string representing this horizontal alignment.
    ''' </summary>
    ''' <param name="style">The horizontal alignment.</param>
    ''' <returns>The related string.</returns>
    Private Shared Function GetAlignString(style As HorizontalAlignmentStyle) As String
      Select Case style
        Case HorizontalAlignmentStyle.Left
          Return "left"
        Case HorizontalAlignmentStyle.Center
          Return "center"
        Case HorizontalAlignmentStyle.Right
          Return "right"
      End Select
      Return ""
    End Function

    ''' <summary>
    ''' Writes a row of a table section.
    ''' </summary>
    ''' <param name="secType">The section type.</param>
    ''' <param name="data">The table section data.</param>
    ''' <param name="numberOfRows">The row number.</param>
    ''' <param name="numberOfColumns">The number of columns to write.</param>
    Private Sub WriteSectionRowTemp(secType As Integer, data As TableSectionData, numberOfRows As Integer, numberOfColumns As Integer)

      Using sw As New StreamWriter("c:\temp\scheduleexport.txt", True)

        ' Loop over the table section row.
        For i As Integer = data.FirstColumnNumber To numberOfColumns - 1

          ' Skip already written cells
          If _cells.Contains(New Tuple(Of Integer, Integer)(numberOfRows, i)) Then Continue For

          ' Merged cells
          Dim m_merged As TableMergedCell = data.GetMergedCell(numberOfRows, i)

          ' If merged cell spans multiple columns
          If m_merged.Left <> m_merged.Right Then
            ' _writer.AddAttribute(HtmlTextWriterAttribute.Colspan, (m_merged.Right - m_merged.Left + 1).ToString())
          End If

          ' If merged cell spans multiple rows
          If m_merged.Top <> m_merged.Bottom Then
            ' _writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, (m_merged.Bottom - m_merged.Top + 1).ToString())
          End If

          ' Remember all written cells related to the merge 
          For iRow As Integer = m_merged.Top To m_merged.Bottom
            For iCol As Integer = m_merged.Left To m_merged.Right
              _cells.Add(New Tuple(Of Integer, Integer)(iRow, iCol))
            Next
          Next

          ' Write cell text
          Dim m_txt As String = Schedule.GetCellText(secType, numberOfRows, i)
          If m_txt.Length > 0 Then
            sw.Write(m_txt & vbTab)
          Else
            sw.Write(vbTab)
          End If

        Next

        ' End Line
        sw.Write(vbCr)

      End Using

    End Sub

    ''' <summary>
    ''' Writes a row of the header.
    ''' </summary>
    ''' <param name="iRow">The row number.</param>
    ''' <param name="numberOfColumns">The number of columns to write.</param>
    Private Sub WriteHeaderSectionRowHtml(iRow As Integer, numberOfColumns As Integer)
      WriteSectionRowHtml(SectionType.Header, _headerSection, iRow, numberOfColumns)
    End Sub

    ''' <summary>
    ''' Writes a row of the body.
    ''' </summary>
    ''' <param name="iRow">The row number.</param>
    ''' <param name="numberOfColumns">The number of columns to write.</param>
    Private Sub WriteBodySectionRowHtml(iRow As Integer, numberOfColumns As Integer)
      WriteSectionRowHtml(SectionType.Body, _bodySection, iRow, numberOfColumns)
    End Sub
    
    ''' <summary>
    ''' Writes a row of a table section.
    ''' </summary>
    ''' <param name="iRow">The row number.</param>
    ''' <param name="numberOfColumns">The number of columns to write.</param>
    ''' <param name="secType">The section type.</param>
    ''' <param name="data">The table section data.</param>
    Private Sub WriteSectionRowHtml(secType As SectionType, data As TableSectionData, iRow As Integer, numberOfColumns As Integer)
      ' Start the table row tag.
      _writer.RenderBeginTag(HtmlTextWriterTag.Tr)

      ' Loop over the table section row.
      For iCol As Integer = data.FirstColumnNumber To numberOfColumns - 1
        ' Skip already written cells
        If _cells.Contains(New Tuple(Of Integer, Integer)(iRow, iCol)) Then
          Continue For
        End If

        ' Get style
        Dim style As TableCellStyle = data.GetTableCellStyle(iRow, iCol)
        Dim numberOfStyleTags As Integer = 1



        ' Merged cells
        Dim mergedCell As TableMergedCell = data.GetMergedCell(iRow, iCol)

        ' If merged cell spans multiple columns
        If mergedCell.Left <> mergedCell.Right Then
          _writer.AddAttribute(HtmlTextWriterAttribute.Colspan, (mergedCell.Right - mergedCell.Left + 1).ToString())
        End If

        ' If merged cell spans multiple rows
        If mergedCell.Top <> mergedCell.Bottom Then
          _writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, (mergedCell.Bottom - mergedCell.Top + 1).ToString())
        End If

        ' Remember all written cells related to the merge 
        For iMergedRow As Integer = mergedCell.Top To mergedCell.Bottom
          For iMergedCol As Integer = mergedCell.Left To mergedCell.Right
            _cells.Add(New Tuple(Of Integer, Integer)(iMergedRow, iMergedCol))
          Next
        Next

        ' Write formatting attributes for the upcoming cell
        ' Background color
        If Not ColorsEqual(style.BackgroundColor, White) Then
          _writer.AddAttribute(HtmlTextWriterAttribute.Bgcolor, GetColorHtmlString(style.BackgroundColor))
        End If

        ' Horizontal alignment
        _writer.AddAttribute(HtmlTextWriterAttribute.Align, GetAlignString(style.FontHorizontalAlignment))

        ' Write cell tag
        _writer.RenderBeginTag(HtmlTextWriterTag.Td)

        ' Write subtags for the cell
        ' Underline
        If style.IsFontUnderline Then
          _writer.RenderBeginTag(HtmlTextWriterTag.U)
          numberOfStyleTags += 1
        End If

        'Italic
        If style.IsFontItalic Then
          _writer.RenderBeginTag(HtmlTextWriterTag.I)
          numberOfStyleTags += 1
        End If

        'Bold
        If style.IsFontBold Then
          _writer.RenderBeginTag(HtmlTextWriterTag.B)
          numberOfStyleTags += 1
        End If

        ' Write cell text
        Dim m_txt As String = Schedule.GetCellText(secType, iRow, iCol)
        If m_txt.Length > 0 Then
          _writer.Write(m_txt)
        Else
          _writer.Write("&nbsp;")
        End If

        ' Close open style tags & cell tag
        For i = 0 To numberOfStyleTags - 1
          _writer.RenderEndTag()
        Next
      Next
      ' Close row tag
      _writer.RenderEndTag()
    End Sub

    ''' <summary>
    ''' An utility method to replace illegal characters of the Schedule name when creating the HTML file name.
    ''' </summary>
    ''' <param name="stringWithIllegalChar">The Schedule name.</param>
    ''' <returns>The updated string without illegal characters.</returns>
    Private Shared Function ReplaceIllegalCharacters(stringWithIllegalChar As String) As String
      Dim m_badChars As Char() = Path.GetInvalidFileNameChars()
      Dim updated As String = stringWithIllegalChar
      For Each x As Char In m_badChars
        updated = updated.Replace(x, "_"c)
      Next
      Return updated
    End Function

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Turn the schedule into a table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetData() As List(Of List(Of String))

      Dim m_data As New List(Of List(Of String))

      Try

        ' Master Data
        Dim m_td As TableData = Schedule.GetTableData()
        For iSec = 0 To m_td.NumberOfSections - 1

          ' Iterate Sections of the Data
          Dim m_sectionData As TableSectionData = m_td.GetSectionData(iSec)

          ' Process Rows
          For m_rowNumber As Integer = m_sectionData.FirstRowNumber To m_sectionData.NumberOfRows - 1

            Dim m_rowText As New List(Of String)


            ' Loop over the table section row.
            For m_colNumber As Integer = m_sectionData.FirstColumnNumber To m_sectionData.NumberOfColumns - 1

              ' Write cell text
              Dim m_txt As String = Schedule.GetCellText(iSec, m_rowNumber, m_colNumber)
              If m_txt.Length > 0 Then
                m_rowText.Add(m_txt)
              Else
                m_rowText.Add("")
              End If

            Next

            ' Add Row
            m_data.Add(m_rowText)

          Next

        Next

      Catch
      End Try

      ' Result
      Return m_data

    End Function

#End Region

  End Class
End Namespace