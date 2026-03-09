namespace ProductManagement.Validation.Interfaces;

using ProductManagement.Models;
public interface IProductValidator
{
    public abstract bool TryValidateAllAndPrintErrors(Product product, string leadingMsg);
}
