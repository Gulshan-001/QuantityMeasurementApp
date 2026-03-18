using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementConsole.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void PerformEquality(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                bool result = _service.CompareEquality(q1, q2);
                Console.WriteLine($"Equality Result: {result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected Error] {ex.Message}");
            }
        }

        public void PerformConversion(QuantityDTO source, string targetUnit)
        {
            try
            {
                var result = _service.Convert(source, targetUnit);
                Console.WriteLine($"Converted Result: {result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected Error] {ex.Message}");
            }
        }

        public void PerformAddition(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                var result = _service.Add(q1, q2);
                Console.WriteLine($"Addition Result: {result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected Error] {ex.Message}");
            }
        }

        public void PerformSubtraction(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                var result = _service.Subtract(q1, q2);
                Console.WriteLine($"Subtraction Result: {result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected Error] {ex.Message}");
            }
        }

        public void PerformDivision(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                var result = _service.Divide(q1, q2);
                Console.WriteLine($"Division Result: {result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected Error] {ex.Message}");
            }
        }
    }
}