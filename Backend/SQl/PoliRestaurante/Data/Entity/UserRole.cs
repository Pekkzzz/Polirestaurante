using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsOwner { get; set; } = false!;
    public bool CanEditSystems { get; set; } = false!;
    public bool CanEditOrders { get; set; } = false!;
    public bool CanSetOrdersStatus { get; set; } = false!;
    public bool CanSetToPickUpStatus { get; set; } = false!;
    public bool CanSetProductStock { get; set; } = false!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
