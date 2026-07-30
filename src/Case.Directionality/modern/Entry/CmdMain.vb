Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Directionality.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    ''' <summary>
    ''' Main entry point for every external command
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
        ' Construct and Display the Form
        Using d As New form_Main(New clsSettings(commandData))
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