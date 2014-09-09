using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_Load;
using LissovBase;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlLoads : IModelControlLoadModel
    {
        public ControlLoads()
        {
            InitializeComponent();
        }

        private bool _inited = false;

        

        public override void Update()
        {
            base.Update();

            CheckInit();

            foreach (var item in controls)
            {
                item.RefreshData();
            }
        }

        List<ParameterLoadFunctionEditControl> controls;
        private void CheckInit()
        {
            if (_inited) return;

            panLoads.Controls.Clear();
            controls = new List<ParameterLoadFunctionEditControl>();
            foreach (var item in CVSSubModels.LoadModel.LoadFunctions)
            {
                var control = new ParameterLoadFunctionEditControl(item);
                panLoads.Controls.Add(control);
                control.Dock = DockStyle.Top;
                control.Height = 200;
                controls.Add(control);

                control.Changed += new EventHandler(control_Changed);

                control.btnChange.Text = "Change";// "Изменить";
                control.checkSimulate.Text = "Simulate";// "Моделировать";
                control.labelInitial.Text = "Initial value";// "Начальное значение:";
                control.DisplayName = item.DisplayName;
                /*switch (item.Name)
                {
                    case "AtmospherePressureFunction":
                        control.DisplayName = "Атмосферное давление";
                        break;
                    case "MuscularActivityFunction":
                        control.DisplayName = "Мышечная активность";
                        break;
                    case "AngleFunction":
                        control.DisplayName = "Положение тела";
                        break;
                    case "HemorrhageFunction":
                        control.DisplayName = "Кровопотеря";
                        break;
                    case "ExternalTemperatureFunction":
                        control.DisplayName = "Температура окружающей среды";
                        break;
                    case "WaterDrinkingRateFunction":
                        control.DisplayName = "Потребление воды";
                        break;
                }*/
            }

            _inited = true;
        }

        void control_Changed(object sender, EventArgs e)
        {
            IModelSetupControl contr = (CVSSubModels.LoadModel.GetControl() as IModelSetupControl);
            if (contr != null)
                contr.Update();

            ParameterLoadFunctionEditControl control = sender as ParameterLoadFunctionEditControl;
            if (control != null)
            {
                if (control.Function.InnerFunction.MaxX > (double)CVSSubModels.LoadModel.ExperimentTime)
                {
                    MessageBox.Show("Load time is greater than experiment time");                        
                }
            }
        }
    }

    public class IModelControlLoadModel : IModelControl<LoadModel> { }
}
