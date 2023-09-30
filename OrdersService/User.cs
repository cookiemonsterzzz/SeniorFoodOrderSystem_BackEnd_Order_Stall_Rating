using System;
using System.Collections.Generic;

namespace OrdersService;

public partial class User
{
    public Guid Id { get; set; }

    public string Phoneno { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Passcode { get; set; } = null!;

    public string Roletype { get; set; } = null!;

    public DateTimeOffset? Datetimecreated { get; set; }

    public DateTimeOffset? Datetimeupdated { get; set; }

    public virtual ICollection<Customerenquiry> Customerenquiries { get; set; } = new List<Customerenquiry>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
