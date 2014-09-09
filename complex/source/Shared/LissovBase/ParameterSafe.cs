using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using LissovLog;
using System.Windows.Forms;
using System.Xml;

namespace LissovBase
{
    public class ParameterSafe : Parameter
    {
        public ParameterSafe(string Name, string DisplayName)
            : base(Name, DisplayName)
        {
        }

        public ParameterSafe(string DisplayName)
            : base(DisplayName.ToIdentifier(true), DisplayName)
        {
        }

        public override object FromXml(XmlElement currentElement)
        {
            return InvokeSafe(delegate() { return base.FromXml(currentElement); });
        }

        public override object FromXml(XmlElement xmlElement, string name)
        {
            try
            {
                XmlElement currentElement = xmlElement.FirstChild as XmlElement;
                return FromXml(currentElement);
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Exception in parameter [{0}], method [{1}] : [{2}] ", this.Name, "FromXml", ex.Message);
                return null;
            }
        }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            return InvokeSafe(delegate() { return base.ToXml(parentElement); }) as XmlElement;
        }

        public override XmlElement ToXml(XmlElement parentElement, string name)
        {
            try
            {
                return base.ToXml(parentElement, name);
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Exception in parameter [{0}], method [{1}] : [{2}] ", this.Name, "ToXml", ex.Message);
                return null;
            }
        }

        private delegate object ObjectMethodInvoker();
        private object InvokeSafe(ObjectMethodInvoker d)
        {
            try
            {
                return d.DynamicInvoke(null);
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Exception in parameter [{0}], method [{1}] : [{2}] ", this.Name, d.Method.Name, ex.Message);
                return null;
            }
        }
        
        public static implicit operator double(ParameterSafe ps)
        {
            return ps.Value;
        }
    }
}
