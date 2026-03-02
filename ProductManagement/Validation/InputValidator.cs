using ProductManagement.Validation.Interfaces;

namespace ProductManagement.Validation
{
    public class InputValidator : IInputValidator
    {
        public bool TryReadRequired(string prompt, out string input)
        {
            input = string.Empty;

            Console.Write(prompt);
            input = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return true;
        }

        public string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public bool TryReadDecimal(string prompt, out decimal value)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out value))
            {
                return true;
            }
            return false;
        }

        public bool TryReadInt(string prompt, out int value)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return true;
            }
            return false;

        }
    }
}