using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class OrderType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
