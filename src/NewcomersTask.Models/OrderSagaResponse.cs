namespace NewcomersTask.Models
{
    public class OrderSagaResponse
    {
        public Guid OrderId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}