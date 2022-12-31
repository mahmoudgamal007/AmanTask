using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmanTaskBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneRepo repo;
        public PhoneController(IPhoneRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<PhoneDto>>> GetPhones()
        {
            SharedResponse<List<PhoneDto>> response = await repo.GetAll();
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<PhoneDto>> GetPhoneById([FromQuery] int id)
        {
            SharedResponse<PhoneDto> response = await repo.GetById(id);
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<List<PhoneDto>>> GetPhonesByUserId([FromQuery] int empId)
        {
            SharedResponse<List<PhoneDto>> response = await repo.GetPhonesByEmpId(empId);
            if (response.status == Status.notFound) return NotFound();
            return Ok(response.data);
        }
        [HttpPost]
        public async Task<ActionResult<PhoneDto>> PostAddress(PhoneDto phone)
        {
            SharedResponse<PhoneDto> response = await repo.Create(phone);
            if (response.status == Status.problem) return Problem(response.message);
            if (response.status == Status.badRequest) return BadRequest(response.message);
            return Ok(response.data);
        }
        [HttpPut]
        public async Task<ActionResult<PhoneDto>> PutAddress([FromQuery] int id, PhoneDto phone)
        {
            SharedResponse<PhoneDto> response = await repo.Update(id, phone);
            if (response.status == Status.badRequest) return BadRequest();
            else if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<PhoneDto>> DeleteAddress([FromQuery] int id)
        {
            SharedResponse<PhoneDto> response = await repo.Delete(id);
            if (response.status == Status.notFound) return NotFound();
            return NoContent();
        }
    }
}
