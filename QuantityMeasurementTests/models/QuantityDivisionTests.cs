using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityDivisionTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testDivision_SameUnit_Feet()
        {
            var a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

            var result = a.Divide(b);

            Assert.AreEqual(5.0, result, Epsilon);
        }

        [TestMethod]
        public void testDivision_CrossUnit_Length()
        {
            var a = new Quantity<LengthUnit>(24.0, LengthUnit.Inches);
            var b = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

            var result = a.Divide(b);

            Assert.AreEqual(1.0, result, Epsilon);
        }

        [TestMethod]
        public void testDivision_Weight()
        {
            var a = new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram);
            var b = new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram);

            var result = a.Divide(b);

            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testDivision_Volume()
        {
            var a = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            var b = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

            var result = a.Divide(b);

            Assert.AreEqual(1.0, result, Epsilon);
        }

        [TestMethod]
        public void testDivision_RatioLessThanOne()
        {
            var a = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);

            var result = a.Divide(b);

            Assert.AreEqual(0.5, result, Epsilon);
        }

[TestMethod]
public void testDivision_ByZero()
{
    var a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
    var b = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);

    try
    {
        a.Divide(b);
        Assert.Fail("Expected ArithmeticException was not thrown.");
    }
    catch (ArithmeticException)
    {
        return; // success
    }
}
    }
}