
using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto> GetEmployeeByIdAsync(int id);

        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto department);

        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto department);

        Task<bool> DeleteEmployeeAsync(int id);

        Task<List<EmployeeDto>> GetAllEmployeesByDepartmentIdAsync(int id);
    }
}
