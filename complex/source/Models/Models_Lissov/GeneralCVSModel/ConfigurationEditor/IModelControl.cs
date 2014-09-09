using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using System.Windows.Forms;
using LissovBase;

namespace GeneralCVSModel.ConfigurationEditor
{
    public interface IModelControl : IModelSetupControl
    {
        void Init(IModel model, ConfigurationEditorControl editor);
    }

    public class IModelControl<T> : CModelSetupControl<T>, IModelControl
        where T : class, IModel
    {        
        protected ConfigurationEditorControl _genEditor;
        protected GeneralSetupControl _GeneralSetupControl;

        public virtual void Init(IModel model, ConfigurationEditorControl editor)
        {
            Model = model;
            _genEditor = editor;
            _GeneralSetupControl = _genEditor.Parent.Parent.Parent as GeneralSetupControl;
        }
    }
}
