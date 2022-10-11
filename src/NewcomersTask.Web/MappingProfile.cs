// <copyright file="MappingProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
            CreateMap<CreateOrderRequest, OrderCreated>();
            CreateMap<ChangedOrderStatusRequest, OrderStatusChanged>().ForMember(dest => dest.CorrelationId, act => act.MapFrom(src => src.OrderId));
            CreateMap<CancelOrderRequest, OrderCancelled>().ForMember(dest => dest.CorrelationId, act => act.MapFrom(src => src.OrderId));
        }
    }
}