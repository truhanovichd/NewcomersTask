namespace NewcomersTask.Models
{
    public class OrderCreateRequest
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }

        public List<OrderSagaItemRequest>? OrderSagaItems { get; set; }
    }
}
