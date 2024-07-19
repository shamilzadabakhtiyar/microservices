using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap();

            CreateMap<Address, AddressDto>()
                .ReverseMap();
        }
    }
}
