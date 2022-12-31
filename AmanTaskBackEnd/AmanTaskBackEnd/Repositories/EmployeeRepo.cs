using AmanTaskBackEnd.AmanDBContexts;
using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Entities;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskBackEnd.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AmanDbContext context;
        private readonly IMapper mapper;
        public EmployeeRepo(AmanDbContext context , IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }
        public async Task<SharedResponse<EmployeeDto>> Create(EmployeeDto model)
        {
            if (context.Employees == null)
            {
                return new SharedResponse<EmployeeDto>(Status.problem, null, "Entity Set 'db.Employees' is null");
            }

            Employee employee = mapper.Map<Employee>(model);
            context.Employees.Add(employee);
            try
            {
                await context.SaveChangesAsync();
                model = mapper.Map<EmployeeDto>(employee);
                return new SharedResponse<EmployeeDto>(Status.createdAtAction, model);
            }
            catch (Exception ex)
            {
                return new SharedResponse<EmployeeDto>(Status.badRequest, null, ex.ToString());
            }
        }

        public async Task<SharedResponse<EmployeeDto>> Delete(int Id)
        {
            if (context.Employees == null)
            {
                return new SharedResponse<EmployeeDto>(Status.notFound, null);

            }
            var employee = await context.Employees.Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (employee == null)
            {
                return new SharedResponse<EmployeeDto>(Status.notFound, null);
            }
            employee.IsDeleted = true;
            await context.SaveChangesAsync();
            return new SharedResponse<EmployeeDto>(Status.noContent, null);
        }

        public async Task<SharedResponse<List<EmployeeDto>>> GetAll()
        {
            if (context.Employees == null)
                return new SharedResponse<List<EmployeeDto>>(Status.notFound, null);
            var employeeDto = await context.Employees.Where(e => e.IsDeleted == false).ToListAsync();
            List<EmployeeDto> employees = mapper.
            Map<List<EmployeeDto>>(employeeDto);
            return new SharedResponse<List<EmployeeDto>>(Status.found, employees);
        }

        public async Task<SharedResponse<EmployeeDto>> GetById(int Id)
        {
            if (context.Employees == null)
                return new SharedResponse<EmployeeDto>(Status.notFound, null);
            var employeeDto = await context.Employees.Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            EmployeeDto employee = mapper.
            Map<EmployeeDto>(employeeDto);
            return new SharedResponse<EmployeeDto>(Status.found, employee);
        }

        public async Task<SharedResponse<List<EmployeeDto>>> GetEmployeesByDeptId(int DeptId)
        {
            if (context.Employees == null)
                return new SharedResponse<List<EmployeeDto>>(Status.notFound, null);
            var employeeDto = await context.Employees.Where(e => e.DepartmentId == DeptId && e.IsDeleted == false).ToListAsync();
            List<EmployeeDto> employees = mapper.
            Map<List<EmployeeDto>>(employeeDto);
            return new SharedResponse<List<EmployeeDto>>(Status.found, employees);
        }

        public bool IsExists(int Id)
        {
            return (context.Employees?.Any(e => e.Id == Id&&e.IsDeleted==false)).GetValueOrDefault();
        }

        public async Task<SharedResponse<EmployeeDto>> Update(int Id, EmployeeDto model)
        {
            if (Id != model.Id)
            {
                return new SharedResponse<EmployeeDto>(Status.badRequest, null);
            }

            Employee employee = mapper.Map<Employee>(model);

            context.Entry(employee).State = EntityState.Modified;

            try
            {
                if (IsExists(Id))
                    await context.SaveChangesAsync();
                else
                    return new SharedResponse<EmployeeDto>(Status.notFound, null);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return new SharedResponse<EmployeeDto>(Status.noContent, null);
        }
    }
}
