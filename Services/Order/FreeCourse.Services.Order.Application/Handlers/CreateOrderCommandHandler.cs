using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = ObjectMapper.Mapper.Map<Address>(request.Address);
            Domain.OrderAggregate.Order order = new(address, request.UserId);
            request.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.PictureUrl, x.Price);
            });
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new() { OrderId = order.Id }, StatusCodes.Status200OK);
        }
    }
}
