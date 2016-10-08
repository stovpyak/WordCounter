using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib.Test
{
    [TestClass]
    public class TestAppl
    {
        const string FileDir = "..\\..\\..\\TestData\\";

        [TestMethod]
        public void TestOneWord()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "OneWord..txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(1, resultSaver.Items.Count, "words count");
            var wordCount = resultSaver.Items.First();
            Assert.AreEqual("one", wordCount.Word, "word");
            Assert.AreEqual(1, wordCount.Count, "count");
        }

        [TestMethod]
        public void TestTwoEqualsWord()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "TwoEqualsWords.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(1, resultSaver.Items.Count, "words count");
            var wordCount = resultSaver.Items.First();
            Assert.AreEqual("one", wordCount.Word, "word");
            Assert.AreEqual(2, wordCount.Count, "count");
        }

        [TestMethod]
        public void TestTwoEqualsWordTwoLines()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "TwoEqualsWordsTwoLines.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(1, resultSaver.Items.Count, "words count");
            var wordCount = resultSaver.Items.First();
            Assert.AreEqual("one", wordCount.Word, "word");
            Assert.AreEqual(2, wordCount.Count, "count");
        }

        [TestMethod]
        public void TestTwoDifferentWord()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "TwoDifferentWords.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(2, resultSaver.Items.Count, "words count");
            var wordCount1 = resultSaver.Items.First();
            Assert.AreEqual("one", wordCount1.Word, "word one");
            Assert.AreEqual(1, wordCount1.Count, "count one");

            var wordCount2 = resultSaver.Items[1];
            Assert.AreEqual("two", wordCount2.Word, "word two");
            Assert.AreEqual(1, wordCount2.Count, "count two");
        }

        [TestMethod]
        public void TestTwoFiles()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "TwoDifferentWords.txt");
            fileSource.Add(FileDir + "OneWord..txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(2, resultSaver.Items.Count, "words count");
            var wordCount1 = resultSaver.Items.First();
            Assert.AreEqual("one", wordCount1.Word, "word one");
            Assert.AreEqual(2, wordCount1.Count, "count one");

            var wordCount2 = resultSaver.Items[1];
            Assert.AreEqual("two", wordCount2.Word, "word two");
            Assert.AreEqual(1, wordCount2.Count, "count two");
        }

        [TestMethod]
        public void TestRihter()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "rihter.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(1693, resultSaver.Items.Count, "words count");
        }
    }
}
