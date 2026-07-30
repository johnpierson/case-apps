Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Imports System.Collections.Generic
Imports RMA.OpenNURBS

''' <summary>
''' MODEL CURVE TRANSLATE CLASS
''' </summary>
''' <remarks></remarks>
Public Class RVTBrepEdgeUtils

  Private _s As clsSettings

  Private _doc As Document
  Private _app As Autodesk.Revit.UI.UIApplication

  Public Sub New(ByVal settings As clsSettings)

    'Widen Scope
    _s = settings


    _doc = _s.ActiveDoc
    _app = _s.UIApplication

  End Sub

  ''' <summary>
  ''' Converts a linear NURBS curve to a line ModelCurve
  ''' </summary>
  ''' <param name="rh_linecurve">Linear NURBS Curve</param>
  ''' <param name="skplane">Sketch Plane</param>
  ''' <returns>ModelCurve</returns>
  ''' <remarks></remarks>
  Public Function RVTLineCurve(ByVal rh_linecurve As IOnNurbsCurve, ByVal skplane As SketchPlane) As ModelCurve

    Dim rh_line As IOnLineCurve = New OnLineCurve(rh_linecurve.PointAtStart, rh_linecurve.PointAtEnd)

    Dim rh_startPt As IOn3dPoint = rh_line.PointAtStart
    Dim rh_endPt As IOn3dPoint = rh_line.PointAtEnd

    'Find third point for sketch plane
    Dim rh_lnlen As Double = rh_line.m_line.Length
    Dim rh_origin As On3dPoint = rh_line.PointAt(0)
    Dim rh_tan As On3dVector = rh_line.TangentAt(0)

    'Defining the RMA plane
    Dim rh_pln As New OnPlane(rh_origin, rh_tan)

    'Establish perpendicular vector
    Dim rh_perp As On3dVector = rh_pln.xaxis
    Dim rh_transvec As New On3dVector(rh_perp.x * rh_lnlen, rh_perp.y * rh_lnlen, rh_perp.z * rh_lnlen)
    rh_pln.Translate(rh_transvec)

    'Third plane point
    Dim rh_thirdpt As On3dPoint = rh_pln.origin

    'start point values
    Dim x1 As Double = rh_startPt.x
    Dim y1 As Double = rh_startPt.y
    Dim z1 As Double = rh_startPt.z

    'end point values
    Dim x2 As Double = rh_endPt.x
    Dim y2 As Double = rh_endPt.y
    Dim z2 As Double = rh_endPt.z

    'end point values
    Dim x3 As Double = rh_thirdpt.x
    Dim y3 As Double = rh_thirdpt.y
    Dim z3 As Double = rh_thirdpt.z

    'create Revit XYZ vectors
    Dim startXYZ As New XYZ(x1, y1, z1)
    Dim endXYZ As New XYZ(x2, y2, z2)
    Dim thirdXYZ As New XYZ(x3, y3, z3)

    'define sketch plane
    Dim worldOrigin As XYZ = XYZ.Zero
    Dim plnLine1 As Line = Line.CreateBound(startXYZ, endXYZ)
    Dim plnLine2 As Line = Line.CreateBound(startXYZ, thirdXYZ)

    Dim mCurve As ModelCurve

      mCurve = _doc.FamilyCreate.NewModelCurve(plnLine1, skplane)

      Return mCurve

  End Function

  ''' <summary>
  ''' Translate an Arc NURBS curve to an arc ModelCurve
  ''' </summary>
  ''' <param name="rh_arc">Arc NURBS Curve</param>
  ''' <param name="skplane">Sketch Plane</param>
  ''' <returns>ModelCurve</returns>
  ''' <remarks></remarks>
  Public Function RVTArcCurve(ByVal rh_arc As IOnNurbsCurve, ByVal skplane As SketchPlane) As ModelCurve
    Dim rh_nrbscrv As OnNurbsCurve = rh_arc.NurbsCurve()

    Dim tParam1 As Double = rh_nrbscrv.GrevilleAbcissa(0)
    Dim tParam2 As Double = rh_nrbscrv.GrevilleAbcissa(1)
    Dim tParam3 As Double = rh_nrbscrv.GrevilleAbcissa(2)
    Dim rh_startPt As IOn3dPoint = rh_nrbscrv.PointAtStart
    Dim rh_midPt As IOn3dPoint = rh_nrbscrv.PointAt(tParam2)
    Dim rh_endPt As IOn3dPoint = rh_nrbscrv.PointAtEnd

    Dim startPt As IOn3dPoint = rh_startPt
    Dim endPt As IOn3dPoint = rh_endPt
    Dim midPt As IOn3dPoint = rh_midPt

    'start point values
    Dim x1 As Double = startPt.x
    Dim y1 As Double = startPt.y
    Dim z1 As Double = startPt.z

    'end point values
    Dim x2 As Double = endPt.x
    Dim y2 As Double = endPt.y
    Dim z2 As Double = endPt.z

    'end point values
    Dim x3 As Double = midPt.x
    Dim y3 As Double = midPt.y
    Dim z3 As Double = midPt.z

    Dim startXYZ As New XYZ(x1, y1, z1)
    Dim endXYZ As New XYZ(x2, y2, z2)
    Dim midXYZ As New XYZ(x3, y3, z3)

    Dim myArc As Arc = Arc.Create(startXYZ, endXYZ, midXYZ)
    Dim mCurve As ModelCurve

    mCurve = _doc.FamilyCreate.NewModelCurve(myArc, skplane)

    Return mCurve

  End Function

  ''' <summary>
  ''' Translate an OpenNURBS NURBS Curve to a Revit NurbSpline
  ''' </summary>
  ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
  ''' <returns>A Revit ModelCurve</returns>
  ''' <remarks>Translates an openNURBS Spline Curve to a Revit NurbSpline</remarks>
  Public Function RVTNurbsCrv(ByVal rh_splncrv As IOnNurbsCurve, ByVal skplane As SketchPlane) As ModelCurve

    Dim ctrlPts As New List(Of XYZ)
    Dim weights As New List(Of Double)
    Dim rvtPts As New List(Of XYZ)

    Dim degree As Integer = rh_splncrv.Degree()
    Dim knotcount As Integer = rh_splncrv.KnotCount
    Dim knots As New DoubleArray
    Dim weightarr As New DoubleArray

    For i As Integer = 0 To knotcount - 1
      Dim knot As Double = rh_splncrv.Knot(i)
      If i = 0 Then
        knots.Append(knot)
      End If

      If i = knotcount - 1 Then
        knots.Append(knot)
      End If

      knots.Append(knot)

    Next

    For i As Integer = 0 To rh_splncrv.CVCount - 1
      Dim ctrlPt As New On3dPoint(0, 0, 0)
      Dim bool As Boolean = rh_splncrv.GetCV(i, ctrlPt)
      If bool = True Then
        Dim weight As Double = rh_splncrv.Weight(i)

        Dim x As Double = ctrlPt.x
        Dim y As Double = ctrlPt.y
        Dim z As Double = ctrlPt.z

        Dim myXYZ As New XYZ(x, y, z)
        rvtPts.Add(myXYZ)
        ctrlPts.Add(myXYZ)
        weights.Add(weight)

        weightarr.Append(weight)
      End If

    Next

      Dim hermiteSpline As HermiteSpline = Autodesk.Revit.DB.HermiteSpline.Create(ctrlPts,False)
      Dim splineCurve As NurbSpline = NurbSpline.Create(hermiteSpline)

    Dim mCurve As ModelCurve

    mCurve = _doc.FamilyCreate.NewModelCurve(splineCurve, skplane)

    'Make a nurbs spline curve
    Return mCurve

  End Function

  Public Function RVTNurbsCrvClosed(ByVal rh_splncrv As IOnNurbsCurve, ByVal skplane As SketchPlane) As List(Of ModelCurve)

    Dim ctrlPts As New List(Of XYZ)
    Dim weights As New List(Of Double)
    Dim rvtPts As New List(Of XYZ)

    For i As Integer = 0 To rh_splncrv.CVCount - 1
      Dim ctrlPt As New On3dPoint(0, 0, 0)
      Dim bool As Boolean = rh_splncrv.GetCV(i, ctrlPt)
      If bool = True Then
        Dim weight As Double = rh_splncrv.Weight(i)

        Dim x As Double = ctrlPt.x
        Dim y As Double = ctrlPt.y
        Dim z As Double = ctrlPt.z

        Dim myXYZ As New XYZ(x, y, z)
        rvtPts.Add(myXYZ)
        ctrlPts.Add(myXYZ)
        weights.Add(weight)
      End If

    Next

      Dim hermiteSpline As HermiteSpline = Autodesk.Revit.DB.HermiteSpline.Create(ctrlPts,False)
      Dim splineCurve As NurbSpline = NurbSpline.Create(hermiteSpline)

    'Get spline segments because Revit hates the shit out of periodic ModelCurves.
    Dim seg1 As NurbSpline = splineCurve.Clone
    Dim seg2 As NurbSpline = splineCurve.Clone
    Dim length As Double = splineCurve.ApproximateLength
    seg1.MakeBound(0, (length / 2))
    seg2.MakeBound((length / 2), length)

    'Make ModelCurve segments.
    Dim msplines As New List(Of ModelCurve)

    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg1, skplane))
    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg2, skplane))

    'Make a nurbs spline curve
    Return msplines

  End Function

  ''' <summary>
  ''' Translate an OpenNURBS NURBS Curve to a Revit HermiteSpline
  ''' </summary>
  ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
  ''' <returns>A list of ModelHermite Splines</returns>
  ''' <remarks>Calculates appoximated Hermite Splines using Greville vertices from flat, smooth NURBS curves</remarks>
  Public Function RVTHermSplineCrv(ByVal rh_splncrv As IOnNurbsCurve, ByVal skplane As SketchPlane) As List(Of ModelHermiteSpline)

    Dim ctrlPts As New List(Of XYZ)
    Dim weights As New List(Of Double)
    Dim rvtPts As New List(Of XYZ)

    If _s.PrecisionSpline = 1 Then

      For i As Integer = 1 To rh_splncrv.CVCount - 3
        Dim tParam As Double = rh_splncrv.GrevilleAbcissa(i)
        Dim ctrlPt As IOn3dPoint = rh_splncrv.PointAt(tParam)

        Dim weight As Double = rh_splncrv.Weight(i)

        Dim x As Double = ctrlPt.x
        Dim y As Double = ctrlPt.y
        Dim z As Double = ctrlPt.z

        Dim myXYZ As New XYZ(x, y, z)
        rvtPts.Add(myXYZ)
        ctrlPts.Add(myXYZ)

        'calculate weights in the off chance they become useful for nurbs in the future.
        weights.Add(weight)

      Next

    Else


      Dim p As Integer = _s.PrecisionSpline
      Dim cvs As Integer = rh_splncrv.CVCount * p

      Dim sinterval As OnInterval = rh_splncrv.Domain()
      Dim t0 As Double = sinterval.Min
      Dim t1 As Double = sinterval.Max
      Dim slength As Double = t1 - t0
      Dim incr As Double = slength / cvs

      For i As Integer = 0 To cvs - 2
        Dim tParam As Double = i * incr
        Dim ctrlPt As IOn3dPoint = rh_splncrv.PointAt(tParam)

        Dim x As Double = ctrlPt.x
        Dim y As Double = ctrlPt.y
        Dim z As Double = ctrlPt.z

        Dim myXYZ As New XYZ(x, y, z)
        rvtPts.Add(myXYZ)
        ctrlPts.Add(myXYZ)

      Next

    End If

    ctrlPts.Add(ctrlPts(0))

    'The HermiteSpline
    Dim splinecurve As HermiteSpline
    splinecurve = HermiteSpline.Create(ctrlPts, True)

    'Get spline segments because Revit hates the shit out of periodic ModelCurves.
    Dim seg1 As HermiteSpline = splinecurve.Clone
    Dim seg2 As HermiteSpline = splinecurve.Clone
    Dim seg3 As HermiteSpline = splinecurve.Clone
    Dim seg4 As HermiteSpline = splinecurve.Clone
    Dim length As Double = splinecurve.ApproximateLength
    seg1.MakeBound(0, (length / 4))
    seg2.MakeBound((length / 4), 2 * (length / 4))
    seg3.MakeBound(2 * (length / 4), 3 * (length / 4))
    seg4.MakeBound(3 * (length / 4), length)

    'Make ModelCurve segments.
    Dim msplines As New List(Of ModelHermiteSpline)

    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg1, skplane))
    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg2, skplane))
    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg3, skplane))
    msplines.Add(_doc.FamilyCreate.NewModelCurve(seg4, skplane))



    'Make a nurbs spline curve
    Return msplines

  End Function

  ''' <summary>
  ''' Translate an OpenNURBS NURBS Curve to a Revit CurveByPoints in a Family Mass.
  ''' </summary>
  ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
  ''' <returns>Revit CurveByPoints</returns>
  ''' <remarks>Calculates appoximated CurveByPoints using Greville vertices.  FAMILY MASS ONLY!</remarks>
  Public Function RVTCurveByPoints(ByVal rh_splncrv As IOnNurbsCurve, ByVal skplane As SketchPlane) As CurveByPoints

    Dim refPtArr As New ReferencePointArray()
    Dim firstpt As XYZ = Nothing


    If _s.PrecisionSpline = 1 Then

      'set range conditions
      Dim d0 As Integer = 0
      Dim d1 As Integer = 0
      If rh_splncrv.IsPeriodic Then
        d0 = 1
        d1 = rh_splncrv.CVCount - 3
      Else
        d0 = 0
        d1 = rh_splncrv.CVCount - 1
      End If

      For i As Integer = d0 To d1
        Dim tParam As Double = rh_splncrv.GrevilleAbcissa(i)
        Dim editPt As IOn3dPoint = rh_splncrv.PointAt(tParam)

        Dim x As Double = editPt.x
        Dim y As Double = editPt.y
        Dim z As Double = editPt.z

        Dim xyzVec As New XYZ(x, y, z)
        Dim refPt As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(xyzVec)
        If i = d0 Then
          firstpt = xyzVec
        End If
        refPtArr.Append(refPt)

      Next

    Else

      'Use Spline Precision Multiplier

      Dim p As Integer = _s.PrecisionSpline
      Dim cvs As Integer = rh_splncrv.CVCount * p

      Dim sinterval As OnInterval = rh_splncrv.Domain()
      Dim t0 As Double = sinterval.Min
      Dim t1 As Double = sinterval.Max
      Dim slength As Double = t1 - t0
      Dim incr As Double = slength / cvs

      Dim c As Integer = 0
      If rh_splncrv.IsPeriodic Then
        c = 1
      Else
        c = 0
      End If

      For i As Integer = 0 To cvs - c

        Dim tParam As Double = (i * incr) + t0
        Dim ctrlPt As IOn3dPoint = rh_splncrv.PointAt(tParam)

        Dim x As Double = ctrlPt.x
        Dim y As Double = ctrlPt.y
        Dim z As Double = ctrlPt.z

        Dim xyzVec As New XYZ(x, y, z)
        Dim refPt As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(xyzVec)
        If i = 0 Then
          firstpt = xyzVec
        End If
        refPtArr.Append(refPt)

      Next

    End If

    If rh_splncrv.IsPeriodic Then
      Dim refPt As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(firstpt)
      refPtArr.Append(refPt)
    End If

    Dim ptscrv As CurveByPoints = _doc.FamilyCreate.NewCurveByPoints(refPtArr)


    Return ptscrv

  End Function

End Class
