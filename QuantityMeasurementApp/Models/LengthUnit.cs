namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inches
    }

    public static class LengthUnitExtensions
    {
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }
    }
}