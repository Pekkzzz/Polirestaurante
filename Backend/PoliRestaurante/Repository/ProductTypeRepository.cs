using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class ProductTypeRepository : IProductTypeRepository
{
   private readonly ApplicationDbContext _db;

   public ProductTypeRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(ProductType ProductType)
   {
      _db.ProductTypes.Add(ProductType);

      return _db.SaveChanges() > 0;
   }

   public ProductType? Get(int id)
   {
      return _db.ProductTypes.FirstOrDefault(rs => rs.Id == id);
   }

   public ProductType? Get(string name)
   {
      return _db.ProductTypes.FirstOrDefault(rs => rs.Name == name);
   }

   public List<ProductType> GetProductTypes()
   {
      return _db.ProductTypes.ToList();
   }

   public bool Update(ProductType ProductType)
   {
      _db.ProductTypes.Update(ProductType);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(ProductType ProductType)
   {
      _db.ProductTypes.Remove(ProductType);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.ProductTypes.Any(rs => rs.Id == id);
    }


}