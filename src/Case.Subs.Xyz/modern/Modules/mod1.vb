Imports Autodesk.Revit.DB

Module mod1

  Public Function OrientCs(ByVal pln As Plane, ByVal coord As Autodesk.Revit.DB.XYZ) As Autodesk.Revit.DB.XYZ

    'Plane components
    'CS Plane Origin
    Dim pln_origin As Autodesk.Revit.DB.XYZ = pln.Origin
    'CS Plane Axis
    Dim pln_xaxis As Autodesk.Revit.DB.XYZ = pln.XVec
    Dim pln_yaxis As Autodesk.Revit.DB.XYZ = pln.YVec

    'CS Plane Origin Components
    Dim plnX As Double = pln_origin.X
    Dim plnY As Double = pln_origin.Y
    Dim plnZ As Double = pln_origin.Z

    'Insertion point coordinates
    Dim coordX As Double = coord.X
    Dim coordY As Double = coord.Y
    Dim coordZ As Double = coord.Z

    'Get relative Z Distance
    Dim relativeZ As Double = coordZ - plnZ

    'Get Insertion point projected on CS XY
    Dim projXYZ As New Autodesk.Revit.DB.XYZ(coordX, coordY, plnZ)

    'Get XY distance to insertion point
    Dim xyDistance As Double = projXYZ.DistanceTo(pln_origin)
    'Get rotation of insertion point calcuated by X-Axis
    Dim xRotation As Double = projXYZ.AngleTo(pln_xaxis)

    Dim almostCoord As New Autodesk.Revit.DB.XYZ(xyDistance, 0, relativeZ)

    'Not used to transformations in Revit...bug fix the following

    Dim xForm As Transform = Transform.CreateRotationAtPoint(Autodesk.Revit.DB.XYZ.BasisX,
                                                             xRotation,
                                                             Autodesk.Revit.DB.XYZ.Zero)
    Dim m_finalXyz As Autodesk.Revit.DB.XYZ = xForm.OfPoint(almostCoord)


    Return m_finalXyz

  End Function


End Module