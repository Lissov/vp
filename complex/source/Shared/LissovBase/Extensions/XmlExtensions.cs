using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using ModelBase;

namespace LissovBase
{
    public static class XmlExtensions
    {
        public static XmlElement GetChildNode(this XmlElement element, string name)
        {
            foreach (XmlElement child in element.ChildNodes)
            {
                if (child.Name == name) return child;
            }
            return null;
        }

        public static XmlElement CreateChildNode(this XmlElement element, string name)
        {
            XmlElement child = element.OwnerDocument.CreateElement(name);
            element.AppendChild(child);
            return child;
        }

        public static XmlElement WriteObjectProperties(this XmlElement element, object obj)
        {
            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (var item in pis)
            {
                object val = item.GetValue(obj, null);
                element.Attributes.Append(element.OwnerDocument.CreateAttribute(item.Name)).Value = val.ToString();
            }
            return element;
        }

        public static object ReadObjectProperties(this XmlElement element, object obj)
        {
           PropertyInfo[] pis = obj.GetType().GetProperties();
           foreach (var item in pis)
           {
               if (element.HasAttribute(item.Name))
               {
                   if (item.PropertyType == typeof(bool)) item.SetValue(obj, ObjectBase.GetBoolean(element, item.Name), null);
                   if (item.PropertyType == typeof(DateTime)) item.SetValue(obj, ObjectBase.GetDate(element, item.Name), null);
                   if (item.PropertyType == typeof(double)) item.SetValue(obj, ObjectBase.GetDouble(element, item.Name), null);
                   if (item.PropertyType == typeof(int)) item.SetValue(obj, ObjectBase.GetInteger(element, item.Name), null);
               }
           }
           return obj;
        }
    }
}
