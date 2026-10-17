using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class PaymentStatusRepository : IPaymentStatusRepository
{
   private readonly ApplicationDbContext _db;

   public PaymentStatusRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool Create(PaymentStatus PaymentStatus)
   {
      _db.PaymentStatuses.Add(PaymentStatus);

      return _db.SaveChanges() > 0;
   }

   public PaymentStatus? Get(int id)
   {
      return _db.PaymentStatuses.FirstOrDefault(rs => rs.Id == id);
   }

   public PaymentStatus? Get(string name)
   {
      return _db.PaymentStatuses.FirstOrDefault(rs => rs.Name == name);
   }

   public bool Update(PaymentStatus PaymentStatus)
   {
      _db.PaymentStatuses.Update(PaymentStatus);

      return _db.SaveChanges() > 0;
   }

   public bool Delete(PaymentStatus PaymentStatus)
   {
      _db.PaymentStatuses.Remove(PaymentStatus);

      return _db.SaveChanges() > 0;
   }

    public bool Exists(int id)
    {
        return _db.PaymentStatuses.Any(rs => rs.Id == id);
    }

    public bool Exists(string name)
    {
        return _db.PaymentStatuses.Any(rs => rs.Name == name);
    }


}