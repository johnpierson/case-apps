using System;
using System.Windows.Forms;


namespace Case.ApplySysOrient.UI
{
  public partial class form_Orient : Form
  {

    internal bool DoDuct;
    internal bool DoPipe;
    internal bool DoTray;
    internal bool DoConduit;

    /// <summary>
    /// Constructor
    /// </summary>
    public form_Orient()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Process
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonOk_Click(object sender, EventArgs e)
    {


      DoDuct = checkBoxDuct.Checked;
      DoPipe = checkBoxPipe.Checked;
      DoTray = checkBoxTray.Checked;
      DoConduit = checkBoxConduit.Checked;
      Close();
    }

    /// <summary>
    /// Cancel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      DoDuct = false;
      DoPipe = false;
      DoTray = false;
      DoConduit = false;
      Close();
    }

  }
}
