using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovLog;

namespace LissovBase
{
    public partial class ModelSetupControlSimple : UserControl
    {
        LissovModelBase _model;
        public void SetModel(LissovModelBase model)
        {
            _model = model;
            if (_model == null || string.IsNullOrEmpty(_model.VP_OLE_GUID))
            {
                btnLoadVPOle.Visible = false;
                logControl1.Dock = DockStyle.Fill;
            }
            else
            {
                btnLoadVPOle.Visible = true;
                logControl1.Dock = DockStyle.None;
                logControl1.Top = btnLoadVPOle.Bottom + 4;
                logControl1.Height = this.ClientSize.Height - logControl1.Top - 2;
                logControl1.Left = 1;
                logControl1.Width = this.ClientSize.Width - 2;
                logControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        public ModelSetupControlSimple()
        {
            InitializeComponent();
            SetModel(null);
        }

        public ModelSetupControlSimple(LissovModelBase model)
        {
            InitializeComponent();
            SetModel(model);
        }

        private void btnLoadVPOle_Click(object sender, EventArgs e)
        {
            ModelHelper.LoadVPOleParams(_model);
        }
    }
}
