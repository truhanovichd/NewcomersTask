using MassTransit;
using NewcomersTask.Models;

namespace NewcomersTask.Host
{
    public sealed partial class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State AwaitingPacking { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }
        public State Cancelled { get; private set; }

        public Event<CreateOrder> OrderCreated { get; private set; }
        public Event<OrderStatusChanged> OrderStatusChanged { get; private set; }
        public Event<OrderCancelled> OrderCancelled { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState, AwaitingPacking, Packed, Shipped, Cancelled);

            Initially(
                When(OrderCreated)
                    .Then((x) =>
                    {
                        x.Saga.CorrelationId = x.Message.OrderId;
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
                        //x.Saga.CorrelationId = x.Message.OrderId;
                        x.Saga.OrderDate = DateTime.Now;
                    })
                    .TransitionTo(Packed));

            During(Packed,
                When(OrderStatusChanged)
                    .Then((x) =>
                    {
                        x.Saga.CorrelationId = x.Message.CorrelationId;
                        //x.Saga.CorrelationId = x.Message.OrderId;
                        x.Saga.OrderDate = DateTime.Now;
                    })
                    .TransitionTo(Shipped));

            During(AwaitingPacking,
                When(OrderCancelled).TransitionTo(Cancelled));

            Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderStatusChanged);
        }
    }
}