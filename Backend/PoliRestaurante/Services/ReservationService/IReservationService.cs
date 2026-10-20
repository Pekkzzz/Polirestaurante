
using PoliRestaurante.Models;

public interface IReservationService
{
    ReservationService_Response Create(Reservation_CreateDto reservation);

    ReservationService_Response GetByID(int id);

    ReservationService_Response GetAll();

    ReservationService_Response Update(Reservation_UpdateDto reservation);

    ReservationService_Response Delete(int id);
}
