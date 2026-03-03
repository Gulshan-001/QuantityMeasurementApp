using System;

namespace QuantityMeasurementApp.Models
{
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitBehavior
    {
        public static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.Kilogram => 1.0,
                WeightUnit.Gram => 0.001,
                WeightUnit.Pound => 0.453592,
                _ => throw new ArgumentException("Unsupported weight unit.")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            return value * unit.GetConversionFactor();
        }

        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            if (!double.IsFinite(baseValue))
                throw new ArgumentException("Base value must be finite.");

            return baseValue / unit.GetConversionFactor();
        }

        public static string GetUnitName(this WeightUnit unit)
            => unit.ToString();
    }
}