﻿using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.Models;

namespace NewcomersTask.DB
{
    public class SagaContext : SagaDbContext
    {
        public SagaContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderStateMap(); }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<OrderSagaItem>()
                .HasOne(i => i.OrderSaga)
                .WithMany(o => o.OrderSagaItem)
                .HasForeignKey("OrderCorrelationId");
        }
    }
}