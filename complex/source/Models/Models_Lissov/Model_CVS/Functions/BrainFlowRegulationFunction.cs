using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class BrainFlowRegulationFunction : Function
    {
        [FunctionEditableParameter("PminC", Constants.UnitsC.mmHg)]
        public double PminC { get; set; }
        [FunctionEditableParameter("PmaxC", Constants.UnitsC.mmHg)]
        public double PmaxC { get; set; }
        [FunctionEditableParameter("Pmax", Constants.UnitsC.mmHg)]
        public double Pmax { get; set; }

        [FunctionEditableParameter("Rmin", Constants.UnitsC.unit)]
        public double Rmin { get; set; }
        [FunctionEditableParameter("Rmax", Constants.UnitsC.unit)]
        public double Rmax { get; set; }
        [FunctionEditableParameter("Gain Linear", Constants.UnitsC.none)]
        public double GainLinear { get; set; }
        [FunctionEditableParameter("Amplitude Exponential", Constants.UnitsC.unit)]
        public double AmplitudeExponential { get; set; }
        [FunctionEditableParameter("Gain Exponential", Constants.UnitsC.none)]
        public double GainExponential { get; set; }   

        public BrainFlowRegulationFunction()
        {
            PminC = 50;
            PmaxC = 150;
        }

        public override double getValue(double p)
        {
            if (p < PminC)
                return Rmin;

            if (p < PmaxC)
                return GainLinear * (p - PminC) + Rmin;

            if (p < Pmax)
                return Rmax - AmplitudeExponential * Math.Exp(GainExponential * (p - PmaxC));

            return Rmin;
        }

        public override string FunctionType
        {
            get
            {
                return "BrainFlowRegulation";
            }
        }

        public override string Unit
        {
            get
            {
                return Constants.Units.resist;
            }
            set
            {
                throw new Exception("Cannot change unit of Brain Flow Regulation Function");
            }
        }
    }
}
