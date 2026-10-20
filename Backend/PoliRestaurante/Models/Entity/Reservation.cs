using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class Reservation
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int People { get; set; }

    public string? Reason { get; set; }

    public int ReservationStatusId { get; set; }

    public int UserId { get; set; }

    public int TableId { get; set; }

    public virtual ReservationStatus ReservationStatus { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
