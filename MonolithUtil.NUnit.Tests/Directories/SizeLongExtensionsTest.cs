using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonolithUtil.Directories;

namespace MonolithUtil.NUnit.Tests
{
    [TestFixture]
    public class SizeLongExtensionsTest
    {
        [Test]
        public void TEST00001_To100MB()
        {
            //100MB分のバイトを用意
            long b = 104857600;
            long mb = 100;

            var converted = b.FileSizeUnitFormat("MB");
            Assert.AreEqual(100, converted);

            var longConverted = converted.ToInt64MegaByte();

            Assert.AreEqual(longConverted, mb);
        }

        [Test]
        public void TEST00001_ToMB()
        {
            //適当な分のバイトを用意
            long b = 29952259;

            var converted = b.FileSizeUnitFormat("MB");
            Assert.AreEqual(28, converted);
        }
    }
}
