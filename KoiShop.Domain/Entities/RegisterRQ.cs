using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Entities
{
    public class RegisterRQ
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; init; }

        public string PhoneNumber { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }

    }
}
