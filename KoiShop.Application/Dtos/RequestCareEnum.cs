using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public enum RequestCareEnum
    {
        Success,
        UserNotAuthenticated,
        NotLoggedInYet,
        InvalidParameters,
        InvalidTypeParameters,
        ItemUpdated,
        NotAddOrderInYet,
        FailAddPackage,
        FailAddRequest,
        Fail
    }
}
