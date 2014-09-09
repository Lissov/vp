using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Functions;

namespace LissovBase.Functions
{
    public class ExpressionFunction : Function
    {
        [FunctionEditableParameter("Expression", Constants.UnitsC.value)]
        public string Expression { get; set; }

        public ExpressionFunction()
        {
            Expression = "time";
        }

        public override double getValue(double x)
        {
            return MathExpression.EvaluateTimeExpression(Expression, x);
        }

        public override string FunctionType
        {
            get
            {
                return "Expression";
            }
        }
    }
}
