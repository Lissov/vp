//#define DEBUG_LOGGING
#define INTERPOLATE_VALUES
//#define FIRST_STEPS_CALCULATE_HASH

using System.Collections.Generic;
using ModelBase;
using System.Xml;
using LissovLog;
using System.Windows.Forms;
using System;
using System.Reflection;
namespace LissovBase
{
    public abstract class LissovModelBase : ModelBase.ModelBase
    {
        #region Old model compatibility
        public string VP_OLE_GUID = string.Empty;
        #endregion

        #region Tanya Compatibility
        protected enum StepMethod { CalculateCurrent, CalculateNext }
        protected const StepMethod stepMethod = StepMethod.CalculateCurrent;
        #endregion

        #region Constants
        public const double TRUE = 1;
        public const double FALSE = 0;
        public const double NOT_LOADED = Constants.INVALID_DOUBLE;
        #endregion

        public LissovModelBase()
        {
            Log.Prepare(this.GetType().Name + "Log.txt");
            CompressionDensity.Value = 1;
        }

        public int ModelStepsCount
        {
            get
            {
                int stepMult = (int)((CompressionDensity.Value > 1) ? CompressionDensity.Value : 1);
                return (int)Math.Ceiling((ExperimentTime / Step) / stepMult) + stepMult;
            }
        }

        public ParameterSafe ChangeLevelToSkip = new ParameterSafe("Change level to perform skip") { Value = 0.5 };
        public ParameterSafe SkipStepTime = new ParameterSafe("Skip Time") { Value = 60 };
        public ParameterSafe CompressionDensity = new ParameterSafe("Compression Density");

        public LissovValue ChangeLevel = new LissovValue("Change level", Value.ValueType.Output) { InitValue = 0, LinkExpected = false };

        public List<Parameter> GetSingleModelParameters()
        {
            var res = new List<Parameter>();
            res.Add(CompressionDensity);
            res.Add(ChangeLevelToSkip);
            res.Add(SkipStepTime);
            return res;
        }

        protected List<Value> GetSingleModelValues()
        {
            return new List<Value>(new Value[] { ChangeLevel });
        }

        public override void CollectLists()
        {
            Values = new List<Value>();
            Parameters = new List<Parameter>();

            foreach (FieldInfo item in this.GetType().GetFields())
            {
                if (item.FieldType.Equals(typeof(Parameter)) || item.FieldType.IsSubclassOf(typeof(Parameter)))
                    Parameters.Add((Parameter)item.GetValue(this));

                IParameterCollection parcoll = item.GetValue(this) as IParameterCollection;
                if (parcoll != null) Parameters.AddRange(parcoll.getParameters());

                if (item.FieldType.Equals(typeof(Value)) || item.FieldType.IsSubclassOf(typeof(Value)))
                    Values.Add((Value)item.GetValue(this));

                IValueCollection valcoll = item.GetValue(this) as IValueCollection;
                if (valcoll != null)
                    Values.AddRange(valcoll.getValues());
            }

            IsLoaded = true;
        }

        protected void ProcessCollections(IList<string> names)
        {
            foreach (FieldInfo item in this.GetType().GetFields())
            {
                IParameterCollection parcoll = item.GetValue(this) as IParameterCollection;
                if (parcoll != null)
                    parcoll.Setup(names);

                IValueCollection valcoll = item.GetValue(this) as IValueCollection;
                if (valcoll != null)
                    valcoll.Setup(names);
            }
        }

        private ModelSetupControlSimple _control;
        public override UserControl GetControl()
        {
            if (_control == null)
                _control = new ModelSetupControlSimple(this);
            return _control;
        }

        #region Load and save

        /// <summary>
        /// Inserts double.MinValue to all Parameters and Values to check after load that everything is loaded
        /// </summary>
        protected void MarkAllUnloaded()
        {
            foreach (var item in GetParameters())
                item.Value = NOT_LOADED;
            foreach (var item in GetValues())
                item.InitValue = NOT_LOADED;
        }
        /// <summary>
        /// Checks that there are no double.MinValue in any of Parameters and Values - that means that loader processed every field
        /// </summary>
        protected bool CheckAllLoaded()
        {
            bool allOk = true;
            foreach (var item in GetParameters())
                if (item.Value == NOT_LOADED)
                {
                    Log.Write(this.Name, LogLevel.WARN, "Parameter {0} not loaded", item.Name);
                    item.Value = 0;
                    allOk = false;
                }
            foreach (var item in GetValues())
                if (item.InitValue == NOT_LOADED ||
                    (item.Type == Value.ValueType.Input && (string.IsNullOrEmpty(item.LinkModelName) || string.IsNullOrEmpty(item.LinkValueName))))
                {
                    Log.Write(this.Name, LogLevel.WARN, "Value {0} not loaded", item.Name);
                    allOk = false;
                }

            return allOk;
        }

