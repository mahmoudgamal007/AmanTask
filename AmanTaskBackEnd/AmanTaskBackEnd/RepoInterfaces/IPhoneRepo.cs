using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Shared;

namespace AmanTaskBackEnd.RepoInterfaces
{
    public interface IPhoneRepo : IBaseRepo<SharedResponse<PhoneDto>, SharedResponse<List<PhoneDto>>, PhoneDto>
    {
        public Task<SharedResponse<List<PhoneDto>>> GetPhonesByEmpId(int EmpId);
    }
}
