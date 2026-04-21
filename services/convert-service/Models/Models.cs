using System.ComponentModel.DataAnnotations;

namespace ConvertService.Models
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        [Required]
        public string OperationType { get; set; } = string.Empty;

        public int OperationCode { get; set; }

        [Required]
        public string InputType { get; set; } = string.Empty;

        [Required]
        public string OutputType { get; set; } = string.Empty;

        [Required]
        public string InputData { get; set; } = string.Empty;

        [Required]
        public string ResultData { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }

        [Required]
        public string ErrorMessage { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class QuantityDTO
    {
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string MeasurementType { get; set; } = string.Empty;
    }

    public class QuantityRequestDTO
    {
        [Required]
        public QuantityDTO First { get; set; } = null!;

        [Required]
        public QuantityDTO Second { get; set; } = null!;

        public string? TargetUnit { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
