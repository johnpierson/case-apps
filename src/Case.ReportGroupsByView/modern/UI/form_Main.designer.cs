namespace Case.ReportGroupsByView.UI
{
  partial class form_Main
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_Main));
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOk = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.checkBoxGroupsDetail = new System.Windows.Forms.CheckBox();
      this.checkBoxGroupsModel = new System.Windows.Forms.CheckBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(278, 102);
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
      this.buttonOk.Location = new System.Drawing.Point(197, 102);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(75, 40);
      this.buttonOk.TabIndex = 1;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.checkBoxGroupsDetail);
      this.groupBox1.Controls.Add(this.checkBoxGroupsModel);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(341, 84);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select Reporting Options";
      // 
      // checkBoxGroupsDetail
      // 
      this.checkBoxGroupsDetail.AutoSize = true;
      this.checkBoxGroupsDetail.Checked = true;
      this.checkBoxGroupsDetail.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxGroupsDetail.Location = new System.Drawing.Point(198, 35);
      this.checkBoxGroupsDetail.Name = "checkBoxGroupsDetail";
      this.checkBoxGroupsDetail.Size = new System.Drawing.Size(128, 17);
      this.checkBoxGroupsDetail.TabIndex = 3;
      this.checkBoxGroupsDetail.Text = "Include Detail Groups";
      this.checkBoxGroupsDetail.UseVisualStyleBackColor = true;
      this.checkBoxGroupsDetail.CheckedChanged += new System.EventHandler(this.checkBoxGroupsDetail_CheckedChanged);
      // 
      // checkBoxGroupsModel
      // 
      this.checkBoxGroupsModel.AutoSize = true;
      this.checkBoxGroupsModel.Checked = true;
      this.checkBoxGroupsModel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxGroupsModel.Location = new System.Drawing.Point(24, 35);
      this.checkBoxGroupsModel.Name = "checkBoxGroupsModel";
      this.checkBoxGroupsModel.Size = new System.Drawing.Size(130, 17);
      this.checkBoxGroupsModel.TabIndex = 2;
      this.checkBoxGroupsModel.Text = "Include Model Groups";
      this.checkBoxGroupsModel.UseVisualStyleBackColor = true;
      this.checkBoxGroupsModel.CheckedChanged += new System.EventHandler(this.checkBoxGroupsModel_CheckedChanged);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 102);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(341, 40);
      this.progressBar1.TabIndex = 3;
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "*.csv";
      this.saveFileDialog1.FileName = "Group Report.csv";
      this.saveFileDialog1.Filter = "CSV Files | *.csv";
      this.saveFileDialog1.Title = "Save Groups by View Report";
      // 
      // form_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(365, 153);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(381, 192);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(381, 192);
      this.Name = "form_Main";
      this.Text = "Report Groups by View";
      this.Load += new System.EventHandler(this.form_Main_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox checkBoxGroupsDetail;
    private System.Windows.Forms.CheckBox checkBoxGroupsModel;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
  }
}