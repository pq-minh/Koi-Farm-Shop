using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Users
{

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public interface IUserContext
        {
            CurrentUser GetCurrentUser();
        }
        //lấy ra user hiện tại qua authorize, nếu làm phương thức cần Id user thì chỉ cần ta
        // gọi hàm này sẽ trả về id , email , roles 
        public CurrentUser GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (user == null)
            {
                throw new InvalidOperationException("user context is not present");
            }
            if (user == null || !user.IsAuthenticated)
            {
                return null;
            }
            IEnumerable<Claim> claim = user.Claims;

            var userId = claim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst("Email")?.Value;
            if (userId == null || email == null)
            {
                throw new InvalidOperationException("User ID or Email is not present in claims");
            }
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

            return new CurrentUser(userId, email, roles);
        }
    }
}

