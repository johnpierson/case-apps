Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

    Friend FamilyElements As List(Of FamilySymbol)

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
          Return Nothing
        End Try
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
          Return ""
        End Try
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

    End Sub

    ''' <summary>
    ''' Get All Families
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetFams()

      ' Fresh List
      FamilyElements = New List(Of FamilySymbol)

      ' Get the Families
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(FamilySymbol))

        For Each x In col.ToElements

          Try
            FamilyElements.Add(x)
          Catch
          End Try

        Next

      End Using

    End Sub

  End Class
End Namespace