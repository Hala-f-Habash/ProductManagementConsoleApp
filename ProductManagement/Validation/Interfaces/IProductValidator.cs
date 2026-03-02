namespace ProductManagement.Validation.Interfaces;

using ProductManagement.Models;
public interface IProductValidator
{
    abstract bool ValidateProductCode(string? productCode, out string error);
    abstract bool ValidateName(string? name, out string error);
    abstract bool ValidatePrice(decimal price, out string error);
    abstract bool ValidateQuantity(int quantity, out string error);
    abstract bool ValidateDescription(string? description, out string error);
    public abstract bool TryValidateAll(Product product, out List<string> errors);
    public abstract bool TryValidateAllAndPrintErrors(Product product, string leadingMsg);
}
