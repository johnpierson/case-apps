Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].SharedParameters.Data

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
        ' Settings File
        Dim m_s As New clsSettings(commandData, elements)
        If m_s.ActiveDoc.IsFamilyDocument = True Then
          message = "Sorry, but only the subscription version supports parameter loading for family documents. Contact info@case-inc.com for information on how to get subscription tool licenses."
          Return Result.Failed
        End If

        ' Construct and Display the Form
        Using d As New form_Main(m_s)
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