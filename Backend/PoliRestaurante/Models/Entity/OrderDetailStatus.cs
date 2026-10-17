using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class OrderDetailStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
