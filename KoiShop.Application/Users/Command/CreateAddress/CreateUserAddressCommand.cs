using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.UpdateAddress
{
    public class CreateUserAddressCommand() : IRequest<int>
    {
        public string? City { get; set; } 
        public string? Dictrict { get; set; } 
        public string? UserId { get; set; } 
        public virtual User? User { get; set; } 
        public string? StreetName { get; set; } 
        public string? Ward { get; set; } 
    }
}
