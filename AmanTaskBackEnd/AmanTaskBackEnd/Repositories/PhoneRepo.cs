using AmanTaskBackEnd.AmanDBContexts;
using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Entities;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskBackEnd.Repositories
{
    public class PhoneRepo : IPhoneRepo
    {
        private readonly AmanDbContext context;
        private readonly IMapper mapper;
        public PhoneRepo(AmanDbContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }
        public async Task<SharedResponse<PhoneDto>> Create(PhoneDto model)
        {
            if (context.Phones == null)
            {
                return new SharedResponse<PhoneDto>(Status.problem, null, "Entity Set 'db.Phone' is null");
            }

            Phone Phone = mapper.Map<Phone>(model);
            context.Phones.Add(Phone);
            try
            {
                await context.SaveChangesAsync();
                model = mapper.Map<PhoneDto>(Phone);
                return new SharedResponse<PhoneDto>(Status.createdAtAction, model);
            }
            catch (Exception ex)
            {
                return new SharedResponse<PhoneDto>(Status.badRequest, null, ex.ToString());
            }
        }

        public async Task<SharedResponse<PhoneDto>> Delete(int Id)
        {
            if (context.Phones == null)
            {
                return new SharedResponse<PhoneDto>(Status.notFound, null);

            }
            var Phone = await context.Phones.Where(p => p.Id == Id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (Phone == null)
            {
                return new SharedResponse<PhoneDto>(Status.notFound, null);
            }
            Phone.IsDeleted = true;
            await context.SaveChangesAsync();
            return new SharedResponse<PhoneDto>(Status.noContent, null);
        }

        public async Task<SharedResponse<List<PhoneDto>>> GetAll()
        {
            if (context.Phones == null)
                return new SharedResponse<List<PhoneDto>>(Status.notFound, null);
            var PhonesDto = await context.Phones.Where(a => a.IsDeleted == false).ToListAsync();
            List<PhoneDto> Phones = mapper.
            Map<List<PhoneDto>>(PhonesDto);
            return new SharedResponse<List<PhoneDto>>(Status.found, Phones);
        }

        public async Task<SharedResponse<PhoneDto>> GetById(int Id)
        {
            if (context.Phones == null)
                return new SharedResponse<PhoneDto>(Status.notFound, null);
            var PhoneDto = await context.Phones.Where(a => a.Id == Id && a.IsDeleted == false).FirstOrDefaultAsync();
            PhoneDto Phone = mapper.
            Map<PhoneDto>(PhoneDto);
            return new SharedResponse<PhoneDto>(Status.found, Phone);
        }

        public async Task<SharedResponse<List<PhoneDto>>> GetPhonesByEmpId(int EmpId)
        {
            if (context.Phones == null)
                return new SharedResponse<List<PhoneDto>>(Status.notFound, null);
            var PhonesDto = await context.Phones.Where(p => p.EmployeeId == EmpId && p.IsDeleted == false).ToListAsync();
            List<PhoneDto> Phones = mapper.
            Map<List<PhoneDto>>(PhonesDto);
            return new SharedResponse<List<PhoneDto>>(Status.found, Phones);
        }

        public bool IsExists(int Id)
        {
            return (context.Phones?.Any(p => p.Id == Id&&p.IsDeleted==false)).GetValueOrDefault();
        }

        public async Task<SharedResponse<PhoneDto>> Update(int Id, PhoneDto model)
        {
            if (Id != model.Id)
            {
                return new SharedResponse<PhoneDto>(Status.badRequest, null);
            }

            Phone Phone = mapper.Map<Phone>(model);

            context.Entry(Phone).State = EntityState.Modified;

            try
            {
                if (IsExists(Id))
                    await context.SaveChangesAsync();
                else
                    return new SharedResponse<PhoneDto>(Status.notFound, null);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return new SharedResponse<PhoneDto>(Status.noContent, null);
        }
    }
}
