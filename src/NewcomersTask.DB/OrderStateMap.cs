using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.Models;

namespace NewcomersTask.DB
{
    public class OrderStateMap : SagaClassMap<OrderSaga>
    {
        protected override void Configure(EntityTypeBuilder<OrderSaga> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.OrderDate);

            // If using Optimistic concurrency, otherwise remove this property
            entity.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
