using MassTransit;
using NewcomersTask.Models;

namespace NewcomersTask.Host
{
    public sealed partial class OrderStateMachine :
    MassTransitStateMachine<OrderSaga>
    {
        public State Initial { get; private set; }
        public State AwaitingPacking { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }
        public State Cancelled { get; private set; }
        //public Event CurrentState { get; private set; }

        public Event<OrderSaga> OrderCreatedEvent { get; set; }
        public Event<OrderSaga> OrderStatusChangedEvent { get; set; }
        public Event<OrderSaga> ShippedEvent { get; set; }
        public Event<OrderSaga> CancelledEvent { get; set; }

        public OrderStateMachine()
        {
            //InstanceState(x => x.CurrentState, Initial, AwaitingPacking, Packed, Shipped, Cancelled);

            InstanceState(x => x.CurrentState);
            Event(() => OrderCreatedEvent, x => x.CorrelateById(y => (Guid)y.CorrelationId));
        }
    }
}