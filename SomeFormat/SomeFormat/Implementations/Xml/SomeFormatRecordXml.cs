using CommonFormat.Exceptions;
using System;
using System.Xml.Serialization;

namespace SomeFormat.Implementations.Xml
{
    [Serializable]
    public class SomeFormatRecordXml : ISomeFormatRecord
    {
        public string Date
        {
            get;
            set;
        }

        public string BrandName
        {
            get;
            set;
        }

        public int Price
        {
            get;
            set;
        }
                
    }
}
