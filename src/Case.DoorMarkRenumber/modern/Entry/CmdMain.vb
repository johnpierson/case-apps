Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].DoorMarkRenumber.Data

Namespace Entry

  ''' <summary>
  ''' Renumber doors based on Door-To room number (A, B, C, D, etc.)
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

    Implements IExternalCommand

    ''' <summary>
    '''  Main entry point for every external command
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
        Dim m_s As New clsSettings(commandData)

        ' Generation Level
        If m_s.Doc.ActiveView.GenLevel Is Nothing Then
          Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Continue")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "View not Supported"
              .MainContent = "Please open a Floor Plan, Ceiling Plan, or Area Plan and try again."
              .Show()
            End With
          End Using
          Return Result.Cancelled
        End If

        ' Main Dialog
        Using d As New form_Main(m_s)
          d.ShowDialog()
        End Using

        ' Success
        Return Result.Succeeded

      Catch

        ' Fail
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace