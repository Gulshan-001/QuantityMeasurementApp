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
            double result = QuantityLength.Convert(1.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.Inches, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_YardsToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Yards, LengthUnit.Inches);
            Assert.AreEqual(36.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_CentimetersToInches()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.Centimeters, LengthUnit.Inches);
            Assert.AreEqual(1.0, result, 1e-4);
        }

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            double value = 5.0;

            double converted = QuantityLength.Convert(value, LengthUnit.Feet, LengthUnit.Yards);
            double back = QuantityLength.Convert(converted, LengthUnit.Yards, LengthUnit.Feet);

            Assert.AreEqual(value, back, Epsilon);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(-12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InvalidValue_Throws()
        {
            try
            {
                QuantityLength.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inches);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                return; // success
            }
        }
    }
}