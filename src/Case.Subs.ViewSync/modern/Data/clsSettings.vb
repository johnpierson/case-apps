Imports System.Linq
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

#Region "Enumerators"

  ''' <summary>
  ''' Master Data State
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumCfgState
    IsOk
    IsNullValue
    IsPathNotFound
    IsMissingParameter
    IsError
  End Enum

  ''' <summary>
  ''' Log Kind
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumLogKind
    IsConfig
    IsTag
    IsSync
    IsOther
  End Enum

  ''' <summary>
  ''' Tag State
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum EnumTagState
    IsOk
    IsNull
    IsOrphan
  End Enum

#End Region

  ''' <summary>
  ''' Master Settings Class
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsSettings

    Private _cmd As ExternalCommandData
    Private _eSet As ElementSet
    Private _appVer As String = ""

#Region "Public Properties - Document"

    ''' <summary>
    ''' Zoom to a Set of Elements
    ''' </summary>
    ''' <param name="e">Element to Zoom in on</param>
    ''' <remarks></remarks>
    Friend Sub ZoomToElement(e As Element)
      Try
        _cmd.Application.ActiveUIDocument.ShowElements(e)
      Catch
      End Try
    End Sub

    ''' <summary>
    ''' Active Selections in the Model
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedElements As ElementSet
      Get
        Return UiDoc.Selection.GetElementIds()
      End Get
    End Property

    ''' <summary>
    ''' UI Application
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiApp As UIApplication
      Get
        Try
          Return _cmd.Application
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' UI Doc
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UiDoc As UIDocument
      Get
        Try
          Return _cmd.Application.ActiveUIDocument
        Catch
        End Try
        Return Nothing
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
        Catch
        End Try
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' Document Name And Path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DocName As String
      Get
        Try

          ' Workshared?
          If Doc.IsWorkshared Then

            ' Test for a valid file name
            If Not String.IsNullOrEmpty(Doc.GetWorksharingCentralModelPath.CentralServerPath) Then

              ' Only use central file name if the model has been saved
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath

            Else

              ' Non Revit Server Path
              Dim m_centralFilePath As String = ModelPathUtils.ConvertModelPathToUserVisiblePath(Doc.GetWorksharingCentralModelPath)
              If Not String.IsNullOrEmpty(m_centralFilePath) Then
                Return m_centralFilePath
              Else
                Return ""
              End If
            End If

          Else

            ' Use the document title
            Return Doc.PathName

          End If

        Catch
        End Try

        ' Failure
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
        Return _appVer
      End Get
    End Property

#End Region

#Region "Public Properties - Data"

    ''' <summary>
    ''' Configuration Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConfigData As clsConfig

#End Region

#Region "Public Properties - Elements"

    ''' <summary>
    ''' Tag Symbols
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamTagSymbols As clsSortableBindingList(Of FamilySymbol)

    ''' <summary>
    ''' Tag Instances
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamTagInst As Dictionary(Of Integer, List(Of clsTagInstance))

    ''' <summary>
    ''' Tag Instance Helpers
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FamTagHelpers As Dictionary(Of EnumTagState, clsSortableBindingList(Of clsTagInstance))

#End Region

    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="cmd">IExternalCommand</param>
    ''' <param name="eSet"></param>
    ''' <remarks></remarks>
    Public Sub New(cmd As ExternalCommandData, eSet As ElementSet)

      ' Widen Scope
      _cmd = cmd
      _eSet = eSet

      ' Setup
      DoSetup()

    End Sub

    ''' <summary>
    ''' Base Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoSetup()

      ' Fresh Lists
      FamTagInst = New Dictionary(Of Integer, List(Of clsTagInstance))
      ConfigData = New clsConfig

      Try

        ' Raw Config
        ConfigData.GetProjectInfo(Doc, ConfigData.ParamData)

        ' Version
        _appVer = " v" & My.Application.Info.Version.ToString

      Catch
      End Try

    End Sub

#Region "Friends - Tag Updates"

    ''' <summary>
    ''' Update the Element Tags
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Friend Sub UpdateTags(v As clsVp, e As List(Of Element))

      ' Process Each Element in List 
      For Each x In e

        ' Start a New Transaction
        Using t As New Transaction(Doc, "Update Pseudo Tag")
          If t.Start = TransactionStatus.Started Then

            Try

              ' Instance Helper
              Dim m_tag As New clsTagInstance(x, ConfigData.ParamGuid, ConfigData.ParamView, ConfigData.ParamShtn)

              ' Update the Tag
              m_tag.SetValues(v)

            Catch
            End Try

            ' Commit
            t.Commit()

          End If

        End Using

      Next

    End Sub

    ''' <summary>
    ''' Update All Tags
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateAll()

      Try

        ' Get Symbols and Instances
        GetSymbols()

        If FamTagSymbols.Count > 0 Then

          ' Find the Instances
          GetInstances()

          ' Anything to sync?
          Dim m_iCnt As Integer = FamTagInst.Sum(Function(x) x.Value.Count)

          ' No Instances?
          If m_iCnt = 0 Then

            ' Inform User
            Using td As New Autodesk.Revit.UI.TaskDialog("No Pseudo Tag Family Placements")
              td.TitleAutoPrefix = False
              td.MainInstruction = "Place at least one of the following:"
              Dim m_msg As String = ""
              For Each x In ConfigData.Families
                m_msg += "  " & x.DisplayName & vbCr
              Next
              td.MainContent = m_msg
              td.Show()
            End Using

            ' Exit Sub
            Return

          End If

          Try

            ' ANything to Sync?
            If FamTagHelpers(EnumTagState.IsOk).Count = 0 Then

              ' Nothing to Update
              Return

            Else

              ' Results Counts
              Dim m_iFail As Integer = 0
              Dim m_iSucc As Integer = 0

              ' Iterate
              For Each x As clsTagInstance In FamTagHelpers(EnumTagState.IsOk)

                ' Start a transaction
                Using t As New Transaction(Doc, "Subscription Tag Sync: " & x.DetailNumber & "/" & x.SheetNumber)
                  If t.Start = TransactionStatus.Started Then

                    Try

                      ' Update the Tag
                      x.SetValues(ConfigData.ViewData.ViewPorts(x.SyncGuid))

                      ' Success
                      t.Commit()

                      m_iSucc += 1

                    Catch

                      m_iFail += 1

                    End Try

                  End If
                End Using

              Next

              Dim m_title As String = ""
              Dim m_msg As String = ""

              If m_iSucc = 0 Then
                m_title = "All Tags Failed to Update..."
                m_msg = m_iFail.ToString & " tags failed to update"
              End If

              If m_iFail = 0 Then
                m_title = "Updated Succesfully!"
                m_msg = m_iSucc.ToString & " tags update"
              End If

              If m_iFail > 0 And m_iSucc > 0 Then
                m_title = "Some Tags Updated Succesfully!"
                m_msg = m_iFail.ToString & " tags failed to update..." & vbCr
                m_msg += m_iSucc.ToString & " tags updated succesfully"
              End If

              ' Report Results to User
              Using td As New Autodesk.Revit.UI.TaskDialog("Tag Update Results")
                With td
                  .TitleAutoPrefix = False
                  .MainInstruction = m_title
                  .MainContent = m_msg
                  .Show()
                End With
              End Using

            End If

          Catch
          End Try

        Else

          ' No Symbols Loaded
          Using td As New Autodesk.Revit.UI.TaskDialog("No Pseudo Tag Families Loaded")
            td.MainInstruction = "Load at least one of the following:"
            Dim m_msg As String = ""
            For Each x In ConfigData.Families
              m_msg += "  " & x.DisplayName & vbCr
            Next
            td.MainContent = m_msg
            td.Show()
          End Using

        End If

      Catch
      End Try

    End Sub

#End Region

#Region "Friends - Element Collections"

    ''' <summary>
    ''' Get the Viewports in the Model
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetViewports()

      ' Fresh Dictionary
      Dim m_modelVp As New Dictionary(Of String, clsVp)

      Try

        ' Get Sheets
        Dim m_sh = From e In New FilteredElementCollector(Doc) _
              .OfClass(GetType(ViewSheet))
              Let vs = TryCast(e, ViewSheet)
              Select vs

        ' Valid?
        If Not m_sh Is Nothing Then
          For Each x In m_sh.ToList
            For Each vid In x.GetAllPlacedViews()

              ' Add View to Local Set
              Dim v As View = TryCast(Doc.GetElement(vid), View)
              m_modelVp.Add(v.UniqueId.ToString.ToLower, New clsVp(v))

            Next
          Next
        End If

      Catch
      End Try

      ' Are all Current VP in Local?
      For Each x In ConfigData.ViewData.ViewPorts
        Try
          If Not m_modelVp.ContainsKey(x.Key) Then
            x.Value.ViewState = "Deleted"
          Else
            x.Value.ViewState = "Active"
          End If
        Catch
        End Try
      Next

      ' Add the Local to the Master
      For Each x In m_modelVp.Values
        Try
          If ConfigData.ViewData.ViewPorts.ContainsKey(x.Guid.ToLower) Then
            ConfigData.ViewData.ViewPorts(x.Guid.ToLower) = x
          Else
            ConfigData.ViewData.ViewPorts.Add(x.Guid.ToLower, x)
          End If
        Catch
        End Try
      Next

    End Sub

    ''' <summary>
    ''' Get all Tag Instances
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetInstances()

      ' Fresh List
      FamTagHelpers = New Dictionary(Of EnumTagState, clsSortableBindingList(Of clsTagInstance))
      FamTagHelpers.Add(EnumTagState.IsNull, New clsSortableBindingList(Of clsTagInstance))
      FamTagHelpers.Add(EnumTagState.IsOrphan, New clsSortableBindingList(Of clsTagInstance))
      FamTagHelpers.Add(EnumTagState.IsOk, New clsSortableBindingList(Of clsTagInstance))

      Try

        ' Detail Component Instances
        Dim m_fi = From e In New FilteredElementCollector(Doc) _
              .OfCategory(BuiltInCategory.OST_DetailComponents) _
              .OfClass(GetType(FamilyInstance))
              Let fi = TryCast(e, FamilyInstance)
              Where FamTagInst.ContainsKey(fi.GetTypeId.Value)
              Select fi

        ' Any Results?
        If Not m_fi Is Nothing Then
          For Each x In m_fi.ToList

            ' Helper Instance
            Dim m_tagI As New clsTagInstance(x,
                                             ConfigData.ParamGuid,
                                             ConfigData.ParamView,
                                             ConfigData.ParamShtn)

            ' Add as Raw
            FamTagInst(x.GetTypeId.Value).Add(m_tagI)

            ' GUID Param
            Dim m_tagGuid As String = m_tagI.SyncGuid

            ' NULL, Orphaned, or OK?
            If String.IsNullOrEmpty(m_tagGuid) Then

              Try
                ' NULL
                FamTagHelpers(EnumTagState.IsNull).Add(m_tagI)
              Catch
              End Try

            Else

              ' Orphan?
              If ConfigData.ViewData.ViewPorts.ContainsKey(m_tagGuid) Then

                ' Deleted?
                Dim m_vpMatch As clsVp = ConfigData.ViewData.ViewPorts(m_tagGuid)
                Try
                  m_tagI.SetVp(m_vpMatch)
                Catch
                End Try
                If m_vpMatch.ViewState.ToLower = "deleted" Then

                  ' Orphaned
                  FamTagHelpers(EnumTagState.IsOrphan).Add(m_tagI)

                Else

                  ' OK
                  FamTagHelpers(EnumTagState.IsOk).Add(m_tagI)

                End If

              Else

                ' Orphaned
                FamTagHelpers(EnumTagState.IsOrphan).Add(m_tagI)

              End If

            End If

          Next
        End If

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Find the Tag Symbols for Sync
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetSymbols()

      ' Fresh List
      FamTagSymbols = New clsSortableBindingList(Of FamilySymbol)
      FamTagInst = New Dictionary(Of Integer, List(Of clsTagInstance))

      Try

        ' Fresh Collection
        Dim m_fi = From e In New FilteredElementCollector(Doc) _
              .OfCategory(BuiltInCategory.OST_DetailComponents) _
              .OfClass(GetType(FamilySymbol))
              Let fs = TryCast(e, FamilySymbol)
              Select fs

        ' Anything?
        If Not m_fi Is Nothing Then
          For Each x In m_fi.ToList

            ' Name Verification
            If ConfigData.HasFamily(x.Family.Name.ToLower & "|" & x.Name.ToLower) Then

              ' Add to Dictionary
              FamTagInst.Add(x.Id.Value, New List(Of clsTagInstance))

              ' Matching Item
              FamTagSymbols.Add(x)

            End If

          Next
        End If

      Catch
      End Try

    End Sub

#End Region

  End Class
End Namespace