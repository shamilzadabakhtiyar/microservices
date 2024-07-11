using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new {id});
            return status < 1 ?
                Response<NoContent>.Fail("Internal server error", StatusCodes.Status500InternalServerError) :
                Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<List<Models.Discount>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), StatusCodes.Status200OK);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userId=@userId and code=@code", new { code, userId }))
                .FirstOrDefault();
            return discount is null ?
                Response<Models.Discount>.Fail("Not found", StatusCodes.Status404NotFound) :
                Response<Models.Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<Models.Discount>> GetByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new { id }))
                .FirstOrDefault();
            return discount is null ?
                Response<Models.Discount>.Fail("Not found", StatusCodes.Status404NotFound) :
                Response<Models.Discount>.Success(discount, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> SaveAsync(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("insert into discount(userId, rate, code) values(@UserId, @Rate, @Code)", discount);
            return status < 1 ?
                Response<NoContent>.Fail("Internal server error", StatusCodes.Status500InternalServerError) :
                Response<NoContent>.Success(StatusCodes.Status201Created);
        }

        public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userId=@UserId, rate=@Rate, code=@Code where id=@Id", discount);
            return status < 1 ?
                Response<NoContent>.Fail("Internal server error", StatusCodes.Status500InternalServerError) :
                Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
