Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].ChangeLineStyles.Data
Imports [Case].LineChanger.Data

Namespace Entry

  ''' <summary>
  ''' Change and Replace Line Styles
  ''' </summary>
  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    Public Function Execute(ByVal commandData As ExternalCommandData, ByRef message As String, ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute
      Try


        ' Construct and Display the Form
        Using m_dlg As New form_Main(New clsSettings(commandData))
          m_dlg.ShowDialog()
        End Using

        ' Success
        Return Result.Succeeded

      Catch

        ' Failure
        message = "Change and Replace Line Style Failed..."
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace