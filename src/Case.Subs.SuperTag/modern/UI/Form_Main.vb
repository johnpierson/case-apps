Imports Autodesk.Revit.DB
Imports System.Diagnostics
Imports [Case].Subs.SuperTag.Data

Public Class Form_Main

  Private _s As clsSettings
  Private _closeForm As Boolean = False

  ''' <summary>
  ''' Form
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Setup
    Setup()

  End Sub

#Region "Form Visualization"

  Private Enum EnumFormViz
    IsStandBy
    IsProcessing
  End Enum

  ''' <summary>
  ''' Set appearance of controls based on state
  ''' </summary>
  ''' <param name="fv"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(fv As EnumFormViz)

    ' Style
    Select Case fv

      Case EnumFormViz.isProcessing
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonOk.Hide()
        ButtonHelp.Hide()
        ButtonViewsCheckAll.Hide()
        ButtonViewsCheckNone.Hide()
        ButtonSymbolsCheckAll.Hide()
        ButtonSymbolsCheckNone.Hide()

      Case EnumFormViz.isStandBy
        ProgressBar1.Hide()
        ButtonViewsCheckAll.Show()
        ButtonViewsCheckNone.Show()
        ButtonCancel.Show()
        ButtonOk.Show()
        ButtonHelp.Show()
        ButtonSymbolsCheckAll.Show()
        ButtonSymbolsCheckNone.Show()

    End Select

  End Sub

