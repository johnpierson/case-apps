Imports Autodesk.Revit.DB.Architecture
Imports Autodesk.Revit.DB

Namespace Data

  ''' <summary>
  ''' A Class for handling Door data
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsDoors

    Private ReadOnly Property Doc As Document
      Get
        Return _door.Document
      End Get
    End Property
    Private _door As FamilyInstance
    Private _modelPhases As SortedDictionary(Of Integer, Phase)

#Region "Public Properties"

    ''' <summary>
    ''' Door Family Instance
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property DoorInstance As FamilyInstance
      Get
        Return _door
      End Get
    End Property

    ''' <summary>
    ''' Current Mark
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MarkNow As String

    ''' <summary>
    ''' Suggested Mark
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MarkSuggestion As String

    ''' <summary>
    ''' To Room
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property toRoom As String

    ''' <summary>
    ''' From Room
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fromRoom As String

    ''' <summary>
    ''' Type Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FamilyType As String
      Get
        Return _door.Name
      End Get
    End Property

    ''' <summary>
    ''' Family Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Family As String
      Get
        Return _door.Symbol.Family.Name
      End Get
    End Property

    ''' <summary>
    ''' Creation Phase
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Phase As String

    ''' <summary>
    ''' Generation Level
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Level As String

    ''' <summary>
    ''' Can Edit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowEdits As Boolean

#End Region

    ''' <summary>
    ''' General Class Constructor
    ''' </summary>
    ''' <param name="d"></param>
    ''' <remarks></remarks>
    Public Sub New(
              ByVal d As FamilyInstance,
              modelPhases As SortedDictionary(Of Integer, Phase))

      ' Widen Scope
      _door = d
      _modelPhases = modelPhases

      ' Setup
      AllowEdits = True
      MarkSuggestion = ""
      GetData()
      GetRoomData()

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetData()

      Try
        Level = ""
        Dim m_level As Level = _door.Document.GetElement(_door.LevelId)
        If Not m_level Is Nothing Then Level = m_level.Name
      Catch
      End Try

      Try
        Dim m_phase As Element = Doc.GetElement(_door.CreatedPhaseId)
        Phase = m_phase.Name
      Catch
        Phase = ""
      End Try

      Try
        Dim m_p As Parameter = _door.Parameter(BuiltInParameter.ALL_MODEL_MARK)
        MarkNow = m_p.AsString()
        If String.IsNullOrEmpty(MarkNow) Then MarkNow = ""
      Catch
        MarkNow = ""
      End Try

    End Sub

    ''' <summary>
    ''' Get To and From Room
    ''' </summary>
    ''' <param name="isToRoom"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetValidRoom(isToRoom As Boolean) As Room

      ' Default
      Dim m_rm As Room = Nothing

      Try

        Dim m_testNumber As Integer = 0
        For Each x In _modelPhases.Values
          If x.Id.Value > m_testNumber And x.Id.Value <= _door.CreatedPhaseId.Value Then

            Try
              Dim m_roomTest As Room = Nothing
              If isToRoom = True Then
                m_roomTest = _door.ToRoom(x)
              Else
                m_roomTest = _door.FromRoom(x)
              End If
              If m_roomTest IsNot Nothing Then m_rm = m_roomTest
            Catch
            End Try

            m_testNumber = x.Id.Value

          End If
        Next

      Catch
      End Try

      ' Result
      Return m_rm

    End Function

    ''' <summary>
    ''' To and From Room - with correct phase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRoomData()

      Try

        ' To Room
        Dim m_toRoom As Room = GetValidRoom(True)
        If m_toRoom IsNot Nothing Then
          toRoom = m_toRoom.Number
        Else
          toRoom = ""
        End If

        ' From Room
        Dim m_fromRoom As Room = GetValidRoom(False)
        If m_fromRoom IsNot Nothing Then
          fromRoom = m_fromRoom.Number
        Else
          fromRoom = ""
        End If

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Apply Mark Value to Door
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDoorMark()
      Try
        DoorInstance.Parameter(BuiltInParameter.ALL_MODEL_MARK).Set(MarkSuggestion)
      Catch
      End Try
    End Sub

#End Region

  End Class
End Namespace