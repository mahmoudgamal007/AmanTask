using AmanTaskBackEnd.AmanDBContexts;
using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using AutoMapper;
using AmanTaskBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskBackEnd.Repositories
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AmanDbContext context;
        private readonly IMapper mapper;
        public DepartmentRepo(AmanDbContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }
        public async Task<SharedResponse<DepartmentDto>> Create(DepartmentDto model)
        {
            if (context.Departments == null)
            {
                return new SharedResponse<DepartmentDto>(Status.problem, null, "Entity Set 'db.Department' is null");
            }

            Department department = mapper.Map<Department>(model);
            context.Departments.Add(department);
            try
            {
                await context.SaveChangesAsync();
                model = mapper.Map<DepartmentDto>(department);
                return new SharedResponse<DepartmentDto>(Status.createdAtAction, model);
            }
            catch (Exception ex)
            {
                return new SharedResponse<DepartmentDto>(Status.badRequest, null, ex.ToString());
            }
        }

        public async Task<SharedResponse<DepartmentDto>> Delete(int Id)
        {
            if (context.Departments == null)
            {
                return new SharedResponse<DepartmentDto>(Status.notFound, null);

            }
            var department = await context.Departments.Where(d =>d.Id == Id && d.IsDeleted == false).FirstOrDefaultAsync();
            if (department == null)
            {
                return new SharedResponse<DepartmentDto>(Status.notFound, null);
            }
            department.IsDeleted = true;
            await context.SaveChangesAsync();
            return new SharedResponse<DepartmentDto>(Status.noContent, null);
        }

        public async Task<SharedResponse<List<DepartmentDto>>> GetAll()
        {
            if (context.Departments == null)
                return new SharedResponse<List<DepartmentDto>>(Status.notFound, null);
            var departmentDto = await context.Departments.Where(d => d.IsDeleted == false).ToListAsync();
            List<DepartmentDto> departments = mapper.
            Map<List<DepartmentDto>>(departmentDto);
            return new SharedResponse<List<DepartmentDto>>(Status.found, departments);
        }

        public async Task<SharedResponse<DepartmentDto>> GetById(int Id)
        {
            if (context.Departments == null)
                return new SharedResponse<DepartmentDto>(Status.notFound, null);
            var departmentDto = await context.Departments.Where(d => d.Id == Id && d.IsDeleted == false).FirstOrDefaultAsync();
            DepartmentDto department = mapper.
            Map<DepartmentDto>(departmentDto);
            return new SharedResponse<DepartmentDto>(Status.found, department);
        }

        public bool IsExists(int Id)
        {
            return (context.Departments?.Any(d => d.Id == Id&&d.IsDeleted==false)).GetValueOrDefault();
        }

        public async Task<SharedResponse<DepartmentDto>> Update(int Id, DepartmentDto model)
        {
            if (Id != model.Id)
            {
                return new SharedResponse<DepartmentDto>(Status.badRequest, null);
            }

            Department department = mapper.Map<Department>(model);

            context.Entry(department).State = EntityState.Modified;

            try
            {
                if (IsExists(Id))
                    await context.SaveChangesAsync();
                else
                    return new SharedResponse<DepartmentDto>(Status.notFound, null);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return new SharedResponse<DepartmentDto>(Status.noContent, null);
        }
    }
}
