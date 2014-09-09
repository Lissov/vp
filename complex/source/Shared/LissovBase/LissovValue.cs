using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using LissovLog;
using System.Xml;

namespace LissovBase
{
    public class LissovValue : Value
    {
        public LissovValue(string name, string displayName, ValueType valueType)
            : base(name, displayName, valueType)
        {
        }

        public LissovValue(string name, string displayName, ValueType valueType, string measure)
            :base(name, displayName, valueType, measure)
        {
        }

        public LissovValue(string name, string displayName, ValueType valueType, string groupName, bool initValueVisible)
            : base(name, displayName, valueType, groupName, initValueVisible)
        {
        }

        public LissovValue(string displayName, ValueType valueType, string measure)
            : base(displayName.ToIdentifier(true), displayName, valueType, measure)
        {
        }
        public LissovValue(string displayName, ValueType valueType)
            : base(displayName.ToIdentifier(true), displayName, valueType)
        {
        }

        public LissovValue(string displayName, ValueType valueType, string groupName, bool initValueVisible)
            : base(displayName.ToIdentifier(true), displayName, valueType, groupName, initValueVisible)
        {
        }

        public LissovValue(string displayName, ValueType valueType, string groupName, bool initValueVisible, string measure)
            : base(displayName.ToIdentifier(true), displayName, valueType, groupName, initValueVisible, measure)
        {
        }

        internal double[] _value;
        public override double[] Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public bool LinkExpected
        {
            get;
            set;
        }

        private bool _noInterpolation = false;
        public bool NoInterpolation
        {
            get { return _noInterpolation; }
            set { _noInterpolation = value; }
        }

        /// <summary>
        /// Gets the value from underlying array without Compression!!!
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public double this[int step]
        {
            get
            {
                try
                {
                    return _value[step];
                }
                catch (Exception ex)
                {
                    Log.Write(LogLevel.ERROR, "Exception [L002]: " + ex.Message);
                    throw;
                }
            }
            set
            {
                try
                {
                    _value[step] = value;
                }
                catch (Exception ex)
                {
                    Log.Write(LogLevel.ERROR, "Exception [L003]: " + ex.Message);
                    throw;
                }
            }
        }

        public override object FromXml(System.Xml.XmlElement currentElement)
        {
            try
            {
                if (currentElement.Attributes["InitValue"] != null)
                    InitValue = GetDouble(currentElement, "InitValue");
                if (currentElement.Attributes["Visible"] != null)
                    Visible = GetBoolean(currentElement, "Visible");
                if (currentElement.Attributes["InitValueVisible"] != null)
                    InitValueVisible = GetBoolean(currentElement, "InitValueVisible");
                if (currentElement.Attributes["LinkModelName"] != null)
                    LinkModelName = GetString(currentElement, "LinkModelName");
                if (currentElement.Attributes["LinkValueName"] != null)
                    LinkValueName = GetString(currentElement, "LinkValueName");

                return this;
            }
            catch (Exception)
            {
                Log.Write(LogLevel.ERROR, "Cannot load value: {0}", this.Name);
                return null;
            }
        }

        public override object FromXml(System.Xml.XmlElement xmlElement, string name)
        {
            if (xmlElement == null)
            {
                Log.Write(LogLevel.ERROR, "Cannot load value [{0}] - xml node is empty", this.Name);
                return null;
            }
            if (xmlElement.Name != name)
            {
                Log.Write(LogLevel.ERROR, "Cannot load value [{0}] - wrong xml node (found: {1}, expected: {2})", this.Name, name, xmlElement.Name);
                return null;
            }

            XmlElement currentElement = xmlElement.ChildNodes[0] as XmlElement;

            return this.FromXml(currentElement);
        }

        #region Compression
        public int CompressionStep
        {
            get;
            set;
        }
        public void Compress(int CurrStep)
        {
            if (CompressionStep <= 1) return;
            _value[CurrStep - CompressionStep + 1] = _value[CurrStep];
        } 
        #endregion
    }
}
