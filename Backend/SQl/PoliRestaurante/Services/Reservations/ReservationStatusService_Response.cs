using PoliRestaurante.Models;

public class ReservationStatusService_Response
{
    public bool Response;
    public string Description;
    public ReservationStatus? reservationStatus;

    public ReservationStatusService_Response(bool response, string description, ReservationStatus? reservationStatusObject)
    {
        Response = response;
        Description = description;
        reservationStatus = reservationStatusObject;
    }

    public ReservationStatusService_Response()
    {
        Response = false;
        Description = "";
        reservationStatus = null;
    }
}