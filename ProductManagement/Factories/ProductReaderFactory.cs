using ProductManagement.Repositories.Interfaces;
using ProductManagement.Readers;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Validation;

namespace ProductManagement.Factories;

public static class ProductReaderFactory
{
    public static IProductReader CreateReader(string type, IProductRepository repository, IProductValidator validator, IInputValidator? inputValidator = null)
    {
        return type switch
        {
            "console" => new ConsoleProductReader(repository, validator, inputValidator ?? new ConsoleInputValidator()),
            "csv" => new CsvProductReader(repository, validator),
            _ => throw new ArgumentException($"Unknown reader type: {type}")
        };
    }
}