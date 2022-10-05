// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewcomersTask.DB;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

var config = builder.Build();

//var orderStateMachine = new OrderStateMachine();
//var repository = new InMemorySagaRepository<OrderState>();

//var busControl = Bus.Factory.CreateUsingInMemory(x =>
//{
//    x.ReceiveEndpoint("order-state", e =>
//    {
//        e.StateMachineSaga(orderStateMachine, repository);
//    });
//});


var optionsBuilder = new DbContextOptionsBuilder<SagaContext>();
optionsBuilder.UseNpgsql(config.GetSection("ConnectionStrings:ServerConnection").Value);

using var context = new SagaContext(optionsBuilder.Options);
