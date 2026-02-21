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

            Console.Write("Enter first value in feet: ");
            string? input1 = Console.ReadLine();

            Console.Write("Enter second value in feet: ");
            string? input2 = Console.ReadLine();

            if (InputHelper.TryParseDouble(input1, out double value1) &&
                InputHelper.TryParseDouble(input2, out double value2))
            {
                bool result = service.AreEqual(value1, value2);
                Console.WriteLine($"Equal: {result}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter numeric values.");
            }
        }
    }
}