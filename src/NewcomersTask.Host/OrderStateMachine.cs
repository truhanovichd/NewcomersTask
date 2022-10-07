using MassTransit;
using NewcomersTask.Models;

namespace NewcomersTask.Host
{
    public sealed partial class OrderStateMachine : MassTransitStateMachine<OrderSaga>
    {
        // public State Initial { get; private set; }
        public State AwaitingPacking { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }
        public State Cancelled { get; private set; }

        public Event<Order> OrderCreated { get; set; }
        public Event<OrderStatusChanged> OrderStatusChanged { get; set; }
        //public Event<OrderStatusChanged> OrderCancelled { get; set; }

        public OrderStateMachine()
        {
            //InstanceState(x => x.CurrentState, Initial, AwaitingPacking, Packed, Shipped, Cancelled);

            InstanceState(x => x.CurrentState);

            Initially(
                When(OrderCreated)
                    .Then((x) =>
                    {
                        x.Saga.CustomerSurname = x.Message.CustomerSurname;
                        x.Saga.CustomerName = x.Message.CustomerName;
                        x.Saga.OrderNumber = x.Message.OrderNumber;
                        x.Saga.OrderDate = DateTime.Now;
                        x.Saga.OrderSagaItem = x.Message?.OrderSagaItems?.Select(item => new OrderSagaItem { Sku = item.Sku, Price = item.Price, Quantity = item.Quantity }).ToList();
                    })
                    .TransitionTo(AwaitingPacking));

            During(AwaitingPacking,
                When(OrderStatusChanged)
                    .Then((x) =>
                    {
                        x.Saga.CorrelationId = x.Message.CorrelationId;
                        x.Saga.OrderDate = DateTime.Now;
                    })
                    .TransitionTo(Packed));

            During(Packed,
                When(OrderStatusChanged)
                    .Then((x) =>
                    {
                        x.Saga.CorrelationId = x.Message.CorrelationId;
                        x.Saga.OrderDate = DateTime.Now;
                    })
                    .TransitionTo(Shipped));

            //During(AwaitingPacking, Packed, Shipped,
            //    When(OrderCancelled).TransitionTo(Cancelled));

            Event(() => OrderCreated, x => x.CorrelateById(y => (Guid)y.CorrelationId));
        }
    }
}