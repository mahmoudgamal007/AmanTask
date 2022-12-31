using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Shared;

namespace AmanTaskBackEnd.RepoInterfaces
{
    public interface IAddressRepo : IBaseRepo<SharedResponse<AddressDto>, SharedResponse<List<AddressDto>>, AddressDto>
    {
        public Task<SharedResponse<List<AddressDto>>> GetAddressesByEmpId(int EmpId);
    }
}
