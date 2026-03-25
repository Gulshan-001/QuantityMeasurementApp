using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModel.Entities
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        [Required]
        public string OperationType { get; set; } = string.Empty;      // Add, Convert, etc

        public int OperationCode { get; set; }                         // 0–4

        [Required]
        public string InputType { get; set; } = string.Empty;          // Length, Weight, etc

        [Required]
        public string OutputType { get; set; } = string.Empty;         // same or target

        [Required]
        public string InputData { get; set; } = string.Empty;          // "5 Feet + 10 Feet"

        [Required]
        public string ResultData { get; set; } = string.Empty;         // "15 Feet"

        public bool IsSuccess { get; set; }

        [Required]
        public string ErrorMessage { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}