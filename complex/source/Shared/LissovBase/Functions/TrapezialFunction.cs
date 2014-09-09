using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class TrapezialFunction : Function
    {
        [FunctionEditableParameter("Start of load", Constants.UnitsC.second)]
        public double StartTime { get; set; }
        [FunctionEditableParameter("Duration of increase", Constants.UnitsC.second)]
        public double IncreaseTime { get; set; }
        [FunctionEditableParameter("Duration of level", Constants.UnitsC.second)]
        public double LevelTime { get; set; }
        [FunctionEditableParameter("Level", Constants.UnitsC.value)]
        public double Level { get; set; }
        [FunctionEditableParameter("Duration of decrease", Constants.UnitsC.second)]
        public double DecreaseTime { get; set; }

        public override double getValue(double x)
        {
            double t = x;
            if (t < StartTime) return 0;
            t -= StartTime;
            if (t < IncreaseTime) return Level * t / IncreaseTime;
            t-= IncreaseTime;
            if (t < LevelTime) return Level;
            t -= LevelTime;
            if (t < DecreaseTime) return Level * (DecreaseTime - t) / DecreaseTime;

            return 0;
        }

        public override string FunctionType
        {
            get
            {
                return "Trapezial";
            }
        }

        public override List<double> GetCriticalPoints()
        {
            return new List<double>(new double[] {
                StartTime, 
                StartTime + IncreaseTime,
                StartTime + IncreaseTime + LevelTime,
                StartTime + IncreaseTime + LevelTime + DecreaseTime
            });
        }
    }
}
