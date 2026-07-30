Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.Exceler8.Data

Namespace Entry

  ''' <summary>
  ''' Smart Import
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class DE309158ACE741799F90E9210DEEF5F4

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry Point
    ''' </summary>
    ''' <param name="commandData"></param>
    ''' <param name="message"></param>
    ''' <param name="elements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Execute(ByVal commandData As ExternalCommandData,
                            ByRef message As String,
                            ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute

      Try
        ' Do they have Excel Installed?
        Dim m_s As New clsSettings(commandData, elements)
          If m_s.OfficeInstallVersion = EnumOfficeVersion.isNotInstalled Then
            message = "This command depends on the installation of Microsoft Excel and was not found to be installed on your system. Cannot continue."
            Return Result.Failed
          End If
          If m_s.OfficeInstallVersion = EnumOfficeVersion.isNotSupported Then
            message = "The version of Excel detected on your machine is not supported with this tool. We currently only support Excel 2010 and up. Cannot continue."
            Return Result.Failed
          End If

          ' Navigate to a File
          Dim m_path As String
        Using d As New System.Windows.Forms.OpenFileDialog()
          d.Title = "Select a Valid Exceler8 Workbook to Sync"
          d.DefaultExt = "*.xlsx"
          d.Filter = "Exceler8 Documents | *.xlsx"
          d.FileName = ""
          If d.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If String.IsNullOrEmpty(d.FileName) Then
              message = "No valid file..."
              Return Result.Failed
            Else
              m_path = d.FileName
            End If
          Else
            'message = "Cancelled by user..."
            Return Result.Cancelled
          End If
        End Using

          ' Read Excel Data
          Dim m_excel As New clsExcel(m_path, m_s, EnumExcelSrartupMode.isSmartSync)
          m_excel.GetHeaderData("key", True)

          ' Construct and Display the Form
          Using d As New form_SmartSync(m_s)
            d.ShowDialog()
          End Using

        ' Success
        Return Result.Succeeded

      Catch ex As Exception

        ' Failure
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace