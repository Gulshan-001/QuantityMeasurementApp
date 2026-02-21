using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void GivenSameValue_WhenCompared_ShouldReturnTrue()
        {
            var inch1 = new Inches(1.0);
            var inch2 = new Inches(1.0);

            Assert.IsTrue(inch1.Equals(inch2));
        }

        [TestMethod]
        public void GivenDifferentValue_WhenCompared_ShouldReturnFalse()
        {
            var inch1 = new Inches(1.0);
            var inch2 = new Inches(2.0);

            Assert.IsFalse(inch1.Equals(inch2));
        }

        [TestMethod]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var inch = new Inches(1.0);

            Assert.IsFalse(inch.Equals(null));
        }

        [TestMethod]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var inch = new Inches(1.0);

            Assert.IsTrue(inch.Equals(inch));
        }

        [TestMethod]
        public void GivenDifferentType_WhenCompared_ShouldReturnFalse()
        {
            var inch = new Inches(1.0);

            Assert.IsFalse(inch.Equals("1.0"));
        }
    }
}