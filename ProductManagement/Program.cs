using ProductManagement;

IProductStore store = new ProductStore(new List<Product>());
var ui = new ProductUI(store);

IProductInputStrategy manualStrategy = new ManualEntryStrategy(store);
IProductInputStrategy csvStrategy = new CsvImportStrategy(store);

while (true)
{
    ProductUI.DisplayMenu();
    string choice = ConsoleHelpers.ReadOptional("Choose option: ").Trim();

    switch (choice)
    {
        case "1":
            manualStrategy.ImportProducts();
            break;

        case "2":
            csvStrategy.ImportProducts();
            break;

        case "3":
            ui.DisplayProducts();
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
            break;
    }
}