using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovLog;

namespace LissovBase.Functions
{
    /// <summary>
    /// Function = A*(x + B)^2 + C
    /// </summary>
    public class SquareFunction : Function
    {
        [FunctionEditableParameter("A", Constants.UnitsC.none)]
        public double A { get; set; }
        [FunctionEditableParameter("B", Constants.UnitsC.none)]
        public double B { get; set; }
        [FunctionEditableParameter("C", Constants.UnitsC.none)]
        public double C { get; set; }

        public override double getValue(double x)
        {
            return A * (x + B) * (x + B) + C;
        }

        public void set0_1IntervalThroughPoint(double x, double y)
        {
            A = (y - x) / (x * x - x);
            B = (1 - A) / (2 * A);
            C = -A * B * B;

            if (y - getValue(x) > 0.0000001)
                Log.Write(LogLevel.ERROR, "Cannot setup SquareFunction - eeror in set0_1IntervalThroughPoint({0}, {1})", x, y);
        }

        public override string FunctionType
        {
            get
            {
                return "Square";
            }
        }
    }
}
