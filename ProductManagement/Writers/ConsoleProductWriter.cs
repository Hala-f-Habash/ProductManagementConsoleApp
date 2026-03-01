using ProductManagement.Repositories.Interfaces;

namespace ProductManagement.Writers;

public class ConsoleProductWriter
{
    private readonly IProductRepository _store;

    public ConsoleProductWriter(IProductRepository store)
    {
        _store = store;
    }

    public void DisplayProducts()
    {
        var products = _store.GetAll();
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        const int idWidth = 4;
        const int codeWidth = 12;
        const int nameWidth = 20;
        const int descWidth = 60;
        const int priceWidth = 12;
        const int qtyWidth = 8;

        int totalWidth = idWidth + codeWidth + nameWidth + descWidth + priceWidth + qtyWidth + 5;

        Console.WriteLine();
        Console.WriteLine("--- Product List ---");

        Console.WriteLine(
            "ID".PadRight(idWidth) + " " +
            "Code".PadRight(codeWidth) + " " +
            "Name".PadRight(nameWidth) + " " +
            "Description".PadRight(descWidth) + " " +
            "Price".PadLeft(priceWidth) + " " +
            "Quantity".PadLeft(qtyWidth)
        );

        Console.WriteLine(new string('-', totalWidth));

        foreach (var product in products)
        {
            string desc = product.Description ?? string.Empty;
            string descDisplay = desc.Length > descWidth
                ? string.Concat(desc.AsSpan(0, descWidth - 3), "...")
                : desc;

            Console.WriteLine(
                product.ProductId.ToString().PadRight(idWidth) + " " +
                (product.ProductCode ?? string.Empty).PadRight(codeWidth) + " " +
                (product.Name ?? string.Empty).PadRight(nameWidth) + " " +
                descDisplay.PadRight(descWidth) + " " +
                product.Price.ToString("C").PadLeft(priceWidth) + " " +
                product.Quantity.ToString().PadLeft(qtyWidth)
            );
        }
    }

}