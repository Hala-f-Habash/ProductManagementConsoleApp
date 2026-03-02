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


namespace ProductManagement;

class Program
{
    static void Main(string[] args)
    {
        IProductRepository repository = new ProductRepository(new List<Product>());
        IProductService service = new ProductService(repository);
        service.Run();
    }
}