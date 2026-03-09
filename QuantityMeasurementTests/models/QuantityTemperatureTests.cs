using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityTemperatureTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testTemperatureEquality_CelsiusToFahrenheit()
        {
            var a = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var b = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testTemperatureEquality_CelsiusToKelvin()
        {
            var a = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            var b = new Quantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testTemperatureConversion_CelsiusToFahrenheit()
        {
            var temp = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);

            var result = temp.ConvertTo(TemperatureUnit.Fahrenheit);

            Assert.AreEqual(212.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testTemperatureConversion_FahrenheitToCelsius()
        {
            var temp = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);

            var result = temp.ConvertTo(TemperatureUnit.Celsius);

            Assert.AreEqual(0.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testTemperatureUnsupportedOperation_Add()
        {
            var a = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var b = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);

            try
            {
                a.Add(b);
                Assert.Fail("Expected InvalidOperationException was not thrown.");
            }
            catch (InvalidOperationException)
            {
                return; // success
            }
        }

        [TestMethod]
        public void testTemperatureUnsupportedOperation_Subtract()
        {
            var a = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var b = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);

            try
            {
                a.Subtract(b);
                Assert.Fail("Expected InvalidOperationException was not thrown.");
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        [TestMethod]
        public void testTemperatureUnsupportedOperation_Divide()
        {
            var a = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            var b = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);

            try
            {
                a.Divide(b);
                Assert.Fail("Expected InvalidOperationException was not thrown.");
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}