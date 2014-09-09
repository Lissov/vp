using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelBase;

namespace LissovBase
{
    public class CModelSetupControl : UserControl, IModelSetupControl
    {
        #region IModelSetupControl Members

        public new virtual void Update()
        {
            EventHandler e = OnUpdate;
            if (e != null)
                e(this, EventArgs.Empty);
        }

        public virtual ModelBase.IModel Model
        {
            get;
            set;
        }

        public event EventHandler OnUpdate;

        public virtual Type GetModelType()
        {
            return typeof(IModel);
        }

        #endregion
    }

    public class CModelSetupControl<T> : CModelSetupControl
        where T:class, IModel
    {
        protected T _model
        {
            get { return Model as T; }
            set { Model = value; }
        }

        public override Type GetModelType()
        {
            return typeof(T);
        }
    }
}
