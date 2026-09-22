using PoliRestaurante.Models;

public static class ReservationsMapper
{
    public static ReservationStatus_Dto ReservationStatusToDto(ReservationStatus reservationStatus)
    {
        return new ReservationStatus_Dto
        {
            Id = reservationStatus.Id,
            Name = reservationStatus.Name,
            Description = reservationStatus.Description
        };
    }

    public static ReservationStatus ReservationStatusToEnity(ReservationStatus_CreateDto reservationStatus)
    {
        return new ReservationStatus
        {
            Name = reservationStatus.Name,
            Description = reservationStatus.Description,
        };
    }

    public static ReservationStatus ReservationStatusToEnity(ReservationStatus_UpdateDto reservationStatus)
    {
        return new ReservationStatus
        {
            Id = reservationStatus.Id,
            Name = reservationStatus.Name,
            Description = reservationStatus.Description
        };
    }
}