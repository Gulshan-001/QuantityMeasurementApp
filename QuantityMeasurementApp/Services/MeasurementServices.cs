using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool AreEqual(double value1, double value2)
        {
            var feet1 = new Feet(value1);
            var feet2 = new Feet(value2);

            return feet1.Equals(feet2);
        }
    }
}