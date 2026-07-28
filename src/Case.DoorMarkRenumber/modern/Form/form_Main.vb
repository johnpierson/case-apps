Imports Autodesk.Revit.UI
Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports System.Linq
Imports [Case].DoorMarkRenumber.API
Imports [Case].DoorMarkRenumber.Data

Public Class form_Main

  ' Safety First
  Private Event DataError As DataGridViewDataErrorEventHandler

  ' Private Members
  Private _doEvents As Boolean = False
  Private _s As clsSettings
  Private _allDoors As New List(Of FamilyInstance)
  Private _allDoorsHelpers As New SortableBindingList(Of clsDoors)
  Private _lstDoorsFilteredClass As New SortableBindingList(Of clsDoors)
  Private _dicDoorsNewMark As New SortedDictionary(Of String, clsDoors)
  Private _firstLoad As Boolean = False

  Private _phaseOrder As SortedDictionary(Of Integer, Phase)

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(ByVal s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

#Region "Form Visibility Settings"

  ''' <summary>
  ''' Toggle Controls Based on State
  ''' </summary>
  ''' <param name="isProcessing"></param>
  ''' <remarks></remarks>
  Private Sub SetFormVisibility(isProcessing As Boolean)
    If isProcessing = True Then
      ButtonHelp.Hide()
      ButtonOK.Hide()
      ButtonCancel.Hide()
      ProgressBar1.Show()
    Else
      ButtonHelp.Show()
      ButtonOK.Show()
      ButtonCancel.Show()
      ProgressBar1.Hide()
    End If
  End Sub

#End Region

#Region "Private Members"

  ''' <summary>
  ''' Calculate the new door mark id's
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub RenumberDoors()

    ' Clear the collections and dictionaries
    _lstDoorsFilteredClass = New SortableBindingList(Of clsDoors)
    _dicDoorsNewMark = New SortedDictionary(Of String, clsDoors)

    Try

      ' Element Processing
      For Each d As clsDoors In _allDoorsHelpers

        ' Phase must match selection
        If d.Phase.ToLower = LabelPhaseName.Text.ToLower Then

          ' Scope
          If RadioButtonCurrentLevel.Checked = True Then

            ' Belong on current view level?
            If d.Level.ToUpper <> _s.Doc.ActiveView.GenLevel.Name.ToUpper Then Continue For

          Else

            ' Selected Filter
            If _s.ActiveUIdoc.Selection.GetElementIds().Count > 0 Then

              ' Selected elements
              Dim addE As Boolean = False
              For Each e As ElementId In _s.ActiveUIdoc.Selection.GetElementIds()
                If d.DoorInstance.Id.Value = e.Value Then
                  d.AllowEdits = True
                End If
              Next
              If addE = False Then
                d.AllowEdits = False
              End If

            End If

          End If

          ' Ignore NULL rooms
          If String.IsNullOrEmpty(d.toRoom) And String.IsNullOrEmpty(d.fromRoom) Then Continue For

          ' Room Number
          Dim rmNumber As String = ""
          If String.IsNullOrEmpty(d.toRoom) Then
            rmNumber = d.fromRoom
          Else
            rmNumber = d.toRoom
          End If

          ' Rumber to Check?
          If Not String.IsNullOrEmpty(rmNumber) Then

            ' Alpha Iteration starting from A
            Dim cntAscii As Integer = 65
            Do While _dicDoorsNewMark.ContainsKey(rmNumber & (Chr(cntAscii).ToString)) = True
              cntAscii = cntAscii + 1
            Loop

            ' Set the prospective Mark value
            rmNumber = rmNumber & (Chr(cntAscii))
            d.MarkSuggestion = rmNumber

            ' Uncheck if suggestion is same as the mark
            If d.MarkNow.ToLower = d.MarkSuggestion.ToLower Then d.AllowEdits = False

            ' Add the unique NewMark to the dictionary...
            If Not _dicDoorsNewMark.ContainsKey(rmNumber) Then
              _dicDoorsNewMark.Add(rmNumber, d)
            End If

          Else

            ' Empty
            Dim m_todo As String = ""

          End If

          ' Filter by Editable?
          'If d.AllowEdits = False And Me.CheckBoxShowChecked.Checked = True Then Continue For

          ' Post to Datagrid if criteria valid
          _lstDoorsFilteredClass.Add(d)

        End If
      Next

    Catch
    End Try

    ' Cycle through the collection and fix singletons (no A)... fix and then apply to datagrid
    Dim lastDoor As clsDoors = Nothing
    For Each d As clsDoors In _dicDoorsNewMark.Values

      ' If no B after an A, set to remove A
      If lastDoor IsNot Nothing Then
        If Strings.Right(d.MarkSuggestion, 1) = "A" And Strings.Right(lastDoor.MarkSuggestion, 1) = "A" Then
          lastDoor.MarkSuggestion = Strings.Replace(lastDoor.MarkSuggestion, "A", "", , , CompareMethod.Text)
        End If

      End If

      ' Last One
      lastDoor = d

    Next

    DisplayDoorItems()

    ' Set the Form to Standby
    SetFormVisibility(False)

    Dim m_failCnt As Integer = 0

    ' Analyze Room Data
    For Each x As DataGridViewRow In DataGridViewDoors.Rows

      ' Do we have a value for the Suggested Mark?
      If String.IsNullOrEmpty(x.Cells("MarkSuggestion").Value) Then

        ' Count as Failure - Missing Room in Last Phase
        m_failCnt += 1

      End If

    Next

    ' Did we get any failures?
    If m_failCnt > 0 Then

      ' Report to User
      MsgBox(m_failCnt.ToString & " out of a possible " & DataGridViewDoors.Rows.Count.ToString & " elements have missing room data!" & vbCr &
             "This condition is due to the room elements either being placed in an earlier phase or missing alltogether." & vbCr & vbCr &
             "'To' and 'From' Room data only exists for rooms placed in the last and newest phase of the model!" & vbCr & vbCr &
             "As a result, this tool will only analyze rooms placed in the newset phase of the model.",
             MsgBoxStyle.Information + MsgBoxStyle.OkOnly,
             "Room Elements")

    End If

  End Sub

  ''' <summary>
  ''' This function will actually renumber the door marks
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateDoors()

    ' The Stats
    Dim m_CntModified As Integer = 0
    Dim m_CntSkipped As Integer = 0

    ' New Transaction
    Using t As New Transaction(_s.Doc, "Case Renumber Door Marks")
      If t.Start() = TransactionStatus.Started Then

        Try

          ' Write all updates to objects
          For Each itemFix As clsDoors In _lstDoorsFilteredClass
            ' Only edit items checked as "Allow" and avoid blank NewMarks
            If itemFix.AllowEdits = True Then

              ' NULL Suggestion?
              If itemFix.MarkSuggestion = "" Then
                m_CntSkipped += 1
                Continue For
              End If

              ' This is new, test it first
              itemFix.SetDoorMark()
              m_CntModified += 1

            End If
          Next

          ' Commit
          t.Commit()

        Catch
        End Try

      End If
    End Using

    Dim m_StatusString As String = m_CntModified & " Doors were succesfully renumbered!"
    If m_CntSkipped > 0 Then
      m_StatusString = m_StatusString & vbCr & m_CntSkipped & " Doors were skipped possibly due to the lack of a room on either side of the swing."
    End If

    ' Report what we did...
    Using td As Autodesk.Revit.UI.TaskDialog = New Autodesk.Revit.UI.TaskDialog("Door Renumbering Statistics")
      With td
        .MainInstruction = "Door Mark Renumbering Statistics"
        .MainContent = m_StatusString
        .Show()
      End With
    End Using

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Displays the filtered doors in the data grid view
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub DisplayDoorItems()

    ' Anything
    If _allDoorsHelpers.Count = 0 Then Return

    ' Clear Main List
    _lstDoorsFilteredClass = New SortableBindingList(Of clsDoors)

    ' Filter Kind
    If Me.RadioButtonCurrentLevel.Checked = True Then

      ' Level
      For Each d As clsDoors In _allDoorsHelpers
        If d.MarkNow = d.MarkSuggestion Then d.AllowEdits = False
        If Me.CheckBoxShowChecked.Checked = True Then
          If d.AllowEdits = True And _
              d.Phase = Me.LabelPhaseName.Text And _
              d.Level = _s.Doc.ActiveView.GenLevel.Name Then _lstDoorsFilteredClass.Add(d)
        Else
          If d.Phase = Me.LabelPhaseName.Text And _
              d.Level = _s.Doc.ActiveView.GenLevel.Name Then _lstDoorsFilteredClass.Add(d)
        End If
      Next

    Else

      ' Selection
      For Each d As clsDoors In _allDoorsHelpers
        If d.MarkNow = d.MarkSuggestion Then d.AllowEdits = False

        ' All non selected doors will remain unchecked
        If _s.ActiveUIdoc.Selection.GetElementIds().Count > 0 Then
          If d.Phase = Me.LabelPhaseName.Text Then
            If Me.CheckBoxShowChecked.Checked = True Then
              ' Only allow edits for the selected elements
              Dim addE As Boolean = False
              For Each myDr As ElementId In _s.ActiveUIdoc.Selection.GetElementIds()
                If d.DoorInstance.Id.ToString.ToUpper = myDr.ToString.ToUpper And d.AllowEdits = True Then
                  addE = True
                End If
              Next
              If addE = False Then
                d.AllowEdits = False
              Else
                _lstDoorsFilteredClass.Add(d)
              End If
            Else
              ' Only allow edits for the selected elements
              Dim addE As Boolean = False
              For Each myDr As ElementId In _s.ActiveUIdoc.Selection.GetElementIds()
                If d.DoorInstance.Id.ToString.ToUpper = myDr.ToString.ToUpper Then
                  addE = True
                End If
              Next
              If addE = False Then
                d.AllowEdits = False
              Else
                _lstDoorsFilteredClass.Add(d)
              End If
            End If
          End If
        End If
      Next

    End If

    Try

      If _lstDoorsFilteredClass.Count = 0 Then
        MsgBox("No more Mark values need changing...",
               MsgBoxStyle.Information,
               "All Markse are OK")
        Close()

      Else

        ' Quantity
        LabelInstances.Text = _lstDoorsFilteredClass.Count.ToString & " Doors in Scope"

        ' Populate Datagridview with the doors list
        'Me.DataGridViewDoors.DataSource = Nothing
        DataGridViewDoors.DataSource = _lstDoorsFilteredClass

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Zoom to all selected doors in Data Grid View ; Event run
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ZoomToElementsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomToElementsToolStripMenuItem.Click

    ' Get the elementID's from the selected rows as an element selection
    Dim eSet As New ElementSet
    Dim selectedRowCount As Int32 = DataGridViewDoors.Rows.GetRowCount(DataGridViewElementStates.Selected)
    If selectedRowCount = 0 Then
      Exit Sub
    Else
      For i As Integer = 0 To selectedRowCount - 1
        If DataGridViewDoors.SelectedRows(i).Visible Then
          Dim myE As Element = _s.Doc.GetElement(DataGridViewDoors.SelectedRows(i).Cells(9).Value)
          eSet.Insert(myE)
        End If
      Next
    End If

    Try

      ' Zoom to the selected objects
      _s.ActiveUIdoc.ShowElements(eSet)

    Catch
    End Try

  End Sub

  Private Sub ToggleViewCategory(catId As ElementId, isVisible As Boolean)
    If _s.Doc.IsModifiable = False Then
      Dim m_todo As String = ""
    End If
  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Starts the primary scan after form loads
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Doors_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Processing Visibility
    SetFormVisibility(True)

    Try

      ' Form Setup
      Text = "Door Mark Renumber by Room Number " & _s.AppVersion

      ' Get Current View Phase
      Dim m_currentPhaseId As ElementId = _s.Doc.ActiveView.Parameter(BuiltInParameter.VIEW_PHASE).AsElementId

      ' List all Phases in Model
      _phaseOrder = New SortedDictionary(Of Integer, Phase)
      For Each x As Phase In _s.Doc.Phases
        _phaseOrder.Add(x.Id.Value, x)

        ' Set the Phase Name
        If x.Id.Value = m_currentPhaseId.Value Then
          LabelPhaseName.Text = x.Name
        End If

      Next

      ' LINQ Door Instances from current view
      Dim m_fiList = From ele In New FilteredElementCollector(_s.Doc, _s.Doc.ActiveView.Id) _
                     .OfCategory(BuiltInCategory.OST_Doors) _
                     .OfClass(GetType(FamilyInstance))
                     Let fi = TryCast(ele, FamilyInstance)
                     Where fi.CreatedPhaseId.Value = m_currentPhaseId.Value
                     Select fi

      ' Keep Doors Generated in Current Phase of View Only
      If Not m_fiList Is Nothing Then _allDoors = m_fiList.ToList()

      ' Progress Setup
      With ProgressBar1
        .Minimum = 0
        .Maximum = _allDoors.Count
        .Value = 0
      End With

      ' Enable current selection only if there is one
      If Not _s.ActiveUIdoc.Selection.GetElementIds.Count < 1 Then
        RadioButtonSelected.Enabled = True
        RadioButtonSelected.Checked = True
      End If

      ' Collection of Door Objects
      For Each d As FamilyInstance In _allDoors

        Try
          ' Step the Progress
          ProgressBar1.Increment(1)
        Catch
        End Try

        Try

          ' Door Helper
          Dim m_clsDoor As New clsDoors(d, _phaseOrder)
          
          ' Buildup the Dictionary
          Dim m_roomNumber As String = ""
          If m_clsDoor.toRoom = "" Then
            m_roomNumber = m_clsDoor.fromRoom
          Else
            m_roomNumber = m_clsDoor.toRoom
          End If

          ' Add the object to the dictionary
          _allDoorsHelpers.Add(m_clsDoor)

        Catch
        End Try

      Next

      _firstLoad = True

      ' Logically update numerals for New Mark Value
      RenumberDoors()

    Catch
    End Try

    _doEvents = True

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-door-mark-updater")
  End Sub

  ''' <summary>
  ''' Renumber Doors
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click

    ' Changes
    UpdateDoors()

    ' Close
    Close()

  End Sub

  Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  Private Sub CheckBoxShowChecked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowChecked.CheckedChanged
    DisplayDoorItems()
  End Sub

  Private Sub RadioButtonCurrentLevel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonCurrentLevel.CheckedChanged
    If _firstLoad = True Then DisplayDoorItems()
  End Sub

  Private Sub RadioButtonSelected_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonSelected.CheckedChanged
    If _firstLoad = True Then DisplayDoorItems()
  End Sub

  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  Private Sub DataGridViewDoors_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DataGridViewDoors.DataError
    ' Leave empty... to prevent lockups
  End Sub

#End Region

End Class