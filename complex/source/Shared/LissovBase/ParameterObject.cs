using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ModelBase;

namespace LissovBase
{
    public class ParameterObject<T> : ParameterSafe
        where T : class
    {
        public T Content;
        public ParameterObject(string name, string displayname, T content)
            : base(name, displayname)
        {
            Content = content;
        }

        public ParameterObject(string displayname, T content)
            : this(displayname.ToIdentifier(true), displayname, content)
        {
        }

        public override object FromXml(XmlElement currentElement)
        {
            XmlElement xe = currentElement.ChildNodes[0] as XmlElement;
            if (Content is ObjectBase)
                (Content as ObjectBase).FromXml(xe);
            else
                xe.ReadObjectProperties(Content);
            this.Value = 0;
            return this;
        }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            XmlElement xe = base.ToXml(parentElement);
            if (Content is ObjectBase)
                (Content as ObjectBase).ToXml(xe);
            else
                xe.WriteObjectProperties(Content);
            return xe;
        }

        public static implicit operator T(ParameterObject<T> value)
        {
            return value.Content;
        }
    }
}
