namespace QuantityMeasurementModelLayer.DTO
{
    using System.ComponentModel.DataAnnotations;

public class QuantityDTO
{
    [Required]
    public double Value { get; set; }

    [Required]
    public string Unit { get; set; }

    [Required]
    public string MeasurementType { get; set; }
}
}