using Microsoft.Data.SqlClient;
using PoliRestaurante.Data;
using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Repository;

public class ReservationStatusRepository : IReservationStatusRepository
{
  
   private readonly ApplicationDbContext _db;

   public ReservationStatusRepository(ApplicationDbContext db)
   {
      _db = db;
   }

   public bool CreateReservationStatus(ReservationStatus reservationStatus)
   {
      _db.ReservationStatuses.Add(reservationStatus);

      return _db.SaveChanges() > 0;
   }

   public ReservationStatus? GetReservationStatus(int id)
   {
      return _db.ReservationStatuses.FirstOrDefault(rs => rs.Id == id);
   }

   public bool UpdateReservationStatus(ReservationStatus reservationStatus)
   {
      _db.ReservationStatuses.Update(reservationStatus);

      return _db.SaveChanges() > 0;
   }

   public bool DeleteReservationStatus(ReservationStatus reservationStatus)
   {
      _db.ReservationStatuses.Remove(reservationStatus);

      return _db.SaveChanges() > 0;
   }

    public bool ReservationStatusExists(int id)
    {
        return _db.ReservationStatuses.Any(rs => rs.Id == id);
    }

    public bool ReservationStatusExists(string name)
    {
        return _db.ReservationStatuses.Any(rs => rs.Name == name);
    }
}