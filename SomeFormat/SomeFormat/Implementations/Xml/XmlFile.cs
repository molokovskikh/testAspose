using CommonFormat.Exceptions;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SomeFormat.Implementations.Xml
{
    public class XmlFile: AbstractSomeFormatActions
    {
        private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(SomeFormatXml));
     
        protected override void WriteTo(string filename, List<ISomeFormat> records)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(filename))
            {
                _xmlSerializer.Serialize(xmlWriter, records);
            }
        }

        protected override List<ISomeFormat> ReadFrom(string filename)
        {
            List<ISomeFormat> result = null;

            using (XmlReader xmlReader = XmlReader.Create(filename))
            {
                try
                {
                    SomeFormatXml someFormatXml = (SomeFormatXml)_xmlSerializer.Deserialize(xmlReader);
                    result = someFormatXml.getRecords();
                }
                catch (Exception e)
                {
                    if (e is InvalidOperationException)
                    {
                        throw new CorruptedFormatException();
                    }
                }
            }

            return result;
        }
    }
}
