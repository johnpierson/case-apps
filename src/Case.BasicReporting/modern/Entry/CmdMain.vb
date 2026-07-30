Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].BasicReporting.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

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
        ' Construct and Display the Form
        Using d As New form_Main(New clsSettings(commandData, elements))
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