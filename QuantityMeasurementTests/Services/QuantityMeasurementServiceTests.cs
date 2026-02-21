using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.Models;

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

        [TestMethod]
        public void GivenFeetToFeet_SameValue_ShouldReturnTrue()
        {
            var result = _service.AreEqual(1.0, LengthUnit.Feet,
                                           1.0, LengthUnit.Feet);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenInchToInch_SameValue_ShouldReturnTrue()
        {
            var result = _service.AreEqual(1.0, LengthUnit.Inches,
                                           1.0, LengthUnit.Inches);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFeetToInch_EquivalentValue_ShouldReturnTrue()
        {
            var result = _service.AreEqual(1.0, LengthUnit.Feet,
                                           12.0, LengthUnit.Inches);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentValues_ShouldReturnFalse()
        {
            var result = _service.AreEqual(2.0, LengthUnit.Feet,
                                           12.0, LengthUnit.Inches);

            Assert.IsFalse(result);
        }
    }
}