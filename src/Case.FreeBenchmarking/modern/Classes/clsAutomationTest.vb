Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Public Class clsAutomationTest

  Private _s As clsSettings

  ''' <summary>
  ''' Automation Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)

    ' Widen Scope
    _s = s

  End Sub

#Region "Sequence 1 Members"

  Public Function Test1() As Boolean

    ' Fail Test
    Dim FailCaught As Boolean = False

    ' Basic1 Elements for Initial Copy
    Dim m_elements1 As New List(Of ElementId)

    Try

      ' Levels
      Using t As New Transaction(_s.Doc, "Benchmarking - Level Creation")
        If t.Start Then
          Try

            ' Step the levels
            For i = 20 To 2520 Step 10

              Try

                Dim m_level As Level = Level.Create(_s.Doc,i)
                m_level.Name = Environment.UserName & " Level " & i.ToString
                _s.GetLevels()

              Catch ex As Exception

              End Try

            Next

            ' Success
            t.Commit()
            _s.Doc.Regenerate()

          Catch ex As Exception

            ' Failure
            FailCaught = True

          End Try
        End If
      End Using

      ' Rooms
      Using t As New Transaction(_s.Doc, "Benchmarking - Room Creation")
        If t.Start Then
          Try

            ' Wall Curve
            Dim pt1 As New XYZ(0, 0, 0)
            Dim pt2 As New XYZ(0, 9, 0)
            Dim pt3 As New XYZ(9, 9, 0)
            Dim pt4 As New XYZ(9, 0, 0)
            Dim m_line1 As Line = Line.CreateBound(pt1, pt2)
            Dim m_line2 As Line = Line.CreateBound(pt2, pt3)
            Dim m_line3 As Line = Line.CreateBound(pt3, pt4)
            Dim m_line4 As Line = Line.CreateBound(pt4, pt1)

            ' First Wall type
            Dim m_wt As WallType = _s.FirstWallType

            ' Lowest level
            Dim m_level As Level = Nothing
            For Each l As Level In _s.Levels.Values
              m_level = l
              Exit For
            Next

            If Not m_wt Is Nothing And Not m_level Is Nothing Then

              ' Create the Walls and Room
              Dim w As Wall = Wall.Create(_s.Doc,m_line1,m_wt.Id,m_level.Id,10.0,m_level.Elevation,False,false)
              m_elements1.Add(w.Id)
              w = Wall.Create(_s.Doc,m_line2,m_wt.Id,m_level.Id,10.0,m_level.Elevation,False,false)
              m_elements1.Add(w.Id)
              w = Wall.Create(_s.Doc,m_line3,m_wt.Id,m_level.Id,10.0,m_level.Elevation,False,false)
              m_elements1.Add(w.Id)
              w = Wall.Create(_s.Doc,m_line4,m_wt.Id,m_level.Id,10.0,m_level.Elevation,False,false)
              m_elements1.Add(w.Id)

              ' Place Room
              Dim m_room As Architecture.Room = _s.Doc.Create.NewRoom(m_level, New UV(5, 5))
              m_room.Name = Environment.UserName & " HDR Room"
              m_elements1.Add(m_room.Id)

              ' Succes
              t.Commit()

            End If

          Catch ex As Exception

            ' Failure
            FailCaught = True

          End Try
        End If
      End Using

      ' Array
      Using t As New Transaction(_s.Doc, "Benchmarking - Room Array")
        If t.Start Then

          ' Ilist
          Dim m_i As IList(Of ElementId)
          m_i = m_elements1.ToList

          Try

            ' Array
            For i = 10 To 500 Step 10
              Dim m_eids As New List(Of ElementId)
              m_eids = ElementTransformUtils.CopyElements(_s.Doc, m_i, New XYZ(0, i, 0))
              m_elements1.AddRange(m_eids)
            Next

            ' Commit
            t.Commit()

          Catch ex As Exception

            ' Failure
            FailCaught = True

          End Try

        End If
      End Using

      ' Group This Row of Rooms and Walls
      Dim m_g As Group = Nothing
      Using t As New Transaction(_s.Doc, "Benchmarking - Grouping Stuff")
        If t.Start Then

          Try

            ' Group the Collection
            Dim m_eset As New ElementSet
            For Each x In m_elements1
              m_eset.Insert(_s.Doc.GetElement(x))
            Next

            ' Group It
            m_g = _s.Doc.Create.NewGroup(m_eset)

            ' Commit
            t.Commit()

          Catch ex As Exception

          End Try

        End If
      End Using

      ' Copy Group
      Using t As New Transaction(_s.Doc, "Benchmarking - Copying Group")
        If t.Start Then

          ' Copy the group several times
          For i = 10 To 500 Step 10

            Try

              Dim m_eids As New List(Of ElementId)
              ElementTransformUtils.CopyElement(_s.Doc, m_g.Id, New XYZ(i, 0, 0))
              m_elements1.AddRange(m_eids)

            Catch ex As Exception

            End Try

          Next

          ' Commit
          t.Commit()

        End If
      End Using

      ' Copy to Each Level
      Using t As New Transaction(_s.Doc, "Benchmarking - Copying All to Each Level")
        If t.Start Then

          Try

            ' Iterate Each level
            For Each l As Level In _s.Levels.Values

            Next


            ' Commit
            t.Commit()

          Catch ex As Exception

          End Try
        End If
      End Using

      ' Success
      Return True

    Catch ex As Exception

      ' Failure
      Return False

    End Try

  End Function

#End Region

#Region "Sequence 2 Members"

  Public Function Test2() As Boolean

    Try

      ' Success
      Return True

    Catch ex As Exception

      ' Failure
      Return False

    End Try

  End Function

#End Region

#Region "Sequence 3 Members"

  Public Function Test3() As Boolean

    Try

      ' Success
      Return True

    Catch ex As Exception

      ' Failure
      Return False

    End Try

  End Function

#End Region

#Region "Sequence 4 Members"

  Public Function Test4() As Boolean

    Try

      ' Success
      Return True

    Catch ex As Exception

      ' Failure
      Return False

    End Try

  End Function

#End Region

#Region "Sequence 5 Members"

  Public Function Test5() As Boolean

    Try

      ' Success
      Return True

    Catch ex As Exception

      ' Failure
      Return False

    End Try

  End Function

#End Region

End Class