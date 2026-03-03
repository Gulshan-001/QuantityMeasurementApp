using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityLengthAdditionTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

            var result = q1.Add(q2);

            Assert.AreEqual(3.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_FeetPlusInches()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = q1.Add(q2);

            Assert.AreEqual(2.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_InchPlusFeet()
        {
            var q1 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            var result = q1.Add(q2);

            Assert.AreEqual(24.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Inches, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_YardPlusFeet()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var q2 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);

            var result = q1.Add(q2);

            Assert.AreEqual(2.0, result.Value, Epsilon);
            Assert.AreEqual(LengthUnit.Yards, result.Unit);
        }

        [TestMethod]
        public void testAddition_WithZero()
        {
            var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.Inches);

            var result = q1.Add(q2);

            Assert.AreEqual(5.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testAddition_NegativeValues()
        {
            var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(-2.0, LengthUnit.Feet);

            var result = q1.Add(q2);

            Assert.AreEqual(3.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testAddition_Commutativity()
        {
            var a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var b = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result1 = a.Add(b);
            var result2 = b.Add(a);

            Assert.IsTrue(result1.Equals(result2));
        }

        [TestMethod]
        public void testAddition_NullSecondOperand()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            try
            {
                q1.Add(null!);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                return; // success
            }
        }
    }
}