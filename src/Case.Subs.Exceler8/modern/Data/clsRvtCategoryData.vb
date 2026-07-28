Imports System.Windows.Forms
Imports Autodesk.Revit.DB
Imports System.Linq

Namespace Data

  Public Class clsRvtCategoryData

    Private _c As Category
    Private _doc As Document
    Private _getInst As Boolean
    Private _getType As Boolean
    Private _isNumeric As Boolean
    Private _isNumericEid As Boolean
    Private _tableScope As EnumRevitElementTableType
    Private _xyz As XYZ = Nothing
    Private _userWs As New SortedDictionary(Of Integer, clsRvtWorksets)

#Region "Public Properties"

    ''' <summary>
    ''' Table Scope
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TableType As EnumRevitElementTableType
      Get
        Return _tableScope
      End Get
    End Property

    ''' <summary>
    ''' Category Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CategoryName As String
      Get
        Try
          Return _c.Name
        Catch
          Return ""
        End Try
      End Get
    End Property

    ''' <summary>
    ''' All Parameters
    '''  - Element(e) - Types(t) - Instance(i)
    '''  - Parameter Group Name as Key
    '''  - Parameters with Name as Key
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllParams As Dictionary(Of String, SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))

    ''' <summary>
    ''' Type Elements
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeElem As SortedDictionary(Of Integer, Element)

    ''' <summary>
    ''' Instance Elements
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InstElem As SortedDictionary(Of Integer, Element)

#End Region

    ''' <summary>
    ''' Category Data Helper
    ''' </summary>
    ''' <param name="c">Revit Category</param>
    ''' <param name="d">Revit Document</param>
    ''' <param name="getInstances"></param>
    ''' <param name="getTypes"></param>
    ''' <param name="isNumericValues"></param>
    ''' <param name="tableKind">Types, Instances, or both</param>
    ''' <param name="uws">User Worksets</param>
    ''' <remarks></remarks>
    Public Sub New(c As Category,
                   d As Document,
                   getInstances As Boolean,
                   getTypes As Boolean,
                   isNumericValues As Boolean,
                   isNumEid As Boolean,
                   tableKind As EnumRevitElementTableType,
                   uws As SortedDictionary(Of Integer, clsRvtWorksets))

      Try

        ' Widen Scope
        _c = c
        _doc = d
        _getInst = getInstances
        _getType = getTypes
        _isNumeric = isNumericValues
        _isNumericEid = isNumEid
        _tableScope = tableKind
        _userWs = uws

        ' Setup
        Setup()

      Catch
      End Try

    End Sub

