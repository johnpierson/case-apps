Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Reflection
Imports System.Data
Imports System.IO

''' <summary>
''' Form Visibility Enum
''' </summary>
''' <remarks></remarks>
Public Enum formViz
  isProcessing
  isStandBy
End Enum

''' <summary>
''' Location Kind
''' </summary>
''' <remarks></remarks>
Public Enum LocationKind
  isNamed
  isProject
  isShared
End Enum

''' <summary>
''' Parameter value storage format
''' </summary>
''' <remarks></remarks>
Public Enum paramFormat
  isString
  isInteger
  isYesNo
  isElementID
  isDouble
  isAll
End Enum

Public Class clsSettings

  Private _msg As String
  Private _eset As ElementSet

#Region "Public Properties"

  Public Property CmdData As ExternalCommandData
  Public Property ConfigFile As clsConfiguration
  Public ReadOnly Property AppVersion As String
    Get
      Return " v" & Assembly.GetExecutingAssembly.GetName.Version.ToString
    End Get
  End Property
  Public Property SharedBasePoint As Autodesk.Revit.DB.XYZ
  Public Property PointsCollection As List(Of clsReportingData)
  Public Property PointsCollectionDatatable As DataTable
  Public Property Categories As SortedDictionary(Of String, Category)
  Public Property Levels As List(Of clsLevel)
  Public Property DesignOptionNames As SortedDictionary(Of String, clsDesignOption)
  Public Property FamilyTypeNames As SortedDictionary(Of String, clsFamily)
  Public Property TagFamilies As SortedDictionary(Of String, clsFamily)
  Public ReadOnly Property LocationCurrent As ProjectLocation
    Get
      Try
        Return Doc.ActiveProjectLocation
      Catch ex As Exception
        Return Nothing
      End Try
    End Get
  End Property
  Public Property NamedLocations As SortedDictionary(Of String, ProjectLocation)
  Public Property LocationTransforms As SortedDictionary(Of String, Transform)
  Public ReadOnly Property Doc As Document
    Get
      Try
        Return CmdData.Application.ActiveUIDocument.Document
      Catch
        Return Nothing
      End Try
    End Get
  End Property

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="c">The ExternalCommandData Object</param>
  ''' <param name="m">Failure Message</param>
  ''' <param name="e">Elements Argument for Command</param>
  ''' <remarks></remarks>
  Public Sub New(c As ExternalCommandData,
                 ByRef m As String,
                 e As ElementSet)

    ' Widen Scope
    CmdData = c
    _msg = m
    _eset = e

    ' Configuration File
    ConfigFile = New clsConfiguration(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) & "\CASE.PNEZD.cfg")

    ' Setup
    PointsCollection = New List(Of clsReportingData)

    ' Shared Base Point
    Using col As New FilteredElementCollector(Doc)
      col.OfCategory(BuiltInCategory.OST_ProjectBasePoint)
      For Each x In col.ToElements

        Try

          Dim m_Elev As Double = 0
          Dim m_East As Double = 0
          Dim m_North As Double = 0

          For Each p As Parameter In x.Parameters
            If p.Definition.Name.ToLower = "elev" Then
              m_Elev = p.AsDouble
            End If
            If p.Definition.Name.ToLower = "e/w" Then
              m_East = p.AsDouble
            End If
            If p.Definition.Name.ToLower = "n/s" Then
              m_North = p.AsDouble
            End If
          Next

          ' Setting
          SharedBasePoint = New Autodesk.Revit.DB.XYZ(m_East, m_North, m_Elev)

          Exit For

        Catch
        End Try

      Next
    End Using

  End Sub

#Region "Public Members - Scan Base Elements"

  ''' <summary>
  ''' Get a Sorted List of Categories in Use
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function ScanCategories() As SortedDictionary(Of String, Category)

    ' The Data
    Dim m_c As New SortedDictionary(Of String, Category)

    ' Filter for Instances
    Using col As New FilteredElementCollector(Doc)
      col.WhereElementIsElementType()

      ' Iterate the Elements
      For Each e In col.ToElements

        ' Category Present
        If Not e.Category Is Nothing Then

          ' Avoid Categories:
          If e.Category.Name.ToLower.Contains("elevation") Then Continue For
          If e.Category.Name.ToLower.Contains("section") Then Continue For
          If e.Category.Name.ToLower.Contains("title") Then Continue For
          If e.Category.Name.ToLower.Contains("view") Then Continue For
          If e.Category.Name.ToLower.Contains("detail") Then Continue For
          If e.Category.Name.ToLower.Contains("annotation") Then Continue For
          If e.Category.Name.ToLower.Contains("heads") Then Continue For
          If e.Category.Name.ToLower.Contains("raster") Then Continue For
          If e.Category.Name.ToLower.Contains("image") Then Continue For
          If e.Category.Name.ToLower.Contains("tags") Then Continue For

          ' Category Name
          If Not m_c.ContainsKey(e.Category.Name) Then

            ' Add to the List
            m_c.Add(e.Category.Name, e.Category)

          End If

        End If

      Next

    End Using

    ' Set Categories to Result
    Me.Categories = m_c

    ' Return the Results
    Return m_c

  End Function

  ''' <summary>
  ''' Get the Levels
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetLevels()

    ' Fresh Lists
    Dim m_levelsByElevation As New SortedDictionary(Of Double, Level)
    Levels = New List(Of clsLevel)

    Try
      Using col As New FilteredElementCollector(Doc)
        col.WhereElementIsNotElementType()
        col.OfClass(GetType(Level))
        For Each x As Level In col.ToElements
          m_levelsByElevation.Add(x.Elevation, x)
        Next
      End Using
    Catch
    End Try

    ' Add in Order
    Levels.Add(New clsLevel(""))
    For Each x As Level In m_levelsByElevation.Values
      Levels.Add(New clsLevel(x))
    Next

  End Sub

  ''' <summary>
  ''' Get the Levels
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetDesignOptions()

    ' Fresh list
    DesignOptionNames = New SortedDictionary(Of String, clsDesignOption)

    Try
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(DesignOption))
        DesignOptionNames.Add("", New clsDesignOption(""))
        For Each x As DesignOption In col.ToElements
          Dim m_do As New clsDesignOption(x)
          DesignOptionNames.Add(m_do.DisplayString, m_do)
        Next
      End Using
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Get All Symbol Names
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetSymbolNames(Optional isFamPlacement As Boolean = False)

    ' Fresh List
    FamilyTypeNames = New SortedDictionary(Of String, clsFamily)

    Try

      If isFamPlacement = False Then
        FamilyTypeNames.Add("$<All>", New clsFamily(""))
      End If

      ' Collect all Type Elements
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(FamilySymbol))

        ' Iterate over each type element
        For Each x As Element In col.ToElements

          Try
            If Not TypeOf x Is FamilySymbol Then Continue For
            If x.Category Is Nothing Then Continue For
          Catch
          End Try

          Try

            If isFamPlacement = True Then
              If x.Category.Name.ToLower.StartsWith("door") Then Continue For
              If x.Category.Name.ToLower.StartsWith("spot") Then Continue For
              If x.Category.Name.ToLower.StartsWith("mass ") Then Continue For
              If x.Category.Name.ToLower.Contains("panel") Then Continue For

              If x.Category.Name.ToLower.Contains("level") Then Continue For
              If x.Category.Name.ToLower.Contains("view") Then Continue For
              If x.Category.Name.ToLower.Contains("tags") Then Continue For
              If x.Category.Name.ToLower.Contains("grid") Then Continue For
              If x.Category.Name.ToLower.Contains("callout") Then Continue For
              If x.Category.Name.ToLower.Contains("baluster") Then Continue For
              If x.Category.Name.ToLower.Contains("ceiling") Then Continue For
              If x.Category.Name.ToLower.Contains("legend") Then Continue For
              If x.Category.Name.ToLower.Contains("conduit") Then Continue For
              If x.Category.Name.ToLower.Contains("construction") Then Continue For
              If x.Category.Name.ToLower.Contains("wire") Then Continue For
              If x.Category.Name.ToLower.Contains("duct") Then Continue For
              If x.Category.Name.ToLower.Contains("pipe") Then Continue For
              If x.Category.Name.ToLower.Contains("system") Then Continue For
              'If x.Category.Name.ToLower.Contains("panel") Then Continue For
              If x.Category.Name.ToLower.Contains("wall") Then Continue For
              If x.Category.Name.ToLower.Contains("detail") Then Continue For
              'If x.Category.Name.ToLower.Contains("door") Then Continue For
              If x.Category.Name.ToLower.Contains("marks") Then Continue For
              If x.Category.Name.ToLower.Contains("floor") Then Continue For
              If x.Category.Name.ToLower.Contains("fluid") Then Continue For
              If x.Category.Name.ToLower.Contains("annotation") Then Continue For
              If x.Category.Name.ToLower.Contains("finish") Then Continue For
              If x.Category.Name.ToLower.Contains("roof") Then Continue For
              If x.Category.Name.ToLower.Contains("opening") Then Continue For
              'If x.Category.Name.ToLower.Contains("window") Then Continue For
              If x.Category.Name.ToLower.Contains("pattern") Then Continue For
              If x.Category.Name.ToLower.Contains("profile") Then Continue For
              If x.Category.Name.ToLower.Contains("railing") Then Continue For
              If x.Category.Name.ToLower.Contains("reveal") Then Continue For
              If x.Category.Name.ToLower.Contains("revision") Then Continue For
              If x.Category.Name.ToLower.Contains("slab") Then Continue For
              If x.Category.Name.ToLower.Contains("stair") Then Continue For
              If x.Category.Name.ToLower.Contains("title") Then Continue For
              If x.Category.Name.ToLower.Contains("voltage") Then Continue For
              If x.Category.Name.ToLower.Contains("ramp") Then Continue For
              If x.Category.Name.ToLower.Contains("fascia") Then Continue For
              If x.Category.Name.ToLower.Contains("location") Then Continue For
              If x.Category.Name.ToLower.Contains("gutter") Then Continue For

              Dim m_f As New clsFamily(x)
              FamilyTypeNames.Add(m_f.ElementFullName, m_f)

            Else

              If Me.Categories.ContainsKey(x.Category.Name) Then
                Dim m_f As New clsFamily(x)
                FamilyTypeNames.Add(m_f.ElementFullName, m_f)
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
  ''' List of Tag Families
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetTags()

    ' Fresh List
    TagFamilies = New SortedDictionary(Of String, clsFamily)

    Try

      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(FamilySymbol))
        For Each x As Element In col.ToElements
          Try
            If x.Category.Name.ToLower.Contains("tag") Then
              Dim m_f As New clsFamily(x)
              TagFamilies.Add(m_f.ElementFullName, m_f)
            End If
          Catch
          End Try
        Next
      End Using
    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Get All Named Locations
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetNamedLocations()

    ' Fresh List
    NamedLocations = New SortedDictionary(Of String, ProjectLocation)
    LocationTransforms = New SortedDictionary(Of String, Transform)

    Try
      ' Get the List
      For Each x As ProjectLocation In Doc.ProjectLocations
        ' The Location
        NamedLocations.Add(x.Name, x)
        ' The Transform
        LocationTransforms.Add(x.Name, x.GetTotalTransform)
      Next
    Catch
    End Try

  End Sub

