Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].RoomSync.Data

Public Class form_Sync

  Private _s As clsSettings

  ''' <summary>
  ''' Sync Room Data 
  ''' </summary>
  ''' <param name="s">Settings Class</param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Form Visibility
  ''' </summary>
  ''' <param name="isProcessing"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(isProcessing As Boolean)

    ' Controls
    ProgressBar1.Visible = isProcessing
    ButtonCancel.Visible = Not isProcessing
    ButtonSave.Visible = Not isProcessing
    ComboBoxDocuments.Visible = Not isProcessing

  End Sub

  ''' <summary>
  ''' Sync the Room Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SyncRooms()

    Dim m_iCntNewRooms As Integer = 0

    Try

      ' Selected External Document for Sync
      If ComboBoxDocuments.SelectedIndex < 0 Then Return
      Dim m_doc As Document = ComboBoxDocuments.SelectedItem
      If m_doc Is Nothing Then Return

      ' Location Point
      ' Dim m_modelXyz As XYZ = m_doc.ProjectLocations

      ' Progress 
      With ProgressBar1
        .Visible = True
        .Minimum = 0
        .Maximum = _s.Rooms.Count + ComboBoxDocuments.Items.Count - 1
        .Value = 0
      End With
      System.Windows.Forms.Application.DoEvents()

      ' Get External Rooms
      Dim m_externalRooms As List(Of clsRoom) = _s.GetRoomsFromLinkedModel(m_doc)

      ' Find Existing Placed Rooms
      For Each extRm As clsRoom In m_externalRooms

        ' Linked Room Insertion
        Dim m_linkedInstance As RevitLinkInstance = _s.LinkedDocs(m_doc.Title)
        Dim m_linkedXyz As XYZ = extRm.GetTransformInsertion(m_linkedInstance)

        ' Find Matching Local
        For Each locRm As clsRoom In _s.Rooms

          ' Match Location
          If Math.Round(m_linkedXyz.Z, 0) = Math.Round(locRm.RoomCentroid.Z, 0) Then
            If Math.Round(m_linkedXyz.X, 0) = Math.Round(locRm.RoomCentroid.X, 0) Then
              If Math.Round(m_linkedXyz.Y, 0) = Math.Round(locRm.RoomCentroid.Y, 0) Then

                ' Match Found
                extRm.RoomMatchProcessed = True
                locRm.RoomMatchProcessed = True

              End If
            End If
          End If

        Next

      Next

      ' Place Missing Rooms
      For Each extRm As clsRoom In m_externalRooms

        ' Linked Room Insertion
        Dim m_linkedInstance As RevitLinkInstance = _s.LinkedDocs(m_doc.Title)
        Dim m_linkedXyz As XYZ = extRm.GetTransformInsertion(m_linkedInstance)

        ' Processed?
        If extRm.RoomMatchProcessed = True Then Continue For

        Try

          ' Level
          Dim m_insertLevel As Level = Nothing
          For Each m_localLevel As Level In _s.Levels

            ' Integers
            Dim i1 As Integer = CType(m_localLevel.Elevation, Integer)
            Dim i2 As Integer = CType(extRm.RoomElement.Level.Elevation, Integer)
            If i1 = i2 Then
              m_insertLevel = m_localLevel
              Exit For
            End If

          Next

          ' Make Sure we Have a Valid Level
          If Not m_insertLevel Is Nothing Then

            ' UV
            Dim m_uv As New UV(m_linkedXyz.X, m_linkedXyz.Y)

            ' Place the Room
            Dim m_rm As Architecture.Room = _s.ActiveDoc.Create.NewRoom(m_insertLevel, m_uv)
            If Not m_rm Is Nothing Then

              m_iCntNewRooms += 1

              ' Update Matching Parameters
              For Each p As Parameter In m_rm.Parameters
                If p.IsReadOnly = True Then Continue For

                Dim m_pExt As Parameter = extRm.RoomElement.Parameter(p.Definition.Name)
                If m_pExt Is Nothing Then Continue For
                'If m_pExt.Definition.IsReadOnly = True Then Continue For

                Try

                  Dim m_value As String = GetParamValue(extRm.RoomElement, p.Definition.Name, True)
                  If Not m_value Is Nothing Then
                    SetParamValue(m_rm, p.Definition.Name, m_value)
                  End If

                Catch
                End Try

              Next

            End If

          End If

        Catch
        End Try

      Next

      ' Update Parameters for existing rooms
      For Each locRm As clsRoom In _s.Rooms

        Try
          ' Step the Progress
          ProgressBar1.Increment(1)
          System.Windows.Forms.Application.DoEvents()
        Catch
        End Try

        Try

          ' Find the Matching Linked Room
          For Each extRm As clsRoom In m_externalRooms

            ' Linked Room Insertion
            Dim m_linkedInstance As RevitLinkInstance = _s.LinkedDocs(m_doc.Title)
            Dim m_linkedXyz As XYZ = extRm.GetTransformInsertion(m_linkedInstance)

            If Math.Round(m_linkedXyz.Z, 1) = Math.Round(locRm.RoomCentroid.Z, 1) Then
              If Math.Round(m_linkedXyz.X, 1) = Math.Round(locRm.RoomCentroid.X, 1) Then
                If Math.Round(m_linkedXyz.Y, 1) = Math.Round(locRm.RoomCentroid.Y, 1) Then

                  ' Update Matching Parameters
                  For Each p As Parameter In locRm.RoomElement.Parameters
                    If p.IsReadOnly = True Then Continue For

                    Dim m_extParam As Parameter = extRm.RoomElement.Parameter(p.Definition.Name)
                    If Not m_extParam Is Nothing Then
                      If m_extParam.IsReadOnly = True Then Continue For

                      SetParamValue(locRm.RoomElement,
                                    p.Definition.Name,
                                    GetParamValue(extRm.RoomElement,
                                                  p.Definition.Name,
                                                  False))

                    End If

                  Next

                End If

              End If

            End If

          Next

        Catch
        End Try

      Next

    Catch
    End Try

    ' Report to the User
    Dim m_msg As String = ""
    If _s.Rooms.Count > 0 Then
      m_msg = "Updated Room Data for " & _s.Rooms.Count & " existing rooms!"
    End If
    If m_iCntNewRooms > 0 Then
      If String.IsNullOrEmpty(m_msg) Then
        m_msg = "Added " & m_iCntNewRooms & " NEW Rooms!"
      Else
        m_msg += vbCr & "Added " & m_iCntNewRooms & " NEW Rooms!"
      End If
    End If
    Using td As New Autodesk.Revit.UI.TaskDialog("Sync Statistics")
      With td
        .TitleAutoPrefix = False
        .MainInstruction = "Room Sync Results"
        .MainContent = m_msg
        .Show()
      End With
    End Using

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Startup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Sync_Load(sender As Object, e As EventArgs) Handles Me.Load

    Try

      ' Form Viz
      SetFormViz(False)

      ' Loading Sync Status
      Text = "CASE Pro Room Sync v" & My.Application.Info.Version.ToString()

      ' Documents
      If _s.LinkedDocs.Count < 1 Then
        Using td As New Autodesk.Revit.UI.TaskDialog("Cannot Continue")
          With td
            .TitleAutoPrefix = False
            .MainInstruction = "No Linked Models"
            .MainContent = "No linked model to use as source!" & vbCr &
               "Link in a model containing rooms and try again."
            .Show()
          End With
        End Using
        Close()
      Else
        For Each x In _s.AvailableModels.Values
          ComboBoxDocuments.Items.Add(x)
        Next
        ComboBoxDocuments.DisplayMember = "Title"
        ComboBoxDocuments.SelectedIndex = 0
      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Sync Values
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSave_Click(sender As System.Object, e As EventArgs) Handles ButtonSave.Click

    SetFormViz(True)

    ' Start a New Transaction
    Using t As New Transaction(_s.ActiveDoc, "CASE Room Sync")
      If t.Start Then

        Try

          ' Sync
          SyncRooms()
          t.Commit()
          Close()

        Catch
        End Try

      End If
    End Using

    SetFormViz(False)

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

#End Region

End Class