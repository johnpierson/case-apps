Imports System.Drawing
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture

Namespace Data

  Public Class clsRoom

    Private _room As Room
    Private _uvCentroid As UV
    Private _maxU As Double = -99999999
    Private _minU As Double = 99999999
    Private _maxV As Double = -99999999
    Private _minV As Double = 99999999

#Region "Public Properties"

    ''' <summary>
    ''' Room Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmName As String
      Get
        Try
          Return _room.Parameter(BuiltInParameter.ROOM_NAME).AsString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Room Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmNumber As String
      Get
        Try
          Return _room.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Room Level Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmLevel As String
      Get
        Try
          Return _room.Level.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Room Area
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmArea As Double
      Get
        Try
          Return _room.Area
        Catch
        End Try
        Return 0
      End Get
    End Property

    ''' <summary>
    ''' Centroid Point
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property CentroidPoint As UV
      Get
        Return _uvCentroid
      End Get
    End Property

    ''' <summary>
    ''' Room Width
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RoomWidth As Double
      Get
        Return _maxU - _minU
      End Get
    End Property

    ''' <summary>
    ''' Room Length
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RoomLength As Double
      Get
        Return _maxV - _minV
      End Get
    End Property

    ''' <summary>
    ''' Room Width
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RoomU As Double
      Get
        Return _minU + (0.5 * (_maxU - _minU))
      End Get
    End Property

    ''' <summary>
    ''' Room Length
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property RoomV As Double
      Get
        Return _minV + (0.5 * (_maxV - _minV))
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Room Helper
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Public Sub New(r As Room)

      ' Widen Scope
      _room = r

      ' Setup
      ExtractBoundaryPointsFromRoom()

    End Sub

    ''' <summary>
    ''' Get the Room
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRoom() As Room
      Return _room
    End Function

#Region "Private Members"

    ''' <summary>
    ''' Extract a List of 2D Points from a Room's Boundary
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExtractBoundaryPointsFromRoom()

      Try

        ' The Points List
        Dim m_pts As New System.Collections.Generic.List(Of PointF)

        ' Work with the Boundary
        Dim m_opt As New SpatialElementBoundaryOptions

        Dim m_bsaa As IList(Of IList(Of Autodesk.Revit.DB.BoundarySegment)) = _room.GetBoundarySegments(m_opt)

        ' Segment Array at Floor Level
        Dim m_isFirst As Boolean = True
        For Each bsa As IList(Of Autodesk.Revit.DB.BoundarySegment) In m_bsaa
          Try

            For Each bs As Autodesk.Revit.DB.BoundarySegment In bsa

              Dim m_c As Curve = bs.GetCurve()

              ' First Endpoint
              Dim m_endPoint1 As XYZ = m_c.GetEndPoint(0)
              Dim m_pointF1 As New PointF(m_endPoint1(0), m_endPoint1(1))
              m_pts.Add(m_pointF1)

              ' Second Endpoint
              Dim m_endPoint2 As XYZ = m_c.GetEndPoint(1)
              Dim m_pointF2 As New PointF(m_endPoint2(0), m_endPoint2(1))
              m_pts.Add(m_pointF2)

              ' Ranges
              If m_endPoint1.X < _minU Then _minU = m_endPoint1.X
              If m_endPoint1.X > _maxU Then _maxU = m_endPoint1.X
              If m_endPoint1.Y < _minV Then _minV = m_endPoint1.Y
              If m_endPoint1.Y > _maxV Then _maxV = m_endPoint1.Y
              If m_endPoint2.X < _minU Then _minU = m_endPoint2.X
              If m_endPoint2.X > _maxU Then _maxU = m_endPoint2.X
              If m_endPoint2.Y < _minV Then _minV = m_endPoint2.Y
              If m_endPoint2.Y > _maxV Then _maxV = m_endPoint2.Y

            Next
          Catch
          End Try

        Next

        ' Return the 2D Centroid
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, _room.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        _uvCentroid = New UV(m_2Dcentroid.X, m_2Dcentroid.Y)

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

        ' Divide by 6X the Area of the Polygon
        X /= (6 * p_rmArea)
        Y /= (6 * p_rmArea)

        ' This is the Final Result
        Return New PointF(X, Y)

      Catch
      End Try

    End Function

#End Region

  End Class
End Namespace