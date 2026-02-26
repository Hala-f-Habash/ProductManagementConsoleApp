namespace ProductManagement;

public interface IProductStore
{
    void Add(Product product);
    List<Product> GetAll();
    bool Exists(string productCode);
}

public class ProductStore : IProductStore
{
    private readonly List<Product> _products;

    private int _nextId = 1;

    public ProductStore(List<Product> products)
    {
        _products = products ?? new List<Product>();
        // Initialize nextId based on existing products
        if (_products.Count > 0)
        {
            _nextId = _products.Max(p => p.ProductId) + 1;
        }
    }

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        product.ProductId = _nextId++;
        _products.Add(product);
    }

    public List<Product> GetAll() => _products;

    public bool Exists(string productCode)
    {
        if (string.IsNullOrWhiteSpace(productCode))
            return false;

        return _products.Any(p => string.Equals(p.ProductCode, productCode, StringComparison.OrdinalIgnoreCase));
    }
}
