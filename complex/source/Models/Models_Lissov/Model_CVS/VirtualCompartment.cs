using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using ModelBase;
using System.Xml.Serialization;

namespace Model_CVS
{
    [Serializable]
    public class VirtualCompartment
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Compartment1.DisplayName = value + " Compartment 1";
                Compartment1.Name = Compartment1.DisplayName.ToIdentifier();
                Compartment2.DisplayName = value + " Compartment 2";
                Compartment2.Name = Compartment2.DisplayName.ToIdentifier();
                Compartment1Gain.DisplayName = value + " Gain 1";
                Compartment1Gain.Name = Compartment1Gain.DisplayName.ToIdentifier();
                Compartment2Gain.DisplayName = value + " Gain 2";
                Compartment2Gain.Name = Compartment2Gain.DisplayName.ToIdentifier();
            }
        }

        [XmlIgnore]
        public ParameterSafe Compartment1 = new ParameterSafe("Compartment 1");
        [XmlIgnore]
        public ParameterSafe Compartment2 = new ParameterSafe("Compartment 2");

        [XmlIgnore]
        public ParameterSafe Compartment1Gain = new ParameterSafe("Compartment 1 gain");
        [XmlIgnore]
        public ParameterSafe Compartment2Gain = new ParameterSafe("Compartment 2 gain");

        //Input flow applied to compartment2
        [XmlIgnore]
        public LissovValue inputFlow = new LissovValue("External Input Flow", Value.ValueType.Input, Constants.Units.ml_per_second);

        //Pressure equals to        P1 * Gain1 + P2 * Gain2
        [XmlIgnore]
        public LissovValue pressure = new LissovValue("Pressure", ModelBase.Value.ValueType.Output, Constants.Units.mmHg);

        //full pressure is pressure + external atmosphere pressure
        [XmlIgnore]
        private LissovValue pressureFull = new LissovValue("Full Pressure", ModelBase.Value.ValueType.Output, Constants.Units.mmHg);

        [XmlIgnore]
        public CVSModel Model;

        public VirtualCompartment(CVSModel parent)
        {
            Model = parent;
        }
        public VirtualCompartment()
        {
            //
        }

        public List<Parameter> GetParameters()
        {
            return new List<Parameter>(new ParameterSafe[]{
                Compartment1,
                Compartment2,
                Compartment1Gain,
                Compartment2Gain
            });
        }

        public List<Value> GetValues()
        {
            pressure.DisplayName = "Pressure in " + Name;
            pressureFull.DisplayName = "Pressure Full " + Name;
            inputFlow.DisplayName = "External Flow to " + Name;

            List<Value> res = new List<Value>(new LissovValue[] { inputFlow, pressure, pressureFull });

            foreach (Value item in res)
            {
                item.Name = item.DisplayName.ToIdentifier();
                item.GroupName = Name;
            }

            return res;
        }

        public void Calculate(long stepnum, long input_step, double step)
        {
            Model.volume[(int)Compartment2.Value][stepnum] = Model.volume[(int)Compartment2.Value][stepnum]
                + inputFlow.Value[input_step] * step;

            pressure.Value[stepnum] =
                Model.pressure[(int)Compartment1.Value][stepnum] * Compartment1Gain +
                Model.pressure[(int)Compartment2.Value][stepnum] * Compartment2Gain;

            pressureFull.Value[stepnum] = pressure.Value[stepnum]
                + Model.ext_pressure[(int)Compartment1.Value][input_step];
        }

        public void StepBack(long fromStep, long toStep)
        {
            pressure.Value[toStep] = pressure.Value[fromStep];
            pressureFull.Value[toStep] = pressureFull.Value[fromStep];
        }
    }
}
