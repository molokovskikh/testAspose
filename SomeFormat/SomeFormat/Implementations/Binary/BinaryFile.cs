using System.IO;
using System.Collections.Generic;
using CommonFormat.Exceptions;
using System.Text;
using System;


namespace SomeFormat.Implementations.Binary
{
    public class BinaryFile: AbstractSomeFormat
    {
        /// <summary>
        /// Magic word header
        /// </summary>
        static readonly  byte [] HEADER = { 0x25, 0x26 };

        protected override void WriteTo(string filename, List<ISomeFormatRecord> records)
        {
          
        }

        class SomeFormatRecord: ISomeFormatRecord
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

                        ISomeFormatRecord record = new SomeFormatRecord
                        {
                            Date = date,
                            BrandName = brandName,
                            Price = price
                        };

                        result = result ?? new List<ISomeFormatRecord>();
                        result.Add(record);
                    }
                    catch (Exception e)
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

        private string _ReadString(BinaryReader b)
        {
            short strLen = b.ReadInt16();
            byte [] strBytes = b.ReadBytes(strLen * 2);
            return Encoding.Unicode.GetString(strBytes);
        }

        

        protected override string GetTag()
        {
            return "SOMEFORMAT.BINARY";
        }
    }
}
