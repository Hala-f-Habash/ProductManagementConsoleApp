using ProductManagement.Repositories.Interfaces;
using ProductManagement.Readers;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Validation;

namespace ProductManagement.Factories;

public static class ProductReaderFactory
{
    public static IProductReader CreateReader(string type, IProductRepository repository, IProductValidator validator)
    {
        return type switch
        {
            "console" => new ConsoleProductReader(repository, validator, new ConsoleInputValidator()),
            "csv" => new CsvProductReader(repository, validator),
            _ => throw new ArgumentException($"Unknown reader type: {type}")
        };
    }
}