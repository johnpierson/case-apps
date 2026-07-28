Imports Autodesk.Revit.DB
Imports System.IO
Imports System.Windows.Forms
Imports [Case].BasicReporting.API
Imports [Case].BasicReporting.Data

Public Class form_Main

  Private _s As clsSettings

  ''' <summary>
  ''' All Hosted Elements
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property AllHostedElements As List(Of clsHosts)

  ''' <summary>
  ''' All Formulas
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property AllFormulas As List(Of clsFormulas)

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="s"></param>
  ''' <remarks></remarks>
  Public Sub New(s As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = s
    AllHostedElements = New List(Of clsHosts)
    AllFormulas = New List(Of clsFormulas)

    ' Form Viz
    AllowOk()
    SetFormViz(FormViz.StandBy)
    Text = "Basic Reporting v" & _s.AppVersion

  End Sub

#Region "Form Visibility"

  Private Enum FormViz
    Processing
    StandBy
  End Enum

  ''' <summary>
  ''' Set the Form Visibility Basedon State
  ''' </summary>
  ''' <param name="p_v"></param>
  ''' <remarks></remarks>
  Private Sub SetFormViz(p_v As FormViz)
    Select Case p_v
      Case FormViz.Processing
        ProgressBar1.Show()
        ButtonCancel.Hide()
        ButtonOk.Hide()
        ButtonHelp.Hide()
      Case FormViz.StandBy
        ProgressBar1.Hide()
        ButtonCancel.Show()
        ButtonOk.Show()
    End Select
  End Sub

#End Region

