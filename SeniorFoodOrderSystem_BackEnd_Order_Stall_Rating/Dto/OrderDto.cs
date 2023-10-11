namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string OrderName { get; set; } = null!;

        public string? OrderDescription { get; set; }

        public DateTimeOffset? OrderDate { get; set; }

        public Guid UserId { get; set; }

        public Guid StallId { get; set; }

        public string FoodName { get; set; } = null!;

        public string FoodCustomization { get; set; } = null!;

        public decimal FoodPrice { get; set; }

        public decimal Amount { get; set; }

        public decimal Quantity { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public RatingDto? Rating { get; set; } = null;
    }

    public class RatingDto
    {
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
