using System.Text.Json;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.DTOs;
using ProductManagement.Serialization;
using ProductManagement.Validation;
using ProductManagement.Helpers.Interfaces;
using ProductManagement.Helpers;

namespace ProductManagement.Writers;

public class JsonProductWriter : IProductWriter
{
    private readonly IProductRepository _repository;
    private readonly IInputHelper _consoleInputHelper;

    public JsonProductWriter(IProductRepository repository, IInputHelper consoleInputHelper)
    {
        _repository = repository;
        _consoleInputHelper = consoleInputHelper;
    }

    public void WriteProducts()
    {
        var products = _repository.GetAll();

        if (products.Count == 0)
        {
            Console.WriteLine("No products available to write.");
            return;
        }

        var dtos = products
            .Select(ProductJsonDto.FromProduct)
            .ToList();

        string filePath = _consoleInputHelper.ReadRequired("Enter path to JSON output file: ");


        try
        {
            string json = JsonSerializer.Serialize(dtos, JsonSerializerConfig.Options);

            File.WriteAllText(filePath, json);

            Console.WriteLine($"Products successfully written to '{filePath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write JSON file: {ex.Message}");
        }

    }
}