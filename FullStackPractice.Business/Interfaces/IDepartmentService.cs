
using FullStackPractice.Contracts;
using FullStackPractice.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartmentsAsync();

        Task<DepartmentDto> GetDepartmentByIdAsync(int id);

        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);

        Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto);

        Task<bool> DeleteDepartmentAsync(int id);
    }
}
