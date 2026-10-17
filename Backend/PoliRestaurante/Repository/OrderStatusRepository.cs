using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class OrderStatusRepository : IOrderStatusRepository
{
   private readonly ApplicationDbContext _db;

   public OrderStatusRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(OrderStatus OrderStatus)
   {
      _db.OrderStatuses.Add(OrderStatus);

      return _db.SaveChanges() > 0;
   }

   public OrderStatus? Get(int id)
   {
      return _db.OrderStatuses.FirstOrDefault(rs => rs.Id == id);
   }

   public OrderStatus? Get(string name)
   {
      return _db.OrderStatuses.FirstOrDefault(rs => rs.Name == name);
   }

   public bool Update(OrderStatus OrderStatus)
   {
      _db.OrderStatuses.Update(OrderStatus);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(OrderStatus OrderStatus)
   {
      _db.OrderStatuses.Remove(OrderStatus);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.OrderStatuses.Any(rs => rs.Id == id);
    }

    public bool Exists(string name)
    {
        return _db.OrderStatuses.Any(rs => rs.Name == name);
    }


}