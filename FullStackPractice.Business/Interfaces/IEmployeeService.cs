using FullStackPractice.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task CreateEmployeeAsync(Employee department);

        Task UpdateEmployeeAsync(Employee department);

        Task DeleteEmployeeAsync(int id);

        Task<List<Employee>> GetAllEmployeesByDepartmentId(int id);
    }
}
