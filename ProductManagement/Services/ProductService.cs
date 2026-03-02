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
    private IProductValidator _validator;

    public ProductService(
        IProductRepository repository, IProductValidator validator)
    {
        _repository = repository;
        _validator = validator;
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
    }

    public void Run()
    {
        IProductWriter? writer;
        IProductReader? reader;
        while (true)
        {
            DisplayMenu();
            string choice = ConsoleHelpers.ReadOptional("Choose option: ").Trim();

            switch (choice)
            {
                case "1":
                    reader = ProductReaderFactory.CreateReader("console", _repository, _validator);
                    reader.ImportProducts();
                    break;
                case "2":
                    reader = ProductReaderFactory.CreateReader("csv", _repository, _validator);
                    reader.ImportProducts();
                    break;
                case "3":
                    writer = ProductWriterFactory.CreateWriter("console", _repository);
                    writer.WriteProducts();
                    break;
                case "4":
                    writer = ProductWriterFactory.CreateWriter("json", _repository);
                    writer.WriteProducts();
                    break;
                case "5":
                    return;
            }
        }
    }
}