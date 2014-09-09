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

namespace Model_Fluids
{
    public partial class ParamsGridControl : UserControl
    {
        public ParamsGridControl()
        {
            InitializeComponent();
        }

        FluidsModel _model;

        public void Init(FluidsModel model)
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
            
            FunctionEditForm.ShowEditForm(data.PV_FunctionI, false);
        }


        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView1.SelectedRowsCount != 1) throw new Exception("Editing unselected row.");
            OneCompartmentData data = gridView1.GetRow(gridView1.GetSelectedRows()[0]) as OneCompartmentData;

            FunctionEditForm.ShowEditForm(data.PV_FunctionC, false);
        }

        private void btnChangeAll_Click(object sender, EventArgs e)
        {
            GridColumnValuesChangeForm form = new GridColumnValuesChangeForm(grid);
            if (form.ShowDialog() == DialogResult.OK)
                ApplyData();
        }
    }

    internal class OneCompartmentData
    {
        public string CompartmentName { get; set; }
        public double VolumeI { get; set; }
        public double UnstressedI { get; set; }
        public double RigidityI { get; set; }
        public double VolumeC { get; set; }
        public double UnstressedC { get; set; }
        public double RigidityC { get; set; }
        public double ResistanceVI { get; set; }
        public double ResistanceIC { get; set; }
        public double ResistanceIL { get; set; }
        public double Height { get; set; }
        public double KElastI { get; set; }
        public double KElastC { get; set; }
        public double CFRPressure0 { get; set; }
        public double CFRPressureGain { get; set; }
        
        public PVFunction PV_FunctionI { get; set; }
        public PVFunction PV_FunctionC { get; set; }

        public OneCompartmentData(FluidsModel model, int cnum)
        {
            try
            {
                StringBuilder dep = new StringBuilder();
                CompartmentName = model.configuration.CompartmentNames[cnum];
                VolumeI = model.VolumeInter[cnum].InitValue;
                VolumeC = model.VolumeCell[cnum].InitValue;
                UnstressedI = model.UnstressedInter[cnum];
                UnstressedC = model.UnstressedCell[cnum];
                RigidityI = model.RigidityInterst[cnum];
                RigidityC = model.RigidityCell[cnum];
                ResistanceVI = model.ResistanceInterstitialVascular[cnum];
                ResistanceIC = model.ResistanceInterstitialCellular[cnum];
                ResistanceIL = model.ResistanceInterstitialLympha[cnum];
                Height = model.Height[cnum];
                KElastI = model.ElasticKoeffInterstitial[cnum];
                KElastC = model.ElasticKoeffCellular[cnum];
                CFRPressure0 = model.CFR_Pressure0[cnum];
                CFRPressureGain = model.CFR_PressureGain[cnum];

                PV_FunctionI = model.PV_interstitial[cnum];
                PV_FunctionC = model.PV_celular[cnum];
            }
            catch (Exception ex)
            {
                CompartmentName = "Exception: " + ex.Message;
            }
        }

        public void applyToModel(FluidsModel model, int cnum)
        {
            model.VolumeInter[cnum].InitValue = VolumeI;
            model.UnstressedInter[cnum] = UnstressedI;
            model.RigidityInterst[cnum] = RigidityI;
            model.VolumeCell[cnum].InitValue = VolumeC;
            model.UnstressedCell[cnum] = UnstressedC;
            model.RigidityCell[cnum] = RigidityC;
            
            model.ResistanceInterstitialVascular[cnum] = ResistanceVI;
            model.ResistanceInterstitialCellular[cnum] = ResistanceIC;
            model.ResistanceInterstitialLympha[cnum] = ResistanceIL;

            model.Height[cnum] = Height;
            model.ElasticKoeffInterstitial[cnum] = KElastI;
            model.ElasticKoeffCellular[cnum] = KElastC;
            model.CFR_Pressure0[cnum] = CFRPressure0;
            model.CFR_PressureGain[cnum] = CFRPressureGain;
        }

        public string PVFunctionTextI
        {
            get { return PV_FunctionI != null ? PV_FunctionI.level1 + " -- " + PV_FunctionI.level2 : string.Empty; }
            set { /*Ignore*/ }
        }
        public string PVFunctionTextC
        {
            get { return PV_FunctionC != null ? PV_FunctionC.level1 + " -- " + PV_FunctionC.level2 : string.Empty; }
            set { /*Ignore*/ }
        }
    }
}