using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using ModelBase;
using LissovBase.Functions;
using LissovLog;
using System.Xml;

namespace Model_Kidney
{
    public class KidneyModel : LissovModelBase
    {
        public KidneyModel()
        {
            Name = "Kidney";
            DisplayName = "Kidney";

            VP_OLE_GUID = "{2B21EDBD-5AB9-4B56-A514-951A7A5C7D34}";
        }

        public const int CONSUMPTION_MODE_NONE = 0;
        public const int CONSUMPTION_MODE_CONSTANT = 1;
        public const int CONSUMPTION_MODE_RESERVOIR = 2;
        public const int CONSUMPTION_MODE_FUNCTION = 3;

        #region Vars
        public ParameterSafe UseKidney = new ParameterSafe("UseKidney", "Use Kidney");
        public ParameterSafe CopyFirstStepValue = new ParameterSafe("CopyFirstStepValue", "Copy first step value");
        public ParameterObject<SFunction> GainADG_Press = new ParameterObject<SFunction>("ADG to pressure dependency", new SFunction());
        public ParameterSafe PrF0 = new ParameterSafe("Zero Filtering Pressure");
        public ParameterSafe KF = new ParameterSafe("Filtering Koefficient");
        public ParameterObject<SFunction> GainRF_ADG = new ParameterObject<SFunction>("Reabsorbtion to ADG dependency", new SFunction());
        public ParameterSafe ReabsKoeffByEnergyDeff = new ParameterSafe("ReabsKoeffByEnergyDeff", "Reabs. koeff. by Energy deficite");
        
        public LissovValue PrCap = new LissovValue("Pressure in Kidney capillares", Value.ValueType.Input, Constants.Units.mmHg);
        public LissovValue PrOsm = new LissovValue("Osmotic pressure in Kidney capillares", Value.ValueType.Input, Constants.Units.mmHg);
        public LissovValue Energy = new LissovValue("Energy", Value.ValueType.Input, Constants.Units.unit);

        public LissovValue ADG = new LissovValue("ADG", Value.ValueType.Output, Constants.Units.unit);
        public LissovValue FiltFlow = new LissovValue("Filtering Flow", Value.ValueType.Output, Constants.Units.ml_per_second);
        public LissovValue ReabFlow = new LissovValue("Reabsorbing Flow", Value.ValueType.Output, Constants.Units.ml_per_second);
        public LissovValue FlowOut = new LissovValue("Flow Out", Value.ValueType.Output, Constants.Units.ml_per_second);
        public LissovValue FlowOutWithCons = new LissovValue("Flow Out (including comsumption)", Value.ValueType.Output, Constants.Units.ml_per_second) { LinkExpected = true };
        public LissovValue FlowOutTotal = new LissovValue("Output volume", Value.ValueType.Output, Constants.Units.ml_per_second);

        public LissovValue DrinkingRate = new LissovValue("Drinking rate", Value.ValueType.Input, Constants.Units.ml_per_second);
        public LissovValue WaterConsumption = new LissovValue("Water consumption", Value.ValueType.Output);
        public LissovValue ReservoirVolume = new LissovValue("Reservoir Volume", Value.ValueType.Output);
        public LissovValue CVSVolume = new LissovValue("CVS Volume", Value.ValueType.Input);
        public ParameterSafe ConsumptionMode = new ParameterSafe("Consumption Mode");
        public ParameterSafe ConstantConsumptionSpeed = new ParameterSafe("Constant consumption speed");
        public ParameterSafe ReservoirConsumptionSpeed = new ParameterSafe("Reservoir consumption speed");
        public ParameterSafe ReservoirRefillCVSVolume = new ParameterSafe("Reservoir refill CVS Volume");
        public ParameterSafe ReservoirRefillVolume = new ParameterSafe("Reservoir refill volume");
        public ParameterSafe ReservoirRefillDelay = new ParameterSafe("Reservoir refill delay");
        public ParameterSafe ReservoirMaxVolume = new ParameterSafe("Reservoir maximum volume");
        public ParameterSafe UseEnergy = new ParameterSafe("Simulate energy influence");
        private decimal _lastConsumptionTime;
        #endregion

