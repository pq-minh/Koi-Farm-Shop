using KoiShop.Application.JwtToken;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Queries.Login
{
    public class LoginUserQuery(LoginRequest login) : IRequest<string>
    {
        public LoginRequest Login = login;
    }
}
