using ProductManagement.Repositories.Interfaces;
using ProductManagement.Readers;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Validation.Interfaces;

namespace ProductManagement.Factories;

public static class ProductReaderFactory
{
    public static IProductReader CreateReader(string type, IProductRepository repository, IProductValidator validator)
    {
        return type switch
        {
            "console" => new ConsoleProductReader(repository, validator),
            "csv" => new CsvProductReader(repository, validator),
            _ => throw new ArgumentException($"Unknown reader type: {type}")
        };
    }
}