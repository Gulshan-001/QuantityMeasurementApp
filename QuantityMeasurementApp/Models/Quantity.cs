using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class Quantity<U> where U : struct, Enum
    {
        public double Value { get; }
        public U Unit { get; }

        private const double Epsilon = 1e-6;

        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Value = value;
            Unit = unit;
        }

        // ---------------- INTERNAL BASE CONVERSION ----------------

        private double ConvertToBase()
        {
            if (Unit is LengthUnit lengthUnit)
                return lengthUnit.ConvertToBaseUnit(Value);

            if (Unit is WeightUnit weightUnit)
                return weightUnit.ConvertToBaseUnit(Value);

            throw new ArgumentException("Unsupported unit type.");
        }

        private static double ConvertFromBase(U targetUnit, double baseValue)
        {
            if (targetUnit is LengthUnit lengthUnit)
                return lengthUnit.ConvertFromBaseUnit(baseValue);

            if (targetUnit is WeightUnit weightUnit)
                return weightUnit.ConvertFromBaseUnit(baseValue);

            throw new ArgumentException("Unsupported unit type.");
        }

        // ---------------- CONVERSION ----------------

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase();
            double converted = ConvertFromBase(targetUnit, baseValue);

            return new Quantity<U>(converted, targetUnit);
        }

        // ---------------- ADDITION ----------------

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, this.Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Other quantity cannot be null.");

            double sumBase = this.ConvertToBase() + other.ConvertToBase();
            double resultValue = ConvertFromBase(targetUnit, sumBase);

            return new Quantity<U>(resultValue, targetUnit);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            return Math.Abs(this.ConvertToBase() - other.ConvertToBase()) < Epsilon;
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}