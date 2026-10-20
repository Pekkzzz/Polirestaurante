
namespace PoliRestaurante.Models;

public class Reservation_CreateDto
{
    public DateTime Date { get; set; }

    public int People { get; set; }

    public string? Reason { get; set; }

    public int ReservationStatusId { get; set; }

    public int UserId { get; set; }

    public int TableId { get; set; }
}
