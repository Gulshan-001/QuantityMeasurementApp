using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModel.DTOs
{
    public class QuantityRequestDTO
    {
        [Required]
        public QuantityDTO First { get; set; } = null!;

        [Required]
        public QuantityDTO Second { get; set; } = null!;

        public string? TargetUnit { get; set; }   
    }
}