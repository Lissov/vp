using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class SFunction : Function
    {
        [FunctionEditableParameter("Minimum time", Constants.UnitsC.second)]
        public double XMin { get; set; }
        [FunctionEditableParameter("Maximum time", Constants.UnitsC.second)]
        public double XMax { get; set; }
        [FunctionEditableParameter("Time scale", Constants.UnitsC.none)]
        public double XScale { get; set; }
        [FunctionEditableParameter("Level", Constants.UnitsC.value)]
        public double Level { get; set; }
        [FunctionEditableParameter("A", Constants.UnitsC.none)]
        public double A { get; set; }
        [FunctionEditableParameter("B", Constants.UnitsC.none)]
        public double B { get; set; }
        [FunctionEditableParameter("N", Constants.UnitsC.none)]
        public double N { get; set; }

        public override double getValue(double x)
        {
            if (x < XMin) return getValue(XMin);
            if (x > XMax) return getValue(XMax);
            double xn = XScale * (x - XMin) / (XMax - XMin);
            return Level + A * Math.Pow(xn, N) / (B + Math.Pow(xn, N));
        }

        public override double MaxX
        {
            get { return XMax; }
            set { base.MaxX = value; }
        }
        public override double MinX
        {
            get { return XMin; }
            set { base.MinX = value; }
        }

        public override string FunctionType
        {
            get
            {
                return "S-curve";
            }
        }
    }
}
