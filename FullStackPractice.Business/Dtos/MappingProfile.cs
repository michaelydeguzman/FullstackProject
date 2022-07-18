using AutoMapper;
using FullStackPractice.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
