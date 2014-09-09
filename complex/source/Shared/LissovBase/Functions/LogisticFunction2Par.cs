using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class LogisticFunction2Par : Function
    {
        [FunctionEditableParameterAttribute("Alpha", Constants.UnitsC.none)]
        public double alpha { get; set; }
        [FunctionEditableParameterAttribute("Theta", Constants.UnitsC.none)]
        public double theta { get; set; }

        public override double getValue(double x)
        {
            double exp = Math.Exp(-alpha * x);
            return (1 - exp) / (1 + theta * exp);
        }

        public override string FunctionType
        {
            get
            {
                return "2-parametric Logistic";
            }
        }
    }
}
