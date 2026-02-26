using ProductManagement;

var store = new ProductStore(new List<Product>());
var productUI = new ProductUI(store);
while (true)
{

    ProductUI.DisplayMenu();
    string choice = Console.ReadLine()?.Trim() ?? "";

    switch (choice)
    {
        case "1":
            productUI.InteractiveAdd();
            break;

        case "2":
            productUI.DisplayProducts();
            break;

        case "3":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}
