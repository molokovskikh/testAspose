using System;
using System.Text;
using System.IO;
using CommonFormat.Exceptions;
using SomeFormat;
using SomeFormat.Implementations.Xml;
using NUnit.Framework;
using SomeFormat.Implementations.Binary;




namespace TestSomeFormat
{
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

            ISomeFormatActions someFormatActions = new BinaryFile();
            someFormatActions.Read(testFilename);

            Assert.AreEqual(1,someFormatActions.Count());

            ISomeFormat someFormat = someFormatActions.Get(0);

            Assert.AreEqual("10.10.2008", someFormat.Date);
            Assert.AreEqual("Alpha Romeo Brera", someFormat.BrandName);            
            Assert.AreEqual(37000, someFormat.Price);            
        }


        [Test]
        public void XmlFileReadWhenEmptyCars()
        {            
            ISomeFormatActions someFormatActions = new BinaryFile();
            someFormatActions.Read(testFilename);

            Assert.AreEqual(0, someFormatActions.Count());            
        }

        [Test]
        public void XmlFileReadWhenGreatOneCar()
        {
           

            ISomeFormatActions someFormatActions = new BinaryFile();
            someFormatActions.Read(testFilename);

            Assert.AreEqual(2, someFormatActions.Count());

            ISomeFormat someFormat = someFormatActions.Get(0);

            Assert.AreEqual("10.10.2008", someFormat.Date);
            Assert.AreEqual("Alpha Romeo Brera", someFormat.BrandName);
            Assert.AreEqual(37000, someFormat.Price);

            ISomeFormat someFormat2 = someFormatActions.Get(1);

            Assert.AreEqual("16.11.2014", someFormat2.Date);
            Assert.AreEqual("Lamborghini Veneno Roadster", someFormat2.BrandName);
            Assert.AreEqual(5000000, someFormat2.Price);
        }

        [Test]        
        public void BinaryFileReadWherCorrupt()
        {            
            ISomeFormatActions someFormatActions = new BinaryFile();
            Assert.That(() => someFormatActions.Read(testFilename), Throws.TypeOf<CorruptedFormatException>());
        }
    }
}
