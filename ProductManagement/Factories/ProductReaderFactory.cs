using ProductManagement.Repositories.Interfaces;
using ProductManagement.Readers;
using ProductManagement.Readers.Interfaces;

namespace ProductManagement.Factories;

public static class ProductReaderFactory
{
    public static IProductReader CreateReader(string type, IProductRepository repository)
    {
        return type switch
        {
            "console" => new ConsoleProductReader(repository),
            "csv" => new CsvProductReader(repository),
            _ => throw new ArgumentException($"Unknown reader type: {type}")
        };
    }
}