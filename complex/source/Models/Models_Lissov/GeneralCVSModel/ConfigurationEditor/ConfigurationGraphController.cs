using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualControls.ConfigurationEditor;
using System.Windows.Forms;
using ModelBase;
using System.Reflection;

namespace GeneralCVSModel.ConfigurationEditor
{
    internal class ConfigurationGraphController : ConfigurationEditorController
    {

        private ConfigurationEditorControl _view;

        public ConfigurationGraphController(VisualControls.ConfigurationEditor.Configuration config, ConfigurationEditorControl editor)
            :base(config, editor.confEditor)
        {
            _view = editor;
            _editor.panTop.Visible = false;
            VERTEX_RADIUS = 38;
        }

        protected override void ProcessMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.ProcessMouseDown(sender, e);
            string name = "none";
            if (_activeVertex != null && _activeVertex.Properties != null)
                name = (_activeVertex.Properties as IModel).GetName();
            switch (name)
            {
                case "GeneralCVS":
                    SetControl(new ControlGeneralCVSModel());
                    break;
                case "CVSModel":
                    SetControl(new ControlCVSModel());
                    break;
                case "Baroreception":
                    SetControl(new ControlBaroreceptionModel());
                    break;
                case "Kidney":
                    SetControl(new ControlKidneyModel());
                    break;
                case "Fluids":
                    SetControl(new ControlFluidsModel());
                    break;
                case "HeartStable":
                    SetControl(new ControlHeartModel());
                    break;
                case "Loads":
                    SetControl(new ControlLoads());
                    break;
                default:
                    SetControl(new ControlNoModelSelected());
                    break;
            }
        }


        public IModelControl ActiveModelControl = null;
        List<IModelControl> controls = new List<IModelControl>();

        private void SetControl(IModelControl control)
        {
            IModelControl contr = null;
            foreach (var item in controls)
            {
                if (item.GetType().Equals(control.GetType()))
                {
                    contr = item;
                    break;
                }
            }
            if (contr == null)
            {
                contr = control;
                controls.Add(contr);
            }

            if (ActiveModelControl != contr)
            {
                ActiveModelControl = contr;
                _view.splitContr.Panel2.Controls.Clear();
                _view.splitContr.Panel2.Controls.Add(ActiveModelControl as UserControl);
                (ActiveModelControl as UserControl).Dock = DockStyle.Fill;

                ActiveModelControl.Init(
                    _activeVertex != null ? _activeVertex.Properties as IModel : null,
                    _view);
            } 
            
            ActiveModelControl.Update();            
        }
    }
}
