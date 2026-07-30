Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.ViewTemplates.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CEB749CFCF844AE8B76F40E3EBAA2FF7

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

          ' Basic Settings
          Dim m_s As New clsSettings(commandData)
          m_s.GetViews()

          ' Any View Templates?
          If m_s.ViewTemplates.Count < 1 Then
            message = "Your active model does not contain any View Templates..."
            Return Result.Failed
          End If

          ' Construct and Display the Form
          Using d As New form_Main(m_s)

            ' Show It
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