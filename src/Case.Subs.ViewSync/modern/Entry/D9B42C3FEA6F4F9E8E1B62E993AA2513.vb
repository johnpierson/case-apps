Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.ViewSync.Data

Namespace Entry

  ''' <summary>
  ''' Sync
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class D9B42C3FEA6F4F9E8E1B62E993AA2513

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

          ' Named Model
          If String.IsNullOrEmpty(m_s.DocName) Then
            message = "This tool requires that the model first be saved prior to use..."
            Return Result.Failed
          End If

          ' Missing Parameters?
          Select Case m_s.ConfigData.ConfigDataState

            Case EnumCfgState.IsOk

              ' INI File Success
              If Not m_s.ConfigData Is Nothing Then
                m_s.ConfigData.ReadFile()
                m_s.ConfigData.ViewData = New clsViews(m_s.ConfigData.ViewSyncPath)

                ' Test for Master
                Dim m_tesPath As String = Mid(m_s.DocName, 3)
                If m_s.ConfigData.DocumentationModelPath.ToLower.Contains(m_tesPath.ToLower) Then

                  Try
                    ' Log
                    m_s.ConfigData.WriteLogLine("Tag Sync Started (Master Model)", m_s.DocName, EnumLogKind.IsSync)
                  Catch
                  End Try

                  Try

                    ' This is the Master
                    m_s.GetViewports()

                    ' Write the Updated INI File
                    m_s.ConfigData.UpdateConfigFile()
                    m_s.ConfigData.ViewData.UpdateViewsFile()

                    Try
                      ' Log 
                      m_s.ConfigData.WriteLogLine("Tag Sync Completed (Master Model)", m_s.DocName, EnumLogKind.IsSync)
                    Catch
                    End Try

                    Using td As New Autodesk.Revit.UI.TaskDialog("Success!")
                      With td
                        .TitleAutoPrefix = False
                        .MainInstruction = m_s.ConfigData.ViewData.ViewPorts.Values.Count.ToString & " Viewports Records Update"
                        .MainContent = "Your view reference records have been updated..."
                        .Show()
                      End With
                    End Using

                  Catch

                    ' Error?
                    Using td As New Autodesk.Revit.UI.TaskDialog("Failed!")
                      With td
                        .TitleAutoPrefix = False
                        .MainInstruction = "Unidentified Failure! #Ex00102"
                        .MainContent = "Your view reference records failed to update..."
                        .Show()
                      End With
                    End Using

                  End Try

                Else

                  Try
                    ' Log 
                    m_s.ConfigData.WriteLogLine("Tag Sync Started (General Model)", m_s.DocName, EnumLogKind.IsSync)
                  Catch
                  End Try

                  ' Sync Placed Tags
                  If m_s.ConfigData.Families.Count > 0 Then

                    ' Find them and Sync
                    m_s.UpdateAll()

                    Try
                      ' Log 
                      m_s.ConfigData.WriteLogLine("Tag Sync Completed (General Model)", m_s.DocName, EnumLogKind.IsSync)
                    Catch
                    End Try

                    ' Any Orphans?
                    If m_s.FamTagHelpers(EnumTagState.IsOrphan).Count +
                       m_s.FamTagHelpers(EnumTagState.IsNull).Count > 0 Then

                      ' Inform User
                      Using td As New Autodesk.Revit.UI.TaskDialog("Orphaned and/or NULL Tags Detected!")
                        With td
                          .TitleAutoPrefix = False

                          Dim m_title As String = ""
                          If m_s.FamTagHelpers(EnumTagState.IsOrphan).Count > 0 Then
                            m_title += m_s.FamTagHelpers(EnumTagState.IsOrphan).Count.ToString & " Orphaned View References Detected"
                          End If
                          If m_s.FamTagHelpers(EnumTagState.IsNull).Count > 0 Then
                            If Not String.IsNullOrEmpty(m_title) Then
                              m_title += vbCr
                            End If
                            m_title += m_s.FamTagHelpers(EnumTagState.IsNull).Count.ToString & " NULL View References Detected"
                          End If

                          .MainInstruction = m_title
                          .MainContent = "A dialog will display where you can view and correct these issues."

                          .Show()
                        End With



                        Try
                          ' Log
                          m_s.ConfigData.WriteLogLine(m_s.FamTagHelpers(EnumTagState.IsOrphan).Count.ToString &
                                                      " Orphaned Tags and " &
                                                      m_s.FamTagHelpers(EnumTagState.IsNull).Count.ToString &
                                                      " NULL Tags Detected, Showing Find Form",
                                                      m_s.DocName,
                                                      EnumLogKind.IsSync)
                        Catch
                        End Try

                      End Using

                      ' Show the Orphans
                      Using d As New form_Find(m_s)
                        d.ShowDialog()
                      End Using

                    End If

                  Else

                    Dim m_logMsg As String = "The INI configuration file contains 0 listings of family tags to sync with"

                    Try
                      ' Log
                      m_s.ConfigData.WriteLogLine(m_logMsg, m_s.DocName, EnumLogKind.IsSync)
                    Catch
                    End Try

                    ' No Families Set to Sync
                    message = m_logMsg
                    Return Result.Failed

                  End If

                End If

              End If

            Case Else
              Try
                ' Log
                m_s.ConfigData.WriteLogLine("Please run Config prior to any other commands, setup not complete for current model", m_s.DocName, EnumLogKind.IsTag)
              Catch
              End Try

              ' Inform User
              Using td As New Autodesk.Revit.UI.TaskDialog("Incomplete Setup")
                With td
                  .TitleAutoPrefix = False
                  .MainInstruction = "Current Model Needs Configuration"
                  .MainContent = "Please run Config prior to any other commands, setup not complete for current model"
                  .Show()
                End With
              End Using

              ' Close
              Return Result.Cancelled

          End Select

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