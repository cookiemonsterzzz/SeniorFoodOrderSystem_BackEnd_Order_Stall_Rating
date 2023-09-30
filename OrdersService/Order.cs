using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Order
{
    public Guid Id { get; set; }

    public string Ordername { get; set; } = null!;

    public string? Orderdescription { get; set; }

    public DateTimeOffset? Orderdate { get; set; }

    public Guid Userid { get; set; }

    public string Foodname { get; set; } = null!;

    public string Foodcustomization { get; set; } = null!;

    public decimal Foodprice { get; set; }

    public decimal Quantity { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
