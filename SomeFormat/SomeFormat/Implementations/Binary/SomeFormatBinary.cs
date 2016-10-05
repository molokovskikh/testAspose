using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFormat.Implementations.Binary
{
    class SomeFormatRecordBinary: ISomeFormatRecord
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
