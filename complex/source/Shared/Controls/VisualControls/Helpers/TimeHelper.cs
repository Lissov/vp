using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using LissovBase.Functions;

namespace VisualControls.Helpers
{
    public static class TimeHelper
    {

        public enum MeasureUnit
        {
            Custom = -1,
            Second = 0,
            Minute = 1,
            Hour = 2,
            Day = 3
        }

        public class TimeDivider
        {
            public MeasureUnit Measure { get; set; }

            private string _unit;
            public string Unit
            {
                get
                {
                    switch (Measure)
                    {
                        case MeasureUnit.Second:
                            return Constants.Units.second;
                        case MeasureUnit.Minute:
                            return Constants.Units.minute;
                        case MeasureUnit.Hour:
                            return Constants.Units.hour;
                        case MeasureUnit.Day:
                            return Constants.Units.day;
                        default:
                            return _unit;
                    }
                }
                set
                {
                    _unit = value;
                }
            }

            public decimal Divider
            {
                get
                {
                    return GetDivider(Measure);
                }
            }
        }

        private static decimal MINUTE = 60;
        private static decimal HOUR = MINUTE * 60;
        private static decimal DAY = HOUR * 24;

        private static decimal SECONDS_LIMIT =  5 * MINUTE;
        private static decimal MINUTES_LIMIT = 3 * HOUR;
        private static decimal HOURS_LIMIT = 3 * DAY;

        public static TimeDivider GetOptimalUnit(Function f)
        {
            if (f.XUnit == Constants.Units.second || f.XUnit == Constants.UnitsC.second)
                return GetOptimalUnit((decimal)f.MaxX);
            else
                return new TimeDivider() { Measure = MeasureUnit.Custom, Unit = f.XUnit };
        }

        public static TimeDivider GetOptimalUnit(decimal time)
        {
            if (Math.Abs(time) > HOURS_LIMIT)
                return new TimeDivider { Measure = MeasureUnit.Day };

            if (Math.Abs(time) > MINUTES_LIMIT)
                return new TimeDivider { Measure = MeasureUnit.Hour };

            if (Math.Abs(time) > SECONDS_LIMIT)
                return new TimeDivider { Measure = MeasureUnit.Minute };

            return new TimeDivider { Measure = MeasureUnit.Second };
        }

        public static decimal GetDivider(MeasureUnit measure)
        {
            switch (measure)
            {
                case MeasureUnit.Day: return DAY;
                case MeasureUnit.Hour : return HOUR;
                case MeasureUnit.Minute: return MINUTE;
                case MeasureUnit.Second : return 1;
                default:
                    return 1;
            }
        }


    }
}
