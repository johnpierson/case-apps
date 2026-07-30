namespace Case.ObjectStyles.UI
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
      this.FamSourceCheckBox = new System.Windows.Forms.CheckBox();
      this.AnalyticalCatCheck = new System.Windows.Forms.CheckBox();
      this.AnnotationCatCheck = new System.Windows.Forms.CheckBox();
      this.ModelCatCheck = new System.Windows.Forms.CheckBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.FolderBrowser1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ButtonHelp = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(278, 144);
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
      this.buttonOk.Location = new System.Drawing.Point(197, 144);
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
      this.groupBox1.Controls.Add(this.FamSourceCheckBox);
      this.groupBox1.Controls.Add(this.AnalyticalCatCheck);
      this.groupBox1.Controls.Add(this.AnnotationCatCheck);
      this.groupBox1.Controls.Add(this.ModelCatCheck);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(341, 123);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select Category Definitions";
      // 
      // FamSourceCheckBox
      // 
      this.FamSourceCheckBox.AutoSize = true;
      this.FamSourceCheckBox.Location = new System.Drawing.Point(12, 96);
      this.FamSourceCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.FamSourceCheckBox.Name = "FamSourceCheckBox";
      this.FamSourceCheckBox.Size = new System.Drawing.Size(181, 17);
      this.FamSourceCheckBox.TabIndex = 3;
      this.FamSourceCheckBox.Text = "Include Component Family Styles";
      this.FamSourceCheckBox.UseVisualStyleBackColor = true;
      this.FamSourceCheckBox.CheckedChanged += new System.EventHandler(this.FamSourceCheckBox_CheckedChanged);
      // 
      // AnalyticalCatCheck
      // 
      this.AnalyticalCatCheck.AutoSize = true;
      this.AnalyticalCatCheck.Checked = true;
      this.AnalyticalCatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AnalyticalCatCheck.Location = new System.Drawing.Point(12, 71);
      this.AnalyticalCatCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.AnalyticalCatCheck.Name = "AnalyticalCatCheck";
      this.AnalyticalCatCheck.Size = new System.Drawing.Size(162, 17);
      this.AnalyticalCatCheck.TabIndex = 2;
      this.AnalyticalCatCheck.Text = "Include Analytical Categories";
      this.AnalyticalCatCheck.UseVisualStyleBackColor = true;
      this.AnalyticalCatCheck.CheckedChanged += new System.EventHandler(this.AnalyticalCatCheck_CheckedChanged);
      // 
      // AnnotationCatCheck
      // 
      this.AnnotationCatCheck.AutoSize = true;
      this.AnnotationCatCheck.Checked = true;
      this.AnnotationCatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AnnotationCatCheck.Location = new System.Drawing.Point(12, 47);
      this.AnnotationCatCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.AnnotationCatCheck.Name = "AnnotationCatCheck";
      this.AnnotationCatCheck.Size = new System.Drawing.Size(168, 17);
      this.AnnotationCatCheck.TabIndex = 1;
      this.AnnotationCatCheck.Text = "Include Annotation Categories";
      this.AnnotationCatCheck.UseVisualStyleBackColor = true;
      this.AnnotationCatCheck.CheckedChanged += new System.EventHandler(this.AnnotationCatCheck_CheckedChanged);
      // 
      // ModelCatCheck
      // 
      this.ModelCatCheck.AutoSize = true;
      this.ModelCatCheck.Checked = true;
      this.ModelCatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ModelCatCheck.Location = new System.Drawing.Point(12, 23);
      this.ModelCatCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.ModelCatCheck.Name = "ModelCatCheck";
      this.ModelCatCheck.Size = new System.Drawing.Size(146, 17);
      this.ModelCatCheck.TabIndex = 0;
      this.ModelCatCheck.Text = "Include Model Categories";
      this.ModelCatCheck.UseVisualStyleBackColor = true;
      this.ModelCatCheck.CheckedChanged += new System.EventHandler(this.ModelCatCheck_CheckedChanged);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 144);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(341, 40);
      this.progressBar1.TabIndex = 3;
      // 
      // ButtonHelp
      // 
      this.ButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonHelp.Location = new System.Drawing.Point(150, 144);
      this.ButtonHelp.Name = "ButtonHelp";
      this.ButtonHelp.Size = new System.Drawing.Size(41, 40);
      this.ButtonHelp.TabIndex = 0;
      this.ButtonHelp.Text = "?";
      this.ButtonHelp.UseVisualStyleBackColor = true;
      this.ButtonHelp.Click += new System.EventHandler(this.HelpButton_Click);
      // 
      // form_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(363, 196);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.ButtonHelp);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(379, 234);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(379, 234);
      this.Name = "form_Main";
      this.Text = "Export Object Styles";
      this.TopMost = true;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.CheckBox AnalyticalCatCheck;
    private System.Windows.Forms.CheckBox AnnotationCatCheck;
    private System.Windows.Forms.CheckBox ModelCatCheck;
    private System.Windows.Forms.FolderBrowserDialog FolderBrowser1;
    private System.Windows.Forms.CheckBox FamSourceCheckBox;
    private System.Windows.Forms.Button ButtonHelp;
  }
}