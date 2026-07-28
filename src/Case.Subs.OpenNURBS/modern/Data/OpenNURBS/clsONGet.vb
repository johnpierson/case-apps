Imports RMA.OpenNURBS

Public Class clsONGet
  'Path variable

  Private _p As String

  Public Sub New(ByVal p As String)
    'Widen Scope
    _p = p

  End Sub


  Public Function GetModel() As OnXModel
    'RMA Model Object
    Dim rh_model As New OnXModel()

    'RMA error log
    Dim dump As OnTextLog = Nothing

    '3DM file path
    Dim filepath As String = _p

    'Open the archive
    Dim archive_open As OnFileHandle = OnUtil.OpenFile(filepath, "rb")

    'Create archive object from file pointer
    Dim archive As New OnBinaryFile(IOn.archive_mode.read3dm, archive_open)

    'Read the contents of the file into "model"
    Dim rc As Boolean = rh_model.Read(archive, dump)

    'Close the 3DM file
    OnUtil.CloseFile(archive_open)

    Return rh_model
  End Function

End Class
