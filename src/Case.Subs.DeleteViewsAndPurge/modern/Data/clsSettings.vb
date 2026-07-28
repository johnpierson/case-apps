Imports System.Linq
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

#Region "Public Properties"

    ''' <summary>
    ''' Sheets
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sheets As SortableBindingList(Of clsSheets)

    ''' <summary>
    ''' View Types
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewTypes As Dictionary(Of Integer, clsViewType)

    ''' <summary>
    ''' Views
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Views As SortableBindingList(Of clsViews)

    ''' <summary>
    ''' Links
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Links As SortableBindingList(Of clsLinks)

    ''' <summary>
    ''' Active Doc
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

#End Region

    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData)

      ' Widen Scope
      _cmd = c

      ' Fresh Lists
      Links = New SortableBindingList(Of clsLinks)
      Sheets = New SortableBindingList(Of clsSheets)
      Views = New SortableBindingList(Of clsViews)
      ViewTypes = New Dictionary(Of Integer, clsViewType)

    End Sub

#Region "Friend Members"

    ''' <summary>
    ''' Return a List of Views
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetViews()

      ' Fresh List
      Views = New SortableBindingList(Of clsViews)
      ViewTypes = New Dictionary(Of Integer, clsViewType)

      Try

        ' LINQ Query - Get the View Types
        Dim m_linq = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(ViewFamilyType))
              Let vt = TryCast(e, ViewFamilyType)
              Select vt

        ' Process to Helpers
        For Each x In m_linq
          Try
            ViewTypes.Add(x.Id.Value, New clsViewType(x))
          Catch
          End Try
        Next

      Catch
      End Try

      Try

        ' LINQ Query - Get the Views
        Dim m_linq = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(View))
              Let v = TryCast(e, View)
              Where v.ViewType <> ViewType.Internal And
                    v.ViewType <> ViewType.Undefined And
                    v.ViewType <> ViewType.ProjectBrowser And
                    v.ViewType <> ViewType.SystemBrowser
              Select v

        ' Process to Helpers
        For Each x In m_linq
          Try
            Dim m_view As New clsViews(x)
            Try
              Dim m_int As Integer = m_view.GetViewFamilyTypeId.Value
              If ViewTypes.ContainsKey(m_int) Then
                ViewTypes(m_int).Count += 1
              End If
            Catch
            End Try
            Views.Add(m_view)
          Catch
          End Try
        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Return a List of Sheets
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetSheets()

      ' Fresh List
      Sheets = New SortableBindingList(Of clsSheets)

      Try

        ' LINQ Query
        Dim m_linq = From s In New FilteredElementCollector(Doc) _
              .OfClass(GetType(ViewSheet))

        ' Process to Helpers
        For Each x In m_linq
          If Doc.ActiveView.Id.Value = x.Id.Value Then Continue For
          Try
            Sheets.Add(New clsSheets(x))
          Catch
          End Try
        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get Revit Links
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetLinks()

      ' Fresh List
      Links = New SortableBindingList(Of clsLinks)

      Try

        ' LINQ Query
        Dim m_linq = From l In New FilteredElementCollector(Doc) _
              .OfCategory(BuiltInCategory.OST_RvtLinks) _
              .WhereElementIsElementType

        ' Process to Helpers
        For Each x In m_linq
          Try
            Links.Add(New clsLinks(x))
          Catch
          End Try
        Next

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace