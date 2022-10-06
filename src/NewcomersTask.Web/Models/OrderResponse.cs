namespace NewcomersTask.Web.Models
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }

        public string? ErrorMessage { get; set; }
    }
}