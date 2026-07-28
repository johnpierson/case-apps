Imports Autodesk.Revit.DB
Imports System.IO
Imports System.Linq
Imports [Case].ImageToDraftingView.Data

Public Class form_Main

  Private _SelectedImg() As String
  Private _width As Double
  Private _height As Double

  Private _s As clsSettings

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s

    ' Form Settings
    SetFormViz(FormState.isStandby)
    Text = "Import Image Files to Drafting Views " & _s.Version

  End Sub

#Region "Form Viz"

  Private Enum FormState
    isStandby
    isProcessing
  End Enum

  ''' <summary>
  ''' Set the Form Visibility State
  ''' </summary>
  ''' <param name="p_state"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(p_state As FormState)
    Select Case p_state
      Case FormState.isProcessing
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonHelp.Hide()
        ButtonSelectImages.Hide()
      Case FormState.isStandby
        ProgressBar1.Hide()
        ButtonCancel.Show()
        ButtonSelectImages.Show()
    End Select
  End Sub

#End Region

  ''' <summary>
  ''' Import Images
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub ImportImages()

    ' Select the files
    OpenFileDialogImages.ShowDialog()
    If OpenFileDialogImages.FileNames.Count = 0 Then
      MsgBox("Nothing to Process!", MsgBoxStyle.Critical, "Terminating")
      Close()
    End If
    _SelectedImg = OpenFileDialogImages.FileNames
    With ProgressBar1
      .Minimum = 0
      .Value = 0
      .Maximum = OpenFileDialogImages.FileNames.Count
    End With
    If TextBoxWidth.Text <> "" Then
      _width = (TextBoxWidth.Text / 12)
    Else
      _width = 0
    End If
    If TextBoxHeight.Text <> "" Then
      _height = (TextBoxHeight.Text / 12)
    Else
      _height = 0
    End If

    ' Collect the list of drafting views
    Dim m_v As IEnumerable(Of Element) = From e In New FilteredElementCollector(_s.Doc) _
                                         .OfClass(GetType(ViewDrafting))
                                         Select e

    ' Drafting View Type
    Dim m_vType As ViewFamilyType = (From f In New FilteredElementCollector(_s.Doc) _
                                     .OfClass(GetType(ViewFamilyType)) _
                                     .Cast(Of ViewFamilyType)()
                                     Where f.ViewFamily = ViewFamily.Drafting).First()

    Dim m_views As List(Of Element) = m_v.ToList()
    
    ' Start adding the views and importing the images
    For i = 0 To UBound(_SelectedImg)

      Try
        ' Step Progress
        ProgressBar1.Increment(1)
      Catch
      End Try

      Dim m_imgFileName As String = Path.GetFileNameWithoutExtension(_SelectedImg(i))

      ' Only create the view if it does not already exist
      For Each el As Element In m_views
        If el.Name.ToUpper = m_imgFileName.ToUpper Then Continue For
      Next

      ' Only continue here if the view does not exist
      Dim dv As ViewDrafting = ViewDrafting.Create(_s.Doc, m_vType.Id)
      Try
        dv.Name = m_imgFileName
      Catch
      End Try

      ' Link the image
      Try
        ImportJPG(_SelectedImg(i), dv)
      Catch ex As Exception
        MsgBox("Error Importing '" & _SelectedImg(i) & "'" & vbCr &
               "Image Format not Supported in API",
               MsgBoxStyle.Information,
               "Skipping an Image Import")
      End Try

    Next

  End Sub

  ''' <summary>
  ''' Import a JPG
  ''' </summary>
  ''' <param name="m_importFileFullName"></param>
  ''' <param name="m_View"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function ImportJPG(ByVal m_importFileFullName As String, ByVal m_View As ViewDrafting) As Boolean

    ' Get the last elementID
    Dim eCnt As Integer = _s.GetElementCount()

        ' Setup the image import options
        Dim m_ImgOpt As New ImagePlacementOptions
        m_ImgOpt.PlacementPoint = BoxPlacement.Center

      #If Revit2020 Or Revit2021 Or Revit2022
         Dim m_ImgTypeOpt As New ImageTypeOptions
#End If
      #If Revit2023 Or Revit2025 Or Revit2026 Or Revit2027
      Dim m_ImgTypeOpt As ImageTypeOptions
#End If

        m_ImgTypeOpt.SetPath(m_importFileFullName)

        Dim m_imageType = ImageType.Create(_s.Doc, m_ImgTypeOpt)

        ' Import the image
        Dim imported As Boolean = ImageInstance.Create(_s.Doc, m_View, m_imageType.Id, m_ImgOpt) IsNot Nothing

        ' Test if user requires the width or height overriden
        If _height > 0 Or _width > 0 Then

      ' Get the imported image element
      Dim m_elems As New List(Of Element)
      m_elems = _s.GetElementsAfter(eCnt, _s.Doc)

      ' Get the newest element
      For Each e As Element In m_elems

        Try

          ' Test to see if it is a rasterimage
          If e.Category.Name.ToUpper = "RASTER IMAGES" Then

            ' Set the Width
            Dim m_w As Parameter = e.LookupParameter("Width")
            Dim m_w_p As New clsPara(m_w)
            If _width > 0 Then
              m_w_p.Value = _width
            End If

            ' Set the Height
            Dim m_h As Parameter = e.LookupParameter("Height")
            Dim m_h_p As New clsPara(m_h)
            If _height > 0 Then
              m_h_p.Value = _height
            End If

          End If

        Catch
        End Try

      Next

    End If

    ' Result
    Return imported

  End Function

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-import-images-drafting-views")
  End Sub

  ''' <summary>
  ''' Case Site
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  ''' <summary>
  ''' Image Selection
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonSelectImages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectImages.Click

    ' Form Vix
    SetFormViz(FormState.isProcessing)

    ' New Transaction
    Using t As New Transaction(_s.Doc, "Image to Drafting Views")
      If t.Start Then
        Try

          ' Import the Images
          ImportImages()

          ' Commit the transaction
          t.Commit()

        Catch
        End Try

      End If
    End Using

    ' Close the Form
    Close()

  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Close()
  End Sub

  ''' <summary>
  ''' Only Allow Numbers
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxHeight.KeyPress
    Dim allowedChars As String = "0123456789."
    If allowedChars.IndexOf(e.KeyChar) = -1 Then
      ' Invalid Character
      e.Handled = True
    End If
  End Sub

  ''' <summary>
  ''' Only Allow Numbers
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub TextBoxWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxWidth.KeyPress
    Dim allowedChars As String = "0123456789."
    If allowedChars.IndexOf(e.KeyChar) = -1 Then
      ' Invalid Character
      e.Handled = True
    End If
  End Sub

#End Region

End Class