Imports Autodesk.Revit.DB
Imports System.IO

Module modGeneral

#Region "Friend Members - Parameters"

  ''' <summary>
  ''' Set the Value of a Parameter
  ''' </summary>
  ''' <param name="e"></param>
  ''' <param name="paramName"></param>
  ''' <param name="newValue"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function SetParamValue(e As Element, paramName As String, newValue As String) As Boolean

    Try

      If e Is Nothing Then Return False
      Dim p As Parameter = e.Parameter(paramName)
      If p Is Nothing Then Return False
      If p.IsReadOnly = True Then Return False

      If p.StorageType = StorageType.String Then
        p.Set(newValue)
        Return True
      Else
        p.SetValueString(newValue)
        Return True
      End If

    Catch
    End Try

    ' Fail
    Return False

  End Function

  ''' <summary>
  ''' Set the Value of a Parameter
  ''' </summary>
  ''' <param name="e"></param>
  ''' <param name="paramName"></param>
  ''' <param name="newValue"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function SetParamValue(e As Element, paramName As String, newValue As Integer) As Boolean

    Try

      If e Is Nothing Then Return False
      Dim p As Parameter = e.Parameter(paramName)
      If p Is Nothing Then Return False

      If p.StorageType = StorageType.Integer Then
        p.Set(newValue)
        Return True
      End If

    Catch
    End Try

    ' Fail
    Return False

  End Function

  ''' <summary>
  ''' Set the Value of a Parameter
  ''' </summary>
  ''' <param name="e"></param>
  ''' <param name="paramName"></param>
  ''' <param name="newValue"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function SetParamValue(e As Element, paramName As String, newValue As Double) As Boolean

    Try

      If e Is Nothing Then Return False
      Dim p As Parameter = e.Parameter(paramName)
      If p Is Nothing Then Return False

      If p.StorageType = StorageType.Double Then
        p.Set(newValue)
        Return True
      End If

    Catch
    End Try

    ' Fail
    Return False

  End Function

  ''' <summary>
  ''' Set the Value of a Parameter
  ''' </summary>
  ''' <param name="e"></param>
  ''' <param name="paramName"></param>
  ''' <param name="newValue"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function SetParamValue(e As Element, paramName As String, newValue As ElementId) As Boolean

    Try

      If e Is Nothing Then Return False
      Dim p As Parameter = e.Parameter(paramName)
      If p Is Nothing Then Return False

      If p.StorageType = StorageType.ElementId Then
        p.Set(newValue)
        Return True
      End If

    Catch
    End Try

    ' Fail
    Return False

  End Function

  ''' <summary>
  ''' Get the Value of a Parameter
  ''' </summary>
  ''' <param name="e"></param>
  ''' <param name="paramName"></param>
  ''' <param name="getString"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function GetParamValue(e As Element, paramName As String, getString As Boolean) As Object

    Try

      If e Is Nothing Then Return ""

      Dim p As Parameter = e.Parameter(paramName)
      If p Is Nothing Then Return ""

      Select Case p.StorageType

        Case StorageType.String
          Return p.AsString()

        Case StorageType.Double
          If getString = True Then
            Return p.AsValueString()
          Else
            Return p.AsDouble()
          End If

        Case StorageType.Integer
          If getString = True Then
            Return p.AsValueString()
          Else
            Return p.AsInteger()
          End If

        Case StorageType.ElementId
          Return p.AsValueString()
          'If getString = True Then
          '  Return p.AsValueString()
          'Else
          '  Return p.AsElementId()
          'End If

      End Select

    Catch
    End Try

    ' Fail
    Return ""

  End Function

#End Region

#Region "Friend Members - IO"

  ''' <summary>
  ''' Get or Create the Master CASE Temp Path
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Friend Function GetCaseTempPath() As String
    Try

      ' Export Paths
      Const c_dirName As String = "CASE Design, Inc."

      ' Main CASE Temp Folder
      Dim m_master As String = Path.Combine(Path.GetTempPath(), c_dirName)
      If Not Directory.Exists(m_master) Then Directory.CreateDirectory(m_master)

      ' Success?
      If Directory.Exists(m_master) Then Return m_master

    Catch
    End Try

    ' fail
    Return ""

  End Function

#End Region

End Module