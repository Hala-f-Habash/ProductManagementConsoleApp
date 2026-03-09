using ProductManagement.Repositories.Interfaces;
using ProductManagement.Readers;
using ProductManagement.Readers.Interfaces;
using ProductManagement.Validation.Interfaces;
using ProductManagement.Validation;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;

namespace ProductManagement.Factories;

public static class ProductReaderFactory
{
    public static IProductReader CreateReader(string type, IProductRepository repository, IProductValidator validator, IInputHelper? consoleInputHelper = null)
    {
        return type switch
        {
            "console" => new ConsoleProductReader(repository, validator, consoleInputHelper ?? new ConsoleInputHelper()),
            "csv" => new CsvProductReader(repository, validator),
            _ => throw new ArgumentException($"Unknown reader type: {type}")
        };
    }
}