using System;
using System.Collections.Generic;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating;

public partial class Stall
{
    public Guid Id { get; set; }

    public string StallName { get; set; } = null!;

    public string? StallDescription { get; set; }

    public string StallOwner { get; set; } = null!;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StallRating> StallRatings { get; set; } = new List<StallRating>();
}
