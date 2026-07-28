Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsViews

#Region "Public Properties"

    ''' <summary>
    ''' View Element
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewElement As View

    ''' <summary>
    ''' View Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewName As String
      Get
        Try
          Return ViewElement.Name
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Level of View
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewLevel As String
      Get
        Try
          If Not ViewElement.GenLevel Is Nothing Then
            Return ViewElement.GenLevel.Name
          End If
        Catch
        End Try
        Return "n/a"
      End Get
    End Property

    ''' <summary>
    ''' View Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ViewKind As String
      Get
        Try
          Return ViewElement.ViewType.ToString
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

#End Region

    ''' <summary>
    ''' View Helper
    ''' </summary>
    ''' <param name="v"></param>
    ''' <remarks></remarks>
    Public Sub New(v As View)

      ' Widen Scope
      ViewElement = v

    End Sub

    ''' <summary>
    ''' Set the View Template
    ''' </summary>
    ''' <param name="eid">ElementID for Template</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTemplate(eid As String) As Boolean

      ' Start a New Transaction
      Using t As New Transaction(ViewElement.Document, "Update View Template Assignment")
        If t.Start Then

          Try

            ' Assign the New Template
            ViewElement.ViewTemplateId = New ElementId(CInt(eid))

            ' Success
            t.Commit()
            Return True

          Catch
          End Try

        End If
      End Using

      ' Failure
      Return False

    End Function

  End Class
End Namespace