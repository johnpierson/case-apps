namespace Case.ApplySysOrient.UI
{
  partial class form_MaxSize
  {
	 /// <summary>
	 /// Required designer variable.
	 /// </summary>
	 private System.ComponentModel.IContainer components = null;

	 /// <summary>
	 /// Clean up any resources being used.
	 /// </summary>
	 /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	 protected override void Dispose(bool disposing)
	 {
		if (disposing && (components != null))
		{
		  components.Dispose();
		}
		base.Dispose(disposing);
	 }

	 #region Windows Form Designer generated code

	 /// <summary>
	 /// Required method for Designer support - do not modify
	 /// the contents of this method with the code editor.
	 /// </summary>
	 private void InitializeComponent()
	 {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_MaxSize));
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOk = new System.Windows.Forms.Button();
      this.groupBoxDuct = new System.Windows.Forms.GroupBox();
      this.checkBoxDuct = new System.Windows.Forms.CheckBox();
      this.comboBoxDuctParameter = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.textBoxMaxHeight = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxMaxDuctWidth = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.groupBoxPipe = new System.Windows.Forms.GroupBox();
      this.checkBoxPipe = new System.Windows.Forms.CheckBox();
      this.comboBoxPipeParameter = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.textBoxMaxPipeSize = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.groupBoxTerms = new System.Windows.Forms.GroupBox();
      this.textBoxBranch = new System.Windows.Forms.TextBox();
      this.textBoxTrunk = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBoxDuct.SuspendLayout();
      this.groupBoxPipe.SuspendLayout();
      this.groupBoxTerms.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(356, 495);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 40);
      this.buttonCancel.TabIndex = 0;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOk
      // 
      this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOk.Location = new System.Drawing.Point(275, 495);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(75, 40);
      this.buttonOk.TabIndex = 1;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // groupBoxDuct
      // 
      this.groupBoxDuct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxDuct.Controls.Add(this.checkBoxDuct);
      this.groupBoxDuct.Controls.Add(this.comboBoxDuctParameter);
      this.groupBoxDuct.Controls.Add(this.label3);
      this.groupBoxDuct.Controls.Add(this.textBoxMaxHeight);
      this.groupBoxDuct.Controls.Add(this.label2);
      this.groupBoxDuct.Controls.Add(this.textBoxMaxDuctWidth);
      this.groupBoxDuct.Controls.Add(this.label1);
      this.groupBoxDuct.Location = new System.Drawing.Point(12, 118);
      this.groupBoxDuct.Name = "groupBoxDuct";
      this.groupBoxDuct.Size = new System.Drawing.Size(419, 197);
      this.groupBoxDuct.TabIndex = 2;
      this.groupBoxDuct.TabStop = false;
      this.groupBoxDuct.Text = "Select the Maximums for Duct Trunk Calssifications";
      // 
      // checkBoxDuct
      // 
      this.checkBoxDuct.AutoSize = true;
      this.checkBoxDuct.Checked = true;
      this.checkBoxDuct.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxDuct.Location = new System.Drawing.Point(32, 33);
      this.checkBoxDuct.Name = "checkBoxDuct";
      this.checkBoxDuct.Size = new System.Drawing.Size(247, 17);
      this.checkBoxDuct.TabIndex = 6;
      this.checkBoxDuct.Text = "Assign Classifications for Ductwork and Fittings";
      this.checkBoxDuct.UseVisualStyleBackColor = true;
      this.checkBoxDuct.CheckedChanged += new System.EventHandler(this.checkBoxDuct_CheckedChanged);
      // 
      // comboBoxDuctParameter
      // 
      this.comboBoxDuctParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBoxDuctParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxDuctParameter.FormattingEnabled = true;
      this.comboBoxDuctParameter.Location = new System.Drawing.Point(178, 152);
      this.comboBoxDuctParameter.Name = "comboBoxDuctParameter";
      this.comboBoxDuctParameter.Size = new System.Drawing.Size(219, 21);
      this.comboBoxDuctParameter.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(29, 155);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(122, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Classification Parameter:";
      // 
      // textBoxMaxHeight
      // 
      this.textBoxMaxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxMaxHeight.Location = new System.Drawing.Point(178, 112);
      this.textBoxMaxHeight.Name = "textBoxMaxHeight";
      this.textBoxMaxHeight.Size = new System.Drawing.Size(219, 20);
      this.textBoxMaxHeight.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(29, 115);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(62, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Max Depth:";
      // 
      // textBoxMaxDuctWidth
      // 
      this.textBoxMaxDuctWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxMaxDuctWidth.Location = new System.Drawing.Point(178, 76);
      this.textBoxMaxDuctWidth.Name = "textBoxMaxDuctWidth";
      this.textBoxMaxDuctWidth.Size = new System.Drawing.Size(219, 20);
      this.textBoxMaxDuctWidth.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(29, 79);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(61, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Max Width:";
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 495);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(419, 40);
      this.progressBar1.TabIndex = 3;
      // 
      // groupBoxPipe
      // 
      this.groupBoxPipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxPipe.Controls.Add(this.checkBoxPipe);
      this.groupBoxPipe.Controls.Add(this.comboBoxPipeParameter);
      this.groupBoxPipe.Controls.Add(this.label4);
      this.groupBoxPipe.Controls.Add(this.textBoxMaxPipeSize);
      this.groupBoxPipe.Controls.Add(this.label6);
      this.groupBoxPipe.Location = new System.Drawing.Point(12, 321);
      this.groupBoxPipe.Name = "groupBoxPipe";
      this.groupBoxPipe.Size = new System.Drawing.Size(419, 162);
      this.groupBoxPipe.TabIndex = 7;
      this.groupBoxPipe.TabStop = false;
      this.groupBoxPipe.Text = "Select the Maximums for Pipe Trunk Calssifications";
      // 
      // checkBoxPipe
      // 
      this.checkBoxPipe.AutoSize = true;
      this.checkBoxPipe.Checked = true;
      this.checkBoxPipe.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxPipe.Location = new System.Drawing.Point(32, 33);
      this.checkBoxPipe.Name = "checkBoxPipe";
      this.checkBoxPipe.Size = new System.Drawing.Size(230, 17);
      this.checkBoxPipe.TabIndex = 6;
      this.checkBoxPipe.Text = "Assign Classifications for Piping and Fittings";
      this.checkBoxPipe.UseVisualStyleBackColor = true;
      this.checkBoxPipe.CheckedChanged += new System.EventHandler(this.checkBoxPipe_CheckedChanged);
      // 
      // comboBoxPipeParameter
      // 
      this.comboBoxPipeParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBoxPipeParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxPipeParameter.FormattingEnabled = true;
      this.comboBoxPipeParameter.Location = new System.Drawing.Point(178, 115);
      this.comboBoxPipeParameter.Name = "comboBoxPipeParameter";
      this.comboBoxPipeParameter.Size = new System.Drawing.Size(219, 21);
      this.comboBoxPipeParameter.TabIndex = 5;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(29, 118);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(122, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "Classification Parameter:";
      // 
      // textBoxMaxPipeSize
      // 
      this.textBoxMaxPipeSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxMaxPipeSize.Location = new System.Drawing.Point(178, 76);
      this.textBoxMaxPipeSize.Name = "textBoxMaxPipeSize";
      this.textBoxMaxPipeSize.Size = new System.Drawing.Size(219, 20);
      this.textBoxMaxPipeSize.TabIndex = 1;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(29, 79);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(84, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "Max Trunk Size:";
      // 
      // groupBoxTerms
      // 
      this.groupBoxTerms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxTerms.Controls.Add(this.textBoxBranch);
      this.groupBoxTerms.Controls.Add(this.textBoxTrunk);
      this.groupBoxTerms.Controls.Add(this.label5);
      this.groupBoxTerms.Controls.Add(this.label7);
      this.groupBoxTerms.Location = new System.Drawing.Point(12, 12);
      this.groupBoxTerms.Name = "groupBoxTerms";
      this.groupBoxTerms.Size = new System.Drawing.Size(419, 100);
      this.groupBoxTerms.TabIndex = 8;
      this.groupBoxTerms.TabStop = false;
      this.groupBoxTerms.Text = "Classification Terms to Use:";
      // 
      // textBoxBranch
      // 
      this.textBoxBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxBranch.Location = new System.Drawing.Point(178, 67);
      this.textBoxBranch.Name = "textBoxBranch";
      this.textBoxBranch.Size = new System.Drawing.Size(219, 20);
      this.textBoxBranch.TabIndex = 6;
      this.textBoxBranch.Text = "BRANCH";
      // 
      // textBoxTrunk
      // 
      this.textBoxTrunk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxTrunk.Location = new System.Drawing.Point(178, 31);
      this.textBoxTrunk.Name = "textBoxTrunk";
      this.textBoxTrunk.Size = new System.Drawing.Size(219, 20);
      this.textBoxTrunk.TabIndex = 5;
      this.textBoxTrunk.Text = "TRUNK";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(29, 67);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(113, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Branch Classifications:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(29, 31);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(126, 13);
      this.label7.TabIndex = 3;
      this.label7.Text = "Trunks (Max and Larger):";
      // 
      // form_MaxSize
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(443, 547);
      this.Controls.Add(this.groupBoxTerms);
      this.Controls.Add(this.groupBoxPipe);
      this.Controls.Add(this.groupBoxDuct);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(459, 585);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(459, 585);
      this.Name = "form_MaxSize";
      this.Text = "X";
      this.Load += new System.EventHandler(this.form_MaxSize_Load);
      this.groupBoxDuct.ResumeLayout(false);
      this.groupBoxDuct.PerformLayout();
      this.groupBoxPipe.ResumeLayout(false);
      this.groupBoxPipe.PerformLayout();
      this.groupBoxTerms.ResumeLayout(false);
      this.groupBoxTerms.PerformLayout();
      this.ResumeLayout(false);

	 }

	 #endregion

	 private System.Windows.Forms.Button buttonCancel;
	 private System.Windows.Forms.Button buttonOk;
	 private System.Windows.Forms.GroupBox groupBoxDuct;
	 private System.Windows.Forms.ComboBox comboBoxDuctParameter;
	 private System.Windows.Forms.Label label3;
	 private System.Windows.Forms.TextBox textBoxMaxHeight;
	 private System.Windows.Forms.Label label2;
	 private System.Windows.Forms.TextBox textBoxMaxDuctWidth;
	 private System.Windows.Forms.Label label1;
	 private System.Windows.Forms.ProgressBar progressBar1;
	 private System.Windows.Forms.CheckBox checkBoxDuct;
	 private System.Windows.Forms.GroupBox groupBoxPipe;
	 private System.Windows.Forms.CheckBox checkBoxPipe;
	 private System.Windows.Forms.ComboBox comboBoxPipeParameter;
	 private System.Windows.Forms.Label label4;
	 private System.Windows.Forms.TextBox textBoxMaxPipeSize;
	 private System.Windows.Forms.Label label6;
	 private System.Windows.Forms.GroupBox groupBoxTerms;
	 private System.Windows.Forms.TextBox textBoxBranch;
	 private System.Windows.Forms.TextBox textBoxTrunk;
	 private System.Windows.Forms.Label label5;
	 private System.Windows.Forms.Label label7;
  }
}