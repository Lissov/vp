#define DEBUG_LOGGING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelBase;
using LissovBase;
using System.IO;
using LissovBase.Functions;
using System.Xml;
using LissovLog;
using System.Reflection;

namespace Model_CVS
{
    public class CVSModel : LissovBase.LissovModelBase
    {
        private const double LEAK_PRECISION = 1E-12;

        internal NetConfiguration configuration;
        public NetConfiguration NetConfiguration { get { return configuration; } }

        #region Arrays
        internal double[] compartmentVertOffset;     // height of department in standing state
        internal double[] koeff_elast;               // elastic coefficients
        internal double[] muscularTone;              // Coefficient of muscular tone for active ortostatic stress
        #region Gormons
		internal double[] rigidity_zero;             // initial rigidity
        public double[] RigidityZero { get { return rigidity_zero; } }
        internal double[] gain_rigidity_adrenalin;   // Gain of rigidity to adrenalin
        internal double[] gain_unstressed_pext;      // Gain of unstressed to external pressure
        internal double[] gain_unstressed_adrenalin; // Gain of unstressed to adrenalin
        #endregion
        
        internal double[] unstressed_zero;           // initial unstressed volume
        internal double[] volume_zero;               // initial volume
        // resistance between comprtments "i" and "j" is calculated as:  res = resistanceOutput[i] * resistanceInput[j]
        internal double[] resistanceOutput_zero;     // initial output resistance
        internal double[] resistanceInput;           // input resistance koefficient
        internal PVFunction[] pv_functions;          // Pressure-volume relations
        public PVFunction[] PV_Functions { get { return pv_functions; } }
        private double[] ideal_flow;                // Ideal flow to calculate Ideal configuration

        private double[][] height;                  // height
        public double[][] pressure;                // pressure
        internal double[][] volume;                  // Volume
        private double[][] flow_in;                 // Flow into compartment
        private double[][] flow_out;                 // Flow from compartment
        internal double[][] ext_pressure;            // External Pressure
        private double[][] flow_external;           // Flow from Outside
        private double[][] rigidity;                // vascular rigidity
        private double[][] unstressed;              // unstressed volume
        private double[][] resistanceOutput;        // resistance (output part)
        private double[][] temperature_tone_koeff;  // Flow from Outside

        public double[][] department_volume;       // volumes of departments
        public double[][] department_unstressed_volume;       // unstressed volumes of departments

        //TODO: Optimize
        #region //PL: TODO: Optimize
        private double[][] pressure_full;           // pressure
        #endregion

        #region Internal
        private double[][] dpress;
        private double[][] dflow;
        #endregion

        private int currentCompCount = 0;
        private int currentDepCount = 0;
        internal void UpdateArraysLengthsToConfiguration(NetConfiguration config)
        {
            UpdateArraysLengthsToConfiguration(config, null);
        }
        internal void UpdateArraysLengthsToConfiguration(NetConfiguration config, List<int> toremove)
        {
            if (currentCompCount != config.CompartmentCount)
            {
                InvalidateParamsValues();

                int cc = config.CompartmentCount;
                ModelHelper.SetArrayLength<double>(ref compartmentVertOffset, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref koeff_elast, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref muscularTone, cc, toremove);

                #region Gormons
                ModelHelper.SetArrayLength<double>(ref rigidity_zero, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref gain_rigidity_adrenalin, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref gain_unstressed_pext, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref gain_unstressed_adrenalin, cc, toremove);
                #endregion

                ModelHelper.SetArrayLength<double>(ref ideal_flow, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref unstressed_zero, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref volume_zero, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref resistanceOutput_zero, cc, toremove);
                ModelHelper.SetArrayLength<double>(ref resistanceInput, cc, toremove);
                ModelHelper.SetArrayLength<PVFunction>(ref pv_functions, cc, toremove);
                for (int i = 0; i < cc; i++)
                {
                    if (pv_functions[i] == null)
                        pv_functions[i] = new PVFunction();
                }

                height = new double[cc][];
                pressure = new double[cc][];
                pressure_full = new double[cc][];
                volume = new double[cc][];
                flow_in = new double[cc][];
                flow_out = new double[cc][];
                ext_pressure = new double[cc][];
                flow_external = new double[cc][];
                temperature_tone_koeff = new double[cc][];

                rigidity = new double[cc][];
                unstressed = new double[cc][];
                resistanceOutput = new double[cc][];

                currentCompCount = config.CompartmentCount;
            }

            if (currentDepCount != config.DepartmentCount)
            {
                department_volume = new double[config.DepartmentCount][];
                department_unstressed_volume = new double[config.DepartmentCount][];
                currentDepCount = config.DepartmentCount;
            }
        }

        internal void PrepareArraysToCalculate()
        {
            int maxStep = ModelStepsCount + (int)StepDivider.Value + 1;
            foreach (var item in GetValues())
            {
                item.Value = new double[maxStep];
                item.Value[0] = item.InitValue;
            }

            dpress = new double[configuration.CompartmentCount][];
            dflow = new double[configuration.CompartmentCount][];
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                dpress[i] = new double[configuration.Links[i].Length];
                dflow[i] = new double[configuration.Links[i].Length];

                volume[i][0] = volume_zero[i];
                unstressed[i][0] = unstressed_zero[i];
                rigidity[i][0] = rigidity_zero[i];
                resistanceOutput[i][0] = resistanceOutput_zero[i];
            }
            CalculateDepartmentVolumes(0);
        }
        #endregion

