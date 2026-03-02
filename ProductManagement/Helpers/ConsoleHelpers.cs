using ProductManagement.Validation.Interfaces;
using ProductManagement.Validation;
namespace ProductManagement.Helpers;

public static class ConsoleHelpers
{
    public static string ReadUntilValid(string prompt, IInputValidator validator)
    {
        return validator.ReadRequired(prompt).Trim();
    }

}
