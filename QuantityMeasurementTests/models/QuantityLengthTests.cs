using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityLengthTests
    {
        // ---------------- FEET TESTS ----------------

        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_FeetToFeet_DifferentValue()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(q1.Equals(q2));
        }

        // ---------------- INCH TESTS ----------------

        [TestMethod]
        public void testEquality_InchToInch_SameValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_InchToInch_DifferentValue()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        // ---------------- YARD TESTS (UC4) ----------------

        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_YardToYard_DifferentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Yards);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var q2 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_YardToInches_EquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var q2 = new Quantity<LengthUnit>(36.0, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        // ---------------- CENTIMETER TESTS (UC4) ----------------

        [TestMethod]
        public void testEquality_CentimeterToCentimeter_SameValue()
        {
            var q1 = new Quantity<LengthUnit>(2.0, LengthUnit.Centimeters);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Centimeters);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_CentimeterToCentimeter_DifferentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
            var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Centimeters);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_CentimeterToInch_EquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
            var q2 = new Quantity<LengthUnit>(0.393701, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testEquality_CentimeterToFeet_NonEquivalentValue()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
            var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(q1.Equals(q2));
        }

        // ---------------- GENERAL CONTRACT TESTS ----------------

        [TestMethod]
        public void testEquality_SameReference()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsTrue(q.Equals(q));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(q.Equals(null));
        }
    }
}