namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating;

public partial class StallRating
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid StallId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string? Review { get; set; }

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Stall Stall { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
