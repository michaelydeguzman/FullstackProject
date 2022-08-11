using AutoMapper;
using FullStackPractice.Business.Interfaces;
using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using FullStackPractice.Repository;
using FullStackPractice.Repository.Interfaces;
using FullStackPractice.Security;
using FullStackPractice.Services;
using FullStackPractice.Services.Constants;
using FullStackPractice.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationManager _validator;
        private readonly ISecurityManager _securityManager;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IValidationManager validator, ISecurityManager securityManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _securityManager = securityManager;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var obj = (List<Employee>)await _unitOfWork.EmployeeRepository.GetAllAsync();

            var result = _mapper.Map<List<EmployeeDto>>(obj);
            return result;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var obj = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            var result = _mapper.Map<EmployeeDto>(obj);
            return result;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var newEmployeeEntity = _mapper.Map<Employee>(employeeDto);
           
            newEmployeeEntity.DateofJoining = DateTime.Now;

            var validationResult = await _validator.CreateEmployee.ValidateAsync(newEmployeeEntity);

            if (validationResult.IsValid)
            {
                newEmployeeEntity.Password = _securityManager.GeneratePasswordHash(employeeDto.Password);

                await _unitOfWork.EmployeeRepository.AddAsync(newEmployeeEntity);
                await _unitOfWork.Complete();

                var result = _mapper.Map<EmployeeDto>(newEmployeeEntity);

                return result;
            }
            else
            {
                throw new ServiceException(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employeeEntity = _mapper.Map<Employee>(employeeDto);

            var validationResult = await _validator.UpdateEmployee.ValidateAsync(employeeEntity);

            if (validationResult.IsValid)
            {
                await _unitOfWork.EmployeeRepository.UpdateAsync(employeeEntity);
                await _unitOfWork.Complete();

                var result = _mapper.Map<EmployeeDto>(employeeEntity);

                return result;
            }
            else
            {
                throw new ServiceException(validationResult.Errors.First().ErrorMessage);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                throw new ServiceException(ValidationMessages.EmployeeNotFound);
            }

            await _unitOfWork.EmployeeRepository.RemoveAsync(employee);
            await _unitOfWork.Complete();

            return true;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesByDepartmentIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department != null)
            {
                return (List<EmployeeDto>)await _unitOfWork.EmployeeRepository.FindAsync(x => x.DepartmentId == department.DepartmentId);
            }
            else
            {
                return null;
            }
        }
    }
}
