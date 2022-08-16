using AutoMapper;
using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;

namespace FullStackPractice.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

            CreateMap<Employee, EmployeeDto>().ForMember(x=>x.Password, opt => opt.Ignore());
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
