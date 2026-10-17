using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class OrderTypeRepository : IOrderTypeRepository
{
   private readonly ApplicationDbContext _db;

   public OrderTypeRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(OrderType OrderType)
   {
      _db.OrderTypes.Add(OrderType);

      return _db.SaveChanges() > 0;
   }

   public OrderType? Get(int id)
   {
      return _db.OrderTypes.FirstOrDefault(rs => rs.Id == id);
   }

   public OrderType? Get(string name)
   {
      return _db.OrderTypes.FirstOrDefault(rs => rs.Name == name);
   }

   public bool Update(OrderType OrderType)
   {
      _db.OrderTypes.Update(OrderType);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(OrderType OrderType)
   {
      _db.OrderTypes.Remove(OrderType);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.OrderTypes.Any(rs => rs.Id == id);
    }

    public bool Exists(string name)
    {
        return _db.OrderTypes.Any(rs => rs.Name == name);
    }


}