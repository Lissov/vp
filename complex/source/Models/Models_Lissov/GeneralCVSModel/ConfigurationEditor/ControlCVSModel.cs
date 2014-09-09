using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualControls.ConfigurationEditor;
using Model_CVS;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlCVSModel : IModelControlCVSModel
    {
        public ControlCVSModel()
        {
            InitializeComponent();
        }

        ConfigurationEditorController controller;
        Configuration _visualConfig;
        public override void Init(ModelBase.IModel model, ConfigurationEditorControl editor)
        {
            base.Init(model, editor);

            _visualConfig = ((model as CVSModel).GetControl() as SetupControl).GetVisualConfig(300, 300);
            controller = new ConfigurationEditorController(
                _visualConfig,
                configurationEditor1)
                {
                    VERTEX_RADIUS = 26,
                    vertexFont = new Font(FontFamily.GenericSerif, 8, FontStyle.Italic)
                };
            controller.ApplyPerformed += ApplyGraph;
        }
        private void ApplyGraph(object sender, EventArgs e)
        {
            for (int i = 0; i < (_model as CVSModel).NetConfiguration.CompartmentCount; i++)
            {
                OneCompartmentData ocd = (_visualConfig.Vertices[i].Properties as OneCompartmentData);
                if (ocd != null)
                    ocd.applyToModel(_model as CVSModel, i);
            }
        }
    }

    public class IModelControlCVSModel : IModelControl<CVSModel> { }
}
