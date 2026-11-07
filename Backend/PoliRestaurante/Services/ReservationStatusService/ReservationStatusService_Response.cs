using PoliRestaurante.Models.Entity;

public class ReservationStatusService_Response
{
    public bool Response;
    public string Description;
    public ReservationStatus? reservationStatus;
    public List<ReservationStatus>? reservationStatuses;

    public ReservationStatusService_Response(bool response, string description, ReservationStatus? reservationStatusObject, List<ReservationStatus> reservationStatusesObject)
    {
        Response = response;
        Description = description;
        reservationStatus = reservationStatusObject;
        reservationStatuses = reservationStatusesObject;
    }

    public ReservationStatusService_Response()
    {
        Response = false;
        Description = "";
        reservationStatus = null;
        reservationStatuses = null;
    }
}