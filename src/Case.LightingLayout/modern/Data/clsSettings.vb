Imports System.Linq
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Mechanical
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet
    Private _version As String = ""
    Private _foundMEP As Boolean = False

#Region "Public Properties"

    ''' <summary>
    ''' Rooms
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rooms As List(Of clsRoom)

    ''' <summary>
    ''' Spaces
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Spaces As List(Of clsSpace)

    ''' <summary>
    ''' Lights
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixtureTypes As List(Of clsLight)

    ''' <summary>
    ''' Reference Planes with Names
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RefPlanes As List(Of clsRefPlane)

    ''' <summary>
    ''' Levels by Elevation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Levels As SortedDictionary(Of Double, String)

    ''' <summary>
    ''' Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveUIdoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Active Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.Document
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Active Document and All Linked Docs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AllLinkedDocs As List(Of Document)
      Get
        Dim m_docs As New List(Of Document)
        m_docs.Add(Doc)

        Try
          ' Get All Linked Docs - only if currently loaded in model

          Using col As New FilteredElementCollector(Doc)
            col.OfClass(GetType(RevitLinkInstance))
            For Each x In col.ToList
              Try
                Dim m_link As RevitLinkInstance = TryCast(x, RevitLinkInstance)
                If Not m_link Is Nothing Then

                End If
              Catch
              End Try
            Next
          End Using

        Catch
        End Try

        Return m_docs
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AppVersion As String
      Get
        Return _version
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="eSet"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet

      Try

        ' Levels
        GetLevels()

        ' Rooms
        GetRooms()

        ' Spaces
        GetSpaces()

        If Rooms.Count + Spaces.Count = 0 Then Return
        GetLightTypes()
        GetReferencePlanes()

        ' Setup
        _version = Assembly.GetExecutingAssembly.GetName.Version.ToString

      Catch
      End Try

    End Sub

