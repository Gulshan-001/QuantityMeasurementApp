using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityLengthExplicitTargetAdditionTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = a.Add(b, LengthUnit.Feet);

            Assert.AreEqual(2.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = a.Add(b, LengthUnit.Inches);

            Assert.AreEqual(24.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Inches, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = a.Add(b, LengthUnit.Yards);

            Assert.AreEqual(0.666666, result.Value, 1e-4);
            Assert.AreEqual(LengthUnit.Yards, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result1 = a.Add(b, LengthUnit.Yards);
            var result2 = b.Add(a, LengthUnit.Yards);

            Assert.IsTrue(result1.Equals(result2));
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_InvalidTargetUnit()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            try
            {
                a.Add(b, (LengthUnit)999);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (ArgumentException)
            {
                return;
            }
        }
    }
}