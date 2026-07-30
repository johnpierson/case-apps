Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

Public Class SplitContainerEx
  Inherits SplitContainer

  ''' <summary>
  ''' Determines the thickness of the splitter
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  <DefaultValue(GetType(Integer), "5"), Description("Determines the thickness of the splitter.")> _
  Public Overridable Shadows Property SplitterWidth() As Integer
    Get
      Return MyBase.SplitterWidth
    End Get
    Set(ByVal value As Integer)
      If value < 5 Then value = 5

      MyBase.SplitterWidth = value
    End Set
  End Property

  ''' <summary>
  ''' On Paint Override for Split Container
  ''' </summary>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)
    'paint the three dots
    Dim points(2) As Point
    Dim pointRect = Rectangle.Empty

    'calculate the position of the points
    If Orientation = Orientation.Horizontal Then
      points(0) = New Point((MyBase.Width \ 2), SplitterDistance + (SplitterWidth \ 2))
      points(1) = New Point(points(0).X - 10, points(0).Y)
      points(2) = New Point(points(2).X + 10, points(0).Y)
      pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 25, 5)
    Else
      points(0) = New Point(SplitterDistance + (SplitterWidth \ 2), (MyBase.Height \ 2))
      points(1) = New Point(points(0).X, points(0).Y - 10)
      points(2) = New Point(points(0).X, points(0).Y + 10)
      pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 5, 25)
    End If

    e.Graphics.FillRectangle(Brushes.Gray, pointRect)

    For Each p In points
      p.Offset(-1, -1)
      e.Graphics.FillEllipse(Brushes.Black, New Rectangle(p, New Size(3, 3)))
    Next
  End Sub

End Class