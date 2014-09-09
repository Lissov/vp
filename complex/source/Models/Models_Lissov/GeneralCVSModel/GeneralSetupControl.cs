using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_Baroreception;
using ModelBase;
using Model_CVS;
using Model_Kidney;
using Model_Load;
using LissovLog;
using LissovBase;
using Model_Fluids;
using VisualControls;
using System.Threading;
using GeneralCVSModel.ConfigurationEditor;
using System.IO;

namespace GeneralCVSModel
{
    public partial class GeneralSetupControl : UserControl
    {
        private const double ONE_THIRD = 0.3333333333333;

        private GeneralModel _model;
        public GeneralSetupControl(GeneralModel model)
        {
            InitializeComponent();
            _model = model;
            Init();
            resultsTable.Init(model.Configuration);
            resultsTable.Readd(model.ResultsTable_Times.StringValue, model.ResultsTable_Outputs.StringValue);
            ShowData();
        }

        private bool _internalChanging = false;

        ConfigurationEditorControl confEdit;
        
        private void Init()
        {
            CVSSubModels.InitModels(_model);

            confEdit = new ConfigurationEditorControl(_model);
            tpConfiguration.Controls.Add(confEdit);
            confEdit.Dock = DockStyle.Fill;

            ThreadPool.QueueUserWorkItem(new WaitCallback(RefreshData));
        }        

        //IModel
        public void ShowData()
        {
            _internalChanging = true;
            try
            {
                if (CVSSubModels.BaroreceptionModel != null)
                {
                    cbBaroreception.Enabled = true;
                    cbWillisBaroreception.Enabled = true;
                    cbBaroreception.Checked = (CVSSubModels.BaroreceptionModel.BaroreceptionCopyFirst.Value == LissovModelBase.FALSE);
                    if (CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[0] == ONE_THIRD
                        && CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[1] == ONE_THIRD
                        && CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[2] == ONE_THIRD)
                        cbWillisBaroreception.CheckState = CheckState.Checked;
                    else if (
                        CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[0] == 0.5
                        && CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[1] == 0.5
                        && CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[2] == 0)
                        cbWillisBaroreception.CheckState = CheckState.Unchecked;
                    else
                        cbWillisBaroreception.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    cbBaroreception.Enabled = false;
                    cbWillisBaroreception.Enabled = true;
                }

                if (CVSSubModels.KidneyModel != null)
                {
                    cbKidney.Enabled = true;
                    cbKidneyRegulation.Enabled = true;
                    cbKidney.Checked = CVSSubModels.KidneyModel.UseKidney.Value == LissovModelBase.TRUE;
                    cbKidneyRegulation.Checked = CVSSubModels.KidneyModel.CopyFirstStepValue.Value != LissovModelBase.TRUE;
                }
                else
                {
                    cbKidney.Enabled = false;
                    cbKidneyRegulation.Enabled = false;
                }

                if (CVSSubModels.FluidsModel != null)
                {
                    cbCapillaryFiltration.Enabled = true;
                    cbCapillaryFiltration.Checked = CVSSubModels.FluidsModel.UseFilteringRegulation.Value == LissovModelBase.TRUE;
                }
                else
                    cbCapillaryFiltration.Enabled = false;

                confEdit.Update();
            }
            finally
            {
                _internalChanging = false;
            }
        }

        private void RefreshData(object state)
        {
            while (true)
            {
                try
                {
                    this.Invoke(new Action(ShowData));
                    Thread.Sleep(500);
                }
                catch (Exception) { }
            }
        }

        private void cbBaroreception_CheckedChanged(object sender, EventArgs e)
        {
            CVSSubModels.BaroreceptionModel.BaroreceptionCopyFirst.Value = cbBaroreception.Checked ? LissovModelBase.FALSE : LissovModelBase.TRUE;
            CVSSubModels.BaroreceptionModel.SetupControl.Update();
        }

        private void cbKidney_CheckedChanged(object sender, EventArgs e)
        {
            CVSSubModels.KidneyModel.UseKidney.Value = cbKidney.Checked ?
                LissovModelBase.TRUE : LissovModelBase.FALSE;
        }

        private void cKidneyRegulation_CheckedChanged(object sender, EventArgs e)
        {
            CVSSubModels.KidneyModel.CopyFirstStepValue.Value = cbKidneyRegulation.Checked ?
                LissovModelBase.FALSE : LissovModelBase.TRUE;
        }

        private void cbCapillaryFiltration_CheckedChanged(object sender, EventArgs e)
        {
            CVSSubModels.FluidsModel.UseFilteringRegulation.Value = cbCapillaryFiltration.Checked ?
                LissovModelBase.TRUE : LissovModelBase.FALSE;
        }

        private void cbWillisBaroreception_CheckStateChanged(object sender, EventArgs e)
        {
            if (_internalChanging) return;

            if (sender.Equals(cbWillisBaroreception) && cbWillisBaroreception.CheckState == CheckState.Indeterminate)
                cbWillisBaroreception.CheckState = CheckState.Unchecked;

            switch (cbWillisBaroreception.CheckState)
            {
                case CheckState.Checked:
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[0] = ONE_THIRD;
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[1] = ONE_THIRD;
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[2] = ONE_THIRD;
                    break;
                case CheckState.Unchecked:
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[0] = 0.5;
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[1] = 0.5;
                    CVSSubModels.BaroreceptionModel.baroreceptionZoneWeight[2] = 0;
                    break;
                case CheckState.Indeterminate: 
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _model.Validate();
            
            //Hash codes
            foreach (var item in _model.Configuration.Models)
            {
                if (item is LissovModelBase)
                    Log.Write(LogLevel.INFO, "Full hash code of [{0}] is [{1}]", item.GetName(), (item as LissovModelBase).GetModelHash());
            }
        }

        private void btnSaveGraphs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { DefaultExt = "rsf", Filter = "Results file (*.rsf)|*.rsf|All files (*.*)|*.*"})
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                FileStream sw = new FileStream(sfd.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(sw);
                
                List<valDt> values = new List<valDt>();
                foreach (var mod in _model.Configuration.Models)
                {
                    foreach (var val in mod.GetValues())
                        if (val.Visible)
                            values.Add(new valDt(){ mdl = mod, vl = val});
                }
                bw.Write(values.Count);
                foreach (var val in values)
                {
                    bw.Write(val.mdl.Step);
                    bw.Write(val.vl.Name);
                    bw.Write(val.vl.Measure);

                    int cnt = val.vl.Value.Length;
                    bw.Write(cnt);
                    for (int i = 0; i < cnt; i++)
                        bw.Write(val.vl.Value[i]);
                }

                sw.Close();
            }
        }

        class valDt
        {
            public IModel mdl;
            public Value vl;
        }
    }
}
