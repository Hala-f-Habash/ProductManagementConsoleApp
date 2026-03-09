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
    private readonly IInputHelper _inputValidator;

    public ConsoleProductReader(IProductRepository repository, IProductValidator validator, IInputHelper inputValidator)
    {
        _repository = repository;
        _validator = validator;
        _inputValidator = inputValidator;
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
        string code = _inputValidator.ReadRequired("Product Code (required): ");
        string name = _inputValidator.ReadRequired("Name (required): ");
        string description = _inputValidator.ReadOptional("Description (optional): ");
        decimal price = _inputValidator.ReadDecimal("Price (required): ");
        int quantity = _inputValidator.ReadInt("Quantity (required): ");

        var product = new Product(code, name, description, price, quantity);
        return product;
    }

}