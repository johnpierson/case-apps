Imports Autodesk.Revit.DB
Imports System.Windows.Forms
Imports [Case].Subs.RoomsToMass.Data

Public Class form_Parameters

  Private _modelPath As String = ""
  Private _massParams As SortedDictionary(Of String, clsParameterDescription)
  Private _roomParams As SortedDictionary(Of String, clsParameterDescription)

#Region "Public Properties"

  ''' <summary>
  ''' Stored Parameter Mappings
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property MapConfig As clsConfig

#End Region

  ''' <summary>
  ''' Parameter Availability for Rooms and Masses
  ''' </summary>
  ''' <param name="m">Mass Parameters</param>
  ''' <param name="r">Room Parameters</param>
  ''' <param name="c">Saved Settings</param>
  ''' <param name="p">This model path</param>
  ''' <remarks></remarks>
  Public Sub New(m As SortedDictionary(Of String, clsParameterDescription),
                 r As SortedDictionary(Of String, clsParameterDescription),
                 ByRef c As clsConfig,
                 p As String)
    InitializeComponent()

    ' Widen Scope
    _massParams = m
    _roomParams = r
    MapConfig = c
    _modelPath = p

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Populate Datagrid
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadDatagrid()

    ' Populate Listings
    For Each x In _roomParams.Values

      ' No none's
      If x.Kind = StorageType.None Then Continue For

      ' Row
      Dim m_row As New DataGridViewRow

      ' Source Name
      Dim m_source As New DataGridViewTextBoxCell
      m_source.Value = x.Name
      m_row.Cells.Add(m_source)

      ' Combo
      Dim m_comboListing As New DataGridViewComboBoxCell
      m_comboListing.Items.Add("[Ignore]")
      m_comboListing.Value = "[Ignore]"

      ' Destination Options
      For Each p In _massParams.Values

        Select Case x.Kind

          Case StorageType.Double
            If p.Kind = StorageType.String Then
              m_comboListing.Items.Add(p.Name)
            End If
            If p.Kind = StorageType.Double Then
              m_comboListing.Items.Add(p.Name)
            End If

          Case StorageType.Integer
            If p.Kind = StorageType.String Then
              m_comboListing.Items.Add(p.Name)
            End If
            If p.Kind = StorageType.Integer Then
              m_comboListing.Items.Add(p.Name)
            End If

          Case StorageType.String
            If p.Kind = StorageType.String Then
              m_comboListing.Items.Add(p.Name)
            End If

        End Select

      Next

      ' Add to Row
      m_row.Cells.Add(m_comboListing)

      ' Add to Grid
      Me.DataGridViewParameters.Rows.Add(m_row)

    Next

    ' If Saved Settings - Read and Update Selections
    If MapConfig.Mappings.Count > 0 Then

      ' Set Values
      For Each x As DataGridViewRow In Me.DataGridViewParameters.Rows

        Try

          ' Source and Destination
          Dim m_s As String = x.Cells(0).Value.ToString
          Dim m_d As String = x.Cells(1).Value.ToString

          ' Find and Set
          For Each xx In MapConfig.Mappings

            ' Match
            If m_s.ToLower = xx.SourceName.ToLower Then
              If xx.DestinationName.ToLower.Contains("ignore") Then Exit For

              Try
                x.Cells(1).Value = xx.DestinationName
              Catch
              End Try

            End If

          Next

        Catch
        End Try

      Next

    End If

  End Sub

  ''' <summary>
  ''' Save Settings
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub SaveSettings()

    ' Fresh Config
    MapConfig = New clsConfig()
    MapConfig.ModelPath = _modelPath

    ' Return Saved Selections
    For Each x As DataGridViewRow In Me.DataGridViewParameters.Rows

      Try

        ' Add it
        MapConfig.Mappings.Add(New clsMapping(x.Cells(0).Value.ToString, x.Cells(1).Value.ToString))

      Catch
      End Try

    Next

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Startup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_Parameters_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Populate Listings
    LoadDatagrid()

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

    ' Close
    MapConfig = Nothing
    Me.Close()

  End Sub

  ''' <summary>
  ''' Save Settings
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonGenerateMasses_Click(sender As Object, e As EventArgs) Handles ButtonGenerateMasses.Click

    ' Save Settings
    SaveSettings()

    ' Close
    Me.Close()

  End Sub

#End Region

End Class