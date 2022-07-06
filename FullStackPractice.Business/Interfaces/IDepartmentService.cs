using FullStackPractice.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();

        Task<Department> GetDepartmentById(int id);

        Task CreateDepartment(Department department);

        Task UpdateDepartment(Department department);

        Task DeleteDepartment(int id);
    }
}
