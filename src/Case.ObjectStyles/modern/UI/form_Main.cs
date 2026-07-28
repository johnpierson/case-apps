using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ObjectStyles.Data;
using Microsoft.VisualBasic.ApplicationServices;
using clsSettings = Case.ObjectStyles.Data.clsSettings;
using Form = System.Windows.Forms.Form;

namespace Case.ObjectStyles.UI
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
      progressBar1.Hide();
    }

    #region Private Members

    /// <summary>
    ///   Get the Data
    /// </summary>
    private void ProcessData()
    {
      try
      {
        //Populate Lists of Categoreies split by function
        _s.OrganizeCategorySets();
      }
      catch
      {
        using (var x = new Autodesk.Revit.UI.TaskDialog("Error"))
        {
          x.TitleAutoPrefix = false;
          x.MainContent = "Could not capture Category Data. Contact CASE.";
          x.MainInstruction = "Error on Data Capture";
          x.Show();
        }

        Close();
      }

      if (FolderBrowser1.ShowDialog() == DialogResult.OK)
      {
        // Save the Data to File
        if (!string.IsNullOrEmpty(FolderBrowser1.SelectedPath))
        {
          string path = Path.Combine(FolderBrowser1.SelectedPath, _s.DocTitle());
          //Export Annotation Categories if Checked
          if (AnnotationCatCheck.Checked)
          {
            try
            {
              _s.SaveDataToCsv(path + "-Annoation-OS.txt", _s.AnnoCatSet);
            }
            catch (Exception exception)
            {
              Debug.WriteLine(exception.Message);
            }
          }

          //Export Analytical Categories if Checked
          if (AnalyticalCatCheck.Checked)
          {
            try
            {
              _s.SaveDataToCsv(path + "-Analytical-OS.txt", _s.AnalyticalCatSet);
            }
            catch (Exception exception)
            {
              Debug.WriteLine(exception.Message);
            }
          }

          //Export Model Categories if Checked
          if (ModelCatCheck.Checked)
          {
            try
            {
              _s.SaveDataToCsv(path + "-Model-OS.txt", _s.ModelCatSet);
            }
            catch (Exception exception)
            {
              Debug.WriteLine(exception.Message);
            }
          }
          progressBar1.Increment(1);

          if (FamSourceCheckBox.Checked)
          {
            var famList = new List<Family>();

            if (AnnotationCatCheck.Checked)
            {
              progressBar1.Value = 0;

              try
              {
                foreach (Category c in _s.AnnoCatSet)
                {
                  try
                  {
                    List<Family> catFams = _s.FamiliesByCategory(c);
                    famList.AddRange(catFams);
                    progressBar1.Maximum += catFams.Count;
                  }
                  catch (Exception exception)
                  {
                    Debug.WriteLine(exception.Message);
                  }
                }

                foreach (Family f in famList)
                {
                  progressBar1.Increment(1);
                  Document fd = _s.ActiveDoc.EditFamily(f);
                  _s.FamReport.Add(new clsFamData(fd, f));
                }
                _s.SaveFamDataToCsv(path + "-FamilySource-Annotation-OS.txt");
                famList.Clear();
              }
              catch (Exception exception)
              {
                Debug.WriteLine(exception.Message);
              }
            }

            if (ModelCatCheck.Checked)
            {
              progressBar1.Value = 0;

              try
              {
                foreach (Category c in _s.ModelCatSet)
                {
                  try
                  {
                    List<Family> catFams = _s.FamiliesByCategory(c);
                    famList.AddRange(catFams);
                    progressBar1.Maximum += catFams.Count;
                  }
                  catch (Exception exception)
                  {
                    Debug.WriteLine(exception.Message);
                  }
                }

                foreach (Family f in famList)
                {
                  progressBar1.Increment(1);
                  Document fd = _s.ActiveDoc.EditFamily(f);
                  _s.FamReport.Add(new clsFamData(fd, f));
                }

                _s.SaveFamDataToCsv(path + "-FamilySource-Model-OS.txt");
                famList.Clear();
              }
              catch (Exception exception)
              {
                Debug.WriteLine(exception.Message);
              }
            }
          }
        }
      }
      else
      {
        using (var x = new Autodesk.Revit.UI.TaskDialog("No Results"))
        {
          x.TitleAutoPrefix = false;
          x.MainContent = "No Object Styles exist? Well somethings off there. Contact CASE.";
          x.MainInstruction = "Nothing to Report";
          x.Show();
        }
      }

      // Close
      Close();
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

      // Progress Bar
      buttonOk.Hide();
      buttonCancel.Hide();
      ButtonHelp.Hide();
      progressBar1.Show();
      progressBar1.Minimum = 0;
      progressBar1.Maximum = 1;
      progressBar1.Value = 0;
      ProcessData();
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

    private void AnnotationCatCheck_CheckedChanged(object sender, EventArgs e)
    {
      buttonOk.Enabled = (AnnotationCatCheck.Checked || ModelCatCheck.Checked || AnalyticalCatCheck.Checked);
      FamSourceCheckBox.Enabled = (AnnotationCatCheck.Checked || ModelCatCheck.Checked);
    }

    private void ModelCatCheck_CheckedChanged(object sender, EventArgs e)
    {
      buttonOk.Enabled = (AnnotationCatCheck.Checked || ModelCatCheck.Checked || AnalyticalCatCheck.Checked);
      FamSourceCheckBox.Enabled = (AnnotationCatCheck.Checked || ModelCatCheck.Checked);
    }

    private void AnalyticalCatCheck_CheckedChanged(object sender, EventArgs e)
    {
      buttonOk.Enabled = (AnnotationCatCheck.Checked || ModelCatCheck.Checked || AnalyticalCatCheck.Checked);
    }

    private void FamSourceCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      const string defaultVal = @"Include Component Family Styles";
      const string warning = @"  (This will take some time)";

      FamSourceCheckBox.Text = FamSourceCheckBox.Checked ? defaultVal + warning : defaultVal;
    }

    #endregion

    private void HelpButton_Click(object sender, EventArgs e)
    {
      Process.Start("http://apps.case-inc.com/content/free-object-styles-exporter");
    }
  }
}