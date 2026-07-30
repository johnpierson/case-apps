Imports Autodesk.Revit.DB

Public Class clsFamily

#Region "Public Properties"

  ''' <summary>
  ''' Family and Type Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property FamilyAndTypeName As String
    Get
      Try
        If TypeOf ElementObject Is FamilySymbol Then
          Dim m_fam As FamilySymbol = TryCast(ElementObject, FamilySymbol)
          If Not m_fam Is Nothing Then
            Return "[" & m_fam.Family.Name & "] " & ElementObject.Name
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
  ''' Long Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property ElementFullName As String
    Get
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
  Public Property ElementObject As Element

  ''' <summary>
  ''' Inclusion
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property isChecked As Boolean

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Public Sub New(e As Element)
    ElementObject = e
    isChecked = False
  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As String)
  End Sub

End Class
