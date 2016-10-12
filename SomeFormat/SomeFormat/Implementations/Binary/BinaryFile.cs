using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CommonFormat.Exceptions;
using PackingNumber;
using CommonFormat;


namespace SomeFormat.Implementations.Binary
{
    public class BinaryFile: AbstractSomeFormat
    {
        /// <summary>
        /// Magic word header
        /// </summary>
        static readonly  byte [] HEADER = { 0x25, 0x26 };

      

        /// <summary>
        /// Implementation "Write to binary file"
        /// </summary>
        /// <param name="filename">filename destination</param>
        /// <param name="records">records to write</param>
        protected override void WriteTo(string filename, List<ISomeFormatRecord> records)
        {
            using (BinaryWriter b = new BinaryWriter(File.OpenWrite(filename)))
            {
                //Write header
                b.Write(HEADER);
                
                //Write count records
                b.Write(PackingOfNumberUtils.PackNumber(records.Count, 4, true));

                foreach(ISomeFormatRecord r in records)
                {
                    //public fixed byte Date[8];
                    //public fixed byte BrandNameLen[2];
                    //public byte [] BrandName;
                    //public fixed byte Price[4];
                    
                    _WriteDate(b, r.Date);
                    _WriteString(b, r.BrandName);
                    b.Write(PackingOfNumberUtils.PackNumber(r.Price, 4, true));
                }
                

            }
        }

      

        /// <summary>
        /// Implementation "Read from binary file"
        /// </summary>
        /// <param name="filename">filename source</param>
        /// <returns>records</returns>
        protected override List<ISomeFormatRecord> ReadFrom(string filename)
        {
            List<ISomeFormatRecord> result = null;
            using (BinaryReader b = new BinaryReader(File.OpenRead(filename)))
            {
                //public fixed byte Header[2];
                //public fixed byte Count[4];
                byte [] header = b.ReadBytes(HEADER.Length);
                int countRecords = b.ReadInt32();

                if(  header[0] != HEADER[0] 
                  || header[1] != HEADER[1])
                    throw new CorruptedFormatException();
                
                if(b.PeekChar()>0 && countRecords == 0)
                    throw new CorruptedFormatException();
                
                //public fixed byte Date[8];
                //public fixed byte BrandNameLen[2];
                //public byte [] BrandName;
                //public fixed byte Price[4];

                while(b.PeekChar()>0)
                {
                    try
                    {
                        string date = _ReadDate(b);
                        string brandName = _ReadString(b);
                        int price = b.ReadInt32();

                        ISomeFormatRecord record = CreateRecord(
                            date,
                            brandName,
                            price
                        );

                        result = result ?? new List<ISomeFormatRecord>();
                        result.Add(record);
                    }
                    catch
                    {
                        throw new CorruptedFormatException();
                    }
                }

                if(countRecords > 0
                    && (result == null || result.Count != countRecords) )
                    throw new CorruptedFormatException();

            }

            return result;
        }



        #region Reading and writing of data, such as date and string from the description format

        private string _ReadDate(BinaryReader b)
        {
            StringBuilder strDate = new StringBuilder();              
            for(int i=0; i < 8 ;i++)
            {
                strDate.AppendFormat("{0}{1}", 
                    b.ReadByte(),
                    i == 1 || i==3 ? "." : string.Empty);
            }
            
            return strDate.ToString();
        }


        private void _WriteDate(BinaryWriter b, string strDate)
        {
            byte digit;

            strDate =  strDate.Trim();
            
            if(strDate.Length != 10) {
                throw new CorruptedFormatException();
            }

            for (int i = 0; i < 10; i++)
            {                
                if (i != 2 && i != 5)
                {                    
                    
                    if (byte.TryParse(strDate.Substring(i,1),  out digit))
                        b.Write(digit);
                    else
                        throw new CorruptedFormatException();
                }
            }                        
        }


        private string _ReadString(BinaryReader b)
        {
            short strLen = b.ReadInt16();
            byte [] strBytes = b.ReadBytes(strLen * 2);
            return Encoding.Unicode.GetString(strBytes);
        }

        private void _WriteString(BinaryWriter b, string str)
        {
            byte[] bLength = PackingOfNumberUtils.PackNumber(str.Length, 2, true);
            b.Write(bLength);
            b.Write(Encoding.Unicode.GetBytes(str));            
        }

        #endregion


        protected override string GetTag()
        {
            return "SOMEFORMAT.BINARY";
        }

        public override R Convert<R>()
        {
            R result = (R) Activator.CreateInstance(typeof(R));

            //If is compatible format
            if ("SOMEFORMAT.XML".Equals(result.Tag))
            {
                IFormat<ISomeFormatRecord> resultActions = result as IFormat<ISomeFormatRecord>;
                for (int i = 0; i < Count(); i++)
                {
                    ISomeFormatRecord resultRecord = CloneRecord(Get(i));
                    if (resultActions != null)
                        resultActions.Add(resultRecord);
                }
                return result;
            }


            //In all others cases, throw exception
            throw new IncompatibleFormatException();
        }
    }
}
