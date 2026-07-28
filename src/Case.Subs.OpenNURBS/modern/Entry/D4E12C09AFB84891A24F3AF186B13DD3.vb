Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class D4E12C09AFB84891A24F3AF186B13DD3

    Implements IExternalCommand

    ''' <summary>
    ''' 
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
          Dim d As New form_ImportMain(New clsSettings(commandData))
          ' Show It
          d.ShowDialog()

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