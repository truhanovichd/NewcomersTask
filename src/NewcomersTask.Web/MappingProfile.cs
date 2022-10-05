using AutoMapper;
using NewcomersTask.Models;

namespace NewcomersTask.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderSagaItemRequest, OrderSagaItem>();
            CreateMap<OrderCreateRequest, OrderSaga>();
        }
    }
}