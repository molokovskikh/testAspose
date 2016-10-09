using System;
using System.Text;
using System.IO;
using CommonFormat.Exceptions;
using SomeFormat;
using SomeFormat.Implementations.Xml;
using SomeFormat.Implementations.Binary;
using NUnit.Framework;





namespace TestSomeFormat
{
    [TestFixture]
    public class ConvertFilesTest
    {
        static private Encoding DefaultEncoding = Encoding.UTF8;

        //Generate filenames
        private readonly string testXmlFilename = Path.GetTempFileName();
        private readonly string testBinaryFilename = Path.GetTempFileName();

        [TearDown]
        public void Terminate(){
            //Delete temporary files
            File.Delete(testXmlFilename);
            File.Delete(testBinaryFilename);
        }
        

        [Test]
        public void XmlFileToBinaryFile()
        {
            File.WriteAllText(testXmlFilename,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Document>
  <Car>
    <Date>10.10.2008</Date>
    <BrandName>Alpha Romeo Brera</BrandName>
    <Price>37000</Price>
  </Car>
</Document>
", DefaultEncoding);

            ISomeFormat xmlFormat = new XmlFile();
            xmlFormat.Read(testXmlFilename);

            ISomeFormat binaryFormat = xmlFormat.Convert<BinaryFile>();

            Assert.AreEqual(xmlFormat.Count(), binaryFormat.Count());

            for (int i = 0; i < xmlFormat.Count(); i++)
            {
                ISomeFormatRecord xmlRecord = xmlFormat.Get(i);
                ISomeFormatRecord binaryRecord = binaryFormat.Get(i);
                
                Assert.AreEqual(xmlRecord.Date, binaryRecord.Date);
                Assert.AreEqual(xmlRecord.BrandName, binaryRecord.BrandName);
                Assert.AreEqual(xmlRecord.Price, binaryRecord.Price);
            }
        }


        [Test]
        public void BinaryFileToXmlFile()
        {

            File.WriteAllBytes(testBinaryFilename, new byte[] {
                0x25, 0x26, //Header
                0x02, 0x00, 0x00, 0x00, //Count Records
                //First record
                0x01, 0x00, 0x01, 0x00, 0x02, 0x00, 0x00, 0x08, //Date
                0x11, 0x00,//Brand Name Len [2] (value = 17)
                0x41, 0x00, //1
                0x6c, 0x00, //2
                0x70, 0x00, //3
                0x68, 0x00, //4
                0x61, 0x00, //5
                0x20, 0x00, //6
                0x52, 0x00, //7
                0x6f, 0x00, //8
                0x6d, 0x00, //9
                0x65, 0x00, //10
                0x6f, 0x00, //11
                0x20, 0x00, //12
                0x42, 0x00, //13
                0x72, 0x00, //14
                0x65, 0x00, //15
                0x72, 0x00, //16
                0x61, 0x00, //17
                //Brand Name End

                0x88, 0x90, 0x00, 0x00, //Price
                // End first record

                0x01, 0x06, 0x01, 0x01, 0x02, 0x00, 0x01, 0x04, //Date
                0x1B, 0x00,//Brand Name Len [2] (value = 27)
                //Two record
                0x4c, 0x00,
                0x61, 0x00,
                0x6d, 0x00,
                0x62, 0x00,
                0x6f, 0x00,
                0x72, 0x00,
                0x67, 0x00,
                0x68, 0x00,           
                0x69, 0x00,
                0x6e, 0x00,
                0x69, 0x00,
                0x20, 0x00,
                0x56, 0x00,
                0x65, 0x00,
                0x6e, 0x00,
                0x65, 0x00,
                0x6e, 0x00,
                0x6f, 0x00,
                0x20, 0x00,
                0x52, 0x00,
                0x6f, 0x00,
                0x61, 0x00,
                0x64, 0x00,
                0x73, 0x00,
                0x74, 0x00,
                0x65, 0x00,
                0x72, 0x00,
                //Brand Name End

               0x40, 0x4B, 0x4C, 0x00, //Price
                // End two record
            });

            ISomeFormat binaryFormat = new BinaryFile();
            binaryFormat.Read(testBinaryFilename);

            ISomeFormat xmlFormat = binaryFormat.Convert<XmlFile>();

            Assert.AreEqual(binaryFormat.Count(), xmlFormat.Count());

            for (int i = 0; i < binaryFormat.Count(); i++)
            {
                ISomeFormatRecord binaryRecord = binaryFormat.Get(i);
                ISomeFormatRecord xmlRecord = xmlFormat.Get(i);

                Assert.AreEqual(binaryRecord.Date, xmlRecord.Date);
                Assert.AreEqual(binaryRecord.BrandName, xmlRecord.BrandName);
                Assert.AreEqual(binaryRecord.Price, xmlRecord.Price);
            }
        }     
     
    }
}
