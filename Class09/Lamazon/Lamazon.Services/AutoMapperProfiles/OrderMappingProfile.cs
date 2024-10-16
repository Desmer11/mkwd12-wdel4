﻿using AutoMapper;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.OrderStatus, opt => opt.Ignore())
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(y => y.OrderStatusId))
                .ReverseMap()
                .ForMember(x => x.OrderStatus, opt => opt.Ignore())
                .ForMember(x => x.OrderStatusId, opt => opt.MapFrom(y => y.OrderStatus));

            CreateMap<OrderLineItem, OrderLineItemViewModel>()
                .ReverseMap()
                .ForMember(x => x.Product, opt => opt.Ignore());

            CreateMap<PagedResultModel<Order>, PagedResultViewModel<OrderViewModel>>().ReverseMap();

            CreateMap<Order, Invoice>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.OrderId, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.InvoiceDate, opt => opt.MapFrom(y => DateTime.Now))
                .ForMember(x => x.InvoiceStatusId, opt => opt.MapFrom(y => (int)InvoiceStatusEnum.PendingPayment))
                .ForMember(x => x.InvoiceLineItems, opt => opt.MapFrom(y => y.OrderLineItems));

            CreateMap<OrderLineItem, InvoiceLineItem>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.OrderLineItemId, opt => opt.MapFrom(y => y.Id));

        }
    }
}
