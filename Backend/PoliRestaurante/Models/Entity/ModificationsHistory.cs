using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class ModificationsHistory
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
