Imports System.Data
Imports Autodesk.Revit.DB

''' <summary>
''' A Helper Class to Hold Data for Reporting
''' </summary>
''' <remarks></remarks>
Public Class clsReportingData

  Private Const cModulus As Double = 0.0174532925199433
  Private _loc As LocationKind
  Private _projXYZ As Autodesk.Revit.DB.XYZ

#Region "Public Properties"

  Public Property Elem As Element
  Public Property MyLocation As ProjectLocation
  Public Property ElementXyz As Autodesk.Revit.DB.XYZ
  Public Property ProjectXyz As Autodesk.Revit.DB.XYZ
  Public Property ParamMap As clsParamMap
  Public Property eID As String
  Public Property P As String
  Public Property N As String
  Public Property E As String
  Public Property Z As String
  Public Property D As String
  Public Property FamilyFullDisplayName As String
  Public Property FamilyName As String
  Public Property FamilyType As String
  Public Property Category As String
  Public Property LevelName As String
  Public Property DesignOptionSetName As String
  Public Property DesignOptionName As String
  Public Property DesignOptionFullDisplayName As String
  Public Property Description As String
  Public Property Param1 As String
  Public Property Param2 As String
  Public Property Param3 As String
  Public Property Param4 As String
  Public Property Param5 As String

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_e">Element to Process</param>
  ''' <param name="p_l">Location Kind Enum</param>
  ''' <param name="p_n">Project Location</param>
  ''' <param name="p_map">Parameter Map</param>
  ''' <remarks></remarks>
  Public Sub New(p_e As Element,
                 p_l As LocationKind,
                 projectXYZ As Autodesk.Revit.DB.XYZ,
                 Optional p_n As ProjectLocation = Nothing,
                 Optional p_map As clsParamMap = Nothing)

    ' Widen Scope
    Elem = p_e
    eID = Elem.Id.ToString()
    _loc = p_l
    _projXYZ = projectXYZ
    MyLocation = p_n

    ' Fresh List
    Description = ""
    P = ""
    D = ""
    Param1 = ""
    Param2 = ""
    Param3 = ""
    Param4 = ""
    Param5 = ""

    ' Read from Settings Configuration File
    If Not p_map Is Nothing Then
      If Not String.IsNullOrEmpty(p_map.Param_P) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_P))
          If Not m_para Is Nothing Then Me.P = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_D) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_D))
          If Not m_para Is Nothing Then Me.D = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_1) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_1))
          If Not m_para Is Nothing Then Param1 = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_2) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_2))
          If Not m_para Is Nothing Then Param2 = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_3) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_3))
          If Not m_para Is Nothing Then Param3 = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_4) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_4))
          If Not m_para Is Nothing Then Param4 = m_para.Value
        Catch
        End Try
      End If
      If Not String.IsNullOrEmpty(p_map.Param_5) Then
        Try
          Dim m_para As New clsPara(Elem.LookupParameter(p_map.Param_5))
          If Not m_para Is Nothing Then Param5 = m_para.Value
        Catch
        End Try
      End If
    End If

    Try

      LevelName = p_e.Document.GetElement(p_e.LevelId).Name
    Catch ex As Exception
      LevelName = "n/a"
    End Try

    If Not p_map Is Nothing Then
      ParamMap = p_map

      ' Get the Resulting Data
      GetPDdata()

    Else
      ParamMap = New clsParamMap(p_e.Category.Name)
    End If

    ' Get the Element Data
    GetElementData()

  End Sub

  ''' <summary>
  ''' Build an Item from Comma or Tab Delimited String Row
  ''' </summary>
  ''' <param name="p_s">Full String to Analyze</param>
  ''' <param name="p_s_header">Full String Header</param>
  ''' <param name="p_sep">Column Separator String</param>
  ''' <remarks></remarks>
  Public Sub New(p_s As String, p_s_header As String, p_sep As String)

    ' New Map
    ParamMap = New clsParamMap("N/A")

    ' Split into an Array - Header
    Dim m_items_h() As String = Split(p_s_header, p_sep, , CompareMethod.Text)
    ' Split into an Array - Data
    Dim m_items() As String = Split(p_s, p_sep, , CompareMethod.Text)

    ' Iterate And Assign Values
    For i = 0 To UBound(m_items_h)

      ' Name
      Dim m_ParamName As String = m_items_h(i).ToString

      If m_ParamName.ToLower = "elementid" Then
        Me.eID = m_items(i)
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower.StartsWith("p[") Then
        Try
          Me.P = m_items(i)
          ParamMap.Param_P = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("n[") Then
        Try
          Me.N = m_items(i)
          ParamMap.Param_N = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("e[") Then
        Try
          Me.E = m_items(i)
          ParamMap.Param_E = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("z[") Then
        Try
          Me.Z = m_items(i)
          ParamMap.Param_Z = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("d[") Then
        Try
          Me.D = m_items(i)
          ParamMap.Param_D = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower = "family name" Then
        Me.FamilyName = m_items(i)
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower = "family type name" Then
        Me.FamilyType = m_items(i)
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower = "design option set" Then
        Me.DesignOptionSetName = m_items(i)
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower = "design option" Then
        Me.DesignOptionName = m_items(i)
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower = "category" Then
        Me.Category = m_items(i)
        Try
          Dim m_c As Category = Elem.Document.Settings.Categories.Item(Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3))
          ParamMap.Param_Category = m_c
        Catch ex As Exception
        End Try
        GoTo DoNextItem
      End If
      If m_ParamName.ToLower = "level" Then
        Me.LevelName = m_items(i)
        GoTo DoNextItem
      End If

      If m_ParamName.ToLower.StartsWith("1[") Then
        Try
          Me.Param1 = m_items(i)
          ParamMap.Param_1 = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("2[") Then
        Try
          Me.Param2 = m_items(i)
          ParamMap.Param_2 = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("3[") Then
        Try
          Me.Param3 = m_items(i)
          ParamMap.Param_3 = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("4[") Then
        Try
          Me.Param4 = m_items(i)
          ParamMap.Param_4 = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If
      If m_ParamName.ToLower.StartsWith("5[") Then
        Try
          Me.Param5 = m_items(i)
          ParamMap.Param_5 = Mid(m_ParamName, 3, InStr(m_items_h(i).ToString, "]", CompareMethod.Text) - 3)
          GoTo DoNextItem
        Catch ex As Exception

        End Try
      End If

DoNextItem:

    Next

  End Sub

  ''' <summary>
  ''' Build an Item from a Datarow
  ''' </summary>
  ''' <param name="dr"></param>
  ''' <remarks></remarks>
  Public Sub New(dr As DataRow)

    ' New Map
    ParamMap = New clsParamMap("N/A")

    ' The List of Column Names
    Dim m_cols As New Dictionary(Of Integer, String)
    Dim iCnt As Integer = 0
    For Each col As DataColumn In dr.Table.Columns
      m_cols.Add(iCnt, col.ColumnName)
      iCnt += 1
    Next

    ' Match to Class Properties
    For i = 0 To m_cols.Values.Count - 1

      If m_cols(i).ToLower = "elementid" Then
        Try
          Me.eID = dr(m_cols(i))
        Catch ex As Exception
          Me.eID = ""
        End Try
        GoTo DoNextItem
      End If

      ' P
      If Mid(m_cols(i).ToLower, 1, 2) = "p[" Then
        Try
          Me.P = dr(m_cols(i))
          ParamMap.Param_P = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.P = ""
        End Try
        GoTo DoNextItem
      End If

      ' N
      If Mid(m_cols(i).ToLower, 1, 2) = "n[" Then
        Try
          Me.N = dr(m_cols(i))
          ParamMap.Param_N = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.N = ""
        End Try
        GoTo DoNextItem
      End If

      ' E
      If Mid(m_cols(i).ToLower, 1, 2) = "e[" Then
        Try
          Me.E = dr(m_cols(i))
          ParamMap.Param_E = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.E = ""
        End Try
        GoTo DoNextItem
      End If

      ' Z
      If Mid(m_cols(i).ToLower, 1, 2) = "z[" Then
        Try
          Me.Z = dr(m_cols(i))
          ParamMap.Param_Z = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Z = ""
        End Try
        GoTo DoNextItem
      End If

      ' D
      If Mid(m_cols(i).ToLower, 1, 2) = "d[" Then
        Try
          Me.D = dr(m_cols(i))
          ParamMap.Param_D = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.D = ""
        End Try

        GoTo DoNextItem
      End If

      ' (Family Name)
      If m_cols(i).ToLower = "(family name)" Then
        Try
          Me.FamilyName = dr(m_cols(i))
        Catch ex As Exception
          Me.FamilyName = ""
        End Try
        GoTo DoNextItem
      End If

      ' (Family Type Name)
      If m_cols(i).ToLower = "(family type name)" Then
        Try
          Me.FamilyType = dr(m_cols(i))
        Catch ex As Exception
          Me.FamilyType = ""
        End Try
        GoTo DoNextItem
      End If

      ' (Design Option Set)
      If m_cols(i).ToLower = "(design option set)" Then
        Try
          Me.DesignOptionSetName = dr(m_cols(i))
        Catch ex As Exception
          Me.DesignOptionSetName = ""
        End Try
        GoTo DoNextItem
      End If

      ' (Design Option)
      If m_cols(i).ToLower = "(design option)" Then
        Try
          Me.DesignOptionName = dr(m_cols(i))
        Catch ex As Exception
          Me.DesignOptionName = ""
        End Try
        GoTo DoNextItem
      End If

      ' (Category)
      If m_cols(i).ToLower = "(category)" Then
        Try
          Me.Category = dr(m_cols(i))
        Catch ex As Exception
          Me.Category = ""
        End Try
        GoTo DoNextItem
      End If

      ' (Level)
      If m_cols(i).ToLower = "(level)" Then
        Try
          Me.LevelName = dr(m_cols(i))
        Catch ex As Exception
          Me.LevelName = ""
        End Try
        GoTo DoNextItem
      End If

      ' 1
      If Mid(m_cols(i).ToLower, 1, 2) = "1[" Then
        Try
          Me.Param1 = dr(m_cols(i))
          ParamMap.Param_1 = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Param1 = ""
        End Try

        GoTo DoNextItem
      End If
      ' 2
      If Mid(m_cols(i).ToLower, 1, 2) = "2[" Then
        Try
          Me.Param2 = dr(m_cols(i))
          ParamMap.Param_2 = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Param2 = ""
        End Try

        GoTo DoNextItem
      End If
      ' 3
      If Mid(m_cols(i).ToLower, 1, 2) = "3[" Then
        Try
          Me.Param3 = dr(m_cols(i))
          ParamMap.Param_3 = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Param3 = ""
        End Try

        GoTo DoNextItem
      End If
      ' 4
      If Mid(m_cols(i).ToLower, 1, 2) = "4[" Then
        Try
          Me.Param4 = dr(m_cols(i))
          ParamMap.Param_4 = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Param4 = ""
        End Try

        GoTo DoNextItem
      End If
      ' 5
      If Mid(m_cols(i).ToLower, 1, 2) = "5[" Then
        Try
          Me.Param5 = dr(m_cols(i))
          ParamMap.Param_5 = Mid(m_cols(i), 3, InStr(m_cols(i).ToString, "]", CompareMethod.Text) - 3)
        Catch ex As Exception
          Me.Param5 = ""
        End Try

        GoTo DoNextItem
      End If

