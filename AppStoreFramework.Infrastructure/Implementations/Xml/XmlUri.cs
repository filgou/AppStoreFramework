using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AppStoreFramework.Infrastructure.Implementations.Xml
{
    public class XmlUri : IXmlSerializable
    {
        private Uri value;

        public XmlUri() { }
        public XmlUri(Uri source) { this.value = source; }

        public static implicit operator Uri(XmlUri o)
        {
            return o == null ? null : o.value;
        }

        public static implicit operator XmlUri(Uri o)
        {
            return o == null ? null : new XmlUri(o);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            this.value = new Uri(reader.ReadElementContentAsString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(this.value.ToString());
        }
    }
}
