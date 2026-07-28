using System;
using System.Windows.Forms;
using Case.ApplySysOrient.Data;

namespace Case.ApplySysOrient.UI
{
  public partial class form_MaxSize : Form
  {

	 private clsSettings _s;

	 public string ErrorMsg = string.Empty;

	 /// <summary>
	 /// Constructor
	 /// </summary>
	 /// <param name="s"></param>
	 public form_MaxSize(clsSettings s)
	 {
		InitializeComponent();

		// Widen Scope
		_s = s;

	 }

	 #region Form Controls & Events

	 /// <summary>
	 /// Setup
	 /// </summary>
	 /// <param name="sender"></param>
	 /// <param name="e"></param>
	 private void form_MaxSize_Load(object sender, EventArgs e)
	 {

		// Title
		this.Text = " v" + "";
		this.progressBar1.Hide();

		// Parameters

	 }

	 /// <summary>
	 /// OK
	 /// </summary>
	 /// <param name="sender"></param>
	 /// <param name="e"></param>
	 private void buttonOk_Click(object sender, EventArgs e)
	 {
     
	 }

	 /// <summary>
	 /// Cancel
	 /// </summary>
	 /// <param name="sender"></param>
	 /// <param name="e"></param>
	 private void buttonCancel_Click(object sender, EventArgs e)
	 {
		this.Close();
	 }

	 /// <summary>
	 /// Duct
	 /// </summary>
	 /// <param name="sender"></param>
	 /// <param name="e"></param>
	 private void checkBoxDuct_CheckedChanged(object sender, EventArgs e)
	 {
		this.textBoxMaxDuctWidth.Enabled = this.checkBoxDuct.Checked;
		this.textBoxMaxHeight.Enabled = this.checkBoxDuct.Checked;
		this.comboBoxDuctParameter.Enabled = this.checkBoxDuct.Checked;
	 }

	 /// <summary>
	 /// Pipe
	 /// </summary>
	 /// <param name="sender"></param>
	 /// <param name="e"></param>
	 private void checkBoxPipe_CheckedChanged(object sender, EventArgs e)
	 {
		this.textBoxMaxPipeSize.Enabled = this.checkBoxPipe.Checked;
		this.comboBoxPipeParameter.Enabled = this.checkBoxPipe.Checked;
	 }

	 #endregion

  }
}