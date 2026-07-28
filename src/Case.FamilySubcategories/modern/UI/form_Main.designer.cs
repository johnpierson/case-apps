namespace Case.FamilySubcategories.UI
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.FolderBrowser1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ButtonHelp = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.CheckSubFolders = new System.Windows.Forms.CheckBox();
      this.TextboxFamilyPath = new System.Windows.Forms.TextBox();
      this.ButtonFolderBrowse = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(296, 111);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 40);
      this.buttonCancel.TabIndex = 5;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOk
      // 
      this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOk.Enabled = false;
      this.buttonOk.Location = new System.Drawing.Point(215, 111);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(75, 40);
      this.buttonOk.TabIndex = 3;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 111);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(359, 40);
      this.progressBar1.TabIndex = 51;
      // 
      // FolderBrowser1
      // 
      this.FolderBrowser1.ShowNewFolderButton = false;
      // 
      // ButtonHelp
      // 
      this.ButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonHelp.Location = new System.Drawing.Point(168, 111);
      this.ButtonHelp.Name = "ButtonHelp";
      this.ButtonHelp.Size = new System.Drawing.Size(41, 40);
      this.ButtonHelp.TabIndex = 4;
      this.ButtonHelp.Text = "?";
      this.ButtonHelp.UseVisualStyleBackColor = true;
      this.ButtonHelp.Click += new System.EventHandler(this.HelpButton_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.CheckSubFolders);
      this.groupBox2.Controls.Add(this.TextboxFamilyPath);
      this.groupBox2.Controls.Add(this.ButtonFolderBrowse);
      this.groupBox2.Location = new System.Drawing.Point(12, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(359, 91);
      this.groupBox2.TabIndex = 50;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Family Folder";
      // 
      // CheckSubFolders
      // 
      this.CheckSubFolders.AutoSize = true;
      this.CheckSubFolders.Enabled = false;
      this.CheckSubFolders.Location = new System.Drawing.Point(9, 57);
      this.CheckSubFolders.Name = "CheckSubFolders";
      this.CheckSubFolders.Size = new System.Drawing.Size(120, 17);
      this.CheckSubFolders.TabIndex = 1;
      this.CheckSubFolders.Text = "Include Sub-Folders";
      this.CheckSubFolders.UseVisualStyleBackColor = true;
      // 
      // TextboxFamilyPath
      // 
      this.TextboxFamilyPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TextboxFamilyPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TextboxFamilyPath.Location = new System.Drawing.Point(9, 29);
      this.TextboxFamilyPath.MaximumSize = new System.Drawing.Size(460, 22);
      this.TextboxFamilyPath.MinimumSize = new System.Drawing.Size(260, 22);
      this.TextboxFamilyPath.Name = "TextboxFamilyPath";
      this.TextboxFamilyPath.Size = new System.Drawing.Size(260, 22);
      this.TextboxFamilyPath.TabIndex = 2;
      this.TextboxFamilyPath.Leave += new System.EventHandler(this.TextboxFamilyPath_TextChanged);
      // 
      // ButtonFolderBrowse
      // 
      this.ButtonFolderBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonFolderBrowse.Location = new System.Drawing.Point(275, 29);
      this.ButtonFolderBrowse.Name = "ButtonFolderBrowse";
      this.ButtonFolderBrowse.Size = new System.Drawing.Size(75, 22);
      this.ButtonFolderBrowse.TabIndex = 0;
      this.ButtonFolderBrowse.Text = "Browse";
      this.ButtonFolderBrowse.UseVisualStyleBackColor = true;
      this.ButtonFolderBrowse.Click += new System.EventHandler(this.ButtonFolderBrowse_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "txt";
      this.saveFileDialog1.FileName = "CASE-FamilySubCategories";
      this.saveFileDialog1.Filter = "Text Files |*.txt";
      this.saveFileDialog1.Title = "Family Subcategory Report Save-As";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoEllipsis = true;
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 91);
      this.label1.MaximumSize = new System.Drawing.Size(355, 13);
      this.label1.MinimumSize = new System.Drawing.Size(355, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(355, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Processing: ";
      // 
      // form_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(384, 161);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.ButtonHelp);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(600, 200);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "form_Main";
      this.Text = "Get Family Subcategories";
      this.Load += new System.EventHandler(this.form_Main_Load);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.FolderBrowserDialog FolderBrowser1;
    private System.Windows.Forms.Button ButtonHelp;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox CheckSubFolders;
    private System.Windows.Forms.TextBox TextboxFamilyPath;
    private System.Windows.Forms.Button ButtonFolderBrowse;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Label label1;
  }
}