using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.Writers;

namespace ProductManagement.Factories;


public static class ProductWriterFactory
{
    public static IProductWriter CreateWriter(string type, IProductRepository repository)
    {
        return type switch
        {
            "console" => new ConsoleProductWriter(repository),
            // "json" => new JsonProductWriter(repository),
            _ => throw new ArgumentException($"Unknown writer type: {type}")
        };
    }
}