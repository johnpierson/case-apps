Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

<Transaction(TransactionMode.Manual)>
Public Class cmd

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

      ' Version
      If Not commandData.Application.Application.VersionName.Contains("2021") Then

        ' Failure
        message = "This Add-In was built for Revit 2021, please contact CASE for assistance..."
        Return Result.Failed

      End If

      ' Construct and Display the Form
      Using m_d As New form_Main(New clsSettings(commandData, elements))
        m_d.ShowDialog()
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