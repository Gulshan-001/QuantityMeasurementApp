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

        // ---------------- CONVERSION ----------------

        private double ConvertToBase()
        {
            return Unit.ConvertToBaseUnit(Value);
        }

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            double baseValue = ConvertToBase();
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityLength(convertedValue, targetUnit);
        }

        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            double baseValue = source.ConvertToBaseUnit(value);
            return target.ConvertFromBaseUnit(baseValue);
        }

        // ---------------- ADDITION (UC6) ----------------

        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentException("Other length cannot be null.");

            return AddInternal(this, other, this.Unit);
        }

        // ---------------- ADDITION WITH TARGET (UC7) ----------------

        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Unsupported target unit.");

            return AddInternal(first, second, targetUnit);
        }

        // Centralized private helper (DRY)
        private static QuantityLength AddInternal(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            double firstBase = first.Unit.ConvertToBaseUnit(first.Value);
            double secondBase = second.Unit.ConvertToBaseUnit(second.Value);

            double sumBase = firstBase + secondBase;

            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new QuantityLength(resultValue, targetUnit);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not QuantityLength other)
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