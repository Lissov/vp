using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Model_CVS
{
    public partial class VirtualCompartmentForm : Form
    {
        private VirtualCompartmentForm()
        {
            InitializeComponent();
        }

        public static VirtualCompartment Execute(CVSModel model)
        {
            VirtualCompartmentForm form = new VirtualCompartmentForm();
            model.configuration.FillCombo(form.cbCompartment1);
            model.configuration.FillCombo(form.cbCompartment2);

            if (form.ShowDialog() == DialogResult.Yes)
            {
                VirtualCompartment res = new VirtualCompartment(model);
                res.Name = form.tbName.Text;
                res.Compartment1.Value = form.cbCompartment1.SelectedIndex;
                res.Compartment2.Value = form.cbCompartment2.SelectedIndex;
                res.Compartment1Gain.Value = double.Parse(form.tbGain1.Text);
                res.Compartment2Gain.Value = double.Parse(form.tbGain2.Text);
                return res;
            }
            else
                return null;
        }
    }
}
