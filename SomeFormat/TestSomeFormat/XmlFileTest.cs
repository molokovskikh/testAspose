using System;
using System.Text;
using System.IO;
using CommonFormat.Exceptions;
using SomeFormat;
using SomeFormat.Implementations.Xml;
using NUnit.Framework;




namespace TestSomeFormat
{
    [TestFixture]
    public class XmlFileTest
    {
        static private Encoding DefaultEncoding = Encoding.UTF8;

        //Generate filename
        private readonly string testFilename = Path.GetTempFileName();
                

        [TearDown]
        public void Terminate(){
            //Delete temporary file
            File.Delete(testFilename);
        }
        

        [Test]
        public void XmlFileRead()
        {
            File.WriteAllText(testFilename,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Document>
  <Car>
    <Date>10.10.2008</Date>
    <BrandName>Alpha Romeo Brera</BrandName>
    <Price>37000</Price>
  </Car>
</Document>
", DefaultEncoding);

            ISomeFormat someFormat = new XmlFile();
            someFormat.Read(testFilename);

            Assert.AreEqual(1,someFormat.Count());

            ISomeFormatRecord someFormatRecord = someFormat.Get(0);

            Assert.AreEqual("10.10.2008", someFormatRecord.Date);
            Assert.AreEqual("Alpha Romeo Brera", someFormatRecord.BrandName);            
            Assert.AreEqual(37000, someFormatRecord.Price);            
        }


        [Test]
        public void XmlFileReadWhenEmptyCars()
        {
            File.WriteAllText(testFilename,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Document>  
</Document>
", DefaultEncoding);

            ISomeFormat someFormat = new XmlFile();
            someFormat.Read(testFilename);

            Assert.AreEqual(0, someFormat.Count());            
        }

        [Test]
        public void XmlFileReadWhenGreatOneCar()
        {
            File.WriteAllText(testFilename,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Document>
  <Car>
    <Date>10.10.2008</Date>
    <BrandName>Alpha Romeo Brera</BrandName>
    <Price>37000</Price>
  </Car>
 <Car>
    <Date>16.11.2014</Date>
    <BrandName>Lamborghini Veneno Roadster</BrandName>
    <Price>5000000</Price>
  </Car>
</Document>
", DefaultEncoding);

            ISomeFormat someFormat = new XmlFile();
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
        public void XmlFileReadWherCorrupt()
        {
            File.WriteAllText(testFilename,
@"<?xml version=""1.0"" encoding=""utf-8""?>
<DocuBrandNamsssssssssssss Lamborghini Veneno Roadster</BrandName>
</Document>
", DefaultEncoding);

            ISomeFormat someFormat = new XmlFile();
            Assert.That(() => someFormat.Read(testFilename), Throws.TypeOf<CorruptedFormatException>());
        }


        [Test]
        public void XmlFileWrite()
        {
            ISomeFormat someFormatBefore = new XmlFile();
            ISomeFormatRecord someFormatRecordBefore = new SomeFormatRecordXml()
            {
                Date = "13.12.2016",
                BrandName = "Test data",
                Price = 12345678
            };
            someFormatBefore.Add(someFormatRecordBefore);
            someFormatBefore.Write(testFilename);


            ISomeFormat someFormatAfter = new XmlFile();
            someFormatAfter.Read(testFilename);

            Assert.AreEqual(someFormatBefore.Count(), someFormatAfter.Count());

            ISomeFormatRecord someFormatRecordAfter = someFormatAfter.Get(0);

            Assert.AreEqual(someFormatRecordBefore.Date, someFormatRecordAfter.Date);
            Assert.AreEqual(someFormatRecordBefore.BrandName, someFormatRecordAfter.BrandName);
            Assert.AreEqual(someFormatRecordBefore.Price, someFormatRecordAfter.Price);

        }
    }
}
