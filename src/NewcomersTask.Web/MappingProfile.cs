using AutoMapper;
using NewcomersTask.Models;
using NewcomersTask.Web.Models;

namespace NewcomersTask.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItemRequest, OrderItem>();
            CreateMap<CreateOrderRequest, CreateOrder>();
            CreateMap<ChangedOrderStatusRequest, OrderStatusChanged>().ForMember(dest => dest.CorrelationId, act => act.MapFrom(src => src.OrderId));
            CreateMap<CancelOrderRequest, OrderCancelled>().ForMember(dest => dest.CorrelationId, act => act.MapFrom(src => src.OrderId));
        }
    }
}