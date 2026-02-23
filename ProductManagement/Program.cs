using System;
using ProductManagement;
var productUI = new ProductUI(new ProductStore());
while (true)
{

    ProductUI.DisplayMenu();
    string choice = ProductUI.ReadOptional("Choose option: ").Trim();

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
