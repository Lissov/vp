using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovLog;

namespace LissovBase.Functions
{
    /// <summary>
    /// Function = A*SQRT(Bx + C)+D
    /// </summary>
    public class RootFunction : Function
    {
        [FunctionEditableParameter("A", Constants.UnitsC.none)]
        public double A { get; set; }
        [FunctionEditableParameter("B", Constants.UnitsC.none)]
        public double B { get; set; }
        [FunctionEditableParameter("C", Constants.UnitsC.none)]
        public double C { get; set; }
        [FunctionEditableParameter("C", Constants.UnitsC.none)]
        public double D { get; set; }

        public override double getValue(double x)
        {
            return A * Math.Sqrt(B*x + C) + D;
        }

        public void setParams(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public override string FunctionType
        {
            get
            {
                return "Root";
            }
        }
    }
}
