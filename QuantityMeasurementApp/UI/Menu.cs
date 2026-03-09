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

                Console.WriteLine("----- LENGTH -----");
                Console.WriteLine("1. Compare Length");
                Console.WriteLine("2. Convert Length");
                Console.WriteLine("3. Add Length");
                Console.WriteLine("4. Subtract Length");
                Console.WriteLine("5. Divide Length");

                Console.WriteLine("\n----- WEIGHT -----");
                Console.WriteLine("6. Compare Weight");
                Console.WriteLine("7. Convert Weight");
                Console.WriteLine("8. Add Weight");
                Console.WriteLine("9. Subtract Weight");
                Console.WriteLine("10. Divide Weight");

                Console.WriteLine("\n----- VOLUME -----");
                Console.WriteLine("11. Compare Volume");
                Console.WriteLine("12. Convert Volume");
                Console.WriteLine("13. Add Volume");
                Console.WriteLine("14. Subtract Volume");
                Console.WriteLine("15. Divide Volume");

                Console.WriteLine("\n16. Exit");

                Console.Write("\nSelect option: ");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1": CompareLength(); break;
                    case "2": ConvertLength(); break;
                    case "3": AddLength(); break;
                    case "4": SubtractLength(); break;
                    case "5": DivideLength(); break;

                    case "6": CompareWeight(); break;
                    case "7": ConvertWeight(); break;
                    case "8": AddWeight(); break;
                    case "9": SubtractWeight(); break;
                    case "10": DivideWeight(); break;

                    case "11": CompareVolume(); break;
                    case "12": ConvertVolume(); break;
                    case "13": AddVolume(); break;
                    case "14": SubtractVolume(); break;
                    case "15": DivideVolume(); break;

                    case "16": return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // ---------------- LENGTH ----------------

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

            Console.Write("Enter target unit: ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit target))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = q.ConvertTo(target);
            Console.WriteLine($"Converted: {result.Value} {result.Unit}");
        }

        private static void AddLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            var result = q1.Add(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void SubtractLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            var result = q1.Subtract(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void DivideLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            double result = q1.Divide(q2);
            Console.WriteLine($"Ratio: {result}");
        }

        private static bool ReadLength(out Quantity<LengthUnit> quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            if (!InputHelper.TryParseDouble(Console.ReadLine(), out double value))
                return false;

            Console.Write("Enter unit (Feet/Inches/Yards/Centimeters): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out LengthUnit unit))
                return false;

            quantity = new Quantity<LengthUnit>(value, unit);
            return true;
        }

        // ---------------- WEIGHT ----------------

        private static void CompareWeight()
        {
            if (!ReadWeight(out var q1) || !ReadWeight(out var q2))
                return;

            Console.WriteLine($"Equal: {q1.Equals(q2)}");
        }

        private static void ConvertWeight()
        {
            if (!ReadWeight(out var q))
                return;

            Console.Write("Enter target unit: ");
            if (!Enum.TryParse(Console.ReadLine(), true, out WeightUnit target))
                return;

            var result = q.ConvertTo(target);
            Console.WriteLine($"Converted: {result.Value} {result.Unit}");
        }

        private static void AddWeight()
        {
            if (!ReadWeight(out var q1) || !ReadWeight(out var q2))
                return;

            var result = q1.Add(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void SubtractWeight()
        {
            if (!ReadWeight(out var q1) || !ReadWeight(out var q2))
                return;

            var result = q1.Subtract(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void DivideWeight()
        {
            if (!ReadWeight(out var q1) || !ReadWeight(out var q2))
                return;

            double result = q1.Divide(q2);
            Console.WriteLine($"Ratio: {result}");
        }

        private static bool ReadWeight(out Quantity<WeightUnit> quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            if (!InputHelper.TryParseDouble(Console.ReadLine(), out double value))
                return false;

            Console.Write("Enter unit (Kilogram/Gram/Pound): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out WeightUnit unit))
                return false;

            quantity = new Quantity<WeightUnit>(value, unit);
            return true;
        }

        // ---------------- VOLUME ----------------

        private static void CompareVolume()
        {
            if (!ReadVolume(out var q1) || !ReadVolume(out var q2))
                return;

            Console.WriteLine($"Equal: {q1.Equals(q2)}");
        }

        private static void ConvertVolume()
        {
            if (!ReadVolume(out var q))
                return;

            Console.Write("Enter target unit: ");
            if (!Enum.TryParse(Console.ReadLine(), true, out VolumeUnit target))
                return;

            var result = q.ConvertTo(target);
            Console.WriteLine($"Converted: {result.Value} {result.Unit}");
        }

        private static void AddVolume()
        {
            if (!ReadVolume(out var q1) || !ReadVolume(out var q2))
                return;

            var result = q1.Add(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void SubtractVolume()
        {
            if (!ReadVolume(out var q1) || !ReadVolume(out var q2))
                return;

            var result = q1.Subtract(q2);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static void DivideVolume()
        {
            if (!ReadVolume(out var q1) || !ReadVolume(out var q2))
                return;

            double result = q1.Divide(q2);
            Console.WriteLine($"Ratio: {result}");
        }

        private static bool ReadVolume(out Quantity<VolumeUnit> quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            if (!InputHelper.TryParseDouble(Console.ReadLine(), out double value))
                return false;

            Console.Write("Enter unit (Litre/Millilitre/Gallon): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out VolumeUnit unit))
                return false;

            quantity = new Quantity<VolumeUnit>(value, unit);
            return true;
        }
    }
}