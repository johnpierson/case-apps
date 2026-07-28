using System.Windows.Forms;

namespace Case.ExportSharedParameters.UI
{
  public partial class form_Main : Form
  {

    /// <summary>
    /// Progress Bar
    /// </summary>
    /// <param name="maxNumber"></param>
    public form_Main(int maxNumber)
    {
      InitializeComponent();

      // Setup Progress
      this.progressBar1.Maximum = maxNumber;
      this.progressBar1.Minimum = 0;
      this.progressBar1.Value = 0;

    }

    /// <summary>
    /// Increment the Progress
    /// </summary>
    public void IncrementProgress()
    {
      try
      {
        this.progressBar1.Increment(1);
        Application.DoEvents();
      }
      catch { }
    }

  }
}