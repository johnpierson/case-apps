Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.Renamer.Data

Public Class form_Families

  Private _s As clsSettings

  ''' <summary>
  ''' Family Renaming Form
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

  End Sub
End Class