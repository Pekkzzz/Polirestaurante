using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public int ProductTypeId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductType ProductType { get; set; } = null!;
}
