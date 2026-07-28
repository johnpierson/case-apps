Imports System.Drawing
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture

Namespace Data

  Public Class clsRoomTagInfo

    Private _r As Room
    Private _ip As XYZ

#Region "Public Properties"

    ''' <summary>
    ''' Room Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
      Get
        Try
          Dim m_s As String = _r.Parameter(BuiltInParameter.ROOM_NAME).AsString
          If String.IsNullOrEmpty(m_s) Then
            Return "-"
          Else
            Return m_s
          End If
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Room Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Number As String
      Get
        Try
          Dim m_s As String = _r.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
          If String.IsNullOrEmpty(m_s) Then
            Return "-"
          Else
            Return m_s
          End If
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Room Department
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Dept As String
      Get
        Try
          Dim m_s As String = _r.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString
          If String.IsNullOrEmpty(m_s) Then
            Return "-"
          Else
            Return m_s
          End If
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Centroid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property InsertionPoint As XYZ
      Get
        Return _ip
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Public Sub New(r As Room)

      ' Widen Scope
      _r = r

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Setup the Class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSetup()

      ' Find the Centroid of Room
      ExtractBoundaryPointsFromRoom(_r)

    End Sub

    ''' <summary>
    ''' Extract a List of 2D Points from a Room's Boundary
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Private Sub ExtractBoundaryPointsFromRoom(r As Room)

      Try

        ' The Points List
        Dim m_pts As New System.Collections.Generic.List(Of PointF)

        ' The Z Height
        Dim m_z As Double = 0

        ' Work with the Boundary
        Dim m_opt As New SpatialElementBoundaryOptions

        Dim m_bsaa As IList(Of IList(Of Autodesk.Revit.DB.BoundarySegment)) = r.GetBoundarySegments(m_opt) ' r.Boundary

        '' ''Dim m_bsaa As Autodesk.Revit.DB.Architecture.BoundarySegmentArrayArray = r.GetBoundarySegments(m_opt) ' r.Boundary

        ' Segment Array at Floor Level
        For Each bsa As IList(Of Autodesk.Revit.DB.BoundarySegment) In m_bsaa
          Try

            For Each bs As Autodesk.Revit.DB.BoundarySegment In bsa

              Dim m_c As Curve = bs.GetCurve()

              ' First Endpoint
              Dim m_EndPoint1 As XYZ = m_c.GetEndPoint(0)
              Dim m_PointF1 As New PointF(m_EndPoint1(0), m_EndPoint1(1))
              m_pts.Add(m_PointF1)

              ' Second Endpoint
              Dim m_EndPoint2 As XYZ = m_c.GetEndPoint(1)
              Dim m_PointF2 As New PointF(m_EndPoint2(0), m_EndPoint2(1))
              m_pts.Add(m_PointF2)

              ' The Height
              m_z = m_EndPoint1(2)

            Next
          Catch
          End Try

        Next

        ' Return the 2D Centroid
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, _r.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        _ip = New XYZ(m_2Dcentroid.X, m_2Dcentroid.Y, m_z)

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Find 2D Centroid
    ''' </summary>
    ''' <param name="pts">Collection of Points Describing the Polygon</param>
    ''' <param name="p_rmArea">The Area of the Polygon</param>
    ''' <returns>2D Point (Pointf)</returns>
    ''' <remarks>This Function Kicks Ass</remarks>
    Private Function GetCentroid(ByVal pts() As PointF, p_rmArea As Single) As PointF

      Try

        ' Add the First PT to the End of the Array (full circulation)
        ReDim Preserve pts(pts.Length)
        pts(pts.Length - 1) = New PointF(pts(0).X, pts(0).Y)

        ' Get the Centroid
        Dim X As Single = 0
        Dim Y As Single = 0
        Dim m_sf As Single

        ' This is Where the Magic Happens
        For i As Integer = 0 To pts.Length - 2
          m_sf = pts(i).X * pts(i + 1).Y - pts(i + 1).X * pts(i).Y
          X += (pts(i).X + pts(i + 1).X) * m_sf
          Y += (pts(i).Y + pts(i + 1).Y) * m_sf
        Next i

        ' Divide by 6X the Are of the Polygon
        X /= (6 * p_rmArea)
        Y /= (6 * p_rmArea)

        ' If Negative, Reorient Signs for Counterclockwise Orientations
        '' ''If X < 0 Then
        '' ''    X = -X
        '' ''    Y = -Y
        '' ''End If

        ' This is the Final Result
        Return New PointF(X, Y)

      Catch
      End Try

    End Function

#Region "Public Members"

    ''' <summary>
    ''' Place a 3D Tag on the Room
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Function PlaceTagElement(s As FamilySymbol) As Boolean

      ' Start a New Transaction
      Using t As New Transaction(_r.Document, "3D Room Tag Placement: " & Me.Name & ", " & Me.Number)
        If t.Start() Then      
          Try
              ' Check if family symbol is active, if not activate it. Since Revit 2014.
              If not s.IsActive Then 
                  s.Activate()
              End If

            ' Place the Element
            Dim m_inst As FamilyInstance = _r.Document.Create.NewFamilyInstance _
                  (InsertionPoint, s, [Structure].StructuralType.NonStructural)

            '' ''Try

            '' ''  ' Move it to Room Level Elevation
            '' ''  Dim m_offset As Parameter = m_inst.Parameter("Offset")
            '' ''  Dim m_para As New clsPara(m_offset)
            '' ''  m_para.Value = InsertionPoint.Z

            '' ''Catch
            '' ''End Try

            ' Populate the Room Name and Number Parameters
            Dim m_paraName As Parameter = m_inst.LookupParameter("3dRmName")
            Dim m_paraNumber As Parameter = m_inst.LookupParameter("3dRmNumber")
            Dim m_paraDept As Parameter = m_inst.LookupParameter("3dRmDept")

            ' Convert the Para Data
            Dim m_paraNa As New clsPara(m_paraName)
            m_paraNa.Value = Me.Name
            Dim m_paraNo As New clsPara(m_paraNumber)
            m_paraNo.Value = Me.Number
            Dim m_paraDp As New clsPara(m_paraDept)
            m_paraDp.Value = Me.Dept

            ' Success
            t.Commit()
            Return True

          Catch

            ' Failure
            t.RollBack()

          End Try

        End If

      End Using

      ' Failure
      Return False

    End Function

#End Region

  End Class
End Namespace
