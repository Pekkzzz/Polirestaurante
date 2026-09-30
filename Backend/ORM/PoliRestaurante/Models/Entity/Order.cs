using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class Order
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal Payments { get; set; }

    public decimal TotalBalance { get; set; }

    public DateTime CreationDate { get; set; }

    public int PaymentStatusId { get; set; }

    public int OrderStatusId { get; set; }

    public int OrderTypeId { get; set; }

    public int? TableId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderModification> OrderModifications { get; set; } = new List<OrderModification>();

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual OrderType OrderType { get; set; } = null!;

    public virtual PaymentStatus PaymentStatus { get; set; } = null!;

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual Table? Table { get; set; }

    public virtual ICollection<ToPickUp> ToPickUps { get; set; } = new List<ToPickUp>();
}
