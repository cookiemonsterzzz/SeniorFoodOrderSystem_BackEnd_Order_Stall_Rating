using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class CustomerEnquiry
{
    public Guid Id { get; set; }

    public string EnquiriesSubject { get; set; } = null!;

    public string EnquiriesDescription { get; set; } = null!;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
