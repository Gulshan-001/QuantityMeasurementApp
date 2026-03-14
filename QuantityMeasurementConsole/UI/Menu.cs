using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementConsole.Interfaces;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementConsole.UI
{
    public class Menu : IMenu
    {
        private readonly QuantityMeasurementController _controller;

        private readonly string[] _mainMenu =
        {
            "Compare Quantities",
            "Convert Quantity",
            "Add Quantities",
            "Subtract Quantities",
            "Divide Quantities",
            "View History",
            "Exit"
        };

        public Menu(QuantityMeasurementController controller)
        {
            _controller = controller;
        }

        public void Start()
        {
            while (true)
            {
                int choice = SelectFromMenu("QUANTITY MEASUREMENT TERMINAL (UC15)", _mainMenu);

                if (choice == -1) continue;

                switch (choice)
                {
                    case 0: Compare(); break;
                    case 1: Convert(); break;
                    case 2: Add(); break;
                    case 3: Subtract(); break;
                    case 4: Divide(); break;
                    case 5: ShowHistory(); break;
                    case 6: Environment.Exit(0); break;
                }
            }
        }

        // ============================
        // UNIVERSAL ARROW MENU
        // ============================

        private int SelectFromMenu(string title, string[] options)
        {
            int index = 0;

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ {title.PadRight(50)} ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($" ▶ {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"   {options[i]}");
                    }
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Use ↑ ↓ arrows | ENTER to select | B to go back");
                Console.ResetColor();

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                    index = index == 0 ? options.Length - 1 : index - 1;

                else if (key.Key == ConsoleKey.DownArrow)
                    index = index == options.Length - 1 ? 0 : index + 1;

                else if (key.Key == ConsoleKey.Enter)
                    return index;

                else if (key.Key == ConsoleKey.B)
                    return -1;
            }
        }

        // ============================
        // MEASUREMENT TYPE
        // ============================

        private string? AskMeasurementType()
        {
            string[] types =
            {
                "Length",
                "Weight",
                "Volume",
                "Temperature"
            };

            int choice = SelectFromMenu("Select Measurement Type", types);

            if (choice == -1)
                return null;

            return types[choice].ToLower();
        }

        // ============================
        // UNIT SELECTION
        // ============================

        private string? AskUnit(string type)
        {
            string[] units = type switch
            {
                "length" => new[] { "Feet", "Inches", "Yard", "Centimeter" },
                "weight" => new[] { "Kg", "g", "tonne" },
                "volume" => new[] { "Litre", "ml", "gallon" },
                "temperature" => new[] { "Celsius", "Fahrenheit", "Kelvin" },
                _ => Array.Empty<string>()
            };

            int choice = SelectFromMenu("Select Unit", units);

            if (choice == -1)
                return null;

            return units[choice];
        }

        // ============================
        // VALUE INPUT
        // ============================

        private QuantityDTO? ReadQuantity(string type)
        {
            Console.Clear();

            Console.WriteLine("Enter Value (or type B to go back)");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Value ➤ ");
            Console.ResetColor();

            var input = Console.ReadLine();

            if (input?.ToLower() == "b")
                return null;

            if (!double.TryParse(input, out double value))
                throw new Exception("Invalid number");

            var unit = AskUnit(type);
            if (unit == null) return null;

            return new QuantityDTO(value, unit, type);
        }

        // ============================
        // OPERATIONS
        // ============================

        private void Compare()
        {
            var type = AskMeasurementType();
            if (type == null) return;

            var q1 = ReadQuantity(type);
            if (q1 == null) return;

            var q2 = ReadQuantity(type);
            if (q2 == null) return;

            _controller.PerformEquality(q1, q2);

            Pause();
        }

        private void Convert()
        {
            var type = AskMeasurementType();
            if (type == null) return;

            var source = ReadQuantity(type);
            if (source == null) return;

            var target = AskUnit(type);
            if (target == null) return;

            _controller.PerformConversion(source, target);

            Pause();
        }

        private void Add()
        {
            var type = AskMeasurementType();
            if (type == null) return;

            var q1 = ReadQuantity(type);
            if (q1 == null) return;

            var q2 = ReadQuantity(type);
            if (q2 == null) return;

            _controller.PerformAddition(q1, q2);

            Pause();
        }

        private void Subtract()
        {
            var type = AskMeasurementType();
            if (type == null) return;

            var q1 = ReadQuantity(type);
            if (q1 == null) return;

            var q2 = ReadQuantity(type);
            if (q2 == null) return;

            _controller.PerformSubtraction(q1, q2);

            Pause();
        }

        private void Divide()
        {
            var type = AskMeasurementType();
            if (type == null) return;

            var q1 = ReadQuantity(type);
            if (q1 == null) return;

            var q2 = ReadQuantity(type);
            if (q2 == null) return;

            _controller.PerformDivision(q1, q2);

            Pause();
        }

        private void ShowHistory()
        {
            Console.WriteLine("\nHistory feature coming soon...");
            Pause();
        }

        private void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}