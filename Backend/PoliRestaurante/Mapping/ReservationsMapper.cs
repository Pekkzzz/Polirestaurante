using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

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

        
    public static Reservation ReservationToEntity(
        Reservation_CreateDto reservation)
    {
        return new Reservation
        {
            Date = reservation.Date,
            People = reservation.People,
            Reason = reservation.Reason,
            ReservationStatusId = reservation.ReservationStatusId,
            UserId = reservation.UserId,
            TableId = reservation.TableId
        };
    }

    public static Reservation ReservationToEntity(
        Reservation_UpdateDto reservation)
    {
        return new Reservation
        {
            Id = reservation.Id,
            Date = reservation.Date,
            People = reservation.People,
            Reason = reservation.Reason,
            ReservationStatusId = reservation.ReservationStatusId,
            UserId = reservation.UserId,
            TableId = reservation.TableId
        };
    }

    public static Reservation_Dto ReservationToDto(
        Reservation reservation)
    {
        return new Reservation_Dto
        {
            Id = reservation.Id,
            Date = reservation.Date,
            People = reservation.People,
            Reason = reservation.Reason,
            ReservationStatusId = reservation.ReservationStatusId,
            UserId = reservation.UserId,
            TableId = reservation.TableId
        };
    }

}