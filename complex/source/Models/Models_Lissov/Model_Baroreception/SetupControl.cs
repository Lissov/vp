using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;

namespace Model_Baroreception
{
    public partial class SetupControl : CModelSetupControlBaroreception
    {
        public SetupControl(BaroreceptionModel model)
        {
            InitializeComponent();
            _model = model;

            tabZones.TabPages.Clear();
            for (int i = 0; i < _model.baroZoneCount; i++)
            {
                TabPage tp = new TabPage(_model.zoneID[i]);
                ReceptorZoneControl contr = new ReceptorZoneControl();
                contr.Dock = DockStyle.Fill;
                tp.Controls.Add(contr);
                tabZones.TabPages.Add(tp);
                contr.Init(i, _model);
            }

            RefreshControl();
        }

        private void btnLoadVPOle_Click(object sender, EventArgs e)
        {
            ModelHelper.LoadVPOleParams(_model);
        }

        private void cbUseBaroreflex_CheckedChanged(object sender, EventArgs e)
        {
            _model.BaroreceptionCopyFirst.Value = ModelHelper.BoolToDouble(cbUseBaroreflex.Checked);
        }

        #region IModelSetupControl Members

        public void RefreshControl()
        {
            cbUseBaroreflex.Checked = _model.BaroreceptionCopyFirst.Value == LissovModelBase.TRUE;
            foreach (TabPage page in tabZones.TabPages)
            {
                ReceptorZoneControl rzc = page.Controls[0] as ReceptorZoneControl;
                if (rzc != null)
                    rzc.ShowData();
            }
        }

        #endregion
        
    }

    public class CModelSetupControlBaroreception : CModelSetupControl<BaroreceptionModel>
    {
    }
}
