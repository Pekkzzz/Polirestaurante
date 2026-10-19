using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class OrderDetail
{
    public int Id { get; set; }

    public required string Description { get; set; }
    public string Observations { get; set; } = null!;

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int OrderDetailStatusId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual OrderDetailStatus OrderDetailStatus { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
