using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.BaseControllers;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateResponse(await _discountService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateResponse(await _discountService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            return CreateResponse(await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            return CreateResponse(await _discountService.SaveAsync(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateResponse(await _discountService.UpdateAsync(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateResponse(await _discountService.DeleteAsync(id));
        }
    }
}