        public override object FromXml(XmlElement currentElement)
        {
            MarkAllUnloaded();
            object o = base.FromXml(currentElement);
            CheckAllLoaded();
            return o;
        }

        #region Old complex
        public void LoadOldModelParameters(string filename)
        {
            Log.Write(LogLevel.INFO, string.Format("Start loading model from {0}", filename));
            XmlDocument xml_file = new XmlDocument();
            xml_file.Load(filename);
            XmlElement xml = xml_file.ChildNodes[0] as XmlElement;
            if (xml.ChildNodes[0].Name == "One_model")
            {
                LoadOLEFromNode(xml.ChildNodes[0].ChildNodes[1] as XmlElement); // take model_data
            }
            else
            {
                foreach (XmlElement item in xml.ChildNodes[0].ChildNodes)
                {
                    if (item.Attributes.Count > 0 && item.Attributes[0].Value == VP_OLE_GUID)
                    {
                        LoadOLEFromNode(item.ChildNodes[0] as XmlElement);
                    }
                }
            }
            Log.Write(LogLevel.INFO, string.Format("Data loading successfully completed"));
        }

        private void LoadOLEFromNode(XmlElement xml)
        {
            if (xml == null) throw new Exception("Could not find model to load");
            xml = xml.GetChildNode("parameters");
            if (xml == null) throw new Exception("Could not find parameters node");
            LoadOLEFromParametersNode(xml);
        }
        public virtual void LoadOLEFromParametersNode(XmlElement xml)
        {
            Log.Write(LogLevel.ERROR, "Loading from VP_OLE format not implemented for model {0}", Name);
        }
        #endregion

        #endregion

        #region Calulations

        public int CurrStep;
        /*{
            get { return stepMethod == StepMethod.CalculateNext ? CurrentStep : CurrentStep - 1; }
        }*/

        protected decimal CurrTime
        {
            get { return stepMethod == StepMethod.CalculateNext ? CurrentTime : CurrentTime - Step; }
        }

        public override void BeforeCalculate()
        {
            base.BeforeCalculate();

            PrepareBeforeCalculations();
        }

        protected virtual void PrepareBeforeCalculations()
        {
            #region Override of Tania
            CurrentTime = Step;
            CurrentStep = 1;
            #endregion

            if (stepMethod == StepMethod.CalculateNext)
                CurrStep = 1;
            else
                CurrStep = 0;

            lastCompressed = 0;
            lastCompressedTime = 0;

            this.CalculatingState = ModelBase.Enums.CalculatingStates.NotStarted;
        }

        internal struct PerformanceCounters
        {
            public double caclulationTime;
            public decimal skippedTime;
        }
        private PerformanceCounters performance;

        public override void Calculate()
        {
            bool skippedLastStep = false;
            performance = new PerformanceCounters() { caclulationTime = 0, skippedTime = 0 };

            CalculatingState = ModelBase.Enums.CalculatingStates.InProcess;

            while (CurrentTime < ExperimentTime)
            {
                if (CalculatingState != ModelBase.Enums.CalculatingStates.InProcess) continue;

                RaiseEvent_CycleStarted(this);

#if FIRST_STEPS_CALCULATE_HASH
                if (CurrStep <3 && lastCompressed == 0) //first steps
                {                    
                    Log.Write(LogLevel.INFO, "Model [{0}] values hash at step {1} is [{2}]", this.Name, CurrStep, this.GetModelHash(CurrStep));                    
                }
#endif

                try
                {
                    DateTime calcStart = DateTime.Now;

                    if ((CompressionDensity.Value <= 1 || lastCompressed == CurrStep) &&
                        GetChangeLevel() < ChangeLevelToSkip)
                    {
                        PerformSkip();
                        RaiseEvent_CycleStarted(this);
                        skippedLastStep = true;
                    }

                    Cycle();

                    if (ChangeLevel != null)
                        ChangeLevel[CurrStep + 1] = GetChangeLevel() / (double)Step;

                    performance.caclulationTime += (DateTime.Now - calcStart).TotalMilliseconds;
                }
                catch (Exception ex)
                {
                    RaiseEvent_ExceptionOccurred(this, ex);
                    if (this.Thread != null)
                        this.Thread.Abort();
                }

                RaiseEvent_CycleCalculated(this);

                CurrStep++;

                CompressValues();

                CurrentTime += Step;
                CurrentStep++;

                if (skippedLastStep)
                {
#if DEBUG_LOGGING
                    Log.Write(LogLevel.INFO, "Change level after first Cycle after skip is [{0}]", GetChangeLevel());
#endif
                    skippedLastStep = false;
                }
            }

            CalculatingState = ModelBase.Enums.CalculatingStates.Finished;
            Log.Write(LogLevel.INFO, "Calcuation time of model [{0}] is: {1} ms", this.DisplayName, performance.caclulationTime);
            if (ChangeLevelToSkip.Value > 0)
                Log.Write(LogLevel.INFO, true, "Skipped time for model [{0}] is {1}s ({2}% of Experiment time)", this.DisplayName,
                    performance.skippedTime, 100 * performance.skippedTime / ExperimentTime);
        }

