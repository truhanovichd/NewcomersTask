namespace NewcomersTask.Models
{
    public class OrderStatusChanged
    {
        public Guid CorrelationId { get; set; }

        // public Guid OrderId { get; set; }

        public Status Status { get; set; }
    }
}
