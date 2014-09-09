using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using System.Xml;

namespace LissovBase.Functions
{
    public class PVFunction : Function
    {
        [FunctionEditableParameter("Level 1", Constants.UnitsC.none)]
        public double level1 { get; set; }
        [FunctionEditableParameter("Level 2", Constants.UnitsC.none)]
        public double level2 { get; set; }
        [FunctionEditableParameter("Power 1", Constants.UnitsC.none)]
        public double power1 { get; set; }
        [FunctionEditableParameter("Power 2", Constants.UnitsC.none)]
        public double power2 { get; set; }
        [FunctionEditableParameter("Linear Volume", Constants.UnitsC.ml)]
        public double linearV { get; set; }
        [FunctionEditableParameter("Linear Pressure", Constants.UnitsC.value)]
        public double linearP { get; set; }

        private double k1, k2, x1, x2;

        public override void Update()
        {
            if (level2 >= 1) level2 = 0.4;
            k2 = 1;

            if (linearP != 0 && linearV != 0)
            {
                x2 = linearV - power2 * linearV * (1 - level2);
                k2 = (linearP * (1 - level2)) / Math.Pow(linearV - x2, power2);
            }
            else if (linearP == 0)
                x2 = linearV; 
            else if (linearV == 0)
                x2 = 0;

            x1 = 0;
            if (level1 > 0)
            {
                double l1 = (1 / level1) - 1;
                if (linearV != 0)
                {
                    x1 = power1 * l1 * linearV / linearP;
                    k1 = l1 / Math.Pow(x1, power1);
                }
                else
                    k1 = 1;
            }

            MinX = -linearV / 2;
            MaxX = linearV * 1.5;
        }

        public override double getValue(double x)
        {
            if (x<0) return (1 / level1) - 1 - k1 * Math.Pow(x1 - x, power1);
            if (x < linearV) return x * (linearP / linearV);
            return level2 * linearP + k2 * Math.Pow(x - x2, power2);
        }

        public override object FromXml(XmlElement xmlElement)
        {
            base.FromXml(xmlElement);
            Update();
            return this;
        }

        public override string FunctionType
        {
            get
            {
                return "3-interval PV";
            }
        }
    }
}
