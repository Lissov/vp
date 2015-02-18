using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase.Functions;
using LissovBase;
using ModelBase;

namespace Model_Load
{
    public class ParameterLoadFunction : ParameterFunction, IParameterCollection, IValueCollection
    {
        public ParameterSafe Simulate { get; set; }
        public LissovValue Output { get; set; }
        public ParameterSafe BaseValue { get; set; }

        public ParameterLoadFunction(string displayName, Function f, string measure)
            : base(displayName.ToIdentifier() + "Function", displayName, f)
        {
            Output = new LissovValue(displayName, ModelBase.Value.ValueType.Output) { Measure = measure };
            Simulate = new ParameterSafe("Use" + displayName.ToIdentifier(), "Simulate " + displayName);
            BaseValue = new ParameterSafe("Base" + displayName.ToIdentifier(), "Base " + displayName);

            if (f != null)
                f.Unit = measure;
            
            BaseValue.Value = 0;
        }

        #region IParameterCollection Members
        public IEnumerable<Parameter> getParameters()
        {
            return new Parameter[] { Simulate, BaseValue };
        }
        public void Setup(IList<string> names)
        {
            return;
        }
        #endregion

        #region IValueCollection Members


        public ICollection<Value> getValues()
        {
            return new LissovValue[] { Output };
        }

        #endregion

        public double Calculate(long stepn, double time)
        {
            double val;
            if (Simulate.Value == LissovModelBase.TRUE)
                val = InnerFunction.getValue(time);
            else
                val = Output.InitValue;
            Output.Value[stepn] = val + BaseValue.Value;
            return val;
        }        
    }
}
