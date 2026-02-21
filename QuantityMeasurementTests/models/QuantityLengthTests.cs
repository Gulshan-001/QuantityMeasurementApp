using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityLengthTests
    {
        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            var q1 = new QuantityLength(12.0, LengthUnit.Inches);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_DifferentValue()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.Feet);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var q = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(q.Equals(null));
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var q = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(q.Equals(q));
        }
    }
}