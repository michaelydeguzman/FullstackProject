using FullStackPractice.Business.Interfaces;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository;
using FullStackPractice.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return (List<Employee>)await _unitOfWork.EmployeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.AddAsync(employee);
            await _unitOfWork.Complete();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            await _unitOfWork.Complete();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee != null)
            {
                await _unitOfWork.EmployeeRepository.RemoveAsync(employee);
                await _unitOfWork.Complete();
            }
        }

        public async Task<List<Employee>> GetAllEmployeesByDepartmentId(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department != null)
            {
                return (List<Employee>)await _unitOfWork.EmployeeRepository.FindAsync(x => x.DepartmentId == department.DepartmentId);
            }
            else
            {
                return null;
            }
        }
    }
}
