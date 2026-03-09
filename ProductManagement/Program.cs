using ProductManagement.Repositories;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Services;
using ProductManagement.Services.Interfaces;
using ProductManagement.Validation;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Writers;
using ProductManagement.Writers.Interfaces;
using ProductManagement.Models;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Factories;
using ProductManagement.Readers;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;


namespace ProductManagement;

class Program
{
    static void Main(string[] args)
    {
        IProductRepository repository = new ProductRepository(new List<Product>());
        IProductValidator validator = new ProductValidator(repository);
        IInputHelper consoleInputHelper = new ConsoleInputHelper();
        IProductService service = new ProductService(repository, validator, consoleInputHelper);
        service.Run();
    }
}