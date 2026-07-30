namespace Case.ApplySysOrient.UI
{
  partial class form_Orient
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_Orient));
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOk = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.checkBoxConduit = new System.Windows.Forms.CheckBox();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.checkBoxTray = new System.Windows.Forms.CheckBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.checkBoxPipe = new System.Windows.Forms.CheckBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.checkBoxDuct = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.Location = new System.Drawing.Point(138, 313);
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
      this.buttonOk.Location = new System.Drawing.Point(57, 313);
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
      this.groupBox1.Controls.Add(this.pictureBox4);
      this.groupBox1.Controls.Add(this.checkBoxConduit);
      this.groupBox1.Controls.Add(this.pictureBox3);
      this.groupBox1.Controls.Add(this.checkBoxTray);
      this.groupBox1.Controls.Add(this.pictureBox2);
      this.groupBox1.Controls.Add(this.checkBoxPipe);
      this.groupBox1.Controls.Add(this.pictureBox1);
      this.groupBox1.Controls.Add(this.checkBoxDuct);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(201, 295);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "MEP Categories";
      // 
      // pictureBox4
      // 
      this.pictureBox4.BackColor = System.Drawing.Color.White;
      this.pictureBox4.Image = global::Case.ApplySysOrient.Properties.Resources.cond_32;
      this.pictureBox4.Location = new System.Drawing.Point(26, 197);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(50, 50);
      this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox4.TabIndex = 7;
      this.pictureBox4.TabStop = false;
      // 
      // checkBoxConduit
      // 
      this.checkBoxConduit.AutoSize = true;
      this.checkBoxConduit.Checked = true;
      this.checkBoxConduit.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxConduit.Location = new System.Drawing.Point(95, 212);
      this.checkBoxConduit.Name = "checkBoxConduit";
      this.checkBoxConduit.Size = new System.Drawing.Size(62, 17);
      this.checkBoxConduit.TabIndex = 6;
      this.checkBoxConduit.Text = "Conduit";
      this.checkBoxConduit.UseVisualStyleBackColor = true;
      // 
      // pictureBox3
      // 
      this.pictureBox3.BackColor = System.Drawing.Color.White;
      this.pictureBox3.Image = global::Case.ApplySysOrient.Properties.Resources.tray_32;
      this.pictureBox3.Location = new System.Drawing.Point(26, 141);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(50, 50);
      this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox3.TabIndex = 5;
      this.pictureBox3.TabStop = false;
      // 
      // checkBoxTray
      // 
      this.checkBoxTray.AutoSize = true;
      this.checkBoxTray.Checked = true;
      this.checkBoxTray.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxTray.Location = new System.Drawing.Point(95, 156);
      this.checkBoxTray.Name = "checkBoxTray";
      this.checkBoxTray.Size = new System.Drawing.Size(77, 17);
      this.checkBoxTray.TabIndex = 4;
      this.checkBoxTray.Text = "Cable Tray";
      this.checkBoxTray.UseVisualStyleBackColor = true;
      // 
      // pictureBox2
      // 
      this.pictureBox2.BackColor = System.Drawing.Color.White;
      this.pictureBox2.Image = global::Case.ApplySysOrient.Properties.Resources.pipe_32;
      this.pictureBox2.Location = new System.Drawing.Point(26, 85);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(50, 50);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox2.TabIndex = 3;
      this.pictureBox2.TabStop = false;
      // 
      // checkBoxPipe
      // 
      this.checkBoxPipe.AutoSize = true;
      this.checkBoxPipe.Checked = true;
      this.checkBoxPipe.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxPipe.Location = new System.Drawing.Point(95, 100);
      this.checkBoxPipe.Name = "checkBoxPipe";
      this.checkBoxPipe.Size = new System.Drawing.Size(79, 17);
      this.checkBoxPipe.TabIndex = 2;
      this.checkBoxPipe.Text = "HVAC Pipe";
      this.checkBoxPipe.UseVisualStyleBackColor = true;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.White;
      this.pictureBox1.Image = global::Case.ApplySysOrient.Properties.Resources.duct_32;
      this.pictureBox1.Location = new System.Drawing.Point(26, 29);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(50, 50);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // checkBoxDuct
      // 
      this.checkBoxDuct.AutoSize = true;
      this.checkBoxDuct.Checked = true;
      this.checkBoxDuct.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxDuct.Location = new System.Drawing.Point(95, 44);
      this.checkBoxDuct.Name = "checkBoxDuct";
      this.checkBoxDuct.Size = new System.Drawing.Size(81, 17);
      this.checkBoxDuct.TabIndex = 0;
      this.checkBoxDuct.Text = "HVAC Duct";
      this.checkBoxDuct.UseVisualStyleBackColor = true;
      // 
      // form_Orient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(225, 365);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "form_Orient";
      this.Text = "Select Categories";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOk;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.CheckBox checkBoxDuct;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.CheckBox checkBoxConduit;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.CheckBox checkBoxTray;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.CheckBox checkBoxPipe;
  }
}