using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class QuantityLength
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        private const double Epsilon = 1e-6;

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number.");

            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Unsupported unit.");

            Value = value;
            Unit = unit;
        }

        // ---------------- BASE CONVERSION ----------------

        private double ConvertToFeet()
        {
            return Value * Unit.ToFeetFactor();
        }

        public double ConvertTo(LengthUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            double valueInFeet = ConvertToFeet();
            return valueInFeet / targetUnit.ToFeetFactor();
        }

        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (!Enum.IsDefined(typeof(LengthUnit), source) ||
                !Enum.IsDefined(typeof(LengthUnit), target))
                throw new ArgumentException("Unsupported unit.");

            double valueInFeet = value * source.ToFeetFactor();
            return valueInFeet / target.ToFeetFactor();
        }

        // ---------------- PRIVATE ADDITION HELPER (UC7 DRY) ----------------

        private static double AddInFeet(QuantityLength first, QuantityLength second)
        {
            return first.ConvertToFeet() + second.ConvertToFeet();
        }

        // ---------------- ADDITION (UC6 - implicit target) ----------------

        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentException("Other length cannot be null.");

            double sumInFeet = AddInFeet(this, other);
            double resultInOriginalUnit = sumInFeet / this.Unit.ToFeetFactor();

            return new QuantityLength(resultInOriginalUnit, this.Unit);
        }

        // ---------------- ADDITION (UC7 - explicit target) ----------------

        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            double sumInFeet = AddInFeet(first, second);
            double resultValue = sumInFeet / targetUnit.ToFeetFactor();

            return new QuantityLength(resultValue, targetUnit);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not QuantityLength other)
                return false;

            return Math.Abs(ConvertToFeet() - other.ConvertToFeet()) < Epsilon;
        }

        public override int GetHashCode()
        {
            return ConvertToFeet().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}