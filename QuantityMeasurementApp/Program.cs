using QuantityMeasurementApp.Helpers;
using QuantityMeasurementApp.Services;
using System;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new QuantityMeasurementService();

            Console.WriteLine("Select Unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.Write("Enter choice (1 or 2): ");

            string? choice = Console.ReadLine();

            Console.Write("Enter first value: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter second value: ");
            string? input2 = Console.ReadLine();

            if (InputHelper.TryParseDouble(input1, out double value1) &&
                InputHelper.TryParseDouble(input2, out double value2))
            {
                bool result = false;

                switch (choice)
                {
                    case "1":
                        result = service.AreFeetEqual(value1, value2);
                        Console.WriteLine($"Feet Equal: {result}");
                        break;

                    case "2":
                        result = service.AreInchesEqual(value1, value2);
                        Console.WriteLine($"Inches Equal: {result}");
                        break;

                    default:
                        Console.WriteLine("Invalid unit selection.");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter numeric values.");
            }
        }
    }
}