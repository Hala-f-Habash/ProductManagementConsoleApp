namespace ProductManagement;

/// summary
/// Imports products from a CSV file.
///
/// Expected CSV format (with header row):
///   ProductCode,Name,Description,Price,Quantity
///
/// Rules:
///   - The header row is always skipped.
///   - Each row is validated with the same ProductValidator used for manual entry.
///   - Invalid rows are reported with the row number and reason, but do NOT stop the import.
///   - The Description column is optional and may be empty.
/// 
public class CsvImportStrategy : IProductInputStrategy
{
    private readonly IProductStore _store;
    private readonly ProductValidator _validator;

    public CsvImportStrategy(IProductStore store)
    {
        _store = store;
        _validator = new ProductValidator(store);
    }

    public void ImportProducts()
    {

        // Prompt for file path
        string filePath = ConsoleHelpers.ReadRequired("Enter the path to the CSV file: ").Trim();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Read all lines from the file
        string[] lines;
        try
        {
            lines = File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not read file: {ex.Message}");
            return;
        }

        if (lines.Length <= 1)
        {
            Console.WriteLine("The file is empty or contains only a header row. Nothing was imported.");
            return;
        }

        int imported = 0;
        int failed = 0;

        // Start at index 1 to skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Skip completely blank lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            int displayRow = i + 1; // 1-based, including header, friendlier for the user

            var result = ParseRow(line);
            if (result.Product is null)
            {
                Console.WriteLine($"  Row {displayRow}: {result.Error}");
                failed++;
                continue;
            }

            var errors = _validator.ValidateAll(result.Product);
            if (errors.Count > 0)
            {
                Console.WriteLine($"  Row {displayRow} skipped — validation failed:");
                foreach (var error in errors)
                    Console.WriteLine($"    - {error}");
                failed++;
                continue;
            }

            _store.Add(result.Product);
            imported++;
        }

        Console.WriteLine();
        Console.WriteLine($"Import complete. {imported} product(s) imported, {failed} row(s) skipped.");
    }

    // ------------------------------------------------------------
    // Private helpers
    // ------------------------------------------------------------

    private static ParseResult ParseRow(string line)
    {
        // Split on comma but respect that Description may contain commas(not handled)
        // by limiting to 5 parts (last part is the remainder).
        string[] parts = line.Split(',', 5);

        if (parts.Length < 5)
            return new ParseResult(null, $"Expected 5 columns (ProductCode,Name,Description,Price,Quantity) but found {parts.Length}.");

        string code = parts[0].Trim();
        string name = parts[1].Trim();
        string description = parts[2].Trim();
        string rawPrice = parts[3].Trim();
        string rawQty = parts[4].Trim();

        if (!decimal.TryParse(rawPrice, out decimal price))
            return new ParseResult(null, $"'{rawPrice}' is not a valid price.");

        if (!int.TryParse(rawQty, out int quantity))
            return new ParseResult(null, $"'{rawQty}' is not a valid quantity.");

        return new ParseResult(new Product(code, name, description, price, quantity), null);
    }
    private record ParseResult(Product? Product, string? Error);
}