        #region Parameters
        public ParameterString NetConfigurationFileName = new ParameterString("NetConfigurationFileName", "Net configuration filename");
        public Parameter StepDivider = new ModelBase.Parameter("StepDivider", "Step divider");
        public Parameter StepDividerSkipDelta = new ModelBase.Parameter("StepDividerSkipDelta", "Step divider skip delta");
        public ParameterBool RegulateBrainFlow = new ParameterBool("Regulate Brain Flow");
        public ParameterFunction BrainFlowRegulationFunction = new ParameterFunction("BrainFlowRegulationFunction", "BrainFlow Regulation Function",
            new BrainFlowRegulationFunction());
        public ParameterBool BrainFlowResistanceInertiality = new ParameterBool("BrainFlow regulated resistance inertiality");
        public ParameterBool BaroreceptionSensitivity = new ParameterBool("BaroreceptionSensitivity") { Value = 1, MinValue = 0, MaxValue = 1000 };
        #region Gormones
        public Parameter RigidityInertiality = new Parameter("RigidityInertiality", "Inertiality of rigidity");
        public Parameter UnstressedInertiality = new Parameter("UnstressedInertiality", "Inertiality of Unstressed volume");
        public Parameter RigidityMaxChange = new Parameter("RigidityMaximumChange", "Rigidity maximum change");
        public Parameter UnstressedMaxChange = new Parameter("UnstressedMaximumChange", "Unstressed maximum change");
        public Parameter AdrenalinNorm = new Parameter("AdrenalineNormal", "Normal concentration of adrenaline");
        #endregion

        public List<Parameter> parameters = null;
        public override List<Parameter> GetParameters()
        {
            if (parameters != null) return parameters;
            parameters = base.GetSingleModelParameters();
            parameters.Add(NetConfigurationFileName);
            parameters.Add(StepDivider);
            parameters.Add(StepDividerSkipDelta);
            parameters.Add(RegulateBrainFlow);
            parameters.Add(BrainFlowRegulationFunction);
            parameters.Add(BrainFlowResistanceInertiality);
            parameters.Add(BaroreceptionSensitivity);
            #region Gormons
            parameters.Add(RigidityInertiality);
            parameters.Add(UnstressedInertiality);
            parameters.Add(RigidityMaxChange);
            parameters.Add(UnstressedMaxChange);
            parameters.Add(AdrenalinNorm);
            #endregion
            
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                string cn = configuration.CompartmentNames[i];
                string cid = cn.ToIdentifier();
                parameters.Add(new ParameterArrayElement("Height_" + cid, "Height of " + cn, compartmentVertOffset, i));
                parameters.Add(new ParameterArrayElement("KElast_" + cid, "Elastic koefficient of " + cn, koeff_elast, i));
                parameters.Add(new ParameterArrayElement("MuscularTone_" + cid, "Muscular tone of " + cn, muscularTone, i));

                parameters.Add(new ParameterArrayElement("RigidityZero_" + cid, "Initial rigidity of " + cn, rigidity_zero, i));
                parameters.Add(new ParameterArrayElement("GainRigAdrenalin_" + cid, "Gain of rigidity to adrenalin of " + cn, gain_rigidity_adrenalin, i));
                parameters.Add(new ParameterArrayElement("GainUnstressedPExt_" + cid, "Gain of Unstressed volume to External pressure of " + cn, gain_unstressed_pext, i));
                parameters.Add(new ParameterArrayElement("GainUnstressedAdrenalin_" + cid, "Gain of Unstressed volume to adrenalin of " + cn, gain_unstressed_adrenalin, i));

                parameters.Add(new ParameterArrayElement("UnstressedZero_" + cid, "Initial Unstressed volume of " + cn, unstressed_zero, i));
                parameters.Add(new ParameterArrayElement("VolumeZero_" + cid, "Initial volume of " + cn, volume_zero, i));
                parameters.Add(new ParameterArrayElement("ResistanceOutputZero_" + cid, "Initial resistance (output part) of " + cn, resistanceOutput_zero, i));
                parameters.Add(new ParameterArrayElement("ResistanceInput_" + cid, "Input resistance koefficient of " + cn, resistanceInput, i));

                parameters.Add(new ParameterArrayElement("IdealFlow_" + cid, "Ideal flow through " + cn, ideal_flow, i));
            }

            foreach (var item in configuration.virtualCompartments)
                parameters.AddRange(item.GetParameters());

