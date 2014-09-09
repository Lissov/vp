using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualControls.Editors
{
    public partial class DoubleParameterEditor : TextParameterEditor
    {
        public DoubleParameterEditor()
        {
            InitializeComponent();
        }

        public double? DoubleValue
        {
            get
            {
                double d;
                if (double.TryParse(TextValue, out d))
                    return d;
                return null;
            }
            set
            {
                if (value == null)
                    TextValue = string.Empty;
                else
                    TextValue = value.ToString();
            }
        }

        protected override void OnValueChanged()
        {
            if (Value == null)
                errorProvider1.SetError(lUnit, "Please insert a correct double value");
            else
                base.OnValueChanged();
        }

        public override object Value
        {
            get { return DoubleValue; }
            set { DoubleValue = (double)value; }
        }
    }
}
