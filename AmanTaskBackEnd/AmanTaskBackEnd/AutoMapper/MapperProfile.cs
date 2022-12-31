using AmanTaskBackEnd.DTOs;
using AmanTaskBackEnd.Entities;
using AutoMapper;
namespace AmanTaskBackEnd.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ForMember(d => d.DepartmentName, o => o.MapFrom(o => o.Department.Name));
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<PhoneDto, Phone>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
