using ProductManagement.Models;
using ProductManagement.Repositories.Interfaces;
using ProductManagement.Validation.Interfaces;

namespace ProductManagement.Validation;

public class ProductValidator : IProductValidator
{
    private IProductRepository _store;

    public ProductValidator(IProductRepository store)
    {
        _store = store;
    }

    // --- Individual validation methods ---

    public bool ValidateProductCode(string? productCode, out string error)
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(productCode))
        {
            error = $"Product Code is required.";
            return false;
        }

        if (_store.Exists(productCode))
        {
            error = $"Product code '{productCode}' already exists.";
            return false;
        }

        return true;
    }

    public bool ValidateName(string? name, out string error)
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            error = $"Name is required.";
            return false;
        }

        return true;
    }

    public bool ValidatePrice(decimal price, out string error)
    {
        error = string.Empty;

        if (price <= 0)
        {
            error = "Price must be greater than 0.";
            return false;
        }

        return true;
    }

    public bool ValidateQuantity(int quantity, out string error)
    {
        error = string.Empty;

        if (quantity < 0)
        {
            error = "Quantity must be zero or positive.";
            return false;
        }

        return true;
    }

    public bool ValidateDescription(string? description, out string error)
    {
        error = string.Empty;

        if (description?.Length > 500)
        {
            error = "Description cannot exceed 500 characters.";
            return false;
        }

        return true;
    }

    // --- Aggregate method that runs all validations ---

    public bool TryValidateAll(Product product, out List<string> errors)
    {
        errors = new List<string>();

        if (!ValidateProductCode(product.ProductCode, out var codeError))
            errors.Add(codeError);

        if (!ValidateName(product.Name, out var nameError))
            errors.Add(nameError);

        if (!ValidatePrice(product.Price, out var priceError))
            errors.Add(priceError);

        if (!ValidateQuantity(product.Quantity, out var qtyError))
            errors.Add(qtyError);

        if (!ValidateDescription(product.Description, out var descError))
            errors.Add(descError);

        return errors.Count == 0;
    }
}