        #region Load&Save
        public override void LoadOLEFromParametersNode(System.Xml.XmlElement xml)
        {
            int all = xml.ChildNodes.Count;
            Log.Write(LogLevel.INFO, "Parameters count to load: " + all.ToString());
            int errors = 0;
            foreach (XmlElement item in xml.ChildNodes)
            {
                string name = item.Attributes[0].Value;
                string value = item.Attributes[1].Value;
                double d;
                double.TryParse(value, out d);

                switch (name)
                {
                    case "Step": Step = (decimal)d; continue;

                    case "Filtering flow / pressure": KF.Value = d; continue;
                    case "Filtering pressure 0": PrF0.Value = d; continue;

                    case "ADG_PRESS_level":     GainADG_Press.Content.Level = d; continue;
                    case "ADG_PRESS_a":         GainADG_Press.Content.A = d; continue;
                    case "ADG_PRESS_b":         GainADG_Press.Content.B = d; continue;
                    case "ADG_PRESS_n":         GainADG_Press.Content.N = d; continue;
                    case "ADG_PRESS_xscale":    GainADG_Press.Content.XScale = d; continue;
                    case "ADG_PRESS_threshold": GainADG_Press.Content.XMin = d; continue;
                    case "ADG_PRESS_scale":     GainADG_Press.Content.XMax = d; continue;

                    case "QF_ADG_level":        GainRF_ADG.Content.Level = d; continue;                    
                    case "QF_ADG_a":            GainRF_ADG.Content.A = d; continue;
                    case "QF_ADG_b":            GainRF_ADG.Content.B = d; continue;
                    case "QF_ADG_n":            GainRF_ADG.Content.N = d; continue;
                    case "QF_ADG_xscale":       GainRF_ADG.Content.XScale = d; continue;
                    case "QF_ADG_threshold":    GainRF_ADG.Content.XMin = d; continue;
                    case "QF_ADG_scale":        GainRF_ADG.Content.XMax = d; continue;
                        
                    case "Sleep time":
                    case "Exposure":
                        Log.Write(LogLevel.WARN, string.Format("Ignored parameter [{0}], value [{1}]", name, value));
                        all--;
                        continue;

                    default:
                        Log.Write(LogLevel.WARN, string.Format("Ignored unknown parameter [{0}], value [{1}]", name, value));
                        errors++;
                        continue;
                }
            }
            Log.Write(LogLevel.INFO, "Successfully loaded: {0} of {1} ({2} %)", (all - errors).ToString(), all.ToString(), (100 * (all - errors) / all).ToString());
        }
        #endregion

        #region Calcs
        public override void BeforeCalculate()
        {
            base.BeforeCalculate();
            CalcStep(0, 0);
            FlowOutTotal[0] = 0;
            _lastConsumptionTime = 0;
        }

        public override void Cycle()
        {
            CalcStep(CurrStep + 1, CurrStep);
        }

        private void CalcStep(int ns, int bs)
        {
            ADG[ns] = GainADG_Press.Content.getValue(PrOsm[bs]);

            FiltFlow[ns] = (PrCap[bs] > PrF0) ? KF * (PrCap[bs] - PrF0) : 0;
            double d = 1;
            if (UseEnergy.Value == LissovModelBase.TRUE 
                && Energy.Value[bs] < 0)
            {
                d = 1 -ReabsKoeffByEnergyDeff * (Energy.Value[bs]);
            }
            ReabFlow[ns] = FiltFlow[ns] * GainRF_ADG.Content.getValue(ADG[ns]) * d;
            if (ReabFlow[ns] > FiltFlow[ns])
                ReabFlow[ns] = FiltFlow[ns];

            #region Consumption
            //water consumption
            switch ((int)(ConsumptionMode.Value))
            {
                case CONSUMPTION_MODE_NONE:
                    WaterConsumption[ns] = 0;
                    break;
                case CONSUMPTION_MODE_CONSTANT:
                    WaterConsumption[ns] = ConstantConsumptionSpeed.Value;
                    break;
                case CONSUMPTION_MODE_RESERVOIR:
                    if (CVSVolume[bs] < ReservoirRefillCVSVolume.Value
                        && (CurrTime - _lastConsumptionTime) > (decimal)ReservoirRefillDelay.Value
                        && (ReservoirVolume.Value[bs] + ReservoirRefillVolume.Value < ReservoirMaxVolume.Value))
                    {
                        ReservoirVolume[bs] = ReservoirVolume[bs] + ReservoirRefillVolume.Value;
                        _lastConsumptionTime = CurrTime;
                    }
                    WaterConsumption[ns] = ReservoirVolume[bs] * ReservoirConsumptionSpeed.Value;
                    ReservoirVolume[ns] = ReservoirVolume[bs] - WaterConsumption[ns] * (double)Step;
                    break;
                case CONSUMPTION_MODE_FUNCTION:
                    WaterConsumption[ns] = DrinkingRate.Value[bs];
                    break;
            }
            #endregion

            if (UseKidney.Value == LissovModelBase.TRUE)
            {
                if (ns > 1 && CopyFirstStepValue.Value == LissovModelBase.TRUE)
                    FlowOut[ns] = FlowOut[1];
                else
                    FlowOut[ns] = FiltFlow[ns] - ReabFlow[ns];

                FlowOutWithCons[ns] = FlowOut[ns] - WaterConsumption[ns];
            }
            else
            {
                FlowOut[ns] = 0;
                FlowOutWithCons[ns] = 0;
            }

            FlowOutTotal[ns] = FlowOutTotal[bs] + FlowOutWithCons[ns] * (double)Step;
        }
        #endregion

        public override double GetValueByNameAndTime(string valueName, decimal time)
        {
            return base.GetValueByNameAndTime(valueName, time);
        }
    }
}
