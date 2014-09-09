using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase.Functions;
using LissovBase;

namespace Model_Load
{
    public partial class LoadsSetupControl : FLoadsSetupControl
    {
        public LoadsSetupControl()
        {
            InitializeComponent();
        }

        private List<ParameterLoadFunctionEditControl> controls;
        internal void Init(LoadModel model)
        {
            _model = model;

            controls = new List<ParameterLoadFunctionEditControl>();

            foreach (var func in _model.LoadFunctions)
            {
                if (func.InnerFunction.MaxX < (double)_model.ExperimentTime)
                    func.InnerFunction.MaxX = (double)_model.ExperimentTime;

                var control = new ParameterLoadFunctionEditControl(func);
                control.Height = 184;
                control.Width = 400;
                flowLayoutPanel1.Controls.Add(control);
                controls.Add(control);
            }

            RefreshControl();
        }

        #region IModelSetupControl Members

        public void RefreshControl()
        {
            foreach (var item in controls)
            {
                item.RefreshData();
            }
        }

        #endregion

        private void LoadsSetupControl_Enter(object sender, EventArgs e)
        {
            RefreshControl();
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            int w = flowLayoutPanel1.ClientSize.Width;
            if (flowLayoutPanel1.VerticalScroll.Visible)
                w -= 10;
            if (w > 800) w /= 2;
            w -= 1;
            foreach (var item in controls)
            {
                item.Width = w;
            }
        }
    }

    public class FLoadsSetupControl : CModelSetupControl<LoadModel>
    {
    }
}
