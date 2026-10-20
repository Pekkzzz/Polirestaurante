
using PoliRestaurante.Models.Entity;

public class ReservationService_Response
{
    public bool Response;
    public string Description;
    public Reservation? Reservation;
    public List<Reservation> Reservations;

    public ReservationService_Response()
    {
        Response = false;
        Description = "";
        Reservation = null;
        Reservations = new List<Reservation>();
    }
}
