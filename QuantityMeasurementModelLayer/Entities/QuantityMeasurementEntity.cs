using System;

namespace QuantityMeasurementModelLayer.Entities
{
    [Serializable]
    public class QuantityMeasurementEntity
    {
        public string OperationType { get; }
        public string Operand1 { get; }
        public string Operand2 { get; }
        public string Result { get; }
        public bool HasError { get; }
        public string ErrorMessage { get; }

        public QuantityMeasurementEntity(string operationType, string operand1, string operand2, string result)
        {
            OperationType = operationType;
            Operand1 = operand1;
            Operand2 = operand2;
            Result = result;
            HasError = false;
            ErrorMessage = null;
        }

        public QuantityMeasurementEntity(string operationType, string errorMessage)
        {
            OperationType = operationType;
            HasError = true;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            if (HasError)
                return $"Operation: {OperationType} | ERROR: {ErrorMessage}";

            return $"Operation: {OperationType} | {Operand1} , {Operand2} => Result: {Result}";
        }
    }
}