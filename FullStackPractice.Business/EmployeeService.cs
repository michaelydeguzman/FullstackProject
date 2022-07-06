using FullStackPractice.Business.Interfaces;
using FullStackPractice.Persistence.Models;
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

        public async Task<List<Employee>> GetAllEmployees()
        {
            return (List<Employee>)await _unitOfWork.EmployeeRepository.GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.EmployeeRepository.GetById(id);
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.Add(employee);
            await _unitOfWork.Complete();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.Update(employee);
            await _unitOfWork.Complete();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetById(id);

            if (employee != null)
            {
                await _unitOfWork.EmployeeRepository.Remove(employee);
                await _unitOfWork.Complete();
            }
        }

        public async Task<List<Employee>> GetAllEmployeesByDepartmentId(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetById(id);

            if (department != null)
            {
                return (List<Employee>)await _unitOfWork.EmployeeRepository.Find(x => x.DepartmentId == department.DepartmentId);
            }
            else
            {
                return null;
            }
        }
    }
}
