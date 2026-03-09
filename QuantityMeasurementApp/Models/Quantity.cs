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

            Unit = unit;
            Value = value;
        }

        // ---------------- BASE CONVERSION ----------------

        private double ConvertToBase()
        {
            if (Unit is LengthUnit length)
                return length.ConvertToBaseUnit(Value);

            if (Unit is WeightUnit weight)
                return weight.ConvertToBaseUnit(Value);

            if (Unit is VolumeUnit volume)
                return volume.ConvertToBaseUnit(Value);

            throw new ArgumentException("Unsupported unit type.");
        }

        private static double ConvertFromBase(double baseValue, U targetUnit)
        {
            if (targetUnit is LengthUnit length)
                return length.ConvertFromBaseUnit(baseValue);

            if (targetUnit is WeightUnit weight)
                return weight.ConvertFromBaseUnit(baseValue);

            if (targetUnit is VolumeUnit volume)
                return volume.ConvertFromBaseUnit(baseValue);

            throw new ArgumentException("Unsupported unit type.");
        }

        // ---------------- ENUM FOR ARITHMETIC ----------------

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        // ---------------- VALIDATION HELPER ----------------

        private void ValidateArithmeticOperands(Quantity<U> other, U? targetUnit, bool targetRequired)
        {
            if (other is null)
                throw new ArgumentException("Other quantity cannot be null.");

            if (!double.IsFinite(this.Value) || !double.IsFinite(other.Value))
                throw new ArgumentException("Values must be finite.");

            if (targetRequired && targetUnit is null)
                throw new ArgumentException("Target unit cannot be null.");

            if (this.Unit.GetType() != other.Unit.GetType())
                throw new ArgumentException("Cross-category operations not allowed.");
        }

        // ---------------- CORE ARITHMETIC HELPER ----------------

        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            double base1 = this.ConvertToBase();
            double base2 = other.ConvertToBase();

            return operation switch
            {
                ArithmeticOperation.ADD => base1 + base2,

                ArithmeticOperation.SUBTRACT => base1 - base2,

                ArithmeticOperation.DIVIDE =>
                    Math.Abs(base2) < Epsilon
                        ? throw new ArithmeticException("Division by zero quantity.")
                        : base1 / base2,

                _ => throw new ArgumentException("Unsupported operation.")
            };
        }

        // ---------------- CONVERSION ----------------

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase();
            double convertedValue = ConvertFromBase(baseValue, targetUnit);

            return new Quantity<U>(convertedValue, targetUnit);
        }

        // ---------------- ADDITION ----------------

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);

            double result = ConvertFromBase(baseResult, targetUnit);

            result = Math.Round(result, 2);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- SUBTRACTION ----------------

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

            double result = ConvertFromBase(baseResult, targetUnit);

            result = Math.Round(result, 2);

            return new Quantity<U>(result, targetUnit);
        }

        // ---------------- DIVISION ----------------

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, null, false);

            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Quantity<U> other)
                return false;

            return Math.Abs(ConvertToBase() - other.ConvertToBase()) < Epsilon;
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