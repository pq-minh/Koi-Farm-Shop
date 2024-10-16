using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IRequestRepository
    {
        Task<Koi> CreateRequest(Koi entity);

        Task<IEnumerable<Request>> GetAllRequest(string userID);
        Task<string> DecisionRequest(int rqID,string decision);
    }
}
