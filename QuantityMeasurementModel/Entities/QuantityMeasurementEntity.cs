namespace QuantityMeasurementModel.Entities
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        public string OperationType { get; set; }      // Add, Convert, etc
        public int OperationCode { get; set; }         // 0–4

        public string InputType { get; set; }          // Length, Weight, etc
        public string OutputType { get; set; }         // same or target

        public string InputData { get; set; }          // "5 Feet + 10 Feet"
        public string ResultData { get; set; }         // "15 Feet"

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}