#End Region

#Region "Public Members"

  ''' <summary>
  ''' Create Datatable from Points Data
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub UpdateDatatableFromPoints()

    ' Fresh Table
    Dim m_DataTable As New DataTable("PointsExport")

    ' Assemble the Header
    m_DataTable.Columns.Add("ElementID", GetType(String))
    m_DataTable.Columns.Add("P", GetType(String))
    m_DataTable.Columns.Add("N", GetType(String))
    m_DataTable.Columns.Add("E", GetType(String))
    m_DataTable.Columns.Add("Z", GetType(String))
    m_DataTable.Columns.Add("D", GetType(String))
    m_DataTable.Columns.Add("Family Name", GetType(String))
    m_DataTable.Columns.Add("Family Type Name", GetType(String))
    m_DataTable.Columns.Add("Category", GetType(String))
    m_DataTable.Columns.Add("Level", GetType(String))
    m_DataTable.Columns.Add("Design Option Set", GetType(String))
    m_DataTable.Columns.Add("Design Option", GetType(String))
    m_DataTable.Columns.Add("Param1", GetType(String))
    m_DataTable.Columns.Add("Param2", GetType(String))
    m_DataTable.Columns.Add("Param3", GetType(String))
    m_DataTable.Columns.Add("Param4", GetType(String))
    m_DataTable.Columns.Add("Param5", GetType(String))

    ' Iterate the Items and Apply as Rows
    Dim m_row As System.Data.DataRow
    For Each x As clsReportingData In PointsCollection

      ' Item Data
      m_row = m_DataTable.NewRow()
      m_row(0) = x.eID
      m_row(1) = x.P
      m_row(2) = x.N
      m_row(3) = x.E
      m_row(4) = x.Z
      m_row(5) = x.D
      m_row(6) = x.FamilyName
      m_row(7) = x.FamilyType
      m_row(8) = x.Category
      m_row(9) = x.LevelName
      m_row(10) = x.DesignOptionSetName
      m_row(11) = x.DesignOptionName
      m_row(12) = x.Param1
      m_row(13) = x.Param2
      m_row(14) = x.Param3
      m_row(15) = x.Param4
      m_row(16) = x.Param5

      ' Add the Row
      m_DataTable.Rows.Add(m_row)

    Next

    ' Apply the Data
    PointsCollectionDatatable = m_DataTable

  End Sub

#End Region

End Class