DoNextItem:
    Next

  End Sub

  ''' <summary>
  ''' Data Reporting Variables
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub GetElementData(Optional isNew As Boolean = False)

    Try ' Family Name
      If TypeOf Elem Is FamilyInstance Then
        Dim m_fam As FamilyInstance = TryCast(Elem, FamilyInstance)
        If Not m_fam Is Nothing Then

          ' Get the Type
          Dim m_type_e As Element = Elem.Document.GetElement(m_fam.GetTypeId)
          Dim m_t As FamilySymbol = TryCast(m_type_e, FamilySymbol)

          If Not m_t Is Nothing Then
            FamilyName = m_t.Family.Name
          Else
            FamilyName = ""
          End If


          FamilyType = Elem.Name
          FamilyFullDisplayName = "[" & Elem.Category.Name & "] " & FamilyName
        End If
      Else
        FamilyName = Elem.Category.Name
        FamilyType = Elem.Name
        FamilyFullDisplayName = "[" & Elem.Category.Name & "] " & Elem.Name
      End If
    Catch ex As Exception
      FamilyName = ""
      FamilyType = ""
      FamilyFullDisplayName = "Error"
      Exit Sub
    End Try

    Try ' Category
      Category = Elem.Category.Name
    Catch ex As Exception
      Category = "N/a"
    End Try

    Try ' Level
      If Elem.LevelId.Value > 1 Then
        LevelName = Elem.Document.GetElement(Elem.LevelId).Name
      Else
        LevelName = "N/a"
      End If
    Catch ex As Exception
      LevelName = "N/a"
    End Try

    Try ' Design Option
      If Not Elem.DesignOption Is Nothing Then
        Dim m_optionSet As Element
        m_optionSet = Elem.Document.GetElement(New ElementId(Elem.Parameter(BuiltInParameter.OPTION_SET_ID).AsElementId.Value))
        DesignOptionSetName = m_optionSet.Name
        DesignOptionName = Elem.DesignOption.Name
        DesignOptionFullDisplayName = "[" & m_optionSet.Name & "] " & Elem.DesignOption.Name
      Else
        DesignOptionSetName = ""
        DesignOptionName = ""
        DesignOptionFullDisplayName = "N/a"
      End If
    Catch ex As Exception
      DesignOptionSetName = ""
      DesignOptionName = ""
      DesignOptionFullDisplayName = "N/a"
    End Try

    ' Done on New Item
    If isNew = True Then Exit Sub

    ' Update the XYZ
    GetPointFromElement()

    ' Update to Named Location
    If Not MyLocation Is Nothing Then
      Try

        Dim m_pp As ProjectPosition = MyLocation.GetProjectPosition(New Autodesk.Revit.DB.XYZ(0, 0, 0))

        ' New Transform, adding the Rotation
        Dim m_transform As Transform = MyLocation.GetTransform

        Dim m_rt As Transform = Transform.CreateRotationAtPoint(New Autodesk.Revit.DB.XYZ(0, 0, 1),
                                                                m_pp.Angle,
                                                                New Autodesk.Revit.DB.XYZ(0, 0, 0))

        ' Get the Translated Point
        ElementXyz = m_rt.OfPoint(ProjectXyz)

        ' Update Properties
        Dim dN As Decimal = CDec(Math.Round(ElementXyz.Y + _projXYZ.Y, 5))
        Dim dE As Decimal = CDec(Math.Round(ElementXyz.X + _projXYZ.Y, 5))
        Dim dZ As Decimal = CDec(Math.Round(ElementXyz.Z + _projXYZ.Y, 5))

        Me.N = dN.ToString
        Me.E = dE.ToString
        Me.Z = dZ.ToString

      Catch ex As Exception
        MsgBox("failed to retrieve transform point data", MsgBoxStyle.Critical, "CRAP")
      End Try

    End If

  End Sub

