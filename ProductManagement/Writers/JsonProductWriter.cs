// using System.Text.Json;
// using ProductManagement.Repositories.Interfaces;
// using ProductManagement.Writers.Interfaces;

// namespace ProductManagement.Writers;

// public class JsonProductWriter : IProductWriter
// {
//     private readonly IProductRepository _repository;
//     private static JsonSerializerOptions? _jsonOptions;

//     public JsonProductWriter(IProductRepository repository)
//     {
//         _repository = repository;
//         if (_jsonOptions == null)
//         {
//             _jsonOptions = new JsonSerializerOptions
//             {
//                 WriteIndented = true
//             };
//         }
//     }

//     /// <summary>
//     /// Prompts the user for a destination file path, then serializes the
//     /// current list of products to JSON.  If there are no products the
//     /// method simply informs the user and returns.
//     /// </summary>
//     public void WriteProducts()
//     {
//         var products = _repository.GetAll();
//         if (products == null || products.Count == 0)
//         {
//             Console.WriteLine("No products available to write.");
//             return;
//         }

//         string filePath = ConsoleHelpers.ReadRequired("Enter path to JSON output file: ").Trim();

//         try
//         {
//             var options = new JsonSerializerOptions
//             {
//                 WriteIndented = true
//             };

//             string json = JsonSerializer.Serialize(products, options);
//             File.WriteAllText(filePath, json);
//             Console.WriteLine($"Successfully wrote {products.Count} product(s) to '{filePath}'");
//         }
//         catch (Exception ex)
//         {
//             // catch broad exception to prevent application crash; display message to user.
//             Console.WriteLine($"Failed to write JSON file: {ex.Message}");
//         }
//     }
// }

using System.Text.Json;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Writers.Interfaces;
using ProductManagement.DTOs;
using ProductManagement.Serialization;

namespace ProductManagement.Writers;

public class JsonProductWriter : IProductWriter
{
    private readonly IProductRepository _repository;

    public JsonProductWriter(IProductRepository repository)
    {
        _repository = repository;
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

        string filePath = ConsoleHelpers.ReadRequired("Enter path to JSON output file: ").Trim();


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