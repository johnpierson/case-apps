Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Diagnostics
Imports System.Collections.Generic
Imports RMA.OpenNURBS
Imports System.Reflection

Public Class form_ImportMain

  ' Add-In Settings
  Private _s As clsSettings

  ' 3DM File Path
  Private _openNurbsFilePath As String = ""

  ' Spline and Surface Precision Parameters
  Private _precisionSplines As Integer = 1
  Private _precisionSurfaces As Integer = 1


  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="Settings"></param>
  ''' <remarks></remarks>
  Public Sub New(ByVal Settings As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = Settings

    ' Set Enabled Imports
    ImportChecks()

    ' Form Set-Up
    ProgressBar_Import.Visibility = Visibility.Hidden
    Me.ION_Window.Title = "Import OpenNURBS (Rhino *.3DM) v" & Assembly.GetExecutingAssembly.GetName.Version.ToString

  End Sub

  ''' <summary>
  ''' Import Geometry on Click
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub Button_Import_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button_Import.Click

    'Hide buttons
    Me.Button_Cancel.Visibility = Visibility.Hidden
    Me.Button_Import.Visibility = Visibility.Hidden

    'Show Progressbar
    Me.ProgressBar_Import.Visibility = Visibility.Visible

    'Import Properties to Settings
    ImportProperties()

    ' Start a New transaction
    Using t As New Transaction(_s.ActiveDoc, "Create openNURBS Curve Objects")
      If t.Start Then

        'Get OpenNURBS Model
        If System.IO.File.Exists(_openNurbsFilePath) Then
          Dim m_rh_model As OnXModel
          Dim m_nurbs As New clsONGet(_openNurbsFilePath)
          m_rh_model = m_nurbs.GetModel()

          'Model tolerance
          _s.ModelTolerance = m_rh_model.m_settings.m_ModelUnitsAndTolerances.m_absolute_tolerance

          'Sort Objects in Rhino Model
          Dim m_obj As New clsONSort(m_rh_model, _s)

          Dim objCount As Integer = m_obj.ObjectCount

          'Progress Bar parameters
          With Me.ProgressBar_Import
            .Maximum = objCount
            .Minimum = 0
            .Value = 0
          End With

          Dim m_rvtCrvUtls As New RVTCurveUtils(_s)
          Dim m_rvtFamUtls As New RVTFamilyMassUtils(_s)

          'CREATE some LINES...
          Dim ln As IOnLineCurve
          For Each ln In m_obj.lines
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            Dim rvt_line As ModelCurve = m_rvtCrvUtls.RVTLine(ln)
          Next

          'CREATE some ARCS...
          Dim arc As IOnArcCurve
          For Each arc In m_obj.arcs
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            Dim rvt_arc As ModelCurve = m_rvtCrvUtls.RVTArc(arc)
          Next

          'CREATE some POLYLINES...
          Dim pline As IOnPolylineCurve
          For Each pline In m_obj.plines
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            Dim rvt_pline As List(Of ModelCurve) = m_rvtCrvUtls.RVTPline(pline)
          Next

          'CREATE some CIRCLES...
          Dim circle As IOnArcCurve
          For Each circle In m_obj.circs
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            Dim rvt_circle As ModelCurve = m_rvtCrvUtls.RVTCircle(circle)
          Next

          'CREATE some ELLIPSES
          Dim ellipse As IOnNurbsCurve
          For Each ellipse In m_obj.ellipses
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            Dim rvt_Ellipse As ModelCurve = m_rvtCrvUtls.RVTEllipse(ellipse)
          Next


          'CREATE some 2D NURBS Curves...
          Dim nrb2d As IOnNurbsCurve
          For Each nrb2d In m_obj.nurbscrv2d
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            'Make a Planar Hermite Spline
            Dim rvt_nrbspline As ModelCurve = m_rvtCrvUtls.RVTNurbsCrv(nrb2d)

          Next

          'Create some Closed 3D Spline Curves
          Dim spline2d As IOnNurbsCurve
          For Each spline2d In m_obj.closedspline2d
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            'Make a Planar Hermite Spline
            Dim rvt_nrbspline As List(Of ModelHermiteSpline) = m_rvtCrvUtls.RVTHermSplineCrv(spline2d)
          Next

          'Create some 3D NURBS Curves...
          'If this is a Mass Family document, use CurveByPoints ELSE make a segmented curve
          Dim nrb3d As IOnNurbsCurve
          For Each nrb3d In m_obj.splinecrv3d
            ' Increment Progress
            ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

            If _s.ActiveDoc.IsFamilyDocument Then
              If _s.ActiveDoc.OwnerFamily.FamilyCategory.Name.Equals("Mass") Then
                'Make a hermite spline using CurveByPoints
                Dim rvt_nrbs As CurveByPoints = m_rvtCrvUtls.RVTCurveByPoints(nrb3d)
              Else
                'Make a segmented curve using available Greville control points
                Dim rvt_nrbs As List(Of ModelCurve) = m_rvtCrvUtls.RVTSegNurbsCurve(nrb3d)
              End If
            End If

          Next

          'Check if the document is a MASS Family before making reference points and surfaces.
          If _s.ActiveDoc.IsFamilyDocument Then
            If _s.ActiveDoc.OwnerFamily.FamilyCategory.Name.Equals("Mass") Then

              'CREATE some POINTS
              Dim pt As IOn3dPoint
              For Each pt In m_obj.points
                ' Increment Progress
                ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

                Dim rvt_refpt As ReferencePoint = m_rvtFamUtls.RVTPoint(pt)
              Next

              'CREATE some NURBS Surfaces...
              Dim srf As IOnBrep
              For Each srf In m_obj.surfaces
                ' Increment Progress
                ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

                Dim brpFace As OnBrepFace = srf.Face(0)
                Dim nrbssrf As OnNurbsSurface = brpFace.NurbsSurface()
                Dim UVSrf As Form = m_rvtFamUtls.RVTPtSurface(nrbssrf)
              Next

              'CREATE some Corner NURBS Surfaces...
              Dim csrf As IOnBrep
              For Each csrf In m_obj.cornersurfaces
                ' Increment Progress
                ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

                Dim brpFace As OnBrepFace = csrf.Face(0)
                Dim nrbssrf As OnNurbsSurface = brpFace.NurbsSurface()
                Dim CornerSrf As Form = m_rvtFamUtls.RVTCornerPtSurface(nrbssrf)
              Next

              'CREATE some Trimmed Planar Surfaces
              Dim trimsrf As IOnBrep
              For Each trimsrf In m_obj.trimsurfaces
                ' Increment Progress
                ProgressBar_Import.Dispatcher.Invoke(New CrossAppDomainDelegate(AddressOf IncrementProgress), System.Windows.Threading.DispatcherPriority.Background, Nothing)

                Dim PlanarSurface As Form = m_rvtFamUtls.RVTTrimSurface(trimsrf)
              Next

            End If
          End If

        Else
          'If the file does not exist, message!
          MsgBox("You must first select a valid Rhino 3DM File.  Please provide a valid file path.", MsgBoxStyle.Exclamation, "File Not Valid")
        End If

        t.Commit()

      End If
    End Using

    ' Close
    Me.Close()

  End Sub

  ''' <summary>
  ''' Sets the Import Model Properties
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub ImportProperties()

    'Units

    If Me.ComboBox_Units.SelectedIndex = 0 Then
      _s.ModelScale = 1

    ElseIf Me.ComboBox_Units.SelectedIndex = 1 Then
      _s.ModelScale = 1 / 12

    ElseIf Me.ComboBox_Units.SelectedIndex = 2 Then
      _s.ModelScale = 1 / 0.3048

    ElseIf Me.ComboBox_Units.SelectedIndex = 3 Then
      _s.ModelScale = 1 / 304.8

    End If


    'Spline Precision
    If Me.RadioButton_SplineX1.IsChecked = True Then
      _s.PrecisionSpline = 1
    ElseIf Me.RadioButton_SplineX2.IsChecked = True Then
      _s.PrecisionSpline = 2
    ElseIf Me.RadioButton_SplineX3.IsChecked = True Then
      _s.PrecisionSpline = 3
    ElseIf Me.RadioButton_SplineX4.IsChecked = True Then
      _s.PrecisionSpline = 4
    End If

    'Spline Precision
    If Me.RadioButton_SurfacesX1.IsChecked = True Then
      _s.PrecisionSurface = 1
    ElseIf Me.RadioButton_SurfacesX2.IsChecked = True Then
      _s.PrecisionSurface = 2
    ElseIf Me.RadioButton_SurfacesX3.IsChecked = True Then
      _s.PrecisionSurface = 3
    ElseIf Me.RadioButton_SurfacesX4.IsChecked = True Then
      _s.PrecisionSurface = 4
    End If

    'CheckBoxes
    _s.ImportPoints = Me.Checkbox_Points.IsChecked
    _s.ImportLines = Me.CheckBox_Lines.IsChecked
    _s.ImportArcs = Me.CheckBox_Lines.IsChecked
    _s.ImportCircles = Me.CheckBox_Circles.IsChecked
    _s.ImportPolyLines = Me.CheckBox_PolyLines.IsChecked
    _s.ImportPolyCurves = Me.CheckBox_Polycurve.IsChecked
    _s.ImportNURBSCurves2D = Me.CheckBox_NurbsCrv.IsChecked
    _s.ImportClosedSplines2D = Me.CheckBox_ClosedSpline2D.IsChecked
    _s.ImportSplines3D = Me.CheckBox_Spline3D.IsChecked
    _s.ImportSurfaces = Me.CheckBox_Surfaces.IsChecked
    _s.ImportCornerSrf = Me.CheckBox_CornerSurfaces.IsChecked
    _s.ImportTrimSrf = Me.CheckBox_TrimmedSurfaces.IsChecked
    _s.ImportPolySrf = Me.CheckBox_PolySrf.IsChecked

  End Sub


  ''' <summary>
  ''' Checks that relevant Checkboxes are enabled/disabled per the document type
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub ImportChecks()

    If _s.ActiveDoc.IsFamilyDocument Then
      If _s.ActiveDoc.OwnerFamily.FamilyCategory.Name.Equals("Mass") Then

        ' Enable Mass Family Imports
        Me.Checkbox_Points.IsEnabled = True
        Me.CheckBox_Spline3D.IsEnabled = True
        Me.CheckBox_Surfaces.IsEnabled = True
        Me.CheckBox_TrimmedSurfaces.IsEnabled = True
        Me.CheckBox_CornerSurfaces.IsEnabled = True

      Else

        ' Disable checkboxes that don't apply to the environment
        Me.Checkbox_Points.IsChecked = False
        Me.Checkbox_Points.IsEnabled = False

        Me.CheckBox_Spline3D.IsChecked = False
        Me.CheckBox_Spline3D.IsEnabled = False

        Me.CheckBox_Surfaces.IsChecked = False
        Me.CheckBox_Surfaces.IsEnabled = False

        Me.CheckBox_CornerSurfaces.IsChecked = False
        Me.CheckBox_TrimmedSurfaces.IsEnabled = False

        Me.CheckBox_TrimmedSurfaces.IsChecked = False
        Me.CheckBox_CornerSurfaces.IsEnabled = False

        Me.CheckBox_PolySrf.IsChecked = False
        Me.CheckBox_PolySrf.IsEnabled = False

      End If
    Else

      ' Disable checkboxes that don't apply to the environment
      Me.Checkbox_Points.IsChecked = False
      Me.Checkbox_Points.IsEnabled = False

      Me.CheckBox_Spline3D.IsChecked = False
      Me.CheckBox_Spline3D.IsEnabled = False

      Me.CheckBox_Surfaces.IsChecked = False
      Me.CheckBox_Surfaces.IsEnabled = False

      Me.CheckBox_CornerSurfaces.IsChecked = False
      Me.CheckBox_TrimmedSurfaces.IsEnabled = False

      Me.CheckBox_TrimmedSurfaces.IsChecked = False
      Me.CheckBox_CornerSurfaces.IsEnabled = False

      Me.CheckBox_PolySrf.IsChecked = False
      Me.CheckBox_PolySrf.IsEnabled = False
    End If

  End Sub

  ''' <summary>
  ''' Increments the Import progress bar
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub IncrementProgress()
    ProgressBar_Import.Value += 1
  End Sub


  Private Sub Button_FileDialogue_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button_FileDialogue.Click
    'Configure SaveFileDialogue
    Dim OpenFileDialog As New Microsoft.Win32.OpenFileDialog()
    OpenFileDialog.FileName = "Document" 'Default file name
    OpenFileDialog.DefaultExt = ".3dm" 'Default file extension
    OpenFileDialog.Filter = "OpenNURBS documents (Rhino 4.0)|*.3dm" 'Filter file

    'Show save file dialog box
    Dim result As Boolean = OpenFileDialog.ShowDialog()
    If result Then

      Me.TextBox_File.Text = OpenFileDialog.FileName
      _openNurbsFilePath = Me.TextBox_File.Text

    End If
  End Sub

  ''' <summary>
  ''' Cancel the Add-In
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button_Cancel.Click
    Me.Close()
  End Sub

  ' Set path to current text
  Private Sub TextBox_File_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles TextBox_File.TextChanged
    _openNurbsFilePath = Me.TextBox_File.Text
  End Sub

  ''' <summary>
  ''' Help
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub Button_Help_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles Button_Help.Click
    Process.Start("http://apps.case-inc.com/content/subscription-import-opennurbs-3dm-file-data")
  End Sub

End Class