using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewcomersTask.Models
{
    [Table("OrderSaga")]
    public class OrderSaga : SagaStateMachineInstance
    {
        public int OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public DateTime? ShippedDate { get; set; }

        [Key, Column(Order = 0)]
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }

        public byte[] RowVersion { get; set; }

        public List<OrderSagaItem>? OrderSagaItem { get; set; }
    }
}