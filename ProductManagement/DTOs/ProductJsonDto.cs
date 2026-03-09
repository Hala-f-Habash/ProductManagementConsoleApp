using ProductManagement.Models;

namespace ProductManagement.DTOs;

public sealed class ProductJsonDto
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }

    public static ProductJsonDto FromProduct(Product product)
    {
        return new ProductJsonDto
        {
            Id = product.ProductId,
            Code = product.ProductCode,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
}