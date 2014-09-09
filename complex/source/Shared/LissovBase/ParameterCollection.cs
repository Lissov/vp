using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;

namespace LissovBase
{
    public interface IParameterCollection
    {
        IEnumerable<Parameter> getParameters();
        void Setup(IList<string> names);
    }

    public class ParameterCollection : IParameterCollection
    {
        private string parameterID;
        private string parameterName;
        private int count;
        public ParameterCollection(string parameterId, string parameterName)
        {
            this.parameterID = parameterId;
            this.parameterName = parameterName;
            count = 0;
        }

        public ParameterCollection(string parameterName)
        {
            this.parameterID = parameterName.ToIdentifier(true)+'_';
            this.parameterName = parameterName;
            count = 0;
        }

        private IList<string> _names;
        public void Setup(IList<string> names)
        {
            if (_names == names) return;

            count = names.Count;
            _names = names;
            values = new double[count];
            parameters = null;
        }

        private double[] values;
        public double this[int index]
        {
            get { return values[index]; }
            set { values[index] = value;}
        }

        private Parameter[] parameters = null;
        public IEnumerable<Parameter> getParameters()
        {
            if (parameters == null)
            {
                parameters = new Parameter[count];
                for (int i = 0; i < count; i++)
                    parameters[i] = new ParameterArrayElement(parameterID + _names[i].ToIdentifier(), parameterName + " " + _names[i], values, i);
            }
            return parameters;
        }
    }

    public class ParameterCollection<T> : IParameterCollection
        where T : class, new()
    {
        private string parameterID;
        private string parameterName;
        private int count;
        public ParameterCollection(string parameterId, string parameterName)
        {
            this.parameterID = parameterId;
            this.parameterName = parameterName;
            count = 0;
        }

        public ParameterCollection(string parameterName)
        {
            this.parameterID = parameterName.ToIdentifier(true);
            this.parameterName = parameterName;
            count = 0;
        }

        private IList<string> _names;
        public void Setup(IList<string> names)
        {
            if (_names == names) return;

            count = names.Count;
            _names = names;
            values = new T[count];
            if (typeof(T).IsClass)
            {
                for (int i = 0; i < count; i++)
                    values[i] = new T();
            }

            parameters = null;
        }

        private T[] values;
        public T this[int index]
        {
            get { return values[index]; }
            set { values[index] = value; }
        }

        private Parameter[] parameters = null;
        public IEnumerable<Parameter> getParameters()
        {
            if (parameters == null)
            {
                parameters = new Parameter[count];
                for (int i = 0; i < count; i++)
                    parameters[i] = new ParameterObject<T>(parameterID + _names[i], parameterName + _names[i].ToIdentifier(), values[i]);
            }
            return parameters;
        }
    }
}
