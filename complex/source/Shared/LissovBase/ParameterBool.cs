using System;
using System.Collections.Generic;
using System.Text;
using ModelBase;
using System.Xml;
using System.Reflection;
using LissovLog;

namespace LissovBase
{
    public class ParameterBool : ParameterSafe
    {
        public ParameterBool(string name, string displayName)
            : base(name, displayName)
        {
            BoolValue = true;
        }

        public ParameterBool(string displayName)
            : this(displayName.ToIdentifier(), displayName)
        {
            BoolValue = true;
        }

        public bool BoolValue { get; set; }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            XmlElement elt = base.ToXml(parentElement);
            SetAttribute(elt, "BoolValue", BoolValue);
            return elt;
        }

        public override object FromXml(XmlElement currentElement)
        {
            base.FromXml(currentElement);
            try
            {
                if (currentElement.Attributes["BoolValue"] != null)
                {
                    BoolValue = int.Parse(currentElement.Attributes["BoolValue"].Value)
                        == LissovModelBase.TRUE;
                }
                else
                {
                    if (Value == LissovModelBase.TRUE) BoolValue = true;
                    if (Value == LissovModelBase.FALSE) BoolValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Error reading [{0}]: {1}",
                    this.DisplayName, ex.Message);
            }
            return this;
        }

        public static implicit operator bool(ParameterBool param)
        {
            return param.BoolValue;
        }
    }
}