using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using LissovBase;
using LissovBase.Functions;

namespace Model_Load
{
    public class LoadModel : LissovBase.LissovModelBase
    {
        public ModelBase.Value zero = new LissovValue("Zero", "Zero", ModelBase.Value.ValueType.Output, Constants.Units.none);
        public ModelBase.Value one = new LissovValue("One", "One", ModelBase.Value.ValueType.Output, Constants.Units.none);
        public ModelBase.Value stableHF = new LissovValue("StableHeartFlow", "Stable Heart Flow", ModelBase.Value.ValueType.Output, Constants.Units.ml_per_second);
        public ModelBase.Value baroreception = new LissovValue("Baroreception", "Baroreception", ModelBase.Value.ValueType.Output, Constants.Units.unit);
        public ModelBase.Value sin1 = new LissovValue("Sinus1", "Sinus 1", ModelBase.Value.ValueType.Output, Constants.Units.none);
        public ModelBase.Value sin5 = new LissovValue("Sinus5", "Sinus 5", ModelBase.Value.ValueType.Output, Constants.Units.none);
        
        public ModelBase.Value heart_rate = new LissovValue("HeartRate", "Heart Rate", ModelBase.Value.ValueType.Output, Constants.Units.beat_per_second);
        public ModelBase.Value inotropismR = new LissovValue("InotropismRight", "Right ventricle inotropism", ModelBase.Value.ValueType.Output, Constants.Units.unit);
        public ModelBase.Value inotropismL = new LissovValue("InotropismLeft", "Left ventricle inotropism", ModelBase.Value.ValueType.Output, Constants.Units.unit);

        public ModelBase.Value vascularOsmoticPressure = new LissovValue("VascularOsmoticPressure", "Vascular osmotic pressure", ModelBase.Value.ValueType.Output, Constants.Units.mmHg);
        public ModelBase.Value pulmonaryOsmoticPressure = new LissovValue("PulmonaryOsmoticPressure", "Pulmonary osmotic pressure", ModelBase.Value.ValueType.Output, Constants.Units.mmHg);

        public ModelBase.Value Gravity = new LissovValue("Gravity", "Gravity", ModelBase.Value.ValueType.Output, Constants.Units.unit);
        
        public Parameter Sinus1Amplitude = new Parameter("Sinus1Amplitude", "Amplitude of sinus 1"); 
        public Parameter Sinus5Amplitude = new Parameter("Sinus5Amplitude", "Amplitude of sinus 5");        

        public ParameterLoadFunction Temperature = new ParameterLoadFunction("External Temperature", null, Constants.Units.degreeCelsium);
        public ParameterLoadFunction AtmospherePressure = new ParameterLoadFunction("Atmosphere Pressure", null, Constants.Units.mmHg);
        public ParameterLoadFunction MuscularActivity = new ParameterLoadFunction("Muscular Activity", null, Constants.Units.unit);
        public ParameterLoadFunction RotationAngle = new ParameterLoadFunction("Angle", null, Constants.Units.degree);
        public ParameterLoadFunction Hemorrhage = new ParameterLoadFunction("Hemorrhage", null, Constants.Units.ml);
        public ParameterLoadFunction WaterInput = new ParameterLoadFunction("Water drinking rate", null, Constants.Units.ml_per_second);
        public ParameterLoadFunction BaroreceptionPower = new ParameterLoadFunction("Baroreception Power", null, Constants.Units.unit);

        public ModelBase.Value HemmorhageFlow = new LissovValue("Hemmorhage flow", ModelBase.Value.ValueType.Output, Constants.Units.ml_per_second);
        
        public LoadModel()
        {
            Name = "Loads";
            DisplayName = "Model for Constants";

            foreach (var item in LoadFunctions)
            {
                item.InnerFunction = new LinearFunction();
                item.InnerFunction.Unit = item.Output.Measure;
            }
        }

        public override void Cycle()
        {
            for (int i = 0; i < Values.Count; i++)
            {
                Values[i].Value[CurrStep+1] = Values[i].InitValue;
            }

            sin1.Value[CurrStep+1] = Sinus1Amplitude.Value * Math.Sin((double)CurrTime);
            sin5.Value[CurrStep + 1] = Sinus5Amplitude.Value * Math.Sin((double)CurrTime / 5);

            foreach (var load in LoadFunctions)
            {
                load.Calculate(CurrStep + 1, (double)CurrTime);
            }

            HemmorhageFlow.Value[CurrStep + 1] = (Hemorrhage.Output.Value[CurrStep + 1] - Hemorrhage.Output.Value[CurrStep])
                        / (double)Step;            
        }

        private LoadsSetupControl _control = null;
        public override System.Windows.Forms.UserControl GetControl()
        {
            if (_control == null)
            {
                _control = new LoadsSetupControl();
                _control.Init(this);
            }
            return _control;
        }

        public override decimal ExperimentTime
        {
            get
            {
                return base.ExperimentTime;
            }
            set
            {
                base.ExperimentTime = value;

                foreach (var item in LoadFunctions)
                {
                    item.InnerFunction.MaxX = (double)value;
                }

                if (_control != null)
                    _control.RefreshControl();
            }
        }

        public List<ParameterLoadFunction> LoadFunctions
        {
            get
            {
                return new List<ParameterLoadFunction>(new ParameterLoadFunction[]{
                    BaroreceptionPower,
                    WaterInput,
                    Temperature,
                    Hemorrhage,
                    RotationAngle,
                    MuscularActivity,
                    AtmospherePressure
                });
            }
        }

        public override object FromXml(System.Xml.XmlElement currentElement)
        {
            object res = base.FromXml(currentElement);
            foreach (var item in LoadFunctions)
            {
                item.InnerFunction.Unit = item.Output.Measure;
            }
            return res;
        }
    }
}