using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thermoregulation
{
    public partial class ThermoSetupControl : UserControl
    {
        ThermoregulationModel _model;

        public ThermoSetupControl(ThermoregulationModel model)
        {
            _model = model;

            InitializeComponent();
        }
    }
}
