Imports Autodesk.Revit.DB

Public Class form_ParameterConfigurationManager

  Private _s As clsSettings
  Private _changes As Boolean = False
  Private _previousCategory As String = ""

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_s">Settings Class</param>
  ''' <remarks></remarks>
  Public Sub New(p_s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = p_s
    LoadData()

  End Sub

  ''' <summary>
  ''' Load the Data
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadData()

    ' Load all Categories
    Me.ComboBoxCategory.Items.Clear()
    For Each x As clsParamMap In _s.ConfigFile.DictParamMaps.Values
      Me.ComboBoxCategory.Items.Add(x.CategoryName)
    Next

    ' Finish if Empty
    If Me.ComboBoxCategory.Items.Count < 1 Then Exit Sub

    ' Set the Default Item
    Me.ComboBoxCategory.SelectedIndex = 0

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Change Category Selection
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ComboBoxCategory_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBoxCategory.SelectionChanged

    Try

      ' Update Form
      Dim m_map As clsParamMap = Nothing
      If _s.ConfigFile.DictParamMaps.TryGetValue(Me.ComboBoxCategory.SelectedItem.ToString, m_map) Then
        Try
          Me.TextBoxParamP.Text = m_map.Param_P
        Catch
        End Try
        Try
          Me.TextBoxParamN.Text = m_map.Param_N
        Catch
        End Try
        Try
          Me.TextBoxParamE.Text = m_map.Param_E
        Catch
        End Try
        Try
          Me.TextBoxParamZ.Text = m_map.Param_Z
        Catch
        End Try
        Try
          Me.TextBoxParamD.Text = m_map.Param_D
        Catch
        End Try
        Try
          Me.TextBoxParam1.Text = m_map.Param_1
        Catch
        End Try
        Try
          Me.TextBoxParam2.Text = m_map.Param_2
        Catch
        End Try
        Try
          Me.TextBoxParam3.Text = m_map.Param_3
        Catch
        End Try
        Try
          Me.TextBoxParam4.Text = m_map.Param_4
        Catch
        End Try
        Try
          Me.TextBoxParam5.Text = m_map.Param_5
        Catch
        End Try
      End If

    Catch
    End Try

    ' Reset Flag
    _changes = False

  End Sub

  ''' <summary>
  ''' Save Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSave_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonSave.Click

    Try

      ' Try to get the Value
      Dim m_map As clsParamMap = Nothing
      If _s.ConfigFile.DictParamMaps.TryGetValue(Me.ComboBoxCategory.SelectedItem.ToString, m_map) Then

        ' Update all Items Selected higher than 0
        If Not String.IsNullOrEmpty(Me.TextBoxParamP.Text) Then
          m_map.Param_P = Me.TextBoxParamP.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParamN.Text) Then
          m_map.Param_N = Me.TextBoxParamN.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParamE.Text) Then
          m_map.Param_E = Me.TextBoxParamE.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParamZ.Text) Then
          m_map.Param_Z = Me.TextBoxParamZ.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParamD.Text) Then
          m_map.Param_D = Me.TextBoxParamD.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParam1.Text) Then
          m_map.Param_1 = Me.TextBoxParam1.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParam2.Text) Then
          m_map.Param_2 = Me.TextBoxParam2.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParam3.Text) Then
          m_map.Param_3 = Me.TextBoxParam3.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParam4.Text) Then
          m_map.Param_4 = Me.TextBoxParam4.Text
        End If
        If Not String.IsNullOrEmpty(Me.TextBoxParam5.Text) Then
          m_map.Param_5 = Me.TextBoxParam5.Text
        End If

        Try
          ' Replace the Master Object
          _s.ConfigFile.DictParamMaps.Item(Me.ComboBoxCategory.SelectedItem.ToString) = m_map
        Catch
        End Try

      End If

      ' Write the Updated Configuration File
      _s.ConfigFile.WriteFile()

    Catch
    End Try

    ' Reset Change Flag
    _changes = False

  End Sub

  ''' <summary>
  ''' Delete a Category
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonDeleteCategory_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonDeleteCategory.Click

    ' The Category Name
    Dim m_catName As String = ""
    Try
      m_catName = Me.ComboBoxCategory.SelectedItem.ToString
    Catch

    End Try

    ' Need a Value to Continue
    If String.IsNullOrEmpty(m_catName) Then Exit Sub

    ' Verify
    If MsgBox("Are you sure you want to delete data for the '" & m_catName & "' category?", MsgBoxStyle.YesNo, "Are You Sure?") Then

      Try
        _s.ConfigFile.DictParamMaps.Remove(m_catName)
      Catch
      End Try

      ' Write the Updated File
      _s.ConfigFile.WriteFile()

    End If

    ' Load the Updates
    LoadData()

  End Sub

  ''' <summary>
  ''' Add a New Category
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonAddNewCategory_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonAddNewCategory.Click

    ' Available Categories
    Dim m_items As New List(Of String)

    ' Available Categories
    _s.ScanCategories()
    For Each x As Category In _s.Categories.Values
      m_items.Add(x.Name)
    Next

    ' Construct and Display the Form
    Dim m_d As New form_ItemSelection(m_items)
    m_d.ShowDialog()
    If Not String.IsNullOrEmpty(m_d.SelectionName) Then

      ' Write a New Item to the Configuration File
      _s.ConfigFile.AddNewCategoryItem(m_d.SelectionName)

    End If

    ' Update Category Drop-Down
    LoadData()

  End Sub

  ''' <summary>
  ''' Close the Form
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonClose_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonClose.Click

    ' Save Unsaved Changes?
    If _changes = True Then
      If MsgBox("Do you want to save your unsaved changes?", MsgBoxStyle.YesNo, "Wait!") = MsgBoxResult.Yes Then
        ButtonSave_Click(Nothing, Nothing)
      End If
    End If

    ' Close
    Me.Close()

  End Sub

  Private Sub TextBoxParamP_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParamP.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParamN_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParamN.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParamE_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParamE.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParamZ_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParamZ.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParamD_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParamD.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParam1_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParam1.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParam2_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParam2.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParam3_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParam3.TextChanged
    _changes = True
  End Sub

  Private Sub TextBoxParam4_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParam4.TextChanged
    _changes = True
  End Sub

  Private Sub TextBox5_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TextBoxParam5.TextChanged
    _changes = True
  End Sub

#End Region

End Class