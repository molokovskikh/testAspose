using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CommonFormat;
using CommonFormat.Exceptions;

namespace ExampleOtherFormat
{
    public class OtherFormat: IFormat<OtherFormatRecord>
    {
        private readonly BinaryFormatter formatter = new BinaryFormatter();

        public void Read(string filename)
        {
            using(FileStream b = File.OpenRead(filename))
            {
               // this.Records = formatter.Deserialize(b);
            }
        }

        public void Write(string filename)
        {
            using (FileStream b = File.OpenWrite(filename))
            {
                //formatter.Serialize(b,this.Records);
            }
        }

        public long Add(OtherFormatRecord record)
        {
            throw new NotImplementedException();
        }

        public void Modify(OtherFormatRecord record, int positionRecord)
        {
            throw new NotImplementedException();
        }

        public OtherFormatRecord Get(int positionRecord)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }

        public R Convert<R>() where R : IFormat<OtherFormatRecord>
        {
            R result = default(R);
            
            return result;
        }

        public string Tag
        {
            get { throw new NotImplementedException(); }
        }
    }
}
