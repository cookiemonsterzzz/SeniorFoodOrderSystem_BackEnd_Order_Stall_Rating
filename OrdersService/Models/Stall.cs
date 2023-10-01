using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Stall
{
    public Guid Id { get; set; }

    public string StallName { get; set; } = null!;

    public string? StallDescription { get; set; }

    public string StallOwner { get; set; } = null!;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<StallRating> StallRatings { get; set; } = new List<StallRating>();
}
