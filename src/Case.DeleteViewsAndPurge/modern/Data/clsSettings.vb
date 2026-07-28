Imports System.Linq
Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet

#Region "Public Properties"

    ''' <summary>
    ''' Sheet Views
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sheets As List(Of ElementId)

    ''' <summary>
    ''' All Other Views (not Sheets)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Views As List(Of Autodesk.Revit.DB.View)

    ''' <summary>
    ''' Document
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
    ''' The Active View in the Session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveView As Autodesk.Revit.DB.View
      Get
        Try
          Return _cmd.Application.ActiveUIDocument.ActiveView
        Catch
        End Try
        Return Nothing
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="c"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub New(c As ExternalCommandData, e As ElementSet)

      ' Widen Scope
      _cmd = c
      _eSet = e

      ' Fresh Lists
      Views = New List(Of Autodesk.Revit.DB.View)
      Sheets = New List(Of ElementId)

    End Sub

#Region "Public Members"

    ''' <summary>
    ''' Delete Links
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteLinks() As Integer

      ' Deleted Items
      Dim iCntLinks As Integer = 0

      ' New Transaction
      Using t As New Transaction(Doc, "Delete Links")
        If t.Start = TransactionStatus.Started Then
          Try

            ' Get all Links
            Using col As New FilteredElementCollector(Doc)
              col.OfCategory(BuiltInCategory.OST_RvtLinks)

              ' Delete them
              For Each x In col.ToElements
                If TypeOf x Is RevitLinkType Then
                  Dim m_link As RevitLinkType = TryCast(x, RevitLinkType)
                End If
                Try
                  Doc.Delete(x.Id)
                  iCntLinks += 1
                Catch
                End Try
              Next

            End Using

            ' Success
            t.Commit()

          Catch

            ' Fialure
            t.RollBack()

          End Try
        End If
      End Using

      ' Succesfull Deletions
      Return iCntLinks

    End Function

    ''' <summary>
    ''' Delete Views
    ''' </summary>
    ''' <param name="p">Progress Bar</param>
    ''' <returns>Number of succesfully deleted views</returns>
    ''' <remarks></remarks>
    Public Function DeleteViews(p As ProgressBar) As Integer

      ' Return
      Dim m_Deleted As Integer = 0

      Try

        ' Fresh List of Views
        GetViews()

        ' Delete as Many as Possible
        For Each x As Autodesk.Revit.DB.View In Views

          Try
            ' Progress
            p.Increment(1)
          Catch
          End Try

          ' Current View?
          If ActiveView.Id.Value = x.Id.Value Then
            Continue For
          End If

          ' Skip Sheets
          If TypeOf x Is ViewSheet Then Continue For

          ' No Browser Views
          If x.ViewType = ViewType.Internal Then Continue For
          If x.ViewType = ViewType.Undefined Then Continue For
          If x.ViewType = ViewType.ProjectBrowser Then Continue For
          If x.ViewType = ViewType.SystemBrowser Then Continue For

          ' New Transaction
          Using t As New Transaction(Doc, "Delete Views")
            If t.Start() Then
              Try

                ' Delete the View
                Doc.Delete(x.Id)

                t.Commit()

                ' Add as Deleted
                m_Deleted += 1

              Catch

                t.RollBack()

              End Try
            End If
          End Using

        Next

      Catch
      End Try

      ' Deleted Items
      Return m_Deleted

    End Function

    ''' <summary>
    ''' Return a List of Views
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetViews()

      ' Fresh List
      Views = New List(Of Autodesk.Revit.DB.View)

      Try

        ' Linq Query
        Dim m_linqViews = From v In New FilteredElementCollector(Doc).OfClass(GetType(Autodesk.Revit.DB.View))
              Let vv = TryCast(v, Autodesk.Revit.DB.View)
              Where TypeOf v Is ViewSheet = False And
                    vv.ViewType <> ViewType.Internal And
                    vv.ViewType <> ViewType.ProjectBrowser And
                    vv.ViewType <> ViewType.SystemBrowser
              Select vv

        ' To List
        Views = m_linqViews.ToList

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Return a List of Sheets
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSheets()

      ' Fresh List
      Sheets = New List(Of ElementId)

      Try

        ' Collector
        Using c As New FilteredElementCollector(Doc)
          c.OfClass(GetType(ViewSheet))
          c.WhereElementIsNotElementType()

          ' List of ElementID's
          Sheets = c.ToElementIds

        End Using

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace