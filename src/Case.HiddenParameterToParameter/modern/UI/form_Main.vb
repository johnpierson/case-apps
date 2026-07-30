Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Windows.Forms
Imports System.Diagnostics
Imports [Case].HiddenParameterToParameter.Data

Public Class form_Main

  Private _s As clsSettings
  Private _events As Boolean = False

  ''' <summary>
  ''' Constructor
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
  Private Sub form_Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    ' Form
    Text = "Hidden Parameter to Parameter v" & _s.Version
    ProgressBar1.Hide()
    ComboBoxCategory.DataSource = _s.Cats
    ComboBoxCategory.DisplayMember = "DisplayName"
    ComboBoxCategory.SelectedIndex = 0

    ' Enable Events
    _events = True

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-migrate-parameter-parameter")
  End Sub

  ''' <summary>
  ''' Show Parameters for Selected Category
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxCategory_SelectedIndexChanged(sender As System.Object,
                                                    e As System.EventArgs) Handles ComboBoxCategory.SelectedIndexChanged

    ' Ignore When Events Disabled
    If _events = False Then Exit Sub

    ' Process the Category Object
    Dim m_c As clsCategory = ComboBoxCategory.SelectedItem
    If m_c.CatParamsText.Count = 0 And m_c.CatParamsElement.Count = 0 Then
      m_c.GetInstances()

      ' Warn when no elements
      If m_c.CatInstances.Count < 1 Then
        MsgBox("There are no element instances for this category in your model", MsgBoxStyle.Information, "No Elements")
      End If

      ComboBoxParamSource.DataSource = m_c.CatParamsElement
      ComboBoxParamTarget.DataSource = m_c.CatParamsText
      Try
        ComboBoxParamSource.SelectedIndex = 0
      Catch
      End Try
      Try
        ComboBoxParamTarget.SelectedIndex = 0
      Catch
      End Try
    End If

    ' Allow Port
    If ComboBoxParamSource.Items.Count = 0 Or ComboBoxParamTarget.Items.Count = 0 Then
      ButtonOk.Enabled = False
    Else
      ButtonOk.Enabled = True
    End If

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object,
                             e As EventArgs) Handles ButtonOk.Click

    ' Results
    Dim iTry As Integer = 0
    Dim iFail As Integer = 0

    Try

      ' The Selected Category
      Dim m_c As clsCategory = ComboBoxCategory.SelectedItem
      If m_c Is Nothing Then
        MsgBox("Failed to Get Selected Category Element Data :(",
               MsgBoxStyle.Critical,
               "Error")
        Exit Sub
      End If

      ' Form Setup
      ButtonOk.Hide()
      ButtonCancel.Hide()
      ButtonHelp.Hide()
      iTry = m_c.CatInstances.Count

      ' Progress Bar
      With ProgressBar1
        .Show()
        .Minimum = 0
        .Value = 0
        .Maximum = m_c.CatInstances.Count + 1
      End With

      ' Process Each Item
      For Each x In m_c.CatInstances

        ' Progress
        Try
          ProgressBar1.Increment(1)
          Application.DoEvents()
        Catch
        End Try

        ' Start a New transaction
        Using t As New Transaction(_s.Doc, "Element Parameters")
          If t.Start = TransactionStatus.Started Then

            Try

              Dim m_sourceValue As String = ""
              If ComboBoxParamSource.SelectedItem.ToString().ToLower() = "elementid" Then
                m_sourceValue = x.Id.ToString()
              Else
                Dim m_sourceParam As New clsParameter(x.LookupParameter(ComboBoxParamSource.SelectedItem.ToString))
                If m_sourceParam.ParameterObject Is Nothing Then Continue For
                If RadioButtonText.Checked = True Then
                  m_sourceValue = m_sourceParam.ValueString
                Else
                  m_sourceValue = m_sourceParam.Value
                End If
              End If
              If String.IsNullOrEmpty(m_sourceValue) Then Continue For
              Dim m_targetParam As New clsParameter(x.LookupParameter(ComboBoxParamTarget.SelectedItem.ToString))
              If m_targetParam.ParameterObject IsNot Nothing Then
                m_targetParam.Value = m_sourceValue
              End If

              ' Success
              t.Commit()

            Catch ex As Exception

              ' Failure
              iFail += 1
              t.RollBack()

            End Try

          End If
        End Using

      Next

    Catch ex As Exception

    End Try

    ' Report Results
    Using td As New Autodesk.Revit.UI.TaskDialog("Element Update Results")
      td.MainInstruction = "Elements Updated!"
      td.MainContent = String.Format("Attempted to Update {0} Elements" & vbCr & vbCr & "{1} Failures",
                                     iTry.ToString, iFail)
      td.Show()
    End Using

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object,
                                 e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Launch Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object,
                                e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

#End Region

End Class