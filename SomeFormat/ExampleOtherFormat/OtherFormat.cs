using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CommonFormat;
using CommonFormat.Exceptions;
using System.Collections.Generic;
using System.Text;
using SomeFormat;

namespace ExampleOtherFormat
{
    [Serializable]
    public class OtherFormat: IFormat
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;
        private List<string> brandNameList = new List<string>();

        public void Read(string filename)
        {            
            string [] csvLines = File.ReadAllLines(filename, ENCODING);

            brandNameList.Clear();

            foreach (string line in csvLines)
            {
                string [] values = line.Split(new char[] { ';' });
                if (values.Length > 5)
                {
                    string brandName = values[5];

                    brandNameList.Add(brandName);
                }
            }            
        }

        public void Write(string filename)
        {
            foreach(string brandName in brandNameList)
            {
                using(FileStream f = File.OpenWrite(filename))
                {
                    string line = string.Format(";;;;;{0}", brandName);

                    byte [] bytesToWrite = ENCODING.GetBytes(line);

                    f.Write(bytesToWrite, 0, bytesToWrite.Length);
                }
            }            
        }

        public long Add(IFormatRecord record)
        {
            throw new NotImplementedException();
        }

        public void Modify(IFormatRecord record, int positionRecord)
        {
            throw new NotImplementedException();
        }

        public IFormatRecord Get(int positionRecord)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }


        public R Convert<R>() where R : IFormat
        {
            R result = (R)Activator.CreateInstance(typeof(R));

            //If is compatible format
            if ("SOMEFORMAT.XML".Equals(result.Tag))
            {
                ISomeFormat someFormat = result as ISomeFormat;
                if (someFormat != null)
                {
                    foreach (string brandName in brandNameList)
                    {
                        ISomeFormatRecord someFormatRecord = new SomeFormatRecord() { BrandName = brandName };
                        
                        someFormat.Add(someFormatRecord);
                    }
                    return result;
                }
            }


            //In all others cases, throw exception
            throw new IncompatibleFormatException();
        }

        public string Tag
        {
            get
            {
                return "OTHERFORMAT.CSV";
            }
        }
    }
}
