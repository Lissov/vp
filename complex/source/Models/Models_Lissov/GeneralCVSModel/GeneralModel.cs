using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using System.Xml;
using LissovLog;
using LissovBase.Functions;

namespace GeneralCVSModel
{
    public class GeneralModel : LissovModelBase
    {
        public ParameterString ResultsTable_Times = new ParameterString("ResultsTableTimes", "ResultsTable Times");
        public ParameterString ResultsTable_Outputs = new ParameterString("ResultsTableOutputs", "ResultsTable Outputs");

        private int _model_count;
        internal int Model_Count { 
            get {return _model_count;}
            set
            {
                if (_model_count != value)
                {
                    _modelPositionX = new int[value];
                    _modelPositionY = new int[value];
                }
                _model_count = value;
            }
        }
        internal int[] _modelPositionX;
        internal int[] _modelPositionY;

        public GeneralModel()
        {
            this.DisplayName = "General CVS Model";
            this.Name = "GeneralCVS";

            Model_Count = 0;
        }

        GeneralSetupControl _control = null;
        public override System.Windows.Forms.UserControl GetControl()
        {
            if (_control == null)
            {
                _control = new GeneralSetupControl(this);
            }
            return _control;             
        }

        public override bool IsControlAlwaysShown() { return true; }

        public override System.Xml.XmlElement ToXml(System.Xml.XmlElement parentElement)
        {
            if (_control != null)
                _control.resultsTable.FillParameters(this);

            XmlElement xel = base.ToXml(parentElement);
            SetAttribute(xel, "ModelCount", _model_count);
            for (int i = 0; i < _model_count; i++)
            {
                SetAttribute(xel, string.Format("ModelPosition_{0}_X", i), _modelPositionX[i]);
                SetAttribute(xel, string.Format("ModelPosition_{0}_Y", i), _modelPositionY[i]);
            }
            return xel;
        }

        public override object FromXml(XmlElement currentElement)
        {
            try
            {
                Model_Count =  int.Parse(currentElement.GetAttribute("ModelCount"));
                for (int i = 0; i < _model_count; i++)
                {
                    _modelPositionX[i] = int.Parse(currentElement.GetAttribute(string.Format("ModelPosition_{0}_X", i)));
                    _modelPositionY[i] = int.Parse(currentElement.GetAttribute(string.Format("ModelPosition_{0}_Y", i)));
                }
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Exception [G001]: " + ex.Message);
            }
            var res = base.FromXml(currentElement);

            if (_control != null)
                _control.resultsTable.Readd(ResultsTable_Times.StringValue, ResultsTable_Outputs.StringValue);

            return res;
        }

        public override void BeforeCalculate()
        {
            base.BeforeCalculate();

            CVSSubModels.TryInit(this);
            List<decimal> points = new List<decimal>();
            points.Add(ExperimentTime);
            
            foreach (var function in CVSSubModels.LoadModel.LoadFunctions)
            {
                foreach (double point in function.InnerFunction.GetCriticalPoints())
                {
                    if (!points.Contains((decimal)point))
                        points.Add((decimal)point);
                }
            }

            foreach (var model in Configuration.Models)
            {
                if (model is LissovModelBase)
                    (model as LissovModelBase).UnbreakablePoints = points;
            }

            Validate();
        }

        public override LissovModelBase.MaxStepInfo GetMaxStepInfo()
        {
            MaxStepInfo minimal = base.GetMaxStepInfo();

            if (CVSSubModels.VsModel != null && CVSSubModels.HeartModel != null)
            {
                double normalHR = 120;
                double stepL = 1 / (2 * CVSSubModels.HeartModel.LeftPower.Value  *
                    (CVSSubModels.VsModel.RigidityZero[CVSSubModels.VsModel.NetConfiguration.LeftBeforeHeart]
                    / CVSSubModels.VsModel.PV_Functions[CVSSubModels.VsModel.NetConfiguration.LeftBeforeHeart].linearV)
                    * (normalHR - CVSSubModels.HeartModel.LeftHeartRate0.Value));
                double stepR = 1 / (2 * CVSSubModels.HeartModel.RightPower.Value *
                    (CVSSubModels.VsModel.RigidityZero[CVSSubModels.VsModel.NetConfiguration.RightBeforeHeart]
                    / CVSSubModels.VsModel.PV_Functions[CVSSubModels.VsModel.NetConfiguration.RightBeforeHeart].linearV)
                    * (normalHR - CVSSubModels.HeartModel.RightHeartRate0.Value));
                if (stepR < stepL)
                {
                    minimal.Step = stepR;
                    minimal.Text = stepR.ToString() + " (CVS to Heart in Right ventricle at 120 bps)";
                }
                else
                {
                    minimal.Step = stepL;
                    minimal.Text = stepL.ToString() + " (CVS to Heart in Left ventricle at 120 bps)";
                }
            }

            foreach (var model in Configuration.Models)
            {
                if (model is LissovModelBase && model != this)
                {
                    MaxStepInfo info = (model as LissovModelBase).GetMaxStepInfo();
                    if (info.Step < minimal.Step)
                        minimal = info;
                }
            }
            return minimal;
        }

        public override LissovModelBase.ValidateResult Validate()
        {
            return base.Validate() + (new ModelValidator(this).ValidateModels());
        }
    }
}
