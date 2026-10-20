using PoliRestaurante.Models;

public interface IReservationStatusService
{
    public ReservationStatusService_Response GetByID(int id);
    public ReservationStatusService_Response Create(ReservationStatus_CreateDto newReservationStatus);
    public ReservationStatusService_Response Update(ReservationStatus_UpdateDto reservationStatus_UpdateDto);
    public ReservationStatusService_Response Detele(int id);
}