Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsExternalWalls

    Public Property WallObject As Wall
    Public Property WallFamilyType As String
    Public Property WallFacing As String
    Public Property WallAngle As String
    Public Property WallLength As String
    Public Property WallLevel As String

    Private _wallDirection As XYZ
    Private _testParam As String = ""
    Private _northDirectionAngle As Double = 0

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Wall)

      ' Widen Scope
      WallObject = e

      Try
        Dim m_para As New clsPara(WallObject.LookupParameter("Comments"))
        _TestParam = m_para.Value
      Catch
      End Try

      ' Family and Type
      Try
        Dim m_fam As String = WallObject.Name
        Dim m_type As WallType = WallObject.Document.GetElement(WallObject.GetTypeId)
        Dim m_typeName As String = m_type.Name
      Catch
      End Try

      ' Facing and Angle
      Try
        GetWallDirection()
        GetFacingDirection()
      Catch
      End Try

      ' Length
      Try
        Dim m_p As New clsPara(WallObject.LookupParameter("Length"))
        If Not m_p Is Nothing Then
          WallLength = m_p.Value
        Else
          WallLength = "n/a"
        End If
      Catch
        WallLength = "n/a"
      End Try

      ' Level
      Try
        WallLevel = WallObject.Document.GetElement(WallObject.LevelId).Name
      Catch
        WallLevel = "n/a"
      End Try

    End Sub

    Private Enum angleDir
      isNorth
      isSouth
      isEast
      isWest
    End Enum

    ''' <summary>
    ''' Calculate Angle Value
    ''' </summary>
    ''' <param name="p_a"></param>
    ''' <param name="p_dir"></param>
    ''' <param name="isToLeft"></param>
    ''' <remarks></remarks>
    Private Sub GetAngleValue(p_a As angleDir, p_dir As Decimal, isToLeft As Boolean)

      ' Clockwise from North as 0
      WallAngle = 0

      ' Angle as Degrees
      Dim m_degrees As Double = p_dir * Math.PI

      Select Case p_a
        Case angleDir.isNorth
          If isToLeft = True Then
            WallAngle = 360 - m_degrees
          Else
            WallAngle = 0 + m_degrees
          End If
        Case angleDir.isSouth
          If isToLeft = True Then
            WallAngle = 180 - m_degrees
          Else
            WallAngle = 180 + m_degrees
          End If
        Case angleDir.isEast
          If isToLeft = True Then
            WallAngle = 90 - m_degrees
          Else
            WallAngle = 90 + m_degrees
          End If
        Case angleDir.isWest
          If isToLeft = True Then
            WallAngle = 270 - m_degrees
          Else
            WallAngle = 270 + m_degrees
          End If
      End Select

    End Sub

#Region "Wall Angle and Direction"

    ''' <summary>
    ''' Get the Wall Direction
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetWallDirection()

      ' Analyze the Location Curve
      Dim m_locCurve As LocationCurve = TryCast(WallObject.Location, LocationCurve)
      Dim m_ExtDirection As XYZ = XYZ.BasisZ

      ' Validate the Curve
      If m_locCurve IsNot Nothing Then

        ' Get the Curve
        Dim m_curve As Curve = m_locCurve.Curve

        ' Write("Wall line endpoints: ", curve)
        Dim m_dir As XYZ = XYZ.BasisX

        If TypeOf m_curve Is Line Then

          ' Obtains the tangent vector of the wall.
          m_dir = m_curve.ComputeDerivatives(0, True).BasisX.Normalize

        Else

          ' An assumption for non-linear walls: "tangent vector" is the direction from the start of the wall to the end.
          m_dir = (m_curve.GetEndPoint(1) - m_curve.GetEndPoint(0)).Normalize

        End If

        ' Calculate the normal vector via cross product.
        m_ExtDirection = XYZ.BasisZ.CrossProduct(m_dir)

        ' Flipped walls need to reverse the calculated direction
        If WallObject.Flipped Then
          m_ExtDirection = -m_ExtDirection
        End If

      End If

      ' Return the Value
      _WallDirection = m_ExtDirection

    End Sub

    ''' <summary>
    ''' Get Facing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetFacingDirection()

      ' Corner Angles for Basis
      Dim m_angleToNorth As Double = _WallDirection.AngleTo(XYZ.BasisY)
      Dim m_angleToSouth As Double = _WallDirection.AngleTo(-XYZ.BasisY)
      Dim m_angleToEast As Double = _WallDirection.AngleTo(XYZ.BasisX)
      Dim m_angleToWest As Double = _WallDirection.AngleTo(-XYZ.BasisX)

      ' Is To Left?
      Dim m_isToLeft As Boolean = True

      If Math.Abs(m_angleToNorth) < Math.PI / 4 Then

        ' North
        WallFacing = "N"

        ' North East
        If Math.Abs(m_angleToEast) < Math.PI / 3 Then
          ' East
          WallFacing = "NE"
          m_isToLeft = False
        End If

        ' North West
        If Math.Abs(m_angleToWest) < Math.PI / 3 Then
          ' West
          WallFacing = "NW"
        End If

        ' Angle Only
        If Math.Abs(m_angleToWest) < Math.PI / 2 Then
          ' Angle is West
          Dim m_todo As String = ""
        End If
        If Math.Abs(m_angleToEast) < Math.PI / 2 Then
          ' Angle is East
          Dim m_todo As String = ""
          m_isToLeft = False
        End If

        ' Angle
        GetAngleValue(angleDir.isNorth, m_angleToNorth, m_isToLeft)

      End If

      ' South
      If Math.Abs(m_angleToSouth) < Math.PI / 4 Then

        ' South
        WallFacing = "S"

        ' East
        If Math.Abs(m_angleToEast) < Math.PI / 3 Then
          ' East
          WallFacing = "SE"
          Dim m_todo As String = ""
        End If

        ' West
        If Math.Abs(m_angleToWest) < Math.PI / 3 Then
          ' West
          WallFacing = "SW"
          Dim m_todo As String = ""
          m_isToLeft = False
        End If

        ' Angle Only
        If Math.Abs(m_angleToWest) < Math.PI / 2 Then
          ' Angle is West
          Dim m_todo As String = ""
          m_isToLeft = False
        End If
        If Math.Abs(m_angleToEast) < Math.PI / 2 Then
          ' Angle is East
          Dim m_todo As String = ""
        End If

        ' Angle
        GetAngleValue(angleDir.isSouth, m_angleToSouth, m_isToLeft)

      End If

      ' East
      If Math.Abs(m_angleToEast) < Math.PI / 4 Then

        ' East
        WallFacing = "E"

        If Math.Abs(m_angleToNorth) < Math.PI / 3 Then
          ' North
          WallFacing = "NE"
        End If

        If Math.Abs(m_angleToSouth) < Math.PI / 3 Then
          ' South
          WallFacing = "SE"
          m_isToLeft = False
        End If

        ' Angle Only
        If Math.Abs(m_angleToNorth) < Math.PI / 2 Then
          ' Angle is North
          Dim m_todo As String = ""
        End If
        If Math.Abs(m_angleToSouth) < Math.PI / 2 Then
          ' Angle is South
          Dim m_todo As String = ""
          m_isToLeft = False
        End If

        ' Angle
        GetAngleValue(angleDir.isEast, m_angleToEast, m_isToLeft)

      End If

      ' West
      If Math.Abs(m_angleToWest) < Math.PI / 4 Then

        ' West
        WallFacing = "W"

        If Math.Abs(m_angleToNorth) < Math.PI / 3 Then
          ' North
          WallFacing = "NW"
          m_isToLeft = False
        End If

        If Math.Abs(m_angleToSouth) < Math.PI / 3 Then
          ' South
          WallFacing = "SW"
        End If

        ' Angle Only
        If Math.Abs(m_angleToNorth) < Math.PI / 2 Then
          ' Angle is North
          Dim m_todo As String = ""
          m_isToLeft = False
        End If
        If Math.Abs(m_angleToSouth) < Math.PI / 2 Then
          ' Angle is South
          Dim m_todo As String = ""
        End If

        ' Angle
        GetAngleValue(angleDir.isWest, m_angleToWest, m_isToLeft)

      End If

    End Sub

    '' '' ''' <summary>
    '' '' ''' South Facing
    '' '' ''' </summary>
    '' '' ''' <remarks></remarks>
    '' ''Private Sub GetDirectionAngleEast()
    '' ''    Dim m_angleToEast As Double = m_WallDirection.AngleTo(XYZ.BasisX)
    '' ''    WallFacing = Math.Abs(m_angleToEast)
    '' ''    ' Return Math.Abs(m_angleToEast) ' < Math.PI / 4
    '' ''End Sub

    '' '' ''' <summary>
    '' '' ''' South Facing
    '' '' ''' </summary>
    '' '' ''' <returns></returns>
    '' '' ''' <remarks></remarks>
    '' ''Private Function IsSouthFacing() As Boolean
    '' ''    Dim angleToSouth As Double = m_WallDirection.AngleTo(-XYZ.BasisY)
    '' ''    Return Math.Abs(angleToSouth) < Math.PI / 4
    '' ''End Function

#End Region

  End Class
End Namespace