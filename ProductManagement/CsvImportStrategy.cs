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
        // 1. Read file
        string[]? lines = TryReadFileLines(out string? fileError);

        if (lines is null)
        {
            Console.WriteLine(fileError);
            return;
        }

        // 2. Check if file has data
        if (lines.Length <= 1)
        {
            Console.WriteLine("The file is empty or contains only a header row. Nothing was imported.");
            return;
        }

        // 3. Process rows
        int imported = 0, failed = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            int displayRow = i + 1; // 1-based row number for user messages
            var result = TryProcessRow(lines[i].Trim(), displayRow);

            switch (result.Status)
            {
                case RowProcessStatus.Success:
                    _store.Add(result.Product!);
                    imported++;
                    break;
                case RowProcessStatus.BlankLine:
                    // Skip silently, don't count as failed
                    break;
                case RowProcessStatus.Error:
                    Console.Write(result.ErrorMessage);
                    failed++;
                    break;
            }
        }

        // 4. Print summary
        PrintSummary(imported, failed);
    }
    // ------------------------------------------------------------
    // Private helpers
    // ------------------------------------------------------------

    private string[]? TryReadFileLines(out string? errorMessage)
    {
        string filePath = ConsoleHelpers.ReadRequired("Enter the path to the CSV file: ").Trim();

        if (!File.Exists(filePath))
        {
            errorMessage = $"File not found: {filePath}";
            return null;
        }

        try
        {
            errorMessage = null;
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            errorMessage = $"Could not read file: {ex.Message}";
            return null;
        }
    }

    private RowProcessResult TryProcessRow(string line, int displayRow)
    {
        // Check for blank line (including lines with just commas)
        if (string.IsNullOrWhiteSpace(line) || IsLineWithOnlyCommas(line))
        {
            return new RowProcessResult(RowProcessStatus.BlankLine);
        }

        var parseResult = ParseRow(line);

        // Handle parsing errors
        if (parseResult.HasErrors)
        {
            string errorMsg = $"  Row {displayRow}: {string.Join(" ", parseResult.Errors!)}\n";
            return new RowProcessResult(RowProcessStatus.Error, ErrorMessage: errorMsg);
        }

        // Handle validation errors
        var errors = _validator.ValidateAll(parseResult.Product!);
        if (errors.Count > 0)
        {
            string errorMsg = $"  Row {displayRow} skipped — validation failed:\n";
            foreach (var error in errors)
                errorMsg += $"    - {error}\n";
            return new RowProcessResult(RowProcessStatus.Error, ErrorMessage: errorMsg);
        }
        // Success
        return new RowProcessResult(RowProcessStatus.Success, Product: parseResult.Product);
    }

    private static ParseResult ParseRow(string line)
    {
        string[] parts = line.Split(',', 5);

        if (parts.Length < 5)
            return new ParseResult(null, new[] { $"Expected 5 columns but found {parts.Length}." });

        string code = parts[0].Trim();
        string name = parts[1].Trim();
        string description = parts[2].Trim();
        string rawPrice = parts[3].Trim();
        string rawQty = parts[4].Trim();

        var errors = new List<string>();
        bool isValidPrice = decimal.TryParse(rawPrice, out decimal price);
        if (!isValidPrice)
            errors.Add($"'{rawPrice}' is not a valid price.");

        bool isValidQuantity = int.TryParse(rawQty, out int quantity);
        if (!isValidQuantity)
            errors.Add($"'{rawQty}' is not a valid quantity.");

        if (errors.Count > 0)
            return new ParseResult(null, errors.ToArray());

        return new ParseResult(new Product(code, name, description, price, quantity), null);
    }

    private bool IsLineWithOnlyCommas(string line) =>
        !string.IsNullOrEmpty(line) && line.Replace(",", "").Trim().Length == 0;

    private void PrintSummary(int imported, int failed)
    {
        Console.WriteLine();
        Console.WriteLine($"Import complete. {imported} product(s) imported, {failed} row(s) skipped.");
    }

    // Record definitions
    private record ParseResult(Product? Product, string[]? Errors)
    {
        public bool HasErrors => Errors != null && Errors.Length > 0;
    }

    private record RowProcessResult(RowProcessStatus Status, Product? Product = null, string? ErrorMessage = null);

    private enum RowProcessStatus { Success, BlankLine, Error }
}