Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection

Namespace Data

  ''' <summary>
  ''' From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  ''' </summary>
  Public Class PropertyComparer(Of T)
    Implements IComparer(Of T)
    Private ReadOnly comparer As IComparer
    Private propertyDescriptor As PropertyDescriptor
    Private reverse As Integer

    Public Sub New(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
      propertyDescriptor = [property]
      Dim comparerForPropertyType As Type = GetType(Comparer(Of )).MakeGenericType([property].PropertyType)
      comparer = DirectCast(comparerForPropertyType.InvokeMember("Default", BindingFlags.[Static] Or BindingFlags.GetProperty Or BindingFlags.[Public], Nothing, Nothing, Nothing), IComparer)
      SetListSortDirection(direction)
    End Sub

    Public Function Compare(ByVal x As T, ByVal y As T) As Integer Implements IComparer(Of T).Compare
      Return Me.reverse * Me.comparer.Compare(propertyDescriptor.GetValue(x), Me.propertyDescriptor.GetValue(y))
    End Function

    Private Sub SetPropertyDescriptor(ByVal descriptor As PropertyDescriptor)
      propertyDescriptor = descriptor
    End Sub

    Private Sub SetListSortDirection(ByVal direction As ListSortDirection)
      reverse = If(direction = ListSortDirection.Ascending, 1, -1)
    End Sub

    Public Sub SetPropertyAndDirection(ByVal descriptor As PropertyDescriptor, ByVal direction As ListSortDirection)
      SetPropertyDescriptor(descriptor)
      SetListSortDirection(direction)
    End Sub
  End Class
End Namespace