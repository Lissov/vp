using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_Kidney;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlKidneyModel : IModelControlKidneyModel
    {
        public ControlKidneyModel()
        {
            InitializeComponent();
        }

        private void cbKidney_CheckedChanged(object sender, EventArgs e)
        {
            _GeneralSetupControl.cbKidney.CheckState = cbKidney.CheckState;
        }

        private void cbRegulation_CheckedChanged(object sender, EventArgs e)
        {
            _GeneralSetupControl.cbKidneyRegulation.CheckState = cbRegulation.CheckState;
        }

        public override void Update()
        {
            base.Update();
            cbKidney.CheckState = _GeneralSetupControl.cbKidney.CheckState;
            cbRegulation.CheckState = _GeneralSetupControl.cbKidneyRegulation.CheckState;
        }
    }

    public class IModelControlKidneyModel : IModelControl<KidneyModel> { }
}
