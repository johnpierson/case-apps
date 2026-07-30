Imports Autodesk.Revit.DB

''' <summary>
''' Design Option Element
''' </summary>
''' <remarks></remarks>
Public Class clsDesignOption

#Region "Public Properties"

  ''' <summary>
  ''' Set Name, Tab, Option Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property DisplayString As String
    Get
      Try
        Return OptionSet.Name & vbTab & DesignOption.Name
      Catch ex As Exception
        Return "<All>"
      End Try
    End Get
  End Property

  ''' <summary>
  ''' Option Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property Name As String
    Get
      Return DesignOption.Name
    End Get
  End Property

  ''' <summary>
  ''' Set Name
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property SetName As String
    Get
      Return OptionSet.Name
    End Get
  End Property

  ''' <summary>
  ''' Option Set Element
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property OptionSet As Element

  ''' <summary>
  ''' DesignOption Object
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property DesignOption As DesignOption

#End Region

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Public Sub New(e As Element)

    ' Cast the Object to a Design Option Object
    DesignOption = TryCast(e, DesignOption)

    ' Get the Set Name
    OptionSet = e.Document.GetElement(New ElementId(e.Parameter(BuiltInParameter.OPTION_SET_ID).AsElementId.Value))

  End Sub

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="n"></param>
  ''' <remarks></remarks>
  Public Sub New(n As String)

    ' Widen Scope
    DesignOption = Nothing

  End Sub

End Class