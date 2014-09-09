using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualControls;
using LissovBase.Functions;

namespace Model_CVS
{
    public partial class ParamsGridControl : UserControl
    {
        public ParamsGridControl()
        {
            InitializeComponent();
        }

        CVSModel _model;

        public void Init(CVSModel model)
        {
            _model = model;
            ShowData();
        }

        public override void Refresh()
        {
            ShowData();
        }

        List<OneCompartmentData> data;
        private void ShowData()
        {
            data = new List<OneCompartmentData>();
            for (int i = 0; i < _model.configuration.CompartmentCount; i++)
            {
                OneCompartmentData cd = new OneCompartmentData(_model, i);
                data.Add(cd);
            }
            oneCompartmentDataBindingSource.DataSource = data;
        }

        public void ApplyData()
        {
            for (int i = 0; i < _model.configuration.CompartmentCount; i++)
                data[i].applyToModel(_model, i);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyData();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView1.SelectedRowsCount != 1) throw new Exception("Editing unselected row.");
            OneCompartmentData data = gridView1.GetRow(gridView1.GetSelectedRows()[0]) as OneCompartmentData;

            FunctionEditForm.ShowEditForm(data.PV_Function, false);
        }

        private void btnChangeAll_Click(object sender, EventArgs e)
        {
            GridColumnValuesChangeForm form = new GridColumnValuesChangeForm(grid);
            if (form.ShowDialog() == DialogResult.OK)
                ApplyData();
        }
    }

    public class OneCompartmentData
    {
        public string DepartmentName { get; set; }
        public string CompartmentName { get; set; }
        public double Volume { get; set; }
        public double Unstressed { get; set; }
        public double Rigidity { get; set; }
        public double ResistanceInputKoeff { get; set; }
        public double ResistanceOutput { get; set; }
        public double Height { get; set; }
        public double Rig_Adr { get; set; }
        public double Unstressed_Adr { get; set; }
        public double Unstressed_PExt { get; set; }
        public double KElast { get; set; }
        public double MuscularTone { get; set; }
        public PVFunction PV_Function { get; set; }
        public string FlowBalance
        {
            get
            {
                double fli = 0;
                for (int i = 0; i < _model.configuration.CompartmentCount; i++)
                {
                    if (_model.configuration.Links[i].Contains(compartmentID))
                    {
                        fli += _model.GetInitialFlow(i, compartmentID);
                    }
                }

                double flo = 0;
                foreach (int item in _model.configuration.Links[compartmentID])
                {
                    flo += _model.GetInitialFlow(compartmentID, item);
                }

                string format = "G6";
                return fli.ToString(format) + " => " + flo.ToString(format);
            }
        }

        CVSModel _model;
        int compartmentID;
        public OneCompartmentData(CVSModel model, int cnum)
        {
            try
            {
                _model = model;
                compartmentID = cnum;

                StringBuilder dep = new StringBuilder();
                for (int i = 0; i < model.configuration.DepartmentCount; i++)
                {
                    if (model.configuration.Departments[i].Contains(cnum))
                    {
                        if (dep.ToString() != string.Empty) dep.Append("; ");
                        dep.Append(model.configuration.DepartmentNames[i]);
                    }
                }
                DepartmentName = dep.ToString();
                CompartmentName = model.configuration.CompartmentNames[cnum];
                Volume = model.volume_zero[cnum];
                Unstressed = model.unstressed_zero[cnum];
                Rigidity = model.rigidity_zero[cnum];
                ResistanceInputKoeff = model.resistanceInput[cnum];
                ResistanceOutput = model.resistanceOutput_zero[cnum];
                Height = model.compartmentVertOffset[cnum];
                KElast = model.koeff_elast[cnum];
                MuscularTone = model.muscularTone[cnum];
                Rig_Adr = model.gain_rigidity_adrenalin[cnum];
                Unstressed_Adr = model.gain_unstressed_adrenalin[cnum];
                Unstressed_PExt = model.gain_unstressed_pext[cnum];

                PV_Function = model.pv_functions[cnum];
            }
            catch (Exception ex)
            {
                CompartmentName = "Exception: " + ex.Message;
            }
        }

        public void applyToModel(CVSModel model, int cnum)
        {
            model.volume_zero[cnum] = Volume;
            model.unstressed_zero[cnum] = Unstressed;
            model.rigidity_zero[cnum] = Rigidity;
            model.resistanceInput[cnum] = ResistanceInputKoeff;
            model.resistanceOutput_zero[cnum] = ResistanceOutput;
            model.compartmentVertOffset[cnum] = Height;
            model.koeff_elast[cnum] = KElast;
            model.muscularTone[cnum] = MuscularTone;
            model.gain_rigidity_adrenalin[cnum] = Rig_Adr;
            model.gain_unstressed_adrenalin[cnum] = Unstressed_Adr;
            model.gain_unstressed_pext[cnum] = Unstressed_PExt;
        }

        public string PVFunctionText
        {
            get { return PV_Function != null ? PV_Function.level1 + " -- " + PV_Function.level2 : string.Empty; }
            set { /*Ignore*/ }
        }
    }
}
