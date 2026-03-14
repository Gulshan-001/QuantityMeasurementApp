using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool CompareEquality(QuantityDTO q1, QuantityDTO q2)
        {
            Validate(q1, q2);

            double base1 = ToBase(q1);
            double base2 = ToBase(q2);

            return Math.Abs(base1 - base2) < 0.0001;
        }

        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            if (source == null)
                throw new QuantityMeasurementException("Source quantity cannot be null");

            double baseValue = ToBase(source);

            double converted = FromBase(baseValue, targetUnit, source.MeasurementType);

            return new QuantityDTO(converted, targetUnit, source.MeasurementType);
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            Validate(q1, q2);

            if (q1.MeasurementType.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException("Temperature arithmetic not supported");

            double resultBase = ToBase(q1) + ToBase(q2);

            double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);

            return new QuantityDTO(result, q1.Unit, q1.MeasurementType);
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            Validate(q1, q2);

            if (q1.MeasurementType.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException("Temperature arithmetic not supported");

            double resultBase = ToBase(q1) - ToBase(q2);

            double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);

            return new QuantityDTO(result, q1.Unit, q1.MeasurementType);
        }

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            Validate(q1, q2);

            double base1 = ToBase(q1);
            double base2 = ToBase(q2);

            if (base2 == 0)
                throw new QuantityMeasurementException("Division by zero");

            return base1 / base2;
        }

        private void Validate(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null || q2 == null)
                throw new QuantityMeasurementException("Quantity cannot be null");

            if (!q1.MeasurementType.Equals(q2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException("Cross category operations not allowed");
        }

        private double ToBase(QuantityDTO q)
        {
            return q.MeasurementType.ToLower() switch
            {
                "length" => q.Unit.ToLower() switch
                {
                    "feet" => q.Value * 12,
                    "inch" or "inches" => q.Value,
                    "yard" => q.Value * 36,
                    "centimeter" => q.Value / 2.54,
                    _ => throw new QuantityMeasurementException("Unknown length unit")
                },

                "weight" => q.Unit.ToLower() switch
                {
                    "kg" => q.Value * 1000,
                    "g" => q.Value,
                    "tonne" => q.Value * 1_000_000,
                    _ => throw new QuantityMeasurementException("Unknown weight unit")
                },

                "volume" => q.Unit.ToLower() switch
                {
                    "litre" => q.Value * 1000,
                    "ml" => q.Value,
                    "gallon" => q.Value * 3785.41,
                    _ => throw new QuantityMeasurementException("Unknown volume unit")
                },

                "temperature" => q.Unit.ToLower() switch
                {
                    "celsius" => q.Value,
                    "fahrenheit" => (q.Value - 32) * 5 / 9,
                    "kelvin" => q.Value - 273.15,
                    _ => throw new QuantityMeasurementException("Unknown temperature unit")
                },

                _ => throw new QuantityMeasurementException("Unknown measurement type")
            };
        }

        private double FromBase(double value, string targetUnit, string type)
        {
            return type.ToLower() switch
            {
                "length" => targetUnit.ToLower() switch
                {
                    "feet" => value / 12,
                    "inch" or "inches" => value,
                    "yard" => value / 36,
                    "centimeter" => value * 2.54,
                    _ => throw new QuantityMeasurementException("Unknown length unit")
                },

                "weight" => targetUnit.ToLower() switch
                {
                    "kg" => value / 1000,
                    "g" => value,
                    "tonne" => value / 1_000_000,
                    _ => throw new QuantityMeasurementException("Unknown weight unit")
                },

                "volume" => targetUnit.ToLower() switch
                {
                    "litre" => value / 1000,
                    "ml" => value,
                    "gallon" => value / 3785.41,
                    _ => throw new QuantityMeasurementException("Unknown volume unit")
                },

                "temperature" => targetUnit.ToLower() switch
                {
                    "celsius" => value,
                    "fahrenheit" => value * 9 / 5 + 32,
                    "kelvin" => value + 273.15,
                    _ => throw new QuantityMeasurementException("Unknown temperature unit")
                },

                _ => throw new QuantityMeasurementException("Unknown measurement type")
            };
        }
    }
}