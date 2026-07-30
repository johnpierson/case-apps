Imports System.Collections.Generic
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Autodesk.Revit
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System
Imports System.Linq
Imports System.Reflection
Imports [Case].ExtrudeRoomsToMass.API

Public Class form_RoomMasses

  Private _cmd As ExternalCommandData

  Private _doc As Document
  Private _famDoc As Document
  Private _app As ApplicationServices.Application
  Private _appCreate As Creation.Application
  Private _sw As New List(Of Stopwatch)
  Private _SECategory As Category
  Private _famFactory As Creation.FamilyItemFactory
  Private _rooms As New List(Of Architecture.Room)
  Private _uiApp As UIApplication
  Private _path As String = ""
  Private _genericModelPath As String = ""

  ''' <summary>
  ''' General Constructor
  ''' </summary>
  ''' <param name="c"></param>
  ''' <remarks></remarks>
  Public Sub New(c As ExternalCommandData)
	 InitializeComponent()

	 ' Widen Scope
	 _cmd = c

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Load Matching Room Data into Data View
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub UpdateRoomsList()

	 ' Clean Grid
    DataGridViewRooms.Rows.Clear()

    ' Fresh List
    _rooms = New List(Of Architecture.Room)

    Try

      ' Query
      Dim m_rooms As IEnumerable(Of Architecture.Room) = From e In New FilteredElementCollector(_doc) _
                                                         .OfCategory(BuiltInCategory.OST_Rooms)
                                                         Let r = TryCast(e, Architecture.Room)
                                                         Where r.Area > 1
                                                         Select r

      ' Process Results
      If Not m_rooms Is Nothing Then
        For Each x As Architecture.Room In m_rooms

          ' Add to the List
          _rooms.Add(x)

          ' Build and Add the Row
          Dim m_row As New DataGridViewRow
          Dim m_cell As New DataGridViewTextBoxCell

          ' UniqueID
          m_cell.Value = x.UniqueId.ToString
          m_row.Cells.Add(m_cell)

          ' Number
          m_cell = New DataGridViewTextBoxCell
          m_cell.Value = x.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
          m_row.Cells.Add(m_cell)

          ' Name
          m_cell = New DataGridViewTextBoxCell
          m_cell.Value = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
          m_row.Cells.Add(m_cell)

          ' Department
          m_cell = New DataGridViewTextBoxCell
          m_cell.Value = x.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString
          m_row.Cells.Add(m_cell)

          ' Level
          m_cell = New DataGridViewTextBoxCell
          m_cell.Value = x.Document.GetElement(x.LevelId).Name
          m_row.Cells.Add(m_cell)

          ' Add the row
          DataGridViewRooms.Rows.Add(m_row)

        Next
      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Create line element
  ''' </summary>
  ''' <param name="app">revit application</param>
  ''' <returns></returns>
  Private Function MakeLine(app As UIApplication, pLine As Line) As ModelCurve

    Try

      ' End Points
      Dim ptA As XYZ = pLine.GetEndPoint(0)
      Dim ptB As XYZ = pLine.GetEndPoint(1)

      ' Boundaries
      Dim m_line As Line = Line.CreateBound(ptA, ptB)
      Dim m_norm As XYZ = ptA.CrossProduct(ptB)
      If m_norm.GetLength() = 0 Then
        m_norm = XYZ.BasisZ
      End If

      ' Plane & Sketch Plane
      Dim m_plane As Plane = Plane.CreateByNormalAndOrigin(m_norm, ptB)

      Dim m_skplane As SketchPlane = SketchPlane.Create(_doc, m_plane)

      ' Create line here
      Dim m_modelcurve As ModelCurve = _doc.FamilyCreate.NewModelCurve(m_line, m_skplane)

      ' Result
      Return m_modelcurve

    Catch
    End Try

    ' Failure
    Return Nothing

  End Function

  ''' <summary>
  ''' Create arc element by three points
  ''' </summary>
  ''' <param name="app">revit application</param>
  ''' <returns></returns>
  Private Function MakeArc(app As UIApplication, pArc As Arc) As ModelCurve

    Try

      ' Points
      Dim ptA As XYZ = pArc.GetEndPoint(0)
      Dim ptB As XYZ = pArc.GetEndPoint(1)
      Dim ptC As XYZ = pArc.Center

      ' Create three lines and a plane by the points
      Dim line1 As Line = Line.CreateBound(ptA, ptB)
      Dim line2 As Line = Line.CreateBound(ptB, ptC)
      Dim line3 As Line = Line.CreateBound(ptC, ptA)
      Dim ca As New CurveArray()
      ca.Append(line1)
      ca.Append(line2)
      ca.Append(line3)

      ' Plane and Sketch Plane
      Dim m_plane As Plane = Plane.CreateByThreePoints(ptA, ptB, ptC)
      Dim m_skplane As SketchPlane = SketchPlane.Create(_doc, m_plane)

      ' Create arc here
      Dim m_mc As ModelCurve = _doc.FamilyCreate.NewModelCurve(pArc, m_skplane)

      ' Result
      Return m_mc

    Catch
    End Try

    ' Failure
    Return Nothing

  End Function

  ''' <summary>
  ''' Generate masses for rooms
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GenerateMasses()

    Dim iCnt As Integer = 0
    Dim iCntFail As Integer = 0

    ' Prime the Progressbar
    With ProgressBar1
      .Minimum = 0
      .Maximum = _rooms.Count
      .Value = 0
      .Show()
    End With

    ' Random Object
    Dim m_r As New Random

    ' Iterate the Rooms
    For Each x As Architecture.Room In _rooms

      Try
        ProgressBar1.Increment(1)
      Catch
      End Try

      Try

        ' Create a new Family
        _famDoc = _app.NewFamilyDocument(_genericModelPath)

        ' Start a new Family Transaction
        Using t As New Transaction(_famDoc, "Transaction in Family Document")
          If t.Start() = TransactionStatus.Started Then

            Try

              ' Get the department name
              Dim m_SubCatName As String = "Mass_Rooms"
              If Not String.IsNullOrEmpty(x.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString) Then
                m_SubCatName = "Mass_Rooms_" & x.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString
              End If

              ' Subcategory named by Department
              Dim m_Subcategory As Category = Nothing

              Try

                ' Create the subcategory if it does not exist
                m_Subcategory = _famDoc.Settings.Categories.NewSubcategory(_SECategory, m_SubCatName)

              Catch

                ' Get the subcategory object since it exists already
                Dim m_NameMap As CategoryNameMap = _SECategory.SubCategories
                For Each x1 As Category In m_NameMap
                  If x1.Name = m_SubCatName Then
                    m_Subcategory = x1
                    Exit For
                  End If
                Next

              End Try

              ' Material named by Department
              Dim m_Material As Material = Nothing
              Dim m_colMat As New FilteredElementCollector(_famDoc)
              m_colMat.OfCategory(BuiltInCategory.OST_Materials)

              ' Find the Material by Name
              For Each m As Material In m_colMat.ToElements
                If m.Name = m_SubCatName Then
                  m_Material = m
                  Exit For
                End If
              Next

              ' Create the Material if Not Found
              If m_Material Is Nothing Then

                Try

                  ' Name as Subcategory
                  Dim m_matid As ElementId = Material.Create(_famDoc, m_SubCatName)
                  m_Material = TryCast(_famDoc.GetElement(m_matid), Material)

                  ' Random Color
                  Dim m_color As New Color(m_r.Next(1, 255), m_r.Next(1, 255), m_r.Next(1, 255))

                  ' Apply the Color to the Material
                  m_Material.Color = m_color

                  ' Set 50% Transparency
                  m_Material.Transparency = 50

                  ' Assign the Material to the Subcategory
                  m_Subcategory.Material = m_Material

                Catch

                End Try

              End If

              ' Apply the material to the subcategory
              m_Subcategory.Material = m_Material

              ' What are the upp bounds or height of the room?
              Dim m_RmHeight As Double = x.UnboundedHeight
              Dim m_RmElev As Double = x.Level.Elevation
              'Dim m_ent_i As Integer = m_RmElev
              If m_RmHeight < 1 Then m_RmHeight = 8
              Dim m_RmUpLevel As Level = x.UpperLimit

              ' Get the room boundary data
              Dim m_Boundary As IList(Of IList(Of BoundarySegment)) = x.GetBoundarySegments(New SpatialElementBoundaryOptions)

              ' Make Sure we Have Something
              If m_Boundary Is Nothing Then Continue For

              ' Flat Sketchplane
              Dim m_plane As Plane = Plane.CreateByNormalAndOrigin(New XYZ(0, 0, 1), New XYZ(0, 0, m_RmElev))
              Dim m_skplane As SketchPlane = SketchPlane.Create(_famDoc, m_plane)

              ' The Reference Array
              Dim m_refArray As New ReferenceArray

              ' Iterate to gather the reference array objects
              For i = 0 To m_Boundary.Count - 1
                Dim il As IList(Of BoundarySegment) = m_Boundary(i)
                ' Segments Array
                For ii = 0 To il.Count - 1

                  Dim m_Seg As BoundarySegment = il.Item(ii)

                  Dim m_mc As ModelCurve = Nothing

                  ' Curve is Line
                  If TypeOf m_Seg.GetCurve() Is Line Then
                    Dim m_cLine As Line = TryCast(m_Seg.GetCurve(), Line)
                    m_mc = _famDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                  End If

                  ' Curve is Arc
                  If TypeOf m_Seg.GetCurve() Is Arc Then
                    Dim m_cLine As Arc = TryCast(m_Seg.GetCurve(), Arc)
                    m_mc = _famDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                  End If

                  ' Curve is NurbSpline
                  If TypeOf m_Seg.GetCurve() Is NurbSpline Then
                    Dim m_cLine As NurbSpline = TryCast(m_Seg.GetCurve(), NurbSpline)
                    m_mc = _famDoc.FamilyCreate.NewModelCurve(m_cLine, m_skplane)
                  End If

                  Try

                    m_refArray.Append(m_mc.GeometryCurve.Reference)
                  Catch
                  End Try

                Next
              Next

              ' The extrusion form direction
              Dim m_dir As New XYZ(0, 0, m_RmHeight)
              ' Extrude the form
              Dim m_form As Autodesk.Revit.DB.Form = _famDoc.FamilyCreate.NewExtrusionForm(True, m_refArray, m_dir)
              Try
                m_form.Subcategory = m_Subcategory
              Catch
              End Try

              ' Commit the Family Transaction
              t.Commit()

            Catch

              ' Cannot Continue to Place Mass Family
              Continue For

            End Try

          End If
        End Using

        ' Save the Family
        Dim m_opt As New SaveAsOptions
        m_opt.OverwriteExistingFile = True
        _famDoc.SaveAs(_path & x.UniqueId.ToString & ".rfa", m_opt)
        _famDoc.Close()

        ' Has it Been Loaded?
        Dim m_family As Family = Nothing
        Dim m_famCol As New FilteredElementCollector(_doc)

        ' Start a new Model Transaction
        Using t As New Transaction(_doc, "Extrude Rooms to Masses")
          If t.Start() = TransactionStatus.Started Then

            Try

              ' Load the Family
              _doc.LoadFamily(_path & x.UniqueId.ToString & ".rfa", m_family)

              ' Get the Default Symbol from the Family
              For Each fs As ElementId In m_family.GetFamilySymbolIds()

                ' The Symbol
                Dim m_famSymbol As FamilySymbol = TryCast(_doc.GetElement(fs), FamilySymbol)

                ' Place the Family at 0,0,0 
                _doc.Create.NewFamilyInstance(New XYZ(0, 0, 0), m_famSymbol, [Structure].StructuralType.NonStructural)

              Next

              ' Commit the Model Transaction
              iCnt += 1
              t.Commit()

            Catch
            End Try

          End If
        End Using

      Catch
      End Try

    Next

    ' Results
    Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Happened")
      With td
        .TitleAutoPrefix = False
        .MainInstruction = "Masses from Rooms"
        .MainContent = "Make sure that the 3D view that you are trying to view the results in has the mass category enabled in the view settings." & vbCr & vbCr
        .MainContent += "Create " & iCnt.ToString & " Masses from " & _rooms.Count.ToString & " Rooms"
        .Show()
      End With
    End Using


  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Form Setup
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub form_RoomMasses_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    Try

      ' Title
      Text = "Extrude Rooms as Masses v" & Assembly.GetExecutingAssembly.GetName.Version.ToString

      ' Hide the progressbar
      ProgressBar1.Hide()

      ' Find the Generic Model
      _genericModelPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\Mass.rft"
      If Not System.IO.File.Exists(_genericModelPath) Then

        ' Failure Message
        MsgBox("Mass.rft was not Found!", MsgBoxStyle.Critical, "Cannot Continue")

      Else

        ' Application and Document References
        _uiApp = _cmd.Application
        _app = _uiApp.Application
        _doc = _uiApp.ActiveUIDocument.Document

        ' Model Category
        _SECategory = _doc.Settings.Categories.Item(BuiltInCategory.OST_Mass)

        ' App creator
        _appCreate = _app.Create

        ' Rooms
        UpdateRoomsList()

      End If

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Try
      Process.Start("http://apps.case-inc.com/content/free-extrude-rooms-3d-mass")
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Start building the mass families
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonGenerateMasses_Click(ByVal sender As System.Object,
                                         ByVal e As EventArgs) Handles ButtonGenerateMasses.Click

    ' Hide Buttons
    ButtonCancel.Hide()
    ButtonGenerateMasses.Hide()
    ButtonHelp.Hide()

    ' Save Location
    If FolderBrowserDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then

      ' Was a Path Returned
      If Not String.IsNullOrEmpty(FolderBrowserDialog1.SelectedPath) Then

        ' Save the Path
        _path = FolderBrowserDialog1.SelectedPath & "\"

        ' Generate the Extrusions
        GenerateMasses()

      End If
    End If

    ' Close the Form when Done
    Close()

  End Sub

  ''' <summary>
  ''' Close and Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(ByVal sender As System.Object,
                                 ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Logo
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Try
      Process.Start("http://apps.case-inc.com/content/free-extrude-rooms-3d-mass")
    Catch
    End Try
  End Sub

#End Region

End Class