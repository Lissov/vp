using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase.Functions;

namespace VisualControls
{
    public partial class ParameterFunctionControl : UserControl
    {
        public ParameterFunctionControl()
        {
            InitializeComponent();
        }

        public ParameterFunctionControl(ParameterFunction paramFunction)
            :base()
        {
            Init(paramFunction);
        }

        private ParameterFunction _param;

        public void Init(ParameterFunction paramFunction)
        {
            _param = paramFunction;
            cbFunctionType.Items.Clear();
            foreach (var item in FunctionFactory.Instance.GetAllTypes())
                cbFunctionType.Items.Add(item);
            RefreshControl();
        }

        private void cbFunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cbFunctionType.SelectedItem == _param.InnerFunction.FunctionType) return;
            Function f = FunctionFactory.Instance.GetFunction((string)cbFunctionType.SelectedItem, _param.InnerFunction.Unit);
            _param.InnerFunction = f;
            fePanel.Init(_param.InnerFunction);
        }

        public void RefreshControl()
        {
            cbFunctionType.SelectedItem = _param.InnerFunction.FunctionType;
            fePanel.Init(_param.InnerFunction);
        }
    }
}
