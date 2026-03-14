using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementConsole.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        public void PerformEquality(QuantityDTO q1, QuantityDTO q2)
        {
            bool result = _service.CompareEquality(q1, q2);
            Console.WriteLine($"Equality Result: {result}");
        }

        public void PerformConversion(QuantityDTO source, string targetUnit)
        {
            var result = _service.Convert(source, targetUnit);
            Console.WriteLine($"Converted Result: {result}");
        }

        public void PerformAddition(QuantityDTO q1, QuantityDTO q2)
        {
            var result = _service.Add(q1, q2);
            Console.WriteLine($"Addition Result: {result}");
        }

        public void PerformSubtraction(QuantityDTO q1, QuantityDTO q2)
        {
            var result = _service.Subtract(q1, q2);
            Console.WriteLine($"Subtraction Result: {result}");
        }

        public void PerformDivision(QuantityDTO q1, QuantityDTO q2)
        {
            var result = _service.Divide(q1, q2);
            Console.WriteLine($"Division Result: {result}");
        }
    }
}