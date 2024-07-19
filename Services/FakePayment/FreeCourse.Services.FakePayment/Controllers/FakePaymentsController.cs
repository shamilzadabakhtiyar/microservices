using FreeCourse.Shared.BaseControllers;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateResponse(Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
