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
using FullStackPractice.Services.Models;
using FullStackPractice.Services.Dtos;
using AutoMapper;

namespace FullStackPractice.Business
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateDepartmentValidator _updateDepartmentValidator;
        private readonly DeleteDepartmentValidator _deleteDepartmentValidator;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, UpdateDepartmentValidator updateDepartmentValidator,
            DeleteDepartmentValidator deleteDepartmentValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _updateDepartmentValidator = updateDepartmentValidator;
            _deleteDepartmentValidator = deleteDepartmentValidator;
        }

        public async Task<ServiceResponse<List<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var response = new ServiceResponse<List<DepartmentDto>>();

            var obj = (List<Department>)await _unitOfWork.DepartmentRepository.GetAllAsync();

            response.Result = _mapper.Map<List<DepartmentDto>>(obj);
            response.IsSuccess = true;
            response.Messages = null;
            response.Error = null;
            return response;
        }

        public async Task<ServiceResponse<DepartmentDto>> GetDepartmentByIdAsync(int id)
        {
            var response = new ServiceResponse<DepartmentDto>();
            var obj = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            response.Result = _mapper.Map<DepartmentDto>(obj);
            response.IsSuccess = true;
            response.Messages = null;
            response.Error = null;
            return response;
        }

        public async Task<ServiceResponse<DepartmentDto>> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var response = new ServiceResponse<DepartmentDto>();

            var newDepartmentEntity = _mapper.Map<Department>(departmentDto);

            var validationResult = await _updateDepartmentValidator.ValidateAsync(newDepartmentEntity);

            if (!validationResult.IsValid)
            {
                response.Result = null;
                response.IsSuccess = false;
                response.Messages = new List<string>();
                response.Error = "Bad Request";

                foreach (var validationError in validationResult.Errors)
                {
                    response.Messages.Add(validationError.ErrorMessage);
                }
            }
            else
            {
                await _unitOfWork.DepartmentRepository.AddAsync(newDepartmentEntity);
                await _unitOfWork.Complete();

                response.Result = _mapper.Map<DepartmentDto>(newDepartmentEntity);
                response.IsSuccess = true;
                response.Messages = null;
                response.Error = null;
            }

            return response;
        }

        public async Task<ServiceResponse<DepartmentDto>> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var response = new ServiceResponse<DepartmentDto>();

            var departmentEntity = _mapper.Map<Department>(departmentDto);

            await _unitOfWork.DepartmentRepository.UpdateAsync(departmentEntity);
            await _unitOfWork.Complete();

            response.Result = _mapper.Map<DepartmentDto>(departmentEntity);
            response.IsSuccess = true;
            response.Messages = null;
            response.Error = null;

            return response;
        }

        public async Task<ServiceResponse<DepartmentDto>> DeleteDepartmentAsync(int id)
        {
            var response = new ServiceResponse<DepartmentDto>();

            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                response.Result = null;
                response.IsSuccess = false;
                response.Messages = new List<string>();
                response.Error = "Not Found";

                return response;
            }

            var validationResult = await _deleteDepartmentValidator.ValidateAsync(department);
            if (!validationResult.IsValid)
            {
                response.Result = null;
                response.IsSuccess = false;
                response.Messages = new List<string>();
                response.Error = "Bad Request";

                foreach (var validationError in validationResult.Errors)
                {
                    response.Messages.Add(validationError.ErrorMessage);
                }
            }
            else
            {
                await _unitOfWork.DepartmentRepository.RemoveAsync(department);
                await _unitOfWork.Complete();

                response.Result = new DepartmentDto();
                response.IsSuccess = true;
                response.Messages = null;
                response.Error = null;
            }

            return response;
        }
    }
}
