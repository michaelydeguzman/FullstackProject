
using FullStackPractice.Repository;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FullStackPractice.Services.Models;
using AutoMapper;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Contracts;
using FullStackPractice.Services;
using FullStackPractice.Services.Constants;
using FullStackPractice.Validations;

namespace FullStackPractice.Business
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationManager _validator;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, IValidationManager validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var obj = (List<Department>)await _unitOfWork.DepartmentRepository.GetAllAsync();

            var result = _mapper.Map<List<DepartmentDto>>(obj);
            return result;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var obj = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            var result = _mapper.Map<DepartmentDto>(obj);
            return result;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var newDepartmentEntity = _mapper.Map<Department>(departmentDto);

            var validationResult = await _validator.CreateDepartment.ValidateAsync(newDepartmentEntity);

            if (validationResult.IsValid)
            {
                await _unitOfWork.DepartmentRepository.AddAsync(newDepartmentEntity);
                await _unitOfWork.Complete();

                var result = _mapper.Map<DepartmentDto>(newDepartmentEntity);

                return result;
            }
            else
            {
                throw new ServiceException(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var departmentEntity = _mapper.Map<Department>(departmentDto);

            var validationResult = await _validator.UpdateDepartment.ValidateAsync(departmentEntity);

            if (validationResult.IsValid)
            {
                await _unitOfWork.DepartmentRepository.UpdateAsync(departmentEntity);
                await _unitOfWork.Complete();

                var result = _mapper.Map<DepartmentDto>(departmentEntity);
                return result;
            }
            else
            {
                throw new ServiceException(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                throw new ServiceException(ValidationMessages.DepartmentNotFound);
            }

            var validationResult = await _validator.DeleteDepartment.ValidateAsync(department);
            if (validationResult.IsValid)
            {
                await _unitOfWork.DepartmentRepository.RemoveAsync(department);
                await _unitOfWork.Complete();

                return true;
            }
            else
            {
                throw new ServiceException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}
