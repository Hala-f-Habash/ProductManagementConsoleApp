using ProductManagement.Models;

namespace ProductManagement.Writers.Interfaces;

public interface IProductWriter
{
    void DisplayProducts(List<Product> products);

}