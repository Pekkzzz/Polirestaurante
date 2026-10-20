using PoliRestaurante.Models.Entity;

public interface IReservationRepository
{
    bool CreateReservation(Reservation reservation);

    Reservation? GetReservation(int id);

    List<Reservation> GetReservations();

    bool UpdateReservation(Reservation reservation);

    bool DeleteReservation(Reservation reservation);

    bool ReservationExists(int id);
}
