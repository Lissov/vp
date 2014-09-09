using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase
{
    public static class Constants
    {
        public const double GRAVITY_TO_MMHG = 75; // 1 meter of blood makes 75 mmHg Pressure

        public const double INVALID_DOUBLE = double.MinValue / 100;
        public const double INVALID_MAX_DOUBLE = double.MaxValue / 100;

        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public static string language = lang_en;
        public const string lang_en = "en-US";
        public const string lang_ru = "ru-RU";
        public static class Units
        {
            public static string none
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru:
                        case lang_en:
                        default:
                            return string.Empty;
                    }
                }
            }

            public static string ml
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "мл";  
                        case lang_en:
                        default:
                            return "ml";
                    }
                }
            }

            public static string second
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "с";
                        case lang_en:
                        default:
                            return "s";
                    }
                }
            }

            public static string minute
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "мин";
                        case lang_en:
                        default:
                            return "min";
                    }
                }
            }

            public static string hour
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "час";
                        case lang_en:
                        default:
                            return "hr";
                    }
                }
            }

            public static string day
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "дн";
                        case lang_en:
                        default:
                            return "d";
                    }
                }
            }

            public static string liter
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "л";
                        case lang_en:
                        default:
                            return "l";
                    }
                }
            }

            public static string beat
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "уд.";
                        case lang_en:
                        default:
                            return "beat";
                    }
                }
            }

            public static string beat_per_second
            {
                get
                {
                    return beat + " / " + second;
                }
            }

            public static string ml_per_second
            {
                get
                {
                    return ml + " / " + second;
                }
            }

            public static string resist
            {
                get
                {
                    return mmHg + "*" + second + " / " + ml;
                }
            }

            public static string mmHg_per_ml
            {
                get
                {
                    return mmHg + " / " + ml;
                }
            }

            public static string mmHg
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "мм.рт.ст.";
                        case lang_en:
                        default:
                            return "mmHg";
                    }
                }
            }

            public static string meter
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "м";
                        case lang_en:
                        default:
                            return "m";
                    }
                }
            }

            public static string PRU
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "ед.";
                        case lang_en:
                        default:
                            return "PRU";
                    }
                }
            }

            public static string radian
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru: return "рад";
                        case lang_en:
                        default:
                            return "rad";
                    }
                }
            }

            public static string degree
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru:
                            return "˚";
                        case lang_en:
                            return "deg";
                        default:
                            return "∠˚";
                    }
                }
            }

            public static string degreeCelsium
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru:
                        case lang_en:
                        default:
                            return "˚C";
                    }
                }
            }

            public static string joule
            {
                get
                {
                    switch (language)
                    {
                        case lang_ru:
                            return "Дж";
                        case lang_en:
                        default:
                            return "J";
                    }
                }
            }

            public static string unit
            {
                get
                {                    
                    switch (language)
                    {
                        case lang_ru: return "ед";
                        case lang_en:
                        default:
                            return "   ";
                    }
                }
            }
            public static string GetLocalizedUnit(string nonlocalized)
            {
                if (nonlocalized == UnitsC.ml)
                    return Units.ml;
                if (nonlocalized == UnitsC.second)
                    return Units.second;
                if (nonlocalized == UnitsC.liter)
                    return Units.liter;
                if (nonlocalized == UnitsC.unit)
                    return Units.unit;
                if (nonlocalized == UnitsC.mmHg)
                    return Units.mmHg;
                if (nonlocalized == UnitsC.none)
                    return Units.none; 
                return nonlocalized;
            }
        }

        public static class UnitsC
        {
            public const string none = "";

            public const string ml = "ml";

            public const string second = "s";

            public const string liter = "l";

            public const string unit = "u";

            public const string value = "value";

            public const string mmHg = "mmHg";

            public const string xunit = "x";
        }
    }
}
