using ProductManagement.Validation.Interfaces;
using ProductManagement.Validation;
namespace ProductManagement.Helpers;

public class ConsoleHelpers
{
    private IInputValidator validator;
    public ConsoleHelpers(IInputValidator validator){
        this.validator = validator;
    }
    
    public string ReadUntilValid(string prompt)
    {
        return this.validator.ReadRequired(prompt).Trim();
    }

}
