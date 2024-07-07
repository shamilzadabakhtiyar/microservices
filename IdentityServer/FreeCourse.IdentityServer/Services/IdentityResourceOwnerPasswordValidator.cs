using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByEmailAsync(context.UserName);
            if (user is null)
            {
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "Email or password incorrect" } }
                };
                return;
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, context.Password);
            if (checkPassword is false)
            {
                context.Result.CustomResponse = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "Email or password incorrect" } }
                };
                return;
            }
            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
