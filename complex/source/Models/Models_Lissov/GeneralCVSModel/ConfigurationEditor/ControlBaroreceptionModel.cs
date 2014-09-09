using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_Baroreception;
using LissovBase;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlBaroreceptionModel : IModelControlBaroreceptionModel
    {
        public ControlBaroreceptionModel()
        {
            InitializeComponent();
        }

        private void cbRegulation_CheckedChanged(object sender, EventArgs e)
        {
            _GeneralSetupControl.cbBaroreception.CheckState = cbRegulation.CheckState;
        }

        private void cbWillis_CheckedChanged(object sender, EventArgs e)
        {
            _GeneralSetupControl.cbWillisBaroreception.CheckState = cbWillis.CheckState;
        }

        private void cbUseThermoHRDelta_CheckedChanged(object sender, EventArgs e)
        {
            (_model as BaroreceptionModel).HeartRateThermoDelta.Use.Value =
                cbUseThermoHRDelta.Checked ? LissovModelBase.TRUE : LissovModelBase.FALSE;
        }

        public override void Update()
        {
            cbRegulation.CheckState = _GeneralSetupControl.cbBaroreception.CheckState;
            cbWillis.CheckState = _GeneralSetupControl.cbWillisBaroreception.CheckState;
            cbUseThermoHRDelta.Checked = (_model as BaroreceptionModel).HeartRateThermoDelta.Use.Value == LissovModelBase.TRUE;
        }
    }

    public class IModelControlBaroreceptionModel : IModelControl<BaroreceptionModel> { }
}
