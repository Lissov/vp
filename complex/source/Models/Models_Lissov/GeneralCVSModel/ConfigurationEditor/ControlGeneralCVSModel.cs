using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlGeneralCVSModel : IModelControlGeneralModel
    {
        public ControlGeneralCVSModel()
        {
            InitializeComponent();
        }

        public override void Update()
        {
            base.Update();
            ShowSteps();
        }
        
        private void ShowSteps()
        {
            tbStep.Text = string.Join(", ", 
                _model.Configuration.Models.Select(m => m.Step + "(" + m.DisplayName + ")").ToArray());
        }

        private void StepMultiply(decimal multiplier)
        {
            foreach (var model in _model.Configuration.Models)
                model.Step *= multiplier;

            ShowSteps();
        }

        private void btnStepDecrease_Click(object sender, EventArgs e)
        {
            StepMultiply(0.5m);
        }

        private void btnStepIncrease_Click(object sender, EventArgs e)
        {
            StepMultiply(2.0m);
        }
    }

    public class IModelControlGeneralModel : IModelControl<GeneralModel> { }
}
