using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class OrderDetailRepository : IOrderDetailRepository
{
   private readonly ApplicationDbContext _db;

   public OrderDetailRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(OrderDetail OrderDetail)
   {
      _db.OrderDetails.Add(OrderDetail);

      return _db.SaveChanges() > 0;
   }

   public OrderDetail? Get(int id)
   {
      return _db.OrderDetails.FirstOrDefault(od => od.Id == id);
   }

   public List<OrderDetail> GetByOrderId(int id)
   {
      return _db.OrderDetails.Where(od => od.OrderId == id).ToList();
   }

   public List<OrderDetail> GetOrderDetailes()
   {
      return _db.OrderDetails.ToList();
   }

   public bool Update(OrderDetail OrderDetail)
   {
      _db.OrderDetails.Update(OrderDetail);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(OrderDetail OrderDetail)
   {
      _db.OrderDetails.Remove(OrderDetail);

      return _db.SaveChanges() > 0;
   }

   public bool Exists(int id)
   {
      return _db.OrderDetails.Any(od => od.Id == id);
   }


}