using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmanTaskBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo repo;
        public DepartmentController(IDepartmentRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> getDepartments()
        {
            SharedResponse<List<DepartmentDto>> response = await repo.GetAll();
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById([FromQuery] int id)
        {
            SharedResponse<DepartmentDto> response = await repo.GetById(id);
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment(DepartmentDto department)
        {
            SharedResponse<DepartmentDto> response = await repo.Create(department);
            if (response.status == Status.problem) return Problem(response.message);
            if (response.status == Status.badRequest) return BadRequest(response.message);
            return Ok(response.data);
        }
        [HttpPut]
        public async Task<ActionResult<DepartmentDto>> PutDepartment([FromQuery] int id, DepartmentDto department)
        {
            SharedResponse<DepartmentDto> response = await repo.Update(id, department);
            if (response.status == Status.badRequest) return BadRequest();
            else if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment([FromQuery] int id)
        {
            SharedResponse<DepartmentDto> response = await repo.Delete(id);
            if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
    }
}
