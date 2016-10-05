using CommonFormat.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFormat.Implementations
{
    public abstract class AbstractSomeFormat: ISomeFormat
    {
        #region Private code
       
        private List<ISomeFormatRecord> _records;

        private void _InitRecords()
        {
            this._records = this._records ?? new List<ISomeFormatRecord>();
        }

        private bool _IsEmptyRecords()
        {
            return this._records == null;
        }

        /// <summary>
        /// Get Tag Format
        /// </summary>
        /// <returns></returns>
        protected abstract string GetTag();

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

        #endregion



        #region For inheritors

        protected abstract void WriteTo(string filename,List<ISomeFormatRecord> records);
        protected abstract List<ISomeFormatRecord> ReadFrom(string filename);

        
        public R Convert<R>() where R : IFormat<ISomeFormatRecord>
        {
            //TODO Release function conversion XML <> Binary
            //return default(R);

            throw new IncompatibleFormatException();
            
        }

        public string Tag 
        { 
            get 
            {
                return GetTag();
            }
        }

        #endregion

       
        
    }
}
