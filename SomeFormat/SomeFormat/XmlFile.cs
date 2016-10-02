using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFormat
{
    class XmlFile: ISomeFormat, ISomeFormatActions
    {
        public void Read(string filename)
        {
            throw new NotImplementedException();
        }

        public void Write(string filename)
        {
            throw new NotImplementedException();
        }

        public ISomeFormat Add(ISomeFormat record)
        {
            throw new NotImplementedException();
        }

        public void Modify(ISomeFormat record, int positionRecord)
        {
            throw new NotImplementedException();
        }

        public string Date
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string BrandName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Price
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CommonFormat.IFormat convert<T>() where T : CommonFormat.IFormat
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ISomeFormat> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
