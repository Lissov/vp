using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class LinearFunction : Function
    {
        [FunctionEditableParameter("Start of load", Constants.UnitsC.second)]
        public double StartTime { get; set; }
        [FunctionEditableParameter("Duration of increase", Constants.UnitsC.second)]
        public double IncreaseTime { get; set; }
        [FunctionEditableParameter("Level", Constants.UnitsC.value)]
        public double Level { get; set; }

        public override double getValue(double x)
        {
            if (x < StartTime) return 0;
            if (x - StartTime >= IncreaseTime) return Level;

            return (Level * (x - StartTime) / IncreaseTime);
        }

        public override string FunctionType
        {
            get
            {
                return "Linear and Plato";
            }
        }

        public override List<double> GetCriticalPoints()
        {
            return new List<double>(new double[]{
                StartTime, StartTime + IncreaseTime
            });
        }
    }
}
