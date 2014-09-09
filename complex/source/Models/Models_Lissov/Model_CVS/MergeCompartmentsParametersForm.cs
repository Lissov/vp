using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Model_CVS
{
    public partial class MergeCompartmentsParametersForm : Form
    {
        public MergeCompartmentsParametersForm()
        {
            InitializeComponent();
        }

        CVSModel _model;

        public MergeParameters Execute(CVSModel model)
        {
            _model = model;
            _model.configuration.FillCombo(cbDestination);
            _model.configuration.FillCombo(cbMerger);

            MergeParameters res = new MergeParameters();
            res.PerformMerge = (this.ShowDialog() == DialogResult.Yes);

            try
            {
                if (res.PerformMerge)
                {
                    res.Destination = cbDestination.SelectedIndex;
                    res.Merger = cbMerger.SelectedIndex;
                    res.Volume = _volume.NewValue;
                    res.Unstressed = _unstressed.NewValue;
                    res.ElasticKoeff = _elastic.NewValue;
                    res.MuscularTone = _muscular.NewValue;
                    res.Rigidity = _rigidity.NewValue;
                    res.Height = _height.NewValue;
                }
            }
            catch (Exception ex)
            {
                res.PerformMerge = false;
                MessageBox.Show("Error: " + ex.Message);
            }

            return res;
        }

        private CompData _volume = new CompData() { ParameterName = "Volume" };
        private CompData _unstressed = new CompData() { ParameterName = "Unstressed volume" };
        private CompData _elastic = new CompData() { ParameterName = "Elastic Koeff" };
        private CompData _muscular = new CompData() { ParameterName = "Muscular tone" };
        private CompData _rigidity = new CompData() { ParameterName = "Rigidity (Pressure)" };
        private CompData _height = new CompData() { ParameterName = "Height" };
        private List<CompData> data = null;
        private void CompartmentSelected(object sender, EventArgs e)
        {
            int dest = cbDestination.SelectedIndex;
            int merger = cbMerger.SelectedIndex;
            if (dest == -1 || merger == -1)
            {
                _volume.NewValue = 0;
                _unstressed.NewValue = 0;                
                _elastic.NewValue = 0;
                _muscular.NewValue = 0;
                _rigidity.NewValue = 0;
                _height.NewValue = 0;
            }
            else
            {
                _volume.ValueDestination = _model.volume_zero[dest];
                _volume.ValueMerger = _model.volume_zero[merger];
                _volume.NewValue = _volume.ValueDestination + _volume.ValueMerger;

                _unstressed.ValueDestination = _model.unstressed_zero[dest];
                _unstressed.ValueMerger = _model.unstressed_zero[merger];
                _unstressed.NewValue = _unstressed.ValueDestination + _unstressed.ValueMerger;

                _elastic.ValueDestination = _model.koeff_elast[dest];
                _elastic.ValueMerger = _model.koeff_elast[merger];
                _elastic.NewValue = _model.koeff_elast[dest];

                _muscular.ValueDestination = _model.muscularTone[dest];
                _muscular.ValueMerger = _model.muscularTone[merger];
                _muscular.NewValue = _model.muscularTone[dest];
                
                _rigidity.ValueDestination = _model.rigidity_zero[dest];
                _rigidity.ValueMerger = _model.rigidity_zero[merger];
                _rigidity.NewValue = _model.rigidity_zero[dest];

                _height.ValueDestination = _model.compartmentVertOffset[dest];
                _height.ValueMerger = _model.compartmentVertOffset[merger];
                _height.NewValue = _model.compartmentVertOffset[dest];
            }

            if (data == null)
            {
                data = new List<CompData>(new CompData[] { 
                    _volume, _unstressed, _elastic, _muscular, _rigidity, _height });
                gridData.DataSource = data;
            }
            gridData.RefreshDataSource();
        }
    }

    public class MergeParameters
    {
        public bool PerformMerge { get; set; }
        public int Destination { get; set; }
        public int Merger { get; set; }
        public double Volume { get; set; }
        public double Unstressed { get; set; }
        public double ElasticKoeff { get; set; }
        public double MuscularTone { get; set; }
        public double Rigidity { get; set; }
        public double Height { get; set; }
    }

    internal class CompData
    {
        public string ParameterName { get; set; }
        public double ValueMerger { get; set; }
        public double ValueDestination { get; set; }
        public double NewValue { get; set; }
    }
}