using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Shared;

namespace AmanTaskBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> getEmployees()
        {
            SharedResponse<List<EmployeeDto>> response = await repo.GetAll();
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById([FromQuery] int id)
        {
            SharedResponse<EmployeeDto> response = await repo.GetById(id);
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesByDeptId([FromQuery] int deptId)
        {
            SharedResponse<List<EmployeeDto>> response = await repo.GetEmployeesByDeptId(deptId);
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employee)
        {
            SharedResponse<EmployeeDto> response = await repo.Create(employee);
            if (response.status == Status.problem) return Problem(response.message);
            if (response.status == Status.badRequest) return BadRequest(response.message);
            return Ok(response.data);
        }
        [HttpPut]
        public async Task<ActionResult<AddressDto>> PutEmployee([FromQuery] int id, EmployeeDto employee)
        {
            SharedResponse<EmployeeDto> response = await repo.Update(id, employee);
            if (response.status == Status.badRequest) return BadRequest();
            else if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployee([FromQuery] int id)
        {
            SharedResponse<EmployeeDto> response = await repo.Delete(id);
            if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
    }
}