#Region "Private Members"

    ''' <summary>
    ''' Class Configuration
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setup()

      Try

        ' Fresh Parameter List
        AllParams = New Dictionary(Of String, SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
        AllParams.Add("e", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
        AllParams.Add("t", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))
        AllParams.Add("i", New SortedDictionary(Of String, SortedDictionary(Of String, clsExcelHeader)))

        ' Fresh Element Lists
        TypeElem = New SortedDictionary(Of Integer, Element)
        InstElem = New SortedDictionary(Of Integer, Element)

        ' Types
        If _getType = True Then
          Using col As New FilteredElementCollector(_doc)
            col.OfCategoryId(_c.Id)
            col.WhereElementIsElementType()
            For Each x In col.ToElements
              TypeElem.Add(x.Id.Value, x)
            Next
          End Using

        End If

        ' Instances
        If _getInst = True Then
          Using col As New FilteredElementCollector(_doc)
            col.OfCategoryId(_c.Id)
            col.WhereElementIsNotElementType()
            For Each x In col.ToElements
              InstElem.Add(x.Id.Value, x)
            Next
          End Using
        End If

      Catch
      End Try

      Try

        ' Get the Params
        GetPropertyNames()

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Parameter Data - Speed up Processing of Instances
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPropertyNames()

      Try

        ' Element Basic Data
        AllParams("e").Add("Element", New SortedDictionary(Of String, clsExcelHeader))
        AllParams("e")("Element").Add("0Key", New clsExcelHeader("Key", EnumExcelHeaderKind.isE, "Element Data", EnumCellDataType.isReadOnly))
        AllParams("e")("Element").Add("ElementID", New clsExcelHeader("ElementID", EnumExcelHeaderKind.isE, "Element", EnumCellDataType.isReadOnly))
        AllParams("e")("Element").Add("Family", New clsExcelHeader("Family", EnumExcelHeaderKind.isE, "Element", EnumCellDataType.isNormal))
        AllParams("e")("Element").Add("Type", New clsExcelHeader("Type", EnumExcelHeaderKind.isE, "Element", EnumCellDataType.isNormal))
        AllParams("e")("Element").Add("Workset", New clsExcelHeader("Workset", EnumExcelHeaderKind.isE, "Element", EnumCellDataType.isReadOnly))
        AllParams("e")("Element").Add("XYZ", New clsExcelHeader("XYZ", EnumExcelHeaderKind.isE, "Element", EnumCellDataType.isReadOnly))

        ' Get all Type Data
        For Each x In TypeElem.Values

          ' Process Each Parameter
          For Each p As Parameter In x.Parameters

            ' Definition
            If Not p.Definition Is Nothing Then
              If Not String.IsNullOrEmpty(p.Definition.Name) Then

                ' Skip none kinds
                If p.StorageType = StorageType.None Then Continue For

                ' Group Name 
                Dim m_groupName As String = LabelUtils.GetLabelForGroup(p.Definition.GetGroupTypeId())
                If Not String.IsNullOrEmpty(m_groupName) Then

                  ' Does the Group Exist?
                  If Not AllParams("t").ContainsKey(m_groupName) Then

                    ' Add the Group
                    AllParams("t").Add(m_groupName, New SortedDictionary(Of String, clsExcelHeader))

                  End If

                  ' Does the Parameter Name Already exist?
                  If Not AllParams("t")(m_groupName).ContainsKey(p.Definition.Name.ToLower) Then

                    ' Is Complex?
                    Dim m_complex = p.StorageType = StorageType.ElementId

                    ' Kind
                    Dim m_kind As EnumCellDataType = EnumCellDataType.isNormal
                    If p.IsReadOnly Then
                      m_kind = EnumCellDataType.isReadOnly
                    Else
                      If m_complex = True Then m_kind = EnumCellDataType.isComplex
                    End If

                    ' Do we have it yet?
                    If Not AllParams("t")(m_groupName).ContainsKey(p.Definition.Name.ToLower) Then

                      ' Add it
                      AllParams("t")(m_groupName).Add(p.Definition.Name.ToLower, New clsExcelHeader(p.Definition.Name, EnumExcelHeaderKind.isT, m_groupName, m_kind))

                    End If

                  End If
                End If
              End If
            End If

          Next

        Next

      Catch
      End Try

      Try

        ' Get all Instance Data
        For Each x In InstElem.Values

          Try
            ' Add Name
            If Not AllParams("i").ContainsKey("Identity Data") Then
              AllParams("i").Add("Identity Data", New SortedDictionary(Of String, clsExcelHeader))
            End If

            ' Add Name
            If Not AllParams("i")("Identity Data").ContainsKey("name") Then
              AllParams("i")("Identity Data").Add("name", New clsExcelHeader("Name", EnumExcelHeaderKind.isI, "Identity Data", EnumCellDataType.isNormal))

            End If

          Catch
          End Try

          ' Process Each Parameter
          For Each p As Parameter In x.Parameters

            ' Definition
            If Not p.Definition Is Nothing Then
              If Not String.IsNullOrEmpty(p.Definition.Name) Then

                ' Skip none kinds
                If p.StorageType = StorageType.None Then Continue For

                ' Items to Ignore
                If p.Definition.Name.ToLower = "workset" Then Continue For

                ' Group Name 
                Dim m_groupName As String = LabelUtils.GetLabelForGroup(p.Definition.GetGroupTypeId())
                If Not String.IsNullOrEmpty(m_groupName) Then

                  ' Does the Group Exist?
                  If Not AllParams("i").ContainsKey(m_groupName) Then

                    ' Add the Group
                    AllParams("i").Add(m_groupName, New SortedDictionary(Of String, clsExcelHeader))

                  End If

                  ' Does the Parameter Name Already exist?
                  If Not AllParams("i")(m_groupName).ContainsKey(p.Definition.Name.ToLower) Then

                    ' Is Complex?
                    Dim m_complex = p.StorageType = StorageType.ElementId

                    ' Kind
                    Dim m_kind As EnumCellDataType = EnumCellDataType.isNormal
                    If p.IsReadOnly Then
                      m_kind = EnumCellDataType.isReadOnly
                    Else
                      If m_complex = True Then m_kind = EnumCellDataType.isComplex
                    End If

                    ' Do we have it yet?
                    If Not AllParams("i")(m_groupName).ContainsKey(p.Definition.Name.ToLower) Then

                      ' Add it
                      AllParams("i")(m_groupName).Add(p.Definition.Name.ToLower, New clsExcelHeader(p.Definition.Name, EnumExcelHeaderKind.isI, m_groupName, m_kind))

                    End If

                  End If
                End If
              End If
            End If

          Next

        Next

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get the Value of the Parameter
    ''' </summary>
    ''' <param name="propName">Property Name</param>
    ''' <param name="isNumeric"></param>
    ''' <param name="isElementId"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPropertyValue(propName As String, isNumeric As Boolean, isElementId As Boolean, e As Element) As String

      ' Value to Return
      Dim m_val As String = ""

      Try

        ' Get the Parameter
        Dim m_p As Parameter = e.LookupParameter(propName)
        If Not m_p Is Nothing Then
          Dim m_para As New clsRvtParameter(m_p)
          If Not m_para Is Nothing Then

            ' ElementID
            If isElementId = False Then
              If m_p.StorageType = StorageType.ElementId Then
                Return m_para.ValueString
              End If
            End If

            If isNumeric = True Then
              m_val = m_para.Value
            Else
              m_val = m_para.ValueString
            End If

          End If
        End If

      Catch
      End Try

      ' Return
      Return m_val

    End Function

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Convert to Data Table
    ''' </summary>
    ''' <param name="p">Progress bar</param>
    ''' <returns>Array containing all element data matching requested element scope</returns>
    ''' <remarks></remarks>
    Public Function GetAllPropertyValues(p As ProgressBar) As Array

      ' Total Elements
      Dim m_totalElements As Integer = 0

      ' Array Sizing
      Dim m_totalFields As Integer = 0
      For Each k In AllParams

        ' All Element Data is Required
        If k.Key.ToLower = "e" Then
          m_totalFields += k.Value.Values.Sum(Function(param) param.Count)
        End If

        Select Case TableType

          Case EnumRevitElementTableType.isJustInstances
            If k.Key.ToLower = "i" Then
              m_totalElements += InstElem.Values.Count
              m_totalFields += k.Value.Values.Sum(Function(param) param.Count)
            End If

          Case EnumRevitElementTableType.isJustTypes
            If k.Key.ToLower = "t" Then
              m_totalElements += TypeElem.Values.Count
              m_totalFields += k.Value.Values.Sum(Function(param) param.Count)
            End If

          Case EnumRevitElementTableType.isAllData
            If k.Key.ToLower = "i" Then
              m_totalElements += InstElem.Values.Count
              m_totalFields += k.Value.Values.Sum(Function(param) param.Count)
            End If
            If k.Key.ToLower = "t" Then
              m_totalFields += k.Value.Values.Sum(Function(param) param.Count)
            End If

          Case Else
            Return Nothing

        End Select

      Next

      Try
        p.Increment(1)
      Catch
      End Try

      ' Dimension the Array
      Dim m_valuesArray(m_totalElements - 1, m_totalFields - 1) As String

      ' Get Values
      Select Case TableType

        Case EnumRevitElementTableType.isJustTypes
          Dim m_iRow As Integer = 0
          For Each x In TypeElem.Values
            Dim m_iCol As Integer = 0

            Try
              ' Step Progress
              p.Increment(1)
            Catch
            End Try

            ' Element Data - Types Only
            For Each param In AllParams("e")("Element").Values

              Try

                Select Case param.Name.ToLower

                  Case "key"
                    m_valuesArray(m_iRow, m_iCol) = x.UniqueId.ToString

                  Case "elementid"
                    m_valuesArray(m_iRow, m_iCol) = x.Id.ToString

                  Case "family"
                    Try
                      m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
                    Catch
                    End Try

                  Case "type"
                    Try
                      m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
                    Catch
                    End Try

                  Case "workset"
                    m_valuesArray(m_iRow, m_iCol) = "n/a"

                  Case "xyz"
                    m_valuesArray(m_iRow, m_iCol) = "n/a"

                End Select

              Catch
              End Try

              ' Next Column
              m_iCol += 1

            Next

            ' Get Each Parameter Value
            For Each grp In AllParams("t")
              For Each param In grp.Value

                Try

                  ' Value
                  m_valuesArray(m_iRow, m_iCol) = GetPropertyValue(param.Value.Name, _isNumeric, _isNumericEid, x)

                Catch
                End Try

                ' Next Column
                m_iCol += 1

              Next
            Next

            ' Next Row
            m_iRow += 1

          Next

        Case EnumRevitElementTableType.isJustInstances
          Dim m_iRow As Integer = 0
          For Each x In InstElem.Values
            Dim m_iCol As Integer = 0

            Try
              ' Step Progress
              p.Increment(1)
            Catch
            End Try

            ' Element Data - Instances Only
            For Each param In AllParams("e")("Element").Values

              Try

                ' Type Element
                Dim m_typeElem As Element = x.Document.GetElement(x.GetTypeId)

                Select Case param.Name.ToLower

                  Case "key"
                    m_valuesArray(m_iRow, m_iCol) = x.UniqueId.ToString

                  Case "elementid"
                    m_valuesArray(m_iRow, m_iCol) = x.Id.ToString

                  Case "family"
                    Try
                      If Not m_typeElem Is Nothing Then
                        m_valuesArray(m_iRow, m_iCol) = m_typeElem.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
                      Else
                        m_valuesArray(m_iRow, m_iCol) = "n/a"
                      End If
                    Catch
                    End Try

                  Case "type"
                    Try
                      If Not m_typeElem Is Nothing Then
                        m_valuesArray(m_iRow, m_iCol) = m_typeElem.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
                      Else
                        m_valuesArray(m_iRow, m_iCol) = "n/a"
                      End If
                    Catch
                    End Try

                  Case "workset"
                    Try
                      If _userWs.ContainsKey(x.WorksetId.IntegerValue) Then
                        m_valuesArray(m_iRow, m_iCol) = _userWs(x.WorksetId.IntegerValue).Name
                      End If
                    Catch
                    End Try

                  Case "xyz"
                    Try

                      ' Get the XYZ Location
                      If TypeOf x Is Autodesk.Revit.DB.Panel Then

                        ' Panel Element
                        Dim m_panel As Autodesk.Revit.DB.Panel = TryCast(x, Autodesk.Revit.DB.Panel)
                        If Not m_panel Is Nothing Then
                          _xyz = m_panel.Transform.Origin
                          m_valuesArray(m_iRow, m_iCol) = _xyz.ToString
                        End If

                      Else

                        ' Regular Location
                        If Not x.Location Is Nothing Then
                          Dim m_location As Location = x.Location
                          If TypeOf m_location Is LocationPoint Then
                            _xyz = DirectCast(m_location, LocationPoint).Point
                            If Not _xyz Is Nothing Then
                              m_valuesArray(m_iRow, m_iCol) = _xyz.ToString
                            End If
                          Else
                            m_valuesArray(m_iRow, m_iCol) = "n/a"
                          End If
                        End If

                      End If

                    Catch
                    End Try

                End Select

              Catch
              End Try

              ' Next Column
              m_iCol += 1

            Next

            ' Get Each Parameter Value
            For Each grp In AllParams("i")
              For Each param In grp.Value

                Try

                  ' Add Name
                  If param.Value.GroupName.ToLower = "identity data" And param.Value.Name.ToLower = "name" Then

                    If TypeOf x Is Area Then
                      m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                      GoTo NextColumn
                    End If
                    If TypeOf x Is Architecture.Room Then
                      m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                      GoTo NextColumn
                    End If
                    If TypeOf x Is Mechanical.Space Then
                      m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                      GoTo NextColumn
                    End If

                    ' Value from Name
                    m_valuesArray(m_iRow, m_iCol) = x.Name

                  Else

                    ' Value
                    m_valuesArray(m_iRow, m_iCol) = GetPropertyValue(param.Value.Name, _isNumeric, _isNumericEid, x)

                  End If

                Catch
                End Try

NextColumn:
                m_iCol += 1

              Next
            Next

            ' Next Row
            m_iRow += 1

          Next

        Case EnumRevitElementTableType.isAllData
          Dim m_iRow As Integer = 0
          For Each x In InstElem.Values
            Dim m_iCol As Integer = 0

            Try
              ' Step Progress
              p.Increment(1)
            Catch
            End Try

            ' Type Element
            Dim m_typeElem As Element = Nothing

            ' Element Data - Type Elements
            For Each param In AllParams("e")("Element").Values

              Try

                ' Type Element
                m_typeElem = x.Document.GetElement(x.GetTypeId)

                Select Case param.Name.ToLower

                  Case "key"
                    m_valuesArray(m_iRow, m_iCol) = x.UniqueId.ToString

                  Case "elementid"
                    m_valuesArray(m_iRow, m_iCol) = x.Id.ToString

                  Case "family"
                    Try
                      If Not m_typeElem Is Nothing Then
                        m_valuesArray(m_iRow, m_iCol) = m_typeElem.Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString
                      Else
                        m_valuesArray(m_iRow, m_iCol) = "n/a"
                      End If
                    Catch
                    End Try

                  Case "type"
                    Try
                      If Not m_typeElem Is Nothing Then
                        m_valuesArray(m_iRow, m_iCol) = m_typeElem.Parameter(BuiltInParameter.SYMBOL_NAME_PARAM).AsString
                      Else
                        m_valuesArray(m_iRow, m_iCol) = "n/a"
                      End If
                    Catch
                    End Try

                  Case "workset"
                    Try
                      If _userWs.ContainsKey(x.WorksetId.IntegerValue) Then
                        m_valuesArray(m_iRow, m_iCol) = _userWs(x.WorksetId.IntegerValue).Name
                      End If
                    Catch
                    End Try

                  Case "xyz"
                    Try

                      ' Get the XYZ Location
                      If TypeOf x Is Autodesk.Revit.DB.Panel Then

                        ' Panel Element
                        Dim m_panel As Autodesk.Revit.DB.Panel = TryCast(x, Autodesk.Revit.DB.Panel)
                        If Not m_panel Is Nothing Then
                          _xyz = m_panel.Transform.Origin
                          m_valuesArray(m_iRow, m_iCol) = _xyz.ToString
                        End If

                      Else

                        ' Regular Location
                        If Not x.Location Is Nothing Then
                          Dim m_location As Location = x.Location
                          If TypeOf m_location Is LocationPoint Then
                            _xyz = DirectCast(m_location, LocationPoint).Point
                            If Not _xyz Is Nothing Then
                              m_valuesArray(m_iRow, m_iCol) = _xyz.ToString
                            End If
                          Else
                            m_valuesArray(m_iRow, m_iCol) = "n/a"
                          End If
                        End If

                      End If

                    Catch
                    End Try

                End Select

              Catch
              End Try

              ' Next Column
              m_iCol += 1

            Next

            ' Get Each Parameter Value
            For Each pair In AllParams
              If pair.Key.ToString.ToLower = "e" Then Continue For
              For Each grp In pair.Value
                For Each param In grp.Value

                  Try

                    ' Is it a Type
                    If pair.Key.ToString.ToLower = "t" Then

                      ' Set the Field Value
                      m_valuesArray(m_iRow, m_iCol) = GetPropertyValue(param.Value.Name, _isNumeric, _isNumericEid, m_typeElem)

                    Else

                      ' Value
                      If param.Value.GroupName.ToLower = "identity data" And param.Value.Name.ToLower = "name" Then

                        If TypeOf x Is Area Then
                          m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                          GoTo NextColumnType
                        End If
                        If TypeOf x Is Architecture.Room Then
                          m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                          GoTo NextColumnType
                        End If
                        If TypeOf x Is Mechanical.Space Then
                          m_valuesArray(m_iRow, m_iCol) = x.Parameter(BuiltInParameter.ROOM_NAME).AsString
                          GoTo NextColumnType
                        End If

                        ' Value from Property
                        m_valuesArray(m_iRow, m_iCol) = x.Name

                      Else

                        ' value
                        m_valuesArray(m_iRow, m_iCol) = GetPropertyValue(param.Value.Name, _isNumeric, _isNumericEid, x)

                      End If

                    End If

                  Catch
                  End Try

NextColumnType:
                  m_iCol += 1

                Next
              Next
            Next

            ' Next Row
            m_iRow += 1

          Next

      End Select

      Try
        p.Increment(1)
      Catch
      End Try

      ' Final Result
      Return m_valuesArray

    End Function

#End Region

  End Class
End Namespace