Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports System.Linq
Imports [Case].Subs.ViewSync.Data

Public Class form_AddFamily

  Private _s As clsSettings

  Friend FamTags As New List(Of clsFam)

  ''' <summary>
  ''' New Family Addition 
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Startup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_AddFamily_Load(sender As Object, e As EventArgs) Handles Me.Load

    Try

      ' Header
      Text = "Select View Tag Families to Register" & _s.Version

      ' Fresh List
      Dim m_famTags As New List(Of clsFam)

      Dim m_symbols = From elem In New FilteredElementCollector(_s.Doc) _
                      .OfClass(GetType(FamilySymbol)) _
                      .OfCategory(BuiltInCategory.OST_DetailComponents)
                      Let ft = TryCast(elem, FamilySymbol)
                      Select ft

      ' Process Results
      If Not m_symbols Is Nothing Then
        m_famTags.AddRange(From x In m_symbols.ToList Select New clsFam(x))
      End If

      ' Bind to Control
      DataGridViewFamilies.DataSource = m_famTags

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Cancel
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

    ' From Grid
    For Each x As DataGridViewRow In DataGridViewFamilies.SelectedRows
      Try
        FamTags.Add(x.DataBoundItem)
      Catch
      End Try
    Next

    ' Anything?
    If FamTags.Count < 1 Then

      ' Warn and Do Not Exit
      MsgBox("No Families Set to Add!", MsgBoxStyle.Critical, "Nothing to Do!")

    Else

      ' Close
      Close()

    End If

  End Sub

#End Region

End Class