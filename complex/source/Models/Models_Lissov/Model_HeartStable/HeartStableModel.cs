using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using ModelBase;
using System.Xml;
using LissovLog;
using System.Windows.Forms;

namespace Model_HeartStable
{
    public class HeartStableModel : LissovModelBase
    {
        public HeartStableModel() :base()
        {
            Log.Write(LogLevel.INFO, "HeartStable model is starting");

            Name = "HeartStable";
            DisplayName = "Stable Heart";

            VP_OLE_GUID = "{7423A039-8EFF-4734-84AE-7DDC71F0C6B6}";
        }

        public Value PressRightBeforeHeart = new LissovValue("PressRightBeforeHeart", "Pressure before right ventricle", Value.ValueType.Input, Constants.Units.mmHg);
        public Value PressRightAfterHeart = new LissovValue("PressRightAfterHeart", "Pressure after right ventricle", Value.ValueType.Input, Constants.Units.mmHg);
        public Value PressLeftBeforeHeart = new LissovValue("PressLeftBeforeHeart", "Pressure before left ventricle", Value.ValueType.Input, Constants.Units.mmHg);
        public Value PressLeftAfterHeart = new LissovValue("PressLeftAfterHeart", "Pressure after left ventricle", Value.ValueType.Input, Constants.Units.mmHg);
        public Value HeartRate = new LissovValue("HeartRate", "Heart Rate", Value.ValueType.Input, Constants.Units.beat_per_second);
        public Value InotropismR = new LissovValue("InotropsmRight", "Inotropism of right ventricle", Value.ValueType.Input, Constants.Units.unit);
        public Value InotropismL = new LissovValue("InotropsmLeft", "Inotropism of left ventricle", Value.ValueType.Input, Constants.Units.unit);
        public Value ExternalPressure = new LissovValue("ExternalPressure", "External pressure", Value.ValueType.Input, Constants.Units.mmHg);

        public Parameter ElasticKoefficientR = new Parameter("ElasticKoefficientRight", "Elastic koefficient Right");
        public Parameter ElasticKoefficientL = new Parameter("ElasticKoefficientLeft", "Elastic koefficient Left");
        public Parameter RightPower = new Parameter("RightPower", "Right ventricle Power");
        public Parameter RightPressure0 = new Parameter("RightPressureThr", "Right ventricle Pressure Threshold");
        public Parameter RightHeartRate0 = new Parameter("RightHeartRateThr", "Right ventricle HeartRate Threshold");
        public Parameter LeftPower = new Parameter("LeftPower", "Left ventricle Power");
        public Parameter LeftPressure0 = new Parameter("LeftPressureThr", "Left ventricle Pressure Threshold");
        public Parameter LeftHeartRate0 = new Parameter("LeftHeartRateThr", "Left ventricle HeartRate Threshold");
        public Parameter FlowInertiality = new Parameter("FlowInertiality", "Flow Inertiality");

        public Value FlowRight = new LissovValue("FlowRight", "Flow through right ventricle", Value.ValueType.Output, Constants.Units.ml_per_second) { LinkExpected = true };
        public Value FlowLeft = new LissovValue("FlowLeft", "Flow through left ventricle", Value.ValueType.Output, Constants.Units.ml_per_second) { LinkExpected = true };

        public override void Cycle()
        {
            double ext_pressureR = ExternalPressure.Value[CurrStep] * ElasticKoefficientR.Value;
            double ext_pressureL = ExternalPressure.Value[CurrStep] * ElasticKoefficientL.Value;
            double pressure_befR = PressRightBeforeHeart.Value[CurrStep] - ext_pressureR;
            double pressure_aftR = PressRightAfterHeart.Value[CurrStep] - ext_pressureR;
            double pressure_befL = PressLeftBeforeHeart.Value[CurrStep] - ext_pressureL;
            double pressure_aftL = PressLeftAfterHeart.Value[CurrStep] - ext_pressureL;
            if (pressure_befR > 10) pressure_befR = 10;
            if (pressure_befL > 20) pressure_befL = 20;
            double heartrate = HeartRate.Value[CurrStep];
            double inotrR = InotropismR.Value[CurrStep];
            double inotrL = InotropismL.Value[CurrStep];

            FlowRight.Value[CurrStep + 1] = RightPower.Value * inotrR *
                (pressure_befR - RightPressure0.Value) * (heartrate - RightHeartRate0.Value);
            FlowLeft.Value[CurrStep + 1] = LeftPower.Value * inotrL *
                (pressure_befL - LeftPressure0.Value) * (heartrate - LeftHeartRate0.Value);
            
            if (FlowInertiality.Value > 0)
            {
                FlowRight.Value[CurrStep+1] = FlowRight.Value[CurrStep] +
                    (FlowRight.Value[CurrStep + 1] - FlowRight.Value[CurrStep]) * (double)Step / FlowInertiality.Value;

                FlowLeft.Value[CurrStep + 1] = FlowLeft.Value[CurrStep] +
                    (FlowLeft.Value[CurrStep + 1] - FlowLeft.Value[CurrStep]) * (double)Step / FlowInertiality.Value;
            }

            if (FlowLeft.Value[CurrStep + 1] < 0)
            {
                FlowLeft.Value[CurrStep + 1] = 0;
                Log.Write(LogLevel.WARN, "Step [{0}], time [{1}]. Flow through left ventricle is negative, changed to 0.", CurrStep, CurrTime);
            }
            if (FlowRight.Value[CurrStep + 1] < 0)
            {
                FlowRight.Value[CurrStep + 1] = 0;
                Log.Write(LogLevel.WARN, "Step [{0}], time [{1}]. Flow through right ventricle is negative, changed to 0.", CurrStep, CurrTime);
            }
        }

        public override void LoadOLEFromParametersNode(XmlElement xml)
        {
            Log.Write(LogLevel.INFO, "Parameters count to load: " + xml.ChildNodes.Count.ToString());
            int errors = 0;
            foreach (XmlElement item in xml.ChildNodes)
            {
                string name = item.Attributes[0].Value;
                string value = item.Attributes[1].Value;
                double d;
                double.TryParse(value, out d);

                switch (name)
                {
                    case "Step": Step = decimal.Parse(value); break;
                    case "Right ventricle power": RightPower.Value = d; break;
                    case "Zero pressure right ventricle": RightPressure0.Value = d; break;
                    case "Right ventricle HR koeff.": RightHeartRate0.Value = d; break;
                    case "Left ventricle power": LeftPower.Value = d; break;
                    case "Zero pressure left ventricle": LeftPressure0.Value = d; break;
                    case "Left ventricle HR koeff.": LeftHeartRate0.Value = d; break;

                    case "Initial flow - Right ventricle": FlowRight.InitValue = d; break;
                    case "Initial flow - Left ventricle": FlowLeft.InitValue = d; break;

                    case "Flow inertiality Left":
                    case "Flow inertiality Right": 
                        FlowInertiality.Value = d; break;

                    case "Elastic coefficient": 
                        ElasticKoefficientR.Value = d; 
                        ElasticKoefficientL.Value = d; 
                        break;
                        
                    default:
                        Log.Write(LogLevel.WARN, string.Format("Ignored unknown parameter [{0}], value [{1}]", name, value));
                        errors++;
                        break;
                }
            }
            int all = xml.ChildNodes.Count;
            Log.Write(LogLevel.INFO, "Successfully loaded: {0} of {1} ({2} %)", (all - errors).ToString(), all.ToString(), (100 * (all - errors) / all).ToString());
        }
    }
}