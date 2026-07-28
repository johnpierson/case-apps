Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].HiddenParameterToParameter.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand, IExternalCommandAvailability
    
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
        ' Settings
        Dim m_s As New clsSettings(commandData, elements)

        ' Family Document?
        If m_s.Doc.IsFamilyDocument Then
          message = "Run this tool in a project document..."
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

    Public Function IsCommandAvailable(applicationData As UIApplication, selectedCategories As CategorySet) As Boolean Implements IExternalCommandAvailability.IsCommandAvailable
      If applicationData.ActiveUIDocument Is Nothing Then Return False
      If applicationData.ActiveUIDocument.Document Is Nothing Then Return False
      If applicationData.ActiveUIDocument.Document.IsFamilyDocument Then Return False
      Return True
    End Function

  End Class
End Namespace