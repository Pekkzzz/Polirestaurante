using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int UserRoleId { get; set; }

    public virtual ICollection<ModificationsHistory> ModificationsHistories { get; set; } = new List<ModificationsHistory>();

    public virtual ICollection<OrderModification> OrderModifications { get; set; } = new List<OrderModification>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<ToPickUp> ToPickUps { get; set; } = new List<ToPickUp>();

    public virtual UserRole UserRole { get; set; } = null!;
}
