using System;
using NUnit.Framework;
using PackingNumber;

namespace TestPackingNumber
{
     [TestFixture]
    public class PackingNumberUtilsTest
    {

         [Test]
         public void testPackLong()
         {
             long num = 123456789;
             byte[] bnum = PackingOfNumberUtils.PackNumber(num, 8);
             Assert.AreEqual(0x00, bnum[0]);
             Assert.AreEqual(0x00, bnum[1]);
             Assert.AreEqual(0x00, bnum[2]);
             Assert.AreEqual(0x00, bnum[3]);
             Assert.AreEqual(0x07, bnum[4]);
             Assert.AreEqual(0x5B, bnum[5]);
             Assert.AreEqual(0xCD, bnum[6]);
             Assert.AreEqual(0x15, bnum[7]);
         }
       

        [Test]
        public void testPackLongReverse()
        {
            long num = 123456789;
            byte[] bnum = PackingOfNumberUtils.PackNumber(num, 8, true);
            Assert.AreEqual(0x15, bnum[0]);
            Assert.AreEqual(0xCD, bnum[1]);
            Assert.AreEqual(0x5B, bnum[2]);
            Assert.AreEqual(0x07, bnum[3]);
            Assert.AreEqual(0x00, bnum[4]);
            Assert.AreEqual(0x00, bnum[5]);
            Assert.AreEqual(0x00, bnum[6]);
            Assert.AreEqual(0x00, bnum[7]);

        }



        [Test]
        public void testPackInt()
        {
            long num = 123456789;
            byte [] bnum = PackingOfNumberUtils.PackNumber(num, 4);
            Assert.AreEqual(0x07, bnum[0]);
            Assert.AreEqual(0x5B, bnum[1]);
            Assert.AreEqual(0xCD, bnum[2]);
            Assert.AreEqual(0x15, bnum[3]);

        }


        [Test]
        public void testPackIntReverse()
        {
            long num = 123456789;
            byte[] bnum = PackingOfNumberUtils.PackNumber(num, 4, true);
            Assert.AreEqual(0x15, bnum[0]);
            Assert.AreEqual(0xCD, bnum[1]);
            Assert.AreEqual(0x5B, bnum[2]);
            Assert.AreEqual(0x07, bnum[3]);            
        }



        [Test]
        public void testPackShort()
        {
            long num = 1883;
            byte[] bnum = PackingOfNumberUtils.PackNumber(num, 2);
            Assert.AreEqual(0x07, bnum[0]);
            Assert.AreEqual(0x5B, bnum[1]);

        }


        [Test]
        public void testPackShortReverse()
        {
            long num = 1883;
            byte[] bnum = PackingOfNumberUtils.PackNumber(num, 2, true);            
            Assert.AreEqual(0x5B, bnum[0]);
            Assert.AreEqual(0x07, bnum[1]);
        }


        [Test]
        public void testPackByte()
        {
            long num = 203;
            byte[] bnum = PackingOfNumberUtils.PackNumber(num, 1);
            Assert.AreEqual(0xCB, bnum[0]);            
        }




        [Test]
        public void testUnpackInt()
        {

            long num = PackingOfNumberUtils.UnpackNumber(new byte[]
              {
                  0x15,
                  0xCD,
                  0x5B,
                  0x07,                  
              });

            Assert.AreEqual(123456789, num);
        }

            
        [Test]
        public void testUnpackIntReverse()
        {           
            long num =  PackingOfNumberUtils.UnpackNumber(new byte[]
              {
                  0x07,
                  0x5B,
                  0xCD,
                  0x15                  
              },true);


            Assert.AreEqual(123456789, num);

            
        }


        [Test]
        public void testUnpackShort()
        {

            byte [] te = PackingOfNumberUtils.PackNumber(43981, 2);

            long num = PackingOfNumberUtils.UnpackNumber(new byte[]
              {                 
                  0xCD,                
                  0xAB,
              });

            Assert.AreEqual(43981, num);
        }


        [Test]
        public void testUnpackShortReverse()
        {           
            long num =  PackingOfNumberUtils.UnpackNumber(new byte[]
              {
                  0xAB,
                  0xCD                 
              },true);


            Assert.AreEqual(43981, num);
            
        }

    }
}
