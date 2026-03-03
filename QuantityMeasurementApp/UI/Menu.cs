using QuantityMeasurementApp.Helpers;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("\n==== Quantity Measurement Menu ====");
                Console.WriteLine("1. Compare Lengths");
                Console.WriteLine("2. Convert Length");
                Console.WriteLine("3. Add Lengths");
                Console.WriteLine("4. Compare Weights");
                Console.WriteLine("5. Convert Weight");
                Console.WriteLine("6. Add Weights");
                Console.WriteLine("7. Exit");
                Console.Write("Select option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CompareLength();
                        break;
                    case "2":
                        ConvertLength();
                        break;
                    case "3":
                        AddLength();
                        break;
                    case "4":
                        CompareWeight();
                        break;
                    case "5":
                        ConvertWeight();
                        break;
                    case "6":
                        AddWeight();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // ===================== LENGTH SECTION =====================

        private static void CompareLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            Console.WriteLine($"Equal: {q1.Equals(q2)}");
        }

        private static void ConvertLength()
        {
            if (!ReadLength(out var q))
                return;

            Console.Write("Enter target unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit targetUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            var result = q.ConvertTo(targetUnit);

            Console.WriteLine($"Converted Value: {result.Value} {result.Unit}");
        }

        private static void AddLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            Console.Write("Enter target unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit targetUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            var result = q1.Add(q2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadLength(out Quantity<LengthUnit> quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            if (!InputHelper.TryParseDouble(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid value.");
                return false;
            }

            Console.Write("Enter unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid unit.");
                return false;
            }

            quantity = new Quantity<LengthUnit>(value, unit);
            return true;
        }

        // ===================== WEIGHT SECTION =====================

        private static void CompareWeight()
        {
            if (!ReadWeight(out var w1) || !ReadWeight(out var w2))
                return;

            Console.WriteLine($"Equal: {w1.Equals(w2)}");
        }

        private static void ConvertWeight()
        {
            if (!ReadWeight(out var w))
                return;

            Console.Write("Enter target unit (Kilogram/Gram/Pound): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out WeightUnit targetUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            var result = w.ConvertTo(targetUnit);

            Console.WriteLine($"Converted Value: {result.Value} {result.Unit}");
        }

        private static void AddWeight()
        {
            if (!ReadWeight(out var w1) || !ReadWeight(out var w2))
                return;

            Console.Write("Enter target unit (Kilogram/Gram/Pound): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out WeightUnit targetUnit))
            {
                Console.WriteLine("Invalid target unit.");
                return;
            }

            var result = w1.Add(w2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadWeight(out Quantity<WeightUnit> weight)
        {
            weight = null!;

            Console.Write("Enter value: ");
            if (!InputHelper.TryParseDouble(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid value.");
                return false;
            }

            Console.Write("Enter unit (Kilogram/Gram/Pound): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out WeightUnit unit))
            {
                Console.WriteLine("Invalid unit.");
                return false;
            }

            weight = new Quantity<WeightUnit>(value, unit);
            return true;
        }
    }
}