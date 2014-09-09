using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using LissovBase.Functions;

namespace VisualControls.Editors
{
    public partial class BaseParameterEditor : UserControl
    {
        public BaseParameterEditor()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return lTitle.Text; }
            set { lTitle.Text = value; }
        }

        public virtual string Unit
        {
            get { return lUnit.Text; }
            set { lUnit.Text = value; }
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged()
        {
            EventHandler e = ValueChanged;
            if (e != null)
                e(this, EventArgs.Empty);
        }

        public virtual object Value
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        public static BaseParameterEditor GetParameterEditor(EditorType editor, Type propertyType)
        {
            switch (editor)
            {
                case EditorType.Text:
                    return new TextParameterEditor();
                case EditorType.Double:
                    return new DoubleParameterEditor();
                case EditorType.Time:
                    return new TimeParameterEditor();
                case EditorType.Default:
                default:
                    if (propertyType == typeof(double))
                        return new DoubleParameterEditor();
                    return new TextParameterEditor();
            }
        }
    }
}
