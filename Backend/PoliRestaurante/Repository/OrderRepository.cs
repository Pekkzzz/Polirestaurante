using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class OrderRepository : IOrderRepository
{
   private readonly ApplicationDbContext _db;

   public OrderRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(Order Order)
   {
      _db.Orders.Add(Order);

      return _db.SaveChanges() > 0;
   }

   public Order? Get(int id)
   {
      return _db.Orders.FirstOrDefault(rs => rs.Id == id);
   }

   public bool Update(Order Order)
   {
      _db.Orders.Update(Order);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(Order Order)
   {
      _db.Orders.Remove(Order);

      return _db.SaveChanges() > 0;
   }

   public bool Exists(int id)
   {
      return _db.Orders.Any(rs => rs.Id == id);
   }


}