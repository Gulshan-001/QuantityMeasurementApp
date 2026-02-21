using QuantityMeasurementApp.Helpers;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new QuantityMeasurementService();

            Console.Write("Enter first value: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter first unit (feet/inches): ");
            string? unit1Input = Console.ReadLine();

            Console.Write("Enter second value: ");
            string? input2 = Console.ReadLine();

            Console.Write("Enter second unit (feet/inches): ");
            string? unit2Input = Console.ReadLine();

            if (InputHelper.TryParseDouble(input1, out double value1) &&
                InputHelper.TryParseDouble(input2, out double value2) &&
                Enum.TryParse(unit1Input, true, out LengthUnit unit1) &&
                Enum.TryParse(unit2Input, true, out LengthUnit unit2))
            {
                bool result = service.AreEqual(value1, unit1, value2, unit2);

                Console.WriteLine($"Equal: {result}");
            }
            else
            {
                Console.WriteLine("Invalid input or unsupported unit.");
            }
        }
    }
}