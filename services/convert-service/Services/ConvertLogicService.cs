using ConvertService.Models;
using ConvertService.Data;

namespace ConvertService.Services
{
    public class QuantityException : Exception
    {
        public QuantityException(string message) : base(message) { }
    }

    public interface IConvertLogicService
    {
        QuantityDTO Convert(QuantityDTO source, string targetUnit);
        List<QuantityMeasurementEntity> GetAllHistory();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        int GetOperationCount(string operation);
    }

    public class ConvertLogicService : IConvertLogicService
    {
        private readonly IMeasurementRepository _repository;

        public ConvertLogicService(IMeasurementRepository repository)
        {
            _repository = repository;
        }

        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            try
            {
                if (source == null) throw new QuantityException("Source cannot be null");

                double baseValue = ToBase(source);
                double converted = FromBase(baseValue, targetUnit, source.MeasurementType);

                var result = new QuantityDTO
                {
                    Value = converted,
                    Unit = targetUnit,
                    MeasurementType = source.MeasurementType
                };

                SaveLog("Convert", 1, source.MeasurementType,
                    $"{source.Value} {source.Unit} → {targetUnit}",
                    $"{converted} {targetUnit}", true, null);

                return result;
            }
            catch (Exception ex)
            {
                SaveLog("Convert", 1, source?.MeasurementType,
                    $"{source?.Value} {source?.Unit}", null, false, ex.Message);
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
