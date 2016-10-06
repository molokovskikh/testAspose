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

    class SomeFormatRecordStub : ISomeFormatRecord
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



    [TestFixture]
    public class BinaryFileTest
    {
        //Generate filename
        private readonly string testFilename = Path.GetTempFileName();  

        [TearDown]
        public void Terminate(){
            //Delete temporary file            
            File.Delete(testFilename);
        }
        

        [Test]
        public void BinaryFileRead()
        {
            File.WriteAllBytes(testFilename, new byte[] {
                0x25, 0x26, //Header
                0x01, 0x00, 0x00, 0x00, //Count Records
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

                0x88, 0x90, 0x00, 0x00 //Price
                // End first record
            });

            ISomeFormat someFormat = new BinaryFile();
            someFormat.Read(testFilename);

            Assert.AreEqual(1,someFormat.Count());

            ISomeFormatRecord someFormatRecord = someFormat.Get(0);

            Assert.AreEqual("10.10.2008", someFormatRecord.Date);
            Assert.AreEqual("Alpha Romeo Brera", someFormatRecord.BrandName);            
            Assert.AreEqual(37000, someFormatRecord.Price);            
        }


        [Test]
        public void BinaryFileReadWhenEmptyCars()
        {
            File.WriteAllBytes(testFilename, new byte[] {
                0x25, 0x26, //Header
                0x00, 0x00, 0x00, 0x00 //Count Records               
            });

            ISomeFormat someFormat = new BinaryFile();
            someFormat.Read(testFilename);

            Assert.AreEqual(0, someFormat.Count());            
        }

        [Test]
        public void BinaryFileReadWhenGreatOneCar()
        {
            File.WriteAllBytes(testFilename, new byte[] {
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

            ISomeFormat someFormat = new BinaryFile();
            someFormat.Read(testFilename);

            Assert.AreEqual(2, someFormat.Count());

            ISomeFormatRecord someFormatRecord = someFormat.Get(0);

            Assert.AreEqual("10.10.2008", someFormatRecord.Date);
            Assert.AreEqual("Alpha Romeo Brera", someFormatRecord.BrandName);
            Assert.AreEqual(37000, someFormatRecord.Price);

            ISomeFormatRecord someFormat2 = someFormat.Get(1);

            Assert.AreEqual("16.11.2014", someFormat2.Date);
            Assert.AreEqual("Lamborghini Veneno Roadster", someFormat2.BrandName);
            Assert.AreEqual(5000000, someFormat2.Price);
        }

        [Test]        
        public void BinaryFileReadWherCorrupt()
        {
            File.WriteAllBytes(testFilename, new byte[] {
                0x25, 0x26, //Header
                0x02, 0x00, 0x00, 0x00, //Count Records
             });

            ISomeFormat someFormatActions = new BinaryFile();
            Assert.That(() => someFormatActions.Read(testFilename), Throws.TypeOf<CorruptedFormatException>());
        }



        [Test]
        public void BinaryFileWrite()
        {
            ISomeFormat someFormatBefore = new BinaryFile();
            ISomeFormatRecord someFormatRecordBefore = new SomeFormatRecordStub() 
            {
                Date = "13.12.2016",
                BrandName = "Test data",
                Price = 12345678
            };
            someFormatBefore.Add(someFormatRecordBefore);
            someFormatBefore.Write(testFilename);


            ISomeFormat someFormatAfter = new BinaryFile();
            someFormatAfter.Read(testFilename);

            Assert.AreEqual(someFormatBefore.Count(), someFormatAfter.Count());

            ISomeFormatRecord someFormatRecordAfter = someFormatAfter.Get(0);

            Assert.AreEqual(someFormatRecordBefore.Date, someFormatRecordAfter.Date);
            Assert.AreEqual(someFormatRecordBefore.BrandName, someFormatRecordAfter.BrandName);
            Assert.AreEqual(someFormatRecordBefore.Price, someFormatRecordAfter.Price);

        }
    }
}
