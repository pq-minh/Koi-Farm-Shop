using Google.Apis.Auth;
using KoiShop.Application.Dtos;
using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiShop.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleLoginController2 : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public GoogleLoginController2(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback([FromBody] ExternalLoginRequest request)
        {
            var token = request.Token; // Lấy token từ request
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { error = "Token is missing." });
            }

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(token);
            }
            catch (InvalidJwtException)
            {
                return BadRequest(new { error = "Invalid token." });
            }

            var email = payload.Email;
            var info = new ExternalLoginInfo(
                new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.GivenName, payload.GivenName),
                    new Claim(ClaimTypes.Surname, payload.FamilyName)
                })),
                "Google", // loginProvider
                payload.Subject, // providerKey
                email // displayName
            );

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return Ok(new { redirectUrl = "/" }); 
            }

            var user = await userManager.FindByEmailAsync(email) ?? new User
            {
                UserName = email,
                Email = email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
            };

            await userManager.CreateAsync(user);
            await userManager.AddLoginAsync(user, info);
            await signInManager.SignInAsync(user, isPersistent: false);

            return Ok(new { redirectUrl = "/" });
        }
    }
}
