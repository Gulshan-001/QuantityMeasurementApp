using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                Validate(q1, q2);

                double base1 = ToBase(q1);
                double base2 = ToBase(q2);

                bool result = Math.Abs(base1 - base2) < 0.0001;

                SaveLog("Compare", 0, q1.MeasurementType,
                    $"{q1.Value} {q1.Unit} == {q2.Value} {q2.Unit}",
                    result.ToString(), true, null);

                return result;
            }
            catch (Exception ex)
            {
                SaveLog("Compare", 0, q1?.MeasurementType,
                    $"{q1?.Value} {q1?.Unit} == {q2?.Value} {q2?.Unit}",
                    null, false, ex.Message);

                throw;
            }
        }

        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            try
            {
                if (source == null)
                    throw new QuantityMeasurementException("Source cannot be null");

                double baseValue = ToBase(source);
                double converted = FromBase(baseValue, targetUnit, source.MeasurementType);

                var result = new QuantityDTO(converted, targetUnit, source.MeasurementType);

                SaveLog("Convert", 1, source.MeasurementType,
                    $"{source.Value} {source.Unit} → {targetUnit}",
                    $"{converted} {targetUnit}", true, null);

                return result;
            }
            catch (Exception ex)
            {
                SaveLog("Convert", 1, source?.MeasurementType,
                    $"{source?.Value} {source?.Unit}",
                    null, false, ex.Message);

                throw;
            }
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                Validate(q1, q2);

                if (q1.MeasurementType.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException("Temperature arithmetic not supported");

                double resultBase = ToBase(q1) + ToBase(q2);
                double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);

                var output = new QuantityDTO(result, q1.Unit, q1.MeasurementType);

                SaveLog("Add", 2, q1.MeasurementType,
                    $"{q1.Value} {q1.Unit} + {q2.Value} {q2.Unit}",
                    $"{result} {q1.Unit}", true, null);

                return output;
            }
            catch (Exception ex)
            {
                SaveLog("Add", 2, q1?.MeasurementType,
                    $"{q1?.Value} {q1?.Unit} + {q2?.Value} {q2?.Unit}",
                    null, false, ex.Message);

                throw;
            }
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                Validate(q1, q2);

                if (q1.MeasurementType.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                    throw new QuantityMeasurementException("Temperature arithmetic not supported");

                double resultBase = ToBase(q1) - ToBase(q2);
                double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);

                var output = new QuantityDTO(result, q1.Unit, q1.MeasurementType);

                SaveLog("Subtract", 3, q1.MeasurementType,
                    $"{q1.Value} {q1.Unit} - {q2.Value} {q2.Unit}",
                    $"{result} {q1.Unit}", true, null);

                return output;
            }
            catch (Exception ex)
            {
                SaveLog("Subtract", 3, q1?.MeasurementType,
                    $"{q1?.Value} {q1?.Unit} - {q2?.Value} {q2?.Unit}",
                    null, false, ex.Message);

                throw;
            }
        }

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                Validate(q1, q2);

                double base1 = ToBase(q1);
                double base2 = ToBase(q2);

                if (base2 == 0)
                    throw new QuantityMeasurementException("Division by zero");

                double result = base1 / base2;

                SaveLog("Divide", 4, q1.MeasurementType,
                    $"{q1.Value} {q1.Unit} / {q2.Value} {q2.Unit}",
                    result.ToString(), true, null);

                return result;
            }
            catch (Exception ex)
            {
                SaveLog("Divide", 4, q1?.MeasurementType,
                    $"{q1?.Value} {q1?.Unit} / {q2?.Value} {q2?.Unit}",
                    null, false, ex.Message);

                throw;
            }
        }

        // ============================
        // 🔥 NEW LOGGING METHOD
        // ============================

        private void SaveLog(string opType, int opCode, string type,
            string input, string result, bool success, string error)
        {
            var entity = new QuantityMeasurementEntity
            {
                OperationType = opType,
                OperationCode = opCode,
                InputType = type,
                OutputType = type,
                InputData = input,
                ResultData = result,
                IsSuccess = success,
                ErrorMessage = error
            };

            _repository.Save(entity);
        }

        // ============================
        // (UNCHANGED CORE LOGIC)
        // ============================

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
        public List<QuantityMeasurementEntity> GetAllHistory()
{
    return _repository.GetAll();
}

public List<QuantityMeasurementEntity> GetByOperation(string operation)
{
    return _repository.GetAll()
                      .Where(x => x.OperationType.ToLower() == operation.ToLower())
                      .ToList();
}

public int GetOperationCount(string operation)
{
    return _repository.GetAll()
                      .Count(x => x.OperationType.ToLower() == operation.ToLower());
}
    }
}