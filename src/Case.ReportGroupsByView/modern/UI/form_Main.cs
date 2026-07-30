using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.ReportGroupsByView.Data;
using Microsoft.VisualBasic.ApplicationServices;

namespace Case.ReportGroupsByView.UI
{
  public partial class form_Main : System.Windows.Forms.Form
  {

    private clsSettings _s = null;

    /// <summary>
    /// Constructor
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
    /// Get the Data
    /// </summary>
    private void ProcessData()
    {
      foreach (Autodesk.Revit.DB.View v in _s.Views)
      {
        try
        {
          this.progressBar1.Increment(1);
        }
        catch { }
        _s.ActiveUIDoc.ActiveView = v;
        try
        {
          List<Group> m_groups = _s.GetGroupsByView(v, this.checkBoxGroupsDetail.Checked, this.checkBoxGroupsModel.Checked);
          if (m_groups.Count > 0)
          {
            _s.GroupsByView.Add(v.Id.Value, m_groups);
          }
        }
        catch { }
        foreach (UIView x in _s.ActiveUIDoc.GetOpenUIViews())
        {
          try
          {
            if (x.ViewId.Value != v.Id.Value) x.Close();
          }
          catch { }
        }
      }
      if (_s.GroupsByView.Count > 0)
      {
        if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          // Save the Data to File
          if (!string.IsNullOrEmpty(this.saveFileDialog1.FileName))
          {
            _s.SaveDataToCSV(this.saveFileDialog1.FileName);
          }
        }
      }
      else
      {
        using (Autodesk.Revit.UI.TaskDialog x = new Autodesk.Revit.UI.TaskDialog("No Results"))
        {
          x.TitleAutoPrefix = false;
          x.MainContent = "No views found in any views";
          x.MainInstruction = "Nothing to Report";
          x.Show();
        }
      }

      // Close
      this.Close();

    }

    #endregion

    #region Form Controls & Events

    /// <summary>
    /// Startup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void form_Main_Load(object sender, EventArgs e)
    {
      Text = string.Format(
        "Free Report Groups v{0}",
        Assembly.GetExecutingAssembly().GetName().Version);
    }

    /// <summary>
    /// OK
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonOk_Click(object sender, EventArgs e)
    {

      try
      {

        // Assembly Data for Tracking
        AssemblyInfo m_a = new AssemblyInfo(Assembly.GetExecutingAssembly());


      }
      catch { }

      // Progress Bar
      this.buttonOk.Hide();
      this.buttonCancel.Hide();
      this.progressBar1.Show();
      this.progressBar1.Minimum = 0;
      this.progressBar1.Maximum = _s.Views.Count;
      this.progressBar1.Value = 0;
      ProcessData();

    }

    /// <summary>
    /// Close
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void checkBoxGroupsDetail_CheckedChanged(object sender, EventArgs e)
    {
      this.buttonOk.Enabled = (checkBoxGroupsDetail.Checked || checkBoxGroupsModel.Checked);
    }

    private void checkBoxGroupsModel_CheckedChanged(object sender, EventArgs e)
    {
      this.buttonOk.Enabled = (checkBoxGroupsDetail.Checked || checkBoxGroupsModel.Checked);
    }

    #endregion

  }
}