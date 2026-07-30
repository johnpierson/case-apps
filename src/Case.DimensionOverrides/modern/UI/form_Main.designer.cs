namespace Case.DimensionOverrides.UI
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
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.btn_ClearOverrides = new System.Windows.Forms.CheckBox();
      this.ResetGroup = new System.Windows.Forms.GroupBox();
      this.btn_ResetPosition = new System.Windows.Forms.CheckBox();
      this.grp_Overrides = new System.Windows.Forms.GroupBox();
      this.lbl_LeftOverride = new System.Windows.Forms.Label();
      this.lbl_RightOverride = new System.Windows.Forms.Label();
      this.lbl_TopOverride = new System.Windows.Forms.Label();
      this.lbl_DimOverride = new System.Windows.Forms.Label();
      this.lbl_BottomOverride = new System.Windows.Forms.Label();
      this.txt_BottomOverride = new System.Windows.Forms.TextBox();
      this.txt_TopOverride = new System.Windows.Forms.TextBox();
      this.txt_RightOverride = new System.Windows.Forms.TextBox();
      this.txt_DimOverride = new System.Windows.Forms.TextBox();
      this.txt_LeftOverride = new System.Windows.Forms.TextBox();
      this.lbl_IntroLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.ResetGroup.SuspendLayout();
      this.grp_Overrides.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(447, 280);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 40);
      this.buttonCancel.TabIndex = 7;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOk
      // 
      this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOk.Location = new System.Drawing.Point(366, 280);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(75, 40);
      this.buttonOk.TabIndex = 6;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(195, 280);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(327, 40);
      this.progressBar1.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(12, 280);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(177, 40);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // btn_ClearOverrides
      // 
      this.btn_ClearOverrides.Appearance = System.Windows.Forms.Appearance.Button;
      this.btn_ClearOverrides.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_ClearOverrides.Location = new System.Drawing.Point(6, 21);
      this.btn_ClearOverrides.Name = "btn_ClearOverrides";
      this.btn_ClearOverrides.Size = new System.Drawing.Size(243, 30);
      this.btn_ClearOverrides.TabIndex = 5;
      this.btn_ClearOverrides.Text = "Clear All Overrides";
      this.btn_ClearOverrides.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_ClearOverrides.UseVisualStyleBackColor = true;
      // 
      // ResetGroup
      // 
      this.ResetGroup.Controls.Add(this.btn_ResetPosition);
      this.ResetGroup.Controls.Add(this.btn_ClearOverrides);
      this.ResetGroup.Location = new System.Drawing.Point(12, 205);
      this.ResetGroup.Name = "ResetGroup";
      this.ResetGroup.Size = new System.Drawing.Size(510, 63);
      this.ResetGroup.TabIndex = 0;
      this.ResetGroup.TabStop = false;
      this.ResetGroup.Text = "Reset Overrides";
      // 
      // btn_ResetPosition
      // 
      this.btn_ResetPosition.Appearance = System.Windows.Forms.Appearance.Button;
      this.btn_ResetPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_ResetPosition.Location = new System.Drawing.Point(261, 21);
      this.btn_ResetPosition.Name = "btn_ResetPosition";
      this.btn_ResetPosition.Size = new System.Drawing.Size(243, 30);
      this.btn_ResetPosition.TabIndex = 6;
      this.btn_ResetPosition.Text = "Reset Text Position";
      this.btn_ResetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_ResetPosition.UseVisualStyleBackColor = true;
      // 
      // grp_Overrides
      // 
      this.grp_Overrides.Controls.Add(this.lbl_LeftOverride);
      this.grp_Overrides.Controls.Add(this.lbl_RightOverride);
      this.grp_Overrides.Controls.Add(this.lbl_TopOverride);
      this.grp_Overrides.Controls.Add(this.lbl_DimOverride);
      this.grp_Overrides.Controls.Add(this.lbl_BottomOverride);
      this.grp_Overrides.Controls.Add(this.txt_BottomOverride);
      this.grp_Overrides.Controls.Add(this.txt_TopOverride);
      this.grp_Overrides.Controls.Add(this.txt_RightOverride);
      this.grp_Overrides.Controls.Add(this.txt_DimOverride);
      this.grp_Overrides.Controls.Add(this.txt_LeftOverride);
      this.grp_Overrides.Location = new System.Drawing.Point(12, 37);
      this.grp_Overrides.Name = "grp_Overrides";
      this.grp_Overrides.Size = new System.Drawing.Size(510, 161);
      this.grp_Overrides.TabIndex = 0;
      this.grp_Overrides.TabStop = false;
      this.grp_Overrides.Text = "Overrides";
      // 
      // lbl_LeftOverride
      // 
      this.lbl_LeftOverride.AutoSize = true;
      this.lbl_LeftOverride.Location = new System.Drawing.Point(6, 62);
      this.lbl_LeftOverride.Name = "lbl_LeftOverride";
      this.lbl_LeftOverride.Size = new System.Drawing.Size(33, 13);
      this.lbl_LeftOverride.TabIndex = 9;
      this.lbl_LeftOverride.Text = "Prefix";
      // 
      // lbl_RightOverride
      // 
      this.lbl_RightOverride.AutoSize = true;
      this.lbl_RightOverride.Location = new System.Drawing.Point(341, 62);
      this.lbl_RightOverride.Name = "lbl_RightOverride";
      this.lbl_RightOverride.Size = new System.Drawing.Size(33, 13);
      this.lbl_RightOverride.TabIndex = 8;
      this.lbl_RightOverride.Text = "Suffix";
      // 
      // lbl_TopOverride
      // 
      this.lbl_TopOverride.AutoSize = true;
      this.lbl_TopOverride.Location = new System.Drawing.Point(172, 14);
      this.lbl_TopOverride.Name = "lbl_TopOverride";
      this.lbl_TopOverride.Size = new System.Drawing.Size(38, 13);
      this.lbl_TopOverride.TabIndex = 7;
      this.lbl_TopOverride.Text = "Above";
      // 
      // lbl_DimOverride
      // 
      this.lbl_DimOverride.AutoSize = true;
      this.lbl_DimOverride.Location = new System.Drawing.Point(172, 62);
      this.lbl_DimOverride.Name = "lbl_DimOverride";
      this.lbl_DimOverride.Size = new System.Drawing.Size(56, 13);
      this.lbl_DimOverride.TabIndex = 6;
      this.lbl_DimOverride.Text = "Dimension";
      // 
      // lbl_BottomOverride
      // 
      this.lbl_BottomOverride.AutoSize = true;
      this.lbl_BottomOverride.Location = new System.Drawing.Point(172, 111);
      this.lbl_BottomOverride.Name = "lbl_BottomOverride";
      this.lbl_BottomOverride.Size = new System.Drawing.Size(36, 13);
      this.lbl_BottomOverride.TabIndex = 5;
      this.lbl_BottomOverride.Text = "Below";
      // 
      // txt_BottomOverride
      // 
      this.txt_BottomOverride.Location = new System.Drawing.Point(175, 131);
      this.txt_BottomOverride.Name = "txt_BottomOverride";
      this.txt_BottomOverride.Size = new System.Drawing.Size(160, 20);
      this.txt_BottomOverride.TabIndex = 1;
      this.txt_BottomOverride.WordWrap = false;
      // 
      // txt_TopOverride
      // 
      this.txt_TopOverride.Location = new System.Drawing.Point(175, 34);
      this.txt_TopOverride.Name = "txt_TopOverride";
      this.txt_TopOverride.Size = new System.Drawing.Size(160, 20);
      this.txt_TopOverride.TabIndex = 0;
      this.txt_TopOverride.WordWrap = false;
      // 
      // txt_RightOverride
      // 
      this.txt_RightOverride.Location = new System.Drawing.Point(344, 83);
      this.txt_RightOverride.Name = "txt_RightOverride";
      this.txt_RightOverride.Size = new System.Drawing.Size(160, 20);
      this.txt_RightOverride.TabIndex = 4;
      this.txt_RightOverride.WordWrap = false;
      // 
      // txt_DimOverride
      // 
      this.txt_DimOverride.Location = new System.Drawing.Point(175, 83);
      this.txt_DimOverride.Name = "txt_DimOverride";
      this.txt_DimOverride.Size = new System.Drawing.Size(160, 20);
      this.txt_DimOverride.TabIndex = 3;
      this.txt_DimOverride.WordWrap = false;
      // 
      // txt_LeftOverride
      // 
      this.txt_LeftOverride.Location = new System.Drawing.Point(6, 83);
      this.txt_LeftOverride.Name = "txt_LeftOverride";
      this.txt_LeftOverride.Size = new System.Drawing.Size(160, 20);
      this.txt_LeftOverride.TabIndex = 2;
      this.txt_LeftOverride.WordWrap = false;
      // 
      // lbl_IntroLabel
      // 
      this.lbl_IntroLabel.AutoSize = true;
      this.lbl_IntroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_IntroLabel.Location = new System.Drawing.Point(18, 13);
      this.lbl_IntroLabel.Name = "lbl_IntroLabel";
      this.lbl_IntroLabel.Size = new System.Drawing.Size(83, 13);
      this.lbl_IntroLabel.TabIndex = 0;
      this.lbl_IntroLabel.Text = "##null_value##";
      // 
      // form_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(534, 332);
      this.Controls.Add(this.lbl_IntroLabel);
      this.Controls.Add(this.grp_Overrides);
      this.Controls.Add(this.ResetGroup);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(550, 370);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(550, 370);
      this.Name = "form_Main";
      this.Text = "Dimension Overrides";
      this.Load += new System.EventHandler(this.form_Main_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResetGroup.ResumeLayout(false);
      this.grp_Overrides.ResumeLayout(false);
      this.grp_Overrides.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.CheckBox btn_ClearOverrides;
    private System.Windows.Forms.GroupBox ResetGroup;
    private System.Windows.Forms.GroupBox grp_Overrides;
    private System.Windows.Forms.Label lbl_BottomOverride;
    private System.Windows.Forms.TextBox txt_BottomOverride;
    private System.Windows.Forms.TextBox txt_TopOverride;
    private System.Windows.Forms.TextBox txt_RightOverride;
    private System.Windows.Forms.TextBox txt_DimOverride;
    private System.Windows.Forms.TextBox txt_LeftOverride;
    private System.Windows.Forms.Label lbl_TopOverride;
    private System.Windows.Forms.Label lbl_DimOverride;
    private System.Windows.Forms.Label lbl_LeftOverride;
    private System.Windows.Forms.Label lbl_RightOverride;
    private System.Windows.Forms.Label lbl_IntroLabel;
    private System.Windows.Forms.CheckBox btn_ResetPosition;
  }
}