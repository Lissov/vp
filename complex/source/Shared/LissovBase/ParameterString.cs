using System;
using System.Collections.Generic;
using System.Text;
using ModelBase;
using System.Xml;
using System.Reflection;

namespace LissovBase
{
    public class ParameterString : ParameterSafe
    {
        public ParameterString(string name, string displayName)
            : base(name, displayName)
        {
            StringValue = string.Empty;
        }

        public string StringValue { get; set; }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            XmlElement elt = base.ToXml(parentElement);
            SetAttribute(elt, "StringValue", StringValue);
            return elt;
        }

        public override object FromXml(XmlElement currentElement)
        {
            base.FromXml(currentElement);
            if (currentElement.Attributes["StringValue"] != null)
                StringValue = (string)currentElement.Attributes["StringValue"].Value;
            return this;
        }
    }
}