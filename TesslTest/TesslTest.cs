using unforgettablemeuk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TesslTest
{


    [TestClass()]
    public class TesslTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void NewShouldConstructNewObjectWhenInStandardModeTest()
        {
            object expected = new object();
            object actual = Tessl.New<object>();
            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            //
            Assert.IsTrue(comparer.Compare(actual, expected), comparer.DifferencesString);

            //try a class with plenty of fields
            var expected2 = new System.Diagnostics.ProcessStartInfo();
            var actual2 = Tessl.New<System.Diagnostics.ProcessStartInfo>();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            //
            Assert.IsTrue( comparer.Compare(expected2, actual2), comparer.DifferencesString);
        }

        [TestMethod()]
        public void NewWith1ParameterShouldConstructNewObjectWhenInStandardModeTest()
        {
            decimal expected = new decimal(123.45);
            decimal actual   = Tessl.New<decimal, double>( 123.45 );
            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            //
            Assert.IsTrue(comparer.Compare(actual, expected), comparer.DifferencesString);
            Assert.AreEqual<decimal>(expected, actual);

            //try a class with plenty of fields
            string aFilename = "filename";
            var expected2 = new System.Diagnostics.ProcessStartInfo(aFilename);
            var actual2 = Tessl.New<System.Diagnostics.ProcessStartInfo, string>(aFilename);
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            //
            Assert.IsTrue(comparer.Compare(expected2, actual2), comparer.DifferencesString);
            Assert.AreEqual<string>(aFilename, expected2.FileName);
            Assert.AreEqual<string>(aFilename, actual2.FileName);
        }
    }
}

