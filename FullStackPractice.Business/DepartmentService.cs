using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Business
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return (List<Department>)await _unitOfWork.DepartmentRepository.GetAllAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
        }

        public async Task CreateDepartmentAsync(Department department)
        {
            var departments = (List<Department>)await _unitOfWork.DepartmentRepository.GetAllAsync();

            if (departments.Any(x=>x.DepartmentName == department.DepartmentName))
            {
                return; 
            }

            await _unitOfWork.DepartmentRepository.AddAsync(department);
            await _unitOfWork.Complete();
            
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _unitOfWork.DepartmentRepository.UpdateAsync(department);
            await _unitOfWork.Complete();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department != null)
            {
                await _unitOfWork.DepartmentRepository.RemoveAsync(department); 
                await _unitOfWork.Complete();
            }
        }
    }
}
