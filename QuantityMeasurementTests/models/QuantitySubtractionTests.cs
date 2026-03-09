using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantitySubtractionTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            var a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);

            var result = a.Subtract(b);

            Assert.AreEqual(5.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            var a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(6.0, LengthUnit.Inches);

            var result = a.Subtract(b);

            Assert.AreEqual(9.5, result.Value, Epsilon);
        }

        [TestMethod]
        public void testSubtraction_ResultingInNegative()
        {
            var a = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);

            var result = a.Subtract(b);

            Assert.AreEqual(-5.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testSubtraction_ResultingInZero()
        {
            var a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(120.0, LengthUnit.Inches);

            var result = a.Subtract(b);

            Assert.AreEqual(0.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testSubtraction_Volume()
        {
            var a = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);

            var result = a.Subtract(b);

            Assert.AreEqual(4.5, result.Value, Epsilon);
        }

        [TestMethod]
        public void testSubtraction_Weight()
        {
            var a = new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram);
            var b = new Quantity<WeightUnit>(5000.0, WeightUnit.Gram);

            var result = a.Subtract(b);

            Assert.AreEqual(5.0, result.Value, Epsilon);
        }
    }
}