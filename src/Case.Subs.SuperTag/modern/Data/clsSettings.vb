Imports System.Linq
Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _c As ExternalCommandData

#Region "Public Properties"

    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _c.Application.ActiveUIDocument.Document
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property Version As String
      Get
        Try
          Return " v" & Assembly.GetExecutingAssembly.GetName.Version.ToString
        Catch
        End Try
        Return ""
      End Get
    End Property
    Public Property Categories As SortedDictionary(Of String, Category)
    Public Property FamilyTypeNames As SortedDictionary(Of String, clsElement)
    Public Property TagFamilies As SortedDictionary(Of String, clsElement)
    Public Property Rooms As List(Of clsElement)
    Public Property Areas As List(Of clsElement)
    Public Property Spaces As List(Of clsElement)
    Public Property Views As List(Of clsView)

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData)

      ' Widen Scope
      _c = c

    End Sub

#Region "Public Members"

    ''' <summary>
    ''' Get spatial elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetAreasSpacesRooms()

      Try

        ' Fresh Lists
        Rooms = New List(Of clsElement)
        Spaces = New List(Of clsElement)
        Areas = New List(Of clsElement)

        ' Rooms
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_Rooms)
          For Each x In col.ToElements
            Try
              Rooms.Add(New clsElement(x))
            Catch
            End Try
          Next
        End Using

        ' Spaces
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_MEPSpaces)
          col.WhereElementIsNotElementType()
          For Each x In col.ToElements
            Try
              Spaces.Add(New clsElement(x))
            Catch
            End Try
          Next
        End Using

        ' Areas
        Using col As New FilteredElementCollector(Doc)
          col.OfCategory(BuiltInCategory.OST_Areas)
          col.WhereElementIsNotElementType()
          For Each x In col.ToElements
            Try
              Areas.Add(New clsElement(x))
            Catch
            End Try
          Next
        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get All Symbol Names
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSymbolNames()

      ' Fresh List
      FamilyTypeNames = New SortedDictionary(Of String, clsElement)

      Try

        ' Collect all Type Elements
        Using col As New FilteredElementCollector(Doc)
          col.WhereElementIsElementType()

          ' Iterate over each type element
          For Each x In col.ToElements
            Try
              If x.Category Is Nothing Then Continue For
              If Categories.ContainsKey(x.Category.Name) Then
                Dim m_f As New clsElement(x)
                FamilyTypeNames.Add(m_f.ElementFullName, m_f)
              End If
            Catch
            End Try
          Next

        End Using

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' List of Tag Families
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetTags()

      Try

        ' Fresh List
        TagFamilies = New SortedDictionary(Of String, clsElement)

        ' Get Tag Elements
        Dim m_tags = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(FamilySymbol))
              Let t = TryCast(e, FamilySymbol)
              Where t.Category.Name.ToLower.Contains("tag")
              Select t

        ' Get Anything?
        If Not m_tags Is Nothing Then
          For Each x In m_tags.ToList
            Dim m_f As New clsElement(x)
            TagFamilies.Add(m_f.ElementFullName, m_f)
          Next
        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get a Sorted List of Categories in Use
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ScanCategories() As SortedDictionary(Of String, Category)

      ' The Data
      Categories = New SortedDictionary(Of String, Category)

      For Each x As Category In Doc.Settings.Categories

        ' Avoid Tags... and some categories
        If x.Name.ToLower.StartsWith("text") Then Continue For
        If x.Name.ToLower.StartsWith("title") Then Continue For
        If x.Name.ToLower.StartsWith("line") Then Continue For
        If x.Name.ToLower.StartsWith("detail") Then Continue For
        If x.Name.ToLower.StartsWith("spot") Then Continue For
        If x.Name.ToLower.StartsWith("reveal") Then Continue For
        If x.Name.ToLower.StartsWith("run") Then Continue For
        If x.Name.ToLower.StartsWith("grid") Then Continue For
        If x.Name.ToLower.StartsWith("location") Then Continue For
        If x.Name.ToLower.StartsWith("mass ") Then Continue For
        If x.Name.ToLower.StartsWith("stair ") Then Continue For
        If x.Name.ToLower.StartsWith("wall ") Then Continue For
        If x.Name.ToLower.StartsWith("wire ") Then Continue For
        If x.Name.ToLower.StartsWith("volt") Then Continue For
        If x.Name.ToLower.StartsWith("patt") Then Continue For
        If x.Name.ToLower.StartsWith("view") Then Continue For
        If x.Name.ToLower.StartsWith("contour") Then Continue For
        If x.Name.ToLower.StartsWith("filled") Then Continue For
        If x.Name.ToLower.StartsWith("elev") Then Continue For
        If x.Name.ToLower.StartsWith("adapt") Then Continue For
        If x.Name.ToLower.StartsWith("anal") Then Continue For
        If x.Name.ToLower.StartsWith("boundary") Then Continue For
        If x.Name.ToLower.StartsWith("brace") Then Continue For
        If x.Name.ToLower.StartsWith("camera") Then Continue For
        If x.Name.ToLower.StartsWith("dim") Then Continue For
        If x.Name.ToLower.StartsWith("match") Then Continue For
        If x.Name.ToLower.StartsWith("mask") Then Continue For
        If x.Name.ToLower.StartsWith("line") Then Continue For
        If x.Name.ToLower.StartsWith("import") Then Continue For
        If x.Name.ToLower.StartsWith("pipe seg") Then Continue For
        If x.Name.ToLower.StartsWith("plan re") Then Continue For
        If x.Name.ToLower.StartsWith("point") Then Continue For
        If x.Name.ToLower.StartsWith("proj") Then Continue For
        If x.Name.ToLower.StartsWith("raster") Then Continue For
        If x.Name.ToLower.StartsWith("refer") Then Continue For
        If x.Name.ToLower.StartsWith("render") Then Continue For
        If x.Name.ToLower.StartsWith("scope") Then Continue For
        If x.Name.ToLower.StartsWith("section") Then Continue For
        If x.Name.ToLower.StartsWith("sheet") Then Continue For
        If x.Name.ToLower.StartsWith("site") Then Continue For

        If x.Name.ToLower.Contains("tags") Then Continue For
        If x.Name.ToLower.Contains("annotation") Then Continue For
        If x.Name.ToLower.Contains("analytical") Then Continue For
        If x.Name.ToLower.Contains("callout") Then Continue For
        If x.Name.ToLower.Contains("legend") Then Continue For
        If x.Name.ToLower.EndsWith("systems") And Not x.Name.ToLower.StartsWith("furn") Then Continue For
        If x.Name.ToLower.Contains("mark") Then Continue For
        If x.Name.ToLower.Contains("head") Then Continue For
        If x.Name.ToLower.Contains("finish") Then Continue For
        If x.Name.ToLower.Contains("material") Then Continue For
        If x.Name.ToLower.Contains("schedule") Then Continue For
        If x.Name.ToLower.Contains("fluids") Then Continue For
        If x.Name.ToLower.Contains("symbols") Then Continue For
        If x.Name.ToLower.Contains("color fill") Then Continue For
        If x.Name.ToLower.Contains(" loads") Then Continue For
        If x.Name.ToLower.Contains("structural load") Then Continue For

        ' Add to the List
        Categories.Add(x.Name, x)

      Next

      ' Return the Results
      Return Categories

    End Function

    ''' <summary>
    ''' Get all qualifying views
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetViews()

      ' Fresh List
      Views = New List(Of clsView)

      ' Collect the Views
      Using col As New FilteredElementCollector(Doc)
        col.OfCategory(BuiltInCategory.OST_Views)
        For Each x In col.ToElements
          Dim m_v As View = TryCast(x, View)
          If Not m_v Is Nothing Then

            ' No View Templates
            If m_v.IsTemplate = True Then Continue For

            If m_v.ViewType = ViewType.ColumnSchedule Then Continue For
            If m_v.ViewType = ViewType.CostReport Then Continue For
            If m_v.ViewType = ViewType.Legend Then Continue For
            If m_v.ViewType = ViewType.LoadsReport Then Continue For
            If m_v.ViewType = ViewType.PanelSchedule Then Continue For
            #If REVIT2027
            If m_v.ViewType = ViewType.PressureLossReport Then Continue For
            #Else
            If m_v.ViewType = ViewType.PresureLossReport Then Continue For
            #End If
            If m_v.ViewType = ViewType.Rendering Then Continue For
            If m_v.ViewType = ViewType.Report Then Continue For
            If m_v.ViewType = ViewType.Schedule Then Continue For
            If m_v.ViewType = ViewType.Undefined Then Continue For
            If m_v.ViewType = ViewType.Walkthrough Then Continue For
            If m_v.ViewType = ViewType.ThreeD Then Continue For

            Try
              Views.Add(New clsView(m_v))
            Catch
            End Try

          End If
        Next
      End Using

    End Sub

#End Region

  End Class
End Namespace