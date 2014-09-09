using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;

namespace Model_Fluids
{
    public partial class FluidsControl : CModelSetupControlFluidsModel
    {
        public FluidsControl(FluidsModel model)
        {
            InitializeComponent();
            _model = model;
            modelSetupControlSimple1.SetModel(_model);
            paramsGridControl1.Init(_model);
        }

        private void btnLoadConfiguation_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _model.configuration.LoadCVSConfiguration(ofd.FileName);
                }
            }
        }
    }

    public class CModelSetupControlFluidsModel : CModelSetupControl<FluidsModel> { }
}
