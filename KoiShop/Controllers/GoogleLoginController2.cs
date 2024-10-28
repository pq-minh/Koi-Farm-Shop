using Google.Apis.Auth;
using KoiShop.Application.Dtos;
using KoiShop.Application.JwtToken;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IJwtTokenService jwtTokenService;
        public GoogleLoginController2(IJwtTokenService jwtTokenService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.jwtTokenService = jwtTokenService;
        }
        [HttpPost("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback([FromBody] ExternalLoginRequest request)
        {
            var token = request.Token;
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
                "Google",
                payload.Subject,
                email
            );

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            User user = null;

            if (signInResult.Succeeded)
            {
                user = await userManager.FindByEmailAsync(email);
               
            }
            else
            {
                user = await userManager.FindByEmailAsync(email);
                if (user == null) 
                {
                    user = new User
                    {
                        UserName = email,
                        Email = email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        PhoneNumber = string.Empty 
                    };
                    var createResult = await userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        return BadRequest(new { error = "User creation failed.", details = createResult.Errors });
                    }

                    await userManager.AddLoginAsync(user, info);
                    await userManager.AddToRoleAsync(user, UserRoles.Customer);
                }

                await signInManager.SignInAsync(user, isPersistent: false);
            }
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                return Ok(new { redirectUrl = "/userinfor", message = "Please complete your profile." });
            }
            var jwtToken = await jwtTokenService.GenerateToken(user);
            return Ok(new { redirectUrl = "/", token = jwtToken });
        }


    }

}
