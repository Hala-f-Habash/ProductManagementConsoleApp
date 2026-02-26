namespace ProductManagement;

public class Product
{
    // private static int _idCounter = 1;

    public int ProductId { get; internal set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string productCode, string name, string description, decimal price, int quantity)
    {
        // ProductId = _idCounter++;
        ProductCode = productCode;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"ID: {ProductId}, Code: {ProductCode}, Name: {Name}, " +
               $"Description: {Description}, Price: {Price:C}, Quantity: {Quantity}";
    }
}