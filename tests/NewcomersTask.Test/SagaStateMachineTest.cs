using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NewcomersTask.Host;
using NewcomersTask.Models;
using NewcomersTask.Models.DB;
using NUnit.Framework;

namespace NewcomersTask.Test
{
    [TestFixture]
    public class SagaStateMachineTest
    {
        [Test]
        public async Task When_CreateOrder_Expect_MoveOrderToStatusAwaitingPacking()
        {
            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>();
            })
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            var sagaId = Guid.NewGuid();
            var orderNumber = 123;

            await harness.Bus.Publish(new OrderCreated
            {
                OrderId = sagaId,
                OrderNumber = orderNumber
            });

            Assert.That(await harness.Consumed.Any<OrderCreated>());

            var sagaHarness = harness.GetSagaStateMachineHarness<OrderStateMachine, OrderState>();

            Assert.That(await sagaHarness.Consumed.Any<OrderCreated>());

            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == sagaId));

            var instance = sagaHarness.Created.ContainsInState(sagaId, sagaHarness.StateMachine, sagaHarness.StateMachine.AwaitingPacking);
            Assert.IsNotNull(instance, "Saga instance not found");
            Assert.That(instance.OrderNumber, Is.EqualTo(orderNumber));
        }

        [Test]
        public async Task When_UpdateOrder_Expect_MoveOrderToStatusPacked()
        {
            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>();
            })
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            var sagaId = Guid.NewGuid();
            var orderNumber = 1234;

            await harness.Bus.Publish(new OrderCreated
            {
                OrderId = sagaId,
                OrderNumber = orderNumber
            });

            await harness.Bus.Publish(new OrderStatusChanged
            {
                CorrelationId = sagaId,
                Status = Status.Packed
            });

            Assert.That(await harness.Consumed.Any<OrderCreated>());

            var sagaHarness = harness.GetSagaStateMachineHarness<OrderStateMachine, OrderState>();

            Assert.That(await sagaHarness.Consumed.Any<OrderStatusChanged>());

            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == sagaId));

            var instance = sagaHarness.Created.ContainsInState(sagaId, sagaHarness.StateMachine, sagaHarness.StateMachine.Packed);
            Assert.IsNotNull(instance, "Saga instance not found");
            Assert.That(instance.OrderNumber, Is.EqualTo(orderNumber));
        }

        [Test]
        public async Task When_CancelOrder_Expect_MoveOrderToStatusCancel()
        {
            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>();
            })
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            var sagaId = Guid.NewGuid();
            var orderNumber = 1235;

            await harness.Bus.Publish(new OrderCreated
            {
                OrderId = sagaId,
                OrderNumber = orderNumber
            });

            await harness.Bus.Publish(new OrderCancelled
            {
                CorrelationId = sagaId
            });

            Assert.That(await harness.Consumed.Any<OrderCreated>());

            var sagaHarness = harness.GetSagaStateMachineHarness<OrderStateMachine, OrderState>();

            Assert.That(await sagaHarness.Consumed.Any<OrderCancelled>());

            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == sagaId));

            var instance = sagaHarness.Created.ContainsInState(sagaId, sagaHarness.StateMachine, sagaHarness.StateMachine.Cancelled);
            Assert.IsNotNull(instance, "Saga instance not found");
            Assert.That(instance.OrderNumber, Is.EqualTo(orderNumber));
        }
    }
}
