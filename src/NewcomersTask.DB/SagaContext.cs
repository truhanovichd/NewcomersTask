using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.Models.DB;

namespace NewcomersTask.DB
{
    public class SagaContext : SagaDbContext
    {
        public SagaContext(DbContextOptions options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
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