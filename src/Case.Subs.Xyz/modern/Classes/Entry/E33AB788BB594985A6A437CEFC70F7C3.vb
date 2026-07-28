Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.Attributes

''' <summary>
''' CASE Point Management Command Entry Point
''' </summary>
''' <remarks></remarks>
<Transaction(TransactionMode.Manual)>
Public Class E33AB788BB594985A6A437CEFC70F7C3

  Implements IExternalCommand

  ''' <summary>
  ''' Command Entry Point
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
      ' Construct and display the form
      Dim m_d As New form_PlacePointsFromFile(New clsSettings(commandData, message, elements))
      m_d.ShowDialog()
      ' Success
      Return Result.Succeeded
    Catch
      ' Failure
      Return Result.Failed
    End Try
  End Function

End Class