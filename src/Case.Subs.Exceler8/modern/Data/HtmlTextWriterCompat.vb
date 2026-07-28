Imports System.IO
Imports System.Text

Namespace Global.System.Web.UI
  ''' <summary>
  ''' Minimal HtmlTextWriter compatibility shim for .NET (System.Web is not available on net8+).
  ''' </summary>
  Public Enum HtmlTextWriterTag
    Div
    Table
    Tr
    Td
    U
    I
    B
  End Enum

  Public Enum HtmlTextWriterAttribute
    Align
    Border
    Colspan
    Rowspan
    Bgcolor
  End Enum

  Public Class HtmlTextWriter
    Implements IDisposable

    Private ReadOnly _writer As TextWriter
    Private ReadOnly _attrs As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly _tagStack As New Stack(Of String)()

    Public Sub New(writer As TextWriter)
      _writer = writer
    End Sub

    Public Sub AddAttribute(attr As HtmlTextWriterAttribute, value As String)
      _attrs(attr.ToString().ToLowerInvariant()) = value
    End Sub

    Public Sub RenderBeginTag(tag As HtmlTextWriterTag)
      Dim name = tag.ToString().ToLowerInvariant()
      Dim sb As New StringBuilder()
      sb.Append("<"c).Append(name)
      For Each kvp In _attrs
        sb.Append(" "c).Append(kvp.Key).Append("="c).Append(""""c).Append(kvp.Value).Append(""""c)
      Next
      sb.Append(">"c)
      _attrs.Clear()
      _writer.Write(sb.ToString())
      _tagStack.Push(name)
    End Sub

    Public Sub RenderEndTag()
      If _tagStack.Count = 0 Then Return
      Dim name = _tagStack.Pop()
      _writer.Write("</" & name & ">")
    End Sub

    Public Sub Write(text As String)
      _writer.Write(text)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub
  End Class
End Namespace
