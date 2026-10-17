using PoliRestaurante.Models.Entity;

public interface IProductTypeRepository
{
  bool Create(ProductType ProductType);
  ProductType? Get(int id);
  List<ProductType> GetProductTypes();
  bool Exists(int id);
  bool Update(ProductType ProductType);
  bool Delete(ProductType ProductType);
}