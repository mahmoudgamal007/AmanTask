using AmanTaskBackEnd.AmanDBContexts;
using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Entities;
using AmanTaskBackEnd.RepoInterfaces;
using AmanTaskBackEnd.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskBackEnd.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly AmanDbContext context;
        private readonly IMapper mapper;
        public AddressRepo(AmanDbContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }

        public async Task<SharedResponse<AddressDto>> Create(AddressDto model)
        {

            if (context.Addresses == null)
            {
                return new SharedResponse<AddressDto>(Status.problem, null, "Entity Set 'db.Address' is null");
            }
            Address address = mapper.Map<Address>(model);
            context.Addresses.Add(address);
            try
            {
                await context.SaveChangesAsync();
                model = mapper.Map<AddressDto>(address);
                return new SharedResponse<AddressDto>(Status.createdAtAction, model);
            }
            catch (Exception ex)
            {
                return new SharedResponse<AddressDto>(Status.badRequest, null, ex.ToString());
            }

        }

        public async Task<SharedResponse<AddressDto>> Delete(int Id)
        {
            if (context.Addresses == null)
            {
                return new SharedResponse<AddressDto>(Status.notFound, null);

            }
            var address = await context.Addresses.Where(a => a.Id == Id && a.IsDeleted == false).FirstOrDefaultAsync();
            if (address == null)
            {
                return new SharedResponse<AddressDto>(Status.notFound, null);
            }
            address.IsDeleted = true;
            await context.SaveChangesAsync();
            return new SharedResponse<AddressDto>(Status.noContent, null);
        }

        public async Task<SharedResponse<List<AddressDto>>> GetAddressesByEmpId(int EmpId)
        {
            if (context.Addresses == null)
                return new SharedResponse<List<AddressDto>>(Status.notFound, null);
            var addressesDto = await context.Addresses.Where(a => a.EmployeeId == EmpId && a.IsDeleted == false).ToListAsync();
            List<AddressDto> addresses = mapper.
            Map<List<AddressDto>>(addressesDto);
            return new SharedResponse<List<AddressDto>>(Status.found, addresses);
        }

        public async Task<SharedResponse<List<AddressDto>>> GetAll()
        {
            if (context.Addresses == null)
                return new SharedResponse<List<AddressDto>>(Status.notFound, null);
            var addressesDto = await context.Addresses.Where(a => a.IsDeleted == false).ToListAsync();
            List<AddressDto> addresses = mapper.
            Map<List<AddressDto>>(addressesDto);
            return new SharedResponse<List<AddressDto>>(Status.found, addresses);
        }

        public async Task<SharedResponse<AddressDto>> GetById(int Id)
        {
            if (context.Addresses == null)
                return new SharedResponse<AddressDto>(Status.notFound, null);
            var addressDto = await context.Addresses.Where(a => a.Id == Id && a.IsDeleted == false).FirstOrDefaultAsync();
            AddressDto address = mapper.
            Map<AddressDto>(addressDto);
            return new SharedResponse<AddressDto>(Status.found, address);
        }

        public bool IsExists(int Id)
        {
            return (context.Addresses?.Any(a => a.Id == Id && a.IsDeleted == false)).GetValueOrDefault();
        }

        public async Task<SharedResponse<AddressDto>> Update(int Id, AddressDto model)
        {
            if (Id != model.Id)
            {
                return new SharedResponse<AddressDto>(Status.badRequest, null);
            }

            Address address = mapper.Map<Address>(model);

            context.Entry(address).State = EntityState.Modified;

            try
            {
                if (IsExists(Id))
                    await context.SaveChangesAsync();
                else
                    return new SharedResponse<AddressDto>(Status.notFound, null);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }

            return new SharedResponse<AddressDto>(Status.noContent, null);
        }
    }
}