#End Region

  ''' <summary>
  ''' For Obfuscation
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub Setup()

    Try

      Text = "Subscription Super Tag" & _s.Version
      SetFormViz(EnumFormViz.isStandBy)

      ' Load Categories
      ComboBoxCategory.DisplayMember = "Name"
      For Each x As Category In _s.Categories.Values
        If x.Name.ToLower.Contains("<all>") Then Continue For
        ComboBoxCategory.Items.Add(x)
      Next
      ComboBoxCategory.SelectedIndex = 0

      ' Load Tag Families
      UpdateTagFamilies()

      ' Load Symbol Selections
      UpdateFamiliesList()

    Catch
    End Try

    Try

      ' Load Views
      UpdateViews()

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' List of Families Subject for Tagging
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateFamiliesList(Optional checkAll As Boolean = False)

    ' Clean List of Families
    Dim m_fams As New List(Of clsElement)

    ' Selected Category
    Dim m_c As Category = TryCast(ComboBoxCategory.SelectedItem, Category)

    ' If Spatial
    Dim m_lowerCat As String = m_c.Name.ToLower
    Select Case m_lowerCat

      Case "rooms"
        If Not String.IsNullOrEmpty(TextBoxTypeName.Text) Then

          For Each x As clsElement In _s.Rooms
            x.IsChecked = CheckAll
            If RadioButtonTypeContains.Checked = True Then
              ' Matches
              If x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            Else
              ' No Matches
              If Not x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            End If
          Next

        Else

          ' All Pass
          For Each x In _s.Rooms
            x.IsChecked = CheckAll
            m_fams.Add(x)
          Next

        End If

      Case "spaces"
        If Not String.IsNullOrEmpty(TextBoxTypeName.Text) Then

          For Each x As clsElement In _s.Spaces
            x.IsChecked = CheckAll
            If RadioButtonTypeContains.Checked = True Then
              ' Matches
              If x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            Else
              ' No Matches
              If Not x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            End If
          Next

        Else

          ' All Pass
          For Each x In _s.Spaces
            x.IsChecked = CheckAll
            m_fams.Add(x)
          Next

        End If

      Case "areas"
        If Not String.IsNullOrEmpty(TextBoxTypeName.Text) Then

          For Each x As clsElement In _s.Areas
            x.IsChecked = CheckAll
            If RadioButtonTypeContains.Checked = True Then
              ' Matches
              If x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            Else
              ' No Matches
              If Not x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
            End If
          Next

        Else

          ' All Pass
          For Each x In _s.Areas
            x.IsChecked = CheckAll
            m_fams.Add(x)
          Next

        End If

      Case Else

        ' Load from m_s
        For Each x As clsElement In _s.FamilyTypeNames.Values
          x.IsChecked = CheckAll

          If x.ElementFullName.ToLower.Contains("<all>") Then Continue For

          If Not m_c Is Nothing Then
            Try
              If Not x.ElementObject.Category.Name.ToLower = m_c.Name.ToLower Then Continue For
            Catch ex As Exception
              ' <All>
            End Try
          End If

          ' '' '' Add the Family
          '' ''m_fams.Add(x)

          If RadioButtonTypeContains.Checked = True Then
            ' Matches
            If x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
          Else
            ' No Matches
            If Not x.ElementFullName.ToLower.Contains(TextBoxTypeName.Text.ToLower) Then m_fams.Add(x)
          End If

        Next

    End Select

    ' Load the Families
    DataGridViewFamilies.DataSource = m_fams

  End Sub

  ''' <summary>
  ''' Update Tag Families List
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateTagFamilies()

    ' Clean Combo List
    ComboBoxTags.Items.Clear()
    ComboBoxTags.DisplayMember = "ElementFullName"

    ' Load from m_s
    For Each x As clsElement In _s.TagFamilies.Values
      If x.ElementFullName.ToLower.Contains("<all>") Then Continue For
      Dim m_c As Category = TryCast(ComboBoxCategory.SelectedItem, Category)
      If Not m_c Is Nothing Then
        Try
          Dim m_ct As String = Mid(m_c.Name.ToLower, 1, Len(m_c.Name.ToLower) - 1)
          If Not x.ElementObject.Category.Name.ToLower.Contains(m_ct) Then Continue For
        Catch ex As Exception
          ' <All>
        End Try
      End If
      ComboBoxTags.Items.Add(x)
    Next

    ' Default Item
    ComboBoxTags.SelectedIndex = 0

  End Sub

  ''' <summary>
  ''' Load the List of Views into the Form
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateViews(Optional checkAll As Boolean = False)

    ' Views
    Dim m_views As New List(Of clsView)

    ' Get Qualifying Views
    For Each x As clsView In _s.Views
      x.IsChecked = CheckAll
      If Not String.IsNullOrEmpty(TextBoxViewName.Text) Then

        If RadioButtonViewContains.Checked = True Then

          ' Only Matches
          If x.ViewName.ToLower.Contains(TextBoxViewName.Text.ToLower) Then
            m_views.Add(x)
          End If

        Else

          ' Only Non Matches
          If Not x.ViewName.ToLower.Contains(TextBoxViewName.Text.ToLower) Then
            m_views.Add(x)
          End If

        End If

      Else

        ' Add All
        m_views.Add(x)

      End If

    Next

    ' Bind to Views Collection
    DataGridViews.DataSource = m_views

    With DataGridViews.Columns(0)
      .Width = 30
      .HeaderText = ""
    End With
    With DataGridViews.Columns(1)
      .Width = 250
      .HeaderText = "Name"
    End With
    With DataGridViews.Columns(2)
      .Width = 100
      .HeaderText = "Level"
    End With
    With DataGridViews.Columns(3)
      .Width = 100
      .HeaderText = "Type"
    End With

  End Sub

  ''' <summary>
  ''' Tag the Elements
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub TagElementsInViews()

    ' The Tag Family to Use
    Dim m_tagFamily As FamilySymbol = Nothing
    Try
      Dim m_f As clsElement = ComboBoxTags.SelectedItem
      m_tagFamily = TryCast(m_f.ElementObject, FamilySymbol)
    Catch ex As Exception

    End Try

    If m_tagFamily Is Nothing Then
      MsgBox("Error Getting Tag Family", MsgBoxStyle.Critical, "Error")
      Exit Sub
    End If

    ' Get the List of Views and Symbols to Tag
    Dim m_toTagSymb As New Dictionary(Of String, clsElement)
    Dim m_toTagView As New List(Of clsView)

    ' The Symbols
    For Each x As clsElement In DataGridViewFamilies.DataSource
      If x.isChecked = True Then
        m_toTagSymb.Add(x.ElementObject.Id.ToString, x)
      End If
    Next

    ' End if Nothing Selected
    If m_toTagSymb.Count = 0 Then
      MsgBox("No Symbols Were Selected to Tag", MsgBoxStyle.Critical, "Nothing to Do")
      Exit Sub
    End If

    ' The Views
    For Each x As clsView In DataGridViews.DataSource
      If x.isChecked = True Then
        m_toTagView.Add(x)
      End If
    Next

    ' End if Nothing Selected
    If m_toTagView.Count = 0 Then
      MsgBox("No Views Were Selected to Tag Elements In", MsgBoxStyle.Critical, "Nothing to Do")
      Exit Sub
    End If

    ' Progress Bar
    With ProgressBar1
      .Minimum = 0
      .Maximum = m_toTagView.Count
      .Value = 0
    End With

    ' Category to Filter By
    Dim m_catFilter As Category = Nothing
    Try
      m_catFilter = TryCast(ComboBoxCategory.SelectedItem, Category)
    Catch ex As Exception
    End Try

    ' Transaction Started
    Dim m_t As New Transaction(_s.Doc, "Tag Elements in Multiple Views")
    m_t.Start()

    Try

      ' Iterate Each View
      For Each v As clsView In m_toTagView

        ' Step the Progressbar
        ProgressBar1.Increment(1)

        ' Track if the View is Active
        'Dim m_isViewActive As Boolean = v.GetViewElement.Document.ActiveView.Id = v.GetViewElement.Id

        ' Matching Tags in this View - Do not add second tag
        Dim m_colTags As New FilteredElementCollector(_s.Doc, v.GetViewElement.Id)
        m_colTags.WhereElementIsNotElementType()
        m_colTags.OfCategoryId(m_tagFamily.Category.Id)
        Dim m_instIdTagged As New List(Of ElementId)
        For Each t As Element In m_colTags.ToElements
          If t.Document.GetElement(t.GetTypeId).Id = m_tagFamily.Id Then
            ' This Tag Matches
            If TypeOf t Is Architecture.RoomTag Then
              Try
                Dim m_e1 As Architecture.RoomTag = TryCast(t, Architecture.RoomTag)
                m_instIdTagged.Add(m_e1.Room.Id)
              Catch ex As Exception
              End Try
            End If
            If TypeOf t Is AreaTag Then
              Try
                Dim m_e1 As AreaTag = TryCast(t, AreaTag)
                m_instIdTagged.Add(m_e1.Area.Id)
              Catch ex As Exception
              End Try
            End If
            If TypeOf t Is Mechanical.SpaceTag Then
              Try
                Dim m_e1 As Mechanical.SpaceTag = TryCast(t, Mechanical.SpaceTag)
                m_instIdTagged.Add(m_e1.Space.Id)
              Catch ex As Exception
              End Try
            End If
            If TypeOf t Is IndependentTag Then
              Try
                Dim m_e1 As IndependentTag = TryCast(t, IndependentTag)
                m_instIdTagged.AddRange(m_e1.GetTaggedLocalElementIds())
              Catch ex As Exception
              End Try
            End If
          End If
        Next

        ' Filter Instances of Category in this View
        Dim m_colInst As New FilteredElementCollector(_s.Doc, v.GetViewElement.Id)
        m_colInst.WhereElementIsNotElementType()
        m_colInst.OfCategoryId(m_catFilter.Id)

        ' Process the Returned Instances
        For Each s As Element In m_colInst.ToElements

          ' Is it a Room?
          If TypeOf s Is Architecture.Room Then

            Try

              Dim m_room As Architecture.Room = TryCast(s, Architecture.Room)

              ' Is it already Tagged?
              If Not m_instIdTagged.Contains(s.Id) Then

                ' Locate the Tag at the Insertion Point
                Dim m_locPt As LocationPoint = TryCast(s.Location, LocationPoint)
                Dim m_uv As New UV(m_locPt.Point.X, m_locPt.Point.Y)

                ' Place the tag
                Dim m_tag As Architecture.RoomTag = _s.Doc.Create.NewRoomTag(New LinkElementId(m_room.Id), m_uv, v.GetViewElement.Id)
                ' Set the Active Tag
                m_tag.ChangeTypeId(m_tagFamily.Id)

              End If

            Catch
            End Try

            ' Next Item
            Continue For

          End If

          ' Is it a Space?
          If TypeOf s Is Mechanical.Space Then

            Try

              ' Is it already Tagged?
              If Not m_instIdTagged.Contains(s.Id) Then

                ' Locate the Tag at the Insertion Point
                Dim m_location As Location = s.Location
                Dim m_xyz As XYZ = TryCast(m_location, LocationPoint).Point
                Dim m_uv As New UV(m_xyz.X, m_xyz.Y)

                ' Place the tag
                Dim m_tag As Mechanical.SpaceTag = _s.Doc.Create.NewSpaceTag(s, m_uv, v.GetViewElement)
                ' Set the Active Tag
                m_tag.ChangeTypeId(m_tagFamily.Id)

              End If

            Catch
            End Try

            ' Next Item
            Continue For
          End If

          ' Is it an Area?
          If TypeOf s Is Area Then

            Try

              ' Is it already Tagged?
              If Not m_instIdTagged.Contains(s.Id) Then

                ' Locate the Tag at the Insertion Point
                Dim m_locPt As LocationPoint = TryCast(s.Location, LocationPoint)
                Dim m_uv As New UV(m_locPt.Point.X, m_locPt.Point.Y)

                ' Place the tag
                Dim m_tag As AreaTag = _s.Doc.Create.NewAreaTag(v.GetViewElement, s, m_uv)
                ' Set the Active Tag
                m_tag.ChangeTypeId(m_tagFamily.Id)

              End If

            Catch
            End Try

            ' Next Item
            Continue For
          End If

          ' Get the Type Element
          Dim m_typeE As Element = s.Document.GetElement(s.GetTypeId)

          ' Only Operate on Matching Symbols
          If m_toTagSymb.ContainsKey(m_typeE.Id.ToString) Then

            ' Is it already Tagged?
            If Not m_instIdTagged.Contains(s.Id) Then

              Try

                ' Point or Curve?
                If TypeOf s.Location Is LocationPoint Then

                  ' Get the Location Point
                  Dim m_locPt As LocationPoint = TryCast(s.Location, LocationPoint)
                  If Not m_locPt Is Nothing Then

                    ' Insertion Point for Placement Elements
                    If Not m_locPt.Point Is Nothing Then

                      ' Place the tag
                      Dim m_tag As IndependentTag = IndependentTag.Create(_s.Doc,v.GetViewElement.Id, new Reference(s) , CheckBoxLeaders.Checked, TagMode.TM_ADDBY_CATEGORY, TagOrientation.Horizontal, m_locPt.Point)

                      ' Set the Active Tag
                      m_tag.ChangeTypeId(m_tagFamily.Id)

                    End If

                  End If

                Else

                  ' Location Curve
                  Dim m_locCrv As LocationCurve = TryCast(s.Location, LocationCurve)
                  If Not m_locCrv Is Nothing Then

                    ' Midpoint of Curve
                    Dim m_xyz As XYZ = m_locCrv.Curve.Evaluate(0.5, True)

                    ' Did we get a valid point?
                    If Not m_xyz Is Nothing Then

                      ' Place the tag
                      Dim m_tag As IndependentTag = IndependentTag.Create(_s.Doc,v.GetViewElement.Id, new Reference(s) , CheckBoxLeaders.Checked, TagMode.TM_ADDBY_CATEGORY, TagOrientation.Horizontal, m_xyz)

                      ' Set the Active Tag
                      m_tag.ChangeTypeId(m_tagFamily.Id)

                    End If

                  End If

                End If

              Catch
              End Try

            End If

          End If

        Next

      Next

      ' Commit on Success
      m_t.Commit()

      ' Close
      _closeForm = True

    Catch ex As Exception

      ' Rollback on Failure
      m_t.RollBack()

      ' Close
      _closeForm = True

    End Try

  End Sub

  ''' <summary>
  ''' Midpoint of two points
  ''' </summary>
  ''' <param name="pt1"></param>
  ''' <param name="pt2"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function GetMidpoint(pt1 As XYZ, pt2 As XYZ) As XYZ

    Try

      ' Return the Midpoint of a Line
      Return pt1.Add(pt2) / 2

    Catch
    End Try

    ' Failure
    Return Nothing

  End Function

  '' ''Private Function GetMidpointArc(pt1 As XYZ, pt2 As XYZ, cpt As XYZ) As XYZ
  '' ''  ' Return the Midpoint of an Arc
  '' ''End Function

#Region "Form Controls & Events"

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    ' Form Visibility
    SetFormViz(EnumFormViz.isProcessing)

    ' Tag Elements
    TagElementsInViews()

    ' Close the Form
    If _closeForm = True Then Close()

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Symbols - All
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSymbolsCheckAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSymbolsCheckAll.Click

    Try
      UpdateFamiliesList(True)
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Symbols - None
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSymbolsCheckNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSymbolsCheckNone.Click

    Try
      UpdateFamiliesList(False)
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Symbols - All
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonViewsCheckAll_Click(sender As System.Object, e As System.EventArgs) Handles ButtonViewsCheckAll.Click

    Try
      UpdateViews(True)
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Symbols - None
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonViewsCheckNone_Click(sender As System.Object, e As System.EventArgs) Handles ButtonViewsCheckNone.Click

    Try
      UpdateViews(False)
    Catch
    End Try

  End Sub

  Private Sub RadioButtonTypeContains_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonTypeContains.CheckedChanged
    Try
      UpdateFamiliesList()
    Catch
    End Try
  End Sub

  Private Sub RadioButtonTypeNotContains_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonTypeNotContains.CheckedChanged
    Try
      UpdateFamiliesList()
    Catch
    End Try
  End Sub

  Private Sub RadioButtonViewContains_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonViewContains.CheckedChanged
    Try
      UpdateViews()
    Catch
    End Try
  End Sub

  Private Sub RadioButtonViewNotContains_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonViewNotContains.CheckedChanged
    Try
      UpdateViews()
    Catch
    End Try
  End Sub

  Private Sub TextBoxTypeName_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxTypeName.TextChanged
    Try
      UpdateFamiliesList()
    Catch
    End Try
  End Sub

  Private Sub TextBoxViewName_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxViewName.TextChanged
    Try
      UpdateViews()
    Catch
    End Try
  End Sub

  Private Sub ComboBoxCategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxCategory.SelectedIndexChanged
    Try
      UpdateTagFamilies()
    Catch
    End Try
    Try
      UpdateFamiliesList()
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Launch Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBoxLogo_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://apps.case-inc.com/")
  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/subscription-super-tag")
  End Sub

#End Region

End Class