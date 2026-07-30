Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].ViewportReporting.Data

Namespace Entry

  ''' <summary>
  ''' Revit 2021 Command Class
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    ''' <summary>
    ''' Command
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
        Dim d As New form_Main(New clsSettings(commandData, elements))
        d.ShowDialog()

        ' Return Success
        Return Result.Succeeded

      Catch ex As Exception

        ' Failure Message
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace