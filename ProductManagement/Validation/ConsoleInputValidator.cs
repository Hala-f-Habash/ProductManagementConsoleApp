using ProductManagement.Validation.Interfaces;

namespace ProductManagement.Validation
{
    public class ConsoleInputValidator : IInputValidator
    {
        public string ReadRequired(string prompt)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));

            return input!;
        }

        public string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;
                Console.WriteLine("Invalid value. Please enter a valid decimal number.");
            }
        }

        public int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Invalid value. Please enter a whole number.");
            }
        }
    }
}