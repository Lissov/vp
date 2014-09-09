using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using System.Xml;

namespace LissovBase.Functions
{
    public class ParameterFunction : ParameterSafe
    {
        private Function _function;
        public Function InnerFunction
        {
            get { return _function; }
            set { _function = value; }
        }

        public ParameterFunction(string name, string displayName, Function f)
            : base(name, displayName)
        {
            _function = f;
        }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            XmlElement element = _function.ToXml(base.ToXml(parentElement));
            element.SetAttribute("FunctionType", _function.FunctionType);
            return element;
        }
        public override object FromXml(XmlElement currentElement)
        {
            Value = 0; // to indicate that this is loaded

            string ftype = null;
            if (currentElement.ChildNodes.Count > 0 && currentElement.ChildNodes[0].Attributes["FunctionType"] != null)
            {
                ftype = currentElement.ChildNodes[0].Attributes["FunctionType"].Value;
                Function f = FunctionFactory.Instance.GetFunction(ftype, Constants.Units.none);
                if (f != null) _function = f;
            }

            return _function.FromXml(currentElement) as Parameter;
        }
    }
}
