using FullStackPractice.Persistence.Models;
using FullStackPractice.Services.Dtos;
using FullStackPractice.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<List<DepartmentDto>>> GetAllDepartmentsAsync();

        Task<ServiceResponse<DepartmentDto>> GetDepartmentByIdAsync(int id);

        Task<ServiceResponse<DepartmentDto>> CreateDepartmentAsync(DepartmentDto departmentDto);

        Task<ServiceResponse<DepartmentDto>> UpdateDepartmentAsync(DepartmentDto departmentDto);

        Task<ServiceResponse<DepartmentDto>> DeleteDepartmentAsync(int id);
    }
}
