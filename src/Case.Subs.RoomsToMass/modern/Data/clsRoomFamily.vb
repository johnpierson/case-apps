Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture

Namespace Data

  Public Class clsRoomFamily

#Region "Public Properties"

    Public Property RoomElement As Room
    Public Property IsChecked As Boolean
    Public ReadOnly Property Number As String
      Get
        Try
          Return RoomElement.Parameter(BuiltInParameter.ROOM_NUMBER).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Name As String
      Get
        Try
          Return RoomElement.Parameter(BuiltInParameter.ROOM_NAME).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Department As String
      Get
        Try
          Return RoomElement.Parameter(BuiltInParameter.ROOM_DEPARTMENT).AsString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Level As String
      Get
        Try
          Return RoomElement.Level.Name
        Catch ex As Exception
          Return "N/A"
        End Try
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Construct a Room Mass from a Room Element in the Model 
    ''' </summary>
    ''' <param name="r">Room Element to build a mass family for</param>
    ''' <remarks></remarks>
    Public Sub New(r As Room)

      ' Widen Scope
      RoomElement = r
      isChecked = True

      ' '' '' Rebuild the Family
      '' ''If BuildFamily() = True Then
      '' ''  ' It Worked
      '' ''End If

    End Sub

    '' '' ''' <summary>
    '' '' ''' Delete and Purge the Family from the Document
    '' '' ''' </summary>
    '' '' ''' <remarks></remarks>
    '' ''Public Sub DeleteAndPurgeFamily()

    '' ''  ' Find the Instance - Delete it

    '' ''  ' Find the Family - Delete it

    '' ''End Sub

    '' '' ''' <summary>
    '' '' ''' Build the Family
    '' '' ''' </summary>
    '' '' ''' <remarks></remarks>
    '' ''Private Function BuildFamily() As Boolean

    '' ''  ' Succes
    '' ''  Return True

    '' ''End Function

    '' '' ''' <summary>
    '' '' ''' Save the Family
    '' '' ''' </summary>
    '' '' ''' <param name="Path">Path to Save the Family</param>
    '' '' ''' <remarks></remarks>
    '' ''Public Sub SaveFamily(Path As String)

    '' ''End Sub

  End Class
End Namespace