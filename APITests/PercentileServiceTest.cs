using System;
using System.Collections.Generic;
using API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APITests
{
    [TestClass]
    public class PercentileServiceTest
    {
        private List<double> values = new List<double>() { 15, 20, 35, 40, 50 };

        [TestMethod]
        public void Will_Calculate_40th_Percentile_For_Control_Set()
        {
            var service = new PercentileService();
            var percentile = service.CalculatePercentile(0.40, values);

            Assert.AreEqual(29, percentile);
        }

        [TestMethod]
        public void Will_Calculate_100th_Percentile_For_Control_Set()
        {
            var service = new PercentileService();
            var percentile = service.CalculatePercentile(1, values);

            Assert.AreEqual(50, percentile);
        }

        [TestMethod]
        public void Will_Calculate_0th_Percentile_For_Control_Set()
        {
            var service = new PercentileService();
            var percentile = service.CalculatePercentile(0, values);

            Assert.AreEqual(15, percentile);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Will_Reject_Percentile_Values_Smaller_Than_Zero()
        {
            var service = new PercentileService();
            service.CalculatePercentile(-1, values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Will_Reject_Percentile_Values_Larger_Than_One()
        {
            var service = new PercentileService();
            service.CalculatePercentile(1.1, values);
        }

    }
}
