namespace NewcomersTask.Web.Models
{
    public class OrderItemRequest
    {
        public string? Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
