using System;
using System.Collections.Generic;

namespace SeniorFoodOrderSystem_BackEnd_Order_Stall_Rating;

public partial class FoodsCustomization
{
    public Guid Id { get; set; }

    public string FoodCustomizationName { get; set; } = null!;

    public decimal FoodCustomizationPrice { get; set; }

    public DateTimeOffset? DateTimeCreated { get; set; }

    public DateTimeOffset? DateTimeUpdated { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid FoodId { get; set; }

    public virtual Food Food { get; set; } = null!;
}
