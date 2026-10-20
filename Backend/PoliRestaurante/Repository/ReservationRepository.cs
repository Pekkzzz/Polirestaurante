
using Microsoft.EntityFrameworkCore;
using PoliRestaurante.Data;
using PoliRestaurante.Models.Entity;
using Polirestaurante.Repository;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _db;

    public ReservationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateReservation(Reservation reservation)
    {
        _db.Reservations.Add(reservation);

        return _db.SaveChanges() > 0;
    }

    public Reservation? GetReservation(int id)
    {
        return _db.Reservations
            .FirstOrDefault(r => r.Id == id);
    }

    public List<Reservation> GetReservations()
    {
        return _db.Reservations.ToList();
    }

    public bool UpdateReservation(Reservation reservation)
    {
        _db.Reservations.Update(reservation);

        return _db.SaveChanges() > 0;
    }

    public bool DeleteReservation(Reservation reservation)
    {
        _db.Reservations.Remove(reservation);

        return _db.SaveChanges() > 0;
    }

    public bool ReservationExists(int id)
    {
        return _db.Reservations.Any(r => r.Id == id);
    }
}
