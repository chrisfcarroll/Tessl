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


        public void NewTestHelper<T>()
            where T : new()
        {
            T expected = new T(); // TODO: Initialize to an appropriate value
            T actual;
            actual = Tessl.New<T>();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NewShouldConstructNewObjectWhenInStandardModeTest()
        {
            object expected = new object();
            object actual = Tessl.New<object>();
            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            //
            Assert.IsTrue(comparer.Compare(actual, expected), comparer.DifferencesString);
        }

    }
}

