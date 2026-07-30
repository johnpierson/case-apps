Imports System.Drawing
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRoom

    Private _elem As Element

#Region "Public Properties"

    Public Property FailureMessage As String
    Public Property RoomMatchProcessed As Boolean
    Public Property RoomDbId As Integer
    Public Property RoomInsertionPoint As XYZ
    Public Property RoomCentroid As XYZ
    Public Property FirstSyncGuid As String
    Public ReadOnly Property RoomElement As Architecture.Room
      Get
        Try
          Return TryCast(_elem, Architecture.Room)
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property RoomLevelElevation As Double
      Get
        Try
          Dim m_level As Level = _elem.Document.GetElement(_elem.LevelId)
          Return m_level.Elevation
        Catch
        End Try
        Return 0
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="elem"></param>
    ''' <remarks></remarks>
    Public Sub New(elem As Element)

      ' Widen Scope
      _elem = elem

      RoomMatchProcessed = False
      FailureMessage = ""

      Try
        RoomCentroid = GetCentroidPoint()
      Catch
        RoomCentroid = New XYZ(0, 0, 0)
      End Try

      Try
        ' Insertion Point - For Placements
        If Not RoomElement Is Nothing Then
          Dim m_location As Location = RoomElement.Location
          If Not m_location Is Nothing Then
            RoomInsertionPoint = DirectCast(m_location, LocationPoint).Point
          End If
        End If
      Catch
      End Try

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Get the XYZ from an Element
    ''' </summary>
    ''' <returns>XYZ of Instance Element</returns>
    ''' <remarks></remarks>
    Private Function GetCentroidPoint() As XYZ

      Try

        ' The Points List
        Dim m_pts As New List(Of PointF)

        ' The Z Height
        Dim m_z As Double = 0

        ' Work with the Boundary - 2021
        Dim m_opt As New SpatialElementBoundaryOptions
        Dim m_bsaa = RoomElement.GetBoundarySegments(m_opt)

        ' Segment Array at Floor Level
        For Each bsa As List(Of BoundarySegment) In m_bsaa
          For Each bs As BoundarySegment In bsa
            Dim m_c As Curve = bs.GetCurve()

            ' First Endpoint
            Dim m_endPoint1 As XYZ = m_c.GetEndPoint(0)
            Dim m_pointF1 As New PointF(m_endPoint1(0), m_endPoint1(1))
            m_pts.Add(m_pointF1)

            ' Second Endpoint
            Dim m_endPoint2 As XYZ = m_c.GetEndPoint(1)
            Dim m_pointF2 As New PointF(m_endPoint2(0), m_endPoint2(1))
            m_pts.Add(m_pointF2)

            ' The Height
            m_z = m_endPoint1(2)

          Next

        Next

        ' Return the 2D Centroid
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, RoomElement.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        Dim p As New XYZ(Math.Round(m_2Dcentroid.X, 1),
                         Math.Round(m_2Dcentroid.Y, 1),
                         Math.Round(m_z, 1))

        Return p

      Catch
      End Try

      ' Failure
      Return Nothing

    End Function

    ''' <summary>
    ''' Find 2D Centroid
    ''' </summary>
    ''' <param name="pts">Collection of Points Describing the Polygon</param>
    ''' <param name="rmArea">The Area of the Polygon</param>
    ''' <returns>2D Point (Pointf)</returns>
    ''' <remarks>This Function Kicks Ass</remarks>
    Private Function GetCentroid(ByVal pts() As PointF, rmArea As Single) As PointF

      ' Add the First PT to the End of the Array (full circulation)
      ReDim Preserve pts(pts.Length)
      pts(pts.Length - 1) = New PointF(pts(0).X, pts(0).Y)

      ' Get the Centroid
      Dim m_x As Single = 0
      Dim m_y As Single = 0
      Dim m_sf As Single

      ' This is Where the Magic Happens
      For i As Integer = 0 To pts.Length - 2
        m_sf = pts(i).X * pts(i + 1).Y - pts(i + 1).X * pts(i).Y
        m_x += (pts(i).X + pts(i + 1).X) * m_sf
        m_y += (pts(i).Y + pts(i + 1).Y) * m_sf
      Next i

      ' Divide by 6X the Are of the Polygon
      m_x /= (6 * rmArea)
      m_y /= (6 * rmArea)

      ' If Negative, Reorient Signs for Counterclockwise Orientations
      '' ''If X < 0 Then
      '' ''    X = -X
      '' ''    Y = -Y
      '' ''End If

      ' This is the Final Result
      Return New PointF(m_x, m_y)

    End Function

#End Region

#Region "Friend Members"

    ''' <summary>
    ''' Get the location of a linked instance relative to current model
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetTransformInsertion(link As RevitLinkInstance) As XYZ

      Try

        ' Transform a Point for a Linked Instance
        Dim m_transform As Transform = link.GetTransform()
        Return m_transform.OfPoint(RoomCentroid)

      Catch
      End Try

      ' Fail
      Return Nothing

    End Function

#End Region

  End Class
End Namespace