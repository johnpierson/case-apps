using Autodesk.Revit.DB;
using Case.UngroupAll.Data;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Diagnostics;
using System.Reflection;
using Autodesk.Revit.UI;

namespace Case.UngroupAll.UI
{
  public partial class form_Main : System.Windows.Forms.Form
  {

    private clsSettings _s;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="s"></param>
    public form_Main(clsSettings s)
    {
      InitializeComponent();
      progressBar1.Hide();
      _s = s;
    }

    #region Form Controls & Events

    /// <summary>
    /// Startup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void form_Main_Load(object sender, EventArgs e)
    {
      Text = string.Format(
        "Ungroup All v{0}",
        Assembly.GetExecutingAssembly().GetName().Version);

      checkBoxGroupsDetail.Checked = false;
      checkBoxGroupsDetail.Enabled = false;
      checkBoxGroupsModel.Checked = false;
      checkBoxGroupsModel.Enabled = false;

      if (_s.ModelGroups.Count > 0)
      {
        checkBoxGroupsModel.Checked = true;
        checkBoxGroupsModel.Enabled = true;
        checkBoxGroupsModel.Text = string.Format("{0} Model Groups", _s.ModelGroups.Count);
      }
      if (_s.DetailGroups.Count > 0)
      {
        checkBoxGroupsDetail.Checked = true;
        checkBoxGroupsDetail.Enabled = true;
        checkBoxGroupsDetail.Text = string.Format("{0} Detail Groups", _s.DetailGroups.Count);
      }

    }

    /// <summary>
    /// Logo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void pictureBox1_Click(object sender, EventArgs e)
    {
      Process.Start("http://apps.case-inc.com/");
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
        // Usage Tracking    
        AssemblyInfo m_a = new AssemblyInfo(Assembly.GetExecutingAssembly());
      }
      catch { }

      int m_total = 0;
      if (checkBoxGroupsDetail.Checked)
        m_total += _s.DetailGroups.Count;
      if (checkBoxGroupsModel.Checked)
        m_total += _s.ModelGroups.Count;

      // Progress Bar
      buttonOk.Hide();
      buttonCancel.Hide();
      progressBar1.Show();
      progressBar1.Minimum = 0;
      progressBar1.Maximum = m_total;
      progressBar1.Value = 0;

      int m_sicM = 0;
      int m_sicD = 0;

      using (Transaction t = new Transaction(_s.ActiveDoc))
      {
        if (t.Start("Ungrouping All Groups") == TransactionStatus.Started)
        {
          if (checkBoxGroupsDetail.Checked)
          {
            foreach (var x in _s.DetailGroups)
            {
              try
              {
                x.UngroupMembers();
                progressBar1.Increment(1);
                m_sicD++;
              }
              catch {}
            }
          }
          if (checkBoxGroupsModel.Checked)
          {
            foreach (var x in _s.ModelGroups)
            {
              try
              {
                x.UngroupMembers();
                progressBar1.Increment(1);
                m_sicM++;
              }
              catch { }
            }
          }
          t.Commit();

        }
      }

      if (m_sicM + m_sicD < 1)
      {
        using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Failed"))
        {
          td.TitleAutoPrefix = false;
          td.MainInstruction = "Ungrouping Groups Failed";
          td.Show();
        }
      }
      else
      {
        string m_msg = string.Format("{0} of {1} Model Groups Ungrouped\n{2} of {3} Detail Groups Ungrouped",
            m_sicM, _s.ModelGroups.Count, m_sicD, _s.DetailGroups.Count);

        using (Autodesk.Revit.UI.TaskDialog td = new Autodesk.Revit.UI.TaskDialog("Results"))
        {
          td.TitleAutoPrefix = false;
          td.MainInstruction = "Ungrouping Results:";
          td.MainContent = m_msg;
          td.Show();
        }
      }

      Close();

    }

    /// <summary>
    /// Close
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void checkBoxGroupsDetail_CheckedChanged(object sender, EventArgs e)
    {
      buttonOk.Enabled = (checkBoxGroupsDetail.Checked || checkBoxGroupsModel.Checked);
    }

    private void checkBoxGroupsModel_CheckedChanged(object sender, EventArgs e)
    {
      buttonOk.Enabled = (checkBoxGroupsDetail.Checked || checkBoxGroupsModel.Checked);
    }

    #endregion

  }
}