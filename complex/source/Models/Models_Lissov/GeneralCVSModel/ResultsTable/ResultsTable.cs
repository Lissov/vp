using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelBase;
using DevExpress.XtraGrid.Columns;

namespace GeneralCVSModel.ResultsTable
{
    public partial class ResultsTable : UserControl
    {
        public ResultsTable()
        {
            InitializeComponent();
        }

        Configuration _config;
        public void Init(Configuration config)
        {
            _config = config;
        }

        public void Readd(string times, string outputs)
        {
            if (!string.IsNullOrEmpty(times) && !string.IsNullOrEmpty(outputs))
                ParseOutputs(times, outputs);
        }

        public void FillParameters(GeneralCVSModel.GeneralModel model)
        {
            model.ResultsTable_Times.StringValue = string.Join(";",
                (timePointBindingSource.List as IEnumerable<TimePoint>).Select(tp => tp.Time.ToString()).ToArray());
            List<string> outs = new List<string>();
            foreach (var col in gridView1.Columns)
            {
                GridColumn c = col as GridColumn;
                if (c == null || !c.Visible || !(c.Tag is OutputInfo))
                    continue;

                OutputInfo oi = (OutputInfo)c.Tag;
                outs.Add(oi.Model.GetName() + "." + oi.OutputName);
            }
            model.ResultsTable_Outputs.StringValue = string.Join(";",
                outs.ToArray());
        }

        private void ParseOutputs(string times, string outputs)
        {
            timePointBindingSource.Clear();
            while (gridView1.Columns.Count > 1)
                gridView1.Columns.RemoveAt(1);

            string[] time = times.Split(';');
            var tms = new List<decimal>(
                from t in time select decimal.Parse(t));
            tms.Sort();
            foreach (decimal t in tms)
            {
                timePointBindingSource.Add(new TimePoint() { Time = t });
            }

            string[] outs = outputs.Split(';');
            foreach (var outi in outs)
            {
                string[] parts = outi.Split('.');
                OutputInfo oi = new OutputInfo()
                {
                    Model = _config.GetModelByName(parts[0]),
                    OutputName = parts[1]
                };
                AddColumn(oi);
            }
        }

        private void btnAddOutput_Click(object sender, EventArgs e)
        {
            OutputInfo oi = AddOutputForm.Execute(_config);
            if (oi != null)
            {
                AddColumn(oi);
            }
        }

        private void AddColumn(OutputInfo oi)
        {
            GridColumn col = new GridColumn();
            col.OptionsColumn.AllowEdit = false;
            col.Caption = oi.Model.GetName() + " - " + oi.OutputName;
            col.Tag = oi;
            col.Visible = true;
            gridView1.Columns.Add(col);
            col.VisibleIndex = 1;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colTime)
            {
                TimePoint tp = null;
                try
                {
                    if (e.RowHandle >= 0)
                        tp = timePointBindingSource[e.RowHandle] as TimePoint;
                }
                catch (Exception) { }
                OutputInfo oi = e.Column.Tag as OutputInfo;
                if (tp == null || oi == null)
                    e.DisplayText = "---";
                else
                {
                    try
                    {
                        double d = oi.Model.GetValueByNameAndTime(oi.OutputName, tp.Time);
                        e.DisplayText = d.ToString();
                    }
                    catch (Exception)
                    {
                        e.DisplayText = "---";
                    }
                }
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            var times = (timePointBindingSource.List as IEnumerable<TimePoint>).Select(tp => tp.Time);

            StringBuilder data = new StringBuilder("!!!");
            foreach (var tm in times)
            {
                data.Append("\t");
                data.Append(tm.ToString() + "s");
            }
            data.Append("\r\n");

            foreach (var col in gridView1.Columns)
            {
                GridColumn c = col as GridColumn;
                if (c == null || !c.Visible || !(c.Tag is OutputInfo))
                    continue;
                OutputInfo oi = (OutputInfo)c.Tag;

                data.Append(oi.Model.GetName() + " " + oi.OutputName);
                foreach (var tm in times)
                {
                    data.Append("\t");
                    if (oi.Model.GetLastCalculatedTime() >= tm)
                        data.Append(oi.Model.GetValueByNameAndTime(oi.OutputName, tm).ToString());
                    else
                        data.Append("---");
                }
                data.Append("\r\n");
            }

            Clipboard.SetText(data.ToString());
        }
    }

    public class TimePoint
    {
        public decimal Time { get; set; }
    }
}
