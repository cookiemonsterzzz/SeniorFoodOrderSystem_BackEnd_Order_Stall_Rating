namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating.Dto
{
    public class StallRatingDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid StallId { get; set; }

        public Guid UserId { get; set; }

        public int Rating { get; set; }

        public string? Review { get; set; }
    }
}
