Imports System.Reflection
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

#Region "Public Properties - Doc and Application"

    ''' <summary>
    ''' UI Application Object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As UIApplication
      Get
        Try
          Return _cmd.Application
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

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
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' The Active UI Document
    ''' </summary>
    ''' <value>Document object</value>
    ''' <returns>a UIDocument</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveUiDoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
        Catch ex As Exception
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
        Catch ex As Exception
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
        Return Assembly.GetExecutingAssembly.GetName.Version.ToString
      End Get
    End Property

#End Region

#Region "Public Properties - Family"

    ''' <summary>
    ''' Is the Current Document a Family?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsFamily As Boolean
      Get
        Try
          Return Doc.IsFamilyDocument
        Catch
        End Try
        Return False
      End Get
    End Property

    ''' <summary>
    ''' The Family of the Family Document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyFileFamily As Family
      Get
        Try
          Return Doc.OwnerFamily
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' The Family Category
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyFileCategory As Category
      Get
        Try
          Return Doc.OwnerFamily.FamilyCategory
        Catch
        End Try
        Return Nothing
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData)

      ' Widen Scope
      _cmd = cmd

    End Sub

  End Class
End Namespace