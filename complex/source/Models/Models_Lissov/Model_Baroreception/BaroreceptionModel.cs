using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using ModelBase;
using LissovBase.Functions;
using LissovLog;
using System.Xml;

namespace Model_Baroreception
{
    public class BaroreceptionModel : LissovModelBase
    {
        public BaroreceptionModel()
        {
            Name = "Baroreception";
            DisplayName = "Baroreception";

            IsLoaded = true; // avoid parameters gathering

            VP_OLE_GUID = "{C4137B81-72A1-45F2-8C5B-C307141C12F2}";
            setArrays();
        }

        #region Internal
        Function barorecNormalizer;

        internal int baroZoneCount = 3;
        internal string[] zoneID;
        private int[] delaySteps;
        internal int hormoneCount = 3;
        internal string[] hormoneID;
        private void setArrays()
        {
            values = null;
            parameters = null;

            useZone = new double[baroZoneCount];
            pressureThresholdZone = new double[baroZoneCount];
            baroreceptionZoneWeight = new double[baroZoneCount];
            elasticKoefficient = new double[baroZoneCount];
            delayTime = new double[baroZoneCount];
            zoneID = new string[baroZoneCount];
            zoneID[0] = "Aorta";
            zoneID[1] = "CarotidSine";
            zoneID[2] = "WillisCircle";

            baroCurve = new LogisticFunction2Par[baroZoneCount];
            for (int i = 0; i < baroZoneCount; i++)
                baroCurve[i] = new LogisticFunction2Par() { Unit = Constants.Units.none, XUnit  = Constants.Units.mmHg };

            hormoneEnergyDefGain = new double[hormoneCount];
            hormoneNormalConc = new double[hormoneCount];
            hormoneMinConc = new double[hormoneCount];
            hormoneMaxConc = new double[hormoneCount];
            hormoneGain0 = new double[hormoneCount];
            hormoneGainBaro = new double[hormoneCount];
            hormoneInitial = new double[hormoneCount];
            hormoneTimeKoeff = new double[hormoneCount];
            hormoneDestr = new double[hormoneCount];
            heartRateGain = new double [hormoneCount];
            inotropismRGain = new double[hormoneCount];
            inotropismLGain = new double[hormoneCount];
            hormoneID = new string[hormoneCount];
            hormoneID[0] = "Adrenalin";
            hormoneID[1] = "Noradrenalin";
            hormoneID[2] = "Acetylholine";

            //Values
            pressureInReceptorZone = new double[baroZoneCount][];
            pressureBaseReceptorZone = new double[baroZoneCount][];
            baroreceptionInZone = new double[baroZoneCount][];
            hormoneConcentrationBaro = new double[hormoneCount][];
            hormoneConcentration = new double[hormoneCount][];
        }
        #endregion

        #region Parameters
        private ParameterSafe BaroreceptionNormal = new ParameterSafe("BaroreceptionNormal", "Normal value of baroreception");
        internal double[] useZone; // 0-false, 1 - true(Use)
        internal double[] pressureThresholdZone;
        internal double[] elasticKoefficient;
        public double[] baroreceptionZoneWeight;
        internal double[] delayTime;
        internal LogisticFunction2Par[] baroCurve;

        private double[] hormoneEnergyDefGain;
        private double[] hormoneNormalConc;
        private double[] hormoneMinConc;
        private double[] hormoneMaxConc;
        private double[] hormoneGain0;
        private double[] hormoneGainBaro;
        private double[] hormoneInitial;
        private double[] hormoneTimeKoeff;
        private double[] hormoneDestr;
        private ParameterSafe HeartRateAuto = new ParameterSafe("HeartRateAuto", "Heart Rate with no hormons");
        private ParameterSafe HeartRateMin = new ParameterSafe("HeartRateMin", "Heart Rate minimum");
        private double[] heartRateGain;
        private ParameterSafe InotropismRAuto = new ParameterSafe("InotropismRightAuto", "Inotropism of Right ventricle with no hormons");
        private double[] inotropismRGain;
        private ParameterSafe InotropismLAuto = new ParameterSafe("InotropismLeftAuto", "Inotropism of Left ventricle with no hormons");
        private double[] inotropismLGain;
        public ParameterSafe BaroreceptionCopyFirst = new ParameterSafe("BaroreceptionCopyFirst", "Copy first Baroreception value");
        public ParameterSafe UseEnergy = new ParameterSafe("UseEnergy", "Simulate energy influence");

