using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementTests.Services
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _service=null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        [TestMethod]
        public void GivenSameValues_WhenCompared_ShouldReturnTrue()
        {
            var result = _service.AreEqual(1.0, 1.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentValues_WhenCompared_ShouldReturnFalse()
        {
            var result = _service.AreEqual(1.0, 2.0);

            Assert.IsFalse(result);
        }
    }
}