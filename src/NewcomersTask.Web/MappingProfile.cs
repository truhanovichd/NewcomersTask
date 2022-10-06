using AutoMapper;
using NewcomersTask.Models;
using NewcomersTask.Web.Models;

namespace NewcomersTask.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItemRequest, OrderSagaItem>();
            CreateMap<OrderCreateRequest, OrderSaga>();
        }
    }
}