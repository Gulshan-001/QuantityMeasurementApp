using System;
namespace QuantityMeasurementApp.Models
{
    public sealed class Feet
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || obj.GetType() != typeof(Feet))
                return false;

            var other = (Feet)obj;
            return double.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}