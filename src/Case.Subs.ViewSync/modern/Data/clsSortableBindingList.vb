Imports System.ComponentModel
Imports System.Reflection

Namespace Data

  ''' <summary>
  ''' From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  ''' Converted to VB and slightly modified for general use by Don Rudder
  ''' </summary>
  Public Class clsSortableBindingList(Of T)

    Inherits BindingList(Of T)

    Private ReadOnly _comparers As Dictionary(Of Type, clsPropertyComparer(Of T))
    Private _isSorted As Boolean
    Private _listSortDirection As ListSortDirection
    Private _propertyDescriptor As PropertyDescriptor

    Public Sub New()
      MyBase.New(New List(Of T))
      _comparers = New Dictionary(Of Type, clsPropertyComparer(Of T))
    End Sub

    Public Sub New(ByVal enumeration As IEnumerable(Of T))
      MyBase.New(New List(Of T)(enumeration))
      _comparers = New Dictionary(Of Type, clsPropertyComparer(Of T))
    End Sub

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
      Get
        Return _isSorted
      End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
      Get
        Return _propertyDescriptor
      End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
      Get
        Return _listSortDirection
      End Get
    End Property

    Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides Sub ApplySortCore(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
      Dim m_itemsList As List(Of T) = DirectCast(Items, List(Of T))
      Dim m_propertyType As Type = [property].PropertyType
      Dim comparer As clsPropertyComparer(Of T)
      If Not _comparers.TryGetValue(m_propertyType, comparer) Then
        comparer = New clsPropertyComparer(Of T)([property], direction)
        _comparers.Add(m_propertyType, comparer)
      End If

      comparer.SetPropertyAndDirection([property], direction)
      m_itemsList.Sort(comparer)

      _propertyDescriptor = [property]
      _listSortDirection = direction
      _isSorted = True

      OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Overrides Sub RemoveSortCore()
      _isSorted = False
      _propertyDescriptor = MyBase.SortPropertyCore
      _listSortDirection = MyBase.SortDirectionCore

      OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Overrides Function FindCore(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer
      Dim m_count As Integer = m_count
      For i As Integer = 0 To m_count - 1
        Dim element As T = Me(i)
        If [property].GetValue(element).Equals(key) Then
          Return i
        End If
      Next

      Return -1
    End Function
  End Class

  ''' <summary>
  ''' From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  ''' Converted to VB and slightly modified for general use by Don Rudder, HOK
  ''' </summary>
  Public Class clsPropertyComparer(Of T)

    Implements IComparer(Of T)

    Private ReadOnly _comparer As IComparer
    Private _propertyDescriptor As PropertyDescriptor
    Private _reverse As Integer

    Public Sub New(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
      _propertyDescriptor = [property]
      Dim m_comparerForPropertyType As Type = GetType(Comparer(Of )).MakeGenericType([property].PropertyType)
      _comparer = DirectCast(m_comparerForPropertyType.InvokeMember("Default", BindingFlags.[Static] Or BindingFlags.GetProperty Or BindingFlags.[Public], Nothing, Nothing, Nothing), IComparer)
      SetListSortDirection(direction)
    End Sub

    Public Function Compare(ByVal x As T, ByVal y As T) As Integer Implements IComparer(Of T).Compare
      Return _reverse * _comparer.Compare(_propertyDescriptor.GetValue(x), _propertyDescriptor.GetValue(y))
    End Function

    Private Sub SetPropertyDescriptor(ByVal descriptor As PropertyDescriptor)
      _propertyDescriptor = descriptor
    End Sub

    Private Sub SetListSortDirection(ByVal direction As ListSortDirection)
      _reverse = If(direction = ListSortDirection.Ascending, 1, -1)
    End Sub

    Public Sub SetPropertyAndDirection(ByVal descriptor As PropertyDescriptor, ByVal direction As ListSortDirection)
      SetPropertyDescriptor(descriptor)
      SetListSortDirection(direction)
    End Sub

  End Class
End Namespace