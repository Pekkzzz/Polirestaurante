using PoliRestaurante.Models;

public interface IReservationStatusRepository
{
  bool CreateReservationStatus(ReservationStatus reservationStatus);
  ReservationStatus? GetReservationStatus(int id);
  bool ReservationStatusExists(int id);
  bool ReservationStatusExists(string name);
  bool UpdateReservationStatus(ReservationStatus reservationStatus);
  bool DeleteReservationStatus(ReservationStatus reservationStatus);
}