using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_Fluids;
using LissovBase;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlFluidsModel : IModelControlFluidsModel
    {
        public ControlFluidsModel()
        {
            InitializeComponent();
        }

        public override void Update()
        {
            base.Update();
            cbFiltrationRegulation.CheckState = _GeneralSetupControl.cbCapillaryFiltration.CheckState;
            cbSimulate.Checked = (_model as FluidsModel).SimulateFluids.Value == LissovModelBase.TRUE;
        }

        private void cbFiltrationRegulation_CheckedChanged(object sender, EventArgs e)
        {
            _GeneralSetupControl.cbCapillaryFiltration.CheckState = cbFiltrationRegulation.CheckState;
        }

        private void cbSimulate_CheckedChanged(object sender, EventArgs e)
        {
            (_model as FluidsModel).SimulateFluids.Value = cbSimulate.Checked ? LissovModelBase.TRUE : LissovModelBase.FALSE;
        }
    }

    public class IModelControlFluidsModel : IModelControl<FluidsModel> { }
}
