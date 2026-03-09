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
                Console.WriteLine("1. Compare Lengths");
                Console.WriteLine("2. Convert Length");
                Console.WriteLine("3. Add Lengths");
                Console.WriteLine("4. Compare Weights");
                Console.WriteLine("5. Convert Weight");
                Console.WriteLine("6. Add Weights");
                Console.WriteLine("7. Compare Volumes");
                Console.WriteLine("8. Convert Volume");
                Console.WriteLine("9. Add Volumes");
                Console.WriteLine("10. Exit");
                Console.Write("Select option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CompareLength(service);
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
                        CompareVolume();
                        break;

                    case "8":
                        ConvertVolume();
                        break;

                    case "9":
                        AddVolume();
                        break;

                    case "10":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // ===================== LENGTH =====================

        private static void CompareLength(QuantityMeasurementService service)
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            bool result = service.AreEqual(q1, q2);
            Console.WriteLine($"Equal: {result}");
        }

        private static void ConvertLength()
        {
            if (!ReadLength(out var q))
                return;

            Console.Write("Enter target unit (Feet/Inches/Yards/Centimeters): ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out LengthUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = q.ConvertTo(targetUnit);

            Console.WriteLine($"Converted Value: {result.Value} {result.Unit}");
        }

        private static void AddLength()
        {
            if (!ReadLength(out var q1) || !ReadLength(out var q2))
                return;

            Console.Write("Enter target unit: ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out LengthUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = q1.Add(q2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadLength(out Quantity<LengthUnit> quantity)
        {
            quantity = null!;

            Console.Write("Enter value: ");
            string? valueInput = Console.ReadLine();

            Console.Write("Enter unit (Feet/Inches/Yards/Centimeters): ");
            string? unitInput = Console.ReadLine();

            if (!InputHelper.TryParseDouble(valueInput, out double value) ||
                !Enum.TryParse(unitInput, true, out LengthUnit unit))
            {
                Console.WriteLine("Invalid input.");
                return false;
            }

            quantity = new Quantity<LengthUnit>(value, unit);
            return true;
        }

        // ===================== WEIGHT =====================

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
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out WeightUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = w.ConvertTo(targetUnit);

            Console.WriteLine($"Converted Value: {result.Value} {result.Unit}");
        }

        private static void AddWeight()
        {
            if (!ReadWeight(out var w1) || !ReadWeight(out var w2))
                return;

            Console.Write("Enter target unit: ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out WeightUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = w1.Add(w2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadWeight(out Quantity<WeightUnit> weight)
        {
            weight = null!;

            Console.Write("Enter value: ");
            string? valueInput = Console.ReadLine();

            Console.Write("Enter unit (Kilogram/Gram/Pound): ");
            string? unitInput = Console.ReadLine();

            if (!InputHelper.TryParseDouble(valueInput, out double value) ||
                !Enum.TryParse(unitInput, true, out WeightUnit unit))
            {
                Console.WriteLine("Invalid input.");
                return false;
            }

            weight = new Quantity<WeightUnit>(value, unit);
            return true;
        }

        // ===================== VOLUME (UC11) =====================

        private static void CompareVolume()
        {
            if (!ReadVolume(out var v1) || !ReadVolume(out var v2))
                return;

            Console.WriteLine($"Equal: {v1.Equals(v2)}");
        }

        private static void ConvertVolume()
        {
            if (!ReadVolume(out var v))
                return;

            Console.Write("Enter target unit (Litre/Millilitre/Gallon): ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out VolumeUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = v.ConvertTo(targetUnit);

            Console.WriteLine($"Converted Value: {result.Value} {result.Unit}");
        }

        private static void AddVolume()
        {
            if (!ReadVolume(out var v1) || !ReadVolume(out var v2))
                return;

            Console.Write("Enter target unit: ");
            string? targetInput = Console.ReadLine();

            if (!Enum.TryParse(targetInput, true, out VolumeUnit targetUnit))
            {
                Console.WriteLine("Invalid unit.");
                return;
            }

            var result = v1.Add(v2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private static bool ReadVolume(out Quantity<VolumeUnit> volume)
        {
            volume = null!;

            Console.Write("Enter value: ");
            string? valueInput = Console.ReadLine();

            Console.Write("Enter unit (Litre/Millilitre/Gallon): ");
            string? unitInput = Console.ReadLine();

            if (!InputHelper.TryParseDouble(valueInput, out double value) ||
                !Enum.TryParse(unitInput, true, out VolumeUnit unit))
            {
                Console.WriteLine("Invalid input.");
                return false;
            }

            volume = new Quantity<VolumeUnit>(value, unit);
            return true;
        }
    }
}