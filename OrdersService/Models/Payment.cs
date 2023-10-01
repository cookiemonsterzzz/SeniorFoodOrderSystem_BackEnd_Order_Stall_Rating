using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public Guid StallId { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Stall Stall { get; set; } = null!;
}
