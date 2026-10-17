using PoliRestaurante.Models.Entity;

public interface IProductRepository
{
  bool Create(Product Product);
  Product? Get(int id);
  Product? Get(string name);
  List<Product> GetProducts();
  bool Exists(int id);
  bool Update(Product Product);
  bool Delete(Product Product);
}