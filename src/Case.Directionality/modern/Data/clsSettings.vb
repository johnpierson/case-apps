Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  Public Class clsSettings

    Private _cmd As ExternalCommandData

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
        Catch ex As Exception
          Return Nothing
        End Try
      End Get
    End Property

    ''' <summary>
    ''' Path to Active Doc
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
              Return Doc.PathName
            End If
          Else
            Return Doc.PathName
          End If
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    Public Property ExternalWalls As List(Of clsExternalWalls)
    Public Property AllWalls As List(Of Wall)
    Public Property WallParametersString As List(Of String)
    Public Property WallParametersNumber As List(Of String)

#End Region

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ECommandData"></param>
    ''' <remarks></remarks>
    Public Sub New(ECommandData As ExternalCommandData)

      ' Widen Scope
      _cmd = ECommandData

      ' Process Data
      GetWalls()

    End Sub

    ''' <summary>
    ''' Get External Walls
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetWalls()

      ' Counter
      Dim iCnt As Integer = 0

      ' Fresh Lists
      WallParametersString = New List(Of String)
      WallParametersNumber = New List(Of String)
      AllWalls = New List(Of Wall)
      ExternalWalls = New List(Of clsExternalWalls)

      ' Collect the Walls
      Using col As New FilteredElementCollector(Doc)
        col.OfClass(GetType(Wall))
        col.WhereElementIsNotElementType()
        For Each x As Element In col.ToElements

          ' '' '' Group Ignore
          '' ''If x.GroupId.Value > 0 Then Continue For

          ' Parameters
          If iCnt = 0 Then
            Try
              For Each p As Parameter In x.Parameters

                ' Type
                  Dim isText As Boolean = False
                  Dim isNumber As Boolean = False

#If Revit2020 Or Revit2021 Or Revit2022
                    Dim m_ptype As ParameterType = p.Definition.ParameterType
                    isText = m_ptype = ParameterType.Text
                    isNumber = m_ptype = ParameterType.Number

#End If
                  #If Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
                  Dim m_ptype As ForgeTypeId  = p.Definition.GetDataType()
                  isText = m_ptype = SpecTypeId.String.Text
                  isNumber = m_ptype = SpecTypeId.Number
#End If
               

                ' Definition Format
                If isText Then

                  Try

                    ' Not Readonly
                    If p.IsReadOnly = False Then WallParametersString.Add(p.Definition.Name)

                  Catch
                  End Try

                End If

                ' Definition Format
                If isText Or isNumber Then

                  Try

                    ' Not Readonly
                    If p.IsReadOnly = False Then WallParametersNumber.Add(p.Definition.Name)

                  Catch
                  End Try

                End If

              Next

              ' Sort the List
              WallParametersString.Sort()
              WallParametersNumber.Sort()

            Catch
            End Try

          End If

          ' Wall Process Count
          iCnt += 1

          Try
            AllWalls.Add(TryCast(x, Wall))
          Catch ex As Exception

          End Try
          Try
            Dim m_w As Wall = TryCast(x, Wall)
            Dim m_symb As WallType = Me.Doc.GetElement(x.GetTypeId)
            Dim m_p As Parameter = m_symb.Parameter(BuiltInParameter.FUNCTION_PARAM)
            Dim m_val As WallFunction = DirectCast(m_p.AsInteger, WallFunction)
            If m_val = WallFunction.Exterior Then
              ExternalWalls.Add(New clsExternalWalls(m_w))
            End If

          Catch
          End Try

        Next

      End Using

    End Sub

  End Class
End Namespace