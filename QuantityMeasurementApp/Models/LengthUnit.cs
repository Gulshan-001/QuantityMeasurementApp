using System;

namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        private static double GetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                LengthUnit.Yards => 3.0,
                LengthUnit.Centimeters => 1.0 / 30.48,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            return value * unit.GetFactor();
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            if (!double.IsFinite(baseValue))
                throw new ArgumentException("Base value must be finite.");

            return baseValue / unit.GetFactor();
        }
    }
}