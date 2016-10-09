using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib.Test
{
    [TestClass]
    public class TestOneFile
    {
        [TestMethod]
        public void TestOneWord()
        {
            var fileSource = new FileNameSourceStub();
            var fileDir = "..\\..\\";
            fileSource.Add(fileDir + "OneWord..txt");

            Assert.AreEqual(1, fileSource.FileNames.Count, "count");
        }
    }
}