#Region "Private Members"

  ''' <summary>
  ''' Perform the reporting
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub Report()

    ' Perform Queries
    If Me.CheckBoxHost.Checked = True Then GetAllFamilyInstanceHosts()
    If Me.CheckBoxFormulas.Checked = True Then GetAllFamilyFormulas()

    ' Save Location & Write File
    Dim m_path As String = ""

    Try

      ' Show the Save Dialog
      If Me.SaveFileDialog1.ShowDialog = DialogResult.OK Then

        ' Did we get a file name?
        If Not String.IsNullOrEmpty(SaveFileDialog1.FileName) Then
          m_path = Me.SaveFileDialog1.FileName
        End If

      End If

    Catch
    End Try

    If String.IsNullOrEmpty(m_path) Then
      MsgBox("No File Path to Save Report...", MsgBoxStyle.Critical, "Sad Times... :(")
    Else

      ' Write the Results
      WriteResultsToFile(m_path)

    End If

  End Sub

  ''' <summary>
  ''' Return a List of Formulas
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetAllFamilyFormulas()

    ' All Symbols
    Using col As New FilteredElementCollector(_s.Doc)
      col.OfClass(GetType(FamilySymbol))

      ' temporary
      col.OfCategory(BuiltInCategory.OST_Furniture)

      ' Iterate over each
      For Each x In col.ToElements

        Try

          ' Get the Family
          Dim m_fs As FamilySymbol = TryCast(x, FamilySymbol)
          If Not m_fs Is Nothing Then

            ' Family Doc
            Dim m_famDoc As Document = _s.Doc.EditFamily(m_fs.Family)

            Dim m_mgr As FamilyManager = m_famDoc.FamilyManager

            ' Iterate the Parameters
            For Each fp As FamilyParameter In m_mgr.Parameters

              Try

                ' Is ther a Formula?
                If Not String.IsNullOrEmpty(fp.Formula) Then

                  ' Get the Formula Object
                  Dim m_form As New clsFormulas(m_fs, fp.Definition.Name, fp.Formula, Not fp.IsInstance)
                  AllFormulas.Add(m_form)

                End If

              Catch
              End Try

            Next

          End If

        Catch
        End Try

      Next

    End Using

  End Sub

  ''' <summary>
  ''' Get All Hosted Elements
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub GetAllFamilyInstanceHosts()

    ' All Instance Elements
    Using col As New FilteredElementCollector(_s.Doc)
      col.OfClass(GetType(FamilyInstance))

      ' List of Elements
      Dim m_e As New List(Of FamilyInstance)
      For Each x In col.ToElements
        Try
          m_e.Add(x)
        Catch
        End Try
      Next

      Dim iCnt As Integer = m_e.Count

      ' Progressbar
      With Me.ProgressBar1
        .Minimum = 0
        .Value = 0
        .Maximum = iCnt
      End With

      ' Process Each Element
      For Each e In m_e

        ' Step the Progress
        Try
          ProgressBar1.Increment(1)
        Catch
        End Try

        Try

          ' Test for Face
          If Not e.HostFace Is Nothing Then

            Try
              Dim h As New clsHosts(e, e.HostFace.ElementId, clsHosts.hostType.isFace)
              AllHostedElements.Add(h)
              Continue For
            Catch
            End Try

          End If

          ' Normal Host
          If Not e.Host Is Nothing Then

            Try
              Dim h As New clsHosts(e, e.Host.Id, clsHosts.hostType.isNormal)
              AllHostedElements.Add(h)
            Catch
            End Try

          End If

        Catch
        End Try

      Next

      ' Levels
      LevelHosting()

    End Using

  End Sub

  ''' <summary>
  ''' Get all Level Inheritances
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LevelHosting()

    ' Levels
    Dim m_levels As New List(Of Level)
    Using col As New FilteredElementCollector(_s.Doc)
      col.OfClass(GetType(Level))
      For Each x In col.ToElements
        Try
          m_levels.Add(x)
        Catch
        End Try
      Next
    End Using

    Try

      ' Delete Levels One by One
      For Each x As Level In m_levels

        ' Resulting Hosted Items if Deleted
        Dim m_ids As IList(Of ElementId) = Nothing

        ' Start a New Transaction
        Using t As New Transaction(_s.Doc, "Levels")
          If t.Start Then

            Try

              ' Delete the Level
              m_ids = _s.Doc.Delete(x.Id)

              ' Hosted Elements 
              t.RollBack()

            Catch
            End Try

          End If

        End Using

        Try

          ' Test List
          For Each eid As ElementId In m_ids

            ' Avoid Self
            If eid.Value = x.Id.Value Then Continue For

            ' Get the Element
            Dim m_e As Element = _s.Doc.GetElement(eid)

            If Not m_e Is Nothing Then

              Try

                ' Add it
                AllHostedElements.Add(New clsHosts(m_e, x))

              Catch
              End Try

            End If

          Next

        Catch
        End Try

      Next

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Write Hosted Data to File
  ''' </summary>
  ''' <param name="fPath">File Path</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Private Function WriteResultsToFile(fPath As String) As String

    Try

      ' Writer
      Using sw As New StreamWriter(fPath, False)

        ' Model Name
        sw.WriteLine(_s.DocName)
        sw.WriteLine(Now.ToString)

        ' Formulas
        If Me.CheckBoxFormulas.Checked = True Then

          sw.WriteLine("")
          sw.WriteLine("")
          sw.WriteLine("Parametric Formula Report:")

          ' Header - Formulas
          Dim m_line As String = "Family,"
          m_line += "Category,"
          m_line += "Parameter,"
          m_line += "isType,"
          m_line += "Formula"
          sw.WriteLine(m_line)

          ' Write Each Item
          For Each x In AllFormulas

            Dim m_l As String = x.FamilyName & ","
            m_l += x.FamilyCategory & ", "
            m_l += x.ParamName & ", "
            m_l += x.IsTypeParameter & ", "
            m_l += x.ParamFormula

            sw.WriteLine(m_l)

          Next

        End If

        ' Hosted Stuff
        If Me.CheckBoxHost.Checked = True Then

          sw.WriteLine("")
          sw.WriteLine("")
          sw.WriteLine("Hosting Report:")

          ' Header - Hosting
          Dim m_line As String = "ElementID,"
          m_line += "ElementName,"
          m_line += "ElementFamilyName,"
          m_line += "ElementCategory,"
          m_line += "ElementHostType,"
          m_line += "HostElementID,"
          m_line += "HostElementName,"
          m_line += "HostElementCategory"
          sw.WriteLine(m_line)

          ' Write Each Item
          For Each x In AllHostedElements

            Dim m_l As String = x.ElementID & ","
            m_l += x.ElementName & ","
            m_l += x.ElementFamilyName & ","
            m_l += x.ElementCategory & ","
            m_l += x.ElementHostType & ","
            m_l += x.HostElementID & ","
            m_l += x.HostElementName & ","
            m_l += x.HostElementCategory & ","

            sw.WriteLine(m_l)

          Next

        End If

      End Using
    Catch ex As Exception

      Return ex.Message

    End Try

    Return ""

  End Function

  ''' <summary>
  ''' 1 selection required minimum
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub AllowOk()

    If Me.CheckBoxFormulas.Checked = True Or Me.CheckBoxHost.Checked = True Then

      ButtonOk.Enabled = True

    Else

      ButtonOk.Enabled = False

    End If

  End Sub

#End Region

#Region "Form Controls & Events"

  ''' <summary>
  ''' Help Button
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-basic-reporting-hosting-and-formulas")
  End Sub

  ''' <summary>
  ''' Launch Case Site on Logo Click
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

  ''' <summary>
  ''' Cancel
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click

    ' Close
    Close()

  End Sub

  ''' <summary>
  ''' Commit
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonOk_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOk.Click

    ' Form Viz
    SetFormViz(FormViz.Processing)

    ' Run Report
    Report()

    ' Close
    Close()

  End Sub

  Private Sub CheckBoxFormulas_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxFormulas.CheckedChanged
    AllowOk()
  End Sub

  Private Sub CheckBoxHost_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxHost.CheckedChanged
    AllowOk()
  End Sub

#End Region

End Class