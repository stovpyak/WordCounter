using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib.ResultSaver;
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

            Assert.AreEqual(1454, resultSaver.Items.Count, "words count");
            var firstWord = resultSaver.Items.First();
            Assert.AreEqual("глава", firstWord.Word, "word");
            Assert.AreEqual(6, firstWord.Count, "count");
        }

        [TestMethod]
        public void TestTwoBigFiles()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "rihter.txt");
            fileSource.Add(FileDir + "xunit.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(1755, resultSaver.Items.Count, "words count");
        }

        [TestMethod]
        public void TestFourFiles()
        {
            var resultSaver = new ResultSaverSpy();
            TestFourFiles(resultSaver);

            Assert.AreEqual(1454, resultSaver.Items.Count, "words count");
            var firstWord = resultSaver.Items.First();
            Assert.AreEqual("глава", firstWord.Word, "word");
            Assert.AreEqual(24, firstWord.Count, "count");
        }

        [TestMethod]
        public void TestFourFilesSaveToFile()
        {
            const string resultFileName = FileDir + "result.txt";
            var resultSaver = new ResultSaverToFile(resultFileName);
            TestFourFiles(resultSaver);

            var actual = ReadFromFile(resultFileName);

            Assert.AreEqual(1454, actual.Count, "words count");
            var firstWord = actual.First();
            Assert.AreEqual("в", firstWord.Word, "word");
            Assert.AreEqual(448, firstWord.Count, "count");
        }

        [TestMethod]
        public void TestMaster()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "master.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(26425, resultSaver.Items.Count, "words count");
            var ordered = resultSaver.Items.OrderByDescending(w => w.Count).ToArray();

            var first = ordered[0];
            Assert.AreEqual("-", first.Word, "first word");
            Assert.AreEqual(5041, first.Count, "first count");

            var second = ordered[1];
            Assert.AreEqual("и", second.Word, "second word");
            Assert.AreEqual(4967, second.Count, "second count");

            var third = ordered[2];
            Assert.AreEqual("в", third.Word, "third word");
            Assert.AreEqual(3617, third.Count, "third count");
        }

        [TestMethod]
        public void TestDogheart()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "dogheart.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(8268, resultSaver.Items.Count, "words count");
            var ordered = resultSaver.Items.OrderByDescending(w => w.Count).ToArray();

            var first = ordered[0];
            Assert.AreEqual("-", first.Word, "first word");
            Assert.AreEqual(1585, first.Count, "first count");

            var second = ordered[1];
            Assert.AreEqual("и", second.Word, "second word");
            Assert.AreEqual(938, second.Count, "second count");

            var third = ordered[2];
            Assert.AreEqual("в", third.Word, "third word");
            Assert.AreEqual(798, third.Count, "third count");
        }

        [TestMethod]
        public void TestAnsi()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "ansi.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(2, resultSaver.Items.Count, "words count");

            var first = resultSaver.Items[0];
            Assert.AreEqual("one", first.Word, "first word");
            Assert.AreEqual(1, first.Count, "first count");

            var second = resultSaver.Items[1];
            Assert.AreEqual("один", second.Word, "second word");
            Assert.AreEqual(1, second.Count, "second count");
        }

        [TestMethod]
        public void TestUtf8()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "utf8.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(2, resultSaver.Items.Count, "words count");

            var first = resultSaver.Items[0];
            Assert.AreEqual("one", first.Word, "first word");
            Assert.AreEqual(1, first.Count, "first count");

            var second = resultSaver.Items[1];
            Assert.AreEqual("один", second.Word, "second word");
            Assert.AreEqual(1, second.Count, "second count");
        }

        [TestMethod]
        public void TestIso8859()
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "iso8859.txt");

            var appl = new Appl(fileSource);
            var resultSaver = new ResultSaverSpy();
            appl.Execute(resultSaver);

            Assert.AreEqual(2, resultSaver.Items.Count, "words count");

            var first = resultSaver.Items[0];
            Assert.AreEqual("one", first.Word, "first word");
            Assert.AreEqual(1, first.Count, "first count");

            var second = resultSaver.Items[1];
            Assert.AreEqual("один", second.Word, "second word");
            Assert.AreEqual(1, second.Count, "second count");
        }

        private void TestFourFiles(IResultSaver saver)
        {
            var fileSource = new FileNameSourceStub();
            fileSource.Add(FileDir + "rihter.txt");
            fileSource.Add(FileDir + "rihter2.txt");
            fileSource.Add(FileDir + "rihter3.txt");
            fileSource.Add(FileDir + "rihter4.txt");

            var appl = new Appl(fileSource);
            appl.Execute(saver);
        }

        private IList<WordCountItem> ReadFromFile(string fileName)
        {
            var result = new List<WordCountItem>();
            var separator = new Char[] {' '};
            foreach (string line in File.ReadLines(fileName))
            {
                var wordAndCount = line.Split(separator);
                var item = new WordCountItem(wordAndCount[0], Int32.Parse(wordAndCount[1]));
                result.Add(item);
            }
            return result;
        }
    }
}