            return parameters;
        }
	    #endregion

        public void InvalidateParamsValues()
        {
            parameters = null;
            values = null;
        }

        #region Values
        #region Inputs
        public ModelBase.Value LeftHeartInFlow = new LissovValue("LeftHeartInFlow", "Left Heart Input Blood Flow", ModelBase.Value.ValueType.Input, Constants.Units.ml_per_second);
        public ModelBase.Value LeftHeartOutFlow = new LissovValue("LeftHeartOutFlow", "Left Heart Output Blood Flow", ModelBase.Value.ValueType.Input, Constants.Units.ml_per_second);
        public ModelBase.Value RightHeartInFlow = new LissovValue("RightHeartInFlow", "Right Heart Input Blood Flow", ModelBase.Value.ValueType.Input, Constants.Units.ml_per_second);
        public ModelBase.Value RightHeartOutFlow = new LissovValue("RightHeartOutFlow", "Right Heart Output Blood Flow", ModelBase.Value.ValueType.Input, Constants.Units.ml_per_second);

        public ModelBase.Value KidneyFlowOut = new LissovValue("KidneyFlowOut", "Kidney Flow Out", ModelBase.Value.ValueType.Input, Constants.Units.ml_per_second);
        public ModelBase.Value Gravity = new LissovValue("Gravity", "Gravity", ModelBase.Value.ValueType.Input);
        public ModelBase.Value RotationAngle = new LissovValue("RotationAngle", "Rotation Angle", ModelBase.Value.ValueType.Input, Constants.Units.degree);

        public ModelBase.Value PressExtPleural = new LissovValue("PressExtPleural", "Pleural External Pressure", ModelBase.Value.ValueType.Input, Constants.Units.mmHg);
        public ModelBase.Value PressExtAbdominal = new LissovValue("PressExtAbdominal", "Abdominal External Pressure", ModelBase.Value.ValueType.Input, Constants.Units.mmHg);
        public ModelBase.Value PressAtm = new LissovValue("PressAtm", "Athmosphere pressure", ModelBase.Value.ValueType.Input, Constants.Units.mmHg);
        public ModelBase.Value MuscularActivity = new LissovValue("MuscularActivity", "Muscular Activity", ModelBase.Value.ValueType.Input, Constants.Units.unit);

        public ModelBase.Value Adrenalin = new LissovValue("Adrenalin", "Adrenalin concentration", ModelBase.Value.ValueType.Input, Constants.Units.unit);
        /*public ModelBase.Value Noradrenalin = new LissovValue("Noradrenalin", "Noradrenalin concentration", ModelBase.Value.ValueType.Input, Constants.Units.unit);
        public ModelBase.Value Acetylcholine = new LissovValue("Acetylcholine", "Acetylcholine concentration", ModelBase.Value.ValueType.Input, Constants.Units.unit);*/
        #endregion

        #region Internal
        public ModelBase.Value AdrenalinEff = new LissovValue("AdrenalinEff", "Adrenalin effective concentration", ModelBase.Value.ValueType.Internal, Constants.Units.unit);
        /*public ModelBase.Value NoradrenalinEff = new LissovValue("NoradrenalinEff", "Noradrenalin effective concentration", ModelBase.Value.ValueType.Internal, Constants.Units.unit);
        public ModelBase.Value AcetylcholineEff = new LissovValue("AcetylcholineEff", "Acetylcholine effective concentration", ModelBase.Value.ValueType.Internal, Constants.Units.unit);*/
        #endregion

        #region Outputs
        public ModelBase.Value PeripherialResistance = new LissovValue("PeripherialResistance", "Peripherial Resistance", ModelBase.Value.ValueType.Output, Constants.Units.PRU);
        public ModelBase.Value ExternalFlow = new LissovValue("TotalExternalFlow", "Total external flow", Value.ValueType.Output, Constants.Units.ml_per_second);
        public ModelBase.Value HeartSumFlow = new LissovValue("HeartSumFlow", "Summary flow from heart", Value.ValueType.Output, Constants.Units.ml_per_second);
        #endregion

        private List<Value> values = null;
        public override List<Value> GetValues()
        {
            if (values != null) return values;
            //Inputs
            values = GetSingleModelValues();
            values.Add(LeftHeartInFlow);
            values.Add(LeftHeartOutFlow);
            values.Add(RightHeartInFlow);
            values.Add(RightHeartOutFlow);
            values.Add(KidneyFlowOut);
            values.Add(Gravity);
            values.Add(RotationAngle);
            values.Add(PressExtPleural);
            values.Add(PressExtAbdominal);
            values.Add(PressAtm);
            values.Add(MuscularActivity);
            values.Add(Adrenalin);
            /*values.Add(Noradrenalin);
            values.Add(Acetylcholine);*/
            //Internal
            values.Add(AdrenalinEff);
            /*values.Add(AcetylcholineEff);
            values.Add(NoradrenalinEff);*/

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                string cn = configuration.CompartmentNames[i];
                string cid = cn.ToIdentifier();

                values.Add(new ValueArrayElement("ExternalFlowInside" + cid, "External flow to " + cn, Value.ValueType.Input, flow_external, i, cn, true, Constants.Units.ml_per_second)
                    {
                        LinkModelName = "Loads",
                        LinkValueName = "Zero"
                    });
                values.Add(new ValueArrayElement("TemperatureToneKoeff" + cid, "Temperature vascular tone multiplier of " + cn, Value.ValueType.Input, temperature_tone_koeff, i, cn, true, Constants.Units.unit)
                {
                    LinkModelName = "Loads",
                    LinkValueName = "One"
                });

                values.Add(new ValueArrayElement("Height" + cid, "Height of " + cn, Value.ValueType.Internal, height, i, cn, false, Constants.Units.meter));
                values.Add(new ValueArrayElement("ExtPressure" + cid, "External Pressure in " + cn, Value.ValueType.Internal, ext_pressure, i, cn, true, Constants.Units.mmHg));
                values.Add(new ValueArrayElement("Pressure" + cid, "Pressure in " + cn, Value.ValueType.Output, pressure, i, cn, false, Constants.Units.mmHg));
                values.Add(new ValueArrayElement("Volume" + cid, "Volume of " + cn, Value.ValueType.Output, volume, i, cn, false, Constants.Units.ml));
                values.Add(new ValueArrayElement("FlowIn" + cid, "Flow inside to " + cn, Value.ValueType.Internal, flow_in, i, cn, false, Constants.Units.ml_per_second));
                values.Add(new ValueArrayElement("FlowOut" + cid, "Flow outside from " + cn, Value.ValueType.Output, flow_out, i, cn, false, Constants.Units.ml_per_second));

                values.Add(new ValueArrayElement("Rigidity" + cid, "Rigidity of " + cn, Value.ValueType.Internal, rigidity, i, cn, true, Constants.Units.mmHg_per_ml));
                values.Add(new ValueArrayElement("Unstressed" + cid, "Unstressed volume of " + cn, Value.ValueType.Internal, unstressed, i, cn, false, Constants.Units.ml));
                values.Add(new ValueArrayElement("ResistanceOutput" + cid, "Resistance (output part) of " + cn, Value.ValueType.Internal, resistanceOutput, i, cn, false, Constants.Units.resist));

                values.Add(new ValueArrayElement("PressureFull" + cid, "Full pressure in " + cn, Value.ValueType.Output, pressure_full, i, cn, false, Constants.Units.mmHg));
            }

            foreach (var item in configuration.virtualCompartments)
                values.AddRange(item.GetValues());

            for (int i = 0; i < configuration.DepartmentCount; i++)
            {
                string dn = configuration.DepartmentNames[i];
                string did = dn.ToIdentifier();
                values.Add(new ValueArrayElement("TotalVolume" + did, "Total volume of " + dn, Value.ValueType.Output, department_volume, i, Constants.Units.ml));
                values.Add(new ValueArrayElement("TotalUnstressed" + did, "Total unstressed volume of " + dn, Value.ValueType.Output, department_unstressed_volume, i, Constants.Units.ml));
            }

            //add outputs
            values.Add(PeripherialResistance);
            values.Add(ExternalFlow);
            values.Add(HeartSumFlow);

            return values;
        } 
        #endregion

        #region Construction
        public CVSModel()
            : base()
        {
            Log.Write(LogLevel.INFO, "Model created");

            Name = "CVSModel";
            DisplayName = "Vascular System";

            IsLoaded = true; // avoid parameters gathering

            VP_OLE_GUID = "{17E617F7-9850-4923-95CF-963AA3BE4635}";

            configuration = new NetConfiguration() { Name = "Empty", Comments = string.Empty, CompartmentCount = 0 };
        } 
        #endregion

        #region Load&Save
        internal string FullConfigFileName
        {
            get
            {
                string fname = !string.IsNullOrEmpty(NetConfigurationFileName.StringValue) ? NetConfigurationFileName.StringValue : "DefaultVsConfig.xml";
                return Path.Combine(Path.GetDirectoryName(this.Configuration.FileName), fname); 
            }
        }
        internal string FullConfigFileDir
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath) + "\\Data"; }
        }

        public override object FromXml(XmlElement currentElement)
        {
            NetConfigurationFileName.StringValue = (string)currentElement.GetChildNode("Parameters").GetChildNode("NetConfigurationFileName").ChildNodes[0].Attributes["StringValue"].Value;
            if (File.Exists(FullConfigFileName))
                configuration = NetConfiguration.LoadFromFile(FullConfigFileName, this);
            else
                throw new Exception(string.Format("Configuration file [{0}] not found", FullConfigFileName));

            InvalidateParamsValues();
            // Load basic...
            base.Values = GetValues();
            base.Parameters = GetParameters();
            base.FromXml(currentElement);

            //Load PV
            for (int i = 0; i < configuration.DepartmentCount; i++) pv_functions[i] = null;
            if (currentElement.GetChildNode("PV_Parameters") != null)
            {
                foreach (XmlElement pv_node in currentElement.GetChildNode("PV_Parameters").ChildNodes)
                {
                    string compartment_id = pv_node.Attributes["compartment_id"].Value;
                    int cn = configuration.GetCompartmentNumByID(compartment_id);
                    if (cn >= 0)
                    {
                        pv_functions[cn] = new PVFunction();
                        pv_functions[cn].FromXml(pv_node);
                    }
                }
            }
            for (int i = 0; i < configuration.DepartmentCount; i++)
                if (pv_functions[i] == null)
                {
                    pv_functions[i] = new PVFunction();
                    Log.Write(LogLevel.ERROR, "PV_Function not loaded for {0}", configuration.CompartmentNames[i]);
                }

            UpdateVisuals();

            return this;
        }

        public override XmlElement ToXml(System.Xml.XmlElement parentElement)
        {
            configuration.SaveToFile(FullConfigFileName);

            base.Values = GetValues();
            base.Parameters = GetParameters();
            XmlElement xml_el = base.ToXml(parentElement);

            XmlElement pv_pars = xml_el.OwnerDocument.CreateElement("PV_Parameters");
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                XmlElement xe = pv_functions[i].ToXml(pv_pars, "Function_"+i);
                SetAttribute(xe, "compartment_id", configuration.CompartmentIDs[i]);
            }
            xml_el.AppendChild(pv_pars);

            return xml_el;
        }

        public override void LoadOLEFromParametersNode(XmlElement xml)
        {
            Log.Write(LogLevel.INFO, "Parameters count to load: " + xml.ChildNodes.Count.ToString());
            int errors = 0;
            foreach (XmlElement item in xml.ChildNodes)
            {
                string name = item.Attributes[0].Value;
                string value = item.Attributes[1].Value;

                if (name == "Step") { Step = decimal.Parse(value); continue; }
                if (name == "Unstressed inertiality") { UnstressedInertiality.Value = double.Parse(value); continue; }
                if (name == "Rigidity inertiality") { RigidityInertiality.Value = double.Parse(value); continue; }

                if (name == "Adrenaline initial") { Adrenalin.InitValue = double.Parse(value); continue; }
                /*if (name == "Noradrenaline initial") { Noradrenalin.InitValue = double.Parse(value); continue; }
                if (name == "Acetilholine initial") { Acetylcholine.InitValue = double.Parse(value); continue; }*/

                if (name == "Save step") { StepDivider.Value = double.Parse(value); continue; }                

                //Compartment parameters
                if (name.Contains("->"))
                {
                    string comp_name = name.Substring(0, name.IndexOf("->") - 1);
                    int cn = configuration.GetCompartmentNumByName(comp_name);
                    if (cn == -1)
                    {
                        Log.Write(LogLevel.WARN, string.Format("Compartment [{0}], does not exists in configuration. Parameter [{1}] (value [{2}]) can not be loaded.", comp_name, name, value));
                        errors++;
                        continue;
                    }

                    string par_name = name.Substring(name.IndexOf("->") + 3);
                    switch (par_name)
                    {
                        case "Volume": volume_zero[cn] = double.Parse(value); continue;
                        case "Unstressed": unstressed_zero[cn] = double.Parse(value); continue;
                        case "Rigidity": rigidity_zero[cn] = double.Parse(value); continue;
                        case "Out-resistance": resistanceOutput_zero[cn] = double.Parse(value); continue;
                        case "In-resistance koeff": resistanceInput[cn] = double.Parse(value); continue;
                        case "Position": compartmentVertOffset[cn] = double.Parse(value); continue;
                        case "Unstr/Adr": gain_unstressed_adrenalin[cn] = double.Parse(value); continue;
                        case "Rig/Adr": gain_rigidity_adrenalin[cn] = double.Parse(value); continue;
                        case "Elastic koeff.": koeff_elast[cn] = double.Parse(value); continue;
                        case "Unstr / PExt": gain_unstressed_pext[cn] = double.Parse(value); continue;
                        case "Ideal flow": ideal_flow[cn] = double.Parse(value); continue;
                            //PV
                        case "PV - linear V": pv_functions[cn].linearV = double.Parse(value); continue;
                        case "PV - linear P": pv_functions[cn].linearP = double.Parse(value); continue;
                        case "PV - level 1": pv_functions[cn].level1 = double.Parse(value); continue;
                        case "PV - level 2": pv_functions[cn].level2 = double.Parse(value); continue;
                        case "PV - power 1": pv_functions[cn].power1 = double.Parse(value); continue;
                        case "PV - power 2": pv_functions[cn].power2 = double.Parse(value); continue;
                        default:
                            Log.Write(LogLevel.WARN, string.Format("Parameter [{0}], does not exists in model. Parameter [{1}] (value [{2}]) can not be loaded.", par_name, name, value));
                            errors++;
                            continue;
                    }                    
                }
                Log.Write(LogLevel.WARN, string.Format("Ignored unknown parameter [{0}], value [{1}]", name, value));
                errors++;
            }

            for (int i = 0; i < configuration.DepartmentCount; i++)
            {
                pv_functions[i].Update();
            }
            int all = xml.ChildNodes.Count;
            Log.Write(LogLevel.INFO, "Successfully loaded: {0} of {1} ({2} %)", (all - errors).ToString(), all.ToString(), (100 * (all - errors) / all).ToString());
        }
        #endregion

        #region Calculations

        internal struct PerformanceCounters
        {
            public double onestepWorkTime;
            public double backReturnTime;
            public double otherTime;
        }
        internal PerformanceCounters performance;
        public override void  Calculate()
        {
            performance = new PerformanceCounters() { onestepWorkTime = 0, backReturnTime = 0, otherTime = 0};

 	        base.Calculate();

            #if DEBUG_LOGGING
            string mask = "CVS Model calculation time for '{0}' is: {1} ms";
            Log.Write(LogLevel.INFO, mask, "OneStep", performance.onestepWorkTime);
            Log.Write(LogLevel.INFO, mask, "Step divider Return", performance.backReturnTime);
            Log.Write(LogLevel.INFO, mask, "Other activities", performance.otherTime);
            #endif
        }

        public override void BeforeCalculate()
        {
            PrepareBeforeCalculations();

            PrepareArraysToCalculate();

            #region Initial Values
            CalculatePressuresExternal(0, 0);
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                pressure[i][0] = rigidity[i][0]
                                    * pv_functions[i].getValue(volume[i][0] - unstressed[i][0]);

                pressure_full[i][0] = pressure[i][0] + ext_pressure[i][0];
                flow_in[i][0] = ideal_flow[i];
                flow_out[i][0] = ideal_flow[i];
            }
            #endregion

            foreach (var item in configuration.virtualCompartments)
            {
                item.Calculate(0, 0, (double)Step);
            }
        }

        public override void Cycle()
        {
            DateTime othertm = DateTime.Now;
            CalculateHeights(CurrStep);
            CalculatePressuresExternal(CurrStep, CurrStep);
            performance.otherTime += (DateTime.Now - othertm).TotalMilliseconds;

            decimal stepDivider = (decimal)StepDivider.Value;
            if (GetChangeLevel() < StepDividerSkipDelta.Value)
                stepDivider = 1;
            int cs = CurrStep;
            decimal calcstep = Step / stepDivider;

            while (cs < CurrStep + stepDivider)
            {
                DateTime onestepst = DateTime.Now;
                CalculateVascularTone(cs + 1, (double)calcstep, CurrStep);

                OneStep(cs, (double)calcstep, CurrStep);
                performance.onestepWorkTime += (DateTime.Now - onestepst).TotalMilliseconds;

                cs++;
            }

            DateTime brtm = DateTime.Now;
            #region Return calculated back in arrays (directly, not through Values - to speed up)
            if (stepDivider > 1)
            {
                //returtn them back
                AdrenalinEff.Value[CurrStep + 1] = AdrenalinEff.Value[cs];
                /*NoradrenalinEff.Value[CurrStep + 1] = NoradrenalinEff.Value[cs];
                AcetylcholineEff.Value[CurrStep + 1] = AcetylcholineEff.Value[cs];*/

                for (int i = 0; i < configuration.CompartmentCount; i++)
                {
                    rigidity[i][CurrStep + 1] = rigidity[i][cs];
                    unstressed[i][CurrStep + 1] = unstressed[i][cs];
                    resistanceOutput[i][CurrStep + 1] = resistanceOutput[i][cs];

                    pressure[i][CurrStep + 1] = pressure[i][cs];
                    flow_in[i][CurrStep + 1] = flow_in[i][cs];
                    flow_out[i][CurrStep + 1] = flow_out[i][cs];
                    volume[i][CurrStep + 1] = volume[i][cs];
                }
            }
            cs = CurrStep;
            #endregion
            performance.backReturnTime += (DateTime.Now - brtm).TotalMilliseconds;

            othertm = DateTime.Now;
            volume[configuration.Kidney][CurrStep + 1] = volume[configuration.Kidney][CurrStep + 1]
                - KidneyFlowOut.Value[CurrStep] * (double)Step;

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                pressure_full[i][CurrStep + 1] = pressure[i][CurrStep + 1] + ext_pressure[i][CurrStep]
                    - muscularTone[i] * MuscularActivity.Value[CurrStep]; ;

                if (volume[i][CurrStep + 1] < 0.001)
                    Log.Write(LogLevel.ERROR, "Volume < 0.001 in [{0}] (equal {1}) at step {2}", configuration.CompartmentNames[i], volume[i][CurrStep + 1].ToString(), cs.ToString());

                if (volume[i][CurrStep + 1] > 5000)
                    Log.Write(LogLevel.ERROR, "Volume > 5000 in [{0}] (equal {1}) at step {2}", configuration.CompartmentNames[i], volume[i][CurrStep + 1].ToString(), cs.ToString());
            }

            #region Averages and Totals
            /* //PL: No averages now
                VolumeM[i][cS+1]    :=  VolumeM[i][cS] + Vol[i][cS+1]/AverageStep;
                InFlowM[i][cS+1]    :=  InFlowM[i][cS] + FlowIn[i][cS+1]/AverageStep;
                if cS+LastSwapped >= AverageStep then
                begin
                    PressureM[i][cS+1]  :=  PressureM[i][cS] + Pressure[i][cS+1]/AverageStep;
                
                    meanStep := cS - round(AverageStep);
                    if (meanStep < 0) then meanStep := SwapStep + meanStep - 1;

                    PressureM[i][cS+1]  :=  PressureM[i][cS+1] - Pressure[i][meanStep]/AverageStep;
                    VolumeM[i][cS+1]    :=  VolumeM[i][cS+1] - Vol[i][meanStep]/AverageStep;
                    InFlowM[i][cS+1]    :=  InFlowM[i][cS+1] - FlowIn[i][meanStep]/AverageStep;
                end
                else begin
                    PressureMTemp[i][cS+1]  :=  PressureMTemp[i][cS] + Pressure[i][cS+1]/AverageStep;
                    PressureM[i][cS+1] := PressureM[i][0];
                    if cS = AverageStep - 1
                    then PressureM[i][cS+1] := PressureMTemp[i][cS+1];
                end;
                */

            ExternalFlow.Value[CurrStep] = 0;
            for (int j = 0; j < configuration.CompartmentCount; j++)
                ExternalFlow.Value[CurrStep] += flow_external[j][CurrStep];
            ExternalFlow.Value[CurrStep] -= KidneyFlowOut.Value[CurrStep];
            HeartSumFlow.Value[CurrStep + 1] = HeartSumFlow.Value[CurrStep] +
                (RightHeartOutFlow.Value[CurrStep] - RightHeartInFlow.Value[CurrStep]
                + LeftHeartOutFlow.Value[CurrStep] - LeftHeartInFlow.Value[CurrStep]) * (double)Step;
            #endregion

            foreach (var item in configuration.virtualCompartments)
            {
                item.Calculate(CurrStep + 1, CurrStep, (double)Step);
            }

            if (flow_in[configuration.LeftAfterHeart][CurrStep + 1] > 0.001)
                PeripherialResistance.Value[CurrStep + 1] =
                    (pressure[configuration.LeftAfterHeart][CurrStep + 1] - pressure[configuration.RightBeforeHeart][CurrStep + 1])
                    / flow_in[configuration.LeftAfterHeart][CurrStep + 1];
            else
                PeripherialResistance.Value[CurrStep + 1] = -1;

            //PL: No analitical PR now

            //VirtPulmCapillarPressure.Value[CurrStep +1] = pressure[

            CalculateDepartmentVolumes(CurrStep + 1);
            performance.otherTime += (DateTime.Now - othertm).TotalMilliseconds;
        }

        private void OneStep(int cs, double step, int base_input_step)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                pressure[i][cs + 1] = rigidity[i][cs + 1]
                                    * pv_functions[i].getValue(volume[i][cs] - unstressed[i][cs + 1]);
