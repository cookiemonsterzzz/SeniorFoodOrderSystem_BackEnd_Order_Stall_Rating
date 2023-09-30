using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class Food
{
    public Guid Id { get; set; }

    public string Foodname { get; set; } = null!;

    public string? Fooddescription { get; set; }

    public decimal Foodprice { get; set; }

    public DateTimeOffset? Datetimecreated { get; set; }

    public DateTimeOffset? Datetimeupdated { get; set; }

    public bool? Isdeleted { get; set; }

    public Guid Stallid { get; set; }

    public virtual ICollection<Foodscustomization> Foodscustomizations { get; set; } = new List<Foodscustomization>();

    public virtual Stall Stall { get; set; } = null!;
}
