Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsCategory

    Private _doc As Document

#Region "Public Properties"

    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return Cat.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property
    Public Property Cat As Category
    Public Property CatParamsElement As List(Of String)
    Public Property CatParamsText As List(Of String)
    Public Property CatInstances As List(Of Element)

#End Region

    ''' <summary>
    ''' Category Helper
    ''' </summary>
    ''' <param name="c"></param>
    ''' <param name="d"></param>
    ''' <remarks></remarks>
    Public Sub New(c As Category,
                   d As Document)

      ' Widen Scope
      Cat = c
      _doc = d

      ' Fresh Lists
      CatParamsElement = New List(Of String)
      CatParamsText = New List(Of String)
      CatInstances = New List(Of Element)

    End Sub

    ''' <summary>
    ''' Get the Element and Text Parameters
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParameters()

      Try
        CatParamsElement.Add("ElementId")

        ' Get the First Element
        For Each x In CatInstances
          ' Get all Instance Parameters
          For Each p As Parameter In x.Parameters
            Try
              ' Helper
              Dim m_para As New clsParameter(p)
              If Not m_para Is Nothing Then
                ' Text and Not Readonly - Text
                If m_para.ParameterIsReadOnly = False Then
                  If m_para.Format = StorageType.String Then
                    CatParamsText.Add(m_para.Name)
                  End If
                End If
                ' Everything Can be Source
                CatParamsElement.Add(m_para.Name)
              End If
            Catch
            End Try
          Next
          ' Only First Element
          Exit For
        Next
      Catch
      End Try

      CatParamsElement.Sort()
      CatParamsText.Sort()

    End Sub

    ''' <summary>
    ''' Get All Instance Elements
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetInstances()

      ' Fresh Lists
      CatParamsElement = New List(Of String)
      CatParamsText = New List(Of String)
      CatInstances = New List(Of Element)

      Try

        ' Get all Instance Elements
        Using c As New FilteredElementCollector(_doc)
          c.WhereElementIsNotElementType()
          c.OfCategoryId(Cat.Id)
          For Each x In c.ToElements
            Try
              ' Ignore Grouped Elements
              If x.GroupId.Value < 1 Then
                CatInstances.Add(x)
              End If
            Catch
            End Try
          Next
        End Using

      Catch
      End Try

      ' Get Parameters
      GetParameters()

    End Sub

  End Class
End Namespace