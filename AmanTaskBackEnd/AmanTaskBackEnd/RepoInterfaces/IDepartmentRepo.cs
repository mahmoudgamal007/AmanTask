using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Shared;

namespace AmanTaskBackEnd.RepoInterfaces
{
    public interface IDepartmentRepo : IBaseRepo<SharedResponse<DepartmentDto>, SharedResponse<List<DepartmentDto>>, DepartmentDto>
    {
    }
}
