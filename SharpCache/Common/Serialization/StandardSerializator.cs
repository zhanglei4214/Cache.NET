namespace SharpCache.Common.Serialization
{
    #region Using Directives
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    #endregion

    internal class StandardSerializator
    {
        public static byte[] Serialize(object value)
        {
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException("The type must be serializable");
            }

            if (ReferenceEquals(value, null))
            {
                throw new NullReferenceException("Souce cannot be null");
            }

            //Remove the namespaces
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            // Remove the <?xml version="1.0" encoding="utf-8"?>
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            StringWriter sw = new StringWriter();

            XmlWriter xmlWriter = XmlWriter.Create(sw, settings);

            XmlSerializer xml = new XmlSerializer(value.GetType());

            xml.Serialize(xmlWriter, value, ns);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(sw.ToString());
        }

        public static ISerializableCache Deserialize(byte[] value)
        {
            throw new NotImplementedException();
        }


    }
}
