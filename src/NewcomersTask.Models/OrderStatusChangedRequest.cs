namespace NewcomersTask.Models
{
    public class OrderStatusChangedRequest
    {
        public Guid CorrelationId { get; set; }
        public Status Status { get; set; }
    }
}