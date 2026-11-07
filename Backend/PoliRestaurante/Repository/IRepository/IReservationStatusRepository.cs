using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public interface IReservationStatusRepository
{
  bool CreateReservationStatus(ReservationStatus reservationStatus);
  ReservationStatus? GetReservationStatus(int id);
  List<ReservationStatus>? GetAll();
  bool ReservationStatusExists(int id);
  bool ReservationStatusExists(string name);
  bool UpdateReservationStatus(ReservationStatus reservationStatus);
  bool DeleteReservationStatus(ReservationStatus reservationStatus);
}