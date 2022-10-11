// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using log4net.Config;
using MassTransit;
using NewcomersTask.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddLog4Net();
XmlConfigurator.Configure(new FileInfo("log4net.config"));

builder.Services.AddReceiveObserver<ReceiveObserver>();
builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddDelayedMessageScheduler();
    cfg.UsingRabbitMq((brc, rbfc) =>
    {
        rbfc.UseInMemoryOutbox();
        rbfc.UseMessageRetry(r =>
        {
            r.Incremental(3, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
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