Imports System.Drawing
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Mechanical

Namespace Data

  Public Class clsSpace

    Private _Space As Space
    Private _uvCentroid As UV
    Private _maxU As Double = -99999999
    Private _minU As Double = 99999999
    Private _maxV As Double = -99999999
    Private _minV As Double = 99999999

#Region "Public Properties"

    ''' <summary>
    ''' Space Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmName As String
      Get
        Try
          Return _Space.Parameter(BuiltInParameter.SPACE_ASSOC_ROOM_NAME).AsString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Space Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmNumber As String
      Get
        Try
          Return _Space.Parameter(BuiltInParameter.SPACE_ASSOC_ROOM_NUMBER).AsString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Space Level Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmLevel As String
      Get
        Try
          Return _Space.Level.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Space Area
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RmArea As Double
      Get
        Try
          Return _Space.Area
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
    ''' Space Width
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property SpaceWidth As Double
      Get
        Return _maxU - _minU
      End Get
    End Property

    ''' <summary>
    ''' Space Length
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property SpaceLength As Double
      Get
        Return _maxV - _minV
      End Get
    End Property

    ''' <summary>
    ''' Space Width
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property SpaceU As Double
      Get
        Return _minU + (0.5 * (_maxU - _minU))
      End Get
    End Property

    ''' <summary>
    ''' Space Length
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property SpaceV As Double
      Get
        Return _minV + (0.5 * (_maxV - _minV))
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Space Helper
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub New(s As Space)

      ' Widen Scope
      _Space = s

      ' Setup
      ExtractBoundaryPointsFromSpace()

    End Sub

    ''' <summary>
    ''' Get the Space
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpace() As Space
      Return _Space
    End Function

#Region "Private Members"

    ''' <summary>
    ''' Extract a List of 2D Points from a Space's Boundary
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExtractBoundaryPointsFromSpace()

      Try

        ' The Points List
        Dim m_pts As New System.Collections.Generic.List(Of PointF)

        ' Work with the Boundary
        Dim m_opt As New SpatialElementBoundaryOptions

        Dim m_bsaa As IList(Of IList(Of Autodesk.Revit.DB.BoundarySegment)) = _Space.GetBoundarySegments(m_opt)

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
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, _Space.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        _uvCentroid = New UV(m_2Dcentroid.X, m_2Dcentroid.Y)

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Find 2D Centroid
    ''' </summary>
    ''' <param name="pts">Collection of Points Describing the Polygon</param>
    ''' <param name="a">The Area of the Polygon</param>
    ''' <returns>2D Point (Pointf)</returns>
    ''' <remarks>This Function Kicks Ass</remarks>
    Private Function GetCentroid(ByVal pts() As PointF, a As Single) As PointF

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
        X /= (6 * a)
        Y /= (6 * a)

        ' This is the Final Result
        Return New PointF(X, Y)

      Catch
      End Try

    End Function

#End Region

  End Class
End Namespace