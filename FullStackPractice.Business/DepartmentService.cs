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

        public async Task<List<Department>> GetAllDepartments()
        {
            return (List<Department>)await _unitOfWork.DepartmentRepository.GetAll();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _unitOfWork.DepartmentRepository.GetById(id);
        }

        public async Task CreateDepartment(Department department)
        {
            await _unitOfWork.DepartmentRepository.Add(department);
            await _unitOfWork.Complete();
        }

        public async Task UpdateDepartment(Department department)
        {
            await _unitOfWork.DepartmentRepository.Update(department);
            await _unitOfWork.Complete();
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetById(id);

            if (department != null)
            {
                await _unitOfWork.DepartmentRepository.Remove(department);
                await _unitOfWork.Complete();
            }
        }
    }
}
