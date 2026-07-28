Imports Autodesk.Revit.DB
Imports System.Diagnostics
Imports [Case].Subs.Exceler8.Data

Public Class form_SmartSync

  Private _s As clsSettings

  ''' <summary>
  ''' Smart Import Form
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

  Private Sub DoSync()

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Load
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_SmartImport_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try

      ' Title
      Text = "Exceler8 Smart Import v" & _s.Version

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

    ' Sync
    DoSync()

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
    Try
      Process.Start("http://apps.case-inc.com/content/subscription-exceler8-revit")
    Catch
    End Try
  End Sub

#End Region

End Class