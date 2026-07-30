Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].LightingLayout.API
Imports [Case].LightingLayout.Data

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
        ' Settings
        Dim m_s As New clsSettings(commandData, elements)

        ' Verify Spaces and Rooms
        If m_s.Rooms.Count < 1 Then
          If m_s.Spaces.Count < 1 Then
            Using td As New Autodesk.Revit.UI.TaskDialog("Something is Missing")
              With td
                .TitleAutoPrefix = False
                .MainInstruction = "No Spaces or Rooms"
                .MainContent = "I need rooms or spaces in your model... please add some rooms or spaces to your model and try again"
                .Show()
              End With
            End Using
            Return Result.Cancelled
          End If
        End If

        ' Verify Fixture Symbols
        If m_s.FixtureTypes.Count < 1 Then
          Using td As New Autodesk.Revit.UI.TaskDialog("Something is Missing")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "No Lighting Fixtures Loaded"
              .MainContent = "Load one or more lighting fixture families and try again..."
              .Show()
            End With
          End Using
          Return Result.Cancelled
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