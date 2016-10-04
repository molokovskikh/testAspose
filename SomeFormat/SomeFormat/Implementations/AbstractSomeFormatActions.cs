using CommonFormat.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFormat.Implementations
{
    public abstract class AbstractSomeFormatActions: ISomeFormatActions
    {
        #region Private code
       
        private List<ISomeFormat> _records;

        private void _InitRecords()
        {
            this._records = this._records ?? new List<ISomeFormat>();
        }

        private bool _IsEmptyRecords()
        {
            return this._records == null;
        }

        #endregion



        #region ISomeFormatActions Implementation

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

        public long Add(ISomeFormat record)
        {
            _InitRecords();

            this._records.Add(record);

            return Count() - 1;
        }

        public void Modify(ISomeFormat record, int positionRecord)
        {
            if (!_IsEmptyRecords() && positionRecord < Count())
            {
                this._records[positionRecord] = record;
            }
        }

        public ISomeFormat Get(int positionRecord)
        {
            return !_IsEmptyRecords() && positionRecord < Count()
                ? this._records[positionRecord]
                : null;

        }

        #endregion



        #region For inheritors

        protected abstract void WriteTo(string filename,List<ISomeFormat> records);
        protected abstract List<ISomeFormat> ReadFrom(string filename);

        #endregion


        public R Convert<R>() where R : IFormatActions<ISomeFormat>
        {
            //TODO Release function conversion XML <> Binary
            //return default(R);

            throw new IncompatibleFormatException();
            
        }
    }
}
