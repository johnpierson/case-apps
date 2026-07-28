Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Events
Imports Autodesk.Revit.UI
Imports [Case].Subs.ViewSync.Data

Namespace Entry

  ''' <summary>
  ''' Tag
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class D9B42C3FEA6F4F9E8E1B62E993AA2512

    Implements IExternalCommand

    Private _newTags As New List(Of Element)

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

          ' Missing Parameter?
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
                    m_s.ConfigData.WriteLogLine("Cannot Place Pseudo Tags: Attempted to place tag in  the Master Model", m_s.DocName, EnumLogKind.IsTag)
                  Catch
                  End Try

                  ' Inform User
                  Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Place Pseudo Tags")
                    With td
                      .TitleAutoPrefix = False
                      .MainInstruction = "Master Documentation Model Active"
                      .MainContent = "Pseudo View Sync Tags cannot be placed in the master documentation model. Place traditional view tag references in this model."
                      .Show()
                    End With
                  End Using

                  ' Close
                  Return Result.Cancelled

                End If

                ' Any Views?
                If m_s.ConfigData.ViewData.ViewPorts.Count = 0 Then

                  Try
                    ' Log
                    m_s.ConfigData.WriteLogLine("You have not yet synced the views from your master model yet. No views available to reference.", m_s.DocName, EnumLogKind.IsTag)
                  Catch
                  End Try

                  ' Inform User
                  Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Place Pseudo Tags")
                    With td
                      .MainInstruction = "No Reference Views"
                      .MainContent = "You havn't synced the views from your master model yet." & vbCr &
                                     "No views available to reference."
                      .TitleAutoPrefix = False
                      .Show()
                    End With
                  End Using

                  ' Close
                  Return Result.Cancelled

                End If

                ' Verify Families
                If m_s.ConfigData.Families.Count > 0 Then

                  ' Get the Detail Items
                  m_s.GetSymbols()

                  ' The Symbol to Place
                  Dim m_fs As FamilySymbol = Nothing
                  Dim m_vp As clsVp = Nothing

                  ' Construct and Display the Form
                  Using d As New form_Tag(m_s)
                    d.ShowDialog()

                    If d.IsCancel = True Then GoTo AllDone

                    ' Symbol to Place
                    If Not d.Fs Is Nothing Then

                      ' Get the Symbol
                      For Each famSym In m_s.FamTagSymbols
                        If famSym.Family.Name.ToLower = d.Fs.FamilyName.ToLower Then
                          If famSym.Name.ToLower = d.Fs.TypeName.ToLower Then

                            ' Found It
                            m_fs = famSym

                          End If
                        End If
                      Next

                      ' Viewport only when found symbol
                      If Not m_fs Is Nothing Then
                        If Not d.Vp Is Nothing Then
                          m_vp = d.Vp
                        End If
                      End If

                    End If

                  End Using

                  ' Valid VP?
                  If Not m_vp Is Nothing Then

                    Try
                      ' Log
                      m_s.ConfigData.WriteLogLine("Started to place tags", m_s.DocName, EnumLogKind.IsTag)
                    Catch
                    End Try

                    Try

                      ' Subscribe to Event
                      AddHandler commandData.Application.Application.DocumentChanged,
                        New EventHandler(Of DocumentChangedEventArgs)(AddressOf DocChange)

                      ' Place Tags
                      m_s.UiDoc.PromptForFamilyInstancePlacement(m_fs)

                      ' Unsubscribe to Event
                      RemoveHandler commandData.Application.Application.DocumentChanged,
                        New EventHandler(Of DocumentChangedEventArgs)(AddressOf DocChange)

                      ' Update Parameters of New Tags
                      m_s.UpdateTags(m_vp, _newTags)

                      Try
                        ' Log
                        m_s.ConfigData.WriteLogLine("Placed " & _newTags.Count.ToString & " Tags referencing " & m_vp.DetailNumber & "/" & m_vp.SheetNumber, m_s.DocName, EnumLogKind.IsTag)
                      Catch
                      End Try

                    Catch
                    End Try

                  Else

                    Try
                      ' Log
                      m_s.ConfigData.WriteLogLine("No view chosen for tag placement", m_s.DocName, EnumLogKind.IsTag)
                    Catch
                    End Try

                    ' Inform User
                    Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Place Tags")
                      With td
                        .TitleAutoPrefix = False
                        .MainInstruction = "No View Selected to Reference"
                        .MainContent = "A view selection is required to place reference tags."
                        .Show()
                      End With
                    End Using

                  End If

                Else

                  Try
                    ' Log
                    m_s.ConfigData.WriteLogLine("Config file missing family listings, cannot place tags", m_s.DocName, EnumLogKind.IsTag)
                  Catch
                  End Try

                  ' Inform User
                  Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Place Tags")
                    With td
                      .TitleAutoPrefix = False
                      .MainInstruction = "Incomplete Configuration"
                      .MainContent = "No Families Entered in Configuration File"
                      .Show()
                    End With
                  End Using

                End If

              Else

                ' Inform User
                Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Place Tags")
                  With td
                    .TitleAutoPrefix = False
                    .MainInstruction = "Incomplete Configuration"
                    .MainContent = "Please run Config prior to any other commands, setup not complete for current model"
                    .Show()
                  End With
                End Using

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

AllDone:

          ' Success
          Return Result.Succeeded


      Catch ex As Exception

        ' Failure
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

    ''' <summary>
    ''' Trace the New Tag Placements
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DocChange(sender As Object, e As DocumentChangedEventArgs)

      ' Get the New Elements
      Dim m_eids As List(Of ElementId) = e.GetAddedElementIds

      ' Convert to Elements
      Dim m_doc As Document = e.GetDocument
      For Each x In m_eids
        _newTags.Add(m_doc.GetElement(x))
      Next

    End Sub

  End Class
End Namespace