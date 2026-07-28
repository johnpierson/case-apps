Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties"

    ''' <summary>
    ''' Active Document
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

    ''' <summary>
    ''' Document Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DocName As String
      Get
        Try
          If Doc.IsWorkshared = True Then
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Else
              Return "Detached Model"
            End If
          Else
            Return Doc.PathName
          End If
        Catch
        End Try
        Return ""
      End Get
    End Property

    ''' <summary>
    ''' App Version
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Version As String
      Get
        Return My.Application.Info.Version.ToString
      End Get
    End Property

    ''' <summary>
    ''' Categories
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Cats As List(Of clsCategory)

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="eSet"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet

      ' Get Categories
      GetCategories()

    End Sub

    ''' <summary>
    ''' Get All Categories
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCategories()

      ' Sorted
      Dim m_c As New SortedDictionary(Of String, Category)
      For Each x As Category In Doc.Settings.Categories
        Try
          m_c.Add(x.Name, x)
        Catch
        End Try
      Next

      ' To List
      Cats = New List(Of clsCategory)
      For Each x In m_c.Values
        Cats.Add(New clsCategory(x, Doc))
      Next

    End Sub

  End Class
End Namespace