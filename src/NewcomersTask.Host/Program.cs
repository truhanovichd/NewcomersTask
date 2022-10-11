// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.DB;
using NewcomersTask.Host;
using NewcomersTask.Models.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SagaContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings:ServerConnection").Value));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddDelayedMessageScheduler();
    cfg.AddSagaStateMachine<OrderStateMachine, OrderState>()
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
        rbfc.Host(builder.Configuration.GetSection("RabbitMq:Host").Value, h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMq:Username").Value);
            h.Password(builder.Configuration.GetSection("RabbitMq:Password").Value);
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