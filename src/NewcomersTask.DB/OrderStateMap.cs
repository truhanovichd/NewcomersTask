using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewcomersTask.Models.DB;

namespace NewcomersTask.DB
{
    public class OrderStateMap : SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.OrderDate);

            // If using Optimistic concurrency, otherwise remove this property
            entity.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}