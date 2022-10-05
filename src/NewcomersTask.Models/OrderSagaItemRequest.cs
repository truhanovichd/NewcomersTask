namespace NewcomersTask.Models
{
    public class OrderSagaItemRequest
    {
        public string? Sku { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
