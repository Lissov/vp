using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Collections;
using System.Reflection;
using DevExpress.XtraGrid.Columns;

namespace VisualControls
{
    public partial class GridColumnValuesChangeForm : Form
    {
        private GridControl _grid;
        
        public GridColumnValuesChangeForm(GridControl grid)
        {
            InitializeComponent();
            _grid = grid;

            SetComboValues();
        }

        private void SetComboValues()
        {
            cbParameters.Items.Clear();
            foreach (GridColumn item in (_grid.Views[0] as DevExpress.XtraGrid.Views.Grid.GridView).Columns)
            {
                if (item.ColumnEdit == null && item.OptionsColumn.AllowEdit)
                    cbParameters.Items.Add(item.Caption);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            double nv = 0;
            double p = 0;
            if (rbNewValue.Checked)
                if (!double.TryParse(tbNewValue.Text, out nv))
                {
                    errorProvider1.SetError(tbNewValue, "Not a correct double value");
                    return;
                }
            if (rbPercentage.Checked)
                if (!double.TryParse(tbPercent.Text, out p))
                {
                    errorProvider1.SetError(tbPercent, "Not a correct double value");
                    return;
                }
            if (cbParameters.SelectedIndex < 0)
            {
                errorProvider1.SetError(cbParameters, "Select a parameter");
                return;
            }

            foreach (GridColumn item in (_grid.Views[0] as DevExpress.XtraGrid.Views.Grid.GridView).Columns)
            {
                if (item.Caption == cbParameters.Text)
                {
                    PropertyInfo fi = null;
                    foreach (object o in (_grid.DataSource as IList))
                    {
                        if (fi == null) fi = o.GetType().GetProperty(item.FieldName);
                        double v = (double)fi.GetValue(o, null);
                        if (rbNewValue.Checked) v = nv;
                        if (rbPercentage.Checked) v = v * p / 100;
                        fi.SetValue(o, v, null);
                    }
                    break;
                }
            }
            _grid.RefreshDataSource();
            DialogResult = DialogResult.OK;
        }

        private void rbNewValue_CheckedChanged(object sender, EventArgs e)
        {
            /*if (sender == rbNewValue)
                rbPercentage.Checked = !rbNewValue.Checked;
            if (sender == rbPercentage)
                rbNewValue.Checked = !rbPercentage.Checked;*/
        }
    }
}
