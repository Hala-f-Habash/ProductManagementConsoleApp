using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Factories;
using ProductManagement.Services.Interfaces;
using ProductManagement.Readers.Interfaces;

namespace ProductManagement.Services;

public class ProductService : IProductService
{
    private IProductRepository _repository;
    private IProductWriter _writer;
    private IProductReader _reader;

    public ProductService(
        IProductRepository repository,
        IProductWriter writer,
        IProductReader? reader = null)
    {
        _repository = repository;
        _writer = writer;
        _reader = reader ?? ProductReaderFactory.CreateReader("console", repository); // check best practices

    }



    public void DisplayMenu()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("Welcome to the Product Management System!");
        Console.WriteLine("1. Add Product (Manual)");
        Console.WriteLine("2. Add Product (CSV Import)");
        Console.WriteLine("3. Display Products");
        Console.WriteLine("4. Exit");
    }

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            string choice = ConsoleHelpers.ReadOptional("Choose option: ").Trim();

            switch (choice)
            {
                case "1":
                    _reader = ProductReaderFactory.CreateReader("console", _repository);
                    _reader.ImportProducts();
                    break;
                case "2":
                    _reader = ProductReaderFactory.CreateReader("csv", _repository);
                    _reader.ImportProducts();
                    break;
                case "3":
                    _writer.DisplayProducts(_repository.GetAll());
                    break;
                case "4":
                    return;
            }
        }
    }
}