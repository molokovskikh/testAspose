using CommonFormat;
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


        public override R Convert<R>()
        {            
            R result = (R)Activator.CreateInstance(typeof(R));
            
            //If is compatible format
            if("SOMEFORMAT.BINARY".Equals(result.Tag))
            {
                IFormat<ISomeFormatRecord> resultActions = result as IFormat<ISomeFormatRecord>;
                
                for (int i = 0; i < Count(); i++)
                {
                    ISomeFormatRecord resultRecord = CloneRecord(Get(i));
                    if(resultActions != null)
                        resultActions.Add(resultRecord);
                }
                return result;
            }


            //In all others cases, throw exception
            throw new IncompatibleFormatException();
        }
    }
}
