namespace ProductManagement;

public interface IProductStore
{
    void Add(Product product);
    IReadOnlyList<Product> GetAll();
    bool Exists(string productCode);
}

public class ProductStore : IProductStore
{
    private readonly List<Product> _products = new();

    // here there is a duplicate checking of null||whitespace and duplicate product code. 

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (string.IsNullOrWhiteSpace(product.ProductCode))
            throw new ArgumentException("Product code is required.", nameof(product));

        if (Exists(product.ProductCode))
            throw new InvalidOperationException($"A product with code '{product.ProductCode}' already exists.");

        _products.Add(product);
    }

    public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();

    public bool Exists(string productCode)
    {
        if (string.IsNullOrWhiteSpace(productCode))
            return false;

        return _products.Any(p => string.Equals(p.ProductCode, productCode, StringComparison.OrdinalIgnoreCase));
    }
}
