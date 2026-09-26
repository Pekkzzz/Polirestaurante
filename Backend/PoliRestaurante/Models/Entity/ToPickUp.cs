using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class ToPickUp
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public DateTime? CompletedTime { get; set; }

    public int PickupStatusId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PickupStatus PickupStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
