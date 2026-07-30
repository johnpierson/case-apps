Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Public Class form_PlacePoint

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)

    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Form Setup
    Me.Title = "CASE Point Placements" & _s.AppVersion

    ' Update Levels
    _s.GetLevels()

    ' Load Family Symbols
    _s.GetSymbolNames(True)
    Me.ComboBoxFamilyName.DisplayMemberPath = "ElementFullName"
    For Each x As clsFamily In _s.FamilyTypeNames.Values
      Me.ComboBoxFamilyName.Items.Add(x)
    Next
    Me.ComboBoxFamilyName.SelectedIndex = 0

  End Sub

#Region "Form Controls & Events"

  ''' <summary>
  ''' Place by XYZ
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonPlaceXYZ_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonPlaceXYZ.Click

    ' Transaction
    Dim m_t As New Transaction(_s.Doc, "Place Point by XYZ")
    m_t.Start()

    Try

      ' Get the Symbol
      Dim m_symbol_i As clsFamily = Me.ComboBoxFamilyName.SelectedItem
      Dim m_symbol As FamilySymbol = TryCast(m_symbol_i.ElementObject, FamilySymbol)
      If m_symbol Is Nothing Then
        MsgBox("Error Getting Family Symbol for Placement", MsgBoxStyle.Critical, "Cannot Continue!")
        Exit Sub
      End If

      ' Validate the XYZ Data
      Dim m_x As Double = Me.TextBoxX.Text
      Dim m_y As Double = Me.TextBoxY.Text
      Dim m_z As Double = Me.TextBoxZ.Text

      ' Point Object
      Dim m_xyz As New Autodesk.Revit.DB.XYZ(m_x, m_y, m_z)

      ' Get Level 0
      Dim m_level As Level = Nothing
      For Each l As clsLevel In _s.Levels
        Try
          If l.LevelElement.Id.Value < m_level.Id.Value Then
            m_level = l.LevelElement
          End If
        Catch
          m_level = l.LevelElement
        End Try
      Next

      ' Place the Symbol
      Dim m_fi As FamilyInstance = Nothing
      m_fi = _s.Doc.Create.NewFamilyInstance(m_xyz, m_symbol, m_level, [Structure].StructuralType.NonStructural)

      ' Commit
      m_t.Commit()

    Catch ex As Exception

      ' Rollback
      m_t.RollBack()

      ' Report Failure
      MsgBox("Failed to Place Point:" & vbCr & ex.Message, MsgBoxStyle.Exclamation, "Oops")

    End Try

  End Sub

  ''' <summary>
  ''' Pick a Point
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonPickPoint_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonPickPoint.Click
    MsgBox("This Feature is Not Ready for Testing Yet", MsgBoxStyle.Information, "Sorry")
  End Sub

  ''' <summary>
  ''' All Elements of a Category
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBasepointByCategory_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonBasepointByCategory.Click

    ' Collect All Categories
    Dim m_cats As New List(Of String)
    For Each x As Category In _s.Categories.Values
      m_cats.Add(x.Name)
    Next

    ' Get User Selection
    Dim m_d As New form_ItemSelection(m_cats, "Select a Category")
    m_d.ShowDialog()

    ' Only Continue on Valid Selection
    If Not String.IsNullOrEmpty(m_d.SelectionName) Then

      ' Get the Symbol
      Dim m_symbol_i As clsFamily = Me.ComboBoxFamilyName.SelectedItem
      Dim m_symbol As FamilySymbol = TryCast(m_symbol_i.ElementObject, FamilySymbol)
      If m_symbol Is Nothing Then
        MsgBox("Error Getting Family Symbol for Placement", MsgBoxStyle.Critical, "Cannot Continue!")
        Exit Sub
      End If

      ' Get Level 0
      Dim m_level As Level = Nothing
      For Each l As clsLevel In _s.Levels
        Try
          If l.LevelElement.Id.Value < m_level.Id.Value Then
            m_level = l.LevelElement
          End If
        Catch
          m_level = l.LevelElement
        End Try
      Next

      ' Get all Instances for Category
      Dim m_col As New FilteredElementCollector(_s.Doc)
      Dim m_category As Category = _s.Doc.Settings.Categories.Item(m_d.SelectionName)
      m_col.OfCategory(m_category.Id.Value)
      m_col.WhereElementIsNotElementType()

      ' Place for Each Item
      For Each x As Element In m_col.ToElements

        ' Transactions
        Dim m_t As New Transaction(_s.Doc, "Place Point by Category")
        m_t.Start()

        Try

          ' Get the XYZ Location
          Dim m_location As Location = x.Location

          ' Continue if we have a location
          If Not m_location Is Nothing Then

            Try

              ' Get the XYZ object and Strings
              Dim m_XYZ = DirectCast(m_location, LocationPoint).Point
              If Not m_XYZ Is Nothing Then

                ' Place the Point Element
                Dim m_fi As FamilyInstance = Nothing
                m_fi = _s.Doc.Create.NewFamilyInstance(m_XYZ, m_symbol, m_level, [Structure].StructuralType.NonStructural)

              End If

            Catch
            End Try

          End If

          ' Commit
          m_t.Commit()

        Catch

          ' Rollback
          m_t.RollBack()

        End Try

      Next

    End If

  End Sub

  ''' <summary>
  ''' All Elements of a Symbol Type
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonBasepointBySymbol_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonBasepointBySymbol.Click

    ' Get the Symbol
    Dim m_symbol_i As clsFamily = TryCast(Me.ComboBoxFamilyName.SelectedItem, clsFamily)
    Dim m_symbol As FamilySymbol = TryCast(m_symbol_i.ElementObject, FamilySymbol)
    If m_symbol Is Nothing Then
      MsgBox("Error Getting Family Symbol for Placement", MsgBoxStyle.Critical, "Cannot Continue!")
      Exit Sub
    End If

    ' What Point to Place
    Dim m_fams As New List(Of clsFamily)
    For Each x As clsFamily In _s.FamilyTypeNames.Values
      m_fams.Add(x)
    Next
    Dim m_d As New form_ItemSelection(m_fams, "Select Family to Place Points AT")
    m_d.ShowDialog()

    ' Only Continue on Valid Selection
    If Not m_d.SelectionFamily Is Nothing Then

      ' Get Level 0
      Dim m_level As Level = Nothing
      For Each l As clsLevel In _s.Levels
        Try
          If l.LevelElement.Id.Value < m_level.Id.Value Then
            m_level = l.LevelElement
          End If
        Catch
          m_level = l.LevelElement
        End Try
      Next

      ' Get all Instances for Symbol
      Dim m_col As New FilteredElementCollector(_s.Doc)
      m_col.OfCategory(m_symbol.Category.Id.Value)
      m_col.WhereElementIsNotElementType()

      ' Place for Each Item
      For Each x As Element In m_col.ToElements

        ' Make sure Symbol Matches
        If m_d.SelectionFamily.ElementObject.Id = x.GetTypeId Then

          ' Transactions
          Dim m_t As New Transaction(_s.Doc, "Place Point by Symbol")
          m_t.Start()

          Try

            ' Get the XYZ Location
            Dim m_location As Location = x.Location

            ' Continue if we have a location
            If Not m_location Is Nothing Then

              Try

                ' Get the XYZ object and Strings
                Dim m_XYZ = DirectCast(m_location, LocationPoint).Point
                If Not m_XYZ Is Nothing Then

                  ' Place the Point Element
                  Dim m_fi As FamilyInstance = Nothing
                  m_fi = _s.Doc.Create.NewFamilyInstance(m_XYZ, m_symbol, m_level, [Structure].StructuralType.NonStructural)

                End If

              Catch
              End Try


            End If

            ' Commit
            m_t.Commit()

          Catch

            ' Rollback
            m_t.RollBack()

          End Try

        End If

      Next

    End If

  End Sub

  ''' <summary>
  ''' Close
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ButtonCancel.Click
    Me.Close()
  End Sub

  Private Sub TextBoxX_PreviewTextInput(sender As Object, e As System.Windows.Input.TextCompositionEventArgs) Handles TextBoxX.PreviewTextInput
    If Char.IsDigit(e.Text) Or e.Text = "." Then
    Else
      e.Handled = True
      Exit Sub
    End If
  End Sub

  Private Sub TextBoxY_PreviewTextInput(sender As Object, e As System.Windows.Input.TextCompositionEventArgs) Handles TextBoxY.PreviewTextInput
    If Char.IsDigit(e.Text) Or e.Text = "." Then
    Else
      e.Handled = True
      Exit Sub
    End If
  End Sub

  Private Sub TextBoxZ_PreviewTextInput(sender As Object, e As System.Windows.Input.TextCompositionEventArgs) Handles TextBoxZ.PreviewTextInput
    If Char.IsDigit(e.Text) Or e.Text = "." Then
    Else
      e.Handled = True
      Exit Sub
    End If
  End Sub

#End Region

End Class