        #region Compression
        private int lastCompressed;
        private decimal lastCompressedTime;
        public void CompressValues()
        {
            if (CompressionDensity.Value <= 1) return;
            if (lastCompressed + CompressionDensity.Value > CurrStep) return;

            lastCompressed++;
            foreach (var item in GetValues())
            {
                if (item is LissovValue)
                    (item as LissovValue).Value[lastCompressed] = (item as LissovValue).Value[CurrStep];
            }
            lastCompressedTime = lastCompressed * Step * (decimal)CompressionDensity.Value;
            CurrStep = lastCompressed;
            CurrentStep = CurrentStep - (int)CompressionDensity.Value + 1;
        }

        public override double GetValueByNameAndTime(string valueName, decimal time)
        {
            if (time > CurrTime + Step)
                throw new Exception(string.Format("Cannot get value at time {0} - last calculated is {1}", time, CurrTime));
            Value v = GetValueByName(valueName);
            return GetValueByTime(v, time);
        }

        public override double GetValueByTime(Value v, decimal time)
        {
            if (v is IDynamicValue)
                return (v as IDynamicValue).GetValueByTime(time);

#if INTERPOLATE_VALUES
            try
            {
                LissovValue lv = v as LissovValue;
                if (CompressionDensity.Value > 1 && lv != null)
                {
                    int sn;
                    double steptime;
                    double valstep;
                    double step = (double)Step;
                    if (time < lastCompressedTime)
                    {
                        sn = (int)((double)time / (step * CompressionDensity.Value));
                        steptime = sn * (step * CompressionDensity.Value);
                        valstep = (step * CompressionDensity.Value);
                    }
                    else
                    {
                        //sn = lastCompressed + (int)((time - lastCompressedTime) / Step);

                        int noncomprSteps = (int)((time - lastCompressedTime) / Step);
                        sn = lastCompressed + noncomprSteps;
                        steptime = (double)lastCompressedTime + noncomprSteps * step;
                        valstep = step;
                    }

                    if (sn > lv.Value.Length - 1)
                    {
                        Log.Write(LogLevel.WARN, "Request to give value for step {0} from {1}", sn, v.Value.Length);
                        return lv.Value[lv.Value.Length - 1];
                    }

                    if (lv.NoInterpolation)
                        return lv.Value[sn];
                    else
                        return lv.Value[sn] + (lv.Value[sn + 1] - v.Value[sn]) * ((double)time - steptime) / valstep;
                }
                else
                    return v.Value[(int)Math.Floor(time / Step)];
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Exception [L001]: " + ex.Message);
                return 0;
            }
#else
            LissovValue lv = v as LissovValue;
            if (CompressionDensity.Value > 1 && lv != null)
            {
                int sn;
                if (time < lastCompressedTime)
                {
                    sn = (int)(time / (Step * CompressionDensity.Value));
                }
                else
                    sn = lastCompressed + (int)((time - lastCompressedTime) / Step);
                return lv.Value[sn];
            }
            else
                return v.Value[(int)Math.Floor(time / Step)];
#endif
        }

        #endregion

        #region Skips
        /// <summary>
        /// Measure of changes during last cycle in a scale [0..1]
        /// 0 is no changes at all, system is fully stable
        /// 1 is infinite change
        /// </summary>
        public virtual double GetChangeLevel()
        {
            return 1;
        }

        public List<decimal> UnbreakablePoints = new List<decimal>();

        public virtual void PerformSkip()
        {
            long fromStep = CurrStep;
            decimal fromTime = CurrentTime;

            decimal skipTime = (decimal)SkipStepTime.Value;
            foreach (decimal point in UnbreakablePoints)
            {
                if (point > CurrentTime && point < CurrentTime + skipTime)
                {
                    skipTime = point - CurrentTime;
                }
            }
            int bigStepC = (int)Math.Floor(skipTime / (Step * (decimal)CompressionDensity.Value));

            CopyValuesToSkip(bigStepC);

            CurrStep += bigStepC;
            CurrentStep += bigStepC;
            lastCompressed += bigStepC;
            lastCompressedTime += (bigStepC * Step * (decimal)CompressionDensity.Value);
            CurrentTime += (bigStepC * Step * (decimal)CompressionDensity.Value);

            FinalizeSkip(fromStep);

#if DEBUG_LOGGING
            Log.Write(LogLevel.INFO, "Skipped from time [{0}] to time [{1}] (steps {2} to {3})", Math.Round(fromTime, 6), Math.Round(CurrentTime, 6), fromStep, CurrStep);
#endif
            performance.skippedTime += CurrentTime - fromTime;
        }

