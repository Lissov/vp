using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using System.Xml;
using System.Reflection;
using LissovLog;

namespace LissovBase.Functions
{
    public abstract class Function : ObjectBase
    {
        public virtual string FunctionType { get { throw new NotImplementedException("FunctionType not implemented for: " + GetType().Name); } }

        public abstract double getValue(double x);

        public override XmlElement ToXml(XmlElement xml)
        {
            return base.ToXml(xml).WriteObjectProperties(this);
        }

        public override object FromXml(XmlElement xmlElement)
        {
            if (xmlElement.Name != this.GetType().Name)
                xmlElement = xmlElement.GetChildNode(this.GetType().Name);

            if (xmlElement == null)
            {
                Log.Write(LogLevel.ERROR, "Error while loading Function {0} - no XML provided", this.GetType().Name);
                return this;
            }

            return xmlElement.ReadObjectProperties(this);
        }

        [FunctionEditableParameter("Min X", Constants.UnitsC.xunit)]
        public virtual double MinX { get; set; }
        [FunctionEditableParameter("Max X", Constants.UnitsC.xunit)]
        public virtual double MaxX { get; set; }

        private string _unit = Constants.UnitsC.none;
        public virtual string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private string _xUnit = Constants.Units.second;
        public virtual string XUnit
        {
            get { return _xUnit; }
            set { _xUnit = value; }
        }

        public virtual void Update() {}

        public virtual List<double> GetCriticalPoints()
        {
            return new List<double>();
        }
    }
}
