using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;

namespace LissovBase
{
    public class ValueArrayElement : LissovValue
    {
        private double[][] _array;
        private int _index;

        public ValueArrayElement(string name, string displayName, ValueType type, double[][] array, int index, string groupName, bool initValueVisible, string measure)
            : base(name, displayName, type, groupName, initValueVisible)
        {
            _array = array;
            _index = index;
            this.Measure = measure;
        }

        public ValueArrayElement(string name, string displayName, ValueType type, double[][] array, int index, string measure)
            : this(name, displayName, type, array, index, string.Empty, false, measure)
        {
        }

        public override double[] Value
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
