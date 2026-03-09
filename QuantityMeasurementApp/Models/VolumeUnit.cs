using System;

namespace QuantityMeasurementApp.Models
{
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon
    }

    public static class VolumeUnitExtensions
    {
        private static double GetFactor(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.Litre => 1.0,
                VolumeUnit.Millilitre => 0.001,
                VolumeUnit.Gallon => 3.78541,
                _ => throw new ArgumentException("Unsupported volume unit.")
            };
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            return value * unit.GetFactor();
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            if (!double.IsFinite(baseValue))
                throw new ArgumentException("Base value must be finite.");

            return baseValue / unit.GetFactor();
        }
    }
}