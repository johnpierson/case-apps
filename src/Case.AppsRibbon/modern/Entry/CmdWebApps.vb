Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdWebApps

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry
    ''' </summary>
    ''' <param name="commandData"></param>
    ''' <param name="message"></param>
    ''' <param name="elements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Execute(commandData As ExternalCommandData,
                            ByRef message As String,
                            elements As ElementSet) As Result Implements IExternalCommand.Execute
      Try
        Process.Start("https://github.com/WeConnect/case-apps")
        Return Result.Succeeded
      Catch ex As Exception
        message = ex.Message
        Return Result.Failed
      End Try
    End Function
  End Class
End Namespace