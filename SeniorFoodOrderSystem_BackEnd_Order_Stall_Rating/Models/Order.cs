namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating;

public partial class Order
{
    public Guid Id { get; set; }

    public string OrderName { get; set; } = null!;

    public string? OrderDescription { get; set; }

    public DateTimeOffset? OrderDate { get; set; }

    public Guid UserId { get; set; }

    public string FoodName { get; set; } = null!;

    public string FoodCustomization { get; set; } = null!;

    public decimal FoodPrice { get; set; }

    public decimal Quantity { get; set; }

    public decimal Amount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public Guid StallId { get; set; }

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Stall Stall { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
