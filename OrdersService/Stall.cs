using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Stall
{
    public Guid Id { get; set; }

    public string Stallname { get; set; } = null!;

    public string? Stalldescription { get; set; }

    public string Stallowner { get; set; } = null!;

    public DateTimeOffset? Datetimecreated { get; set; }

    public DateTimeOffset? Datetimeupdated { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
