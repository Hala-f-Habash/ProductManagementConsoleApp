namespace ProductManagement.Readers;

using ProductManagement.Models;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Readers.Interfaces;

public class ConsoleProductReader : IProductReader
{
    private readonly IProductRepository _repository;
    private readonly IProductValidator _validator;

    public ConsoleProductReader(IProductRepository repository, IProductValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public void ImportProducts()
    {

        var product = ReadProduct();

        bool isValid = _validator.TryValidateAllAndPrintErrors(product, "Product could not be added. Please fix the following errors:");
        if (!isValid)
        {
            return;
        }
        _repository.Add(product);
        Console.WriteLine("Product added successfully.");
    }

    private Product ReadProduct()
    {
        string code = ConsoleHelpers.ReadRequired("Product Code (required): ");
        string name = ConsoleHelpers.ReadRequired("Name (required): ");
        string description = ConsoleHelpers.ReadOptional("Description (optional): ");
        decimal price = ConsoleHelpers.ReadDecimal("Price (required): ");
        int quantity = ConsoleHelpers.ReadInt("Quantity (required): ");

        var product = new Product(code, name, description, price, quantity);
        return product;
    }

}