#Region "Private Members"

  ''' <summary>
  ''' Get the Data - Read
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetPDdata()
    If Not String.IsNullOrEmpty(ParamMap.CategoryName) Then

      ' Value for P
      Dim m_p As Parameter = Elem.LookupParameter(ParamMap.Param_P)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.P = m_para.Value
      End If

      ' Get D
      m_p = Elem.LookupParameter(ParamMap.Param_D)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.D = m_para.Value
      End If

      ' Get 1 - 5
      m_p = Elem.LookupParameter(ParamMap.Param_1)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.Param1 = m_para.Value
      End If
      m_p = Elem.LookupParameter(ParamMap.Param_2)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.Param2 = m_para.Value
      End If
      m_p = Elem.LookupParameter(ParamMap.Param_3)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.Param3 = m_para.Value
      End If
      m_p = Elem.LookupParameter(ParamMap.Param_4)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.Param4 = m_para.Value
      End If
      m_p = Elem.LookupParameter(ParamMap.Param_5)
      If Not m_p Is Nothing Then
        Dim m_para As New clsPara(m_p)
        Me.Param5 = m_para.Value
      End If

    End If

  End Sub

  ''' <summary>
  ''' Update the XYZ from the Element Instance
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetPointFromElement()

    ' Need Elem
    If Elem Is Nothing Then Exit Sub

    ' Get the XYZ Location
    Dim m_location As Location = Elem.Location

    ' Continue if we have a location
    If Not m_location Is Nothing Then

      Try

        ' Get the XYZ object and Strings
        ElementXyz = DirectCast(m_location, LocationPoint).Point
        ProjectXyz = ElementXyz

        ' Rounding
        If Not ElementXyz Is Nothing Then
          Dim dE As Decimal = CDec(Math.Round(ElementXyz.X, 5))
          Dim dN As Decimal = CDec(Math.Round(ElementXyz.Y, 5))
          Dim dZ As Decimal = CDec(Math.Round(ElementXyz.Z, 5))

          E = dE.ToString
          N = dN.ToString
          Z = dZ.ToString
        End If

      Catch ex As Exception

        E = "Error"
        N = "Error"
        Z = "Error"

      End Try

    End If

  End Sub

#End Region

End Class