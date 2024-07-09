using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(StatusCodes.Status204NoContent) :
                Response<bool>.Fail("Basket not found", StatusCodes.Status404NotFound);
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            var basketExist = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(basketExist))
                return Response<BasketDto>.Fail("Basket not found", StatusCodes.Status404NotFound);
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basketExist), StatusCodes.Status200OK);
        }

        public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basket)
        {
           var status = await _redisService.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
            return status ? Response<bool>.Success(StatusCodes.Status204NoContent) : 
                Response<bool>.Fail("Basket couldn't update or save", StatusCodes.Status500InternalServerError);
        }
    }
}
