namespace ProductManagement;

/// <summary>
/// Shared console input helpers used by any strategy or UI class.
/// </summary>
public static class ConsoleHelpers
{
    public static string ReadRequired(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input!;
    }

    public static string ReadOptional(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
                return value;
            Console.WriteLine("Invalid value. Please enter a valid decimal number.");
        }
    }

    public static int ReadInt(string prompt)
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
