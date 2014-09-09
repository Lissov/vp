using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ModelBase;

namespace LissovBase
{
    public interface IValueCollection
    {
        void Setup(IList<string> names); 
        ICollection<Value> getValues();
    }

    public class ValueCollection<T> :IValueCollection
    {
        private string _displayName;
        Value.ValueType _type;
        private bool _groupByName = false;
        private string _measure = string.Empty;
        private bool _linkExpected = false;
        private bool _noInterpolate = false;
        public ValueCollection(string displayname, Value.ValueType type, bool groupByName, string measure, bool linkExpected, bool noInterpolate)
        {
            _displayName = displayname;
            _type = type;
            _count = 0;
            _groupByName = groupByName;
            _measure = measure;
            _linkExpected = linkExpected;
            _noInterpolate = noInterpolate;
        }

        public ValueCollection(string displayname, Value.ValueType type, bool groupByName, string measure)
            : this(displayname, type, groupByName, measure, false, false)
        { }
            
        public ValueCollection(string displayname, Value.ValueType type, string measure)
            : this(displayname, type, false, measure, false, false)
        { }

        private int _count;
        private IList<string> _names = null;

        #region IValueCollection Members
        public void Setup(IList<string> names)
        {
            if (_names == names) return;
            _names = names;
            _count = names.Count;
            _values = null;
        }

        Value[] _values;
        public ICollection<Value> getValues()
        {

            if (_values == null)
            {
                _values = new Value[_count];
                for (int i = 0; i < _count; i++)
                {
                    _values[i] = new LissovValue(_displayName + _names[i], _type,
                        (_groupByName ? _names[i] : string.Empty), true, _measure) { LinkExpected = _linkExpected, NoInterpolation = _noInterpolate };
                }
            }
            return _values;
        }

        #endregion

        public LissovValue this[int index]
        {
            get
            {
                return _values[index] as LissovValue;
            }
            set
            {
                _values[index] = value;
            }
        }
    }
}