        protected virtual void CopyValuesToSkip(int stepCount)
        {
            foreach (var item in GetValues())
            {
                double valDiff = item.Value[CurrStep] - item.Value[CurrStep - 1];
                for (int step = CurrStep; step < CurrStep + stepCount; step++)
                {
                    item.Value[step + 1] = item.Value[step] + valDiff;
                }
            }
        }

        protected virtual void FinalizeSkip(long skippedFrom)
        {
            //
        }
        #endregion

        #endregion

        #region Max Step Info
        public class MaxStepInfo
        {
            public string Text;
            public double Step;
        }

        public virtual MaxStepInfo GetMaxStepInfo()
        {
            return new MaxStepInfo()
            {
                Text = "Undefined",
                Step = Constants.INVALID_MAX_DOUBLE
            };
        }
        #endregion

        #region Validation
        public struct ValidateResult
        {
            public int WarnCount;
            public int ErrorCount;

            public static ValidateResult operator +(ValidateResult v1, ValidateResult v2)
            {
                return new ValidateResult()
                {
                    ErrorCount = v1.ErrorCount + v2.ErrorCount,
                    WarnCount = v1.WarnCount + v2.WarnCount
                };
            }
        }

        public virtual ValidateResult Validate()
        {
            return new ValidateResult()
            {
                WarnCount = 0,
                ErrorCount = 0
            };
        }

        public virtual double GetModelHash()
        {
            return GetModelHash(-1);
        }
        public virtual double GetModelHash(long step)
        {
            return GetModelHash(true, true, true, true, step);
        }
        public virtual double GetModelHash(bool includeParameters, bool includeInputs, bool includeOutputs, bool includeInternals, long step)
        {
            double hash = 0;
            if (includeParameters)
            {
                foreach (var item in GetParameters())
                {
                    hash += item.Value;
                    if (item.Value > Constants.INVALID_MAX_DOUBLE ||
                        item.Value < Constants.INVALID_DOUBLE)
                        Log.Write(LissovLog.LogLevel.ERROR, "Max double value of parameter: [{0}]", item.Name);
                }
            }
            foreach (var item in GetValues())
            {
                double d;
                if ((item.Type == Value.ValueType.Input && includeInputs)
                    || (item.Type == Value.ValueType.Output && includeOutputs)
                    || (item.Type == Value.ValueType.Internal && includeInternals))
                {
                    if (step < 0)
                        d = item.InitValue;
                    else
                        d = item.Value[step];
                    hash += d;
                    if (d > Constants.INVALID_MAX_DOUBLE ||
                        d < Constants.INVALID_DOUBLE)
                        Log.Write(LissovLog.LogLevel.ERROR, "Max double value of parameter: [{0}]", item.Name);
                }
            }
            return hash;
        }
        #endregion

        protected void ReadInputs(long fromStep, long lastBigStep)
        {
            decimal bigStep = Step * (decimal)CompressionDensity.Value;
            decimal time = lastBigStep * bigStep;
            foreach (var item in this.GetValues())
            {
                if (item.Type == Value.ValueType.Input)
                {
                    IModel model = Configuration.GetModelByName(item.LinkModelName);
                    while (model.GetLastCalculatedTime() < time - Step)
                    {
                        System.Threading.Thread.Sleep(0);
                    }

                    for (long step = fromStep; step < lastBigStep; step++)
                    {
                        item.Value[step] = model.GetValueByNameAndTime(item.LinkValueName,
                            step * bigStep);
                    }
                }
            }
        }
        
        public override XmlElement ResultToXml(XmlElement parentElement)
        {
            string prefix = DateTime.Now.ToString("[dd:MM:yyyy HH:mm] ");
            foreach (var value in Values)
                value.DisplayName = prefix + value.DisplayName;
            decimal step = Step;            
            Step = Step * (decimal)CompressionDensity.Value;

            var res = base.ResultToXml(parentElement);

            Step = step;
            foreach (var value in Values)
                value.DisplayName = value.DisplayName.Substring(prefix.Length);

            return res;
        }

        protected List<LissovValue> DynamicValues = new List<LissovValue>();
        public void AddDynamicValue(LissovValue value)
        {
            DynamicValues.Add(value);
            Values.Add(value);
        }
    }
}
