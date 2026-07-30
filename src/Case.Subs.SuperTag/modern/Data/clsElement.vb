Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsElement

#Region "Public Properties"

    ''' <summary>
    ''' Include Checked
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' Element Full Name 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ElementFullName As String
      Get
        Try
          If TypeOf ElementObject Is Architecture.Room Then
            Dim m_dept As String = ElementObject.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString
            Dim m_name As String = ElementObject.Parameter(BuiltInParameter.ROOM_NAME).AsString
            Dim m_number As String = ElementObject.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
            Return "[" & m_number & "] " & m_name & " (" & m_dept & ")"
          End If
        Catch
        End Try

        Try
          If TypeOf ElementObject Is Mechanical.Space Then
            Dim m_name As String = ElementObject.Parameter(BuiltInParameter.ROOM_NAME).AsString
            Dim m_number As String = ElementObject.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
            Return "[" & m_number & "] " & m_name
          End If
        Catch
        End Try

        Try
          If TypeOf ElementObject Is Area Then
            Dim m_name As String = ElementObject.Parameter(BuiltInParameter.ROOM_NAME).AsString
            Dim m_number As String = ElementObject.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
            Return "[" & m_number & "] " & m_name
          End If
        Catch
        End Try

        Try
          If TypeOf ElementObject Is FamilySymbol Then
            Dim m_fam As FamilySymbol = TryCast(ElementObject, FamilySymbol)
            If Not m_fam Is Nothing Then
              Return "[" & ElementObject.Category.Name & " (" & m_fam.Family.Name & ")] " & ElementObject.Name
            End If
          Else
            Return "[" & ElementObject.Category.Name & "] " & ElementObject.Name
          End If
        Catch ex As Exception
        End Try
        Return "<All>"
      End Get
    End Property

    ''' <summary>
    ''' The Element
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property ElementObject As Element

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element)

      ' Widen Scope
      ElementObject = e

      ' Defaults
      isChecked = False

    End Sub

  End Class
End Namespace