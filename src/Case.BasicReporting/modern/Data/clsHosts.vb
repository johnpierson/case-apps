Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsHosts

    Public Enum hostType
      isNormal
      isFace
    End Enum

    Public Property ElementHostType As String = ""
    Public Property ElementID As String = ""
    Public Property HostElementID As String = ""
    Public Property ElementName As String = ""
    Public Property HostElementName As String = ""
    Public Property ElementFamilyName As String = ""
    Public Property ElementCategory As String = ""
    Public Property HostElementCategory As String = ""

    ''' <summary>
    ''' Constructor for Host
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="hostId"></param>
    ''' <param name="hType"></param>
    ''' <remarks></remarks>
    Public Sub New(e As FamilyInstance, hostId As ElementId, hType As hostType)

      ' Widen Scope
      If hType = hostType.isFace Then
        ElementHostType = "Face Based"
      Else
        ElementHostType = "Normal Host"
      End If
      ElementID = e.Id.ToString
      HostElementID = hostId.ToString
      ElementName = e.Name
      Try
        ElementCategory = e.Category.Name
      Catch
      End Try
      Try
        Dim m_fs As FamilySymbol = TryCast(e.Document.GetElement(e.GetTypeId), FamilySymbol)
        If Not m_fs Is Nothing Then
          ElementFamilyName = m_fs.Family.Name
        End If
      Catch
      End Try

      Try

        ' Host Element Data
        Dim m_host As Element = e.Document.GetElement(hostId)
        HostElementName = m_host.Name
        Try
          HostElementCategory = m_host.Category.Name
        Catch
        End Try
      Catch
      End Try

    End Sub

    ''' <summary>
    ''' From basic Elements
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="host"></param>
    ''' <remarks></remarks>
    Public Sub New(e As Element, host As Element)

      ' Kind
      ElementHostType = "Level Inheritance"

      ' Names
      ElementName = e.Name
      HostElementName = host.Name

      ' Category
      If Not e.Category Is Nothing Then
        ElementCategory = e.Category.Name
      Else
        ElementCategory = ""
      End If
      If Not host.Category Is Nothing Then
        HostElementCategory = host.Category.Name
      Else
        HostElementCategory = ""
      End If

      ' ElementIDs
      ElementID = e.Id.ToString
      HostElementID = host.Id.ToString

      ' Family Name
      ElementFamilyName = ""
      Dim m_fs As FamilyInstance = Nothing
      Try
        m_fs = TryCast(e, FamilyInstance)
        If Not m_fs Is Nothing Then
          Dim m_symb As FamilySymbol = TryCast(e.Document.GetElement(m_fs.GetTypeId), FamilySymbol)
          If Not m_symb Is Nothing Then
            ElementFamilyName = m_symb.Family.Name
          End If
        End If
      Catch
        ElementFamilyName = ""
      End Try

    End Sub

  End Class
End Namespace