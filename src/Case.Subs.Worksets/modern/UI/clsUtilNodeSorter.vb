Imports System.Windows.Forms

Namespace UI

  ''' <summary>
  ''' A helper class for sorting tree nodes alphabetically
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsUtilNodeSorter
    Implements IComparer

    ''' <summary>
    ''' Compare the strings, regardless of length
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Compare(ByVal x As Object, ByVal y As Object) _
      As Integer Implements IComparer.Compare
      Dim tx As TreeNode = CType(x, TreeNode)
      Dim ty As TreeNode = CType(y, TreeNode)
      Return String.Compare(tx.Text, ty.Text)
    End Function

  End Class
End Namespace