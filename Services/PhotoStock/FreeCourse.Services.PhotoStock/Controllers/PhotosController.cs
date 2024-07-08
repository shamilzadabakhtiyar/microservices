using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.BaseControllers;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSaveAsync(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo is null || photo.Length <= 0)
                return CreateResponse(Response<NoContent>.Fail("Must be a photo", StatusCodes.Status400BadRequest));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);
            var returnPath = Path.Combine("photos", photo.FileName);
            var photoDto = new PhotoDto()
            {
                Url = returnPath
            };
            return CreateResponse(Response<PhotoDto>.Success(photoDto, StatusCodes.Status201Created));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return CreateResponse(Response<NoContent>.Success(StatusCodes.Status204NoContent));
            }
            return CreateResponse(Response<NoContent>.Fail("Photo not found", StatusCodes.Status404NotFound));
        }
    }
}
