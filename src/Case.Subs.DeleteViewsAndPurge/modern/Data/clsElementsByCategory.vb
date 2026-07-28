Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' Captures all type and instance elements from a given category
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsElementsByCategory

    Private _doc As Document

#Region "Public Properties"

    Public Property Category As Category

    Public ReadOnly Property CatName As String
      Get
        Try
          Return Category.Name
        Catch
          Return "{n/a}"
        End Try
      End Get
    End Property

    Public Property TypeElements As List(Of Element)
    Public Property InstanceElements As List(Of Element)
    Public Property PurgeItems As SortedDictionary(Of String, ElementId)

#End Region

    ''' <summary>
    ''' Class Constructor
    ''' </summary>
    ''' <param name="cat">Category Name</param>
    ''' <param name="d">Document</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cat As Category, ByVal d As Document)

      ' Widen Scope
      Category = cat
      _doc = d

      ' New Lists
      TypeElements = New List(Of Element)
      InstanceElements = New List(Of Element)

      ' Setup
      Setup()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      ' Type Elements
      Using col As New FilteredElementCollector(_doc)
        col.OfCategory(Category.Id.Value)
        col.WhereElementIsElementType()
        TypeElements = col.ToElements
      End Using

      ' Instance Elements
      Using col As New FilteredElementCollector(_doc)
        col.OfCategory(Category.Id.Value)
        col.WhereElementIsNotElementType()
        InstanceElements = col.ToElements
      End Using

      ' Find the Purge Items
      If TypeElements.Count > 0 Then
        FindPurgeItems()
      End If

    End Sub

    ''' <summary>
    ''' Find the items that can be purged
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FindPurgeItems()

      ' The List of Purge Items
      PurgeItems = New SortedDictionary(Of String, ElementId)

      Dim m_usedItems As New SortedDictionary(Of String, ElementId)

      For Each x As Element In InstanceElements

        Try
          ' The Type Id
          Dim m_typeid As ElementId = x.GetTypeId

          If Not m_usedItems.ContainsKey(m_typeid.ToString) Then
            m_usedItems.Add(m_typeid.ToString, m_typeid)
          End If

        Catch
        End Try

      Next

      ' Filter the List - Types
      For Each x As Element In TypeElements

        Try

          ' Add the Item if Not Used
          If Not m_usedItems.ContainsKey(x.Id.ToString) Then
            PurgeItems.Add(x.Id.ToString, x.Id)
          End If

        Catch
        End Try

      Next

    End Sub

#End Region

  End Class
End Namespace