        private List<Parameter> parameters = null;
        public override List<Parameter> GetParameters()
        {
            if (parameters != null) return parameters;
            parameters = base.GetSingleModelParameters();
            parameters.Add(HeartRateThermoDelta.Use);
            parameters.Add(BaroreceptionCopyFirst);
            parameters.Add(BaroreceptionNormal);
            parameters.Add(HeartRateAuto);
            parameters.Add(HeartRateMin);
            parameters.Add(InotropismRAuto);
            parameters.Add(InotropismLAuto);
            parameters.Add(UseEnergy);
            for (int i = 0; i < baroZoneCount; i++)
            {
                parameters.Add(new ParameterArrayElement("UseZone_" + zoneID[i], "Use baroreception zone " + zoneID[i], useZone, i));
                parameters.Add(new ParameterArrayElement("PressureThresholdZone_" + zoneID[i], "Pressure threshold in zone " + zoneID[i], pressureThresholdZone, i));
                parameters.Add(new ParameterArrayElement("ElasticKoefficient_" + zoneID[i], "Elastic koefficient of " + zoneID[i], elasticKoefficient, i));
                parameters.Add(new ParameterArrayElement("BaroreceptionZoneWeight_" + zoneID[i], "Baroreception zone weight of " + zoneID[i], baroreceptionZoneWeight, i));
                parameters.Add(new ParameterArrayElement("BaroreceptionDelay_" + zoneID[i], "Baroreception delay in " + zoneID[i], delayTime, i));
                parameters.Add(new ParameterFunction("BaroreceptionCurve_" + zoneID[i], "Baroreception / pressure dependancy of " + zoneID[i], baroCurve[i]));
            }

            for (int i = 0; i < hormoneCount; i++)
            {
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_EnergyK", hormoneID[i] + " energy deff. Gain", hormoneEnergyDefGain, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_Initial", hormoneID[i] + " initial concentration", hormoneInitial, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_NormalConc", hormoneID[i] + " normal concemtraction", hormoneNormalConc, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_MinConc", hormoneID[i] + " minimum concemtraction", hormoneMinConc, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_MaxConc", hormoneID[i] + " maximum concemtraction", hormoneMaxConc, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_Gain0", hormoneID[i] + " zero gain", hormoneGain0, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_GainBaro", hormoneID[i] + " gain from baoreception", hormoneGainBaro, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_TimeKoeff", hormoneID[i] + " time koefficient", hormoneTimeKoeff, i));
                parameters.Add(new ParameterArrayElement(hormoneID[i] + "_DestrK", hormoneID[i] + " destruction speed", hormoneDestr, i));

