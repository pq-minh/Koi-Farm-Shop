using KoiShop.Domain.Entities ;
namespace KoiShop.Domain.Respositories
{
    public interface IAddressDetailRepository
    {
        Task<int> Create(AddressDetail entity);
        Task<IEnumerable<AddressDetail>> GetAll(string userId);
    }
}
