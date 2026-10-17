using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class ProductRepository : IProductRepository
{
   private readonly ApplicationDbContext _db;

   public ProductRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(Product Product)
   {
      _db.Products.Add(Product);

      return _db.SaveChanges() > 0;
   }

   public Product? Get(int id)
   {
      return _db.Products.FirstOrDefault(rs => rs.Id == id);
   }

   public Product? Get(string name)
   {
      return _db.Products.FirstOrDefault(rs => rs.Name == name);
   }

   public List<Product> GetProducts()
   {
      return _db.Products.ToList();
   }

   public bool Update(Product Product)
   {
      _db.Products.Update(Product);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(Product Product)
   {
      _db.Products.Remove(Product);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.Products.Any(rs => rs.Id == id);
    }


}