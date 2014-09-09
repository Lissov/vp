using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase.Functions;

namespace VisualControls
{
    public partial class FunctionEditForm : Form
    {
        public FunctionEditForm()
        {
            InitializeComponent();
        }

        public void Execute(Function function, bool modal)
        {
            IFunctionEditPanel panel = new FunctionEditPanel();
            this.Controls.Add(panel as Control);
            (panel as Control).Dock = DockStyle.Fill;
            panel.Init(function);
            this.Text = "Edit function: " + function.GetType().Name;
            if (modal)
                ShowDialog();
            else
                Show();
        }

        public static void ShowEditForm(Function function, bool modal)
        {
            FunctionEditForm form = new FunctionEditForm();
            form.Execute(function, modal);
        }
    }
}
