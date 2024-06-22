using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(CreateCourseDto courseDto);
        Task<Response<NoContent>> UpdateAsync(UpdateCourseDto courseDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