/*                if (pressure[i][cs + 1] != pressure[i][cs])
                {
                }*/
            }

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                for (int j = 0; j < configuration.Links[i].Length; j++)
                {
                    int next = configuration.Links[i][j];
                    dpress[i][j] = pressure[i][cs + 1] - pressure[next][cs + 1]
                        + (height[i][base_input_step + 1] - height[next][base_input_step + 1]) * Constants.GRAVITY_TO_MMHG * Gravity.Value[base_input_step]
                        + ext_pressure[i][base_input_step] - ext_pressure[next][base_input_step];

                    dflow[i][j] = dpress[i][j] / (resistanceOutput[i][cs+1] * resistanceInput[next]);
                }
            }

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                flow_in[i][cs+1] = 0;
                for (int prev = 0; prev < configuration.CompartmentCount; prev++)
                {
                    for (int j = 0; j < configuration.Links[prev].Length; j++)
                    {
                        if (configuration.Links[prev][j] == i)
                            flow_in[i][cs+1] += dflow[prev][j];
                    }
                }

                flow_out[i][cs+1] = 0;
                for (int j = 0; j < configuration.Links[i].Length; j++)
                {
                    flow_out[i][cs+1] += dflow[i][j];
                }
            }

            double fli = 0;
            for (int i = 0; i < configuration.CompartmentCount; i++)
                fli += flow_in[i][cs + 1];
            double flo = 0;
            for (int i = 0; i < configuration.CompartmentCount; i++)
                flo += flow_out[i][cs + 1];
            if (Math.Abs(fli - flo) > LEAK_PRECISION)
                Log.Write(LogLevel.ERROR, "Leak: {0}", fli - flo);

            flow_out[configuration.RightBeforeHeart][cs+1] = RightHeartInFlow.Value[base_input_step];
            flow_in[configuration.RightAfterHeart][cs+1] = RightHeartOutFlow.Value[base_input_step];
            flow_out[configuration.LeftBeforeHeart][cs+1] = LeftHeartInFlow.Value[base_input_step];
            flow_in[configuration.LeftAfterHeart][cs+1] = LeftHeartOutFlow.Value[base_input_step];

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                volume[i][cs+1] = volume[i][cs] +
                    (flow_in[i][cs+1] - flow_out[i][cs+1] + flow_external[i][base_input_step]) * step;
            }
        }

        private void CalculateHeights(int cs)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
			{
                height[i][cs + 1] = compartmentVertOffset[i] * Math.Sin(RotationAngle.Value[cs].ToRadians());
			}
        }

        private void CalculatePressuresExternal(int to_step, int base_step)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                ext_pressure[i][to_step] = koeff_elast[i] * PressAtm.Value[base_step]
                     + muscularTone[i] * MuscularActivity.Value[base_step];
            }
        }

        /// <summary>
        /// Calculates Rigidity, Unstressed and Resistance of vascular compartments
        /// </summary>
        private void CalculateVascularTone(int cs, double step, int base_input_step)
        {
            AdrenalinEff.Value[cs] = AdrenalinNorm.Value +
                (1d - Math.Sqrt(1d - Adrenalin.Value[base_input_step]) - AdrenalinNorm.Value) * BaroreceptionSensitivity.Value;
            /*NoradrenalinEff.Value[cs] = Noradrenalin.Value[0] +
                (Noradrenalin.Value[base_input_step] - Noradrenalin.Value[0]) * BaroreceptionSensitivity.Value;
            AcetylcholineEff.Value[cs] = Acetylcholine.Value[0] +
                (Acetylcholine.Value[base_input_step] - Acetylcholine.Value[0]) * BaroreceptionSensitivity.Value;*/
            /*            double baro = Baroreception.Value[0] + 
                                        (Baroreception.Value[base_input_step] - Baroreception.Value[0]) * BaroreceptionSensitivity.Value;

                                    Adrenalin.Value[cs] = Adrenalin.Value[cs-1] + step *
                                        (GainAdrenalinBarorec.Value * (1 - baro)
                                            - DestructionAdrenalin.Value * Adrenalin.Value[cs-1]);

                                    Noradrenalin.Value[cs] = Noradrenalin.Value[cs - 1] + step *
                                        (GainNoredrenalinBarorec.Value * baro
                                            - DestructionNoradrenalin.Value * Noradrenalin.Value[cs - 1]);

                                    Acetylcholine.Value[cs] = Acetylcholine.Value[cs-1] + step *
                                        (GainAcetylcholineBarorec.Value * baro
                                            - DestructionAcetylcholine.Value * Acetylcholine.Value[cs-1]);*/

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                rigidity[i][cs] = rigidity[i][cs - 1] +
                        (rigidity_zero[i] * (1 + gain_rigidity_adrenalin[i] * (AdrenalinEff.Value[cs] - 0.5))
                            - rigidity[i][cs - 1])
                            * temperature_tone_koeff[i][cs - 1]
                            * RigidityInertiality.Value * step;

                unstressed[i][cs] = unstressed[i][cs-1] +
                    ((1 - PressAtm.Value[cs] * gain_unstressed_pext[i])
                    * unstressed_zero[i]
                    * (1 + gain_unstressed_adrenalin[i] * (AdrenalinEff.Value[cs] - 0.5))
                    - unstressed[i][cs-1]) * UnstressedInertiality.Value * step;

                resistanceOutput[i][cs ] = resistanceOutput_zero[i] * (volume_zero[i] / volume[i][cs-1]);

                #region Boundary checks
                if (rigidity[i][cs] < 0.01)
                    Log.Write(LogLevel.ERROR, string.Format("Rigidity of {0} is {1} (time = {2})", configuration.CompartmentNames[i], rigidity[i][cs], CurrentTime));
                if (rigidity[i][cs] > RigidityMaxChange.Value * rigidity_zero[i])
                {
                    //Log.Write(LogLevel.WARN, string.Format("Rigidity of {0} is {1} > {3}*{2} - changed to {3} (time = {4})", configuration.CompartmentNames[i], rigidity[i][cs], rigidity_zero[i], 3 * rigidity_zero[i], CurrentTime, RigidityMaxChange.Value));
                    rigidity[i][cs] = RigidityMaxChange.Value * rigidity[i][cs];
                }

                if (unstressed[i][cs] < 0.001)
                    Log.Write(LogLevel.ERROR, string.Format("Unstressed of {0} is {1} (time = {2})", configuration.CompartmentNames[i], unstressed[i][cs], CurrentTime));
                if (unstressed[i][cs] < unstressed_zero[i] / UnstressedMaxChange.Value)
                {
                    //Log.Write(LogLevel.WARN, string.Format("Unstressed of {0} is {1} < {2} - changed to {3} (time = {4})", configuration.CompartmentNames[i], unstressed[i][cs], unstressed_zero[i], unstressed_zero[i] / UnstressedMaxChange.Value, CurrentTime));
                    unstressed[i][cs] = unstressed_zero[i] / UnstressedMaxChange.Value;
                }

                if (resistanceOutput[i][cs] < 0.25 * resistanceOutput_zero[i])
                {
                    Log.Write(LogLevel.WARN, string.Format("Output resistance of {0} is {1} < 0.25*{2} - changed to {3} (time = {4})", configuration.CompartmentNames[i], resistanceOutput[i][cs], resistanceOutput_zero[i], 0.25 * resistanceOutput_zero[i], CurrentTime));
                    resistanceOutput[i][cs] = 0.25 * resistanceOutput_zero[i];
                }
                #endregion
            }

            if (RegulateBrainFlow)
                CalculateBrainResistance(cs - 1, step);
        }

        private void CalculateBrainResistance(int cs, double step)
        {
            int rec = configuration.Brain;
            int active = configuration.BeforeBrain;
            double p = pressure[rec][cs];
            double r0 = resistanceOutput[active][cs];
            double r = BrainFlowRegulationFunction.InnerFunction.getValue(p);

            resistanceOutput[active][cs + 1] = resistanceOutput[active][cs]
                + BrainFlowResistanceInertiality.Value * (r * resistanceOutput_zero[active] - r0) * step;
        }

        private void CalculateDepartmentVolumes(int stepn)
        {
            for (int d = 0; d < configuration.DepartmentCount; d++)
            {
                department_volume[d][stepn] = 0;
                foreach (int i in configuration.Departments[d])
                    department_volume[d][stepn] += volume[i][stepn];

                department_unstressed_volume[d][stepn] = 0;
                foreach (int i in configuration.Departments[d])
                    department_unstressed_volume[d][stepn] += unstressed[i][stepn];
            }
        }

        const int ChangeStep = 6;
        public override double GetChangeLevel()
        {
            double volkoeff = 0.02;

            if (CurrStep < ChangeStep + 1) return 1;

            double chl = 0;
            double d1 = pressure[configuration.LeftAfterHeart][CurrStep - ChangeStep] -
                pressure[configuration.LeftAfterHeart][CurrStep - ChangeStep - 1];
            double dv1 = volkoeff * (department_volume[0][CurrStep - ChangeStep] - 
                department_volume[0][CurrStep - ChangeStep - 1]);

            for (int i = -ChangeStep; i <= 0; i++)
            {
                double d2 = pressure[configuration.LeftAfterHeart][CurrStep + i] -
                    pressure[configuration.LeftAfterHeart][CurrStep + i - 1];
                if (chl < Math.Abs(d2 - d1))
                    chl = Math.Abs(d2 - d1);

                d2 = volkoeff * (department_volume[0][CurrStep + i] -
                    department_volume[0][CurrStep + 1 - 1]);
                if (chl < Math.Abs(d2 - dv1))
                    chl = Math.Abs(d2 - dv1);
            }
            
            return chl;
        }

        //Simple helper to all external methods
        public double GetInitialFlow(int cFrom, int cTo)
        {
            if (!configuration.Links[cFrom].Contains(cTo))
                return Constants.INVALID_DOUBLE;

            double p1 = rigidity_zero[cFrom] *
                pv_functions[cFrom].getValue(volume_zero[cFrom] - unstressed_zero[cFrom]);
            double p2 = rigidity_zero[cTo] *
                pv_functions[cTo].getValue(volume_zero[cTo] - unstressed_zero[cTo]);


            return (p1 - p2) / (resistanceOutput_zero[cFrom] * resistanceInput[cTo]);
        }
        #endregion

        #region Visuals
        private SetupControl modelControl = null;
		public override UserControl GetControl()
        {
            if (modelControl == null)
                modelControl = new SetupControl(this);
            return modelControl;
        }

        public void UpdateVisuals()
        {
            if (modelControl != null)
                modelControl.Update();
        }
	    #endregion

        #region Helpers
        public override MaxStepInfo GetMaxStepInfo()
        {
            try
            {
                double stepmax = Constants.INVALID_MAX_DOUBLE;
                int criticalFrom = 0;
                int criticalTo = 0;
                for (int i = 0; i < configuration.CompartmentCount; i++)
                {
                    for (int j = 0; j < configuration.Links[i].Length; j++)
                    {
                        int jdep = configuration.Links[i][j];
                        double res = resistanceOutput_zero[i] * resistanceInput[jdep];
                        double k = rigidity_zero[i] / pv_functions[i].linearV;
                        double step = res / (2 * k);

                        if (step < stepmax)
                        {
                            stepmax = step;
                            criticalFrom = i;
                            criticalTo = jdep;
                        }
                    }
                }
                return new MaxStepInfo()
                {
                    Text = string.Format("{0} from [{1}] to [{2}]", stepmax,
                        configuration.CompartmentNames[criticalFrom],
                        configuration.CompartmentNames[criticalTo]),
                    Step = stepmax
                };
            }
            catch (Exception)
            {
                return new MaxStepInfo() { Text = "Undefined", Step = int.MinValue };
            }
        }
        #endregion

        public override LissovModelBase.ValidateResult Validate()
        {
            LissovModelBase.ValidateResult result = base.Validate();
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                if (pv_functions[i].linearV != volume_zero[i] - unstressed_zero[i])
                {
                    Log.Write(LogLevel.WARN, "Linear part of PV dependency for [{0}] is [{1}], not equal to V-U [{2}]", configuration.CompartmentNames[i],
                        pv_functions[i].linearV, volume_zero[i] - unstressed_zero[i]);
                    result.WarnCount++;
                }
            }
            return result;
        }

        protected override void CopyValuesToSkip(int stepCount)
        {
            foreach (var item in GetValues())
            {
                if (item.Type == Value.ValueType.Input)
                    continue;
                for (int step = CurrStep; step < CurrStep + stepCount; step++)
                    item.Value[step + 1] = item.Value[step];
            }
            for (int i = CurrStep; i < CurrStep + stepCount; i++)
            {
                for (int c = 0; c < configuration.CompartmentCount; c++)
                {
                    pressure_full[c][i + 1] = pressure_full[c][i] +
                        (pressure_full[c][CurrStep] - pressure_full[c][CurrStep - 1]);
                }
            }
        }

        protected override void FinalizeSkip(long skippedFrom)
        {
            ReadInputs(skippedFrom, CurrStep);

            decimal bStep = Step * (decimal)CompressionDensity.Value;

            double TotalFlowIn = 0;
            double oneStepFlowIn;
            for (int i = (int)skippedFrom; i < CurrStep; i++)
            {
                oneStepFlowIn = 0;
                for (int c = 0; c < configuration.CompartmentCount; c++)
                {
                    oneStepFlowIn += flow_external[c][i];
                }
                foreach (var comp in configuration.virtualCompartments)
                    oneStepFlowIn += comp.inputFlow[(int)i];

                oneStepFlowIn -= KidneyFlowOut.Value[i];

                TotalFlowIn += oneStepFlowIn * (double)bStep;
                ExternalFlow.Value[i] = oneStepFlowIn;
            }

            volume[configuration.RightBeforeHeart][CurrStep] =
                volume[configuration.RightBeforeHeart][CurrStep] + TotalFlowIn;

/*            double vascularSystemFlowIn = 0;
            double oneStepFlow;

            IModel model = Configuration.GetModelByName("Fluids");

            for (long i = skippedFrom; i < CurrStep; i++)
            {
                oneStepFlow = model.GetValueByNameAndTime("TotalExchangeWithVascularSystem", i * bStep);
                oneStepFlow -= KidneyFlowOut.Value[i];

                vascularSystemFlowIn += oneStepFlow * (double)bStep;

                ExternalFlow.Value[i] = oneStepFlow;
            }      

            volume[configuration.RightBeforeHeart][CurrStep] = 
                volume[configuration.RightBeforeHeart][CurrStep] + vascularSystemFlowIn;*/
        }
    }
}
