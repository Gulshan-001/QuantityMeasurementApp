using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class QuantityVolumeTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testEquality_LitreToLitre_SameValue()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_LitreToMillilitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_MillilitreToLitre()
        {
            var a = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            var b = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testEquality_GallonToLitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var b = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void testConversion_LitreToMillilitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

            var result = a.ConvertTo(VolumeUnit.Millilitre);

            Assert.AreEqual(1000.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testConversion_MillilitreToLitre()
        {
            var a = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            var result = a.ConvertTo(VolumeUnit.Litre);

            Assert.AreEqual(1.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testConversion_GallonToLitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);

            var result = a.ConvertTo(VolumeUnit.Litre);

            Assert.AreEqual(3.78541, result.Value, 1e-4);
        }

        [TestMethod]
        public void testAddition_SameUnit_Litre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);

            var result = a.Add(b);

            Assert.AreEqual(3.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testAddition_CrossUnit_LitrePlusMillilitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            var result = a.Add(b);

            Assert.AreEqual(2.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Millilitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var b = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            var result = a.Add(b, VolumeUnit.Millilitre);

            Assert.AreEqual(2000.0, result.Value, Epsilon);
        }

        [TestMethod]
        public void testAddition_GallonPlusLitre()
        {
            var a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var b = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);

            var result = a.Add(b);

            Assert.AreEqual(2.0, result.Value, 1e-4);
        }

        [TestMethod]
        public void testVolumeVsLength_NotEqual()
        {
            var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(volume.Equals(length));
        }

        [TestMethod]
        public void testVolumeVsWeight_NotEqual()
        {
            var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);

            Assert.IsFalse(volume.Equals(weight));
        }
    }
}