                parameters.Add(new ParameterArrayElement("HeartRateGain_" + hormoneID[i], "HeartRate gain to" + hormoneID[i], heartRateGain, i));
                parameters.Add(new ParameterArrayElement("InotropismRGain_" + hormoneID[i], "Inotromism of right ventricle gain to" + hormoneID[i], inotropismRGain, i));
                parameters.Add(new ParameterArrayElement("InotropismLGain_" + hormoneID[i], "Inotromism of left ventricle gain to" + hormoneID[i], inotropismLGain, i));
            }
            base.Parameters = parameters;
            return parameters;
        }        
        #endregion

        #region Values
        public Value Energy = new LissovValue("Energy", "Energy", Value.ValueType.Input, Constants.Units.unit);
        public Value ExternalPressure = new LissovValue("ExternalPressure", "External Pressure", Value.ValueType.Input, Constants.Units.mmHg);
        public Value HeartRateDelta = new LissovValue("Heart Rate Delta", Value.ValueType.Input, Constants.Units.beat_per_second);
        public Value BaroreceptionTotal = new LissovValue("BaroreceptionTotal", "Baroreception", Value.ValueType.Output, Constants.Units.unit);
        private double[][] pressureInReceptorZone;
        private double[][] pressureBaseReceptorZone;
        private double[][] baroreceptionInZone;
        private double[][] hormoneConcentrationBaro;
        private double[][] hormoneConcentration;
        public Value HeartRate = new LissovValue("HeartRate", "Heart rate", Value.ValueType.Output, Constants.Units.beat_per_second) { LinkExpected = true };
        public Value InotropismRight = new LissovValue("InotropismRight", "Inotropism of Right ventricle", Value.ValueType.Output, Constants.Units.unit) { LinkExpected = true };
        public Value InotropismLeft = new LissovValue("InotropismLeft", "Inotropism of Left ventricle", Value.ValueType.Output, Constants.Units.unit) { LinkExpected = true };
        public Value BaroreceptionNormalized = new LissovValue("BaroreceptionNormalized", "Normalized value of baroreception", Value.ValueType.Output, Constants.Units.unit) { LinkExpected = true };
        public OptionalInput HeartRateThermoDelta = new OptionalInput("Heart Rate Temperature Delta", Value.ValueType.Input, Constants.Units.beat_per_second);
        public Value BaroreceptionPower = new LissovValue("BaroreceptionPower", "Baroreception Power", Value.ValueType.Input, Constants.Units.unit);
        private List<Value> values;
        public override List<Value> GetValues()
        {
            if (values != null) return values;
            values = GetSingleModelValues();
            values.Add(Energy);
            values.Add(ExternalPressure);
            values.Add(HeartRateDelta);
            for (int i = 0; i < baroZoneCount; i++)
            {
                values.Add(new ValueArrayElement("PressureInReceptorZone_" + zoneID[i], "Pressure in baroreception zone " + zoneID[i], Value.ValueType.Input, pressureInReceptorZone, i, Constants.Units.mmHg));
                values.Add(new ValueArrayElement("PressureBaseReceptorZone_" + zoneID[i], "Base pressure in baroreception zone " + zoneID[i], Value.ValueType.Input, pressureBaseReceptorZone, i, Constants.Units.mmHg));
                values.Add(new ValueArrayElement("BaroreceptionInReceptorZone_" + zoneID[i], "Baroreceptory activity in zone " + zoneID[i], Value.ValueType.Output, baroreceptionInZone, i, Constants.Units.mmHg));
            }
            for (int i = 0; i < hormoneCount; i++)
            {
                values.Add(new ValueArrayElement(hormoneID[i] + "ConcentrationBaro", hormoneID[i] + " concentration by B", Value.ValueType.Output, hormoneConcentrationBaro, i, Constants.Units.unit) { InitValue = hormoneInitial[i] });
                values.Add(new ValueArrayElement(hormoneID[i] + "Concentration", hormoneID[i] + " concentration", Value.ValueType.Output, hormoneConcentration, i, Constants.Units.unit) { InitValue = hormoneInitial[i] });
            }
            values.Add(BaroreceptionTotal);
            values.Add(BaroreceptionNormalized);
            values.Add(HeartRate);
            values.Add(InotropismRight);
            values.Add(InotropismLeft);
            values.Add(HeartRateThermoDelta);
            values.Add(BaroreceptionPower);
            base.Values = values;
            return values;
        }
        #endregion

        public override void BeforeCalculate()
        {
            base.BeforeCalculate();
            delaySteps = new int[baroZoneCount];
            for (int i = 0; i < baroZoneCount; i++)
                delaySteps[i] = (int)((decimal)delayTime[i] / Step);

            for (int i = 0; i < hormoneCount; i++)
            {
                hormoneConcentrationBaro[i][0] = hormoneInitial[i];
                hormoneConcentration[i][0] = hormoneInitial[i];
            }

            if (BaroreceptionNormal.Value == 0.25) {
                barorecNormalizer = new RootFunction();
                ((RootFunction)barorecNormalizer).setParams(1, 1, 0, 0);
            } else {
                barorecNormalizer = new SquareFunction();
                ((SquareFunction)barorecNormalizer).set0_1IntervalThroughPoint(BaroreceptionNormal.Value, 0.5);
            }
            BaroreceptionNormalized.Value[0] = barorecNormalizer.getValue(BaroreceptionNormal.Value);
        }
        public override void Cycle()
        {
            double baroreception_total = 0;
            double step = (double)Step;

            bool barorecCalculated = false;
            for (int i = 0; i < baroZoneCount; i++)
            {
                if (useZone[i] == 0) continue;
                barorecCalculated = true;
                double activePressure = pressureInReceptorZone[i][CurrStep]
                                        - pressureBaseReceptorZone[i][CurrStep]
                                        - ExternalPressure.Value[CurrStep] * elasticKoefficient[i]
                                        - pressureThresholdZone[i];

                if (BaroreceptionCopyFirst == LissovModelBase.FALSE)
                {
                    if (activePressure > 0)
                        baroreceptionInZone[i][CurrStep + 1] = baroCurve[i].getValue(activePressure);
                    else
                        baroreceptionInZone[i][CurrStep + 1] = 0;
                }
                else
                    baroreceptionInZone[i][CurrStep + 1] = baroreceptionInZone[i][0];

                double bpower = BaroreceptionPower.Value[CurrStep];
                baroreceptionInZone[i][CurrStep + 1] =
                    (baroreceptionInZone[i][CurrStep + 1] * bpower)
                    + (baroreceptionInZone[i][0] * (1 - bpower));

                if (CurrStep >= delaySteps[i])
                    baroreception_total += baroreceptionInZone[i][CurrStep + 1 - delaySteps[i]] * baroreceptionZoneWeight[i];
                else
                    baroreception_total += baroreceptionInZone[i][CurrStep] * baroreceptionZoneWeight[i];
            }

            if (!barorecCalculated)
            {
                baroreception_total = 0.5;
                Log.Write(LogLevel.WARN, "No baroreception zones used. Baroreception is equal to constant {0}", baroreception_total.ToString());
            }
            BaroreceptionTotal.Value[CurrStep + 1] = baroreception_total;
            BaroreceptionNormalized.Value[CurrStep + 1] = barorecNormalizer.getValue(baroreception_total);

            //Gormons
            for (int h = 0; h < hormoneCount; h++)
            {
                /*Adrenalin.Value[cs] = Adrenalin.Value[cs - 1] + step *
                    (GainAdrenalinBarorec.Value * (1 - baro)
                        - DestructionAdrenalin.Value * Adrenalin.Value[cs - 1]);*/

                var dhorm = hormoneGain0[h] 
                    + hormoneGainBaro[h] * baroreception_total
                    - hormoneDestr[h] * hormoneConcentrationBaro[h][CurrStep];
                hormoneConcentrationBaro[h][CurrStep + 1] = hormoneConcentrationBaro[h][CurrStep]
                    + hormoneTimeKoeff[h] * step * dhorm;

                if (UseEnergy.Value == LissovModelBase.TRUE)
                {
                    var e = Energy.Value[CurrStep];
                    hormoneConcentration[h][CurrStep + 1] = hormoneConcentrationBaro[h][CurrStep + 1]
                        - (e < 0
                                ? hormoneEnergyDefGain[h] * e
                                : 0);
                } else {
                    hormoneConcentration[h][CurrStep + 1] = hormoneConcentrationBaro[h][CurrStep + 1];
                }

                if (hormoneConcentration[h][CurrStep + 1] > hormoneMaxConc[h])
                    hormoneConcentration[h][CurrStep + 1] = hormoneMaxConc[h];
                if (hormoneConcentration[h][CurrStep + 1] < hormoneMinConc[h])
                    hormoneConcentration[h][CurrStep + 1] = hormoneMinConc[h];
            }

            //Heart regulation
            double heartrate = HeartRateAuto.Value + HeartRateDelta.Value[CurrStep];
            double inotrR = InotropismRAuto.Value;
            double inotrL = InotropismLAuto.Value;
            for (int h = 0; h < hormoneCount; h++)
            {
                heartrate += heartRateGain[h] * hormoneConcentration[h][CurrStep + 1];
                inotrR += inotropismRGain[h] * hormoneConcentration[h][CurrStep + 1];
                inotrL += inotropismLGain[h] * hormoneConcentration[h][CurrStep + 1];
            }

            if (HeartRateThermoDelta.Use.Value == LissovModelBase.TRUE)
                HeartRate.Value[CurrStep + 1] = heartrate + HeartRateThermoDelta.Value[CurrentStep - 1];
            else
                HeartRate.Value[CurrStep + 1] = heartrate;

            if (HeartRate.Value[CurrStep + 1] < HeartRateMin.Value)
                HeartRate.Value[CurrStep + 1] = HeartRateMin.Value;

            InotropismRight.Value[CurrStep + 1] = inotrR;
            InotropismLeft.Value[CurrStep + 1] = inotrL;
        }

        public override void LoadOLEFromParametersNode(System.Xml.XmlElement xml)
        {
            Log.Write(LogLevel.INFO, "Parameters count to load: " + xml.ChildNodes.Count.ToString());
            int errors = 0;
            foreach (XmlElement item in xml.ChildNodes)
            {
                string name = item.Attributes[0].Value;
                string value = item.Attributes[1].Value;
                double d;
                double.TryParse(value, out d);

                #region Hormones
                string[] parts = name.Split(' ');
                List<string> hormNames = new List<string>(new string[] { "Adrenaline", "Noradrenaline", "Acetilholine" });
                int index = hormNames.ToList().IndexOf(parts[0]);
                if (index >= 0)
                {
                    string par = name.Substring(parts[0].Length + 1);
                    switch (par)
                    {
                        case "initial": hormoneInitial[index] = d; continue;
                        case "norm. conc.": hormoneNormalConc[index] = d; continue;
                        case "min. conc.": hormoneMinConc[index] = d; continue;
                        case "max. conc.": hormoneMaxConc[index] = d; continue;
                        case "gain 0": hormoneGain0[index] = d; continue;
                        case "gain": hormoneGainBaro[index] = d; continue;
                        case "time koeff": hormoneTimeKoeff[index] = d; continue;
                        default:
                            // continue looking
                            break;
                    }
                }
                #endregion

                switch (name)
                {
                    case "step": Step = decimal.Parse(value); continue;

                    case "Aorctic Arch Use": useZone[0] = d; continue;
                    case "Aorctic Arch threshold": pressureThresholdZone[0] = d; continue;
                    case "Aorctic Arch alpha": baroCurve[0].alpha = d; continue;
                    case "Aorctic Arch theta": baroCurve[0].theta = d; continue;
                    case "Aorctic Arch gain": baroreceptionZoneWeight[0] = d; continue;
                    case "Aorctic Arch elastic coeff": elasticKoefficient[0] = d; continue;
                    case "Aorctic Arch time": delayTime[0] = d; continue;
                    case "Carotid Sinus Use": useZone[1] = d; continue;
                    case "Carotid Sinus threshold": pressureThresholdZone[1] = d; continue;
                    case "Carotid Sinus alpha": baroCurve[1].alpha = d; continue;
                    case "Carotid Sinus theta": baroCurve[1].theta = d; continue;
                    case "Carotid Sinus gain": baroreceptionZoneWeight[1] = d; continue;
                    case "Carotid Sinus elastic coeff": elasticKoefficient[1] = d; continue;
                    case "Carotid Sinus time": delayTime[1] = d; continue;
                    case "Willis circle Use": 
                    case "Pulmonary arterie Use": useZone[2] = d; continue;
                    case "Willis circle threshold":
                    case "Pulmonary arterie threshold": pressureThresholdZone[2] = d; continue;
                    case "Willis circle alpha":
                    case "Pulmonary arterie alpha": baroCurve[2].alpha = d; continue;
                    case "Willis circle theta":
                    case "Pulmonary arterie theta": baroCurve[2].theta = d; continue;
                    case "Willis circle gain":
                    case "Pulmonary arterie gain": baroreceptionZoneWeight[2] = d; continue;
                    case "Willis circle elastic coeff":
                    case "Pulmonary arterie elastic coeff": elasticKoefficient[2] = d; continue;
                    case "Willis circle time":
                    case "Pulmonary arterie time": delayTime[2] = d; continue;

                    case "Heart Rate -Auto":            HeartRateAuto.Value = d; continue;
                    case "Heart Rate -Adrenaline":      heartRateGain[0] = d; continue;
                    case "Heart Rate -Noradrenaline":   heartRateGain[1] = d; continue;
                    case "Heart Rate -Acetilholine":    heartRateGain[2] = d; continue;
                    case "Inotr. R -Auto":              InotropismRAuto.Value = d; continue;
                    case "Inotr. R -Adrenaline":        inotropismRGain[0] = d; continue;
                    case "Inotr. R -Noradrenaline":     inotropismRGain[1] = d; continue;
                    case "Inotr. R -Acetilholine":      inotropismRGain[2] = d; continue;
                    case "Inotr. L -Auto":              InotropismLAuto.Value = d; continue;
                    case "Inotr. L -Adrenaline":        inotropismLGain[0] = d; continue;
                    case "Inotr. L -Noradrenaline":     inotropismLGain[1] = d; continue;
                    case "Inotr. L -Acetilholine":      inotropismLGain[2] = d; continue;

                    case "Emotional - gain Adrenaline":
                    case "HR0":
                    case "Exposure":
                    case "Sleep time":
                        Log.Write(LogLevel.WARN, string.Format("Ignored parameter [{0}], value [{1}]", name, value));
                        errors++;
                        continue;

                    default:
                        Log.Write(LogLevel.WARN, string.Format("Ignored unknown parameter [{0}], value [{1}]", name, value));
                        errors++;
                        continue;
                }
            }
            int all = xml.ChildNodes.Count;
            Log.Write(LogLevel.INFO, "Successfully loaded: {0} of {1} ({2} %)", (all - errors).ToString(), all.ToString(), (100 * (all - errors) / all).ToString());
        }

        private SetupControl _setupControl = null;
        public override System.Windows.Forms.UserControl GetControl()
        {
            if (_setupControl == null)
                _setupControl = new SetupControl(this);
            return _setupControl;
        }

        public IModelSetupControl SetupControl
        {
            get { return GetControl() as IModelSetupControl; }
        }
    }
}