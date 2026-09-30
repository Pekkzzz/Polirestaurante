using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
