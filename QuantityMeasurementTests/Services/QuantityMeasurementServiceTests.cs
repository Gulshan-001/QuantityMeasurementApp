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
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            var result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenInchToInch_SameValue_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);

            var result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFeetToInch_EquivalentValue_ShouldReturnTrue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentValues_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }
    }
}