namespace OrdersService.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public string Ordername { get; set; } = string.Empty;
        public string Orderdescription { get; set; } = string.Empty;
        public DateTime Orderdate { get; set; }
        public Guid Userid { get; set; }
        public string Foodname { get; set; } = string.Empty;
        public string Foodcustomization { get; set; } = string.Empty;
        public decimal Foodprice { get; set; }
        public int Quantity { get; set; }

    }
}
