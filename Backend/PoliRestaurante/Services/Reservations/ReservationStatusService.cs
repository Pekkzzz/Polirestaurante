using PoliRestaurante.Models;

public class ReservationStatusService : IReservationStatusService
{
    private readonly IReservationStatusRepository _repository;

    public ReservationStatusService(IReservationStatusRepository Repository)
    {
        _repository = Repository;
    }

    public ReservationStatusService_Response Create(ReservationStatus_CreateDto newReservationStatusDto)
    {
        ReservationStatus newReservationStatus = ReservationsMapper.ReservationStatusToEnity(newReservationStatusDto);
        ReservationStatusService_Response response = ComprobateReservationStatus(newReservationStatus);
        if (!response.Response)
        {
            return response;
        }

        if (_repository.ReservationStatusExists(newReservationStatus.Name))
        {
            response.Response = false;
            response.Description = "User Role with this username already exists";
            response.reservationStatus = null;
            return response;

        }

        if (_repository.CreateReservationStatus(newReservationStatus))
        {
            response.Response = true;
            response.Description = "User Role Created Succesfully";
            response.reservationStatus = newReservationStatus;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Internal Error: Somethng is wrong when system try to create user role";
            response.reservationStatus = null;
            return response;
        }
        

    }

    public ReservationStatusService_Response GetByID(int id)
    {
        ReservationStatusService_Response response = new ReservationStatusService_Response();

        if (id <= 0)
        {
            response.Description = "User Role id cant be negative or 0";
            return response;
        }

        ReservationStatus? reservationStatus = _repository.GetReservationStatus(id);
        if (reservationStatus == null)
        {
            response.Description = "User Role id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "User Role Finded";
            response.reservationStatus = reservationStatus;
            return response;
        }
    }

    public ReservationStatusService_Response Update(ReservationStatus_UpdateDto reservationStatus_UpdateDto)
    {
        ReservationStatusService_Response reservationStatusService_Response = GetByID(reservationStatus_UpdateDto.Id);
        if (!reservationStatusService_Response.Response)
        {
            return reservationStatusService_Response;
        }

        ReservationStatus reservationStatus = ReservationsMapper.ReservationStatusToEnity(reservationStatus_UpdateDto);

        reservationStatusService_Response.Response = _repository.UpdateReservationStatus(reservationStatus);
        if (!reservationStatusService_Response.Response)
        {
            reservationStatusService_Response.reservationStatus = null;
            reservationStatusService_Response.Description = "ReservationStatus cant be updated";
            return reservationStatusService_Response;
        }
        
        reservationStatusService_Response = GetByID(reservationStatus_UpdateDto.Id);
        reservationStatusService_Response.Description = "ReservationStatus Updated";
        return reservationStatusService_Response;
    }

    public ReservationStatusService_Response Detele(int id)
    {
        ReservationStatusService_Response reservationStatusService_Response = GetByID(id);
        if (!reservationStatusService_Response.Response)
        {
            return reservationStatusService_Response;
        }
        if (reservationStatusService_Response.reservationStatus == null)
        {
            reservationStatusService_Response.Response = false;
            reservationStatusService_Response.Description = "Something is wrong";
            reservationStatusService_Response.reservationStatus = null;
            return reservationStatusService_Response;
        }
        else
        {
            reservationStatusService_Response.Response = _repository.DeleteReservationStatus(reservationStatusService_Response.reservationStatus);
            if (!reservationStatusService_Response.Response)
            {
                reservationStatusService_Response.Description = "User Cant Be Deleted";
                reservationStatusService_Response.reservationStatus = null;
                return reservationStatusService_Response;
            }
            else
            {
                return reservationStatusService_Response;
            }
            
        }

        
    }

    //----Utility----
    private ReservationStatusService_Response ComprobateReservationStatus(ReservationStatus newReservationStatus)
    {
        if (!UtilityInternalTools.NameValidityCheck(newReservationStatus.Name))
        {
            return new ReservationStatusService_Response(false, "User Role name is null or invalid", null);
        }
        return new ReservationStatusService_Response(true, "User Role is available", null);
    }
}