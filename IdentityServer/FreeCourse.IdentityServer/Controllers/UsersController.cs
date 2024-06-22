using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signup)
        {
            var user = new ApplicationUser()
            {
                UserName = signup.Username,
                Email = signup.Email,
                City = signup.City
            };

            var result = await _userManager.CreateAsync(user, signup.Password);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}
