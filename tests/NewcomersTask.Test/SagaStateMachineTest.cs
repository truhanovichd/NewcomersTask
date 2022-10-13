using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewcomersTask.DB;
using NewcomersTask.Host;
using NewcomersTask.Models;
using NewcomersTask.Models.DB;
using NUnit.Framework;
using System.Reflection;

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
                    //cfg.SetSagaRepositoryProvider(new ConnectionFactory().CreateContextForSQLite());

                    cfg.AddDbContext<DbContext, SagaContext>((provider, builder) =>
                    {
                        builder.UseSqlite("DataSource=:memory:", m =>
                        {
                            m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                            m.MigrationsHistoryTable($"__{nameof(SagaContext)}");
                        });
                    });
                })
                //.AddDbContext<SagaContext>(options => );
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            var sagaId = Guid.NewGuid();
            var orderNumber = 123;

            await harness.Bus.Publish(new OrderCreated
            {
                OrderId = sagaId,
                OrderNumber = orderNumber,
                OrderDate = DateTime.UtcNow,
                CustomerName = "Test Customer Name",
                CustomerSurname = "Test Customer Surname",
                OrderSagaItems = new List<OrderItem> { new OrderItem { Price = 123.2m, Quantity = 1, Sku = "123Test"} }
            });

            //Assert.That(await harness.Consumed.Any<OrderItem>());

            var sagaHarness = harness.GetSagaStateMachineHarness<OrderStateMachine, OrderState>();

            //Assert.That(await sagaHarness.Consumed.Any<OrderItem>());

            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == sagaId));

            var instance = sagaHarness.Created.ContainsInState(sagaId, sagaHarness.StateMachine, sagaHarness.StateMachine.AwaitingPacking);
            Assert.IsNotNull(instance, "Saga instance not found");
            Assert.That(instance.OrderNumber, Is.EqualTo(orderNumber));
        }
    }
}
