using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public bool AreFeetEqual(double value1, double value2)
        {
            return new Feet(value1).Equals(new Feet(value2));
        }

        public bool AreInchesEqual(double value1, double value2)
        {
            return new Inches(value1).Equals(new Inches(value2));
        }
    }
}