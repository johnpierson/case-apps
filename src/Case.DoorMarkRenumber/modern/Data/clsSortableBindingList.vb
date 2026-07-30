Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Data

  ''' <summary>
  ''' From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  ''' </summary>
  Public Class SortableBindingList(Of T)
    Inherits BindingList(Of T)
    Private ReadOnly comparers As Dictionary(Of Type, PropertyComparer(Of T))
    Private isSorted As Boolean
    Private listSortDirection As ListSortDirection
    Private propertyDescriptor As PropertyDescriptor

    Public Sub New()
      MyBase.New(New List(Of T))
      comparers = New Dictionary(Of Type, PropertyComparer(Of T))
    End Sub

    Public Sub New(ByVal enumeration As IEnumerable(Of T))
      MyBase.New(New List(Of T)(enumeration))
      comparers = New Dictionary(Of Type, PropertyComparer(Of T))
    End Sub

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
      Get
        Return isSorted
      End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
      Get
        Return propertyDescriptor
      End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
      Get
        Return listSortDirection
      End Get
    End Property

    Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides Sub ApplySortCore(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
      Dim itemsList As List(Of T) = DirectCast(Items, List(Of T))

      Dim propertyType As Type = [property].PropertyType
      Dim comparer As PropertyComparer(Of T)
      If Not Me.comparers.TryGetValue(propertyType, comparer) Then
        comparer = New PropertyComparer(Of T)([property], direction)
        comparers.Add(propertyType, comparer)
      End If

      comparer.SetPropertyAndDirection([property], direction)
      itemsList.Sort(comparer)

      propertyDescriptor = [property]
      listSortDirection = direction
      isSorted = True

      OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Overrides Sub RemoveSortCore()
      isSorted = False
      propertyDescriptor = MyBase.SortPropertyCore
      listSortDirection = MyBase.SortDirectionCore

      OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Overrides Function FindCore(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer
      Dim count As Integer = Me.Count
      For i As Integer = 0 To count - 1
        Dim element As T = Me(i)
        If [property].GetValue(element).Equals(key) Then
          Return i
        End If
      Next

      Return -1
    End Function
  End Class
End Namespace