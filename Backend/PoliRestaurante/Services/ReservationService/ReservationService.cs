
using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public ReservationService_Response Create(
        Reservation_CreateDto reservationDto)
    {
        ReservationService_Response response =
            new ReservationService_Response();

        if (reservationDto.People <= 0)
        {
            response.Description =
                "The number of people must be greater than zero.";
            return response;
        }

        if (reservationDto.Date <= DateTime.Now)
        {
            response.Description =
                "The reservation date must be in the future.";
            return response;
        }

        Reservation reservation =
            ReservationsMapper.ReservationToEntity(reservationDto);

        if (_repository.CreateReservation(reservation))
        {
            response.Response = true;
            response.Description = "Reservation created successfully.";
            response.Reservation = reservation;
        }
        else
        {
            response.Description = "The reservation could not be created.";
        }

        return response;
    }

    public ReservationService_Response GetByID(int id)
    {
        ReservationService_Response response =
            new ReservationService_Response();

        if (id <= 0)
        {
            response.Description = "Invalid reservation ID.";
            return response;
        }

        Reservation? reservation = _repository.GetReservation(id);

        if (reservation == null)
        {
            response.Description = "Reservation not found.";
            return response;
        }

        response.Response = true;
        response.Description = "Reservation found.";
        response.Reservation = reservation;

        return response;
    }

    public ReservationService_Response GetAll()
    {
        ReservationService_Response response =
            new ReservationService_Response();

        response.Reservations = _repository.GetReservations();
        response.Response = true;
        response.Description = "Reservations retrieved successfully.";

        return response;
    }

    public ReservationService_Response Update(
        Reservation_UpdateDto reservationDto)
    {
        ReservationService_Response response =
            new ReservationService_Response();

        if (reservationDto.Id <= 0 ||
            !_repository.ReservationExists(reservationDto.Id))
        {
            response.Description = "Reservation not found.";
            return response;
        }

        if (reservationDto.People <= 0)
        {
            response.Description =
                "The number of people must be greater than zero.";
            return response;
        }

        if (reservationDto.Date <= DateTime.Now)
        {
            response.Description =
                "The reservation date must be in the future.";
            return response;
        }

        Reservation reservation =
            ReservationsMapper.ReservationToEntity(reservationDto);

        if (!_repository.UpdateReservation(reservation))
        {
            response.Description = "The reservation could not be updated.";
            return response;
        }

        response.Response = true;
        response.Description = "Reservation updated successfully.";
        response.Reservation = reservation;

        return response;
    }

    public ReservationService_Response Delete(int id)
    {
        ReservationService_Response response = GetByID(id);

        if (!response.Response || response.Reservation == null)
        {
            return response;
        }

        if (!_repository.DeleteReservation(response.Reservation))
        {
            response.Response = false;
            response.Description = "The reservation could not be deleted.";
            response.Reservation = null;
            return response;
        }

        response.Response = true;
        response.Description = "Reservation deleted successfully.";
        response.Reservation = null;

        return response;
    }
}
