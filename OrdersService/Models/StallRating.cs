using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class StallRating
{
    public Guid Id { get; set; }

    public Guid StallId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string? Review { get; set; }

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual Stall Stall { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
