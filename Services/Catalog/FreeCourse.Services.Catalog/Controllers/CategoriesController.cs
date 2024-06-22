using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Services.Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreeCourse.Shared.BaseControllers;
using FreeCourse.Services.Catalog.Dtos.Category;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateResponse(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateResponse(await _categoryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto categoryDto)
        {
            return CreateResponse(await _categoryService.CreateAsync(categoryDto));
        }
    }
}
