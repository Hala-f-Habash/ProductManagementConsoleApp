using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Factories;
using ProductManagement.Services.Interfaces;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;

namespace ProductManagement.Services;

public class ProductService : IProductService
{
    private IProductRepository _repository;
    private IProductValidator _validator;
    private IInputHelper _consoleInputHelper;
    public ProductService(
        IProductRepository repository, IProductValidator validator, IInputHelper consoleInputHelper)
    {
        _repository = repository;
        _validator = validator;
        _consoleInputHelper = consoleInputHelper;
    }



    public void DisplayMenu()
    {
        Console.WriteLine("--------------------");
        Console.WriteLine("Welcome to the Product Management System!");
        Console.WriteLine("1. Add Product (Manual)");
        Console.WriteLine("2. Add Product (CSV Import)");
        Console.WriteLine("3. Display Products on Console");
        Console.WriteLine("4. Write products to JSON file");
        Console.WriteLine("5. Exit");
        Console.WriteLine("Choose option:");
        Console.WriteLine("--------------------");
    }

    public void Run()
    {
        IProductWriter? writer;
        IProductReader? reader;
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    reader = ProductReaderFactory.CreateReader("console", _repository, _validator, _consoleInputHelper);
                    reader.ImportProducts();
                    break;
                case "2":
                    reader = ProductReaderFactory.CreateReader("csv", _repository, _validator, _consoleInputHelper);
                    reader.ImportProducts();
                    break;
                case "3":
                    writer = ProductWriterFactory.CreateWriter("console", _repository);
                    writer.WriteProducts();
                    break;
                case "4":
                    writer = ProductWriterFactory.CreateWriter("json", _repository, _consoleInputHelper);
                    writer.WriteProducts();
                    break;
                case "5":
                    return;
            }
        }
    }
}