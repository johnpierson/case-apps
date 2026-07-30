Imports System.Collections.Generic
Imports RMA.OpenNURBS

Public Class clsONSort

  Private _s As clsSettings

  'Path variable
  Private _rh As OnXModel

  'List of point objects
  Public points As List(Of IOn3dPoint)

  'Lists of curve objects
  Public lines As List(Of IOnLineCurve)
  Public plines As List(Of IOnPolylineCurve)
  Public arcs As List(Of IOnArcCurve)
  Public circs As List(Of IOnArcCurve)
  Public ellipses As List(Of IOnEllipse)
  Public polycrvs As List(Of IOnPolyCurve)
  Public nurbscrv2d As List(Of IOnNurbsCurve)
  Public closedspline2d As List(Of IOnNurbsCurve)
  Public splinecrv3d As List(Of IOnNurbsCurve)

  'Lists of breps
  Public surfaces As List(Of IOnBrep)
  Public cornersurfaces As List(Of IOnBrep)
  Public trimsurfaces As List(Of IOnBrep)


  Public Sub New(ByVal rh As OnXModel, ByVal settings As clsSettings)

    'Widen Scope
    _s = settings

    'Rhino Model
    _rh = rh

    'SortObjects
    SortObjects(rh)
  End Sub

  Public ReadOnly Property ObjectCount As Integer
    Get
      Dim count As Integer = points.Count + lines.Count + arcs.Count + circs.Count + ellipses.Count + polycrvs.Count + nurbscrv2d.Count + closedspline2d.Count + splinecrv3d.Count + surfaces.Count + trimsurfaces.Count + cornersurfaces.Count
      Return count
    End Get
  End Property


  ''' <summary>
  ''' Sorts Rhino objects into list
  ''' </summary>
  ''' <param name="rh"> Rhino Model Object</param>
  ''' <remarks></remarks>
  Private Sub SortObjects(ByVal rh As OnXModel)

    'List of point objects
    Dim m_rh_points As New List(Of IOn3dPoint)

    'Lists of curve objects
    Dim m_rh_lines As New List(Of IOnLineCurve)
    Dim m_rh_plines As New List(Of IOnPolylineCurve)
    Dim m_rh_arcs As New List(Of IOnArcCurve)
    Dim m_rh_circs As New List(Of IOnArcCurve)
    Dim m_rh_ellipses As New List(Of IOnEllipse)
    Dim m_rh_polys As New List(Of IOnPolyCurve)
    Dim m_rh_nurbscrv2d As New List(Of IOnNurbsCurve)
    Dim m_rh_closedspline2d As New List(Of IOnNurbsCurve)
    Dim m_rh_splinecrv3d As New List(Of IOnNurbsCurve)

    'Lists of breps
    Dim m_rh_surfaces As New List(Of IOnBrep)
    Dim m_rh_cornersurfaces As New List(Of IOnBrep)
    Dim m_rh_trimsurfaces As New List(Of IOnBrep)

    'Iterate through Model Objects and determine what kind of object it is....
    For i As Integer = 0 To rh.m_object_table.Count()
      Dim modelObject As IOnXModel_Object = rh.m_object_table(i)
      If modelObject Is Nothing Then
        Continue For
      Else
        Dim modelobj As IOnObject = modelObject.m_object
        Dim dupobj As OnObject = modelobj.DuplicateOnObject()

        Dim obj As OnGeometry = OnGeometry.Cast(dupobj)


        Dim pln As New OnPlane()
        Dim xform As New OnXform
        Dim scalefactor As Double = _s.ModelScale
        xform.Scale(pln.origin, scalefactor)
        obj.Transform(xform)


        If _s.ImportPoints = True Then
          'CHECK if the Object is a POINT
          If obj.ObjectType.ToString = "point_object" Then
            Dim myPoint As IOnPoint = OnPoint.ConstCast(obj)
            Dim my3dpoint As On3dPoint = myPoint.point

            m_rh_points.Add(my3dpoint)
          End If
        End If


        'Check if the Object is a CURVE
        If obj.ObjectType.ToString = "curve_object" Then
          Dim myCurve As OnCurve = OnCurve.ConstCast(obj)

          'Find out what kind of curve it is....

          If _s.ImportLines = True Then
            'Check if the curve is a Line
            Dim myLine As IOnLineCurve = OnLineCurve.ConstCast(myCurve)
            If myLine IsNot Nothing Then
              m_rh_lines.Add(myLine)
            End If
          End If

          If _s.ImportPolyLines = True Then
            'Check if the curve is a polyline...
            Dim mypline As IOnPolylineCurve = OnPolylineCurve.ConstCast(myCurve)
            If mypline IsNot Nothing Then
              m_rh_plines.Add(mypline)
            End If
          End If

          'Check if the curve is an arc....
          Dim myArc As IOnArcCurve = OnArcCurve.ConstCast(myCurve)
          If myArc IsNot Nothing Then
            'Is the Arc a complete circle...?
            If myArc.IsCircle Then

              If _s.ImportCircles = True Then
                'Sort circles
                m_rh_circs.Add(myArc)
              End If

            Else

              If _s.ImportArcs = True Then
                'Sort arcs
                m_rh_arcs.Add(myArc)
              End If

            End If
          End If

          'Check if the curve is an NURBS Curve....
          Dim myNrbscrv As OnNurbsCurve = OnNurbsCurve.ConstCast(myCurve)
          If myNrbscrv IsNot Nothing Then



            If myNrbscrv.IsPlanar Then

              If myNrbscrv.IsClosed Then

                If myNrbscrv.IsPeriodic Then
                  If _s.ImportClosedSplines2D Then
                    'Sort periodic Nurbs Curves
                    m_rh_closedspline2d.Add(myNrbscrv)
                  End If

                Else
                  Dim spans As New List(Of OnNurbsCurve)
                  Dim knots() As Double = Nothing
                  myNrbscrv.GetSpanVector(knots)

                  For j As Integer = 0 To myNrbscrv.SpanCount - 1
                    Dim t0 As Double = knots(j)
                    Dim t1 As Double = knots(j + 1)
                    Dim cInterval As New OnInterval(t0, t1)

                    Dim Nrbscrv As OnNurbsCurve = myNrbscrv.NurbsCurve(myNrbscrv, _s.ModelTolerance, cInterval)
                    m_rh_nurbscrv2d.Add(Nrbscrv)
                  Next

                End If

              Else

                If _s.ImportNURBSCurves2D Then
                  'Sort 2D Nurbs
                  m_rh_nurbscrv2d.Add(myNrbscrv)
                End If

              End If

            Else

              If _s.ImportSplines3D Then
                'Sort 3D Nurbs
                m_rh_splinecrv3d.Add(myNrbscrv)
              End If

            End If
          End If

          If _s.ImportPolyCurves = True Then
            'Check if the curve is a polycurve....
            Dim myPlycrv As IOnPolyCurve = OnPolyCurve.ConstCast(myCurve)
            If myPlycrv IsNot Nothing Then
              'get segments
              For j As Integer = 0 To myPlycrv.Count - 1
                Dim s_crv As OnCurve = myPlycrv.SegmentCurve(j)

                'test segments
                'Check if the segment curve is a line...
                Dim s_myLine As IOnLineCurve = OnLineCurve.ConstCast(s_crv)
                If s_myLine IsNot Nothing Then
                  m_rh_lines.Add(s_myLine)
                End If

                'Check if the segment curve is a polyline curve...
                Dim s_mypline As IOnPolylineCurve = OnPolylineCurve.ConstCast(s_crv)
                If s_mypline IsNot Nothing Then
                  m_rh_plines.Add(s_mypline)
                End If

                'Check if the segment curve is an arc....
                Dim s_myArc As IOnArcCurve = OnArcCurve.ConstCast(s_crv)
                If s_myArc IsNot Nothing Then
                  'Is the Arc a complete circle...?
                  If s_myArc.IsCircle Then
                    m_rh_circs.Add(s_myArc)
                  Else
                    m_rh_arcs.Add(s_myArc)
                  End If
                End If

                'Check if the segment curve is an NURBS Curve....
                Dim s_myNrbscrv As OnNurbsCurve = OnNurbsCurve.ConstCast(s_crv)
                If s_myNrbscrv IsNot Nothing Then
                  If s_myNrbscrv.IsPlanar Then
                    m_rh_nurbscrv2d.Add(s_myNrbscrv)
                  Else
                    m_rh_splinecrv3d.Add(s_myNrbscrv)
                  End If
                End If
              Next
            End If

          End If
        End If


        If obj.ObjectType.ToString = "brep_object" Then
          Dim myBRep As IOnBrep = OnBrep.ConstCast(obj)

          'Check if the BRep is a surface...
          If myBRep.IsSurface And myBRep.m_F.Count = 1 Then

            Dim brpFace As OnBrepFace = myBRep.Face(0)
            Dim nrbssrf As OnNurbsSurface = brpFace.NurbsSurface()
            If nrbssrf.CVCount = 4 Then

              'Sort Surfaces
              If _s.ImportCornerSrf = True Then
                m_rh_cornersurfaces.Add(myBRep)
              End If

            Else

              'Sort Corner Surfaces
              If _s.ImportSurfaces = True Then
                m_rh_surfaces.Add(myBRep)
              End If

            End If

          ElseIf myBRep.m_F.Count = 1 And myBRep.Face(0).IsPlanar Then

            If _s.ImportTrimSrf = True Then
              'If planar make a cap form
              m_rh_trimsurfaces.Add(myBRep)
            End If

            ' Else if there is more than one face in the BRep...
          ElseIf myBRep.m_F.Count >= 1 Then

            If _s.ImportPolySrf = True Then
              Dim myFaces As IOnBrepFaceArray = myBRep.m_F
              For j As Integer = 0 To myFaces.Count - 1
                Dim f As OnBrepFace = myBRep.Face(j)
                Dim fb As OnBrep = myBRep.DuplicateFace(j, False)

                'If the face is planar then assign to Planar Surface list
                If f.IsPlanar Then
                  Dim fsrf As IOnBrep = OnBrep.ConstCast(fb)

                  'Sort Trim Surfaces
                  m_rh_trimsurfaces.Add(fsrf)

                  'If the face is a surface then assign to the Surface list
                ElseIf f.BrepForm().IsSurface Then
                  Dim fsrf As IOnBrep = OnBrep.ConstCast(f.BrepForm())

                  Dim brpFace As OnBrepFace = fsrf.Face(0)
                  Dim nrbssrf As OnNurbsSurface = brpFace.NurbsSurface()
                  If nrbssrf.CVCount = 4 Then

                    'Sort Surfaces
                    m_rh_cornersurfaces.Add(fsrf)

                  Else

                    'Sort Corner Surfaces
                    m_rh_surfaces.Add(fsrf)

                  End If
                End If
              Next
            End If

          End If

        End If
      End If
    Next

    'widen scope
    points = m_rh_points
    lines = m_rh_lines
    arcs = m_rh_arcs
    plines = m_rh_plines
    circs = m_rh_circs
    ellipses = m_rh_ellipses
    polycrvs = m_rh_polys
    nurbscrv2d = m_rh_nurbscrv2d
    closedspline2d = m_rh_closedspline2d
    splinecrv3d = m_rh_splinecrv3d

    surfaces = m_rh_surfaces
    cornersurfaces = m_rh_cornersurfaces
    trimsurfaces = m_rh_trimsurfaces
  End Sub

End Class

