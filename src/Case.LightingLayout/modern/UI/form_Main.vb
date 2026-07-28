Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.IO
Imports System.Windows.Forms
Imports [Case].LightingLayout.Data

Public Class form_Main

  Private _s As clsSettings
  Private _selectedRoom As clsRoom = Nothing
  Private _selectedSpace As clsSpace = Nothing

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' form_Main_Shown event has code

  End Sub

  ''' <summary>
  ''' Points for Insertion
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub CalculatePoints()

    ' List of Points
    Dim m_pts As New List(Of XYZ)

    ' Names
    Dim m_name As String = ""
    Dim m_level As Level = Nothing

    ' Selection?
    If _selectedRoom Is Nothing Then
      If _selectedSpace Is Nothing Then
        MsgBox("Select a Room or Space Below First", MsgBoxStyle.Exclamation, "No Space or Room :(")
        Return
      End If
    End If

    Try

      ' Numeric Value Testing
      If Me.RadioButtonExact.Checked = True Then
        If Not IsNumeric(TextBoxDistanceU.Text) Then
          MsgBox("Text in 'U' distance textbox is not numeric...", MsgBoxStyle.Critical, "Error")
          Return
        End If
        If Not IsNumeric(TextBoxDistanceV.Text) Then
          MsgBox("Text in 'V' distance textbox is not numeric...", MsgBoxStyle.Critical, "Error")
          Return
        End If
      End If

      ' U Direction
      Dim m_uDistance As Double = 0
      Dim m_uCount As Integer = 1
      If Me.NumericUpDownU.Value > 1 Then
        m_uCount = Me.NumericUpDownU.Value
      End If
      If Not _selectedRoom Is Nothing Then
        m_uDistance = _selectedRoom.RoomWidth / (m_uCount + 1)
      Else
        m_uDistance = _selectedSpace.SpaceWidth / (m_uCount + 1)
      End If

      ' V Direction
      Dim m_vDistance As Double = 0
      Dim m_vCount As Integer = 1
      If Me.NumericUpDownV.Value > 1 Then
        m_vCount = Me.NumericUpDownV.Value
      End If
      If Not _selectedRoom Is Nothing Then
        m_vDistance = _selectedRoom.RoomLength / (m_vCount + 1)
      Else
        m_vDistance = _selectedSpace.SpaceLength / (m_vCount + 1)
      End If

      ' Exact Overrides
      If Me.RadioButtonExact.Checked = True Then
        m_uDistance = CDbl(TextBoxDistanceU.Text)
        m_vDistance = CDbl(TextBoxDistanceV.Text)
      End If

      ' Points
      For i = 1 To m_uCount
        For ii = 1 To m_vCount

          ' Spacing
          Dim m_x As Double = 0
          Dim m_y As Double = 0
          Dim m_z As Double = 0.25

          ' Space or Room
          If Not _selectedRoom Is Nothing Then

            ' Ray Tracing Height
            m_z += _selectedRoom.GetRoom.Level.Elevation

            ' Room - One Row?
            If m_uCount = 1 Then
              If Me.RadioButtonCp.Checked = True Then
                m_x = _selectedRoom.RoomU
              Else
                m_x = _selectedRoom.CentroidPoint.U
              End If
            Else
              ' Calc
              If Me.RadioButtonCp.Checked = True Then
                m_x = (_selectedRoom.RoomU - (0.5 * ((m_uCount - 1) * m_uDistance)) - m_uDistance) + (m_uDistance * i)
              Else
                m_x = (_selectedRoom.CentroidPoint.U - (0.5 * ((m_uCount - 1) * m_uDistance)) - m_uDistance) + (m_uDistance * i)
              End If
            End If

            ' Room - One Column?
            If m_vCount = 1 Then
              If Me.RadioButtonCp.Checked = True Then
                m_y = _selectedRoom.RoomV
              Else
                m_y = _selectedRoom.CentroidPoint.V
              End If
            Else
              ' Calc
              If Me.RadioButtonCp.Checked = True Then
                m_y = (_selectedRoom.RoomV - (0.5 * ((m_vCount - 1) * m_vDistance)) - m_vDistance) + (m_vDistance * ii)
              Else
                m_y = (_selectedRoom.CentroidPoint.V - (0.5 * ((m_vCount - 1) * m_vDistance)) - m_vDistance) + (m_vDistance * ii)
              End If
            End If

          Else

            ' Ray Tracing Height
            m_z += _selectedSpace.GetSpace.Level.Elevation

            ' Space - One Row?
            If m_uCount = 1 Then
              If Me.RadioButtonCp.Checked = True Then
                m_x = _selectedSpace.SpaceU
              Else
                m_x = _selectedSpace.CentroidPoint.U
              End If
            Else
              ' Calc
              If Me.RadioButtonCp.Checked = True Then
                m_x = (_selectedSpace.SpaceU - (0.5 * ((m_uCount - 1) * m_uDistance)) - m_uDistance) + (m_uDistance * i)
              Else
                m_x = (_selectedSpace.CentroidPoint.U - (0.5 * ((m_uCount - 1) * m_uDistance)) - m_uDistance) + (m_uDistance * i)
              End If
            End If

            ' Space - One Column?
            If m_vCount = 1 Then
              If Me.RadioButtonCp.Checked = True Then
                m_y = _selectedSpace.SpaceV
              Else
                m_y = _selectedSpace.CentroidPoint.V
              End If
            Else
              ' Calc
              If Me.RadioButtonCp.Checked = True Then
                m_y = (_selectedSpace.SpaceV - (0.5 * ((m_vCount - 1) * m_vDistance)) - m_vDistance) + (m_vDistance * ii)
              Else
                m_y = (_selectedSpace.CentroidPoint.V - (0.5 * ((m_vCount - 1) * m_vDistance)) - m_vDistance) + (m_vDistance * ii)
              End If
            End If

          End If

          ' Z is the Plane Height
          ' Update using ray tracing method from 6' above current level elevation

          ' Is the point in the room?
          Dim m_point As New XYZ(m_x, m_y, m_z)

          ' Space or Room
          If Not _selectedRoom Is Nothing Then
            If _selectedRoom.GetRoom.IsPointInRoom(m_point) = True Then

              ' Add the Point
              m_name = _selectedRoom.RmNumber & ": " & _selectedRoom.RmName
              m_level = _selectedRoom.GetRoom.Level
              m_pts.Add(m_point)

            End If
          End If
          If Not _selectedSpace Is Nothing Then
            If _selectedSpace.GetSpace.IsPointInSpace(m_point) = True Then

              ' Add the Point
              m_name = _selectedSpace.RmNumber & ": " & _selectedSpace.RmName
              m_level = _selectedSpace.GetSpace.Level
              m_pts.Add(m_point)

            End If
          End If

        Next
      Next

      '' For Testing - One Fixture
      'm_pts = New List(Of XYZ)
      'm_pts.Add(New XYZ(4.23228663530006, 6.8050635580461, 0))

      ' Fixture Height
      Dim m_height As Double = 8
      Try
        m_height = UnitConversion.CalculateDoubleLength(TextBoxHeight.Text)
      Catch
        MsgBox("Failed to calculate fixture mounting height from text entered...",
               MsgBoxStyle.Critical,
               "Cannot Use Height Value")
        Return
      End Try

      ' Insert Fixtures
      _s.PlaceFixtures(ComboBoxSymbol.SelectedItem,
                       m_pts,
                       m_level,
                       m_name,
                       m_height)

    Catch
    End Try

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Title
    Text = "Automated Lighting Fixture Layouts v" & _s.AppVersion

    Try

      ' Hosts
      With Me.ComboBoxSymbol
        .DataSource = _s.FixtureTypes
        .DisplayMember = "DisplayName"
      End With

    Catch
    End Try

    Try

      ' Mounting Height
      TextBoxHeight.Text = "8'"
      TextBoxHeight.Text = DisplayFormat(TextBoxHeight.Text)

    Catch
    End Try

    ' Spaces or Rooms
    If _s.Spaces.Count > 0 Then

      Try

        ' Spaces
        GroupBoxElements.Text = "Select a Space to Light Up"
        Dim m_e As New SortableBindingList(Of clsSpace)
        For Each x In _s.Spaces
          m_e.Add(x)
        Next
        DataGridViewRooms.DataSource = m_e

      Catch
      End Try

    Else

      Try

        ' Rooms
        GroupBoxElements.Text = "Select a Room to Light Up"
        Dim m_e As New SortableBindingList(Of clsRoom)
        For Each x In _s.Rooms
          m_e.Add(x)
        Next
        DataGridViewRooms.DataSource = m_e

      Catch
      End Try

    End If

    Try

      ' Retrieve Settings
      Dim m_path As String = "C:\Temp\CaseLightLayout.ini"
      If File.Exists(m_path) Then

        ' Line Count
        Dim iCnt As Integer = 0
        Using sw As New StreamReader(m_path)
          Do Until sw.EndOfStream

            ' Value
            Dim m_text As String = sw.ReadLine

            If iCnt = 0 Then
              If m_text.ToLower.StartsWith("t") Then
                RadioButtonEvenly.Checked = True
                RadioButtonExact.Checked = False
              Else
                RadioButtonEvenly.Checked = False
                RadioButtonExact.Checked = True
              End If
            End If

            If iCnt = 1 Then
              NumericUpDownU.Value = CInt(m_text)
            End If

            If iCnt = 2 Then
              NumericUpDownV.Value = CInt(m_text)
            End If

            If iCnt = 3 Then
              TextBoxDistanceU.Text = m_text
            End If

            If iCnt = 4 Then
              TextBoxDistanceV.Text = m_text
            End If

            If iCnt = 5 Then
             
            End If

            If iCnt = 6 Then
              Try
                For Each x As clsLight In Me.ComboBoxSymbol.Items
                  If x.DisplayName = m_text Then
                    ComboBoxSymbol.SelectedItem = x
                    Exit For
                  End If
                Next
              Catch
              End Try
            End If

            If iCnt = 7 Then
              If m_text.ToLower.StartsWith("t") Then
                RadioButtonCp.Checked = True
                RadioButtonCentroid.Checked = False
              Else
                RadioButtonCp.Checked = False
                RadioButtonCentroid.Checked = True
              End If
            End If

            ' Step
            iCnt += 1

          Loop
        End Using

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Update to Length Representation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxHeight_LostFocus(sender As Object, e As EventArgs) Handles TextBoxHeight.LostFocus

    Try

      ' Mounting Height
      TextBoxHeight.Text = DisplayFormat(TextBoxHeight.Text)

    Catch
      MsgBox("Error calculating height value, resetting to 8' AFF", MsgBoxStyle.Exclamation, "Can't Compute")
      TextBoxHeight.Text = DisplayFormat("8")
    End Try

  End Sub

  ''' <summary>
  ''' Room Selection
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub DataGridViewRooms_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewRooms.CellClick

    ' Default as Nothing
    _selectedRoom = Nothing

    Try

      ' Get the Row
      Dim m_row As DataGridViewRow = Me.DataGridViewRooms.Rows(e.RowIndex)

      Try

        ' Is it a Room?
        Dim m_rm As clsRoom = m_row.DataBoundItem
        If Not m_rm Is Nothing Then
          _selectedRoom = m_rm

          Try

            ' Zoom to Room
            _s.ActiveUIdoc.ShowElements(_selectedRoom.GetRoom)

          Catch
          End Try

        End If

      Catch
      End Try

      Try

        ' Is it a Space?
        Dim m_rm As clsSpace = m_row.DataBoundItem
        If Not m_rm Is Nothing Then
          _selectedSpace = m_rm

          Try

            ' Zoom to Room
            _s.ActiveUIdoc.ShowElements(_selectedSpace.GetSpace)

          Catch
          End Try

        End If

      Catch
      End Try

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    Try

      ' Save Settings
      Using sr As New StreamWriter("C:\Temp\CaseLightLayout.ini", False)
        sr.WriteLine(RadioButtonEvenly.Checked.ToString)
        sr.WriteLine(NumericUpDownU.Value.ToString)
        sr.WriteLine(NumericUpDownV.Value.ToString)
        sr.WriteLine(TextBoxDistanceU.Text)
        sr.WriteLine(TextBoxDistanceV.Text)
        
        Try
          Dim m_symb As clsLight = Me.ComboBoxSymbol.SelectedItem
          sr.WriteLine(m_symb.DisplayName)
        Catch
        End Try
        sr.WriteLine(RadioButtonCp.Checked.ToString)
      End Using

    Catch
    End Try

    ' Commit
    CalculatePoints()

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

#End Region

End Class