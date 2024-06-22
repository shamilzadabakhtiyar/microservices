using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.Category;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CategoryService(IDatabaseSetting databaseSetting, IMapper mapper)
        {
            var client = new MongoClient(databaseSetting.ConnectionString);
            var database = client.GetDatabase(databaseSetting.DatabaseName);
            _categories = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await (await _categories.FindAsync(x => true)).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), StatusCodes.Status200OK);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var categorie = await (await _categories.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();
            if (categorie is null)
                return Response<CategoryDto>.Fail("NotFound", StatusCodes.Status404NotFound);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categorie), StatusCodes.Status200OK);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(category);
            return Response<CategoryDto>.Success(StatusCodes.Status201Created);
        }
    }
}
