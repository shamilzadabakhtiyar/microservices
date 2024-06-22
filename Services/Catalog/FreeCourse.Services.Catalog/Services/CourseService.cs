using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Dtos.Course;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courses;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CourseService(IDatabaseSetting databaseSetting, IMapper mapper)
        {
            var client = new MongoClient(databaseSetting.ConnectionString);
            var database = client.GetDatabase(databaseSetting.DatabaseName);
            _courses = database.GetCollection<Course>(databaseSetting.CourseCollectionName);
            _categories = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await (await _courses.FindAsync(x => true)).ToListAsync();
            courses.ForEach(async y => y.Category = await (await _categories.FindAsync(z => z.Id == y.CategoryId)).FirstOrDefaultAsync());
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), StatusCodes.Status200OK);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await (await _courses.FindAsync(x => x.UserId == userId)).ToListAsync();
            courses.ForEach(async y => y.Category = await (await _categories.FindAsync(z => z.Id == y.CategoryId)).FirstOrDefaultAsync());
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), StatusCodes.Status200OK);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await (await _courses.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();
            if (course is null)
                return Response<CourseDto>.Fail("NotFound", StatusCodes.Status404NotFound);

            course.Category = await (await _categories.FindAsync(x => x.Id == course.CategoryId)).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), StatusCodes.Status200OK);
        }

        public async Task<Response<CourseDto>> CreateAsync(CreateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedDate = DateTime.Now;
            await _courses.InsertOneAsync(course);
            return Response<CourseDto>.Success(StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateCourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedDate = DateTime.Now;
            var result = await _courses.ReplaceOneAsync(x => x.Id == course.Id, course);
            if (result.IsModifiedCountAvailable)
                return Response<NoContent>.Success(StatusCodes.Status204NoContent);

            return Response<NoContent>.Fail("NotModified", StatusCodes.Status304NotModified);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courses.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
                return Response<NoContent>.Success(StatusCodes.Status204NoContent);

            return Response<NoContent>.Fail("NotModified", StatusCodes.Status304NotModified);
        }
    }
}
