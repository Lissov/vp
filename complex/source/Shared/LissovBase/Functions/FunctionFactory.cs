using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public class FunctionFactory
    {
        private Dictionary<string, Type> types;
        private FunctionFactory()
        {
            types = new Dictionary<string, Type>();
            types.Add(new LinearFunction().FunctionType, typeof(LinearFunction));
            types.Add(new TrapezialFunction().FunctionType, typeof(TrapezialFunction));
            types.Add(new ExpressionFunction().FunctionType, typeof(ExpressionFunction));
        }

        public static FunctionFactory Instance = new FunctionFactory();

        public List<string> GetAllTypes()
        {
            return new List<string>(types.Keys);
        }

        public Function GetFunction(string functionType, string unit)
        {
            Function res = null;
            if (types.ContainsKey(functionType))
                res = (types[functionType].Assembly.CreateInstance(types[functionType].FullName)) as Function;
            if (res != null)
                res.Unit = unit;
            return res;
        }
    }
}
