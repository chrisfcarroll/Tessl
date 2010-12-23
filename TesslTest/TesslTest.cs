using Unforgettablemeuk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Tessl.deconfigure();
        }
        
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
            //
            object actual = Tessl.New<object>();
            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            Assert.IsTrue(comparer.Compare(actual, expected), comparer.DifferencesString);

            //try a class with plenty of fields
            var expected2 = new System.Diagnostics.ProcessStartInfo();
            //
            var actual2 = Tessl.New<System.Diagnostics.ProcessStartInfo>();
            //
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare(expected2, actual2), comparer.DifferencesString);
        }

        [TestMethod()]
        public void NewWith1ParameterShouldConstructNewObjectWhenInStandardModeTest()
        {
            decimal expected = new decimal(123.45);
            //
            decimal actual   = Tessl.New<decimal, double>( 123.45 );
            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
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

        [TestMethod()]
        public void NewWith2ParametersShouldConstructNewObjectWhenInStandardModeTest()
        {
            //
            string filename = "filename";
            string args = "args";
            var expected2 = new System.Diagnostics.ProcessStartInfo( filename, args );
            var actual2 = Tessl.New<System.Diagnostics.ProcessStartInfo, string, string>( filename, args );

            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare( expected2, actual2 ), comparer.DifferencesString );
            Assert.AreEqual<string>( filename, expected2.FileName );
            Assert.AreEqual<string>( filename, actual2.FileName );
            Assert.AreEqual<string>( args, expected2.Arguments );
            Assert.AreEqual<string>( args, actual2.Arguments );
        }

        [TestMethod()]
        public void NewWith4ParametersShouldConstructNewObjectWhenInStandardModeTest()
        {
            //
            var expected = new DateTime( 
                                2000, 
                                1, 
                                2, 
                                new System.Globalization.GregorianCalendar( System.Globalization.GregorianCalendarTypes.Localized ) );
            var actual = Tessl.New<DateTime, int, int, int, System.Globalization.GregorianCalendar>(
                                2000, 
                                1, 
                                2, 
                                Tessl.New<System.Globalization.GregorianCalendar, System.Globalization.GregorianCalendarTypes>(System.Globalization.GregorianCalendarTypes.Localized));

            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare( expected, actual ), comparer.DifferencesString );
            Assert.AreEqual<int>( 2000,  actual.Year);
            Assert.AreEqual<int>( 1, actual.Month);
            Assert.AreEqual<int>( 2, actual.Day);
            Assert.IsTrue( expected.CompareTo(actual) == 0, "Expected CompareTo() to return 0");
        }

        [TestMethod()]
        public void InitShouldInitialiseObjectWhenInStandardModeTest()
        {
            System.Diagnostics.ProcessStartInfo expected= 
                new System.Diagnostics.ProcessStartInfo
                {
                    Arguments="Args",
                    CreateNoWindow=true,
                    Domain="Domain"
                };
            System.Diagnostics.ProcessStartInfo actual = Tessl.Init<System.Diagnostics.ProcessStartInfo>(
                new System.Diagnostics.ProcessStartInfo
                {
                    Arguments="Args",
                    CreateNoWindow=true,
                    Domain="Domain"
                });

            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare( expected, actual ), comparer.DifferencesString );
            Assert.AreEqual<string>( "Domain", actual.Domain );
        }

        [TestMethod()]
        public void FactoryMethodShouldWorkWhenInStandardModeTest()
        {
            DateTime then = DateTime.Now;
            long ft = then.ToFileTime();
            DateTime expected = DateTime.FromFileTime( ft );

            //
            DateTime actual = Tessl.From<DateTime>( new Func<long,DateTime>(DateTime.FromFileTime), ft );

            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare( expected, actual ), comparer.DifferencesString );
            Assert.AreEqual<int>( then.Year, actual.Year );
            Assert.AreEqual<int>( then.Month, actual.Month );
            Assert.AreEqual<int>( then.Day, actual.Day );
            Assert.IsTrue( expected.CompareTo( actual ) == 0, "Expected CompareTo() to return 0" );
        }

        [TestMethod()]
        public void TypedFactoryMethodWith1ParamShouldWorkWhenInStandardModeTest()
        {
            DateTime then = DateTime.Now;
            long ft = then.ToFileTime();
            DateTime expected = DateTime.FromFileTime( ft );

            //
            DateTime actual = Tessl.From<long,DateTime>( new Func<long, DateTime>( DateTime.FromFileTime ), ft );

            //
            var comparer = new KellermanSoftware.CompareNetObjects.CompareObjects();
            comparer.IgnoreIndexersWhichCantBeCompared = true;
            Assert.IsTrue( comparer.Compare( expected, actual ), comparer.DifferencesString );
            Assert.AreEqual<int>( then.Year, actual.Year );
            Assert.AreEqual<int>( then.Month, actual.Month );
            Assert.AreEqual<int>( then.Day, actual.Day );
            Assert.IsTrue( expected.CompareTo( actual ) == 0, "Expected CompareTo() to return 0" );
        }

        [TestMethod()]
        public void TypedFactoryMethodWith2ParamsShouldWorkWhenInStandardModeTest()
        {
            DateTime then = DateTime.Now;
            String expected = then.ToString( "G", DateTimeFormatInfo.CurrentInfo );

            //
            string actual = Tessl.From<string, DateTimeFormatInfo, string>( then.ToString, "G", DateTimeFormatInfo.CurrentInfo );

            //
            Assert.AreEqual<string>( expected, actual );
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfigurationModeShouldThrowIfCalledTwiceWithSameValues()
        {
            Tessl.Configuration= new TesslProductionImplementation();
            Tessl.Configuration= new TesslProductionImplementation();
            //
            // Should throw
        }

        [TestMethod()]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void ConfigurationModeShouldThrowIfCalledTwiceWithDifferentValues()
        {
            Tessl.Configuration= new TesslProductionImplementation();
            Tessl.Configuration= new Moq.Mock<ITessl>().Object;
            //
            // Should throw
        }

        [TestMethod()]
        public void TestModeShouldSwapOutProductionImplementation()
        {
            Tessl.Configuration= new Moq.Mock<ITessl>().Object;

            // Failure to create still counts as a succcess.
            try
            {
                DateTime d = Tessl.New<DateTime>();
                Assert.AreNotEqual<Type>( typeof( DateTime ), d.GetType() );
            } catch ( Exception ) { }

        }
        [TestMethod()]
        public void TestModeShouldReturnSpecifiedMockObjects()
        {
            //Create a moq object with expected behaviour
            var moonLanding = new DateTime(1969, 07, 21);
            var moqDependencies = new Moq.Mock<ITessl>();
            moqDependencies.Setup( t => t.New<DateTime>() ).Returns( moonLanding );

            //
            Tessl.Configuration= (ITessl)moqDependencies.Object;
            DateTime d = Tessl.New<DateTime>();
            //
            Assert.AreEqual<DateTime>( moonLanding, d );
        }
    }
}

