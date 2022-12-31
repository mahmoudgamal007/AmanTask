using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Shared;

namespace AmanTaskBackEnd.RepoInterfaces
{
    public interface IEmployeeRepo : IBaseRepo<SharedResponse<EmployeeDto>, SharedResponse<List<EmployeeDto>>, EmployeeDto>
    {
        public Task<SharedResponse<List<EmployeeDto>>> GetEmployeesByDeptId(int DeptId);

    }
}
