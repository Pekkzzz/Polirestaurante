using System;
using System.Collections.Generic;

namespace PoliRestaurante.Models.Entity;

public partial class Receipt
{
    public int Id { get; set; }

    public decimal PaymentAmount { get; set; }

    public DateTime Datetime { get; set; }

    public int PaymentTypeId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;
}
