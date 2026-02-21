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

            Console.WriteLine("Supported Units: Feet, Inches, Yards, Centimeters");

            Console.Write("Enter first value: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter first unit: ");
            string? unitInput1 = Console.ReadLine();

            Console.Write("Enter second value: ");
            string? input2 = Console.ReadLine();

            Console.Write("Enter second unit: ");
            string? unitInput2 = Console.ReadLine();

            if (InputHelper.TryParseDouble(input1, out double value1) &&
                InputHelper.TryParseDouble(input2, out double value2) &&
                Enum.TryParse(unitInput1, true, out LengthUnit unit1) &&
                Enum.TryParse(unitInput2, true, out LengthUnit unit2))
            {
                bool result = service.AreEqual(value1, unit1, value2, unit2);

                Console.WriteLine($"Equal: {result}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter valid numeric values and supported units.");
            }
        }
    }
}