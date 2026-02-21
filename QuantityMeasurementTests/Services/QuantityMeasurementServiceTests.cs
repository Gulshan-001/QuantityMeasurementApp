using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementTests.Services
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        // 🔹 FEET TESTS

        [TestMethod]
        public void GivenSameFeetValues_WhenCompared_ShouldReturnTrue()
        {
            var result = _service.AreFeetEqual(1.0, 1.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentFeetValues_WhenCompared_ShouldReturnFalse()
        {
            var result = _service.AreFeetEqual(1.0, 2.0);

            Assert.IsFalse(result);
        }

        // 🔹 INCH TESTS

        [TestMethod]
        public void GivenSameInchValues_WhenCompared_ShouldReturnTrue()
        {
            var result = _service.AreInchesEqual(1.0, 1.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentInchValues_WhenCompared_ShouldReturnFalse()
        {
            var result = _service.AreInchesEqual(1.0, 2.0);

            Assert.IsFalse(result);
        }
    }
}