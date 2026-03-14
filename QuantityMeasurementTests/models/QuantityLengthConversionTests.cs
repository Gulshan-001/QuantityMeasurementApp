using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityLengthConversionTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testConversion_FeetToInches()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            double result = q.ConvertTo(LengthUnit.Inches).Value;

            Assert.AreEqual(12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InchesToFeet()
        {
            var q = new Quantity<LengthUnit>(24.0, LengthUnit.Inches);
            double result = q.ConvertTo(LengthUnit.Feet).Value;

            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_YardsToInches()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            double result = q.ConvertTo(LengthUnit.Inches).Value;

            Assert.AreEqual(36.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_CentimetersToInches()
        {
            var q = new Quantity<LengthUnit>(2.54, LengthUnit.Centimeters);
            double result = q.ConvertTo(LengthUnit.Inches).Value;

            Assert.AreEqual(1.0, result, 1e-4);
        }

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            double value = 5.0;

            var q = new Quantity<LengthUnit>(value, LengthUnit.Feet);
            double converted = q.ConvertTo(LengthUnit.Yards).Value;
            double back = new Quantity<LengthUnit>(converted, LengthUnit.Yards)
                                .ConvertTo(LengthUnit.Feet)
                                .Value;

            Assert.AreEqual(value, back, Epsilon);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            var q = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            double result = q.ConvertTo(LengthUnit.Inches).Value;

            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            var q = new Quantity<LengthUnit>(-1.0, LengthUnit.Feet);
            double result = q.ConvertTo(LengthUnit.Inches).Value;

            Assert.AreEqual(-12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InvalidValue_Throws()
        {
            try
            {
                var q = new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet);
                q.ConvertTo(LengthUnit.Inches);

                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                return; // success
            }
        }
    }
}