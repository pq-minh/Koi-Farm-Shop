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
        InvalidTypeParameters,
        ItemUpdated,
        NotAddOrderInYet,
        FailUpdateFish,
        FailPaid,
        FailUpdateCart,
        FailUpdatePayment,
        FailAddPayment,
        FailAdd,
        Fail
    }
    public class PaymentResponse
    {
        public OrderEnum Status { get; set; }
        public string Message { get; set; }
    }
}
