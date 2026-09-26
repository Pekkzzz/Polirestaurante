using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class Table
{
    public int Id { get; set; }

    public int TableNum { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
