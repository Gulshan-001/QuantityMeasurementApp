using ArithmeticService.Models;
using ArithmeticService.Data;

namespace ArithmeticService.Services
{
    public class QuantityException : Exception
    {
        public QuantityException(string message) : base(message) { }
    }

    public interface IArithmeticLogicService
    {
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);
        double Divide(QuantityDTO q1, QuantityDTO q2);
        List<QuantityMeasurementEntity> GetAllHistory();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        int GetOperationCount(string operation);
    }

    public class ArithmeticLogicService : IArithmeticLogicService
    {
        private readonly IMeasurementRepository _repository;

        public ArithmeticLogicService(IMeasurementRepository repository)
        {
            _repository = repository;
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                Validate(q1, q2);
                if (q1.MeasurementType.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                    throw new QuantityException("Temperature arithmetic not supported");

                double resultBase = ToBase(q1) + ToBase(q2);
                double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);
                var output = new QuantityDTO { Value = result, Unit = q1.Unit, MeasurementType = q1.MeasurementType };

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
                    throw new QuantityException("Temperature arithmetic not supported");

                double resultBase = ToBase(q1) - ToBase(q2);
                double result = FromBase(resultBase, q1.Unit, q1.MeasurementType);
                var output = new QuantityDTO { Value = result, Unit = q1.Unit, MeasurementType = q1.MeasurementType };

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

                if (base2 == 0) throw new QuantityException("Division by zero");

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

        public List<QuantityMeasurementEntity> GetAllHistory() => _repository.GetAll();

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
            => _repository.GetByOperation(operation);

        public int GetOperationCount(string operation)
            => _repository.GetOperationCount(operation);

        private void SaveLog(string opType, int opCode, string? type,
            string input, string? result, bool success, string? error)
        {
            _repository.Save(new QuantityMeasurementEntity
            {
                OperationType = opType,
                OperationCode = opCode,
                InputType = type ?? "",
                OutputType = type ?? "",
                InputData = input,
                ResultData = result ?? "",
                IsSuccess = success,
                ErrorMessage = error ?? ""
            });
        }

        private void Validate(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null || q2 == null) throw new QuantityException("Quantity cannot be null");
            if (!q1.MeasurementType.Equals(q2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                throw new QuantityException("Cross category operations not allowed");
        }

        private double ToBase(QuantityDTO q) => q.MeasurementType.ToLower() switch
        {
            "length" => q.Unit.ToLower() switch
            {
                "feet" => q.Value * 12,
                "inch" or "inches" => q.Value,
                "yard" => q.Value * 36,
                "centimeter" => q.Value / 2.54,
                _ => throw new QuantityException("Unknown length unit")
            },
            "weight" => q.Unit.ToLower() switch
            {
                "kg" => q.Value * 1000,
                "g" => q.Value,
                "tonne" => q.Value * 1_000_000,
                _ => throw new QuantityException("Unknown weight unit")
            },
            "volume" => q.Unit.ToLower() switch
            {
                "litre" => q.Value * 1000,
                "ml" => q.Value,
                "gallon" => q.Value * 3785.41,
                _ => throw new QuantityException("Unknown volume unit")
            },
            "temperature" => q.Unit.ToLower() switch
            {
                "celsius" => q.Value,
                "fahrenheit" => (q.Value - 32) * 5 / 9,
                "kelvin" => q.Value - 273.15,
                _ => throw new QuantityException("Unknown temperature unit")
            },
            _ => throw new QuantityException("Unknown measurement type")
        };

        private double FromBase(double value, string targetUnit, string type) => type.ToLower() switch
        {
            "length" => targetUnit.ToLower() switch
            {
                "feet" => value / 12,
                "inch" or "inches" => value,
                "yard" => value / 36,
                "centimeter" => value * 2.54,
                _ => throw new QuantityException("Unknown length unit")
            },
            "weight" => targetUnit.ToLower() switch
            {
                "kg" => value / 1000,
                "g" => value,
                "tonne" => value / 1_000_000,
                _ => throw new QuantityException("Unknown weight unit")
            },
            "volume" => targetUnit.ToLower() switch
            {
                "litre" => value / 1000,
                "ml" => value,
                "gallon" => value / 3785.41,
                _ => throw new QuantityException("Unknown volume unit")
            },
            "temperature" => targetUnit.ToLower() switch
            {
                "celsius" => value,
                "fahrenheit" => value * 9 / 5 + 32,
                "kelvin" => value + 273.15,
                _ => throw new QuantityException("Unknown temperature unit")
            },
            _ => throw new QuantityException("Unknown measurement type")
        };
    }
}
