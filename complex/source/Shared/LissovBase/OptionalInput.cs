using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase
{
    public class OptionalInput : LissovValue
    {
        public OptionalInput(string displayName, ValueType valueType, string measure)
            : base(displayName, valueType, measure)
        {
            _use = new ParameterSafe("Use " + displayName);
        }

        private ParameterSafe _use;
        public ParameterSafe Use { get { return _use; } }
    }
}
