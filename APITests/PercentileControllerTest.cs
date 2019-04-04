using System;
using System.Collections.Generic;
using API;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace APITests
{
    [TestClass]
    public class PercentileControllerTest
    {
        private List<double> testValues = new List<double>() { 15, 20, 35, 40, 50 };
        private IPercentileDataAccess dataAccessService;

        [TestInitialize]
        public void TestInitialize()
        {
            dataAccessService = Substitute.For<IPercentileDataAccess>();
            dataAccessService.GetPercentileData().Returns<List<double>>(testValues);
        }

        [TestMethod]
        public void Will_Return_Percentile_For_Successful_Call()
        {
            IPercentileService percentileService = Substitute.For<IPercentileService>();
            percentileService.CalculatePercentile(.995, testValues).Returns<double>(123);


            PercentileController controller = new PercentileController(percentileService, dataAccessService);
            var percentile = controller.Get(0.995);

            Assert.AreEqual(123, percentile.Value);
        }

        [TestMethod]
        public void Will_Return_BadRequest_For_ArgumentOutOfRangeException()
        {
            IPercentileService percentileService = Substitute.For<IPercentileService>();
            percentileService.CalculatePercentile(.995, testValues).ThrowsForAnyArgs<ArgumentOutOfRangeException>();


            PercentileController controller = new PercentileController(percentileService, dataAccessService);
            var res = controller.Get(0.995);

            Assert.AreEqual("Bad Request", ((ObjectResult)res.Result).Value);
        }

        [TestMethod]
        public void Will_Return_ServiceUnavailable_For_Generic_Exception()
        {
            IPercentileService percentileService = Substitute.For<IPercentileService>();
            percentileService.CalculatePercentile(.995, testValues).ThrowsForAnyArgs<Exception>();


            PercentileController controller = new PercentileController(percentileService, dataAccessService);
            var res = controller.Get(0.995);

            Assert.AreEqual("Service Unavailable", ((ObjectResult)res.Result).Value);
        }

    }
}
