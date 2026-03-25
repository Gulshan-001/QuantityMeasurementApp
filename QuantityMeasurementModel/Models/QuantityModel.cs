namespace QuantityMeasurementModel.Models
{
    public class QuantityModel<U>
    {
        public double Value { get; }
        public U Unit { get; }

        public QuantityModel(double value, U unit)
        {
            Value = value;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}