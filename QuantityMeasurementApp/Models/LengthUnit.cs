using System;

namespace QuantityMeasurementApp.Models
{
    // Simple enum (C# enums cannot use double as base type)
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        // Conversion factors relative to base unit: FEET
        private static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                LengthUnit.Yards => 3.0,
                LengthUnit.Centimeters => 1.0 / 30.48,
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }

        // Convert value in this unit → base unit (Feet)
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            return value * unit.GetConversionFactor();
        }

        // Convert base unit (Feet) → this unit
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            if (!double.IsFinite(baseValue))
                throw new ArgumentException("Base value must be finite.");

            return baseValue / unit.GetConversionFactor();
        }

        // Optional helper for display / UC10 consistency
        public static string GetUnitName(this LengthUnit unit)
        {
            return unit.ToString();
        }
    }
}