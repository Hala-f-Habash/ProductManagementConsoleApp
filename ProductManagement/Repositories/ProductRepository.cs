using ProductManagement.Models;
using ProductManagement.Repositories.Interfaces;

namespace ProductManagement.Repositories;


public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products;
    private readonly HashSet<string> _productCodes;

    private int _nextId = 1;

    public ProductRepository(List<Product> products, HashSet<string> productCodes)
    {
        _products = products ?? new List<Product>();
        _productCodes = productCodes ?? new HashSet<string>();

        // Initialize nextId based on existing products
        if (_products.Count > 0)
        {
            _nextId = _products.Max(p => p.ProductId) + 1;
        }
    }

    public void Add(Product product)
    {
        product.ProductId = _nextId++;
        _products.Add(product);
        _productCodes.Add(product.ProductCode);
    }

    public List<Product> GetAll() => _products;

    public bool Exists(string productCode)
    {
        if (string.IsNullOrWhiteSpace(productCode))
            return false;

        return _productCodes.Contains(productCode);
    }
}
