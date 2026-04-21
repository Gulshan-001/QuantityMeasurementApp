using CompareService.Models;
using CompareService.Data;

namespace CompareService.Services
{
    public class QuantityException : Exception
    {
        public QuantityException(string message) : base(message) { }
    }

    public interface ICompareLogicService
    {
        bool CompareEquality(QuantityDTO q1, QuantityDTO q2);
        List<QuantityMeasurementEntity> GetAllHistory();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        int GetOperationCount(string operation);
    }

    public class CompareLogicService : ICompareLogicService
    {
        private readonly IMeasurementRepository _repository;

        public CompareLogicService(IMeasurementRepository repository)
        {
            _repository = repository;
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
            if (q1 == null || q2 == null)
                throw new QuantityException("Quantity cannot be null");
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
    }
}
