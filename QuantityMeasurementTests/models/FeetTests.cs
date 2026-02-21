using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementTests.Models
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void GivenSameValue_WhenCompared_ShouldReturnTrue()
        {
            var feet1 = new Feet(1.0);
            var feet2 = new Feet(1.0);

            Assert.IsTrue(feet1.Equals(feet2));
        }

        [TestMethod]
        public void GivenDifferentValue_WhenCompared_ShouldReturnFalse()
        {
            var feet1 = new Feet(1.0);
            var feet2 = new Feet(2.0);

            Assert.IsFalse(feet1.Equals(feet2));
        }

        [TestMethod]
        public void GivenNull_WhenCompared_ShouldReturnFalse()
        {
            var feet = new Feet(1.0);

            Assert.IsFalse(feet.Equals(null));
        }

        [TestMethod]
        public void GivenSameReference_WhenCompared_ShouldReturnTrue()
        {
            var feet = new Feet(1.0);

            Assert.IsTrue(feet.Equals(feet));
        }

        [TestMethod]
        public void GivenDifferentType_WhenCompared_ShouldReturnFalse()
        {
            var feet = new Feet(1.0);

            Assert.IsFalse(feet.Equals("1.0"));
        }
    }
}