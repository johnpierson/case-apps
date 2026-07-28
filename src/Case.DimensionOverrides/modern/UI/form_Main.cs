using System;
using System.Diagnostics;
using System.Reflection;
using Autodesk.Revit.DB;
using Microsoft.VisualBasic.ApplicationServices;
using clsSettings = Case.DimensionOverrides.Data.clsSettings;
using Form = System.Windows.Forms.Form;

namespace Case.DimensionOverrides.UI
{
// ReSharper disable once InconsistentNaming
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
      progressBar1.Hide();
      _s = s;

      lbl_IntroLabel.Text = string.Format(
        "Selection contains {0} Dimensions and {1} Dimension Segments.",
        _s.Dimensions.Count,
        _s.SegmentCount);
    }

    #region Form Controls & Events

    /// <summary>
    ///   Startup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void form_Main_Load(object sender, EventArgs e)
    {
      Text = string.Format(
        "Dimension Overrides v{0}",
        Assembly.GetExecutingAssembly().GetName().Version);
    }

    /// <summary>
    ///   Logo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void pictureBox1_Click(object sender, EventArgs e)
    {
      Process.Start("http://apps.case-inc.com/");
    }

    /// <summary>
    ///   OK
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
      catch
      {
      }

      int m_total = _s.Dimensions.Count;

      // Progress Bar
      buttonOk.Hide();
      buttonCancel.Hide();
      progressBar1.Show();
      progressBar1.Minimum = 0;
      progressBar1.Maximum = m_total;
      progressBar1.Value = 0;

      using (Transaction m_t = new Transaction(_s.ActiveDoc, "Adjust Dimensions"))
      {
        m_t.Start();
        try
        {
          if (!btn_ClearOverrides.Checked)
          {
            foreach (Dimension m_dimension in _s.Dimensions)
            {
              if (!m_dimension.Segments.IsEmpty)
              {
                foreach (DimensionSegment m_dim in m_dimension.Segments)
                {
                  if (!string.IsNullOrEmpty(txt_TopOverride.Text))
                  {
                    m_dim.Above = txt_TopOverride.Text;
                  }
                  if (!string.IsNullOrEmpty(txt_BottomOverride.Text))
                  {
                    m_dim.Below = txt_BottomOverride.Text;
                  }
                  if (!string.IsNullOrEmpty(txt_LeftOverride.Text))
                  {
                    m_dim.Prefix = txt_LeftOverride.Text;
                  }
                  if (!string.IsNullOrEmpty(txt_RightOverride.Text))
                  {
                    m_dim.Suffix = txt_RightOverride.Text;
                  }
                  if (!string.IsNullOrEmpty(txt_DimOverride.Text))
                  {
                    m_dim.ValueOverride = txt_DimOverride.Text;
                  }
                  if (btn_ResetPosition.Checked)
                  {
                    m_dim.ResetTextPosition();
                  }
                }
              }
              else
              {
                if (btn_ResetPosition.Checked)
                {
                  m_dimension.ResetTextPosition();
                }
                if (!string.IsNullOrEmpty(txt_TopOverride.Text))
                {
                  m_dimension.Above = txt_TopOverride.Text;
                }
                if (!string.IsNullOrEmpty(txt_BottomOverride.Text))
                {
                  m_dimension.Below = txt_BottomOverride.Text;
                }
                if (!string.IsNullOrEmpty(txt_LeftOverride.Text))
                {
                  m_dimension.Prefix = txt_LeftOverride.Text;
                }
                if (!string.IsNullOrEmpty(txt_RightOverride.Text))
                {
                  m_dimension.Suffix = txt_RightOverride.Text;
                }
                if (!string.IsNullOrEmpty(txt_DimOverride.Text))
                {
                  m_dimension.ValueOverride = txt_DimOverride.Text;
                }
              }
            }
          }
          else
          {
            foreach (Dimension m_dimension in _s.Dimensions)
            {
              if (!m_dimension.Segments.IsEmpty)
              {
                foreach (DimensionSegment m_segment in m_dimension.Segments)
                {
                  m_segment.Above = null;
                  m_segment.Below = null;
                  m_segment.Prefix = null;
                  m_segment.Suffix = null;
                  m_segment.ValueOverride = null;
                }
              }
              else
              {
                m_dimension.Above = null;
                m_dimension.Below = null;
                m_dimension.Prefix = null;
                m_dimension.Suffix = null;
                m_dimension.ValueOverride = null;
              }
            }
          }
          progressBar1.Increment(1);
        }
        catch (Exception)
        {
          m_t.RollBack();
          throw;
        }
        m_t.Commit();
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

    #endregion
  }
}