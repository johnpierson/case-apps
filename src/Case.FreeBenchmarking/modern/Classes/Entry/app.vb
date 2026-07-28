Imports Autodesk.Revit.ApplicationServices
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.UI

Imports System.IO
Imports System.Reflection

Imports Autodesk.Revit.UI.Events
Imports Autodesk.Revit.DB.Events

<Transaction(TransactionMode.Manual)>
Class app

  Implements IExternalApplication

  Private _s As clsSettings
  Private _eventSWC As clsSWC
  Private _eventDocs As clsDocs
  Private _iCnt As Integer = 0
  Private _RevitUserName As String = ""
  Private _RevitBuild As String = ""
  Private _RevitName As String = ""
  Private _RevitNumber As String = ""

  Private _path As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location)

#Region "Event Handler - SWC"

  ''' <summary>
  ''' Sync Start
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub SwcStart(ByVal sender As Object,
                       ByVal e As DocumentSynchronizingWithCentralEventArgs)

    ' The SWC Class
    _eventSWC = Nothing
    Try

      ' Settings
      _s = New clsSettings(e.Document)
      If Not String.IsNullOrEmpty(_s.Doc.Application.Username) Then
        _RevitUserName = _s.Doc.Application.Username
      Else
        _RevitUserName = ""
      End If
      If Not String.IsNullOrEmpty(_s.DocumentName) Then
        _eventSWC = New clsSWC(_s, e.Comments)
      End If

      ' Model ID Attempt Count
      _iCnt = 0

    Catch ex As Exception

      ' Model ID Attempt Count
      _iCnt = 5

    End Try

  End Sub

  ''' <summary>
  ''' Sync To Central End Event
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Sub SwcEnd(ByVal sender As Object,
             ByVal e As DocumentSynchronizedWithCentralEventArgs)

    ' Close the Session
    If Not _eventSWC Is Nothing Then

      ' Finalize
      _eventSWC.EndSwc()

      Try

        ' Write STC end record
        AppendLog("Sync With Central",
                  _eventSWC.SWCstart,
                  Now().ToString,
                  "Model: " & _s.DocumentName & "; Revit Build: " & _RevitBuild)

      Catch ex As Exception
        ' This should NEVER happen
      End Try

    End If

  End Sub

#End Region

#Region "Event Handler - Document Opening"

  ''' <summary>
  ''' Opening a Doc
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub DocStart(ByVal sender As Object,
                       ByVal e As DocumentOpeningEventArgs)

    ' Fresh Docs
    _eventDocs = New clsDocs(e.PathName)

  End Sub

  ''' <summary>
  ''' Opened a Doc
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub DocDone(ByVal sender As Object,
                      ByVal e As DocumentOpenedEventArgs)

    ' Record Data
    If Not _eventDocs Is Nothing Then

      ' Log
      AppendLog("Document Open",
                _eventDocs.TimeStart,
                Now().ToString,
                "Path: " & _eventDocs.FilePath & "; File Size: " & _eventDocs.FileSizeMb)

    End If

  End Sub

#End Region

  ''' <summary>
  ''' Implement the external application
  ''' </summary>
  ''' <param name="a"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function OnStartup(ByVal a As UIControlledApplication) As Result Implements IExternalApplication.OnStartup

    Try

      ' Add Events
      AddHandler a.ControlledApplication.DocumentSynchronizingWithCentral,
          New EventHandler(Of DocumentSynchronizingWithCentralEventArgs)(AddressOf SwcStart)
      AddHandler a.ControlledApplication.DocumentSynchronizedWithCentral,
          New EventHandler(Of DocumentSynchronizedWithCentralEventArgs)(AddressOf SwcEnd)
      AddHandler a.ControlledApplication.DocumentOpening,
          New EventHandler(Of DocumentOpeningEventArgs)(AddressOf DocStart)
      AddHandler a.ControlledApplication.DocumentOpened,
          New EventHandler(Of DocumentOpenedEventArgs)(AddressOf DocDone)

      ' Defaults
      _RevitBuild = a.ControlledApplication.VersionBuild
      _RevitName = a.ControlledApplication.VersionName
      _RevitNumber = a.ControlledApplication.VersionNumber

      ' Return Success
      Return Result.Succeeded

    Catch

      ' Fail
      Return Result.Failed

    End Try

  End Function

  ''' <summary>
  ''' Implement the external application
  ''' </summary>
  ''' <param name="a"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function OnShutdown(ByVal a As UIControlledApplication) As Result Implements IExternalApplication.OnShutdown

    Try

      ' Unsubscribe from Events
      RemoveHandler a.ControlledApplication.DocumentSynchronizingWithCentral,
          New EventHandler(Of DocumentSynchronizingWithCentralEventArgs)(AddressOf SwcStart)
      RemoveHandler a.ControlledApplication.DocumentSynchronizedWithCentral,
          New EventHandler(Of DocumentSynchronizedWithCentralEventArgs)(AddressOf SwcEnd)
      RemoveHandler a.ControlledApplication.DocumentOpening,
          New EventHandler(Of DocumentOpeningEventArgs)(AddressOf DocStart)
      RemoveHandler a.ControlledApplication.DocumentOpened,
          New EventHandler(Of DocumentOpenedEventArgs)(AddressOf DocDone)

    Catch
    End Try

    ' Return Success
    Return Result.Succeeded

  End Function

End Class