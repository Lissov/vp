using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase.Functions;
using DevExpress.XtraCharts;
using System.Reflection;
using VisualControls.Editors;
using VisualControls.Helpers;
using LissovBase;

namespace VisualControls
{
    public partial class FunctionEditPanel : UserControl, IFunctionEditPanel
    {
        public FunctionEditPanel()
        {
            InitializeComponent();

            XYDiagram diagram = (chart.Diagram as XYDiagram);
            diagram.AxisX.Title.Font = diagram.AxisX.Label.Font;
            diagram.AxisY.Title.Font = diagram.AxisY.Label.Font;
        }

        Function _function;

        #region IFunctionEditPanel Members

        public void Init(LissovBase.Functions.Function function)
        {
            _function = function;
            DrawGraph();
            ShowEditors();
        }

        public bool EditorsVisible
        {
            get { return panParams.Visible; }
            set { panParams.Visible = value; }
        }

        public void RedrawGraph()
        {
            DrawGraph();
            //ShowEditors(); //PL:TODO
        }
        #endregion

        private void DrawGraph()
        {
            Series ser = chart.Series[0];
            ser.Points.Clear();

            TimeHelper.TimeDivider timediv = TimeHelper.GetOptimalUnit(_function);
            (chart.Diagram as XYDiagram).AxisX.Title.Text = timediv.Unit;
            (chart.Diagram as XYDiagram).AxisY.Title.Text = _function.Unit;

            if (_function.MaxX == _function.MinX) return;
            if (_function == null) return;
            double step = (_function.MaxX - _function.MinX) / 250;

            try
            {
                for (double d = _function.MinX; d <= _function.MaxX; d+= step)
                {
                    ser.Points.Add(new SeriesPoint((d / (double)timediv.Divider), new double[] { _function.getValue(d) }));
                }
            }
            catch (FormatException) { }
        }

        private void ShowEditors()
        {
            panParams.Controls.Clear();
            if (_function == null)
            {
                panParams.Height = 0;
                return;
            }

            PropertyInfo[] pars = _function.GetType().GetProperties();
            int h = 0;
            foreach (var par in pars)
            {
                foreach (var attr in par.GetCustomAttributes(true))
                {
                    if (attr is FunctionEditableParameterAttribute)
                    {
                        FunctionEditableParameterAttribute feAttr = attr as FunctionEditableParameterAttribute;
                        string title = feAttr.Title;
                        if (string.IsNullOrEmpty(title))
                            title = par.Name;

                        object o = par.GetValue(_function, null);
                        string textValue = o != null ? o.ToString() : string.Empty;

                        string unit = (attr as FunctionEditableParameterAttribute).GetUnit(_function);
                        EditorType edtr = feAttr.Editor;
                        if (edtr == EditorType.Default)
                        {
                            if (unit == Constants.Units.second || unit == Constants.UnitsC.second)
                                edtr = EditorType.Time;
                        }
                        BaseParameterEditor editor = BaseParameterEditor.GetParameterEditor(edtr, par.PropertyType);

                        editor.Title = title;
                        editor.Unit = unit;
                        editor.Value = o;
                        editor.Property = par;

                        panParams.Controls.Add(editor);
                        editor.Left = 0;
                        editor.Top = h;
                        editor.Width = panParams.Width;
                        editor.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        h += editor.Height;
                        editor.ValueChanged += new EventHandler(editor_ValueChanged);
                    }
                }
            }
            panParams.Height = h;
        }

        void editor_ValueChanged(object sender, EventArgs e)
        {
            BaseParameterEditor pareditor = sender as BaseParameterEditor;
            PropertyInfo pi = pareditor.Property;
            if (pi == null) 
                return;
            if (pi.PropertyType == typeof(double))
                pi.SetValue(_function, (double)pareditor.Value, null);
            else
            {
                if (pi.PropertyType == typeof(decimal))
                    pi.SetValue(_function, (decimal)pareditor.Value, null);
                else
                    pi.SetValue(_function, (string)pareditor.Value, null);
            }

            _function.Update();
            DrawGraph();
        }
       
    }


}
