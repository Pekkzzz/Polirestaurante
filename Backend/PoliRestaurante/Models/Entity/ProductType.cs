using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class ProductType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
