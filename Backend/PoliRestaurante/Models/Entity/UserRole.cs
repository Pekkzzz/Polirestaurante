using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class UserRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsOwner { get; set; }

    public bool CanEditSystems { get; set; }

    public bool CanEditOrders { get; set; }

    public bool CanSetOrdersStatus { get; set; }

    public bool CanSetToPickUpStatus { get; set; }

    public bool CanSetProductStock { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
