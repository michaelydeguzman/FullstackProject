using FullStackPractice.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task CreateEmployee(Employee department);

        Task UpdateEmployee(Employee department);

        Task DeleteEmployee(int id);

        Task<List<Employee>> GetAllEmployeesByDepartmentId(int id);
    }
}
