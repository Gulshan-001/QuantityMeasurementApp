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
            Console.WriteLine("1. Compare Quantities");
            Console.WriteLine("2. Convert Units");
            Console.Write("Select option (1 or 2): ");

            string? option = Console.ReadLine();

            Console.Write("Enter first value: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter first unit: ");
            string? unit1Input = Console.ReadLine();

            if (!InputHelper.TryParseDouble(input1, out double value1) ||
                !Enum.TryParse(unit1Input, true, out LengthUnit unit1))
            {
                Console.WriteLine("Invalid first value or unit.");
                return;
            }

            if (option == "1")
            {
                Console.Write("Enter second value: ");
                string? input2 = Console.ReadLine();

                Console.Write("Enter second unit: ");
                string? unit2Input = Console.ReadLine();

                if (!InputHelper.TryParseDouble(input2, out double value2) ||
                    !Enum.TryParse(unit2Input, true, out LengthUnit unit2))
                {
                    Console.WriteLine("Invalid second value or unit.");
                    return;
                }

                bool result = service.AreEqual(value1, unit1, value2, unit2);
                Console.WriteLine($"Equal: {result}");
            }
            else if (option == "2")
            {
                Console.Write("Enter target unit: ");
                string? targetUnitInput = Console.ReadLine();

                if (!Enum.TryParse(targetUnitInput, true, out LengthUnit targetUnit))
                {
                    Console.WriteLine("Invalid target unit.");
                    return;
                }

                double converted = QuantityLength.Convert(value1, unit1, targetUnit);
                Console.WriteLine($"Converted Value: {converted}");
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }
    }
}