using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CommonFormat;
using CommonFormat.Exceptions;
using System.Collections.Generic;

namespace ExampleOtherFormat
{
    [Serializable]
    public class OtherFormat: IFormat
    {
        
        public void Read(string filename)
        {
            using(FileStream b = File.OpenRead(filename))
            {
               
            }
        }

        public void Write(string filename)
        {
            using (FileStream b = File.OpenWrite(filename))
            {
             
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
                for (int i = 0; i < Count(); i++)
                {

                }
                return result;
            }


            //In all others cases, throw exception
            throw new IncompatibleFormatException();
        }

        public string Tag
        {
            get
            {
                return "OTHERFORMAT.BIN";
            }
        }
    }
}
