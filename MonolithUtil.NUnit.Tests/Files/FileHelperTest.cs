using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MonolithUtil.Files;

namespace MonolithUtil.NUnit.Tests.Files
{
    [TestFixture]
    public class FileHelperTest
    {
        [Test]
        public void FileHelper_00001_CopySafe()
        {
            var executablePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            var filePath = Path.Combine(executablePath, "hogehoge.temp");
            var destPath = Path.Combine(executablePath, "mogemoge.temp");

            using (var stream = File.Create(filePath)) { }

            var result = FileHelper.CopySafe(filePath, destPath);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void FileHelper_00002_CopySafeOverWrite()
        {
            var executablePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            var filePath = Path.Combine(executablePath, "hogehoge.temp");
            var destPath = Path.Combine(executablePath, "mogemoge.temp");

            using (var stream = File.Create(filePath)) { }

            using (var stream = File.Create(destPath)) { }

            var result = FileHelper.CopySafe(filePath, destPath);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value);
        }
    }
}
