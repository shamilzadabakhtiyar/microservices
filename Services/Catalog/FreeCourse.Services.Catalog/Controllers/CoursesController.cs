using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.BaseControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateResponse(await _courseService.GetAllAsync());
        }

        [HttpGet]
        [Route("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(string userId)
        {
            return CreateResponse(await _courseService.GetAllByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateResponse(await _courseService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCourseDto courseDto)
        {
            return CreateResponse(await _courseService.CreateAsync(courseDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCourseDto courseDto)
        {
            return CreateResponse(await _courseService.UpdateAsync(courseDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateResponse(await _courseService.DeleteAsync(id));
        }
    }
}
