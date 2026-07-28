namespace Case.ViewCreator
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
      this.btnCreate = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cbViewType = new System.Windows.Forms.ComboBox();
      this.labelViewType = new System.Windows.Forms.Label();
      this.labelViewDiscipline = new System.Windows.Forms.Label();
      this.cbViewDiscipline = new System.Windows.Forms.ComboBox();
      this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
      this.labelLevels = new System.Windows.Forms.Label();
      this.checkBoxAll = new System.Windows.Forms.CheckBox();
      this.labelSubDiscipline = new System.Windows.Forms.Label();
      this.textSubDiscipline = new System.Windows.Forms.TextBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.ButtonHelp = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnCreate
      // 
      this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCreate.Location = new System.Drawing.Point(151, 410);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(110, 40);
      this.btnCreate.TabIndex = 0;
      this.btnCreate.Text = "Create Views";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(267, 410);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 40);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // cbViewType
      // 
      this.cbViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbViewType.FormattingEnabled = true;
      this.cbViewType.Location = new System.Drawing.Point(101, 11);
      this.cbViewType.Name = "cbViewType";
      this.cbViewType.Size = new System.Drawing.Size(246, 21);
      this.cbViewType.TabIndex = 2;
      // 
      // labelViewType
      // 
      this.labelViewType.AutoSize = true;
      this.labelViewType.Location = new System.Drawing.Point(9, 14);
      this.labelViewType.Name = "labelViewType";
      this.labelViewType.Size = new System.Drawing.Size(57, 13);
      this.labelViewType.TabIndex = 3;
      this.labelViewType.Text = "View Type";
      // 
      // labelViewDiscipline
      // 
      this.labelViewDiscipline.AutoSize = true;
      this.labelViewDiscipline.Location = new System.Drawing.Point(9, 47);
      this.labelViewDiscipline.Name = "labelViewDiscipline";
      this.labelViewDiscipline.Size = new System.Drawing.Size(78, 13);
      this.labelViewDiscipline.TabIndex = 4;
      this.labelViewDiscipline.Text = "View Discipline";
      // 
      // cbViewDiscipline
      // 
      this.cbViewDiscipline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbViewDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbViewDiscipline.FormattingEnabled = true;
      this.cbViewDiscipline.Location = new System.Drawing.Point(101, 44);
      this.cbViewDiscipline.Name = "cbViewDiscipline";
      this.cbViewDiscipline.Size = new System.Drawing.Size(246, 21);
      this.cbViewDiscipline.TabIndex = 5;
      // 
      // checkedListBox1
      // 
      this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.checkedListBox1.CheckOnClick = true;
      this.checkedListBox1.FormattingEnabled = true;
      this.checkedListBox1.Location = new System.Drawing.Point(12, 165);
      this.checkedListBox1.Name = "checkedListBox1";
      this.checkedListBox1.Size = new System.Drawing.Size(335, 229);
      this.checkedListBox1.TabIndex = 6;
      // 
      // labelLevels
      // 
      this.labelLevels.AutoSize = true;
      this.labelLevels.Location = new System.Drawing.Point(12, 139);
      this.labelLevels.Name = "labelLevels";
      this.labelLevels.Size = new System.Drawing.Size(118, 13);
      this.labelLevels.TabIndex = 7;
      this.labelLevels.Text = "Levels to Create Views:";
      // 
      // checkBoxAll
      // 
      this.checkBoxAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.checkBoxAll.AutoSize = true;
      this.checkBoxAll.Location = new System.Drawing.Point(199, 139);
      this.checkBoxAll.Name = "checkBoxAll";
      this.checkBoxAll.Size = new System.Drawing.Size(151, 17);
      this.checkBoxAll.TabIndex = 8;
      this.checkBoxAll.Text = "Select/Deselect All Levels";
      this.checkBoxAll.UseVisualStyleBackColor = true;
      this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
      // 
      // labelSubDiscipline
      // 
      this.labelSubDiscipline.AutoSize = true;
      this.labelSubDiscipline.Location = new System.Drawing.Point(12, 84);
      this.labelSubDiscipline.Name = "labelSubDiscipline";
      this.labelSubDiscipline.Size = new System.Drawing.Size(170, 13);
      this.labelSubDiscipline.TabIndex = 9;
      this.labelSubDiscipline.Text = "Sub-Discipline (MEP Product Only)";
      // 
      // textSubDiscipline
      // 
      this.textSubDiscipline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textSubDiscipline.Location = new System.Drawing.Point(15, 105);
      this.textSubDiscipline.Name = "textSubDiscipline";
      this.textSubDiscipline.Size = new System.Drawing.Size(335, 20);
      this.textSubDiscipline.TabIndex = 10;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(12, 410);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(335, 40);
      this.progressBar1.TabIndex = 11;
      // 
      // ButtonHelp
      // 
      this.ButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ButtonHelp.Location = new System.Drawing.Point(95, 410);
      this.ButtonHelp.Name = "ButtonHelp";
      this.ButtonHelp.Size = new System.Drawing.Size(50, 40);
      this.ButtonHelp.TabIndex = 13;
      this.ButtonHelp.Text = "?";
      this.ButtonHelp.UseVisualStyleBackColor = true;
      this.ButtonHelp.Click += new System.EventHandler(this.ButtonHelp_Click);
      // 
      // form_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(359, 462);
      this.Controls.Add(this.ButtonHelp);
      this.Controls.Add(this.textSubDiscipline);
      this.Controls.Add(this.labelSubDiscipline);
      this.Controls.Add(this.checkBoxAll);
      this.Controls.Add(this.labelLevels);
      this.Controls.Add(this.checkedListBox1);
      this.Controls.Add(this.cbViewDiscipline);
      this.Controls.Add(this.labelViewDiscipline);
      this.Controls.Add(this.labelViewType);
      this.Controls.Add(this.cbViewType);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnCreate);
      this.Controls.Add(this.progressBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(375, 500);
      this.Name = "form_Main";
      this.Text = "Create Views by Discipline";
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.Label labelViewType;
        private System.Windows.Forms.Label labelViewDiscipline;
        private System.Windows.Forms.ComboBox cbViewDiscipline;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label labelLevels;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Label labelSubDiscipline;
        private System.Windows.Forms.TextBox textSubDiscipline;
        private System.Windows.Forms.ProgressBar progressBar1;
        internal System.Windows.Forms.Button ButtonHelp;
    }
}