Imports [Case].Subs.Worksets.Data
Imports System.Linq

Namespace Charts
  Module clsModCharts

    ''' <summary>
    ''' Workset Query
    ''' </summary>
    ''' <param name="inst">List of Instances to Query Against</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryWorksets(inst As List(Of clsRvtElemInst)) As SortableBindingList(Of clsChartWorksets)

      ' Values
      Dim m_values As New SortableBindingList(Of clsChartWorksets)

      Try

        ' Query Values
        Dim m_masterInst = From e In inst
                           Group By e.WorksetName Into Group
                           Order By Group.Count Descending

        ' Process Results
        For Each group In m_masterInst

          ' Item
          Dim m_name As String = group.WorksetName
          If String.IsNullOrEmpty(m_name) Then m_name = "n/a"
          Dim m_ws As New clsChartWorksets(m_name, group.Group.Count)

          ' Add the Result
          m_values.Add(m_ws)

        Next

      Catch
      End Try

      ' Return the Values
      Return m_values

    End Function

    ''' <summary>
    ''' Categories
    ''' </summary>
    ''' <param name="inst"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryCategories(inst As List(Of clsRvtElemInst),
                                    type As List(Of clsRvtElemType),
                                    byInstances As Boolean) As SortableBindingList(Of clsChartCategory)

      ' Values
      Dim m_values As New SortableBindingList(Of clsChartCategory)

      ' Temp Dictionary
      Dim m_dict As New Dictionary(Of String, clsChartCategory)

      Try

        ' Query Values
        Dim m_masterInst = From e In inst
                                                          Where e.CategoryName <> "" And e.CategoryName <> "n/a"
                                                          Group By e.CategoryName Into Group
                                                          Order By Group.Count Descending
        Dim m_masterType = From e In type
                                                          Where e.CategoryName <> "" And e.CategoryName <> "n/a"
                                                          Group By e.CategoryName Into Group
                                                          Order By Group.Count Descending

        ' Process Results
        If byInstances = True Then

          ' By Instances
          For Each g In m_masterInst

            ' Dictionary
            m_dict.Add(g.CategoryName, New clsChartCategory(g.CategoryName))
            m_dict(g.CategoryName).Instances = g.Group.Count

          Next
          For Each g In m_masterType
            If Not m_dict.ContainsKey(g.CategoryName) Then
              m_dict.Add(g.CategoryName, New clsChartCategory(g.CategoryName))
            End If
            m_dict(g.CategoryName).Types = g.Group.Count
          Next

        Else

          ' By Types
          For Each g In m_masterType

            ' Dictionary
            m_dict.Add(g.CategoryName, New clsChartCategory(g.CategoryName))
            m_dict(g.CategoryName).Types = g.Group.Count

          Next
          For Each g In m_masterInst
            If Not m_dict.ContainsKey(g.CategoryName) Then
              m_dict.Add(g.CategoryName, New clsChartCategory(g.CategoryName))
            End If
            m_dict(g.CategoryName).Instances = g.Group.Count
          Next

        End If

      Catch
      End Try

      ' Values
      For Each x In m_dict.Values
        m_values.Add(x)
      Next
      Return m_values

    End Function

    ''' <summary>
    ''' Element Kinds
    ''' </summary>
    ''' <param name="inst"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryKinds(inst As List(Of clsRvtElemInst),
                               type As List(Of clsRvtElemType),
                               byInstances As Boolean) As SortableBindingList(Of clsChartKind)

      ' Values
      Dim m_values As New SortableBindingList(Of clsChartKind)

      Try

        ' Query Values
        Dim m_masterInst = From e In inst
                           Group By e.ObjectClass Into Group
                           Order By Group.Count Descending

        Dim m_masterType = From e In type
                           Group By e.ObjectClass Into Group
                           Order By Group.Count Descending

        ' Process Results
        If byInstances = True Then

          ' By Instances
          For Each g In m_masterInst

            ' Kind
            Dim m_kind As New clsChartKind(g.ObjectClass)
            m_kind.Quantity = g.Group.Count
            m_values.Add(m_kind)

          Next

        Else

          ' By Types
          For Each g In m_masterType

            ' Kind
            Dim m_kind As New clsChartKind(g.ObjectClass)
            m_kind.Quantity = g.Group.Count
            m_values.Add(m_kind)

          Next

        End If

      Catch
      End Try

      ' Values
      Return m_values

    End Function

  End Module
End Namespace