#Region "Public Members - Ray Tracing"

    ''' <summary>
    ''' Find the closest Wall, Column or Joist in the direction specified from 'point' to 'direction'
    ''' </summary>
    ''' <param name="ptStart">Point to test from</param>
    ''' <param name="v3d">3D View to test from</param>
    ''' <param name="eId">ElementID to avoid, no self intersection</param>
    ''' <returns>Closest intersecting point in test direction</returns>
    Private Function GetIntersectionHeight(ptStart As XYZ, v3d As View3D, eId As ElementId) As Double

      ' Resulting Height
      Dim m_dist As Double = Double.PositiveInfinity
      Dim m_height As Double = 0

      ' Lowest Element MEP?
      Dim m_lowestElement As Element = Nothing

      Try

        ' Element Collector Query
        Dim m_eIDs As IEnumerable(Of ElementId) = From e In New FilteredElementCollector(Doc, v3d.Id) _
              .WhereElementIsNotElementType()
              Select e.Id

        ' Any Results?
        If Not m_eIDs Is Nothing Then

          ' List
          Dim m_eList As New List(Of ElementId)
          m_eList = m_eIDs.ToList

          ' Reference Intersector - Allow Links
          Dim m_refIntDir As New ReferenceIntersector(m_eList, FindReferenceTarget.Element, v3d)
          m_refIntDir.FindReferencesInRevitLinks = True

          ' Directionality for Reference Collections
          Dim m_dir As New XYZ(0, 0, 1)
          Dim m_references As IList(Of ReferenceWithContext) = m_refIntDir.Find(ptStart, m_dir)


          ' Process Each Reference
          For Each r As ReferenceWithContext In m_references

            ' Get the Element from the Reference
            Dim m_ref As Reference = r.GetReference()
            Dim m_elem As Element = Doc.GetElement(m_ref)

            ' Skip Own Element
            If m_elem.Id.Value = eId.Value Then Continue For

            ' Keep the closest matching reference using the proximity parameter to determine closeness
            Dim m_proximity As Double = r.Proximity
            If m_proximity < m_dist Then
              m_dist = m_proximity
              m_height = m_ref.GlobalPoint.Z
              m_lowestElement = m_elem
            End If

          Next

        End If

      Catch
      End Try

      Try

        ' MEP Warnings
        If Not m_lowestElement Is Nothing Then
          If (m_lowestElement.Category.Id.Value = BuiltInCategory.OST_MechanicalEquipment Or _
              m_lowestElement.Category.Id.Value = BuiltInCategory.OST_CableTray Or _
              m_lowestElement.Category.Id.Value = BuiltInCategory.OST_FlexDuctCurves Or _
              m_lowestElement.Category.Id.Value = BuiltInCategory.OST_DuctCurves Or _
              m_lowestElement.Category.Id.Value = BuiltInCategory.OST_FlexPipeCurves Or _
              m_lowestElement.Category.Id.Value = BuiltInCategory.OST_PipeCurves) Then

            ' Default Failure
            m_height = -3.123

            ' Lowest Element MEP?
            If Not _foundMEP Then

              ' Message
              Using d As New form_Fail
                d.ShowDialog()
              End Using

            End If

            ' Found
            _foundMEP = True

          End If

        End If

      Catch
      End Try

      ' Result
      Return m_height

    End Function

    ''' <summary>
    ''' View to use for Ray Tracing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Get3dView() As View3D

      ' Resulting View
      Dim m_v3d As View3D = Nothing

      ' View Name
      Dim m_name As String = "_work - Light Fixture Automation"

      Try

        ' Query
        Dim m_views = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(View3D))
              Let v As View3D = TryCast(e, View3D)
              Where v.IsTemplate = False
              Select v

        ' Find View
        If Not m_views Is Nothing Then
          For Each x As View3D In m_views
            If x.Name.ToLower = m_name.ToLower Then Return x
          Next
        End If

        ' Query View Types
        Dim m_types = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(ViewFamilyType))
              Let vt As ViewFamilyType = TryCast(e, ViewFamilyType)
              Where vt.ViewFamily = ViewFamily.ThreeDimensional
              Select vt

        ' First Item
        If Not m_types Is Nothing Then
          For Each x As ViewFamilyType In m_types

            ' New Subtransaction
            Using t As New SubTransaction(Doc)
              If t.Start = TransactionStatus.Started Then

                Try

                  ' Create the View
                  m_v3d = View3D.CreateIsometric(Doc, x.Id)
                  m_v3d.Name = m_name
                  t.Commit()
                  Exit For

                Catch
                End Try

              End If
            End Using

          Next
        End If

      Catch
      End Try

      ' Result
      Return m_v3d

    End Function

#End Region

#Region "Private Members - Element Collections"

    ''' <summary>
    ''' Get Levels
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetLevels()

      ' Fresh List
      Levels = New SortedDictionary(Of Double, String)

      Try

        ' Query
        Dim m_levels = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(Level))
              Let l As Level = TryCast(e, Level)
              Select l

        ' Process Items
        If Not m_levels Is Nothing Then
          For Each x In m_levels
            Try
              Levels.Add(x.Elevation, x.Name)
            Catch
            End Try
          Next
        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Spaces
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSpaces()

      ' Fresh List
      Spaces = New List(Of clsSpace)

      Try

        ' Get Spaces
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_MEPSpaces)
          For Each x In col.ToElements
            Try
              If TypeOf x Is Space Then
                Dim m_space As Space = TryCast(x, Space)
                If m_space.Area > 0 Then
                  Spaces.Add(New clsSpace(x))
                End If
              End If
            Catch
            End Try
          Next
        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get Lighting Fixtures
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetLightTypes()

      Try

        ' Sorted
        Dim m_sort As New SortedDictionary(Of String, clsLight)

        ' Get the Symbols
        Using col As New FilteredElementCollector(Doc)
          col.WhereElementIsElementType()
          col.OfCategory(BuiltInCategory.OST_LightingFixtures)
          For Each x In col.ToElements

            ' Cast as Helper
            If TypeOf x Is FamilySymbol Then
              Dim m_elem As FamilySymbol = TryCast(x, FamilySymbol)
              If Not m_elem Is Nothing Then
                Dim m_f As New clsLight(m_elem)
                Try
                  m_sort.Add(m_f.DisplayName, m_f)
                Catch
                End Try
              End If
            End If

          Next
        End Using

        ' Fresh List
        FixtureTypes = New List(Of clsLight)
        For Each x In m_sort.Values
          FixtureTypes.Add(x)
        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get Reference Planes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetReferencePlanes()

      Try

        ' Sorted
        Dim m_sort As New SortedDictionary(Of String, clsRefPlane)

        ' Get the Symbols
        Using col As New FilteredElementCollector(Doc)
          col.OfClass(GetType(ReferencePlane))
          For Each x In col.ToElements

            ' Cast as Helper
            If TypeOf x Is ReferencePlane Then
              Dim m_elem As ReferencePlane = TryCast(x, ReferencePlane)
              If Not m_elem Is Nothing Then
                If Not String.IsNullOrEmpty(m_elem.Name) And Not m_elem.Name.ToLower = "reference plane" Then

                  Dim m_f As New clsRefPlane(m_elem)
                  Try
                    m_sort.Add(m_elem.Name, m_f)
                  Catch
                  End Try

                End If
              End If
            End If

          Next
        End Using

        ' Fresh List
        RefPlanes = New List(Of clsRefPlane)
        For Each x In m_sort.Values
          RefPlanes.Add(x)
        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Rooms
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRooms()

      ' Fresh List
      Rooms = New List(Of clsRoom)

      Try

        ' Get Rooms
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_Rooms)
          For Each x In col.ToElements
            Try
              If TypeOf x Is Architecture.Room Then
                Dim m_room As Architecture.Room = TryCast(x, Architecture.Room)
                If m_room.Area > 0 Then
                  Rooms.Add(New clsRoom(x))
                End If
              End If
            Catch
            End Try
          Next
        End Using

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members - Family Placement"

    ''' <summary>
    ''' Place the Fixtures
    ''' </summary>
    ''' <param name="fs"></param>
    ''' <param name="pts"></param>
    ''' <param name="l"></param>
    ''' <param name="mountingHeight"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PlaceFixtures(fs As clsLight,
                                  pts As List(Of XYZ),
                                  l As Level,
                                  rName As String,
                                  mountingHeight As Double)

      ' MEP Fixtures to Delete
      Dim m_deleteIDs As New List(Of ElementId)

      ' Start a New Transaction
      Using t As New Transaction(Doc, "Lighting: " & rName)
        If t.Start = TransactionStatus.Started Then

          Try

            ' Process Each Point
            For Each pt As XYZ In pts

              ' Symbol
              Dim m_famSymbol As FamilySymbol = fs.GetSymbol

              ' Normal
              Dim m_normal As New XYZ(pt.X + 1, pt.Y + 1, 1)

              ' Xyz Point
              Dim m_inspt As New XYZ(pt.X, pt.Y, 0)

              Try


                ' Family Insert
                Dim m_famInst2 As FamilyInstance = Doc.Create.NewFamilyInstance(pt,
                                                                                m_famSymbol,
                                                                                l,
                                                                                [Structure].StructuralType.NonStructural)

                ' Set Both Levels to Placement Level
                m_famInst2.Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(l.Id)
                m_famInst2.Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).Set(l.Id)

                ' Height Overrides
                m_famInst2.Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).Set(mountingHeight)

                ' Upper Offset
                Dim m_upper As Double = mountingHeight + 0.1

                Try

                  ' Get Next Highest Level
                  For Each x In Levels
                    If x.Key > l.Elevation Then
                      m_upper = x.Key
                      Exit For
                    End If
                  Next

                  ' Calculate Upper Attachment Point (Ray Tracing)
                  Dim m_ht As Double = mountingHeight + 0.01

                  ' View
                  Dim m_view As View3D = Get3dView()
                  If Not m_view Is Nothing Then

                    ' Fixed Point
                    Dim m_ptStart As New XYZ(pt.X, pt.Y, mountingHeight + l.Elevation)
                    m_ht = GetIntersectionHeight(m_ptStart, m_view, m_famInst2.Id)

                    ' Default Failure?
                    If m_ht = -3.123 Then

                      Try

                        ' Delete the Fixture
                        m_deleteIDs.Add(m_famInst2.Id)

                      Catch
                      End Try

                    End If

                  End If

                  ' Height Test
                  If m_ht > mountingHeight And m_ht < m_upper Then
                    m_upper = m_ht
                  End If

                  ' Final Height
                  m_famInst2.Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).Set(m_upper)

                Catch
                  m_famInst2.Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).Set(m_upper)
                End Try

              Catch
              End Try

            Next

            ' Any Bad Ones?
            If m_deleteIDs.Count > 0 Then
              Doc.Delete(m_deleteIDs)
            End If

            ' Success
            t.Commit()
            Return True

          Catch
          End Try

        End If
      End Using

      ' Failure
      Return False

    End Function

#End Region

  End Class
End Namespace