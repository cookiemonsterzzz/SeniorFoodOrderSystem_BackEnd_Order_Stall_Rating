using System;
using System.Collections.Generic;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating;

public partial class User
{
    public Guid Id { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Passcode { get; set; } = null!;

    public string RoleType { get; set; } = null!;

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual ICollection<CustomerEnquiry> CustomerEnquiries { get; set; } = new List<CustomerEnquiry>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StallRating> StallRatings { get; set; } = new List<StallRating>();
}
