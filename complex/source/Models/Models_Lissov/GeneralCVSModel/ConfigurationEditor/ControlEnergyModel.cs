using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;
using Model_Energy;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlEnergyModel : /*UserControl//:*/ IModelControlEnergyModel
    {
        public ControlEnergyModel()
        {
            InitializeComponent();
        }

        public override void Update()
        {
            base.Update();
            tbTimeKoeff.Text = ((EnergyModel)_model).TimeParameter.Value.ToString();
        }

        private void tbTimeKoeff_TextChanged(object sender, EventArgs e)
        {
            double d;
            if (double.TryParse(tbTimeKoeff.Text, out d))
                ((EnergyModel)_model).TimeParameter.Value = d;
        }
    }

    public class IModelControlEnergyModel : IModelControl<EnergyModel> { }
}
