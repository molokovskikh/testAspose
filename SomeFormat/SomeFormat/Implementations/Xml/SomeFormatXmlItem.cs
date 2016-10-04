using CommonFormat.Exceptions;
using System;
using System.Xml.Serialization;

namespace SomeFormat.Implementations.Xml
{
    [Serializable]
    public class SomeFormatXmlItem : ISomeFormat
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
        
        public string Tag
        {
            get { return "SOMEFORMAT.XML"; }
        }
    }
}
