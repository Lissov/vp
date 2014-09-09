using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;

namespace LissovBase
{
    public class ParameterArrayElement : ParameterSafe
    {
        private double[] _array;
        private int _index;

        public ParameterArrayElement(string name, string displayName, double[] array, int index)
            : base(name, displayName)
        {
            _array = array;
            _index = index;
        }

        public override double Value
        {
            get
            {
                return _array[_index];
            }
            set
            {
                _array[_index] = value;
            }
        }
    }    
}
