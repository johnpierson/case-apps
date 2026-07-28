Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Imports RMA.OpenNURBS

''' <summary>
''' Some form utilities
''' </summary>
''' <remarks></remarks>
Public Class clsFormUtil

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
  Public Sub New(ByVal p_doc As Document, ByVal p_app As Autodesk.Revit.UI.UIApplication)

    'Widen Scope
    _doc = p_doc
    _app = p_app

  End Sub

  ''' <summary>
  ''' Creates a plane from 3 points.
  ''' </summary>
  ''' <param name="xyz1"></param>
  ''' <param name="xyz2"></param>
  ''' <param name="xyz3"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CreateSketchPlane(ByVal xyz1 As XYZ, ByVal xyz2 As XYZ, ByVal xyz3 As XYZ) As SketchPlane
    'define sketch plane
    Dim worldOrigin As XYZ = XYZ.Zero
    Dim plnLine1 As Line = Line.CreateBound(xyz1, xyz2)
    Dim plnLine2 As Line = Line.CreateBound(xyz1, xyz3)

    Dim crvArray As New CurveArray()
    crvArray.Append(plnLine1)
    crvArray.Append(plnLine2)

    Dim myPlane As Plane = PlaneFromCurve(plnLine1)

    'create the SketchPlanes and ModelCurves
    Dim skplane As SketchPlane
    If _doc.IsFamilyDocument Then
      skplane = SketchPlane.Create(_doc, myPlane)
    Else
      skplane = SketchPlane.Create(_doc, myPlane)
    End If

    Return skplane

  End Function

End Class