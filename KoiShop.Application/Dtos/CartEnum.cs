using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public enum CartEnum
    {
        Success,
        UserNotAuthenticated,
        NotLoggedInYet,
        InvalidParameters,
        ItemUpdated,
        NotRemovedInYet,
        Fail
    }
}
