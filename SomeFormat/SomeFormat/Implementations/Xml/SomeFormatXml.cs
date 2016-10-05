using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SomeFormat.Implementations.Xml
{
    [Serializable, XmlRoot("Document")]
    public class SomeFormatXml
    {               
        [XmlElement("Car")]
        public List<SomeFormatRecordXml> Items { get; set; }

        internal List<ISomeFormatRecord> getRecords()
        {            
            return Items.Select(i => (ISomeFormatRecord) i).ToList<ISomeFormatRecord>();
        }
    }
}
