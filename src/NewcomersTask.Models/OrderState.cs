using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace NewcomersTask.Models
{
    [Table("OrderSaga")]
    public class OrderState : SagaStateMachineInstance
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        [AllowNull]
        public DateTime? UpdatedDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        [AllowNull]
        public DateTime? ShippedDate { get; set; }

        [Key, Column(Order = 0)]
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }

        [NotMapped]
        public byte[] RowVersion { get; set; }

        public List<OrderSagaItem>? OrderSagaItem { get; set; }
    }
}