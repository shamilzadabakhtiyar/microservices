using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared.BaseControllers;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return CreateResponse( await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            return CreateResponse(await _mediator.Send(createOrderCommand));
        }
    }
}
