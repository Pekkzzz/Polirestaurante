using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class OrderModification
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
