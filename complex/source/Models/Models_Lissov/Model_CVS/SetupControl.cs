using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using LissovBase;
using VisualControls.ConfigurationEditor;
using LissovLog;
using VisualControls;
using LissovBase.DynamicValue;

namespace Model_CVS
{
    public partial class SetupControl : CModelCVSControl
    {
        public SetupControl(CVSModel model)
        {
            InitializeComponent();
            _model = model;
            paramsTable.Init(_model);
            Update();
        }

        public override void Update()
        {
            if (_suppressConfigChanges) return;
            try
            {
                _suppressConfigChanges = true;
                tbConfigName.Text = _model.configuration.Name;
                tbConfigDepCount.Text = _model.configuration.CompartmentCount.ToString();
                tbConfigComments.Text = _model.configuration.Comments;
                paramsTable.Refresh();

                tbMaximumStep.Text = _model.GetMaxStepInfo().Text;

                tbBaroreceptionSensitivity.Text = _model.BaroreceptionSensitivity.Value.ToString();
            }
            finally
            {
                _suppressConfigChanges = false;
            }
            base.Update();
        }

        #region Configuration
        private void btnConfigLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _model.configuration = NetConfiguration.LoadFromFile(ofd.FileName, _model);
                    _model.UpdateVisuals();
                }
            }
        }

        private void btnConfigSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = _model.FullConfigFileDir;
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _model.NetConfigurationFileName.StringValue = Path.GetFileName(sfd.FileName);
                    _model.configuration.SaveToFile(_model.FullConfigFileName);
                }
            }
        }

        private void btnConfigLoadOldFormat_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _model.configuration.LoadFromOldFile(ofd.FileName, _model);
                    _model.UpdateVisuals();
                }
            }
        }

        private bool _suppressConfigChanges = false;
        private void ConfigChanged(object sender, EventArgs e)
        {
            if (_suppressConfigChanges) return;
            try
            {
                _suppressConfigChanges = true;
                _model.configuration.Name = tbConfigName.Text;
                _model.configuration.Comments = tbConfigComments.Text;
            }
            finally
            {
                _suppressConfigChanges = false;
            }
        }
        #endregion

        protected void showParameters()
        {
        }

        private void btnOldLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _model.LoadOldModelParameters(dlg.FileName);
                        MessageBox.Show("Parameters loaded", "Load complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Visual
        private int HORZ_SCALE = 500;
        private int VERT_SCALE = 500;
        private int BODY_HEIGHT = 2;

        private Configuration _visualConfig;
        public Configuration GetVisualConfig()
        {
            return GetVisualConfig(HORZ_SCALE, VERT_SCALE);
        }

        public Configuration GetVisualConfig(int horz_scale, int vert_scale)
        {
            Configuration res = new Configuration();
            for (int i = 0; i < _model.configuration.CompartmentCount; i++)
            {
                if (_model.configuration.CompartmentPositionX[i] == null)
                    _model.configuration.CompartmentPositionX[i] = 1;
                if (_model.configuration.CompartmentPositionY[i] == null)
                    _model.configuration.CompartmentPositionY[i] = _model.compartmentVertOffset[i];
                res.Vertices.Add(new Vertex()
                {
                    ID = i,
                    X = (int)(_model.configuration.CompartmentPositionX[i] * horz_scale),
                    Y = (int)((BODY_HEIGHT - _model.configuration.CompartmentPositionY[i]) * vert_scale),
                    Properties = new OneCompartmentData(_model, i),
                    RealHeight = (int)((BODY_HEIGHT - _model.compartmentVertOffset[i]) * vert_scale),
                    DisplayText = _model.configuration.CompartmentNames[i].Replace(' ', '\n').Replace("anostomosis", "anost.")
                });
            }
            int id = 0;
            for (int i = 0; i < _model.configuration.CompartmentCount; i++)
            {
                for (int j = 0; j < _model.configuration.Links[i].Count(); j++)
                {
                    int linkto = _model.configuration.Links[i][j];
                    res.Links.Add(new Link()
                    {
                        Begin = res.Vertices[i],
                        End = res.Vertices[linkto],
                        ID = id++,
                        Properties = new LinkProperties(
                            res.Vertices[i].Properties as OneCompartmentData,
                            res.Vertices[linkto].Properties as OneCompartmentData)
                    });
                }
            }

            #region Heart
            /*
            int heartr = res.Vertices.Count;
            int heartl = res.Vertices.Count + 1;
            res.Vertices.Add(new Vertex() { DisplayText = "Right\nVentricle", ID = heartr, X = 1, Y = 1 });
            res.Vertices.Add(new Vertex() { DisplayText = "Left\nVentricle", ID = heartr, X = 1, Y = 1 });
            res.Links.Add(new Link()
            {
                Begin =
                    res.Vertices[_model.configuration.LeftBeforeHeart],
                End = res.Vertices[heartl]
            });
            res.Links.Add(new Link()
            {
                Begin = res.Vertices[heartl],
                End = res.Vertices[_model.configuration.LeftAfterHeart]
            });
            res.Links.Add(new Link()
            {
                Begin =
                    res.Vertices[_model.configuration.RightBeforeHeart],
                End = res.Vertices[heartr]
            });
            res.Links.Add(new Link()
            {
                Begin = res.Vertices[heartr],
                End = res.Vertices[_model.configuration.RightAfterHeart]
            });
            */
            #endregion

            return res;
        }

        private ConfigurationEditorController _visualController;
        private void btnVisualEditor_Click(object sender, EventArgs e)
        {
            _visualConfig = GetVisualConfig();
            _visualController = new ConfigurationEditorController(_visualConfig);
            _visualController.VertexMoving += CompartmentMoving;
            _visualController.ApplyPerformed += ApplyGraph;
            _visualController.VertexPropertyChanged += CompartmentPropertyChanged;
            _visualController.ShowForm();
        }

        private void ApplyGraph(object sender, EventArgs e)
        {
            for (int i = 0; i < _model.configuration.CompartmentCount; i++)
            {
                _model.configuration.CompartmentPositionX[i] = (double)_visualConfig.Vertices[i].X / HORZ_SCALE;
                _model.configuration.CompartmentPositionY[i] = BODY_HEIGHT - ((double)_visualConfig.Vertices[i].Y / VERT_SCALE);
                OneCompartmentData ocd = (_visualConfig.Vertices[i].Properties as OneCompartmentData);
                if (ocd != null)
                    ocd.applyToModel(_model, i);
            }
        }
        private void CompartmentMoving(Vertex v)
        {
            if (v == null) return;
            OneCompartmentData ocd = v.Properties as OneCompartmentData;
            if (ocd == null) return;
            if (ConfigurationEditorController.CtrlPressed())
                ocd.Height = BODY_HEIGHT - ((double)v.RealHeight.Value / VERT_SCALE);
            _visualController.UpdatePropertiesGrid();
        }
        private void CompartmentPropertyChanged(Vertex v)
        {
            if (v == null) return;
            OneCompartmentData ocd = v.Properties as OneCompartmentData;
            if (ocd == null) return;
            v.RealHeight = (int)((BODY_HEIGHT - ocd.Height) * VERT_SCALE);
            _visualController.Refresh();
        }

        public class LinkProperties
        {
            OneCompartmentData _ocdBegin;
            OneCompartmentData _ocdEnd;
            internal LinkProperties(OneCompartmentData ocdBegin, OneCompartmentData ocdEnd)
            {
                _ocdBegin = ocdBegin;
                _ocdEnd = ocdEnd;
            }

            public string FromCompartment { get { return _ocdBegin.CompartmentName; } }
            public string ToCompartment { get { return _ocdEnd.CompartmentName; } }

            public double ResistanceOutFrom
            {
                get { return _ocdBegin.ResistanceOutput; }
                set { _ocdBegin.ResistanceOutput = value; }
            }
            public double ResistanceInputTo
            {
                get { return _ocdEnd.ResistanceInputKoeff; }
                set { _ocdEnd.ResistanceInputKoeff = value; }
            }
            public double Resistance
            {
                get { return ResistanceOutFrom * ResistanceInputTo; }
            }

            public double FromPressure
            {
                get
                {
                    try
                    {
                        return _ocdBegin.Rigidity * _ocdBegin.PV_Function.getValue(_ocdBegin.Volume - _ocdBegin.Unstressed);
                    }
                    catch (Exception) { return Constants.INVALID_DOUBLE; }
                }
            }
            public double ToPressure
            {
                get
                {
                    try
                    {
                        return _ocdEnd.Rigidity * _ocdEnd.PV_Function.getValue(_ocdEnd.Volume - _ocdEnd.Unstressed);
                    }
                    catch (Exception) { return Constants.INVALID_DOUBLE; }
                }
            }

            public double Flow
            {
                get
                {
                    try
                    {
                        return (FromPressure - ToPressure) / Resistance;
                    }
                    catch (Exception) { return Constants.INVALID_DOUBLE; }
                }
            }
        }
	    #endregion    

        private void btnMergeCompartments_Click(object sender, EventArgs e)
        {
            PerformMergeCompartments();
        }

        public void PerformMergeCompartments()
        {
            MergeCompartmentsParametersForm form = new MergeCompartmentsParametersForm();
            MergeParameters pars = form.Execute(_model);
            if (pars.PerformMerge)
                PerformMergeCompartments(pars);
        }

        public void PerformMergeCompartments(MergeParameters pars)
        {
            Log.Write(LogLevel.INFO, "Merging {0} to {1} ({0} is removed)",
                _model.configuration.CompartmentNames[pars.Merger],
                _model.configuration.CompartmentNames[pars.Destination]);

            List<int> toremove = new List<int>();
            toremove.Add(pars.Merger);

            Log.Write(LogLevel.INFO, "Updating parameters");
            _model.compartmentVertOffset[pars.Destination] = pars.Height;
            _model.koeff_elast[pars.Destination] = pars.ElasticKoeff;
            _model.muscularTone[pars.Destination] = pars.MuscularTone;
            _model.rigidity_zero[pars.Destination] = pars.Rigidity;
            //gain_rigidity_adrenalin
            //gain_unstressed_pext
            //gain_unstressed_adrenalin
            //ideal_flow
            _model.unstressed_zero[pars.Destination] = pars.Unstressed;
            _model.volume_zero[pars.Destination] = pars.Volume;

            #region Resistances
            Log.Write(LogLevel.INFO, "Recalculate resistances");
            if (_model.configuration.Links[pars.Destination].Contains(pars.Merger))
            {
                Log.Write(LogLevel.INFO, "Moving output flows from merger to destination");
                foreach (int i in _model.configuration.Links[pars.Merger])
                {
                    double flow = (_model.rigidity_zero[pars.Merger] - _model.rigidity_zero[i])
                        / (_model.resistanceInput[i] * _model.resistanceOutput_zero[pars.Merger]);
                    _model.resistanceInput[i] = (_model.rigidity_zero[pars.Destination] - _model.rigidity_zero[i])
                        / (flow * _model.resistanceOutput_zero[pars.Destination]);
                }
            }
            else if (_model.configuration.Links[pars.Merger].Contains(pars.Destination))
            {
                Log.Write(LogLevel.INFO, "Moving input flows from merger to destination");
                for (int j = 0; j < _model.configuration.CompartmentCount; j++)
                {
                    if (_model.configuration.Links[j].Contains(pars.Merger))
                    {
                        double flow = (_model.rigidity_zero[pars.Merger] - _model.rigidity_zero[j])
                            / (_model.resistanceInput[pars.Merger] * _model.resistanceOutput_zero[j]);
                        _model.resistanceOutput_zero[j] = (_model.rigidity_zero[pars.Destination] - _model.rigidity_zero[j])
                            / (flow * _model.resistanceInput[pars.Destination]);
                    }
                }
            }
            else
                Log.Write(LogLevel.WARN, "Resistance recalculation method not found for selected mergers");
            #endregion

            Log.Write(LogLevel.INFO, "Updating PV function");
            _model.pv_functions[pars.Destination].linearV = pars.Volume - pars.Unstressed;
            _model.pv_functions[pars.Destination].Update();

            _model.configuration.mergeCompartment(pars.Destination, pars.Merger);

            //getting input values
            var values = _model.GetValues();

            Log.Write(LogLevel.INFO, "Updating arrays lengths");
            _model.UpdateArraysLengthsToConfiguration(_model.configuration, toremove);

            Log.Write(LogLevel.INFO, "Resetting inputs");
            foreach (var item in _model.GetValues())
            {
                ModelBase.Value input = values.First(v => v.Name == item.Name);
                if (input != null)
                    item.CopyFrom(input, false);
            }

            Log.Write(LogLevel.INFO, "Merge complete");
        }

        private void btnAddVirtCompartment_Click(object sender, EventArgs e)
        {VirtualCompartment comp = VirtualCompartmentForm.Execute(_model);
            if (comp == null)
                return;
            _model.configuration.virtualCompartments = ModelHelper.AddToArray<VirtualCompartment>(ref _model.configuration.virtualCompartments, comp);
            _model.InvalidateParamsValues();
        }

        private void btnEditBrainFlowRegulationFunction_Click(object sender, EventArgs e)
        {
            FunctionEditForm.ShowEditForm(_model.BrainFlowRegulationFunction.InnerFunction, true);
        }

        private void tbBaroreceptionSensitivity_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            double d;
            if (double.TryParse(tbBaroreceptionSensitivity.Text, out d))
            {
                _model.BaroreceptionSensitivity.Value = d;
            }
            else
                errorProvider1.SetError(tbBaroreceptionSensitivity, "Incorrect double value");
        }

        private void fillCompartments(object sender, EventArgs e)
        {
            if (!(sender is ComboBox))
                return;
            (sender as ComboBox).Items.Clear();
            (sender as ComboBox).Items.AddRange(_model.configuration.CompartmentNames);
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            if (cbResFrom.SelectedIndex < 0 
                || cbResTo.SelectedIndex < 0
                || cbThrough.SelectedIndex < 0)
                return;

            _model.AddDynamicValue(new DynamicValueResistance(_model,
                cbResFrom.SelectedIndex, cbResTo.SelectedIndex, cbThrough.SelectedIndex));
        }
    }

    public class CModelCVSControl : CModelSetupControl<CVSModel>
    {
    }
}