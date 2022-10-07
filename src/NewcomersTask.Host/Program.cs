using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.DB;
using NewcomersTask.Host;
using NewcomersTask.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMassTransit(cfg =>
//{

//    cfg.AddSagaStateMachine<OrderStateMachine, OrderSaga>()
//        .EntityFrameworkRepository(r =>
//        {
//            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion
//            r.AddDbContext<DbContext, SagaContext>((provider, builderContext) =>
//            {
//                builderContext.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings:ServerConnection").Value, m =>
//                {
//                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
//                    m.MigrationsHistoryTable($"__{nameof(SagaContext)}");
//                });
//            });
//            //r.ExistingDbContext<SagaContext>();
//            //r.LockStatementProvider = new PostgresLockStatementProvider();
//        });
//    cfg.SetKebabCaseEndpointNameFormatter();
//    cfg.AddDelayedMessageScheduler();
//    cfg.UsingRabbitMq((brc, rbfc) =>
//    {
//        rbfc.UseInMemoryOutbox();
//        rbfc.UseMessageRetry(r =>
//        {
//            r.Incremental(3, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
//        });
//        rbfc.UseDelayedMessageScheduler();
//        rbfc.Host("localhost", h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//        //rbfc.ConfigureEndpoints(brc);
//    });
//});

builder.Services.AddDbContext<SagaContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings:ServerConnection").Value));


builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddDelayedMessageScheduler();
    cfg.AddSagaStateMachine<OrderStateMachine, OrderSaga > ()
    .EntityFrameworkRepository(r =>
    {
        r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
        r.ExistingDbContext<SagaContext>();
        r.LockStatementProvider = new PostgresLockStatementProvider();
    });
    cfg.UsingRabbitMq((brc, rbfc) =>
    {
        rbfc.UseInMemoryOutbox();
        rbfc.UseMessageRetry(r =>
        {
            r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        });
        rbfc.UseDelayedMessageScheduler();
        rbfc.Host("localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        rbfc.ConfigureEndpoints(brc);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
