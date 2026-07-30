Imports System.Net

Namespace API

  Module clsUsage

#Region "Private Members"
    
    ''' <summary>
    ''' Full Qualifying Domain Name
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFullDomainName() As String
      Try

        ' Domain
        Dim m_domainName As String = NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName

        ' Host
        Dim m_hostName As String = Dns.GetHostName()

        If Not m_hostName.Contains(m_domainName) Then
          ' add the domain name part, if the hostname does not already include the domain name
          m_hostName = Convert.ToString(m_hostName & Convert.ToString(".")) & m_domainName
        End If

        Return m_hostName

      Catch
      End Try

      ' Failure
      Return ""

    End Function

    ''' <summary>
    ''' Credential
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCred() As NetworkCredential
      Return New NetworkCredential("somclient", "f9d4269b718121954503fa58dcb3c18e4dc59c32")
    End Function

    ''' <summary>
    ''' API Header
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHeader() As String
      Return "http://api.addins.case-apps.com/"
    End Function

    ''' <summary>
    ''' Public IP Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPublicIp() As String
      Try

        Const c_url As String = "http://checkip.dyndns.org"
        Dim req As WebRequest = WebRequest.Create(c_url)
        Using resp As WebResponse = req.GetResponse()
          Dim sr As New IO.StreamReader(resp.GetResponseStream())
          Dim response As String = sr.ReadToEnd().Trim()
          Dim a As String() = response.Split(":")
          Dim a2 As String = a(1).Substring(1)
          Dim a3 As String() = a2.Split("<")
          Dim a4 As String = a3(0)
          If Len(a4) > 30 Then
            Return "blocked!"
          End If
          Return a4
        End Using

      Catch
      End Try

      ' Failure
      Return "UNKNOWN"

    End Function

#End Region

#Region "Public Members"
    
    ''' <summary>
    ''' Perform the Setup
    ''' </summary>
    ''' <param name="useProxy"></param>
    ''' <remarks></remarks>
    Public Sub RecordUsage(Optional useProxy As Boolean = False)

      ' Ignore My Calls
      If Environment.MachineName.ToLower = "case-platinum" And Environment.UserName.ToLower = "d.rudder" Then Exit Sub
      If Environment.MachineName.ToLower = "masterdonpc" And Environment.UserName.ToLower = "masterdon" Then Exit Sub

      Try
        
        Dim m_url As String = GetHeader() & "logusage?ip=" & GetPublicIp()
        m_url += "&mac=" & GetFullDomainName()
        m_url += "&netb=" & Environment.MachineName
        m_url += "&appn=" & My.Application.Info.Title.ToString
        m_url += "&appv=" & My.Application.Info.Version.ToString
        m_url += "&appf=" & "report"
        m_url += "&user=" & Environment.UserName
        m_url += "&mn=" & ""

        ' The Request Call
        Dim m_req As HttpWebRequest = DirectCast(HttpWebRequest.Create(m_url), HttpWebRequest)
        With m_req
          .Credentials = GetCred()
          .Method = "POST"
        End With
        If useProxy = False Then m_req.Proxy = Nothing

        ' Response String
        m_req.GetResponse()

      Catch
        Try
          If useProxy = False Then RecordUsage(True)
        Catch
        End Try
      End Try

    End Sub

#End Region

  End Module
End Namespace