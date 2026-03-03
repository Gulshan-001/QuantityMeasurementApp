using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class QuantityWeight
    {
        public double Value { get; }
        public WeightUnit Unit { get; }

        private const double Epsilon = 1e-6;

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (!Enum.IsDefined(typeof(WeightUnit), unit))
                throw new ArgumentException("Unsupported weight unit.");

            Value = value;
            Unit = unit;
        }

        private double ConvertToBase()
        {
            return Unit.ConvertToBaseUnit(Value);
        }

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            double baseValue = ConvertToBase();
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityWeight(convertedValue, targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            if (other is null)
                throw new ArgumentException("Other weight cannot be null.");

            return AddInternal(this, other, this.Unit);
        }

        public static QuantityWeight Add(
            QuantityWeight first,
            QuantityWeight second,
            WeightUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            return AddInternal(first, second, targetUnit);
        }

        private static QuantityWeight AddInternal(
            QuantityWeight first,
            QuantityWeight second,
            WeightUnit targetUnit)
        {
            double firstBase = first.Unit.ConvertToBaseUnit(first.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            double sumBase = firstBase + secondBase;

            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new QuantityWeight(resultValue, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not QuantityWeight other)
                return false;

            return Math.Abs(
                this.Unit.ConvertToBaseUnit(this.Value) -
                other.Unit.ConvertToBaseUnit(other.Value)
            ) < Epsilon;
        }

        public override int GetHashCode()
        {
            return Unit.ConvertToBaseUnit(Value).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}