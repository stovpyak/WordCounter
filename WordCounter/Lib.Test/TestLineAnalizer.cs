using System.Linq;
using Lib.Analize;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib.Test
{
    [TestClass]
    public class TestLineAnalizer
    {
        [TestMethod]
        public void TestOneWord()
        {
            var analizer = new LineAnalizer();
            var result = analizer.Pars("one");
            Assert.AreEqual(1, result.Count(), "count");
            Assert.AreEqual("one", result.First(), "word");
        }

        [TestMethod]
        public void TestTwoWordSpace()
        {
            var analizer = new LineAnalizer();
            var result = analizer.Pars("one two");
            Assert.AreEqual(2, result.Count(), "count");
            Assert.AreEqual("one", result[0], "first");
            Assert.AreEqual("two", result[1], "second");
        }

        [TestMethod]
        public void TestTwoWordComma()
        {
            var analizer = new LineAnalizer();
            var result = analizer.Pars("one, two");
            Assert.AreEqual(2, result.Count(), "count");
            Assert.AreEqual("one", result[0], "first");
            Assert.AreEqual("two", result[1], "second");
        }

        [TestMethod]
        public void TestTwoWordDot()
        {
            var analizer = new LineAnalizer();
            var result = analizer.Pars("one. two");
            Assert.AreEqual(2, result.Count(), "count");
            Assert.AreEqual("one", result[0], "first");
            Assert.AreEqual("two", result[1], "second");
        }

        [TestMethod]
        public void TestTwoWordOther()
        {
            var analizer = new LineAnalizer();
            var result = analizer.Pars("one...! two");
            Assert.AreEqual(2, result.Count(), "count");
            Assert.AreEqual("one", result[0], "first");
            Assert.AreEqual("two", result[1], "second");
        }
    }
}
