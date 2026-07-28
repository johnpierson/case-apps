Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.ViewSync.Data

Namespace Entry

  ''' <summary>
  ''' Find
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class D9B42C3FEA6F4F9E8E1B62E993AA2514

    Implements IExternalCommand

    ''' <summary>
    ''' Find Tags
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

          ' Settings
          Dim m_s As New clsSettings(commandData, elements)

          ' Missing Parameters?
          If m_s.ConfigData Is Nothing Then

            ' Major Config Failure
            message = "CASE View Sync Configuration error ex0081..."
            Return Result.Failed

          Else

            ' Read the File
            m_s.ConfigData.ReadFile()

            ' Named Model
            If String.IsNullOrEmpty(m_s.DocName) Then
              message = "This tool requires that the model first be saved prior to use..."
              Return Result.Failed
            End If

            ' State of Config
            Select Case m_s.ConfigData.ConfigDataState

              Case EnumCfgState.IsOk

                ' Get Symbols and Instances
                m_s.GetSymbols()
                m_s.GetInstances()

                ' Any Tags?
                Dim m_iCnt As Integer = 0
                For Each x In m_s.FamTagHelpers
                  m_iCnt += x.Value.Count
                Next
                If m_iCnt > 0 Then

                  ' Find Tags Form
                  Using d As New form_Find(m_s)
                    d.ShowDialog()
                  End Using

                Else

                  ' No Tags
                  Try
                    ' Log
                    m_s.ConfigData.WriteLogLine("No registered tag placements in current model", m_s.DocName, EnumLogKind.IsTag)
                  Catch
                  End Try

                  ' Inform User
                  Using td As New Autodesk.Revit.UI.TaskDialog("No Tag Placements")
                    With td
                      .TitleAutoPrefix = False
                      .MainInstruction = "0 Tags for Registered Tag Families"
                      .MainContent = "No registered tag placements in current model"
                      .Show()
                    End With
                  End Using

                  ' Close
                  Return Result.Cancelled

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

          End If

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