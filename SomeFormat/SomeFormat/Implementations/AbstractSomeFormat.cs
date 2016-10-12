using System.Collections.Generic;
using CommonFormat;

namespace SomeFormat.Implementations
{
    public abstract class AbstractSomeFormat: ISomeFormat
    {
        #region Private code


        /// <summary>
        /// Mock, just implemention ISomeFormatRecord
        /// </summary>
        class SomeFormatRecord : ISomeFormatRecord
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



        private List<ISomeFormatRecord> _records;

        private void _InitRecords()
        {
            this._records = this._records ?? new List<ISomeFormatRecord>();
        }

        private bool _IsEmptyRecords()
        {
            return this._records == null;
        }

      

        #endregion



        #region ISomeFormat Implementation

        public long Count()
        {
            return _IsEmptyRecords() ? 0 : this._records.Count;
        }


        public void Write(string filename)
        {
            if (_IsEmptyRecords()) return;

            WriteTo(filename, this._records);          
        }

        public void Read(string filename)
        {
            this._records = ReadFrom(filename);            
        }

        public long Add(ISomeFormatRecord record)
        {
            _InitRecords();

            this._records.Add(record);

            return Count() - 1;
        }

        public void Modify(ISomeFormatRecord record, int positionRecord)
        {
            if (!_IsEmptyRecords() && positionRecord < Count())
            {
                this._records[positionRecord] = record;
            }
        }

        public ISomeFormatRecord Get(int positionRecord)
        {
            return !_IsEmptyRecords() && positionRecord < Count()
                ? this._records[positionRecord]
                : null;

        }


        public string Tag
        {
            get
            {
                return GetTag();
            }
        }

        #endregion



        #region For inheritors

        protected abstract void WriteTo(string filename,List<ISomeFormatRecord> records);
        protected abstract List<ISomeFormatRecord> ReadFrom(string filename);

        
        public abstract R Convert<R>() where R : IFormat;


        /// <summary>
        /// Get Tag Format
        /// </summary>
        /// <returns></returns>
        protected abstract string GetTag();
      


        /// <summary>
        /// Create instance implementing ISomeFormatRecord
        /// </summary>
        /// <param name="date"></param>
        /// <param name="brandName"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        protected ISomeFormatRecord CreateRecord(string date, string brandName, int price)
        {
            return new SomeFormatRecord
            {
                Date = date,
                BrandName = brandName,
                Price = price
            };
        }

        /// <summary>
        /// Clone instance implementing ISomeFormatRecord
        /// </summary>
        /// <param name="date"></param>
        /// <param name="brandName"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        protected ISomeFormatRecord CloneRecord(ISomeFormatRecord source)
        {
            return CreateRecord(source.Date, source.BrandName, source.Price);
        }

        #endregion

    }
}
