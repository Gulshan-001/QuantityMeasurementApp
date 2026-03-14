using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityWeightTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testEquality_KgToGram()
        {
            var a = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var b = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testConversion_KgToPound()
        {
            var result = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram)
                .ConvertTo(WeightUnit.Pound);

            Assert.AreEqual(2.20462, result.Value, 1e-3);
        }

        [TestMethod]
        public void testAddition_KgPlusGram()
        {
            var a = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var b = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);

            var result = a.Add(b);

            Assert.AreEqual(2.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testWeightVsLength_NotEqual()
        {
            var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(weight.Equals(length));
        }
    }
}