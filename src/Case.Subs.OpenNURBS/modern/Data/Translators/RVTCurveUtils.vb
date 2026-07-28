Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Imports System.Collections.Generic
Imports RMA.OpenNURBS

''' <summary>
''' MODEL CURVE TRANSLATE CLASS
''' </summary>
''' <remarks></remarks>
Public Class RVTCurveUtils

    Private _s As clsSettings

    Private _doc As Document
    Private _app As Autodesk.Revit.UI.UIApplication

    Public Sub New(ByVal settings As clsSettings)

        'Widen Scope
        _s = settings

        _doc = _s.ActiveDoc
        _app = _s.UIApplication

    End Sub
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
    ''' <summary>
    ''' Translate an OpenNURBS Line to a Revit ModelCurve Line
    ''' </summary>
    ''' <param name="rh_line">OpenNURBS Line Object</param>
    ''' <returns>Revit ModelCurve</returns>
    ''' <remarks></remarks>
    Public Function RVTLine(ByVal rh_line As IOnLineCurve) As ModelCurve

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

        Dim crvArray As New CurveArray()
        crvArray.Append(plnLine1)
        crvArray.Append(plnLine2)
#If Revit2018 Then
         Dim myPlane As Plane = _app.Application.Create.NewPlane(crvArray)
#End If

        Dim myPlane As Plane = Plane.CreateByNormalAndOrigin(XYZ.Zero, XYZ.Zero)

        'create the SketchPlanes and ModelCurves
        Dim skplane As SketchPlane
        Dim mCurve As ModelCurve

        'check what kind of document we are working in...
        If _doc.IsFamilyDocument Then
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.FamilyCreate.NewModelCurve(plnLine1, skplane)
        Else
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.Create.NewModelCurve(plnLine1, skplane)
        End If

        Return mCurve

    End Function

    ''' <summary>
    ''' Translate an OpenNURBS Polyline to a Revit ModelCurve Polyline
    ''' </summary>
    ''' <param name="rh_pline">OpenNURBS Polyline Object</param>
    ''' <returns>List of Revit Model Curves</returns>
    ''' <remarks></remarks>
    Public Function RVTPline(ByVal rh_pline As IOnPolylineCurve) As List(Of ModelCurve)

        Dim plineCrvs As New List(Of ModelCurve)
        For i As Integer = 0 To rh_pline.PointCount() - 2
            Dim rh_startPt As IOn3dPoint = rh_pline.m_pline.Item(i)
            Dim rh_endPt As IOn3dPoint = rh_pline.m_pline.Item(i + 1)

            Dim rh_line As New OnLineCurve(rh_startPt, rh_endPt)

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

            Dim crvArray As New CurveArray()
            crvArray.Append(plnLine1)
            crvArray.Append(plnLine2)

            Dim myPlane As Plane = PlaneFromCurve(plnLine1)
            Dim skplane As SketchPlane
            Dim myCurve As ModelCurve

            'check what kind of document we are working in...
            If _app.ActiveUIDocument.Document.IsFamilyDocument Then
                skplane = SketchPlane.Create(_doc, myPlane)
                myCurve = _doc.FamilyCreate.NewModelCurve(plnLine1, skplane)
                plineCrvs.Add(myCurve)
            Else
                skplane = SketchPlane.Create(_doc, myPlane)
                myCurve = _doc.Create.NewModelCurve(plnLine1, skplane)
                plineCrvs.Add(myCurve)
            End If
        Next

        Return plineCrvs

    End Function

    ''' <summary>
    ''' Translate an OpenNURBS Arc to a Revit ModelCurve Arc
    ''' </summary>
    ''' <param name="rh_arc">OpenNURBS Arc Object</param>
    ''' <returns>Revit ModelCurve Arc</returns>
    ''' <remarks></remarks>
    Public Function RVTArc(ByVal rh_arc As IOnArcCurve) As ModelCurve
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

        'Sketch Plane
        Dim plnLine1 As Line = Line.CreateBound(startXYZ, midXYZ)
        Dim plnLine2 As Line = Line.CreateBound(midXYZ, endXYZ)

        Dim crvArray As New CurveArray()
        crvArray.Append(plnLine1)
        crvArray.Append(plnLine2)

        Dim myArc As Arc = Arc.Create(startXYZ, endXYZ, midXYZ)
        Dim myPlane As Plane = PlaneFromCurve(plnLine1)
        Dim skplane As SketchPlane
        Dim mCurve As ModelCurve

        'Determine if the current document is the Family Document
        If _app.ActiveUIDocument.Document.IsFamilyDocument Then
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.FamilyCreate.NewModelCurve(myArc, skplane)
        Else
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.Create.NewModelCurve(myArc, skplane)
        End If

        Return mCurve
    End Function

    ''' <summary>
    ''' Translate an OpenNURBS Circle to a Revit ModelCurve Circle
    ''' </summary>
    ''' <param name="rh_circle">OpenNURBS Circle Object</param>
    ''' <returns>Revit ModelCurve Circle</returns>
    ''' <remarks></remarks>
    Public Function RVTCircle(ByVal rh_circle As IOnArcCurve) As ModelCurve

        'Define Rhino Circle Parameters
        Dim rh_circRadius As Double = rh_circle.Radius
        Dim rh_circPln As OnPlane = rh_circle.m_arc.plane

        'Axis Vectors
        Dim plnX As IOn3dVector = rh_circPln.GetXaxis()
        Dim plnY As IOn3dVector = rh_circPln.GetYaxis()

        'Three Point Plane Parameters
        Dim rh_circPlnX As IOnPlane = rh_circle.m_arc.plane
        Dim rh_circPlnY As IOnPlane = rh_circle.m_arc.plane
        Dim plane1 As New OnPlane(rh_circPlnX)
        Dim plane2 As New OnPlane(rh_circPlnY)
        plane1.Translate(plnX)
        plane2.Translate(plnY)

        'SketchPlan Parameters
        Dim rh_circOrigin As On3dPoint = rh_circPln.origin
        Dim rh_circXAxis As On3dPoint = plane1.origin
        Dim rh_circYAxis As On3dPoint = plane2.origin

        'Revit Axis Vectors
        Dim rvt_xaxis As New XYZ(plnX.x, plnX.y, plnX.z)
        Dim rvt_yaxis As New XYZ(plnY.x, plnY.y, plnY.z)

        'Revit Ellipse Origin
        Dim rvt_origin As New XYZ(rh_circOrigin.x, rh_circOrigin.y, rh_circOrigin.z)

        'Sketch Plane Parameters
        'point values
        Dim x1 As Double = rh_circOrigin.x
        Dim y1 As Double = rh_circOrigin.y
        Dim z1 As Double = rh_circOrigin.z

        'point values
        Dim x2 As Double = rh_circXAxis.x
        Dim y2 As Double = rh_circXAxis.y
        Dim z2 As Double = rh_circXAxis.z

        'point values
        Dim x3 As Double = rh_circYAxis.x
        Dim y3 As Double = rh_circYAxis.y
        Dim z3 As Double = rh_circYAxis.z

        'create Revit XYZ vectors
        Dim XYZ1 As New XYZ(x1, y1, z1)
        Dim XYZ2 As New XYZ(x2, y2, z2)
        Dim XYZ3 As New XYZ(x3, y3, z3)

        'define sketch plane
        Dim worldOrigin As XYZ = XYZ.Zero
        Dim plnLine1 As Line = Line.CreateBound(XYZ1, XYZ2)
        Dim plnLine2 As Line = Line.CreateBound(XYZ2, XYZ3)

        Dim crvArray As New CurveArray()
        crvArray.Append(plnLine1)
        crvArray.Append(plnLine2)

        Dim circArc As Arc = Arc.Create(rvt_origin, rh_circRadius, 0, (2 * Math.PI), rvt_xaxis, rvt_yaxis)
        Dim myPlane As Plane = PlaneFromCurve(plnLine1)

        Dim skplane As SketchPlane
        Dim mCurve As ModelCurve

        'Determine if the current document is the Family Document
        If _app.ActiveUIDocument.Document.IsFamilyDocument Then
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.FamilyCreate.NewModelCurve(circArc, skplane)
        Else
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.Create.NewModelCurve(circArc, skplane)
        End If

        Return mCurve
    End Function

    ''' <summary>
    ''' Translate and OpenNURBS Ellipse to a Revit ModelCurve
    ''' </summary>
    ''' <param name="rh_EllpsNrb">OpenNURBS Ellipse Object</param>
    ''' <returns>A Revit ModelCurve Ellipse</returns>
    ''' <remarks></remarks>
    Public Function RVTEllipse(ByVal rh_EllpsNrb As IOnNurbsCurve) As ModelCurve


        Dim rh_Ellipse As New OnEllipse()
        rh_EllpsNrb.IsEllipse(New OnPlane(), rh_Ellipse, 0.001)


        'Define Rhino Ellipse Parameters
        Dim rh_EllpsRadX As Double = rh_Ellipse.GetRadius(0)
        Dim rh_EllpsRadY As Double = rh_Ellipse.GetRadius(1)
        Dim rh_EllpsPln As OnPlane = rh_Ellipse.plane

        'Axis Vectors
        Dim plnX As IOn3dVector = rh_EllpsPln.GetXaxis()
        Dim plnY As IOn3dVector = rh_EllpsPln.GetYaxis()

        'Three Point Plane Parameters
        Dim rh_ellpsPlnX As IOnPlane = rh_Ellipse.plane
        Dim rh_ellpsPlnY As IOnPlane = rh_Ellipse.plane
        Dim plane1 As New OnPlane(rh_ellpsPlnX)
        Dim plane2 As New OnPlane(rh_ellpsPlnY)
        plane1.Translate(plnX)
        plane2.Translate(plnY)

        'SketchPlan Parameters
        Dim rh_ellpsOrigin As On3dPoint = rh_EllpsPln.origin
        Dim rh_ellpsXAxis As On3dPoint = plane1.origin
        Dim rh_ellpsYAxis As On3dPoint = plane2.origin

        'Revit Axis Vectors
        Dim rvt_xaxis As New XYZ(plnX.x, plnX.y, plnX.z)
        Dim rvt_yaxis As New XYZ(plnY.x, plnY.y, plnY.z)

        'Revit Ellipse Origin
        Dim rvt_origin As New XYZ(rh_ellpsOrigin.x, rh_ellpsOrigin.y, rh_ellpsOrigin.z)

        'Sketch Plane Parameters
        'point values
        Dim x1 As Double = rh_ellpsOrigin.x
        Dim y1 As Double = rh_ellpsOrigin.y
        Dim z1 As Double = rh_ellpsOrigin.z

        'point values
        Dim x2 As Double = rh_ellpsXAxis.x
        Dim y2 As Double = rh_ellpsXAxis.y
        Dim z2 As Double = rh_ellpsXAxis.z

        'point values
        Dim x3 As Double = rh_ellpsYAxis.x
        Dim y3 As Double = rh_ellpsYAxis.y
        Dim z3 As Double = rh_ellpsYAxis.z

        'create Revit XYZ vectors
        Dim XYZ1 As New XYZ(x1, y1, z1)
        Dim XYZ2 As New XYZ(x2, y2, z2)
        Dim XYZ3 As New XYZ(x3, y3, z3)

        'define sketch plane
        Dim worldOrigin As XYZ = XYZ.Zero
        Dim plnLine1 As Line = Line.CreateBound(XYZ1, XYZ2)
        Dim plnLine2 As Line = Line.CreateBound(XYZ2, XYZ3)

        Dim crvArray As New CurveArray()
        crvArray.Append(plnLine1)
        crvArray.Append(plnLine2)

        Dim circle As Ellipse = Ellipse.CreateCurve(rvt_origin, rh_EllpsRadX, rh_EllpsRadY, rvt_xaxis, rvt_yaxis, 0, (2 * Math.PI))

        Dim myPlane As Plane = PlaneFromCurve(plnLine1)
        Dim skplane As SketchPlane = SketchPlane.Create(_doc, myPlane)
        Dim mCurve As ModelCurve

        'Determine if the current document is the Family Document
        If _app.ActiveUIDocument.Document.IsFamilyDocument Then
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.FamilyCreate.NewModelCurve(circle, skplane)
        Else
            skplane = SketchPlane.Create(_doc, myPlane)
            mCurve = _doc.Create.NewModelCurve(circle, skplane)
        End If

        Return mCurve

    End Function

    ''' <summary>
    ''' Translate an OpenNURBS NURBS Curve to a list of Revit ModelCurve segments 
    ''' </summary>
    ''' <param name="rh_nrbscrv">OpenNURBS NurbsCurve Object</param>
    ''' <returns>List of ModelCurves</returns>
    ''' <remarks>Smooths NURBS Curves reduced to Line Segments at Greville vertices</remarks>
    Public Function RVTSegNurbsCurve(ByVal rh_nrbscrv As IOnNurbsCurve) As List(Of ModelCurve)

        Dim plineCrvs As New List(Of ModelCurve)

        'set range conditions
        Dim d0 As Integer = 0
        Dim d1 As Integer = 0
        If rh_nrbscrv.IsPeriodic Then
            d0 = 1
            d1 = rh_nrbscrv.CVCount - 3
        Else
            d0 = 0
            d1 = rh_nrbscrv.CVCount - 2
        End If

        For i As Integer = d0 To d1
            Dim tParam1 As Double = rh_nrbscrv.GrevilleAbcissa(i)
            Dim tParam2 As Double = rh_nrbscrv.GrevilleAbcissa(i + 1)
            Dim rh_startPt As IOn3dPoint = rh_nrbscrv.PointAt(tParam1)
            Dim rh_endPt As IOn3dPoint = rh_nrbscrv.PointAt(tParam2)

            Dim rh_line As New OnLineCurve(rh_startPt, rh_endPt)

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

            Dim crvArray As New CurveArray()
            crvArray.Append(plnLine1)
            crvArray.Append(plnLine2)

            Dim myPlane As Plane = PlaneFromCurve(plnLine1)
            Dim skplane As SketchPlane
            Dim mCurve As ModelCurve

            'Determine if the current document is the Family Document
            If _app.ActiveUIDocument.Document.IsFamilyDocument Then
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.FamilyCreate.NewModelCurve(plnLine1, skplane)
            Else
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.Create.NewModelCurve(plnLine1, skplane)
            End If

            plineCrvs.Add(mCurve)
        Next

        Return plineCrvs
    End Function

    ''' <summary>
    ''' Translate an OpenNURBS NURBS Curve to a Revit NurbSpline
    ''' </summary>
    ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
    ''' <returns>A Revit ModelCurve</returns>
    ''' <remarks>Translates an openNURBS Spline Curve to a Revit NurbSpline</remarks>
    Public Function RVTNurbsCrv(ByVal rh_splncrv As IOnNurbsCurve) As ModelCurve

        If rh_splncrv.IsLinear Then
            Dim rh_line As IOnLineCurve = New OnLineCurve(rh_splncrv.PointAtStart, rh_splncrv.PointAtEnd)

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

            Dim crvArray As New CurveArray()
            crvArray.Append(plnLine1)
            crvArray.Append(plnLine2)

            Dim myPlane As Plane = PlaneFromCurve(plnLine1)

            'create the SketchPlanes and ModelCurves
            Dim skplane As SketchPlane
            Dim mCurve As ModelCurve


            If _doc.IsFamilyDocument Then
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.FamilyCreate.NewModelCurve(plnLine1, skplane)
            Else
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.Create.NewModelCurve(plnLine1, skplane)
            End If

            Return mCurve

        Else

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

            'Define Sketch Plane
            Dim pt1 As XYZ = rvtPts(0)
            Dim pt2 As XYZ = rvtPts(1)
            Dim pt3 As XYZ = rvtPts(2)

            Dim plnLine1 As Line = Line.CreateBound(pt1, pt2)
            Dim plnLine2 As Line = Line.CreateBound(pt2, pt3)

            Dim crvArray As New CurveArray()
            crvArray.Append(plnLine1)
            crvArray.Append(plnLine2)

            Dim myPlane As Plane = PlaneFromCurve(plnLine1)

            Dim skplane As SketchPlane
            Dim mCurve As ModelCurve

            If _app.ActiveUIDocument.Document.IsFamilyDocument Then
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.FamilyCreate.NewModelCurve(splineCurve, skplane)
            Else
                skplane = SketchPlane.Create(_doc, myPlane)
                mCurve = _doc.Create.NewModelCurve(splineCurve, skplane)
            End If

            'Make a nurbs spline curve
            Return mCurve

        End If


    End Function

    ''' <summary>
    ''' Translate an OpenNURBS NURBS Curve to a Revit HermiteSpline
    ''' </summary>
    ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
    ''' <returns>A list of ModelHermite Splines</returns>
    ''' <remarks>Calculates appoximated Hermite Splines using Greville vertices from flat, smooth NURBS curves</remarks>
    Public Function RVTHermSplineCrv(ByVal rh_splncrv As IOnNurbsCurve) As List(Of ModelHermiteSpline)

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


        'Define Sketch Plane
        Dim pt1 As XYZ = rvtPts(1)
        Dim pt2 As XYZ = rvtPts(2)
        Dim pt3 As XYZ = rvtPts(3)

        Dim plnLine1 As Line = Line.CreateBound(pt1, pt2)
        Dim plnLine2 As Line = Line.CreateBound(pt2, pt3)

        Dim crvArray As New CurveArray()
        crvArray.Append(plnLine1)
        crvArray.Append(plnLine2)

        Dim myPlane As Plane = PlaneFromCurve(plnLine1)
        Dim skplane As SketchPlane

        'Make ModelCurve segments.
        Dim msplines As New List(Of ModelHermiteSpline)

        'Determine if the current document is the Family Document
        If _app.ActiveUIDocument.Document.IsFamilyDocument Then
            skplane = SketchPlane.Create(_doc, myPlane)
            msplines.Add(_doc.FamilyCreate.NewModelCurve(seg1, skplane))
            msplines.Add(_doc.FamilyCreate.NewModelCurve(seg2, skplane))
            msplines.Add(_doc.FamilyCreate.NewModelCurve(seg3, skplane))
            msplines.Add(_doc.FamilyCreate.NewModelCurve(seg4, skplane))
        Else
            skplane = SketchPlane.Create(_doc, myPlane)
            msplines.Add(_doc.Create.NewModelCurve(seg1, skplane))
            msplines.Add(_doc.Create.NewModelCurve(seg2, skplane))
            msplines.Add(_doc.Create.NewModelCurve(seg3, skplane))
            msplines.Add(_doc.Create.NewModelCurve(seg4, skplane))
        End If

        'Make a nurbs spline curve
        Return msplines

    End Function

    ''' <summary>
    ''' Translate an OpenNURBS NURBS Curve to a Revit CurveByPoints in a Family Mass.
    ''' </summary>
    ''' <param name="rh_splncrv">OpenNURBS NurbsCurve Object</param>
    ''' <returns>Revit CurveByPoints</returns>
    ''' <remarks>Calculates appoximated CurveByPoints using Greville vertices.  FAMILY MASS ONLY!</remarks>
    Public Function RVTCurveByPoints(ByVal rh_splncrv As IOnNurbsCurve) As CurveByPoints

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