using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Microsoft.VisualBasic.ApplicationServices;
using clsSettings = Case.FamilySubcategories.Data.clsSettings;

namespace Case.FamilySubcategories.UI
{
  public partial class form_Main : Form
  {
    private readonly clsSettings _s;

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="s"></param>
    public form_Main(clsSettings s)
    {
      InitializeComponent();

      // Widen Scope
      _s = s;
    }

    #region Private Members

    private List<string> _fList = new List<string>(); //Family Path List 
    private string FolderPath { get; set; } //The folder
    
    /// <summary>
    /// Just the Text Headers
    /// </summary>
    private string HeaderTabs
    {
      get
      {
        return "Category\tFamily\tSource\tProjection Lineweight\tCut Lineweight\tLine Color\tMaterial\tSub-Categories";
      }
    }

    /// <summary>
    ///   Get the Data
    /// </summary>
    private void ProcessData()
    {
      try
      {
        if (_fList == null) return;
        if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
        if (saveFileDialog1.FileName == null) return;

        using (var sw = new StreamWriter(saveFileDialog1.FileName, false))
        {
          bool isFirstRow = true;

          for (int i = 0; i < _fList.Count; i++)
          {
            if (isFirstRow)
            {
              sw.WriteLine(HeaderTabs);
              isFirstRow = false;
            }
            string f = _fList[i];
            label1.Text = string.Format("Processing: {0}", Path.GetFileName(f));
            label1.Update();
            sw.WriteLine(_s.ProcessFamily(f));
            progressBar1.Increment(1);
          }

          sw.Close();
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        using (var x = new Autodesk.Revit.UI.TaskDialog("Error"))
        {
          x.TitleAutoPrefix = false;
          x.MainContent = "This blew up badly. Im not even sure what happened. Contact CASE.";
          x.MainInstruction = "I died, Jim.";
          x.Show();
        }
      }
      Close();
    }

    /// <summary>
    ///   Checks if there are any SubFolders
    /// </summary>
    /// <param name="path">The parent folder</param>
    /// <returns>bool if folders are present</returns>
    private bool HasSubfolder(string path)
    {
      var directory = new DirectoryInfo(path);
      DirectoryInfo[] subdirs = directory.GetDirectories();
      return (subdirs.Length != 0);
    }

    /// <summary>
    ///   Just a method for error processing
    /// </summary>
    /// <param name="path"></param>
    private void PathDoesNotExist(string path)
    {
      buttonOk.Enabled = false;

      if (string.IsNullOrEmpty(path)) path = "Empty Value";

      using (var x = new Autodesk.Revit.UI.TaskDialog("Error"))
      {
        x.TitleAutoPrefix = false;
        x.MainContent = "Please browse to a valid path. \nInvalid Path: " + path;
        x.MainInstruction = "Invalid";
        x.Show();
      }
    }

    /// <summary>
    ///   Checks that the Directory is a Valid Path
    /// </summary>
    /// <param name="p">The Path</param>
    /// <returns>bool for Valid</returns>
    private bool TestPathInput(string p)
    {
      try
      {
        if (!Directory.Exists(p))
        {
          PathDoesNotExist(p);
          return false;
        }
        FolderPath = Path.GetFullPath(p);

        CheckSubFolders.Enabled = HasSubfolder(p);

        buttonOk.Enabled = true;

        return true;
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        PathDoesNotExist(p);
        return false;
      }
    }

    /// <summary>
    /// Give the user one last chance to get of it
    /// </summary>
    /// <returns>Go Forth?</returns>
    private bool LastChanceExit()
    {
      using (var x = new Autodesk.Revit.UI.TaskDialog("Warning"))
      {
        x.TitleAutoPrefix = false;
        x.MainIcon = Autodesk.Revit.UI.TaskDialogIcon.TaskDialogIconWarning;

        if (_fList.Any())
        {
          x.MainContent = "You are procesing " + _fList.Count +
                          " Families.\nThis will take approx. " +
                          Math.Round(TimeSpan.FromSeconds(_fList.Count*.5).TotalMinutes) +
                          " Minutes.\n" +
                          "This is your last chance to cancel this process.";
          x.MainInstruction = "Long wait time ahead.";
          x.CommonButtons = Autodesk.Revit.UI.TaskDialogCommonButtons.Ok | Autodesk.Revit.UI.TaskDialogCommonButtons.Cancel;
        }
        else
        {
          x.MainContent = "No Families Found in the selected directory";
          x.MainInstruction = "No Families Found";
          x.CommonButtons = Autodesk.Revit.UI.TaskDialogCommonButtons.Cancel;
        }
        Autodesk.Revit.UI.TaskDialogResult tdr = x.Show();
        return tdr == Autodesk.Revit.UI.TaskDialogResult.Ok;
      }
    }

    #endregion

    #region Form Controls & Events

    /// <summary>
    ///   OK
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonOk_Click(object sender, EventArgs e)
    {
      try
      {
        // Assembly Data for Tracking
        var m_a = new AssemblyInfo(Assembly.GetExecutingAssembly());

        // Usage Tracking        
////         var m_usage = new clsUsageFree("spicey", m_a);
      }
      catch (Exception exception)
      {
        Debug.WriteLine(exception.Message);
      }

      // Progress Bar Shuffle
      buttonOk.Hide();
      buttonCancel.Hide();
      ButtonHelp.Hide();

      _fList = _s.FamiliesToProcess(FolderPath, CheckSubFolders.Checked);

      progressBar1.Show();
      progressBar1.Minimum = 0;
      progressBar1.Value = 0;
      progressBar1.Maximum = _fList.Count;
      groupBox2.Hide();
      label1.Show();
      FormBorderStyle = FormBorderStyle.None;
      SizeGripStyle = SizeGripStyle.Hide;
      MinimumSize = new Size(400, 80);
      Size = new Size(400, 80);

      if (LastChanceExit())
      {
        ProcessData();
      }

      Close();
    }

    /// <summary>
    ///   Close
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    /// <summary>
    /// Goto the CaseApps page when someone needs help
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HelpButton_Click(object sender, EventArgs e)
    {
      Process.Start("http://apps.case-inc.com/content/free-family-subcategories");
    }

    /// <summary>
    /// The Event when someone Clicks Browse
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonFolderBrowse_Click(object sender, EventArgs e)
    {
      if (FolderBrowser1.ShowDialog() != DialogResult.OK) return;
      TestPathInput(FolderBrowser1.SelectedPath);
      TextboxFamilyPath.Text = FolderPath;
    }

    /// <summary>
    /// The Evenet when someone changes the Text box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TextboxFamilyPath_TextChanged(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(TextboxFamilyPath.Text)) return;
      if (TestPathInput(TextboxFamilyPath.Text)) return;
      TextboxFamilyPath.Focus();
    }

    /// <summary>
    /// What happens when the form loads 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void form_Main_Load(object sender, EventArgs e)
    {
      progressBar1.Hide();
      label1.Hide();
      buttonOk.Enabled = false;
    }

    #endregion

  }
}