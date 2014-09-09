using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelBase;
using VisualControls.ConfigurationEditor;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ConfigurationEditorControl : UserControl
    {
        private GeneralModel _model;
        private ConfigurationEditorController _controller;

        public ConfigurationEditorControl(GeneralModel model)
        {
            InitializeComponent();

            Init(model);
        }

        VisualControls.ConfigurationEditor.Configuration _modelsConfig = null;

        private void Init(GeneralModel model)
        {
            _model = model;

            _model.Model_Count = _model.Configuration.Models.Count;

            _modelsConfig = getConfiguration(_model.Configuration);
            _controller = new ConfigurationGraphController(_modelsConfig, this);
            _controller.EditRealHeightEnable = false;
            _controller.PropertyGridVisible = false;
            _controller.VertexMoved = VertexMoved;
        }

        private List<string> modelIds;
        private VisualControls.ConfigurationEditor.Configuration getConfiguration(ModelBase.Configuration models_config)
        {
            VisualControls.ConfigurationEditor.Configuration res = new VisualControls.ConfigurationEditor.Configuration();
            modelIds = new List<string>();
            for (int i = 0; i < models_config.Models.Count; i++)
            {
                IModel model = models_config.Models[i];
                //if (model == _model) continue;
                res.Vertices.Add(new Vertex()
                {
                    ID = i,
                    DisplayText = model.DisplayName.Replace(' ', '\n').Replace("Baroreception", "Neural\ncontrol").
                    Replace("Stable\nHeart", "Static\nHeart").Replace("Model\nfor\nConstants", "Loads"),
                    X = _model._modelPositionX[i],
                    Y = _model._modelPositionY[i],
                    Properties = model
                });

                modelIds.Add(model.GetName());
            }

            int linkid = 0;
            foreach (var item in models_config.Models)
            {
                //if (item == _model) continue;

                List<int> links = new List<int>();
                foreach (var value in item.GetValues())
                {
                    if (value.Type != Value.ValueType.Input) continue;
                    int modelnum = modelIds.IndexOf(value.LinkModelName);
                    if (modelnum >= 0 && !links.Contains(modelnum))
                        links.Add(modelnum);
                }
                foreach (int num in links)
                {
                    res.Links.Add(new Link()
                    {
                        Begin = res.Vertices[num],
                        End = res.Vertices[modelIds.IndexOf(item.GetName())],
                        ID = linkid++
                    });
                }
            }

            return res;
        }

        public void VertexMoved(Vertex v)
        {
            _model._modelPositionX[v.ID] = v.X;
            _model._modelPositionY[v.ID] = v.Y;
        }

        public virtual void Update()
        {
            if (ActiveControl != null)
                ActiveControl.Update();
            base.Update();
        }
    }
}
