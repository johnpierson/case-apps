Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSharedParameters

    Private _uiApp As UIApplication
    Private _df As DefinitionFile

#Region "Public Properties"

    ''' <summary>
    ''' Shared Parameter File Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName As String
      Get
        Try
          Return _df.Filename
        Catch
        End Try
        Return "Load a Shared Parameter File"
      End Get
    End Property

    ''' <summary>
    ''' By Group
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionsByGroup As SortedDictionary(Of String, List(Of Definition))

    ''' <summary>
    ''' By Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionsByType As SortedDictionary(Of String, List(Of Definition))

#End Region

    ''' <summary>
    ''' Shared Parameter File 
    ''' </summary>
    ''' <param name="uiApp"></param>
    ''' <remarks></remarks>
    Public Sub New(uiApp As UIApplication)

      ' Widen Scope
      _uiApp = uiApp

      Try

        ' Valid Definition File
        _df = _uiApp.Application.OpenSharedParameterFile

      Catch
      End Try

      ' Get the Definitions
      GetParameters()

    End Sub

#Region "Private Members"
    
    ''' <summary>
    ''' Update the Parameter Lists 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParameters()

      ' Fresh Lists
      DefinitionsByGroup = New SortedDictionary(Of String, List(Of Definition))
      DefinitionsByType = New SortedDictionary(Of String, List(Of Definition))

      ' Definition File Exists?
      If _df Is Nothing Then Exit Sub

      Try

        ' Iterate All Groups
        For Each g As DefinitionGroup In _df.Groups
          For Each d As Definition In g.Definitions

            ' By Groups
            If Not DefinitionsByGroup.ContainsKey(g.Name) Then
              Dim m_dList As New List(Of Definition)
              m_dList.Add(d)
              DefinitionsByGroup.Add(g.Name, m_dList)
            Else
              DefinitionsByGroup(g.Name).Add(d)
            End If

            ' By Type
              #If Revit2020 Or Revit2021
 If Not DefinitionsByType.ContainsKey(d.ParameterType.ToString) Then
              Dim m_dList As New List(Of Definition)
              m_dList.Add(d)
              DefinitionsByType.Add(d.ParameterType.ToString, m_dList)
            Else
              DefinitionsByType(d.ParameterType.ToString).Add(d)
            End If
#End If
           #If Revit2022 Or Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
              If Not DefinitionsByType.ContainsKey(LabelUtils.GetLabelForSpec(d.GetDataType)) Then
                  Dim m_dList As New List(Of Definition)
                  m_dList.Add(d)
                  DefinitionsByType.Add(LabelUtils.GetLabelForSpec(d.GetDataType), m_dList)
              Else
                  DefinitionsByType(LabelUtils.GetLabelForSpec(d.GetDataType)).Add(d)
              End If
#End If

          Next
        Next

      Catch ex As Exception

      End Try

    End Sub

#End Region

#Region "Public Members"
    
    ''' <summary>
    ''' Load the Shared Parameter File
    ''' </summary>
    ''' <param name="f"></param>
    ''' <remarks></remarks>
    Public Sub LoadSharedParameterFile(f As String)

      Try

        ' Load the File
        _uiApp.Application.SharedParametersFilename = f
        _df = _uiApp.Application.OpenSharedParameterFile

        ' Get Parameters List
        GetParameters()

      Catch ex As Exception

        ' Failure
        _df = Nothing

      End Try

    End Sub

    ''' <summary>
    ''' Bind Definitions to Multiple Categories 
    ''' </summary>
    ''' <param name="defList"></param>
    ''' <param name="catList"></param>
    ''' <param name="bipGroup"></param>
    ''' <param name="isInstance"></param>
    ''' <remarks></remarks>
    Public Sub BindDefinitionsToCategories(defList As List(Of Definition),
                                           catList As List(Of Category),
                                           bipGroup As ForgeTypeId,
                                           isInstance As Boolean)

      ' Category Set
      Dim m_catSet As New CategorySet
      For Each cat As Category In catList
        m_catSet.Insert(cat)
      Next

      Dim m_failNames As New List(Of String)
      Dim m_iSuc As Integer = 0

      ' Progress Bar Form
      Dim m_k As String = "Type"
      If isInstance = True Then
        m_k = "Instance"
      End If
      Using m_prog As New form_Progress(defList.Count, m_k)
        m_prog.Show()

        ' Process Each Definition 
        For Each def As Definition In defList

          Try
            m_prog.Inc(def.Name)
          Catch
          End Try

          ' Transaction
          Using t As New Transaction(_uiApp.ActiveUIDocument.Document,
                                     "Add Parameter Binding: " & def.Name)
            If t.Start Then

              Try

                ' Family?
                If _uiApp.ActiveUIDocument.Document.IsFamilyDocument = True Then

                  Try

                    ' Family Manager
                    Dim m_famMan As FamilyManager = _uiApp.ActiveUIDocument.Document.FamilyManager

                    m_famMan.AddParameter(def, bipGroup, isInstance)

                    ' Count Success
                    m_iSuc += 1

                  Catch ex As Exception

                    m_failNames.Add(def.Name & "(" & ex.Message & ")")

                  End Try

                Else

                  ' Bind to Category
                  If isInstance = True Then

                    ' Instance
                    Dim m_bind As InstanceBinding
                    m_bind = _uiApp.Application.Create.NewInstanceBinding(m_catSet)
                    _uiApp.ActiveUIDocument.Document.ParameterBindings.Insert(def, m_bind, bipGroup)

                    ' Count Success
                    m_iSuc += 1

                  Else

                    ' Type
                    Dim m_bind As TypeBinding
                    m_bind = _uiApp.Application.Create.NewTypeBinding(m_catSet)
                    _uiApp.ActiveUIDocument.Document.ParameterBindings.Insert(def, m_bind, bipGroup)

                    ' Count Success
                    m_iSuc += 1

                  End If

                End If

                ' Commit
                t.Commit()

              Catch ex As Exception

                m_failNames.Add(def.Name)

              End Try

            End If
          End Using

        Next

        ' Close Progress
        m_prog.Close()

      End Using

      ' Report to User
      Using td As New Autodesk.Revit.UI.TaskDialog("Here's What Happened: :)")
        td.MainInstruction = m_k & " Parameters Loaded:"
        td.MainContent = m_iSuc.ToString & " Succeeded..."
        Dim m_msg As String = vbCr & vbCr & "Failed to Load These Guys:"
        If m_failNames.Count > 0 Then
          For Each x As String In m_failNames
            m_msg += vbCr & x & vbCr
          Next
          td.MainContent += m_msg
        End If
        td.Show()
      End Using

    End Sub

#End Region

  End Class
End Namespace
