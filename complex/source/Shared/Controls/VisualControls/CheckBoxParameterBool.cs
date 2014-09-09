using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;
using ModelBase;
using System.Reflection;
using System.ComponentModel;

namespace VisualControls
{
    public class CheckBoxParameterBool : CheckBox
    {
        private IModelSetupControl _setupcontrol;

        [TypeConverter(typeof(ParameterNameConverter))]
        public string ParameterName
        {
            get;
            /*{
                if (_param == null) return string.Empty;
                return _param.DisplayName;
            }*/
            set;
            /*{
                try
                {
                    if (value == null) _param = null;
                    if (_modeltype == null) return;
                    PropertyInfo pi = _modeltype.GetType().GetProperty(value);
                    if (pi != null)
                        _param = pi.GetValue(_model, null) as ParameterBool;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex);
                }
            }*/
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (_setupcontrol == null)
                UpdateParent();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (_setupcontrol == null)
                UpdateParent();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            try
            {
                UpdateParent();

                base.OnParentChanged(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex);
            }
        }

        private void UpdateParent()
        {
            if (_setupcontrol != null)
                _setupcontrol.OnUpdate -= ms_OnUpdate;

            _setupcontrol = getParentControl(Parent);
            if (_setupcontrol != null)
            {
                _setupcontrol.OnUpdate += new EventHandler(ms_OnUpdate);
                updateValue();
            }
        }

        void ms_OnUpdate(object sender, EventArgs e)
        {
            updateValue();
        }

        private void updateValue()
        {
            ParameterBool pb = getParameterBool();
            if (pb != null)
                this.Checked = pb.BoolValue;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            ParameterBool pb = getParameterBool();
            if (pb != null)
                pb.BoolValue = this.Checked;
        }

        private ParameterBool getParameterBool()
        {
            if (string.IsNullOrEmpty(ParameterName))
                return null;
            FieldInfo fi = _setupcontrol.GetModelType().GetField(ParameterName);
            if (fi == null)
            {
                MessageBox.Show(string.Format("Can't find property [{0}]", ParameterName));
                return null;
            }
            if (_setupcontrol.Model == null)
            {
                MessageBox.Show("Model is null");
                return null;
            }
            return fi.GetValue(_setupcontrol.Model) as ParameterBool;
        }

        public IModelSetupControl getParentControl(Control c)
        {
            if (c == null) return null;
            if (c is IModelSetupControl)
                return c as IModelSetupControl;
            if (c.Parent == null) return null;

            return getParentControl(c.Parent);
        }
        
        #region ParametersList
		public class ParameterNameConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }

            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                try
                {
                    CheckBoxParameterBool cbpb = context.Instance as CheckBoxParameterBool;
                    if (cbpb == null) return new StandardValuesCollection(new string[0]);

                    if (cbpb._setupcontrol == null)
                    {
                        MessageBox.Show("SetupControl is null");
                        return new StandardValuesCollection(new string[0]); ;
                    }

                    Type modeltype = cbpb._setupcontrol.GetModelType();
                    List<string> names = new List<string>();
                    FieldInfo[] props = modeltype.GetFields();
                    foreach (var prop in props)
                    {
                        if (prop.FieldType.Equals(typeof(ParameterBool)) ||
                            prop.FieldType.IsSubclassOf(typeof(ParameterBool)))
                        {
                            names.Add(prop.Name);
                        }
                    }
                    return new StandardValuesCollection(names);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex);
                }
                return new StandardValuesCollection(null);
            }
        } 
	    #endregion        
    }

}
