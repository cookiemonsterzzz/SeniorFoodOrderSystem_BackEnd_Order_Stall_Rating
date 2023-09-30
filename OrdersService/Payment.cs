using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public decimal Amount { get; set; }

    public Guid Stallid { get; set; }

    public string Paymentstatus { get; set; } = null!;

    public DateTimeOffset? Datetimecreated { get; set; }

    public DateTimeOffset? Datetimeupdated { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Stall Stall { get; set; } = null!;
}
