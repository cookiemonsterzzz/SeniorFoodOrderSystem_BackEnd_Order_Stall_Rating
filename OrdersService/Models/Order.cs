using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Order
{
    public Guid Id { get; set; }

    public string OrderName { get; set; } = null!;

    public string? OrderDescription { get; set; }

    public DateTimeOffset? OrderDate { get; set; }

    public Guid UserId { get; set; }

    public string FoodName { get; set; } = null!;

    public string FoodCustomization { get; set; } = null!;

    public decimal FoodPrice { get; set; }

    public decimal Quantity { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
