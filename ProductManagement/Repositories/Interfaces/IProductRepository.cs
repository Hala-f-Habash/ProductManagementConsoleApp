using ProductManagement.Models;

namespace ProductManagement.Repositories.Interfaces;

public interface IProductRepository
{
    void Add(Product product);
    List<Product> GetAll();
    bool Exists(string productCode);
}