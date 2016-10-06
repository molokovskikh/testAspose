using CommonFormat.Exceptions;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


namespace SomeFormat.Implementations.Xml
{
    public class XmlFile: AbstractSomeFormat
    {
        private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(SomeFormatXml));
     
        protected override void WriteTo(string filename, List<ISomeFormatRecord> records)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(filename))
            {
                SomeFormatXml someFormatXml = new SomeFormatXml(records);
                _xmlSerializer.Serialize(xmlWriter, someFormatXml);
            }
        }

        protected override List<ISomeFormatRecord> ReadFrom(string filename)
        {
            List<ISomeFormatRecord> result = null;

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


        protected override string GetTag()
        {
            return "SOMEFORMAT.XML";
        }

    }
}
