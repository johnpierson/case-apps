Imports Autodesk.Revit.DB

''' <summary>
''' A class to manage parameter assignments for a specified category
''' </summary>
''' <remarks></remarks>
Public Class clsParamMap

  Public Property Param_P As String
  Public Property Param_N As String
  Public Property Param_E As String
  Public Property Param_Z As String
  Public Property Param_D As String
  Public Property Param_1 As String
  Public Property Param_2 As String
  Public Property Param_3 As String
  Public Property Param_4 As String
  Public Property Param_5 As String
  Public Property CategoryName As String
  Public Property Param_Category As Category

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="c">Category Name</param>
  ''' <remarks></remarks>
  Public Sub New(c As String)
    CategoryName = c
    Param_P = ""
    Param_N = ""
    Param_E = ""
    Param_Z = ""
    Param_D = ""
    Param_1 = ""
    Param_2 = ""
    Param_3 = ""
    Param_4 = ""
    Param_5 = ""
  End Sub

End Class