using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.DTO
{
    public class QuantityRequestDTO
    {
        [Required]
        public QuantityDTO First { get; set; }

        [Required]
        public QuantityDTO Second { get; set; }

        [Required]
        public string TargetUnit { get; set; }
    }
}