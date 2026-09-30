using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class PickupStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ToPickUp> ToPickUps { get; set; } = new List<ToPickUp>();
}
