using System;
using ProductManagement;
var store = new ProductStore();

while (true)
{

    welcome();
    string choice = ReadOptional("Choose option: ").Trim();

    switch (choice)
    {
        case "1":
            InteractiveAdd(store);
            break;

        case "2":
            DisplayProducts(store);
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

static void InteractiveAdd(ProductStore store)
{


    string code = ReadRequired("Product Code (required): ");
    if (store.Exists(code))
    {
        Console.WriteLine($"A product with code '{code}' already exists.");
        return;
    }

    string name = ReadRequired("Name (required): ");

    string description = ReadOptional("Description (optional): ");


    decimal price = ReadDecimal("Price (enter decimal input): ");
    int quantity = ReadInt("Quantity (enter a number): ");

    var product = new Product(code, name, description, price, quantity);
    try
    {
        store.Add(product);
        Console.WriteLine("Product added successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to add product: {ex.Message}");
    }
}

static void DisplayProducts(ProductStore store)
{
    var products = store.GetAll();
    if (products.Count == 0)
    {
        Console.WriteLine("No products available.");
        return;
    }
    // Column widths
    const int idWidth = 4;
    const int codeWidth = 12;
    const int nameWidth = 20;
    const int descWidth = 60;
    const int priceWidth = 12;
    const int qtyWidth = 8;

    int totalWidth = idWidth + codeWidth + nameWidth + descWidth + priceWidth + qtyWidth + 5 * 1; // spaces between columns

    Console.WriteLine();
    Console.WriteLine("--- Product List ---");

    // Header
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

        // Truncate for display to fit the column, add ellipsis when truncated
        string descDisplay = desc.Length > descWidth ? string.Concat(desc.AsSpan(0, descWidth - 3), "...") : desc;

        string line =
            product.ProductId.ToString().PadRight(idWidth) + " " +
            (product.ProductCode ?? string.Empty).PadRight(codeWidth) + " " +
            (product.Name ?? string.Empty).PadRight(nameWidth) + " " +
            descDisplay.PadRight(descWidth) + " " +
            product.Price.ToString("C").PadLeft(priceWidth) + " " +
            product.Quantity.ToString().PadLeft(qtyWidth);

        Console.WriteLine(line);
    }
}

static string ReadRequired(string prompt)
{
    string? input;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input));

    // At this point, input is guaranteed non-null and non-whitespace
    return input!;
}

static string ReadOptional(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine() ?? "";
}

static decimal ReadDecimal(string prompt)
{
    decimal value;
    while (true)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out value))
            return value;
        Console.WriteLine("Invalid decimal. Try again.");
    }
}

static int ReadInt(string prompt)
{
    int value;
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out value))
            return value;
        Console.WriteLine("Invalid integer. Try again.");
    }
}

static void welcome()
{
    Console.WriteLine("Welcome to the Product Management System!");
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. Display Products");
    Console.WriteLine("3. Exit");
}