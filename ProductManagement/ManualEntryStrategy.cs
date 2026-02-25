namespace ProductManagement;

public class ManualEntryStrategy : IProductInputStrategy
{
    private readonly IProductStore _store;
    private readonly ProductValidator _validator;

    public ManualEntryStrategy(IProductStore store)
    {
        _store = store;
        _validator = new ProductValidator(store);
    }

    public void ImportProducts()
    {

        var product = ReadProduct();

        if (!ValidateProduct(product))
        {
            Console.WriteLine("Faild to add product.");
            return;
        }
        _store.Add(product);
        Console.WriteLine("Product added successfully.");
    }

    private Product ReadProduct()
    {
        string code = ConsoleHelpers.ReadRequired("Product Code (required): ");
        string name = ConsoleHelpers.ReadRequired("Name (required): ");
        string description = ConsoleHelpers.ReadOptional("Description (optional): ");
        decimal price = ConsoleHelpers.ReadDecimal("Price (required): ");
        int quantity = ConsoleHelpers.ReadInt("Quantity (required): ");

        var product = new Product(code, name, description, price, quantity);
        return product;
    }

    private bool ValidateProduct(Product product)
    {
        var errors = _validator.ValidateAll(product);

        if (errors.Count > 0)
        {
            Console.WriteLine("Product could not be added. Please fix the following:");
            foreach (var error in errors)
                Console.WriteLine($"  - {error}");
            return false;
        }
        return true;
    }
}