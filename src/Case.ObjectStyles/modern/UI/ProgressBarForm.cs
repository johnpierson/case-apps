using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;

//using Autodesk.Revit.UI;
//using Autodesk.Revit.DB;
//using Autodesk.Revit.DB.Architecture;

namespace Case.TemplateLocal {

    public partial class ProgressBarForm : System.Windows.Forms.Form {

        public bool Cancel = false;

        public ProgressBarForm(string title, int maximum) {
            InitializeComponent();
            this.Text = title;
            progressBar1.Maximum = maximum;
        }

        public void SetLabel(string text) {
            labelProgressBar.Text = text;
            labelProgressBar.Refresh();
        }

        public void Increment() {
            progressBar1.Increment(1);
        }

        public void Reset() {
            progressBar1.Value = 0;
            progressBar1.Refresh();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Cancel = true;
        }
    }
   
}