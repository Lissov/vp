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
    public partial class ParameterFunctionEditForm : Form
    {
        public ParameterFunctionEditForm()
        {
            InitializeComponent();
        }

        public void Execute(ParameterFunction function)
        {
            parameterFunctionControl1.Init(function);
            ShowDialog();
        }
    }
}
