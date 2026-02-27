using QuantityMeasurementApp.Helpers;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.UI
{
    public static class Menu
    {
        public static void Start()
        {
            var service = new QuantityMeasurementService();

            while (true)
            {
                Console.WriteLine("\n==== Quantity Measurement Menu ====");
                Console.WriteLine("1. Compare Quantities");
                Console.WriteLine("2. Convert Units");
                Console.WriteLine("3. Add Quantities");
                Console.WriteLine("4. Exit");
                Console.Write("Select option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Compare(service);
                        break;
                    case "2":
                        Convert();
                        break;
                    case "3":
                        Add();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void Compare(QuantityMeasurementService service)
        {
            if (!ReadQuantity(out var q1) || !ReadQuantity(out var q2))
                return;

            bool result = service.AreEqual(q1.Value, q1.Unit, q2.Value, q2.Unit);
            Console.WriteLine($"Equal: {result}");
        }

        private static void Convert()
        {
            if (!ReadQuantity(out var q))
                return;

            Console.Write("Enter target unit: ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out LengthUnit targetUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            double result = q.ConvertTo(targetUnit);
            Console.WriteLine($"Converted Value: {result} {targetUnit}");
        }

        private static void Add()
        {
            if (!ReadQuantity(out var q1) || !ReadQuantity(out var q2))
                return;

            var result = q1.Add(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadQuantity(out QuantityLength quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            string? valueInput = Console.ReadLine();

            Console.Write("Enter unit (Feet/Inches/Yards/Centimeters): ");
            string? unitInput = Console.ReadLine();

            if (!InputHelper.TryParseDouble(valueInput, out double value) ||
                !Enum.TryParse(unitInput, true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid value or unit.");
                return false;
            }

            quantity = new QuantityLength(value, unit);
            return true;
        }
    }
}