namespace ProductManagement.Readers;

using ProductManagement.Models;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;

public class ConsoleProductReader : IProductReader
{
    private readonly IProductRepository _repository;
    private readonly IProductValidator _validator;
    private readonly IInputHelper _consoleInputHelper;

    public ConsoleProductReader(IProductRepository repository, IProductValidator validator, IInputHelper consoleInputHelper)
    {
        _repository = repository;
        _validator = validator;
        _consoleInputHelper = consoleInputHelper;
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
        string code = _consoleInputHelper.ReadRequired("Product Code (required): ");
        string name = _consoleInputHelper.ReadRequired("Name (required): ");
        string description = _consoleInputHelper.ReadOptional("Description (optional): ");
        decimal price = _consoleInputHelper.ReadDecimal("Price (required): ");
        int quantity = _consoleInputHelper.ReadInt("Quantity (required): ");

        var product = new Product(code, name, description, price, quantity);
        return product;
    }

}