using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;

namespace Model_Baroreception
{
    public partial class ReceptorZoneControl : UserControl
    {
        public ReceptorZoneControl()
        {
            InitializeComponent();
        }
        private int _zoneN;
        private BaroreceptionModel _model;

        public void Init(int zoneN, BaroreceptionModel model)
        {
            _zoneN = zoneN;
            _model = model;

            ShowData();
        }

        public void ShowData()
        {
            cbUseZone.Checked = _model.useZone[_zoneN] == LissovModelBase.TRUE;
            lZone.Text = _model.zoneID[_zoneN];
            funcPanel.Init(_model.baroCurve[_zoneN]);
            tbTreshold.Text = _model.pressureThresholdZone[_zoneN].ToString();
            tbElastic.Text = _model.elasticKoefficient[_zoneN].ToString();
            tbWeight.Text = _model.baroreceptionZoneWeight[_zoneN].ToString();
            tbDelay.Text = _model.delayTime[_zoneN].ToString();
        }

        private void ReadData()
        {
            _model.useZone[_zoneN] = cbUseZone.Checked ?
                LissovModelBase.TRUE : LissovModelBase.FALSE;

            errorProvider1.Clear();
            List<Control> incorrect = new List<Control>();
            double treshold, elastic, weight, delay;
            if (!double.TryParse(tbTreshold.Text, out treshold))
                incorrect.Add(tbTreshold);
            if (!double.TryParse(tbElastic.Text, out elastic))
                incorrect.Add(tbElastic);
            if (!double.TryParse(tbWeight.Text, out weight))
                incorrect.Add(tbWeight);
            if (!double.TryParse(tbDelay.Text, out delay))
                incorrect.Add(tbDelay);
            foreach (var c in incorrect)
                errorProvider1.SetError(c, "Incorrect value");

            if (incorrect.Count == 0)
            {
                _model.pressureThresholdZone[_zoneN] = treshold;
                _model.elasticKoefficient[_zoneN] = elastic;
                _model.baroreceptionZoneWeight[_zoneN] = weight;
                _model.delayTime[_zoneN] = delay;
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            ReadData();
        }
    }
}
