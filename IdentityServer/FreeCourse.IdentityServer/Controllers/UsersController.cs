using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
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

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null) return BadRequest();
            return Ok(new {Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City});
        }
    }
}
