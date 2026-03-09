using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.Writers;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;

namespace ProductManagement.Factories;


public static class ProductWriterFactory
{
    public static IProductWriter CreateWriter(string type, IProductRepository repository, IInputHelper? consoleInputHelper =null)
    {
        return type switch
        {
            "console" => new ConsoleProductWriter(repository),
            "json" => new JsonProductWriter(repository, consoleInputHelper?? new ConsoleInputHelper()),
            _ => throw new ArgumentException($"Unknown writer type: {type}")
        };
    }
}