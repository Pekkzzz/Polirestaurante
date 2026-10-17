using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class OrderDetailStatusRepository : IOrderDetailStatusRepository
{
   private readonly ApplicationDbContext _db;

   public OrderDetailStatusRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(OrderDetailStatus OrderDetailStatus)
   {
      _db.OrderDetailStatuses.Add(OrderDetailStatus);

      return _db.SaveChanges() > 0;
   }

   public OrderDetailStatus? Get(int id)
   {
      return _db.OrderDetailStatuses.FirstOrDefault(rs => rs.Id == id);
   }

   public List<OrderDetailStatus> GetOrderDetailStatuses()
   {
      return _db.OrderDetailStatuses.ToList();
   }

   public bool Update(OrderDetailStatus OrderDetailStatus)
   {
      _db.OrderDetailStatuses.Update(OrderDetailStatus);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(OrderDetailStatus OrderDetailStatus)
   {
      _db.OrderDetailStatuses.Remove(OrderDetailStatus);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.OrderDetailStatuses.Any(rs => rs.Id == id);
    }


}