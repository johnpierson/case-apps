Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Imports System.Collections.Generic
Imports RMA.OpenNURBS

''' <summary>
''' FAMILY GEOMETRY TRANSLATING CLASS
''' </summary>
''' <remarks>Translators specific to Conceptual Massing</remarks>
Public Class RVTFamilyMassUtils

  Private _s As clsSettings

  Private _doc As Document
  Private _app As Autodesk.Revit.UI.UIApplication
  Dim Tolerance = 1e-6
  Public Function PlaneFromCurve(ByVal l as Line) As Plane
      Dim period = If(l.IsBound, 0.0,(If(l.IsCyclic, l.Period,1.0)))

      Dim p0 = If(l.IsBound, l.Evaluate(0.0,true),l.Evaluate(0.0,false))
      Dim p1 = If(l.IsBound, l.Evaluate(0.5,true),l.Evaluate(0.25,false))
      Dim p2 = If(l.IsBound, l.Evaluate(1.0,true),l.Evaluate(0.5,false))

      Dim norm As XYZ

      If Math.Abs(p0.Z - p2.Z) < Tolerance
          norm = XYZ.BasisZ
      Else 
          Dim v1 = p1 - p0
          Dim v2 = p2 - p0
          Dim p3 = New XYZ(p2.X, p2.Y, p0.Z)
          Dim v3 = p3 - p0
          norm = v1.CrossProduct(v3)
          If norm.IsZeroLength()
              norm = v2.CrossProduct(XYZ.BasisY)
          End If
          norm = norm.Normalize()
      End If
      return Plane.CreateByNormalAndOrigin(norm,p0)


  End Function
  Public Sub New(ByVal settings As clsSettings)

    'Widen Scope
    _s = settings

    _doc = _s.ActiveDoc
    _app = _s.UIApplication

  End Sub

  ''' <summary>
  ''' Translate an OpenNURBS Point to a Revit Reference point
  ''' </summary>
  ''' <param name="rh_point">OpenNURBS Point Object</param>
  ''' <returns>Revit Reference Point</returns>
  ''' <remarks></remarks>
  Public Function RVTPoint(ByVal rh_point As IOn3dPoint) As ReferencePoint

    Dim x As Double = rh_point.x
    Dim y As Double = rh_point.y
    Dim z As Double = rh_point.z

    Dim myXYZ As New XYZ(x, y, z)
    Return _doc.FamilyCreate.NewReferencePoint(myXYZ)

  End Function

  ''' <summary>
  ''' Translate an OpenNURBS Corner Surface to a Revit Conceptual Form
  ''' </summary>
  ''' <param name="rh_nrbsSurf">OpenNURBS NURBS Surface</param>
  ''' <returns>Revit Form</returns>
  ''' <remarks></remarks>
  Public Function RVTCornerPtSurface(ByVal rh_nrbsSurf As IOnNurbsSurface) As Form

    'Get corner points
    Dim cpt1 As New On3dPoint
    Dim cpt2 As New On3dPoint
    Dim cpt3 As New On3dPoint
    Dim cpt4 As New On3dPoint

    rh_nrbsSurf.GetCV(0, 0, cpt1)
    rh_nrbsSurf.GetCV(0, 1, cpt2)
    rh_nrbsSurf.GetCV(1, 1, cpt3)
    rh_nrbsSurf.GetCV(1, 0, cpt4)

    'Get XYZ values for corners
    Dim x1 As Double = cpt1.x
    Dim y1 As Double = cpt1.y
    Dim z1 As Double = cpt1.z

    Dim x2 As Double = cpt2.x
    Dim y2 As Double = cpt2.y
    Dim z2 As Double = cpt2.z

    Dim x3 As Double = cpt3.x
    Dim y3 As Double = cpt3.y
    Dim z3 As Double = cpt3.z

    Dim x4 As Double = cpt4.x
    Dim y4 As Double = cpt4.y
    Dim z4 As Double = cpt4.z

    'XYZ Vectors
    Dim XYZ1 As New XYZ(x1, y1, z1)
    Dim XYZ2 As New XYZ(x2, y2, z2)
    Dim XYZ3 As New XYZ(x3, y3, z3)
    Dim XYZ4 As New XYZ(x4, y4, z4)

    'Reference Points

    'Determine if the Corner point surface is triangular.
    'Triangluar condition A
    If x1 = x2 And y1 = y2 And z1 = z2 Then
      Dim l1 As Line = Line.CreateBound(XYZ1, XYZ3)
      Dim l2 As Line = Line.CreateBound(XYZ3, XYZ4)
      Dim l3 As Line = Line.CreateBound(XYZ4, XYZ1)

      Dim crvArray As New CurveArray()
      crvArray.Append(l1)
      crvArray.Append(l2)

      Dim pln As Plane = PlaneFromCurve(l1)
      Dim skpln As SketchPlane = SketchPlane.Create(_doc, pln)

      Dim mcrv1 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l1, skpln)
      Dim mcrv2 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l2, skpln)
      Dim mcrv3 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l3, skpln)

      Dim refArr As New ReferenceArray()
      refArr.Append(mcrv1.GeometryCurve.Reference)
      refArr.Append(mcrv2.GeometryCurve.Reference)
      refArr.Append(mcrv3.GeometryCurve.Reference)

      Dim cap As Form = _doc.FamilyCreate.NewFormByCap(True, refArr)

      'Create a Form by Cap
      Return cap

      'Triangluar condition B
    ElseIf x2 = x3 And y2 = y3 And z2 = z3 Then

      Dim l1 As Line = Line.CreateBound(XYZ1, XYZ2)
      Dim l2 As Line = Line.CreateBound(XYZ2, XYZ4)
      Dim l3 As Line = Line.CreateBound(XYZ4, XYZ1)

      Dim crvArray As New CurveArray()
      crvArray.Append(l1)
      crvArray.Append(l2)

        Dim pln As Plane = PlaneFromCurve(l1)
      Dim skpln As SketchPlane = SketchPlane.Create(_doc, pln)

      Dim mcrv1 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l1, skpln)
      Dim mcrv2 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l2, skpln)
      Dim mcrv3 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l3, skpln)

      Dim refArr As New ReferenceArray()
      refArr.Append(mcrv1.GeometryCurve.Reference)
      refArr.Append(mcrv2.GeometryCurve.Reference)
      refArr.Append(mcrv3.GeometryCurve.Reference)

      'Create a Form by Cap
      Dim cap As Form = _doc.FamilyCreate.NewFormByCap(True, refArr)

      Return cap

      'Triangluar condition C
    ElseIf x1 = x4 And y1 = y4 And z1 = z4 Then

      Dim l1 As Line = Line.CreateBound(XYZ1, XYZ2)
      Dim l2 As Line = Line.CreateBound(XYZ2, XYZ3)
      Dim l3 As Line = Line.CreateBound(XYZ3, XYZ1)

      Dim crvArray As New CurveArray()
      crvArray.Append(l1)
      crvArray.Append(l2)

        Dim pln As Plane = PlaneFromCurve(l1)
      Dim skpln As SketchPlane = SketchPlane.Create(_doc, pln)

      Dim mcrv1 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l1, skpln)
      Dim mcrv2 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l2, skpln)
      Dim mcrv3 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l3, skpln)

      Dim refArr As New ReferenceArray()
      refArr.Append(mcrv1.GeometryCurve.Reference)
      refArr.Append(mcrv2.GeometryCurve.Reference)
      refArr.Append(mcrv3.GeometryCurve.Reference)

      'Create a Form by Cap
      Dim cap As Form = _doc.FamilyCreate.NewFormByCap(True, refArr)

      Return cap

      'Triangluar condition D
    ElseIf x3 = x4 And y3 = y4 And z3 = z4 Then

      Dim l1 As Line = Line.CreateBound(XYZ1, XYZ2)
      Dim l2 As Line = Line.CreateBound(XYZ2, XYZ3)
      Dim l3 As Line = Line.CreateBound(XYZ3, XYZ1)

      Dim crvArray As New CurveArray()
      crvArray.Append(l1)
      crvArray.Append(l2)

        Dim pln As Plane = PlaneFromCurve(l1)
      Dim skpln As SketchPlane = SketchPlane.Create(_doc, pln)

      Dim mcrv1 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l1, skpln)
      Dim mcrv2 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l2, skpln)
      Dim mcrv3 As ModelCurve = _doc.FamilyCreate.NewModelCurve(l3, skpln)

      Dim refArr As New ReferenceArray()
      refArr.Append(mcrv1.GeometryCurve.Reference)
      refArr.Append(mcrv2.GeometryCurve.Reference)
      refArr.Append(mcrv3.GeometryCurve.Reference)

      'Create a Form by Cap
      Dim cap As Form = _doc.FamilyCreate.NewFormByCap(True, refArr)

      Return cap

      'Surface has Four Corner Points
    Else

      Dim Ref1 As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(XYZ1)
      Dim Ref2 As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(XYZ2)
      Dim Ref3 As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(XYZ3)
      Dim Ref4 As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(XYZ4)

      'Curves
      Dim refptArr1 As New ReferencePointArray()
      refptArr1.Append(Ref1)
      refptArr1.Append(Ref2)

      Dim refptArr2 As New ReferencePointArray()
      refptArr2.Append(Ref4)
      refptArr2.Append(Ref3)

      Dim cv1 As CurveByPoints = _doc.FamilyCreate.NewCurveByPoints(refptArr1)
      Dim cv2 As CurveByPoints = _doc.FamilyCreate.NewCurveByPoints(refptArr2)

      'Surface
      Dim RefArrArr As New ReferenceArrayArray()

      Dim RefArr1 As New ReferenceArray()
      Dim RefArr2 As New ReferenceArray()

      RefArr1.Append(cv1.GeometryCurve.Reference)
      RefArr2.Append(cv2.GeometryCurve.Reference)

      RefArrArr.Append(RefArr1)
      RefArrArr.Append(RefArr2)

      'Create a New Loft Form
      Dim cSurface As Form = _doc.FamilyCreate.NewLoftForm(True, RefArrArr)

      Return cSurface
    End If
  End Function

  ''' <summary>
  ''' Translate an OpenNURBS Surface to a Revit Conceptual Form using a Point Grid
  ''' </summary>
  ''' <param name="rh_nrbsSurf">OpenNURBS Surface</param>
  ''' <returns>Revit Form</returns>
  ''' <remarks></remarks>
  Public Function RVTPtSurface(ByVal rh_nrbsSurf As IOnNurbsSurface) As Form

    'Determine the U and V Counts for surface points.
    Dim UCount As Integer = (rh_nrbsSurf.CVCount(0) - 1) * _s.PrecisionSurface
    Dim VCount As Integer = (rh_nrbsSurf.CVCount(1) - 1) * _s.PrecisionSurface

    'Determine U and V steps
    Dim uStep As Double = 1 / UCount
    Dim vStep As Double = 1 / VCount

    'Reparameterize the surface to UV domains of 0 and 1
    Dim d1 As OnInterval = rh_nrbsSurf.Domain(0)
    Dim d2 As OnInterval = rh_nrbsSurf.Domain(1)

    Dim ulength As Double = d1.Max - d1.Min
    Dim vlength As Double = d2.Max - d2.Min

    Dim uInc As Double = uStep * ulength
    Dim vInc As Double = vStep * vlength

    'Dim refptlist As New List(Of ReferencePoint)

    'ReferenceArrayArray for use with NewLoftForm
    Dim refarrarr As New ReferenceArrayArray()

    For i As Integer = 0 To UCount
      'ReferencePointArray for Curve by Points
      Dim refptarr As New ReferencePointArray()
      For j As Integer = 0 To VCount

        'Get a point on the surface
        Dim myPoint As On3dPoint = rh_nrbsSurf.PointAt((i * uInc) + d1.Min, (j * vInc) + d2.Min)
        Dim x As Double = myPoint.x
        Dim y As Double = myPoint.y
        Dim z As Double = myPoint.z

        'Convert point to Reference point
        Dim myXYZ As New XYZ(x, y, z)
        Dim refPt As ReferencePoint = _doc.FamilyCreate.NewReferencePoint(myXYZ)
        'refptlist.Add(refPt)
        'Append Reference Point Array
        refptarr.Append(refPt)

      Next

      'Create a curve by points
      Dim curve As CurveByPoints = _doc.FamilyCreate.NewCurveByPoints(refptarr)
      Dim refarr As New ReferenceArray()

      'Append ReferenceArray and ReferenceArrayArray
      refarr.Append(curve.GeometryCurve.Reference)
      refarrarr.Append(refarr)
    Next

    'Create a surface form
    Dim pointsSurface As Form = _doc.FamilyCreate.NewLoftForm(True, refarrarr)

    Return pointsSurface

  End Function

  ''' <summary>
  ''' Create a Planar Surface by CAP
  ''' </summary>
  ''' <param name="rh_brep"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function RVTTrimSurface(ByVal rh_brep As IOnBrep) As Form

    Dim m_crvutils As New RVTBrepEdgeUtils(_s)
    Dim m_formutil As New clsFormUtil(_doc, _app)
    Dim m_edge As IOnBrepEdgeArray = rh_brep.m_E
    Dim m_refarr As New ReferenceArray()
    Dim m_trim As IOnCurveArray = rh_brep.m_C2

    Dim usrf As OnBrep = rh_brep.Face(0).BrepForm

    'Define Plane
    Dim m_verts As OnBrepVertexArray = usrf.m_V
    Dim x1 As Double = m_verts.Item(0).Point.x
    Dim y1 As Double = m_verts.Item(0).Point.y
    Dim z1 As Double = m_verts.Item(0).Point.z

    Dim x2 As Double = m_verts.Item(1).Point.x
    Dim y2 As Double = m_verts.Item(1).Point.y
    Dim z2 As Double = m_verts.Item(1).Point.z

    Dim x3 As Double = m_verts.Item(2).Point.x
    Dim y3 As Double = m_verts.Item(2).Point.y
    Dim z3 As Double = m_verts.Item(2).Point.z

    Dim xyz1 As New XYZ(x1, y1, z1)
    Dim xyz2 As New XYZ(x2, y2, z2)
    Dim xyz3 As New XYZ(x3, y3, z3)

    Dim skplane As SketchPlane = m_formutil.CreateSketchPlane(xyz1, xyz2, xyz3)

    For i As Integer = 0 To m_edge.Count - 1
      Dim edge As IOnBrepEdge = m_edge.Item(i)
      Dim mycurve As OnCurve = edge.DuplicateCurve

      'Check if the curve is a NURBS Curve....
      Dim nrbcrv As OnNurbsCurve = OnNurbsCurve.ConstCast(mycurve)
      If nrbcrv IsNot Nothing Then

        Dim spans As New List(Of OnNurbsCurve)
        Dim knots() As Double = Nothing
        nrbcrv.GetSpanVector(knots)

        For j As Integer = 0 To nrbcrv.SpanCount - 1
          Dim t0 As Double = knots(j)
          Dim t1 As Double = knots(j + 1)
          Dim cInterval As New OnInterval(t0, t1)

          Dim myNrbscrv As OnNurbsCurve = nrbcrv.NurbsCurve(nrbcrv, _s.ModelTolerance, cInterval)

          If myNrbscrv.IsPlanar Then

            'Test if the NURBS curve is a Line
            If myNrbscrv.IsLinear Then
              Dim rvt_crv As ModelCurve = m_crvutils.RVTLineCurve(myNrbscrv, skplane)
              m_refarr.Append(rvt_crv.GeometryCurve.Reference)

              'Test if the NURBS curve is an Arc
            ElseIf myNrbscrv.IsArc Then
              Dim rvt_crv As ModelCurve = m_crvutils.RVTArcCurve(myNrbscrv, skplane)
              m_refarr.Append(rvt_crv.GeometryCurve.Reference)

            ElseIf myNrbscrv.IsClosed Or myNrbscrv.IsPeriodic Then
              'Make planar hermite spline
              Dim rvt_crv As List(Of ModelHermiteSpline) = m_crvutils.RVTHermSplineCrv(myNrbscrv, skplane)
              Dim segcrv As ModelHermiteSpline
              For Each segcrv In rvt_crv
                m_refarr.Append(segcrv.GeometryCurve.Reference)
              Next

            Else
              'Make a Planar NurbSpline
              Dim rvt_crv As ModelCurve = m_crvutils.RVTNurbsCrv(myNrbscrv, skplane)
              m_refarr.Append(rvt_crv.GeometryCurve.Reference)
            End If
          End If

        Next
      End If

    Next

    Dim cap As Form = _doc.FamilyCreate.NewFormByCap(True, m_refarr)
    Return cap

  End Function
End Class
