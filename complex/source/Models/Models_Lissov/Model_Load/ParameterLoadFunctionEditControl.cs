using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;
using VisualControls;

namespace Model_Load
{
    public partial class ParameterLoadFunctionEditControl : UserControl
    {
        ParameterLoadFunction _function;
        public ParameterLoadFunction Function
        {
            get { return _function; }
        }
        public ParameterLoadFunctionEditControl(ParameterLoadFunction function)
        {
            InitializeComponent();

            _function = function;
            DisplayName = _function.DisplayName;
        }

        public string DisplayName { get; set; }
        
        private bool _refreshing = false;
        public void RefreshData()
        {
            _refreshing = true;
            group.Text = DisplayName;
            functionPanel.Init(_function.InnerFunction);
            functionPanel.RedrawGraph();
            checkSimulate.Checked = (_function.Simulate == LissovModelBase.TRUE);
            tbInitial.Text = _function.Output.InitValue.ToString();
            _refreshing = false;
        }

        private void checkSimulate_CheckedChanged(object sender, EventArgs e)
        {
            if (_refreshing) return;
            _function.Simulate.Value =
                (checkSimulate.Checked ? LissovModelBase.TRUE : LissovModelBase.FALSE);
            functionPanel.RedrawGraph();
            OnChanged();
        }

        private void tbInitial_TextChanged(object sender, EventArgs e)
        {
            if (_refreshing) return;
            errorProvider1.Clear();
            double d;
            if (double.TryParse(tbInitial.Text, out d))
            {
                _function.Output.InitValue = d;
                functionPanel.RedrawGraph();
                OnChanged();
            }
            else
                errorProvider1.SetError(tbInitial, "Incorrect numeric value");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (_refreshing) return;
            ParameterFunctionEditForm form = new ParameterFunctionEditForm();
            form.Execute(_function);

            functionPanel.Init(_function.InnerFunction);
            functionPanel.RedrawGraph();

            OnChanged();
        }

        protected virtual void OnChanged()
        {
            EventHandler e = Changed;
            if (e != null)
                e(this, EventArgs.Empty);
        }

        public event EventHandler Changed;
    }
}
