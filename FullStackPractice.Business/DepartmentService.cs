using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStackPractice.Services.Validations;
using FluentValidation;

namespace FullStackPractice.Business
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdateDepartmentValidator _updateDepartmentValidator;
        private readonly DeleteDepartmentValidator _deleteDepartmentValidator;

        public DepartmentService(IUnitOfWork unitOfWork, UpdateDepartmentValidator updateDepartmentValidator,
            DeleteDepartmentValidator deleteDepartmentValidator)
        {
            _unitOfWork = unitOfWork;
            _updateDepartmentValidator = updateDepartmentValidator;
            _deleteDepartmentValidator = deleteDepartmentValidator;
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
            var results = await _updateDepartmentValidator.ValidateAsync(department);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new Exception(failure.ErrorMessage);
                }
            }
            else
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                await _unitOfWork.Complete();
            }
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
                var results = await _deleteDepartmentValidator.ValidateAsync(department);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        throw new Exception(failure.ErrorMessage);
                    }
                }
                else
                {
                    await _unitOfWork.DepartmentRepository.RemoveAsync(department);
                    await _unitOfWork.Complete();
                }
            }
        }
    }
}
