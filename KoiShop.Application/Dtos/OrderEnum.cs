using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public enum OrderEnum
    {
        Success,
        UserNotAuthenticated,
        NotLoggedInYet,
        InvalidParameters,
        ItemUpdated,
        NotAddOrderInYet,
        FailUpdateFish,
        FailUpdateCart,
        FailAddPayment,
        FailAdd,
        Fail
    }
}
